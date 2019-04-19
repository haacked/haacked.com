---
title: Syntax Highlighting.  Converting C# (et al) to HTML.
tags: [code,html]
redirect_from: "/archive/2004/06/15/code-to-html-syntax-highlighting.aspx/"
---

I’ve probably read a hundred or so posts of people looking for a way to
syntax highlight source code listings in HTML. Maybe I’m the last to
discover this site, but
[http://www.manoli.net/csharpformat/](http://www.manoli.net/csharpformat/)
is the answer for me so far. Just cut and paste some C#, VB,
html/xml/aspx code int the text box and click "format my code" and
voila! You get some clean HTML displaying nicely formatted code.

The tool allows you to optionally format the code with alternating line
colors and line numbers if you so desire.

```csharp
public class ThisIsSoCool
{
  ///      /// This is seriously neat.      ///      
  public void YouShouldTryThis()
  {}
}
```

Let’s try it with line numbers.

```csharp
       1:  public class ThisIsAlsoCool
       2:  {
       3:    /// 
       4:    /// Look at me ma! I’m using line numbers
       5:    /// 
       6:    public void YouShouldTryThis()
       7:    {}
       8:  }
```
