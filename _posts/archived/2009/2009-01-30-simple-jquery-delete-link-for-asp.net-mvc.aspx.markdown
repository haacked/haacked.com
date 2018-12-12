---
title: Simple jQuery Delete Link For ASP.NET MVC
date: 2009-01-30 -0800
disqus_identifier: 18582
categories:
- asp.net
- asp.net mvc
redirect_from: "/archive/2009/01/29/simple-jquery-delete-link-for-asp.net-mvc.aspx/"
---

**UPDATE**: I have [a followup to this
post](https://haacked.com/archive/2009/01/30/delete-link-with-downlevel-support.aspx "Delete Link with Downlevel Support")
that works for down-level browsers.

In a recent post, [Stephen
Walther](http://stephenwalther.com/blog/ "Stephen Walther") pointed out
the dangers of using a [**link to delete
data**](http://stephenwalther.com/blog/archive/2009/01/21/asp.net-mvc-tip-46-ndash-donrsquot-use-delete-links-because.aspx "Don't use Delete Links").
Go read it as it provides very good coverage of the issues. The problem
is not restricted to delete operations. **Any time you allow a GET
request to modify data, you’re asking for trouble**. Read [this
story](http://radar.oreilly.com/archives/2005/05/google-web-acce-1.html "Google Web Accelerator considered overzealous")
about something that happened to BackPack way back in the day to see
what I mean.

The reason that delete operations deserve special attention is that it’s
the most common case where you would use a link to change information.
If you were editing a product record, for example, you would use a form.
But a delete operation typically only needs one piece of information
(the id) which is easy to encode in the URL of a GET request.

If you are using jQuery, one simple way to turn any link into a POST
link is to add the following `onclick` attribute value:

> `$.post(this.href); return false;`

For example

```csharp
<a href="/go/delete/1" onclick="$.post(this.href); return false;">Delete</a>
```

Will now make a POST request to /go/delete/1 rather than a GET. Of
course, you need to enforce this on the server side. This is pretty easy
with [ASP.NET MVC](http://asp.net/mvc "ASP.NET MVC Website").

```csharp
[AcceptVerbs(HttpVerbs.Post)]
public ActionResult Delete(int id) {
  //Delete that stuff!
}
```

The `AcceptVerbs` attribute specifies that this action method only
responds to POST requests, not GET requests.

At this point, you could easily write helpers specifically for delete
links. I usually write very specific helper methods such as
`Html.DeleteProduct` or `Html.DeleteQuestion`. Here’s an example of one
I wrote for a sample app I’m building.

```csharp
public static string DeleteAnswerLink(this HtmlHelper html, string linkText
  , Answer answer) {
    return html.RouteLink(linkText, "answer",
        new { answerId = answer.Id, action = "delete" }, 
        new { onclick="$.post(this.href); return false;" });
}
```

The nice thing about this approach is that you can leverage the existing
helper methods by adding a minimal amount of extra information via the
`onclick` attribute.

I hope the combination of Stephen’s post and this post will lead you to
safer deleting.

