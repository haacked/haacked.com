---
title: Writing A Page To A String
tags: [aspnetmvc]
redirect_from: "/archive/2009/05/28/writing-page-to-string.aspx/"
---

ASP.NET Pages are designed to stream their output directly to a response
stream. This can be a huge performance benefit for large pages as it
doesn’t require buffering and allocating very large strings before
rendering. Allocating large strings can put them on the [Large Object
Heap](http://msdn.microsoft.com/en-us/magazine/cc534993.aspx "Large Object Heap")
which means they’ll be sticking around for a while.

[![string](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RenderingaPageToAStringWithoutUsingAResp_7EC8/string_3.jpg "string")](http://www.sxc.hu/photo/979650 "Photo by crisderaud on stock.xchng")
However, there are many cases in which you really want to render a page
to a string so you can perform some post processing. I wrote about one
means [using a Response filter eons
ago](https://haacked.com/archive/2007/07/29/cleanup-the-crap-that-windows-live-writer-injects-with-this.aspx "Using Response Filter").

However, recently, I learned about a method of the Page class I never
noticed which allows me to use a much lighter weight approach to this
problem.

The method in question is `CreateHtmlTextWriter` which is protected, but
also virtual.

So here’s an example of the code-behind for a page that can leverage
this method to filter the output before its sent to the browser.

```csharp
public partial class FilterDemo : System.Web.UI.Page
{
  HtmlTextWriter _oldWriter = null;
  StringWriter _stringWriter = new StringWriter();

  protected override HtmlTextWriter CreateHtmlTextWriter(TextWriter tw)
  {
    _oldWriter = base.CreateHtmlTextWriter(tw);
    return base.CreateHtmlTextWriter(_stringWriter);
  }

  protected override void Render(HtmlTextWriter writer)
  {
    base.Render(writer);
    string html = _stringWriter.ToString();
    html = html.Replace("REPLACE ME!", "IT WAS REPLACED!");
    _oldWriter.Write(html);
  }
}
```

In the `CreateHtmlTextWriter` method, we simply use the original logic
to create the `HtmlTextWriter` and store it away in an instance
variable.

Then we use the same logic to create a new `HtmlTextWriter`, but this
one has our own `StringWriter` as the underlying `TextWriter`. The
`HtmlTextWriter` passed into the `Render` method is the one we created.
We call `Render` on that and grab the output from the `StringWriter` and
now can do all the replacements we want. We finally write the final
output to the original `HtmlTextWriter` which is hooked up to the
response.

A lot of caveats apply in using this technique. First, as I mentioned
before, for large pages, you could be killing scalability and
performance by doing this. Also, I haven’t tested this with output
caching, async pages, etc… etc…, so your mileage may vary.

Note, if you want to call one page from another, and get the output as a
string within the first page, you can pass your own `TextWriter` to
`Server.Execute`, so this technique is not necessary in that case.

