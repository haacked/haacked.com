---
title: Conditional Compilation Constants and ASP.NET
tags: [aspnet]
redirect_from: "/archive/2007/09/15/conditional-compilation-constants-and-asp.net.aspx/"
---

UPDATE: K. Scott Allen got to the root of the problem. It turns out it
was an issue of precedence. Compiler options are not additive.
Specifying options in @Page override those in web.config. [Read his
post](http://odetocode.com/Blogs/scott/archive/2007/09/24/11413.aspx "More on Conditional Compilation")
to find out more.

Conditional compilation constants are pretty useful for targeting your
application for a particular platform, environment, etc... For example,
to have code that only executes in debug mode, you can define a
conditional constant named DEBUG and then do this...

```csharp
#if DEBUG
//This code only runs when the app is compiled for debug
Log.EverythingAboutTheMachine();
#endif
```

It’s not common knowledge to me that these constants work equally well
in ASPX and ASCX files. At least it wasn’t common knowledge for me. For
example:

```csharp
<!-- Note the space between % and # -->
<% #if DEBUG %>
<h1>DEBUG Mode!!!</h1>
<% #endif %>
```

The question is, where do you define these conditional constants for
ASP.NET. The answer is, well it depends on whether you’re using a
Website project or a Web Application project.

For a Web Site project, one option is to define it at the Page level
like so...

```csharp
<%@ Page CompilerOptions="/d:QUUX" %>
```

The nice thing about this approach is that the conditional compilation
works both in the ASPX file as well as in the `CodeFile`, for ASP.NET
*Website* projects.

According to [this
post](http://odetocode.com/Blogs/scott/archive/2005/12/01/2559.aspx "Conditional Compilation in ASP.NET 2.0")
by [K. Scott Allen](http://odetocode.com/Blogs/scott/ "OdeToCode Blog"),
you can also define conditional compilation constants in the Web.config
file using the `<system.codedom />` element (a direct child of the
`<configuration />` element, but this didn’t work for me in either
website projects nor web application projects.

```csharp
<system.codedom>
  <compilers>
    <compiler
      language="c#;cs;csharp" extension=".cs"
      compilerOptions="/d:MY_CONSTANT"
      type="Microsoft.CSharp.CSharpCodeProvider, 
        System, Version=2.0.0.0, Culture=neutral, 
        PublicKeyToken=b77a5c561934e089" />
    </compilers>
</system.codedom>
```

At heart, Web Application Projects are no different from Class Library
projects so you can set conditional compilation constants from the
project properties dialog in Visual Studio.[![ConditionalCompilation -
Microsoft Visual
Studio](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ConditionalConstantsandASP.NET_12C5D/ConditionalCompilation%20-%20Microsoft%20Visual%20Studio_thumb_1.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ConditionalConstantsandASP.NET_12C5D/ConditionalCompilation%20-%20Microsoft%20Visual%20Studio_1.png "Conditional Compilation Constants")

Unfortunately, these only seem to work in the code behind and not within
ASPX files.

Here’s a grid based on my experiments that show when and where setting
conditional compilation constants seem to work in ASP.NET.

<table class="highlightTable matrix" unselectable="on">
    <tbody>
    <tr>
        <th class="pivot"> </th>
        <th>Web.config</th>
        <th>Project Properties</th>
        <th>Page Directive</th></tr>
    <tr>
        <th>Website Code File</th>
        <td class="no">No</td>
        <td class="na">n/a</td>
        <td class="yes">Yes</td></tr>
    <tr>
        <th>Web Application Code File</th>
        <td class="no">No</td>
        <td class="yes">Yes</td>
        <td class="no">No</td></tr>
    <tr>
        <th>ASPX, ASCX File</th>
        <td class="no">No</td>
        <td class="no">No</td>
        <td class="yes">Yes</td>
    </tr>
    </tbody>
</table>


In order to create this grid, I created a solution that includes both a
Web Application project and a Website project and ran through all nine
permutations. You can [download the solution
here](https://haacked.com.nyud.net/code/conditionalcompilation.zip "Conditional Compilation Demo")
if you’re interested.

It’s a bit confusing, but hopefully the above table clears things up
slightly. As for setting the conditional constants in `Web.config`, I’m
quite surprised that it didn’t work for me (yes, I set it to full trust)
and assume that I must’ve made a mistake somewhere. Hopefully someone
will download this solution and show me why it doesn’t work.

