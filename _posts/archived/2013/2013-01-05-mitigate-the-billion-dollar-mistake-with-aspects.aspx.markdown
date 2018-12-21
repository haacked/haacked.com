---
title: Mitigate The Billion Dollar Mistake with Aspects
date: 2013-01-05 -0800
tags:
- code
redirect_from: "/archive/2013/01/04/mitigate-the-billion-dollar-mistake-with-aspects.aspx/"
---

[Tony Hoare](http://en.wikipedia.org/wiki/Tony_Hoare "Tony Hoare"), the
computer scientist who implemented null references in ALGOL calls it his
“billion-dollar mistake.”

> **I call it my billion-dollar mistake.** It was the invention of the
> null reference in 1965. At that time, I was designing the first
> comprehensive type system for references in an object oriented
> language (ALGOL W). My goal was to ensure that all use of references
> should be absolutely safe, with checking performed automatically by
> the compiler. But I couldn't resist the temptation to put in a null
> reference, simply because it was so easy to implement. This has led to
> innumerable errors, vulnerabilities, and system crashes, which have
> probably caused a billion dollars of pain and damage in the last forty
> years.

It may well be that a billion is a vast underestimate. But if you’re
going to make a mistake, might as well go big. Respect!

To this day, we pay the price with tons of boilerplate code. For
example, it’s generally good practice to add guard clauses for each
potentially null parameter to a public method.

```csharp
public void SomeMethod(object x, object y) {
  // Guard clauses
  if (x == null)
    throw new ArgumentNullException("x");
  if (y == null)
    throw new ArgumentNullException("y");

  // Rest of the method...
}
```

While it may feel like unnecessary ceremony, Jon Skeet gives some good
reasons why guard clauses like this are a good idea in [this
StackOverflow
answer](http://stackoverflow.com/questions/7585493/null-parameter-checking-in-c-sharp "Null Parameter Checking"):

> Yes, there are good reasons:
>
> -   It identifies exactly what is null, which may not be obvious from
>     a `NullReferenceException`
> -   It makes the code fail on invalid input even if some other
>     condition means that the value isn't dereferenced
> -   It makes the exception occur *before* the method could have any
>     other side-effects you might reach before the first dereference
> -   It means you can be confident that if you pass the parameter into
>     something else, you're not violating *their*contract
> -   It documents your method's requirements (using [Code
>     Contracts](http://research.microsoft.com/contracts) is even better
>     for that of course)

I agree. The guard clauses are needed, but it’s time for some Real
Talk™. This is [shit
work](http://zachholman.com/posts/shit-work/ "Don't give your users shit work").
And I hate shit work.

In this post,

-   I’ll explain the idea of non-nullable parameters and why I didn’t
    use CodeContracts in the hopes that heads off the first 10 comments
    asking “why didn’t you use CodeContracts dude?”
-   I’ll cover an approach using PostSharp to automatically validate
    null arguments.
-   I’ll then explain how I hope to create an even better approach.

Stick with me.

Non Null Parameters
-------------------

With .NET languages such as C#, there’s no way to prevent a caller of a
method from passing in a null value to a reference type argument.
Instead, we simply end up having to validate the passed in arguments and
ensure they’re not null.

In practice (at least with my code), the number of times I want to allow
a null value is far exceeded by the number of times a null value is not
valid. What I’d really like to do is invert the model. By default, a
parameter cannot be null unless I explicitly say it can. In other words,
make allowing null opt-in rather than opt-out as it is today.

I recall that there was some experimentation around this by Microsoft
with the Spec\# language that introduced a syntax to specify that a
value cannot be null. For example…

```csharp
public void Foo(string! arg);
```

…defines the argument to the method as a non-nullable string. The idea
is this code would not compile if you attempt to pass in a null value
for `arg`. It’s certainly not a trivial change as Craig Gidney [writes
in this
post](http://twistedoakstudios.com/blog/Post330_non-nullable-types-vs-c-fixing-the-billion-dollar-mistake "Fixing the Billion Dollar Mistake").
He covers many of the challenges in adding a non-nullable syntax and
then goes further to provide a proposed solution.

C# doesn’t have such a syntax, but it does have [Code
Contracts](http://msdn.microsoft.com/en-us/devlabs/dd491992.aspx "Code Contracts").
After reading up on it, I really like the idea, but for me it suffers
from one fatal flaw. There’s no way to apply a contract globally and
then opt-out of it in specific places. I still have to apply the
Contract calls to every potentially null argument of every method. In
other words, it doesn’t satisfy my requirement to invert the model and
make allowing null opt in rather than opt out. It’s still shit work.
It’s also error-prone and I’m too lazy a bastard to get it right in
every case.

IL Rewriting to the Rescue
--------------------------

So I figured I’d go off the deep end and experiment with Intermediate
Language (IL)weaving [with
PostSharp](http://www.sharpcrafters.com/postsharp/features "PostSharp")
to insert guard clauses automatically. Usually, any time I think about
rewriting IL, I take a hammer to my head until the idea goes away. A few
good whacks is plenty. However in this case, I thought it’d be a fun
experiment to try. Not to mention I have a very hard head.

I chose to use PostSharp because it’s easy to get started with and it
provides a simple, but powerful, API. It does have a few major downsides
for what I want to accomplish that I’ll cover later.

I wrote an aspect, `EnsureNonNullAspect`, that you apply to a method, a
class, or an assembly that injects on null checks for all public
arguments and return values in your code. You can then opt out of the
null checking using the `AllowNullAttribute`.

Here’s some examples of usage:

```csharp
using NullGuard;

[assembly: EnsureNonNullAspect]

public class Sample 
{
    public void SomeMethod(string arg) {
        // throws ArgumentNullException if arg is null.
    }

    public void AnotherMethod([AllowNull]string arg) {
        // arg may be null here
    }

    public string MethodWithReturn() {
        // Throws InvalidOperationException if return value is null.
    }
   
    // Null checking works for automatic properties too.
    public string SomeProperty { get; set; }

    [AllowNull] // can be applied to a whole property
    public string NullProperty { get; set; }

    public string NullProperty { 
        get; 
        [param: AllowNull] // Or just the setter.
        set; 
}
```

For more examples, check out the [automated
tests](https://github.com/Haacked/NullGuard/blob/master/Tests/EnsureNonNullAspectFacts.cs "NullGuard tests")
in the [NullGuard GitHub
repository](https://github.com/haacked/NullGuard "NullGuard").

By default, the attribute only works for public properties, methods, and
constructors. It also validates return values, out parameters, and
incoming arguments.

If you need more fine grained control of what gets validated, the
`EnsureNonNullAspect` accepts a `ValidationFlags` enum. For example, if
you only want to validate arguments and not return values, you can
specify: `[EnsureNonNullAspect(ValidationFlags.AllPublicArguments)]`.

Downsides
---------

This approach requires that the NullGuard and PostSharp libraries are
redistributed with the application. Also, the generated code is a bit
verbose. Here’s an example of the [generated code of a previously one
line
method](https://gist.github.com/4458752 "Gist: Postsharp Generated Code").

Another downside is that you’ll need to install the PostSharp Visual
Studio extension and register for a license before you can fully use my
library. The license for the free community edition is free, but it does
add a bit of friction just to try this out.

I’d love to see PostSharp add support for generating IL that’s
completely free of dependencies on the PostSharp assemblies. Perhaps by
injecting just enough types into the rewritten assembly so it’s
standalone.

Try it!
-------

To try this out, install the **NullGuard.PostSharp** package from
NuGet.  (It’s a pre-release library so make sure you include preleases
when you attempt to install it).

```csharp
Install-Package NullGuard.PostSharp –IncludePrelease
```

Make sure you also install the [PostSharp Visual Studio
extension](http://www.sharpcrafters.com/postsharp/download "PostSharp Download").

When you install the NuGet package into a project, it should modify that
project to use PostSharp. If not, you’ll need to add an MSBuild task to
run PostSharp against your project. Just look at *[Tests.csproj
file](https://github.com/Haacked/NullGuard/blob/master/Tests/Tests.csproj "Tests.csproj")*
in the [NullGuard
repository](https://github.com/haacked/NullGuard "https://github.com/haacked/NullGuard")
for an example.

If you just want to see things working, clone the [NullGuard
repository](https://github.com/haacked/NullGuard "NullGuard") and run
the unit tests.

File an issue if you have ideas on how to improve it or anything that’s
wonky.

Alternative Approaches and What’s Next?
---------------------------------------

NullGuard.PostSharp is really an experiment. It’s my first iteration in
solving this problem. I think it’s useful in its current state, but
there are much better approaches I want to try.

-   **Use** [**Fody**](https://github.com/Fody/Fody "Fody") **to
    write the guard blocks.** Fody is an IL Weaver tool written by
    [Simon Cropp](http://simoncropp.com/ "Simon Cropp's Blog") that
    provides an MSBuild task to rewrite IL. The benefit of this approach
    is there is no need to redistribute parts of Fody with the
    application. The downside is Fody is much more daunting to use as
    compared to PostSharp. It leverages Mono.Cecil and requires a decent
    understanding of IL. Maybe I can convince Simon to help me out here.
    In the meanwhile, I better start reading up on IL. I think this will
    be the next approach I try. **UPDATE:** Turns out that in response
    to this blog post, the Fody team wrote
    [NullGuard.Fody](https://github.com/Fody/NullGuard) that
    does exactly this!
-   **Use T4 to rewrite the source code.** Rather than rewrite the IL,
    another approach would be to rewrite the source code much like T4MVC
    does with T4 Templates. One benefit of this approach is I could
    inject code contracts and get all the benefits of having them
    declared in the source code. The tricky part is doing this in a
    robust manner that doesn’t mess up the developer’s workflow.
-   **Use Roslyn.** It seems to me that Roslyn should be great for this.
    I just need to figure out how exactly I’d incorporate it. Modify
    source code or update the IL?
-   **Beg the Code Contracts team to address this scenario.** Like the
    Temptations, I ain’t too proud to beg.

Yet another alternative is to embrace the shit work, but write an
automated test that ensures every argument is properly checked. I
started working on a method you could add to any unit test suite that’d
verify every method in an assembly, but it’s not done yet. It’s a bit
tricky.

If you have a better solution, do let me know. I’d love for this to be
handled by the language or Code Contracts, but right now those just
don’t cut it yet.

