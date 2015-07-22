---
layout: post
title: "Practice safe DateTime manipulation"
date: 2004-02-18 -0800
comments: true
disqus_identifier: 198
categories: [csharp code datetime]
---
What is the proper way to add three hours to a `DateTime` for the PST timezone? Is it this?

```csharp
DateTime d = DateTime.Parse("Oct 26, 2003 12:00:00 AM");
d = d.AddHours(3.0);
```

A valiant attempt, but wrong. Instead try this:

```csharp
DateTime d = DateTime.Parse("Oct 26, 2003 12:00:00 AM");
d = d.ToUniversalTime().AddHours(3.0).ToLocalTime();
// displays 10/26/2003 02:00:00 AM which is correct!
Console.WriteLine(d);
```

Why all the [rigamarole](http://dictionary.reference.com/search?r=2&q=rigamarole "Definition of Rigamarole")
of converting to universal time and back to local time? A little thing we like to call Daylight savings time. Without the conversion, adding three hours would have set the time to 3:00 AM which would be wrong.

Realizing this probably would have saved me from many hours of intense debugging sessions. I remember one particular case with a system of scripts and tools I set up to analyze the very large log files for a big client. It hummed along nicely until one fine spring day when it failed miserably. After an entire day of tracking down the source of the problem, I finally nailed it down to a script’s mishandling of Daylight Savings.

To find out more about proper `DateTime` handling, read the following article concerning best practices with [manipulating date times](http://msdn.microsoft.com/netframework/default.aspx?pull=/library/en-us/dndotnet/html/datetimecode.asp "Best practices with DateTime").
