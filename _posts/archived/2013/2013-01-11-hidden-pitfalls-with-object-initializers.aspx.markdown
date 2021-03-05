---
title: Hidden Pitfalls With Object Initializers
tags: [code]
redirect_from: "/archive/2013/01/10/hidden-pitfalls-with-object-initializers.aspx/"
---

I love automation. I’m pretty lazy by nature and the more I can offload
to my little programmatic or robotic helpers the better. I’ll be sad the
day they become self-aware and decide that it’s payback time and enslave
us all.

But until that day, I’ll take advantage of every bit of automation that
I can.

![the-matrix-humans](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/fb03762ddce8_F2D1/the-matrix-humans_3.png "the-matrix-humans")

For example, I’m a big fan of the Code Analysis tool built into Visual
Studio. It’s more more commonly known as FxCop, though judging by the
language I hear from its users I’d guess it’s street name is “YOU BIG
PILE OF NAGGING SHIT STOP WASTING MY TIME AND REPORTING THAT VIOLATION!”

Sure, it has its peccadilloes, but with the right set of rules, it’s
possible to strike a balance between a total nag and a helpful
assistant.

As a developer, it’s important for us to think hard about our code and
take care in its crafting. But we’re all fallible. In the end, I’m just
not smart enough to remember **ALL the possible pitfalls of coding ALL
OF THE TIME** such as avoiding the [Turkish I
problem](https://haacked.com/archive/2012/07/05/turkish-i-problem-and-why-you-should-care.aspx "The Turkish I problem")
when comparing strings. If you are, more power to you!

I try to keep the number of rules I exclude to a minimum. It’s saved my
ass many times, but it’s also strung me out in a harried attempt to make
it happy. Nothing pleases it. Sure, when it gets ornery, it’s easy to
suppress a rule. I try hard to avoid that because suppressing one
violation sometimes hides another.

Here’s an example of a case that confounded me today. The following very
[straightforward looking
code](https://gist.github.com/4507192 "Same code on Gist") ran afoul of
a code analysis rule.

```csharp
public sealed class Owner : IDisposable
{
    Dependency dependency;

    public Owner()
    {
        // This is going to cause some issues.
        this.dependency = new Dependency { SomeProperty = "Blah" };
    }

    public void Dispose()
    {
        this.dependency.Dispose();
    }
}

public sealed class Dependency : IDisposable
{
    public string SomeProperty { get; set; }

    public void Dispose()
    {
    }
}
```

Code Analysis reported the following violation:

> CA2000 **Dispose objects before losing scope** \
> In method 'Owner.Owner()', object '\<\>g\_\_initLocal0' is not
> disposed along all exception paths. Call System.IDisposable.Dispose on
> object '\<\>g\_\_initLocal0' before all references to it are out of
> scope.

That’s really odd. As you can see, dependency is disposed when its owner
is disposed. So what’s the deal?

Can you see the problem?

A Funny Thing about Object Initializers
---------------------------------------

[![A2600\_Pitfall](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/fb03762ddce8_F2D1/A2600_Pitfall_thumb.png "A2600_Pitfall")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/fb03762ddce8_F2D1/A2600_Pitfall_2.png)The
big clue here is the name of the variable that’s not disposed,
`<>g__initLocal0`. As Phil Karlton once said, emphasis mine,

> There are only two hard things in Computer Science: cache invalidation
> and **naming things**.

Naming may be hard, but I can do better than that. Clearly the compiler
came up with that name, not me. I fired up Reflector to see the
generated code. The following is the constructor for Owner.

```csharp
public Owner()
{
    Dependency <>g__initLocal0 = new Dependency {
        SomeProperty = "Blah"
    };
    this.dependency = <>g__initLocal0;
}
```

Aha! So we can see that the compiler generated a temporary local
variable to hold the initialized object while its properties are set,
before assigning it to the member field.

So what’s the problem? Well if for some reason setting `SomeProperty`
throws an exception, `<>g__initiLocal0` will never be disposed. That’s
what the Code Analysis is complaining about. Note that if an exception
is thrown while setting that property, my member field is also never set
to the instance. So it’s a dangling undisposed instance.

So what’s the Plan Stan?
------------------------

Well the fix to keep code analysis happy is simple in this case.

```csharp
public Owner()
{
    this.dependency = new Dependency();
    this.dependency.SomeProperty = "Blah";
}
```

Don’t use the initializer and set the property the old fashioned way.

This shuts up CodeAnalysis, but did it really solve the problem? Not in
this specific case because we happen to be inside a constructor. If the
`Owner` constructor throws, nobody is going to dispose of the
dependency.

As Greg Beech wrote so long ago,

> From this we can ascertain that if the object is not constructed
> correctly then the reference to the object will not be assigned, which
> means that no methods can be called on it, so the Dispose method
> cannot be used to deterministically clean up managed resources. **The
> implication here is that if the constructor creates expensive managed
> resources which need to be cleaned up at the earliest opportunity then
> it should do so in an exception handler within the constructor as it
> will not get another chance.**

So a more robust approach would be the following.

```csharp
public Owner()
{
    this.dependency = new Dependency();
    try
    {
        this.dependency.SomeProperty = "Blah";
    }
    catch (Exception)
    {
        dependency.Dispose();
        throw;
    }       
}
```

This way, if setting the properties of `Dependency` throws an exception,
we can dispose of it properly.

Why isn’t the compiler smarter?
-------------------------------

I’m not the first to run into this pitfall with object initializers and
disposable instances. [Ayende](http://ayende.com/blog "Ayende") wrote
about a related issue with [using blocks and object
initializers](http://ayende.com/blog/3810/avoid-object-initializers-the-using-statement "Avoid object initializers in using blocks")
back in 2009. In that post, he suggests the compiler should generate
safe code for this scenario.

It’s an interesting question. Whenever I think of such questions, I put
on my [Eric Lippert](http://ericlippert.com/ "Eric Lippert's Blog") hat
and hear his proverbial voice (I’ve never heard his actual voice but I
imagine it to be sonorous and profound) in my head
[saying](http://blogs.msdn.com/b/ericlippert/archive/2012/04/03/10251901.aspx "Why not automatically infer constraints?"):

> I'm often asked why the compiler does not implement this feature or
> that feature, and of course the answer is always the same: **because
> no one implemented it.** Features start off as unimplemented and only
> become implemented when people spend effort implementing them: no
> effort, no feature. This is an unsatisfying answer of course, because
> usually the person asking the question has made the assumption that
> the feature is so *obviously good* that we need to have had a reason
> to**not** implement it. I assure you that no, we actually don't need a
> reason to not implement any feature no matter how obviously good. But
> that said, it might be interesting to consider what sort of pros and
> cons we'd consider if asked to implement the "silently put inferred
> constraints on class type parameters" feature.

The current implementation of object initializers seems correct for most
cases. The only time it breaks down is in the case of disposable types.
So let’s think about some possible solutions.

### Why the intermediate variable?

First, let’s look at why the intermediate local variable. My initial
knee-jerk reaction (ever notice how often your knee-jerk reaction makes
you sound like jerk?) was that the intermediate variable is unecessary.
But I thought about it some more and came up with the scenario. Suppose
we’re setting a property to the value of an object created via an
initializer.

```csharp
this.SomePropertyWithSideEffects = new Dependency { Prop = 42 };
```

The way to do this without an intermediate local variable is the
following.

```csharp
this.SomePropertyWithSideEffects = new Dependency();
this.SomePropertyWithSideEffects.Prop = 42;
```

The first code block only calls the setter of
`SomePropertyWithSideEffects`. But the second code block calls both the
getter and setter. That’s pretty different behavior.

Now imagine we’re setting multiple properties or using a collection
initializer with multiple items instead. We’d be calling that property
getter multiple times. Who knows what awful side-effects that might
produce. Sure, side effects in property getters are bad, but as I’ll
point out later, there’s another reason this approach is fraught with
error.

The intermediate local variable is necessary to ensure the object is
only assigned after it’s fully constructed.

### Dispose it for me?

So given that, let’s try implementing the the `Owner` constructor of my
previous example the way a compiler might do it.

```csharp
public Owner()
{
    var <>g__initLocal0 = new Dependency();
    try
    {
        <>g__initLocal0.SomeProperty = "Blah";
    }
    catch (Exception)
    {
        <>g__initLocal0.Dispose();
        throw;
    }
    this.dependency = <>g__initLocal0;
}
```

That’s certainly seems much safer, but there’s still a potential flaw.
It’s optimistically calling dispose on the object when the exception is
thrown. What if I didn’t want to call dispose on it even though it’s
disposable? Maybe the `Dispose` method of this specific object deletes
your hard-drive and plays Justin Bieber music when invoked. In 99.9
times out of 100, you would want `Dispose` called in this case. But this
is still a change in behavior and I can understand why the compiler
might not risk it.

Perhaps the compiler could attempt to figure out if that instance
eventually gets disposed and do the right thing. All you have to do find
a flaw in Turing’s proof of [the Halting
Problem](http://en.wikipedia.org/wiki/Halting_problem "The Halting Problem").
No problem, right?

Perhaps we could be satisfied with good enough. Dispose it always and
just say that’s the behavior of object initializers. It’s probably too
late for that change as that’d be a breaking change. It’d be one I could
live with honestly.

### Let me dispose it

Perhaps the problem isn’t that we want the compiler to automatically
dispose of the intermediate object in the case of an exception. What we
really want is the assignment to  happen no matter what so we can
dispose of it in our code if an exception is thrown. Perhaps the
compiler can generate code that would allow us to do this in *our code*.

```csharp
public Owner()
{
    try
    {
        this.dependency = new Dependency { SomeProperty = "blah" };
    }
    catch (Exception)
    {
        if (this.dependency != null)
            this.dependency.Dispose();
    }
}
```

What might the generated code look like in this case?

```csharp
public Owner()
{
    var <>g__initLocal0 = new Dependency();
    this.dependency = <>g__initLocal0;
    <>g__initLocal0.SomeProperty = "Blah";
}
```

That’s not too shabby. We got rid of the try/catch block that we had to
introduce previously, and if an exception is thrown in the property
setter, we can clean up after it. I’m a genius!

Not so fast Synkowski. There’s a potential problem here. Suppose we’re
not inside a constructor, but rather are in a method that’s setting a
shared member.

```csharp
public void DoStuff()
{
    var <>g__initLocal0 = new Dependency();
    this.dependency = <>g__initLocal0;
    <>g__initLocal0.SomeProperty = "Blah";
}
```

We’ve now introduced a possible race condition if this method is used in
an async or multithreaded environment.

Notice that after `this.dependency` is set to the local incomplete
instance, but before the local property is set, there’s room for another
thread to modify `this.dependency` in some way right in that gap leading
to indeterminate results. That’s definitely a change you wouldn’t want
the compiler doing.

In fact, this same issue affects my earlier proposal of not using an
intermediate variable.

So about that Code Analysis
---------------------------

Note that I didn’t specifically address Ayende’s case. In his case, the
initializer is in a using block. That seems like a more tractable
problem for the compiler to solve, but this post is getting long as it
is and it’s time to wrap up. Maybe someone else can analyze that case
for shits and giggles.

In my case, we’re setting a member that we plan to dispose later. That’s
a much harder (if not impossible) nut to crack.

And here we get to the moral of the story. I get a lot more work done
when I don’t stop every hour to write a blog post about some interesting
bug I found in my code.

No wait, that’s not it.

The point here is that code analysis is a very helpful tool for writing
more robust and correct code. But it’s just an assistant. It’s not a
safety net. It’s more like an air bag. It’ll keep you from splattering
your brains on the dashboard, but you can still totally wreck your car
and break that nose if you’re not careful at the wheel.

Here’s an example where automated tools can both lead you into an
accident, but save your butt at the last second.

If you use Resharper (another tool with its own automated analysis) like
I do and you write some code in a constructor that doesn’t use an object
initializer, you’re very likely to see this (with the default settings).

[![resharper-nag](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/fb03762ddce8_F2D1/resharper-nag_thumb_1.png "resharper-nag")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/fb03762ddce8_F2D1/resharper-nag_4.png)

See that green squiggly under the `new` keyword just inviting, no
**begging**, you to hit `ALT+ENTER` and convert that bad boy into an
object initializer? Go ahead, it seems to suggest. What could go wrong?
Oh it could cause you to now leak a resource as pointed out earlier.

I often like to hit `CTRL E + CTRL C` in Resharper to reformat my entire
source file to be consistent with my coding standards. Depending on how
you set up the reformatting, such an automatic action could easily
change this code from working code to subtly broken code.

I still have to pay careful attention to what it’s doing. It’s easy to
get lulled into a sense of safety when performing automatic
refactorings. But you can’t trust it one hundred percent. You are the
one who is responsible, not the tools. You are the one in control.

Fortunately in this case, Code Analysis brought this issue to my
attention. And in doing so, pointed out an interesting topic for a blog
post. Yay automation!

