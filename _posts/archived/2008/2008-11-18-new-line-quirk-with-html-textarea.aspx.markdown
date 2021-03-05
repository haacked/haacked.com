---
title: New Line Quirk with HTML TextArea
tags: [aspnetmvc]
redirect_from: "/archive/2008/11/17/new-line-quirk-with-html-textarea.aspx/"
---

Pop quiz. What would you expect these three bits of HTML to render?

```csharp
<!-- no new lines after textarea -->
<textarea>Foo</textarea>

<!-- one new line after textarea -->
<textarea>
Foo</textarea>

<!-- two new lines after textarea -->
<textarea>

Foo</textarea>
```

The fact that I’m even writing about this might make you suspicious of
your initial gut answer. Mine would have been that the first would
render a text area with “Foo” on the first line, the second with it on
the second line, and the third with it on the third line.

In fact, here’s what it renders using Firefox 3.0.3. I confirmed the
same behavior with IE 8 beta 2 and Google Chrome.

![three text
areas](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NewLineQuirkwithHTMLTextArea_BDBC/three-text-boxes_3.png "three text areas")

Notice that the first two text areas are identical. Why is this
important?

Suppose you’re building an [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC Website") website and you call the
following bit of code:

```aspx-cs
<%= Html.TextArea("name", "\r\nFoo") %>
```

You probably would expect to see the third text area in the screenshot
above in which “Foo” is rendered on the second line, not the first.

However, suppose our `TextArea` helper didn’t factor in the actual
behavior that browsers exhibit. You would in this case get the behavior
of the second text area in which “Foo” is still on the first line.

Fortunately, our `TextArea` helper does factor this in and renders the
supplied value on the next line after the textarea tag. In my mind, this
is very much an edge case, but it was reported as a bug by someone
external a while back. Isn’t HTML fun?! ;)

