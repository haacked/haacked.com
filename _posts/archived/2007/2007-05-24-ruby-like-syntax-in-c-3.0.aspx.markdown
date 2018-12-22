---
title: Ruby-Like Expressiveness in C# 3.0
date: 2007-05-24 -0800
tags: [csharp,ruby]
redirect_from: "/archive/2007/05/23/ruby-like-syntax-in-c-3.0.aspx/"
---

UPDATE: Looks like [Ian
Cooper](http://iancooper.spaces.live.com/ "Ian Cooper") had posted
pretty much the same code [in the
comments](http://www.hanselman.com/blog/ProgrammerIntentOrWhatYoureNotGettingAboutRubyAndWhyItsTheTits.aspx#1bb18d7f-4ae7-4442-a624-f362d03a1233 "Ian's Comment")
to Scott’s blog post. I hadn’t noticed it. He didn’t have a chance to
compile it, so consider this post a validation of your example Ian! :)

[Scott Hanselman](http://www.hanselman.com/blog/ "Scott Hanselman")
recently [wrote a
post](http://www.hanselman.com/blog/ProgrammerIntentOrWhatYoureNotGettingAboutRubyAndWhyItsTheTits.aspx "Programmer Intent or What you’re not getting about Ruby and why it’s the tits")
about how Ruby has tits or is the tits or something like that. I agree
with much of it. Ruby is in many respects a nice language to use if you
think in Ruby.

One of the comparisons of the syntactic sugar Scott showed was this:

**Java:**

`new Date(new Date().getTime() - 20 * 60 * 1000);`

**Ruby:**

`20.minutes.ago`

That is indeed nice. But I was on the phone with Rob Conery talking
about this when it occurred to me that we’ll be able to do this with
[C# 3.0
extension](http://weblogs.asp.net/scottgu/archive/2007/03/13/new-orcas-language-feature-extension-methods.aspx "New "Orcas" Language Feature: Extension Methods")
methods. That link there is a blog post by [Scott
Guthrie](http://weblogs.asp.net/scottgu/ "Scott Guthrie") talking about
this feature.

Not having any time to install Orcas and try it out, I asked [Rob
Conery](http://blog.wekeroad.com/ "Rob Conery’s Blog") to be my code
monkey and try this out. So we fired up GoToMeeting and started pair
programming. Here is what we came up with:

```csharp
public static class Extenders
{
  public static DateTime Ago(this TimeSpan val)
  {
    return DateTime.Now.Subtract(val);
  }

  public static TimeSpan Minutes(this int val)
  {
    return new TimeSpan(0, val, 0);
  }
}
```

Now we can write a simple console program to test this out.

```csharp
class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine(20.Minutes().Ago());
    Console.ReadLine();
  }
}
```

And it worked!

So that’s very close to the Ruby syntax and not too shabby. It would be
even cleaner if we could create extension properties, but our first
attempt didn’t seem to work and we ran out of time (Rob actually thinks
eating lunch is important).

Found out from ScottGu that Extension Properties aren’t part of the
language yet, but are being considered as a possibility in the future.

So now add this to the comparison:

**C# 3.0**

`20.Minutes().Ago();`

Just one of the many cool new language features coming soon.

