---
title: I Knew How To Validate An Email Address Until I Read The RFC
tags: [code,regex,validation]
redirect_from: "/archive/2007/08/20/i-knew-how-to-validate-an-email-address-until-i.aspx/"
---

Raise your hand if you know how to validate an email address. For those of you with your hand in the air, put it down quickly before someone sees you. It’s an odd sight to see someone sitting alone at the keyboard raising his or her hand. I was speaking metaphorically.

[![at-sign](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IThoughtIKnewHowToValidateAnEmailAddress_E977/at-sign_1.jpg)](http://www.stockxpert.com/browse.phtml?f=profile&l=ErickN "At Sign from stockxpert by ErickN")
Before yesterday I would have raised my hand (metaphorically) as well. I needed to validate an email address on the server. Something I’ve done a hundred thousand times (seriously, I counted) using a handy dandy regular expression in my personal library.

This time, for some reason, I decided to take a look at my underlying assumptions. I had never actually read (or even skimmed) the RFC for an email address. I simply based my implementation on my preconceived assumptions about what makes a valid email address. You know what they say [about assuming](http://jyte.com/cl/when-you-assume-you-make-an-ass-out-of-you-and-me "saying about assumptions").

**What I found out was surprising. Nearly 100% of regular expressions on the web purporting to validate an email address are too strict.**

It turns out that the local part of an email address, the part before the @ sign, allows a lot more characters than you’d expect. According to section 2.3.10 of RFC 2821 which defines SMTP, the part before the `@` sign is called the local part (the part after being the host domain) and it is only intended to be interpreted by the receiving host...

> Consequently, and due to a long history of problems when intermediate
> hosts have attempted to optimize transport by modifying them, **the
> local-part MUST be interpreted and assigned semantics only by the host
> specified in the domain part of the address**.

Section [section 3.4.1](http://tools.ietf.org/html/rfc2822#section-3.4.1 "Section 3.4.1 of rfc2822") of [RFC 2822](http://tools.ietf.org/html/rfc2822 "RFC 2822 Internet Message Format") goes into more detail about the specification of an email address (emphasis mine).

> An addr-spec is a specific Internet identifier that contains a locally
> interpreted string followed by the at-sign character ("@", ASCII value
> 64) followed by an Internet domain.  The locally interpreted string is
> either a **quoted-string** or a **dot-atom**.

A *dot-atom* is a dot delimited series of atoms. An *atom* is defined in [section 3.2.4](http://tools.ietf.org/html/rfc2822#3.2.4 "Section 3.2.4 Atom") as a series of alphanumeric characters and may include the following
characters (*all the ones you need to swear in a comic strip*)...

> ! \$ & \* - = \^ \` \| \~ \# % ' + / ? \_ { }

Not only that, but it’s also valid (though not recommended and very uncommon) to have quoted local parts which allow pretty much any
character. Quoting can be done via the backslash character (what is commonly known as escaping) or via surrounding the local part in double quotes.

[RFC 3696](http://tools.ietf.org/html/rfc3696 "Clarification of internet mailing specs"), *Application Techniques for Checking and Transformation of Names*, was written by the author of the SMTP protocol ([RFC 2821](http://tools.ietf.org/html/rfc2821 "RFC 2821 SMTP")) as a human readable guide to SMTP. In section 3, he gives some examples of valid email addresses.

**These are all valid email addresses!**

-   `Abc\@def@example.com`
-   `Fred\ Bloggs@example.com`
-   `Joe.\\Blow@example.com`
-   `"Abc@def"@example.com`
-   `"Fred Bloggs"@example.com`
-   `customer/department=shipping@example.com`
-   `$A12345@example.com`
-   `!def!xyz%abc@example.com`
-   `_somename@example.com`

*Note: Gotta love the author for using my favorite example person, Joe Blow*.

Quick, run these through your favorite email validation method. Do they all pass?

For fun, I decided to try and write a regular expression ([yes, I know I now have two problems. Thanks.](http://regex.info/blog/2006-09-15/247 "Source of the famous 'Now you have two problems' quote")) that would validate all of these. Here’s what I came up with. (The part in bold is the *local part*. I am not worrying about checking my assumptions for the *domain part* for now.)

```
^(?!\.)("([^"\r\\]|\\["\r\\])*"|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$
```

Note that this expression assumes case insensitivity options are turned on (*RegexOptions.IgnoreCase for .NET*). Yeah, that’s a pretty ugly expression.

I wrote a unit test to demonstrate all the cases this test covers. Each row below is an email address and whether it should be valid or not.

```csharp
[RowTest]
[Row(@"NotAnEmail", false)]
[Row(@"@NotAnEmail", false)]
[Row(@"""test\\blah""@example.com", true)]
[Row(@"""test\blah""@example.com", false)]
[Row("\"test\\\rblah\"@example.com", true)]
[Row("\"test\rblah\"@example.com", false)]
[Row(@"""test\""blah""@example.com", true)]
[Row(@"""test""blah""@example.com", false)]
[Row(@"customer/department@example.com", true)]
[Row(@"$A12345@example.com", true)]
[Row(@"!def!xyz%abc@example.com", true)]
[Row(@"_Yosemite.Sam@example.com", true)]
[Row(@"~@example.com", true)]
[Row(@".wooly@example.com", false)]
[Row(@"wo..oly@example.com", false)]
[Row(@"pootietang.@example.com", false)]
[Row(@".@example.com", false)]
[Row(@"""Austin@Powers""@example.com", true)]
[Row(@"Ima.Fool@example.com", true)]
[Row(@"""Ima.Fool""@example.com", true)]
[Row(@"""Ima Fool""@example.com", true)]
[Row(@"Ima Fool@example.com", false)]
public void EmailTests(string email, bool expected)
{
  string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" 
    + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" 
    + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

  Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
  Assert.AreEqual(expected, regex.IsMatch(email)
    , "Problem with '" + email + "'. Expected "  
    + expected + " but was not that.");
}
```

Before you call me a completely anal nitpicky numnut (you might be right, but wait anyways), I don’t think this level of detail in email validation is absolutely necessary. Most email providers have stricter rules than are required for email addresses. For example, Yahoo requires that an email start with a letter. There seems to be a standard stricter set of rules most email providers follow, but as far as I can tell it is undocumented.

I think I’ll sign up for an email address like `phil.h\@\@ck@haacked.com` and start bitching at sites that require emails but don’t let me create an account with this new email address. Ooooooh I’m such a troublemaker.

The lesson here is that it is healthy to challenge your preconceptions and assumptions once in a while and to never let me near an RFC.

UPDATES: Corrected some mistakes I made in reading the RFC. See! Even after reading the RFC I still don’t know what the hell I’m doing! Just goes to show that [programmers can’t read](https://haacked.com/archive/2007/02/27/Why_Cant_Programmers._Read.aspx "Why Can’t Programmers... Read?"). I updated the post to point to [RFC 822](http://www.faqs.org/rfcs/rfc822.html "RFC 822 Standard for the format of ARPA Internet Text Messages") as well. The *original* RFC.

