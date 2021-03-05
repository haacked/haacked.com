---
title: 'Syntax Highlighting Revisited: SnippetCompiler.'
tags: [html,code]
redirect_from: "/archive/2004/06/16/syntax-highlighting-revisited-snippetcompiler.aspx/"
---

[![Snippet
Compiler](/assets/images/SnippetCompilerIcon.png)](http://www.sliver.com/dotnet/SnippetCompiler/)Regarding
Syntax Highlighting, [Daniel Turini](http://dturini.blogspot.com/)
pointed out that
[SnippetCompiler](http://www.sliver.com/dotnet/SnippetCompiler) has the
ability to export code to the clipboard (and to a file) as HTML.

Snippet Compiler has a lot of nice features and is a welcome addition to
my toolbox, but purely for syntax highlighting, it has a few
disadvantages compared to the manoli.net website I [mentioned
previously](https://haacked.com/archive/2004/06/16/636.aspx). First of
all, although you can view snippets with line numbers, the line numbers
aren't exported to HTML like Manoli does. Secondly, Manoli handles
XML/HTML along with C# and VB, while SnippetCompiler seems to do well
only with C# and VB.NET. Lastly, Manoli uses CSS for styling and you
can have it embed the CSS definitions in the generated HTML, or
reference the provided stylesheet. This is a really nice feature.

One thing I do like about the SnippetCompiler is how the summary tags in
the comments are gray while the actual comment is green. That's a nice
touch.

```csharp

    ///<summary>
    ///Manages cool things
    ///</summary>
    public class ThisIsSoCool
    {
        /// 
        /// This is seriously neat. 
        /// 
        public void YouShouldTryThis()
        {}
    }
```