---
title: Internationalized URLs
tags: [subtext]
redirect_from: "/archive/2007/11/29/internationalized-urls.aspx/"
---

Despite an international team of committers to
[Subtext](http://subtextproject.com/ "SubtextProject.com") and the fact
that [MySpace China
uses](https://haacked.com/archive/2007/10/29/subtext-powers-myspace-china-blogs.aspx "MySpace China Blogs Powered By Subtext")
a customized version of Subtext for its blog, I am ashamed to say that
Subtext’s support for internationalization has been quite weak.

![world
map](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/InternationalizedURLs_604/world-map_3.jpg)

True, I did once write that [The Only Universal Language in Software is
English](https://haacked.com/archive/2007/05/28/the-only-universal-language-in-software-is-english.aspx "English is lingua franca of software development"),
but I didn’t mean that English is the only language that *matters*,
especially on the web.

One area that we need to improve is in dealing with international URLs.
For example, if I’m a user in Korea, I should be able to write a post
with a Korean domain and a Korean title and thus have a friendly URL
like so:

> **http://하쿹.com/blog/안녕하십니까.aspx**

*(As an aside, roughly speaking, 하쿹 would be pronounced hah-kut. About
as close as I can get to haacked which is pronounced like hackt.)*

If you're a kind soul, you will forgive us for punting on this issue for
so long. After all, [RFC
2396](ftp://ftp.isi.edu/in-notes/rfc2396.txt "RFC 2396 URI"), which
defines the syntax for Uniform Resource Identifiers (URI) only allows
for a subset of ASCII (about 60 characters).

But then again, I’ve been hiding behind this RFC as an excuse for a
while fully knowing there are workarounds. I have just been too busy to
fix this.

There are two issues here actually, the hostname (aka domain name) which
is quite restrictive and cannot be URL encoded, AFAIK, and the rest of
the URL which can be encoded.

The domain name issue is resolved by the diminutively named Punycode
(described in [RFC
3492](http://www.ietf.org/rfc/rfc3492.txt "Punycode: A bootstring encoding of unicode for IDNA")).
Punycode is a protocol for converting Unicode strings into the more
limited set of ASCII characters for network host names.

For example, **http://你好.com/** translates to
**http://xn--6qq79v.com/**in Punycode.

Fortunately, this issue is pretty easy to fix. Since the browser is
responsible for converting the Unicode domain name in the URL to
Punycode, all we need to do in Subtext is allow users to setup a
hostname that contains Unicode and we can then convert that to Punycode
using something like the [Punycode / IDN library for .NET
2.0](http://www.simpledns.com/kb.aspx?kbid=1190 ".NET library for converting hostnames to punycode").
For this blog post, I used the web based [phlyLabs IDNA
Converter](http://idnaconv.phlymail.de/ "Punycode converter") for
converting Unicode to Punycode.

The second issue is rest of the URL. When you enter a title of a URL in
Subtext, we convert that to a human and URL friendly ASCII “slug”. For
example, if you enter the title “I like lamp” for a blog post, Subtext
creates the friendly URL ending with “**i\_like\_lamp**.aspx”.

We haven’t totally ignored international URLs. For international western
languages, we have code that effectively replaces accented characters
with a close ASCII equivalent. A couple of examples (there are more in
our unit tests) are:

> **Åñçhòr çùè** becomes **Anchor\_cue**
>
> **Héllò wörld** becomes **Hello\_world**

Unfortunately for my Korean brethren, something like **안녕하십니까**
becomes *(empty string)*. Well that totally sucks!

The thing is, the simple solution in this case is to just allow the
Unicode Korean word as the slug. Browsers will apply the correct URL
encoding to the URL. Thus **https://haacked.com/안녕하십니까/** would
become a request for
**https://haacked.com/%EC%95%88%EB%85%95%ED%95%98%EC%8B%AD%EB%8B%88%EA%B9%8C/**and
everything works just fine as far as I can tell. Please note that
Firefox 2.0 actually replaces the Unicode string in the address bar with
the encoded string while IE7 displays the Unicode as-is, but makes the
request using the encoded URL (as confirmed by
[Fiddler](http://www.fiddlertool.com/fiddler/ "Fiddler")).

For western languages in which we can do a decent enough conversion to
ASCII, the benefit there is the URL remains somewhat readable and
“friendlier” than a long URL encoded string. But for non-western
scripts, we have no choice but to deal with these ugly URL encoded
strings (at least in Firefox).

The interesting thing is, when researching how sites in China handle
internationalized URLs, I discovered that in the same way we did, they
simply punt on the issue. For example,
[http://baidu.com/](http://baidu.com/ "Baidu search engine"), the most
popular search engine in China last I checked, has English URLs.

