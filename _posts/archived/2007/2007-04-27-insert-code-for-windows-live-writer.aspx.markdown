---
title: Insert Code for Windows Live Writer
tags: [code,tech,blogging]
redirect_from: "/archive/2007/04/26/insert-code-for-windows-live-writer.aspx/"
---

Several pople have asked me recently about the nice code syntax
highlighting in my blog. For example:

```csharp
public string Test()
{
  //Look at the pretty colors
  return "Yay!";
}
```

A long time ago, I [wrote
about](https://haacked.com/archive/2004/06/16/code-to-html-syntax-highlighting.aspx "Syntax Highlighting")
using
[http://www.manoli.net/csharpformat/](http://www.manoli.net/csharpformat/)
for converting code to HTML.

But these days, I use [Omar
Shahine’s](http://www.shahine.com/omar/ "Omar Shahine’s Blog") [Insert
Code for Windows Live
Writer](http://www.codeplex.com/insertcode/Release/ProjectReleases.aspx?ReleaseId=840 "Insert Code for Windows Live Writer")
plugin for, you guessed it, [Windows Live
Writer](http://windowslivewriter.spaces.live.com/ "Windows Live Writer Download").
This plugin just happens to use the Manoli code to perform syntax
highlighting.

![Plugin
Screenshot](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/InsertCodeforWindowsLiveWriter_B945/image%7B0%7D%5B9%5D.png)

I recommend downloading and referencing the [CSS
stylesheet](http://www.manoli.net/csharpformat/csharp.css "CSS Stylesheet for Csharp")
from the Manoli site and making sure to uncheck the *Embed StyleSheet*
option in the plugin.

The dropshadow around the code is some CSS I found on the net.

