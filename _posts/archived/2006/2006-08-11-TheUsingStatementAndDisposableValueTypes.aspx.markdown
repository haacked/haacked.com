---
title: The Using Statement And Disposable Value Types
date: 2006-08-11 -0800 9:00 AM
tags: [dotnet,code,concurrency,dispose]
redirect_from: "/archive/2006/08/10/TheUsingStatementAndDisposableValueTypes.aspx/"
---

A while ago [Ian
Griffiths](http://www.interact-sw.co.uk/iangblog/ "Ian Griffiths") wrote
about an [improvement to his `TimedLock`
class](http://www.interact-sw.co.uk/iangblog/2004/04/26/yetmoretimedlocking "Oh No! Not the TimedLock Again!")
in which he changed it from a `class` to a `struct`. This change
resulted in a value type that implements `IDisposable`. I had a nagging
question in the back of my mind at the time that I quickly forgot about.
The question is **wouldn’t instances of that type get boxed when calling
`Dispose`?**

So why would I wonder that? Well let’s take a look at some code and go
spelunking in IL. The following humble `struct` is the star of this
investigation.

```csharp
struct MyStruct : IDisposable
{
    public void Dispose()
    {
        Console.WriteLine("Disposing");
    }
}
```

Let’s write an application that will instantiate this struct and call
its `Dispose` method via the interface.

```csharp
public class App
{
    public void DemoDisposable()
    {
        IDisposable disposable = new MyStruct();
        DisoseIt(disposable);
    }
    
    public void DisoseIt(IDisposable disposable)
    {
        disposable.Dispose();
    }
}
```

Finally we will take our trusty
[Reflector](http://www.aisto.com/roeder/dotnet/ "Reflector") out and
examine the IL (I will leave out the method header).

```csharp
.maxstack 2
.locals init (
    [0] [mscorlib]System.IDisposable disposable1,
    [1] NeverLockThis.MyStruct struct1)
L_0000: ldloca.s struct1
L_0002: initobj NeverLockThis.MyStruct
L_0008: ldloc.1 
L_0009: box NeverLockThis.MyStruct
L_000e: stloc.0 
L_000f: ldarg.0 
L_0010: ldloc.0 
L_0011: call instance void 
    NeverLockThis.App::DisoseIt([mscorlib]System.IDisposable)
L_0016: ret 
```

Notice the bolded line has a boxing instruction. As we can see, our
struct gets boxed before the `Dispose` method is called.

The using statement requires that the object provided to it implements
`IDisposable`. Here is a snippet from the [MSDN2
docs](http://msdn2.microsoft.com/en-us/library/yh598w02.aspx "Using Statement")
on the subject.

> The using statement allows the programmer to specify when objects that
> use resources should release them. The object provided to the using
> statement must implement the IDisposable interface. This interface
> provides the Dispose method, which should release the object's
> resources.

I wondered if the using statement enforced the `IDisposable` constraint
in the same way a method would. Let’s find out. We will add the
following new method to the `App` class.

```csharp
public void UseMyStruct()
{
    MyStruct structure = new MyStruct();
    using (structure)
    {
        Console.WriteLine(structure.ToString());
    }
}
```

This code now implicitely calls the `Dispose` method via the `using`
block. Cracking it open with
[Reflector](http://www.aisto.com/roeder/dotnet/) reveals...

```csharp
.maxstack 1
.locals init (
    [0] NeverLockThis.MyStruct struct1,
    [1] NeverLockThis.MyStruct struct2)
L_0000: ldloca.s struct1
L_0002: initobj NeverLockThis.MyStruct
L_0008: ldloc.0 
L_0009: stloc.1 
L_000a: ldloca.s struct1
L_000c: constrained NeverLockThis.MyStruct
L_0012: callvirt instance string object::ToString()
L_0017: call void [mscorlib]System.Console::WriteLine(string)
L_001c: leave.s L_002c
L_001e: ldloca.s struct2
L_0020: constrained NeverLockThis.MyStruct
L_0026: callvirt instance void 
    [mscorlib]System.IDisposable::Dispose()
L_002b: endfinally 
L_002c: ret 
.try L_000a to L_001e finally handler L_001e to L_002c
```

As you can see, there is no sign of a `box` statement anywhere to be
seen. Forgive me for ever doubting you .NET team. As expected, it does
the right thing. I just had to be sure. But do realize that if you pass
in a value type that implements `IDisposable` to a method that takes in
IDisposable, a box instruction will occur.

