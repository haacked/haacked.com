---
title: A Stable Application Entry Point - Rethinking &quot;Main()&quot;.
tags: [dotnet]
redirect_from: "/archive/2005/03/28/a-stable-application-entry-point--rethinking-main.aspx/"
---

Jason Clark [enlightens us](http://wintellect.com/WEBLOGS/wintellect/archive/2005/03/30/941.aspx)
on creating a stable application entry point at the wintellect blog. I highly recommend reading the whole discussion, but one of the key takeaways is...

> **A Stable Entry Method**
>
> Fortunately it doesn’t take too much effort to make your entry method
> stable. Here are some guidelines:
>
> 1.  Avoid type-loads of any kind in Main. Main should only call
>     methods implemented in the same type definition.
> 2.  Do not implement a static constructor in the type that contains
>     Main. (Avoiding static fields entirely is the safest way to go).
> 3.  Derive the type that contains Main from Object. This means that
>     you need to hoist Main out of the type definition for your main
>     form if you use Visual Studio .Net wizards to create your project.
> 4.  Keep the type that contains Main focused on getting your
>     application started. Don’t think of it as the “Application” type
>     that holds your application-level state.

*[Via
[Wintellog](http://wintellect.com/WEBLOGS/wintellect/archive/2005/03/30/941.aspx)]*

