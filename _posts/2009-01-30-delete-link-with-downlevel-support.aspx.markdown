---
layout: post
title: "jQuery Delete Link With Downlevel Support"
date: 2009-01-29 -0800
comments: true
disqus_identifier: 18583
categories: [asp.net mvc,asp.net]
---
Earlier this morning, I posted on making a [simple jQuery delete
link](http://haacked.com/archive/2009/01/30/simple-jquery-delete-link-for-asp.net-mvc.aspx#feedback "Simple jQuery Delete Link")
which makes it easy to create a delete link that does a form post to a
delete action. Commenters pointed out that my solution won’t work for
down-level browsers such as some mobile phones, and they were right. I
wasn’t really concerned about down-level browsers.

One solution for down-level browsers is to render a proper form with a
submit button, and then hide the form with JavaScript. Of course this
takes a bit more work. Here’s what I did. I made sure I had the
following script in my master template.

```html
<script type="text/javascript">
 $("form.delete-link").css("display", "none");
 $("a.delete-link").show();
 $("a.delete-link").live('click', function(ev) {
    ev.preventDefault(); 
    $("form.delete-link").submit(); 
 });
</script>
```

When the following HTML is rendered in the page…

```csharp
<form method="post" action="/go/delete/1" 
  class="delete-link">
  <input type="submit" value="delete" />
  <input name="__RequestVerificationToken" type="hidden" 
  value="Jrcn83M7T...8Z6RkdIfMZIJ5mVb" />
</form>
<a class="delete-link" href="/go/delete/1" 
  style="display:none;">delete</a>
```

… the jQuery code shown above will hide the form, but display the link
(notice the link is hidden by default). When the link is clicked, it
posts the form. However, in cases where there is no JavaScript, the form
will be displayed, but the link will not be because the JavaScript is
the thing that hides the form.

To make this easier to use, I wrote the following helper:

```csharp
public static string DeleteLink(this HtmlHelper html
  , string linkText
  , string routeName
  , object routeValues) {
  var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
  string url = urlHelper.RouteUrl(routeName, routeValues);

  string format = @"<form method=""post"" action=""{0}"" 
  class=""delete-link"">
<input type=""submit"" value=""{1}"" />
{2}
</form>";

  string form = string.Format(format, html.AttributeEncode(url)
    , html.AttributeEncode(linkText)
    , html.AntiForgeryToken());
  return form + html.RouteLink(linkText, routeName, routeValues
  , new { @class = "delete-link", style = "display:none;" });
}
```

Notice that we’re using the [AntiForgery
helpers](http://blog.codeville.net/2008/09/01/prevent-cross-site-request-forgery-csrf-using-aspnet-mvcs-antiforgerytoken-helper/ "AntiForgery Helpers")
included with [ASP.NET MVC](http://asp.net/mvc "ASP.NET MVC Website").
What this means is that I need to make one small change to my delete
method on my controller. I need to add the `ValidateAntiForgeryToken`
attribute to the method.

```csharp
[ValidateAntiForgeryToken]
[AcceptVerbs(HttpVerbs.Post)]
public ActionResult Delete(int id) {
  //Delete it
}
```

I’ve left out a bit. For example, I didn’t specify a callback to the
jQuery code. So what should happen when this action method returns? I
leave that as an exercise to the reader. I may address it in a future
follow-up to this blog post. In my code, I’m just being cheesy and doing
a full redirect, which works fine.

