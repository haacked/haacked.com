---
title: Html Encoding Nuggets With ASP.NET MVC 2
tags: [aspnet,code,aspnetmvc]
redirect_from: "/archive/2009/11/02/html-encoding-nuggets-aspnetmvc2.aspx/"
---

This is the second in a three part series related to HTML encoding
blocks, aka the `<%: ... %>` syntax.

-   [Html Encoding Code Blocks With ASP.NET
    4](https://haacked.com/archive/2009/09/25/html-encoding-code-nuggets.aspx "Html Encoding Blocks")
-   **Html Encoding Nuggets With ASP.NET MVC 2**
-   [Using AntiXss as the default encoder for
    ASP.NET](https://haacked.com/archive/2010/04/06/using-antixss-as-the-default-encoder-for-asp-net.aspx "Using AntiXSS")

In a recent blog post, I introduced [ASP.NET 4’s new HTML Encoding code
block
syntax](https://haacked.com/archive/2009/09/25/html-encoding-code-nuggets.aspx "Html Encoding Code Blocks")
as well as the corresponding `IHtmlString` interface and `HtmlString`
class. I also mentioned that ASP.NET MVC 2 would support this new syntax
***when running on ASP.NET 4***.

In fact, you can [try it out
now](https://haacked.com/archive/2009/10/20/vs10beta2-and-aspnetmvc.aspx "VS10 Beta 2 from an ASP.NET MVC perspective")
by downloading and installing Visual Studio 2010 Beta 2.

I’ve also mentioned in the past that we are not conditionally compiling
ASP.NET MVC 2 for each platform. Instead, we’re building
`System.Web.Mvc.dll` against ASP.NET 3.5 SP1 and simply including that
one in VS08 and VS10. Thus when you’re running ASP.NET MVC 2 on ASP.NET
4, it’s the same byte for byte assembly as the same one you would run on
ASP.NET 3.5 SP1.

This fact ought to raise a question in your mind. If ASP.NET MVC 2 is
built against ASP.NET 3.5 SP1, **how the heck does it take advantage of
the new HTML encoding blocks which require that you implement an
interface introduced in ASP.NET 4?**

The answer involves a tiny bit of voodoo black magic we’re doing in
ASP.NET MVC
2.[![voodoo](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HtmlEncodingNuggetsWithASP.NETMVC2_13929/voodoo_3.jpg "voodoo")](http://www.sxc.hu/photo/1196752 "Voodoo by sloopjohnb")

We introduced a new type `MvcHtmlString` which is created via a factory
method, `MvcHtmlString.Create`. When this method determines that it is
being called from an ASP.NET 4 application, it uses Reflection.Emit to
dynamically generate a derived type which implements `IHtmlString`.

If you look at the source code for ASP.NET MVC 2 Preview 2, you’ll see
the following method call when we are instantiating an `MvcHtmlString`:

```csharp
Type dynamicType = DynamicTypeGenerator.
  GenerateType("DynamicMvcHtmlString", 
    typeof(MvcHtmlString), new Type[] {
```

Note that we’re using a new internal class, `DynamicTypeGenerator`, to
generate a brand new type named `DynamicMvcHtmlString`. This type
derives from `MvcHtmlString` and implements `IHtmlString`. We’ll return
this instance instead of a standard MvcHtmlString when running on
ASP.NET 4.

When running on ASP.NET 3.5 SP1, we simply new up an `MvcHtmlString` and
return that, completely bypassing the Reflection.Emit logic. Note that
we only generate this type once per AppDomain so you only pay the
Reflection Emit cost once.

The code in `DynamicTypeGenerater` is standard Reflection.Emit stuff
which at runtime creates an assembly at runtime, adds this new type to
it, and returns a lambda used to instantiate the new type. If you’ve
never seen Reflection.Emit code, it’s worth a look.

In general, we really frown on this sort of “tricky” code as it’s often
hard to maintain and a potential bug magnet. For example, since
`System.Web.Mvc.dll` is security transparent, we needed to make sure
that the assembly we generate is marked with the
`SecurityTransparentAttribute`. This is something that would be easy to
overlook until you start testing in medium trust scenarios.

However, in this case, the type we’re generating is very small and very
simple. Not only that, we only need to keep this code for one version of
ASP.NET MVC. ASP.NET MVC 3 will be compiled against ASP.NET 4 only (no
support for ASP.NET 3.5 planned) and we’ll be able to remove this
“clever” code and have much more straightforward code. I’m looking
forward to that. :)

In any case, the point of this post was to fulfill a promise I made in
an earlier post where I said I’d give some more details on how ASP.NET
MVC 2 works with the new Html encoding block feature.

This is all behind-the-scenes detail that’s not necessary to understand
to use ASP.NET MVC, but might be interesting to some of you. Especially
those who ever find themselves in a situation where you need to support
forward compatibility.

