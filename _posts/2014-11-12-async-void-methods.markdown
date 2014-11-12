---
layout: post
title: "Avoid async void methods"
date: 2014-01-02 -0800
comments: true
categories: [csharp async]
---

In C#, it's a good idea to avoid `async void` methods. They really are a scourge upon your code. Stephen Cleary has a pretty detailed article in [MSDN magazine that describes why they are to be avoided](http://msdn.microsoft.com/en-us/magazine/jj991977.aspx).

I found another reason recently. I was investigating a bug and noticed that the unit test that should have caught the bug was passing. That's odd.

Then I noticed that the return type of the method was `async void`. I changed it to `async Task` and it started to fail. Ohhhhh shit.

If you write unit tests using XUnit.NET and accidentally mark them as `async void` instead of `async Task`, the tests are effectively ignored. I learned the hard way we had failing tests that were "passing" our test runs. Ouch!

Pretty much the only valid reason to use `async void` methods is in the case where you need an asynchronous event handler. But if you use Reactive Extensions, there's an even better approach that I've [written about before, `Observable.FromEventPattern`](http://haacked.com/archive/2012/04/09/reactive-extensions-sample.aspx/).

Because there are valid reasons for `async void` methods, Code analysis won't flag them. For example, Code Analysis doesn't flag the following method.

```csharp
public async void Foo()
{
    await Task.Run(() => {});
}
```

Well, it's pretty easy to search for that method. But there are subtle ways where `async void` methods crop up. For example, take a look at the following code.

```csharp
new Subject<Unit>().Subscribe(async _ => await Task.Run(() => {}));
```

Looks legit, right? You are wrong my friend. Take a shot of whiskey. If you look at all the overloads of `Subscribe` you'll see that we're calling one that takes in an `Action<T>` and not a `Func<T, Task>`. In other words, we've unwittingly passed in an `async void` lambda.

## Here I Come To Save The Day

Clearly I should tighten up code reviews to keep an eye out for this problem, right? Hell nah! Let a computer do this crap for you. I wrote some integrity tests that look out for this problem.

These tests make use of this method [I wrote a while back to grab all loadable types](http://haacked.com/archive/2012/07/23/get-all-types-in-an-assembly.aspx/) from an assembly.


```csharp
public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
{
    if (assembly == null) throw new ArgumentNullException("assembly");
    try
    {
        return assembly.GetTypes();
    }
    catch (ReflectionTypeLoadException e)
    {
        return e.Types.Where(t => t != null);
    }
}
```

I also wrote this other extension method to make the final result a bit cleaner.

```csharp
public static bool HasAttribute<TAttribute>(this MethodInfo property) where TAttribute : Attribute
{
    return property.GetCustomAttributes(typeof(TAttribute), false).Any();
}
```

And the power of Reflection compels you! Here's a method that will return every public `async void` method or lambda in an assembly.

```csharp
public static IEnumerable<MethodInfo> GetAsyncVoidMethods(this Assembly assembly)
{
    return assembly.GetLoadableTypes()
      .SelectMany(type => type.GetMethods(
        BindingFlags.NonPublic
        | BindingFlags.Public
        | BindingFlags.Instance
        | BindingFlags.Static
        | BindingFlags.DeclaredOnly))
      .Where(method => method.HasAttribute<AsyncStateMachineAttribute>())
      .Where(method => method.ReturnType == typeof(void));
}
```

And using this method, I can write a helper method for all my unit tests.

```csharp
public static void AssertNoAsyncVoidMethods(Assembly assembly)
{
    var messages = assembly
        .GetAsyncVoidMethods()
        .Select(method =>
            String.Format("'{0}.{1}' is an async void method.",
                method.DeclaringType.Name,
                method.Name))
        .ToList();
    Assert.False(messages.Any(),
        "Async void methods found!" + Environment.NewLine + String.Join(Environment.NewLine, messages));
}
```

Here's an example where I use this method.

```csharp
[Fact]
public void EnsureNoAsyncVoidTests()
{
    AssertExtensions.AssertNoAsyncVoidMethods(GetType().Assembly);
    AssertExtensions.AssertNoAsyncVoidMethods(typeof(Foo).Assembly);
    AssertExtensions.AssertNoAsyncVoidMethods(typeof(Bar).Assembly);
}
```
Here's an example of the output. In this case, it found two `async void` lambdas.

```bash
------ Test started: Assembly: GitHub.Tests.dll ------

Test 'GitHub.Tests.IntegrityTests.EnsureNoAsyncVoidTests' failed: Async void methods found!
'<>c__DisplayClass10.<RetrievesOrgs>b__d' is an async void method.
'<>c__DisplayClass70.<ClearsExisting>b__6f' is an async void method.
	IntegrityTests.cs(104,0): at GitHub.Tests.IntegrityTests.EnsureNoAsyncVoidTests()

0 passed, 1 failed, 0 skipped, took 0.97 seconds (xUnit.net 1.9.2 build 1705).
```

My hope is this code should help us avoid accidentally making this mistake again. It's really subtle and easy to miss during code review if you're not careful. Happy coding!
