---
title: Using AntiXss As The Default Encoder For ASP.NET
tags: [aspnetmvc,aspnet,code]
redirect_from: "/archive/2010/04/05/using-antixss-as-the-default-encoder-for-asp-net.aspx/"
---

This is the third in a three part series related to HTML encoding
blocks, aka the `<%: ... %>` syntax.

-   [Html Encoding Code Blocks With ASP.NET
    4](https://haacked.com/archive/2009/09/25/html-encoding-code-nuggets.aspx "Html Encoding Blocks")
-   [Html Encoding Nuggets With ASP.NET MVC
    2](https://haacked.com/archive/2009/11/03/html-encoding-nuggets-aspnetmvc2.aspx "Html Encoding Nuggets with ASP.NET MVC 2")
-   **Using AntiXss as the default encoder for ASP.NET**

[Scott Guthrie](http://weblogs.asp.net/scottgu/ "Scott Guthrie's blog")
recently wrote about [the new \<%: %\> syntax for HTML encoding output
in ASP.NET
4](http://weblogs.asp.net/scottgu/archive/2010/04/06/new-lt-gt-syntax-for-html-encoding-output-in-asp-net-4-and-asp-net-mvc-2.aspx "New syntax for HTML encoding").
I also covered the topic of [HTML encoding code
nuggets](https://haacked.com/archive/2009/09/25/html-encoding-code-nuggets.aspx "HTML Encoding Code Nuggets")
in the past as well providing some insight into our design choices for
the approach we took.

A commenter to Scott’s blog post asked,

> Will it be possible to extend this so that is uses libraries like
> AntiXSS instead? See:
> [http://antixss.codeplex.com/](http://antixss.codeplex.com/ "AntiXSS on CodePlex")

The answer is **yes!**

ASP.NET 4 includes a new extensibility point which allows you to replace
the default encoding logic with your own anywhere ASP.NET does encoding.

All it requires is to write a class which derives from
`System.Web.Util.HttpEncoder` and register that class in `Web.config`
via the `encoderType` attribute of the `httpRuntime` element.

### Walkthrough

In the following section, I’ll walk you through setting this up. First,
you’re going to need to [download the AntiXSS
library](http://www.microsoft.com/downloads/details.aspx?FamilyId=051ee83c-5ccf-48ed-8463-02f56a6bfc09&displaylang=en "Download Page")
which is at version 3.1 at the time of this writing. On my machine, that
dropped the *AntiXSSLibrary.dll* file at the following location:
*C:\\Program Files (x86)\\Microsoft Information Security\\Microsoft
Anti-Cross Site Scripting Library v3.1\\Library*

Create a new ASP.NET MVC application (note, this works for \*any\*
ASP.NET application). Copy the assembly into the project directory
somewhere where you’ll be able to find it. I typically have a “lib”
folder or a “Dependencies” folder for this purpose. Right clicke on the
*References* node of the project to add a reference to the assembly.

![add-reference](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UsingAntiXssAsTheDefaultEncoderForAS.NET_75E3/add-reference_3.png "add-reference")
![Add-Reference-dialog](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UsingAntiXssAsTheDefaultEncoderForAS.NET_75E3/Add-Reference-dialog_3.png "Add-Reference-dialog")The
next step is to write a class that derives from `HttpEncoder`. Note that
in the following listing, some methods were excluded which are included
in the project.

```csharp
using System;
using System.IO;
using System.Web.Util;
using Microsoft.Security.Application;

/// <summary>
/// Summary description for AntiXss
/// </summary>
public class AntiXssEncoder : HttpEncoder
{
  public AntiXssEncoder() { }

  protected override void HtmlEncode(string value, TextWriter output)
  {
    output.Write(AntiXss.HtmlEncode(value));
  }

  protected override void HtmlAttributeEncode(string value, TextWriter output)
  {
    output.Write(AntiXss.HtmlAttributeEncode(value));
  }

  protected override void HtmlDecode(string value, TextWriter output)
  {
      base.HtmlDecode(value, output);
  }

  // Some code omitted but included in the sample
}
```

Finally, register the type in *web.config*.

```csharp
...
  <system.web>
    <httpRuntime encoderType="AntiXssEncoder, AssemblyName"/>
...
```

Note that you’ll need to replace *AssemblyName* with the actual name of
your assembly. Also, in the sample included with this blog post,
AntiXssEncoder is not in any namespace. If you put your encoder in a
namespace, you’ll need to make sure to provide the fully qualified type
name.

To prove that this is working, run the project in the debugger and set a
breakpoint in the encoding method.

![debugger-breakpoint](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UsingAntiXssAsTheDefaultEncoderForAS.NET_75E3/debugger-breakpoint_3.png "debugger-breakpoint")

With that, you are all set to take full control over how strings are
encoded in your application.

Note that [Scott
Hanselman](http://hanselman.com/blog/ "Scott Hanselman's Blog") and I
gave a live demonstration of setting this up at Mix 10 this year as part
of our security talk if you’re [interested in watching
it](http://live.visitmix.com/MIX10/Sessions/FT05 "HaHaa show").

As usual, I’ve provided a **[sample ASP.NET MVC 2
project](http://code.haacked.com/mvc-2/AntiXssDemo.zip "AntiXssDemo")**
for Visual Studio 2010 which you can look at to see this in action.

