---
title: Using a Regular Expression to Match HTML
date: 2004-10-25 -0800
disqus_identifier: 1471
tags:
- code
- regex
redirect_from: "/archive/2004/10/24/usingregularexpressionstomatchhtml.aspx/"
---

I just love regular expressions. I mean look at the sample below.

`</?\w+((\s+\w+(\s*=\s*(?:".*?"|'.*?'|[\^'">\s]+))?)+\s*|\s*)/?>`

What’s not to like?

Ok I admit, I was a bit intimidated by regular expressions when I first started off as a developer. All I needed was a Substring method and an `IndexOf` method and I was set. But after a few projects that required some intense text processing, I realized the power and utility of regular expressions. They should be on the tool belt of every developer.
To that end, I recommend [Mastering Regular Expressions](http://www.amazon.com/gp/product/0596528124?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=0596528124) by Jeffrey Friedl. This is really *THE* book on Regular Expressions. Reading it will make your Regex-Fu powerful.

So let’s look at a common task of matching HTML tags within the body of some text. When you initially think to parse an HTML tag, it seems quite easy. You might consider the following expression:

`</?\w+\s+[\^>]*>`

Roughly Translated, this expression looks for the beginning tag and tag name, followed by some white-space and then anything that doesn’t end the tag.

Now this will probably work 99 times out of 100, but there’s a flaw in this expression. Do you see it? What if I asked you to match the following tag?

`<img title="displays >" src="big.gif">`

Hopefully you see the issue here. The expression will match

`<img title="displays >`

Unfortunately, this implementation is too naive. We have to consider the fact that the greater-than symbol does not end a tag if it’s within a quoted attribute value. Thus we must correctly match attributes.

Now there are four possible formats for an Html attribute

`name="double quoted value"`
`name='single quoted value'`
`name=notquotedvaluewithnowhitespace`
`name`

Each of these cases are quite simple. In the first case, you could do the following:

`\w+\s*=\s*"[\^"]*"`

The portion `"[\^"]*"` matches a double quote, followed by any non double quote characters, followed by a double quote. Another way to express this is to use lazy evaluation as such:

`\w+\s*=\s*".*?"`

The portion `".*?"` uses lazy evaluation (the "lazy star") to match as few characters as possible. For example, if we had a string like so

`<a name=test value="test2">`

evaluating `".*"` (aka greedy) would match

`"test" value="test2"`

However using the lazy evaluation consumes the fewest characters that match the expression, thus the first match using `".*?"` would be `"test"` and the second match is `"test2"`.

The full expression for matching an HTML tag is that lovely mash of characters presented at the very beginning of this post. It’s a modified version of the one presented in Friedl’s book

However I wouldn’t recommend you just plunk that down in your code. Rather, you should consider adding it to a regular expression library assembly.

Don’t know how? Well I’ll show you a code listing for an exe that when run, builds a fully compiled version of this regular expression into an assembly that you can then reference in any project. In a later installment, I’ll explain in more detail just what the code is doing and how to use the compiled assembly. How irresponsible of me not to do that now. ;)

*[Source Listing](https://gist.github.com/Haacked/7729259 "Source Code Listing")*
