---
title: The Landmine of Parsing HTML and Stripping HTML Comments
tags: [code,regex]
redirect_from: "/archive/2008/11/09/the-landmine-of-parsing-html-and-stripping-html-comments.aspx/"
---

A while ago I wrote a blog post about how painful it is to [properly
parse an email
address](https://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx "Validating an email addres").
This post is kind of like that, except that this time, I take on HTML.

I’ve written about [parsing HTML with a regular
expression](https://haacked.com/archive/2005/04/22/Matching_HTML_With_Regex.aspx "Matching HTML with regular expressions")
in the past and pointed out that it’s extremely tricky and probably not
a good idea to use regular expressions in this case. In this post, I
want to strip out HTML comments. Why?

I had some code that uses a regular expression to strip comments from
HTML, but found one of those feared “pathological” cases in which it
seems to never complete and pegs my CPU at 100% in the meanwhile. I
figure I might as well look into trying a character by character
approach to stripping HTML.

It sounds easy at first, and my first attempt was roughly 34 lines of
procedural style code. But then I started digging into the edge cases.
Take a look at this:

```csharp
<p title="<!-- this is a comment-->">Test 1</p>
```

Should I strip that comment within the attribute value or not?
Technically, this isn’t valid HTML since the first angle bracket within
the attribute value should be encoded. However, the three browsers I
checked (IE 8, FF3, Google Chrome) all honor this markup and render the
following.

![funky
comment](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TheLandmineofParsingHTMLandStrippingHTML_E73B/funky-comment_3.png "funky comment")

Notice that when I put the mouse over “Test 1” and the browser rendered
the value of the *title* attribute as a tooltip. That’s not even the
funkiest case. Check this bit out in which my comment is an unquoted
attribute value. Ugly!

```csharp
<p title=<!this-comment>Test 2</p>
```

Still, the browsers dutifully render it:

![funkier-comment](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TheLandmineofParsingHTMLandStrippingHTML_E73B/funkier-comment_3.png "funkier-comment") 

At this point, It might seem like I’m spending too much time worrying
about crazy edge cases, which is probably true. Should I simply strip
these comments even if they happen to be within attribute values because
they’re technically invalid. However, it worries me a bit to impose a
different behavior than the browser does.

Just thinking out loud here, but what if the user can specify a style
attribute (bad idea) for an element and they enter:

`<!>color: expression(alert('test'))`

Which fully rendered yields:
`<p style="<!>color: expression(alert('test'))">`

If we strip out the comment, then suddenly, the style attribute might
lend itself to an [attribute based XSS
attack](http://jeremiahgrossman.blogspot.com/2007/07/attribute-based-cross-site-scripting.html "Attribute Based XSS").

I tried this on the three browsers I mentioned and nothing bad happened,
so maybe it’s a non issue. But I figured it would probably make sense to
go ahead and strip the HTML comments in the cases that the browser. So I
decided to not strip any comments within an HTML tag, which means I have
to identify HTML tags. That starts to get a bit ugly as \<foo \> is
assumed to be an HTML tag and not displayed while \<çoo /\> is just
content and displayed.

Before I show the code, I should clarify something. I’ve been a bit
imprecise here. Technically, a comment starts with a – character, but
I’ve referred to markup such as `<!>` as being a comment. Technically
it’s not, but it behaves like one in the sense that the browser DOM
recognizes it as such. With HTML you can have multiple comments between
the \<! and the \> delimiters according to [section 3.2.5 of RFC
1866](http://www.freesoft.org/CIE/RFC/1866/15.htm "Section 3.2.5 RFC 1866").

>     3.2.5. Comments
>
>        To include comments in an HTML document, use a comment declaration. A
>        comment declaration consists of `<!' followed by zero or more
>        comments followed by `>'. Each comment starts with `--' and includes
>        all text up to and including the next occurrence of `--'. In a
>        comment declaration, white space is allowed after each comment, but
>        not before the first comment.  The entire comment declaration is
>        ignored.
>
>           NOTE - Some historical HTML implementations incorrectly consider
>           any `>' character to be the termination of a comment.
>
>        For example:
>
>         <!DOCTYPE HTML PUBLIC "-//IETF//DTD HTML 2.0//EN">
>         <HEAD>
>         <TITLE>HTML Comment Example</TITLE>
>         <!-- Id: html-sgml.sgm,v 1.5 1995/05/26 21:29:50 connolly Exp  -->
>         <!-- another -- -- comment -->
>         <!>
>         </HEAD>
>         <BODY>
>         <p> <!- not a comment, just regular old data characters ->
>         

The code I wrote today was straight up old school procedural code with
no attempt to make it modular, maintainable, object oriented, etc… I
posted it [to refactormycode.com
here](http://refactormycode.com/codes/597-strip-html-comments "Refactor My Code")
with the unit tests I defined.

In the end, I might not use this code as I realized later that what I
really should be doing in the particular scenario I have is simply
stripping all HTML tags and comments. In any case, I hope to never have
to parse HTML again. ;)

