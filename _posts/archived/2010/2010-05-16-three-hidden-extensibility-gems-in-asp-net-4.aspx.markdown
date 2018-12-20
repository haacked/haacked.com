---
title: Three Hidden Extensibility Gems in ASP.NET 4
date: 2010-05-16 -0800
tags:
- aspnet
- code
redirect_from: "/archive/2010/05/15/three-hidden-extensibility-gems-in-asp-net-4.aspx/"
---

ASP.NET 4 introduces a few new extensibility APIs that live the hermit
lifestyle away from the public eye. They’re not exactly *hidden -* they
are well documented on MSDN - but they aren’t well publicized. It’s
about time we shine a spotlight on them.

### PreApplicationStartMethodAttribute

This [new
attribute](http://msdn.microsoft.com/en-us/library/system.web.preapplicationstartmethodattribute.aspx "PreApplicationStartMethodAttribute on MSDN")
allows you to have code run way early in the ASP.NET pipeline as an
application starts up. I mean *way* early, even before
`Application_Start`.

This happens to also be before code in your *App\_code* folder (assuming
you have any code in there) has been compiled.

To use this attribute, create a class library and add this attribute as
an assembly level attribute. A common place to add this would be in the
*AssemblyInfo.cs* class within the *Properties* folder.

Here’s an example:

```csharp
[assembly: PreApplicationStartMethod(
  typeof(SomeClassLib.Initializer), "Initialize")]
```

Note that I specified a type and a method. That method needs to be a
public static void method with no arguments. Now, any ASP.NET website
that references this assembly will call the `Initialize` method when the
application is about to start, giving this method a chance to do perform
some early initialization.

```csharp
public static class Initializer
{
  public static void Initialize() { 
    // Whatever can we do here?
  }
}
```

The primary use of this feature is to enable tasks that can’t be done
within `Application_Start` because it’s too late. For example,
registering build providers and adding assembly references.

Which leads us to…

### BuildProvider.RegisterBuildProvider

As you might guess, if one of the key scenarios for the previously
mentioned feature is to allow registering build providers, well ASP.NET
better darn well allow you to register them programmatically.

Prior to ASP.NET 4, the only way to register a custom build provider was
via the `<buildproviders>` node within *web.config*. But now, you can
register them programmatically via a call to the new
`BuildProvider.RegisterBuildProvider method`.

```csharp
BuildProvider.RegisterBuildProvider(".foo", typeof(MyBuildProvider));
```

Combining the `PreApplicationStartMethodAttribute` with this method call
means that installing a build provider can be done in one step -simply
reference the assembly with the build provider and the assembly can
register it for you. Whereas before, you would have to reference the
assembly and then muck around with web.config.

I think I speak for us all when I say “Yay! Less junk in my *web.config*
trunk!”

### BuildManager.AddReferencedAssembly

[Another new
method](http://msdn.microsoft.com/en-us/library/system.web.compilation.buildmanager.addreferencedassembly.aspx "AddReferencedAssembly on MSDN")
added in ASP.NET 4 allows adding an assembly to the application’s list
of referenced assemblies. This is equivalent to adding an assembly to
the `<assemblies>` section of *web.config*.

As you might guess, this comes in handy when registering a custom build
provider. It allows you to programmatically add references to assemblies
that may be needed by your build provider.

Oh, and it’s yet another way to reduce the size of your *web.config*
file. Who doesn’t love that? :)

