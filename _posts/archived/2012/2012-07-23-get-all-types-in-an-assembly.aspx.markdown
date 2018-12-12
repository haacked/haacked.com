---
title: Get All Types in an Assembly
date: 2012-07-23 -0800
disqus_identifier: 18863
categories:
- asp.net mvc
- code
redirect_from: "/archive/2012/07/22/get-all-types-in-an-assembly.aspx/"
---

Sometimes, you need to scan all the types in an assembly for a certain reason. For example, ASP.NET MVC does this to look for potential
controllers.

One naïve implementation is to simply call `Assembly.GetTypes()` and hope for the best. But there’s a problem with this. As [Suzanne Cook points out](http://blogs.msdn.com/b/suzcook/archive/2003/08/11/57236.aspx "ReflectionTypeLoadException"),

> If a type can't be loaded for some reason during a call to
> `Module.GetTypes()`, `ReflectionTypeLoadException` will be thrown.
> `Assembly.GetTypes()` also throws this because it calls
> `Module.GetTypes()`.

In other words, if any type can’t be loaded, the entire method call blows up and you get zilch.

There’s multiple reason why a type can’t be loaded. Here’s one example:

```csharp
public class Foo : Bar // Bar defined in another unavailable assembly
{
}
```

The class `Foo` derives from a class `Bar`, but `Bar` is defined in another assembly. Here’s a non-exhaustive list of reasons why loading `Foo` might fail:

-   The assembly containing `Bar` does not exist on disk.
-   The current user does not have permission to load the assembly
    containing `Bar`.
-   The assembly containing `Bar` is corrupted and not a valid assembly.

Once again, for more details check out Suzanne’s blog post on [Debugging Assembly Loading Failures](http://blogs.msdn.com/b/suzcook/archive/2003/05/29/57120.aspx "Debugging Assembly Load Failures").

Solution
--------

As you might expect, being able to get a list of types, even if you don’t plan on instantiating instances of them, is a common and important task. Fortunately, the `ReflectionTypeLoadException` thrown when a type can’t be loaded contains all the information you need. Here’s an example of ASP.NET MVC taking advantage of this within the internal [TypeCacheUtil
class](http://aspnetwebstack.codeplex.com/SourceControl/changeset/view/eecfe803d31d#src%2fSystem.Web.Mvc%2fTypeCacheUtil.cs "TypeCacheUtil.cs") (there’s a lot of other great code nuggets if you look around [the source
code](http://aspnetwebstack.codeplex.com "ASP.NET Web Stack Source Code"))

```csharp
Type[] typesInAsm;
try
{
    typesInAsm = assembly.GetTypes();
}
catch (ReflectionTypeLoadException ex)
{
    typesInAsm = ex.Types;
}
```

This would be more useful as a generic extension method. Well the estimable [Jon Skeet](http://msmvps.com/blogs/jon_skeet/ "Jon Skeet's Blog") has you covered in this [StackOverflow answer](http://stackoverflow.com/questions/7889228/how-to-prevent-reflectiontypeloadexception-when-calling-assembly-gettypes "StackOverflow question on loading types") (slightly edited to add in parameter validation):

```csharp
public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
{
    if (assembly == null) throw new ArgumentNullException(nameof(assembly));
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

I’ve found this code to be extremely useful many times.
