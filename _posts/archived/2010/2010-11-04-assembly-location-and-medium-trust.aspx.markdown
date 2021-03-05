---
title: A Sordid Little Tale Of Unexpected Security Exceptions
tags: [code,aspnet,security]
redirect_from: "/archive/2010/11/03/assembly-location-and-medium-trust.aspx/"
---

It was a dark and stormy coding session; the rain fell in torrents as my
eyes were locked to two LCD screens in a furious display of coding …

[![stormy](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/775597446ef4_D206/stormy_3.jpg "stormy")](http://www.sxc.hu/photo/1302654 "Photo by Roger Kirby")

…sorry sorry, I just can’t continue. It’s all a lie.

This actually a cautionary tale describing one subtle way that you can
run afoul Code Access Security (CAS) when attempting to run an
application in partial trust. But who wants to read about that? Right?
Right?

Well this isn’t a sordid tale, but if you bear with me, you may just
find it interesting. Either that, or you may just take pity on me that I
find this type of thing interesting.

I was hacking on [NuGet](http://nuget.codeplex.com/ "NuGet Project") the
other day and all I wanted to do was write some code that accessed the
version number of the current assembly. This is something we do in
[Subtext](http://subtextproject.com/ "Subtext"), for example. If you
scroll to the very bottom of the admin section, you’ll see the
following.

![Subtext Admin - Feedback - Google
Chrome](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/775597446ef4_D206/Subtext%20Admin%20-%20Feedback%20-%20Google%20Chrome_753585da-76b5-4af2-9b42-e74c0bae6c28.png "Subtext Admin - Feedback - Google Chrome")

As you can imagine, the code for to get the version number is very
straightforward:

```csharp
System.Reflection.Assembly.ExecutingAssembly().GetName().Version
```

Or is it!? (cue scary organ music)

What the code does here (besides appearing to smack [the Law of
Demeter](https://haacked.com/archive/2009/07/14/law-of-demeter-dot-counting.aspx "Law of Demeter")
in the mouth) is get the currently executing assembly. From that it gets
the Assembly name and extracts the version from the name. What could go
wrong? I tested this in medium trust and it received the “works on my
machine” seal of approval!

![](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UsefulMVC2UpgradeTip_EC18/works-on-my-machine_3.png)

But does it work **all the time**? Well if it did, I wouldn’t be writing
this blog post would I?

Fortunately, my colleague [David
Fowler](http://weblogs.asp.net/davidfowler/ "David Fowler's blog")
caught this latent bug during a code review. Levi (no blog) Broderick
was brought in to help explain the whole issue so a dunce like me could
understand it. These two co-workers are scary smart and must never be
allowed to fall into a life of crime as they would decimate the
countryside. Just letting you know.

As it turns out, code exactly like this was the source of a medium trust
bug in ASP.NET MVC 2 (that we fortunately caught and fixed before RTM).
So what gives?

Well there’s very subtle latent bug with this code. To illustrate, I’ll
put the code in context. The following snippet is a class library that
makes use of the code I just wrote.

```csharp
using System.Reflection;
using System.Security; 
```

```csharp
[assembly: SecurityTransparent] 
namespace ClassLibrary1 {
  public static class Class1 {
    public static string GetExecutingAssemblyVersion() {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
  }
}
```

We need an application to reference that code. The following is code for
an ASP.NET MVC controller with an action method that calls the method in
the class library and returns it as a string. It may seem odd that the
action method returns a string rather than an `ActionResult`, but that’s
allowed. ASP.NET MVC simply wraps it in a `ContentResult`.

```csharp
using System.Web.Mvc;

namespace MvcApplication1.Controllers {
  public class HomeController : Controller {
        public string ClassLibAssemblyVersion() {
            return ClassLibrary1.Class1.GetExecutingAssemblyVersion();
        }
    }
}
```

Still with me?

When I run this application and visit */Home/ClassLibAssemblyVersion*
everything works fine and we see the version number.

![httplocalhost29519homeClassLibAssemblyVersionFixed - Windows Internet
Explorer](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/775597446ef4_D206/httplocalhost29519homeClassLibAssemblyVersionFixed%20-%20Windows%20Internet%20Explorer_19fefe6c-3d6b-4329-89e8-1a4b7cb8ad6f.png "httplocalhost29519homeClassLibAssemblyVersionFixed - Windows Internet Explorer")

Now’s where the party gets a bit wild (but still safe for work). At this
point, I’ll put the class library assembly in the GAC and then recompile
the application. I’m going to assume you know how to do that. Note that
I’ll need to remove the local copy of the class library from the bin
directory of my ASP.NET MVC application and also remove the project
reference and replace it with a GAC reference.

When I do that and run the application again, I get.

![security-exception](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/775597446ef4_D206/security-exception_7d87cfe5-1568-43e4-b627-0fbbdb9a8c28.png "security-exception")

Oh noes!

So what happened here? Reflector to the rescue! Looking at the stack
trace, let’s dig into `RuntimeAssembly.GetName(Boolean copiedName)`
method.

```csharp
[SecuritySafeCritical]
public override AssemblyName GetName(bool copiedName) {
    AssemblyName name = new AssemblyName();
    string codeBase = this.GetCodeBase(copiedName);
    this.VerifyCodeBaseDiscovery(codeBase);
    
    // ... snipped for brevity ...

    return name;
}
```

I’ve snipped out some code so we can focus on the interesting part. This
method wants to return a fully populated `AssemblyName` instance. One of
the properties of `AssemblyName` is `CodeBase`, which is a path to the
assembly.

Once it has this path, it attempts to verify the path by calling
`VerifyCodeBaseDiscovery`. Let’s take a look.

```csharp
[SecurityCritical]
private void VerifyCodeBaseDiscovery(string codeBase)
{
    if ((codeBase != null) && 
      (string.Compare(codeBase, 0, "file:", 0, 5
        , StringComparison.OrdinalIgnoreCase) == 0))
    {
        URLString str = new URLString(codeBase, true);
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery
          , str.GetFileName()).Demand();
    }
}
```

Notice that last line of code? It’s making a security demand to check if
you have path discovery permissions on the specified path. That’s what’s
failing. Why?

Well before you put the assembly in the GAC, the assembly was being
loaded from your bin directory. Naturally, even in medium trust, you
have rights to discover that path. But now that the class library is in
the GAC, it’s being loaded from a subdirectory of
*c:\\Windows\\Assembly* and guess what. Your medium trust application
doesn’t have path discovery permissions to that directory.

As an aside, I think it’s too bad that this particular property doesn’t
check its security demand lazily. That would be my kind of property
access. My gut feeling is that people don’t often ask for an assembly’s
`Codebase` as much as they ask for the other “safe” properties, like
`Version`!

So how do we fix this? Well the answer is to construct our own
`AssemblyName` instance.

```csharp
new AssemblyName(typeof(Class1).Assembly.FullName).Version.ToString();
```

This implementation avoids the security issue I mentioned earlier
because we’re generating the `AssemblyName` instance ourselves and it
never has a reference to the disallowed path.

If you want to see this in action, I put together a [**little
demo**](http://code.haacked.com/mvc-2/MedTrustTestSolution.zip "A little demo.")
showing the bad approach and the fixed approach**.**

You’ll need to GAC the *ClassLibrary1* assembly to see the exception
occurred. I have another action that has the safe implementation. Try it
out.

As a tangent, the astute reader may have noticed that I used the
assembly level `SecurityTransparentAttribute` in my class library. Is
that a case of my assembly attempting to deal with self esteem issues
and shying away from a clamoring public? Why did I put that attribute
there? The answer to that, my friends, is a story for another time.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/775597446ef4_D206/wlEmoticon-smile_2.png)

