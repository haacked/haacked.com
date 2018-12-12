---
title: How To Handle The DIV Tag Around ASP.NET Hidden Inputs
date: 2007-06-15 -0800
disqus_identifier: 18353
categories: []
redirect_from: "/archive/2007/06/14/how-to-handle-the-div-tag-around-asp.net-hidden-inputs.aspx/"
---

One praiseworthy aspect of ASP.NET 2.0 is its much improved XHTML
compliance. However, there is one particular implementation detail
related to this that causes some web designs to break and could
have been implemented in a better manner.

The detail is how ASP.NET 2.0 will [wrap a DIV tag around hidden input
fields](http://vaultofthoughts.net/TheDIVTagAroundHiddenASPNETInputFields.aspx "The DIV Tag Around Hidden ASP.NET Input Fields").
My complaint isn’t that Microsoft added this DIV wrapper, because it is
needed for strict compliance. **My complaint is that there is no CSS
class or id on the DIV to make it easy to exclude CSS styling on it.**

For example, here is a snippet from the output of a simple page.

```csharp
<form name="form1" method="post" action="Default.aspx" id="form1">
<div>
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="Omitted" />
</div>

<div>
Hello World
</div>
</form>
```

It would have been nice if the author of this code could have simply
added something like:

```csharp
<div class="aspnet-generated">
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="Omitted" />
</div>
```

It is quite common for web designers to apply a specific style to all
DIVs on a page, for example, adding a padding of 5px.

```csharp
<style type="text/css">
  div {padding: 5px;}
</style>
```

Unfortunately, this leaves a gap where the ASP.NET generated DIV is
located.

In a [comment made on his
blog](http://weblogs.asp.net/scottgu/archive/2003/11/25/39620.aspx#732432 "XHTML and Accessibility in ASP.NET Whidbey"),
Scott Guthrie makes this remark on this topic:

> You could modify your CSS to exclude the \<div\> we create by default
> immediately underneath the form tag.
>
> In general I’d probably recommend having as broad a CSS rule as the
> one you have above - since it will effect lots of content on the page.
> Can you instead have it apply to a CSS class only?
>
Yes, you could modify the CSS to exclude the first child DIV of the FORM
tag by using a [child
selector](http://meyerweb.com/eric/articles/webrev/200006b.html "The Child Selector")
and a [first-child pseudo
class](http://www.w3schools.com/css/pr_pseudo_first-child.asp "First Child Pseudo Class")
like so:

```csharp
<style type="text/css">
  div {padding: 5px;}
  form>div:first-child {padding: 0; margin: 0;}        
</style>
```

Unfortunately, IE 6 doesn’t support child selectors nor first-child
pseudo classes. Since IE 6 is still quite widely used, this is not a
viable solution.

Regarding Scott’s second question, this isn’t always reasonable because
many web designs apply certain styles most DIVs on a page and then
exclude a few that shouldn’t have that style. In that situation, it
takes more work to give every DIV a CSS class so you can apply the style
to just that class. It is simpler to use an exclusionary approach in
these cases. Simply apply the style to all DIVs and exclude the ones
that need to be excluded.

Unfortunately, because of the way this DIV wrapper was implemented
**and**, because of CSS non-compliance in IE 6, it’s not possible to
exclude this DIV using CSS alone. It requires changing the markup.

Fortunately, there’s an easy solution with a slight change to your
markup, but it requires changing your markup just a bit. Just wrap your
content in a DIV with a specific ID.

```csharp
<form id="form1" runat="server">
  <div id="main">
    <div>
      Hello World
    </div>
  </div>
</form>
```

And then style it like so.

```csharp
<style type="text/css">
  div {padding: 0; margin: 0;} /* generated div */
  #main div {padding: 5px;} /* all other divs */
</style>
```

This is a lot easier (and higher performing) than trying to muck around
with the output via the
[HttpResponse.Filter](http://msdn2.microsoft.com/en-us/library/system.web.httpresponse.filter.aspx "HttpResponse.Filter on MSDN").

So while the solution is easy, it still bothers me that it is necessary.
One main reason why is that I often get CSS designs handed to me and I
have to go through and make sure to make this change appropriately. I’d
rather just be able to plop a one line CSS change into every stylesheet
like so:

```csharp
div.aspnet-generated {padding: 0; margin: 0;}
```

On another note, one other interesting side-effect of this change in
ASP.NET 2.0 is that [many
implementations](http://www.hanselman.com/blog/MovingViewStateToTheBottomOfThePage.aspx "Moving Viewstate To The Bottom")
for [moving viewstate to the
bottom](http://www.madskristensen.dk/blog/An+HttpModule+That+Moves+ViewState+To+The+Bottom.aspx "An Http Modlue that moves Viewstate To The Bottom")
of the page end up breaking XHTML compliance because they only move the
input tags and not the entire DIV to the bottom.

