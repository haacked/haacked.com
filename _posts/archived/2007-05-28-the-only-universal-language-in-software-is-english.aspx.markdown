---
layout: post
title: The Only Universal Language In Software Is English
date: 2007-05-28 -0800
comments: true
disqus_identifier: 18332
categories: []
redirect_from: "/archive/2007/05/27/the-only-universal-language-in-software-is-english.aspx/"
---

In a [recent
post](https://haacked.com/archive/2007/05/24/ruby-like-syntax-in-c-3.0.aspx "Ruby Like syntaxt in C# 3.0"),
I compared the expressiveness of the Ruby style of writing code to the
current C\# style of writing code. I then went on and demonstrated one
approach to achieving something close to Ruby’s expressiveness using
Extension Methods in C\# 3.0.

The discussion focused on how well each code sample expresses the intent
of the author. Let’s look at the comparison:

**Ruby:**

`20.minutes.ago`

**C\#:**

`DateTime.Now.Subtract(TimeSpan.FromMinutes(20));`

**C\# 3.0 using Extension Methods:**

`20.Minutes().Ago();`

It seems obvious to me that the C\# 3.0 example is more expressive than
the classic C\# approach, but not everyone agrees. Several people have
said something to the effect of:

> Yeah, that’s great for those who speak English.

Another person mentioned that the Ruby style of code panders to English
speakers? [Really?!
Really?!](http://www.youtube.com/p.swf?video_id=RjtVnqZCndo&eurl=http%3A//www.google.com/search%3Fq%3Dsnl%2Breally%26ie%3Dutf-8%26oe%3Dutf-8%26aq%3Dt%26rls%3Dorg.mozilla%3Aen-US%3Aofficial%26cl&iurl=http%3A//img.youtube.com/vi/RjtVnqZCndo/2.jpg&t=OEgsToPDskINT6UiHBRUM4_6iGlfBNhC "Really!?! with Seth and Amy")

Yet somehow, the classic C\# example doesn’t pander to English
speakers? In the Ruby example, I count **2 words in English**, *Minutes*
and *Ago*. In the classic C\# example, I count **8 words in
English**-*Date*, *Time*, *Now*, *Subtract*, *Time*, *Span*, *From*,
*Minutes*(decomposing the class names into their constituent words via
Pascal Casing rules).

Not to mention that all of these code samples flow left-to-right, unlike
languages such as Hebrew and Arabic which flow right to left.

Seems to me that if anything, the classic C\# example panders just as
much if not *more* to the English speaking world than the Ruby example.

One explanation given for this statement is the following:

> `DateTime.Now.Subtract(TimeSpan.FromMinutes(20));` follows a common
> convention across languages, a hierarchical OOP syntax that makes
> sense regardless of your native tongue

I don’t get it. How is `20.minutes.ago` not hierarchical and object
oriented yet we wouldn’t even take a second look at
`DateTime.Now.Day `or `20.ToString()`, both of which are currently in
C\# and familiar to developers.

The key goal in object oriented software is to attempt to develop
abstractions and work with in the domain of those abstractions. That’s
the foundation of OO. Working with a Product object and a Customer
object rather than a large set of procedural methods makes it even
possible to understand a large system.

Let’s look at a typical object oriented code sample found in an [OO
tutorial](http://www.developer.com/net/csharp/article.php/10918_1465681_1 "C# and VB Object-Oriented Programming in VS.NET"):

```csharp
Customer customer = Load<Customer>(id);
Order order = customer.GetLastOrder();
ShippingProvider shipper = Shipping.Create();
shipper.Ship(order);
```

I know I know! This code panders to English! Look at the way it’s
written! *GetLastOrder()*? Shouldn’t that be *ConseguirOrdenPasada()*?

Keep in mind that this all stems from a discussion about Ruby, a
language written by [Yukihiro
Matsumoto](http://en.wikipedia.org/wiki/Yukihiro_Matsumoto "Yukihiro Matsumoto"),
a Japanese computer scientist.

Now why would a Japanese programmer write a programming language that
“panders to English?”

**Maybe because the only language in software that is universal is
English.** It’s just not possible to write a programming language that
would be universally expressive in any human language. What might work
for a Spanish speaker might be confusing to a Swahili speaker. Not to
mention the difficulty in writing a programming language that would read
left to right and right to left (*Palindrome\# anyone?*).

Yet we must find common ground for a programming language, so choosing a
human language we must. For historical reasons, English is that de-facto
language. It’s the reason why all the major programming languages have
English keywords and English words for its class libraries. It’s why you
use the Color class in C\# and not the Colour or 색깔 class.

Now I’m not some America-centrist who says this is the way it *should*
be. I’m just saying this is the way *it is*. Feel free to create a
programming language with all its major keywords in another language and
see how widely it is adopted. It’s a fact of life. **If you’re going to
write software, you better learn some degree of English**.

In conclusion, yes, `20.minutes.ago` does pander to English, but
only because all major programming languages pander to English. C\# is
no exception. In fact, pandering to English is our goal when trying to
write readable software.

