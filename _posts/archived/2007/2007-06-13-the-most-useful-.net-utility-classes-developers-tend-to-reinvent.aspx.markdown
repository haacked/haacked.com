---
title: The Most Useful .NET Utility Classes Developers Tend To Reinvent Rather Than
  Reuse
date: 2007-06-13 -0800
tags: [dotnet]
redirect_from: "/archive/2007/06/12/the-most-useful-.net-utility-classes-developers-tend-to-reinvent.aspx/"
---

### [System.IO.Path](http://msdn2.microsoft.com/en-us/library/system.io.path.aspx "Path class on MSDN")

How often do you see code like this to create a file path?

```csharp
public string GetFullPath(string fileName)
{
  string folder = ConfigurationManager.AppSettings["somefolder"];
  return folder + fileName;
}
```

Code like this drives me crazy because it is so prone to error. For
example, when you set the folder setting, you have to remember to make
sure it ends with a slash. Having too many things to remember makes this
setup fragile.

Sure, you write some code to ensure that the folder has an ending slash,
but **I’d rather let someone write that code. For example, Microsoft.**

The .NET framework is definitely huge so it can be understandable to
miss out on some of the useful utility classes in there that will make
your life as a developer easier.

```csharp
public string GetFullPath(string filename)
{
  string folder = ConfigurationManager.AppSettings["somefolder"];
  return System.IO.Path.Combine(folder, filename);
}
```

The Path class is certainly well known and probably well used, but is
still one of those classes that developers seem to never use to its full
potential. For example, how often do you see this?

```csharp
//make sure folder path ends with slash
string folder = GetFolderPath() + @"\";
```

Well that’s nice for Windows machines, but our world is changing and
someday, you may want your code to run on Linux or, god forbid, a Mac!
Instead, you could use this and be safe.

```csharp
string folder = GetFolderPath() + Path.DirectorySeparatorChar;
```

That’ll make sure the slash leans in the correct direction based on the
platform. Oh, and the next time I see code to parse a file name from a
path, I’m going to slap the developer upside the head and mention this
method:

```csharp
string fileName = Path.GetFileName(fullPath);
```

### [System.Web.VirtualPathUtility](http://msdn2.microsoft.com/en-us/library/system.web.virtualpathutility.aspx "VirtualPathUtility class on MSDN") {.clear}

Not knowing and using this class is forgivable because it didn’t exist
until .NET 2.0. But now that you are reading this, you have no excuse.
One great usage is for converting tilde paths to absolute paths.

*Note: The tilde (\~) character is called the **root operator**in the
context of ASP.NET virtual URLs*. *A little trivia for you.*

For example, if you are running an app in a virtual application named
"MyApp", the following:

```csharp
string path = VirtualPathUtility.ToAbsolutePath("~/Controls/Test.ascx");
```

Sets *path* to */MyApp/Controls/Test.ascx*. No need to write your own
ResolveUrl method.

Some other useful methods (there are many more than these listed)...

Method               | Description
---------------------|-----------------------------------------------------------------------------------------------------------------------------------
AppendTrailingSlash  | Appends a / to the end of the path if none exists already.
Combine              | Analagous to Path.Combine, but for URLs.
MakeRelative         | Useful for getting the relative path from one directory to another (*was it dot dot slash dot dot slash? Or just dot dot slash?*)

### [System.Web.HttpUtility](http://msdn2.microsoft.com/en-us/library/system.web.httputility.aspx "HttpUtility class on MSDN")

This class has a wealth of methods for URL/HTML encoding and decoding. A
small sampling...

Method     | Description
-----------|----------------------------------------------
HtmlEncode | Converts a string to an HTML encoded string.
HtmlDecode | Decodes an HTML encoded string.
UrlEncode  | Converts a string to a URL encoded string.
UrlDecode  | Decodes a URL encoded string.

One particular method that is pretty neat in this class is
`HtmlAttributeEncode`. This method is `HtmlEncode`’s lazy cousin. It
does the minimal work to safely encode a string for HTML. For example,
given this string:

*\<p\>&\</p\>*

`HtmlEncode` produces: *&amp;lt;p&amp;gt;&amp;amp;&amp;lt;/p&amp;gt;*

wherease `HtmlAttributeEncode` produces: *&amp;lt;p\>&amp;amp;&amp;lt;/p\>*

In other words, it only encodes left angle brackets, not the right ones.

### [System.Environment](http://msdn2.microsoft.com/en-us/library/system.environment.aspx "Environment class on MSDN")

This class contains a wealth of information about the current
environment in which your code is executing. You can get access to the
`MachineName`, the `CommandLine`, etc...

However, the one property I would like to get developers to use is a
simple one:

```csharp
//Instead of this
string s = "Blah\r\n";
//do this
string s = "Blah" + Environment.NewLine;
```

Again, this falls under the case that your code might actually run on a
different operating system someday. Might as well acquire good habits
now.

### What Classes Am I Missing?

No matter how hard I can try, there is no way that I could make a
complete list. In .NET 3.0, I’d probably add the new `TimeZoneInfo`
class. What classes do you find extremely useful that are not so well
known? Or worse, what classes have functionality that you see developers
reinventing the wheel recreating, rather than using the existing class?
