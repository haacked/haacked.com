---
title: PageParser.GetCompiledPageInstance Weirdness When Debug Set To False In Web.config
date: 2006-05-03 -0800
disqus_identifier: 12642
categories: []
redirect_from: "/archive/2006/05/02/PageParser.GetCompiledPageInstanceWeirdnessWhenDebugSetToFalseInWeb.config.aspx/"
---

This is a story of intrigue.

Ok, perhaps that is a bit overblown. This is really a story of
schizophrenia. It is the story of a method
`PageParser.GetCompiledPageInstance` that exhibits a different behavior
depending on whether or not you have the `<compilation>` tag’s `debug`
attribute set to `true` or `false`.

The problem first came up when deploying the most recent builds of
[Subtext](http://subtextproject.com/ "Subtext Project Website") with
this attribute set to `false`. This was the natural response to Scott
Guthrie’s admonishment, [Don’t Run Production ASP.NET Applications with
debug="true"
enabled.](http://weblogs.asp.net/scottgu/archive/2006/04/11/442448.aspx "Scott Guthrie's Admonishment").

However, this affected Subtext in an unusual manner. Subtext employs an
URL rewriting mechanism I [wrote about
before](https://haacked.com/archive/2006/02/23/TheSubtextAlternativeToUrlRewriting.aspx "Subtext Alternative to URL Rewriting").
It relies on the using an `IHttpHandler` that is created by calling
`PageParser.GetCompiledPageInstance`.

I will spare you all the details and cut to the chase.
`GetCompiledPageInstance` takes in three parameters:

-   virtualPath (string)
-   inputFile (string)
-   context (HttpContext).

In the initial request to the Subtext root, the values for those
parameters on my local machine are:

-   virtualPath = "http://localhost/Subtext.Web/Default.aspx"
-   inputFile = "c:\\projects\\Subtext.Web\\DTP.aspx"
-   context = (the current context passed in by the ASP.NET runtime)

The interesting thing to note is that there is an actual `aspx` file
named `Default.aspx` located at
*http://localhost/Subtext.Web/Default.aspx*. When the `debug`
compilation option was set to `true`, this method would return a
compiled instance of DTP.aspx (hence the URL rewriting).

But when I set `debug="false"`, it would return a compiled instance of
Default.aspx. Holy moly!

I confirmed this by attaching a debugger and going through the process
multiple times. Using [Reflector](http://www.aisto.com/roeder/dotnet/),
I started walking through the code for `GetCompiledPageInstance` until
my eyes started to burst. There is a lot of machinery at work under the
hood. I eventually found some code that appears to generate a URL path
differently based on debugging options. Not sure if this was the
culprit, but it is possible.

Setting `debug="false"` causes the runtime to perform a batch
compilation. Thus a request for /Default.aspx is going to compile all
\*.aspx files in that folder into a single DLL. Setting that debug value
to true causes ASP.NET to compile every page into its own assembly.

My fix is a bit of a hack, until I can get a deeper understanding of
what is really happening. As I see it, calling `GetCompiledPageInstance`
with a `virtualPath` that points to a one file while passing in a
different physical file path to `inputFile` is causing some confusion.
Perhaps due to the batch compilation.

To remedy this, I simply have a check before we call
`GetCompiledPageInstance` to check the end of the `virtualPath` for
*/Default.aspx* (case insensitive of course). If it finds that string,
it truncates the *default.aspx* portion of it. That seems to do the
trick for now since this is pretty much the one place in which URL
rewriting would attempt to rewrite a url that itself points to a real
page.

For a nice look under the hood regarding the `compilation` option, check
out this [post by Milan
Negovan](http://www.aspnetresources.com/articles/debug_code_in_production.aspx "Beware of Deploying Debug Code In Production").

Please keep in mind that this is a separate issue from deploying your
compiled assemblies in debug mode or with debug symbols. This has to do
with the ASP.NET runtime compiling the ASPX files at runtime.

