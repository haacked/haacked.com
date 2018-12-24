---
title: Get The Most Out Of Your Format String
date: 2006-02-15 -0800
tags: [dotnet]
redirect_from: "/archive/2006/02/14/GetTheMostOutOfYourFormatString.aspx/"
---

I was reviewing some code when I ran into code that fit this pattern all
over the place (simplified to make a point).

```csharp
string format = "/comments/{0}/{1}.aspx  #";
string url = String.Format(CultureInfo.InvariantCulture, format +
entity.Id, entity.ParentID, entity.Date.ToString("yyyy/MM/dd",
CultureInfo.InvariantCulture));-
```

There are a couple things I want you to notice here. The first is
concatenation of the entity id to the end of the format string.

format + entity.Id

Second, notice format string passed into the `ToString` method of the
the entity.Date.

entity.Date.ToString(**"yyyy/MM/dd"**, CultureInfo.InvariantCulture)

Ok I lied. There is a third thing I want you to notice - the fact that
**CultureInfo.InvariantCulture** has to be specified twice.

My guess is the author of this code was in a hurry and didn’t realize
the full power of format strings that can be passed to the
`String.Format` method. It is possible to get all the formatting
information in a single string. Here is how I rewrote this.

string format = "/comments/{0:yyyy/MM/dd}/{1}.aspx  #{2}";

string url = String.Format(CultureInfo.InvariantCulture, format,
entity.Date, entity.ParentId, entity.Id);

In this rewritten code, I took advantage of the fact that the
`String.Format` method allows you to specify formatting information
within the place holders like so.

{0:yyyy/MM/dd}

In this way, the code does not need to call the `ToString` method of the
`DateTime` instance. The formatting is all in a single format string and
that is one less `CultureInfo.InvariantCulture` you need to type.

This helps make the code more readable and more flexible. The string
format for this url is all in a single string.

A useful reference for string formats that I keep at my fingertips is
the [following blog
post](http://blog.stevex.net/index.php/string-formatting-in-csharp/ "String Formatting in C# Reference")
by [Steve Tibbett](http://blog.stevex.net/ "Steve Tibbett Blog"). If you
use format strings, you will find this reference invaluable.

