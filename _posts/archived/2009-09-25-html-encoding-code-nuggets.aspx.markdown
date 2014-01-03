---
layout: post
title: "Html Encoding Code Blocks With ASP.NET 4"
date: 2009-09-25 -0800
comments: true
disqus_identifier: 18644
categories: [asp.net,code,asp.net mvc]
---
This is the first in a three part series related to HTML encoding
blocks, aka the `<%: ... %>` syntax.

-   **Html Encoding Code Blocks With ASP.NET 4**
-   [Html Encoding Nuggets With ASP.NET MVC
    2](http://haacked.com/archive/2009/11/03/html-encoding-nuggets-aspnetmvc2.aspx "Html Encoding Nuggets with ASP.NET MVC 2")
-   [Using AntiXss as the default encoder for
    ASP.NET](http://haacked.com/archive/2010/04/06/using-antixss-as-the-default-encoder-for-asp-net.aspx "Using AntiXSS")

One great new feature being introduced in ASP.NET 4 is a new code block
(*often called a Code Nugget by members of the Visual Web Developer
team*) syntax which provides a convenient means to HTML encode output in
an ASPX page or view.

```html
<%: CodeExpression %>
```

*I often tell people it’s `<%=` but with the `=` seen from the front.*

Let’s look at an example of how this might be used in an ASP.NET MVC
view. Suppose you have a form which allows the user to submit their
first and last name. After submitting the form, the same view is used to
display the submitted values.

```html
First Name: <%: Model.FirstName %>
Last Name: <%: Model.FirstName %>

<form method="post">
  <%: Html.TextBox("FirstName") %>
  <%: Html.TextBox("LastName") %>
</form>
```

By using the the new syntax, `Model.FirstName` and `Model.LastName` are
properly HTML encoded which helps in mitigating Cross Site Scripting
(XSS) attacks.

### Expressing Intent with the new `IHtmlString` interface

If you’re paying close attention, you might be asking yourself
“*`Html.TextBox` is supposed to return HTML that is already sanitized.
Wouldn’t using this syntax with `Html.TextBox` cause double encoding?*”

ASP.NET 4 also introduces a new interface, `IHtmlString along with` a
default implementation, `HtmlString`. Any method that returns a value
that implements the `IHtmlString` interface will not get encoded by this
new syntax.

In ASP.NET MVC 2, all helpers which return HTML now take advantage of
this new interface which means that when you’re writing a view, you can
simply use this new syntax *all the time and it will just work*.**By
adopting this habit, you’ve effectively changed the act of HTML encoding
from an *opt-in* model to an *opt-out* model**.

### The Goals

There were four primary goals we wanted to satisfy with the new syntax.

1.  **Obvious at a glance**. When you look at a page or a view, it
    should be immediately obvious which code blocks are HTML encoded and
    which are not. You shouldn’t have to refer back to flags in
    `web.config` or the `page` directive (which could turn encoding on
    or off) to figure out whether the code is actually being encoded.
    Also, it’s not uncommon to review code changes via check-in emails
    which only show a DIFF. This is one reason we didn’t reuse existing
    syntax.

    Not only that, code review becomes a bit easier with this new
    syntax. For example, it would be easy to do a global search for
    `<%=` in a code base and review those lines with more scrutiny
    (though we hope there won’t be any to review). Also, when you
    receive a check-in email which shows a DIFF, you have most of the
    context you need to review that code.

2.  **Evokes a similar meaning to \<%=**. We could have used something
    entirely new, but we didn’t have the time to drastically change the
    syntax. We also wanted something that had a similar *feel* to `<%=`
    which evokes the sense that it’s related to output. Yeah, it’s a bit
    touchy feely and arbitrary, but I think it helps people feel
    immediately familiar with the syntax.

3.  **Replaces the old syntax and allows developers to show their
    intent.** One issue with the current implementation of output code
    blocks is there’s no way for developers to indicate that a method is
    returning *already sanitized HTML*. Having this in place helps
    enable ***our goal of completely replacing the old syntax with this
    new syntax in practice***.

    This also means we need to work hard to make sure all new samples,
    books, blog posts, etc. eventually use the new syntax when targeting
    ASP.NET 4.

    Hopefully, the next generation of ASP.NET developers will experience
    this as *being the default output code block syntax* and `<%=` will
    just be a bad memory for us old-timers like punch cards, manual
    memory allocations, and `Do While Not rs.EOF`.

4.  **Make it easy to migrate from ASP.NET 3.5**. We strongly considered
    just changing the existing `<%=` syntax to encode by default. We
    eventually decided against this for several reasons, some of which
    are listed in the above goals. Doing so would make it tricky and
    painful to upgrade an existing application from earlier versions of
    ASP.NET.

    Also, we didn’t want to impose an additional burden for those who
    already do practice good encoding. For those who don’t already
    practice good encoding, this additional burden might prevent them
    from porting their app and thus they wouldn’t get the benefit
    anyways.

### When Can I Use This?

This is a new feature of ASP.NET 4. If you’re developing on ASP.NET 3.5,
you will have to continue to use the existing `<%=` syntax and remember
to encode the output yourself.

In ASP.NET 4 Beta 2, you will have the ability to try this out yourself
with ASP.NET MVC 2 Preview 2. If you’re running on ASP.NET 3.5, you’ll
have to use the old syntax.

### What about ASP.NET MVC 2?

As mentioned, ASP.NET MVC 2 supports this new syntax in its *helper when
running on ASP.NET 4*.

In order to make this possible, we are making a breaking change such
that the relevant helper methods (ones that return HTML as a string)
will return a type that implements `IHtmlString`.

In a follow-up blog post, I’ll write about the specifics of that change.
It was an interesting challenge given that `IHtmlString` is new to
ASP.NET 4, but ASP.NET MVC 2 is actually compiled against ASP.NET 3.5
SP1. :)

