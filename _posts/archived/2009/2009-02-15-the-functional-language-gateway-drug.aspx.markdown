---
title: The Functional Language Gateway Drug
tags: [code]
redirect_from: "/archive/2009/02/14/the-functional-language-gateway-drug.aspx/"
---

***Alternate Title: Linq, it’s not just for SQL.***

I admit, I’m not very proficient with functional programming. It almost
feels like a gang war at times - on one side of the tracks is Turing’s
crew, sporting their imperative ways. On the other side is the [Church
group](http://www.scribd.com/doc/44241/Churchs-Thesis-and-Functional-Programming "Church's Thesis and Functional Programming"),
luring wayward souls onto their turf with the promise of code salvation
in the form of functional language.

[Matt
Podwysocki](http://weblogs.asp.net/Podwysocki/ "Matthew Podwysocki's Blog")
is one of those Church evangelists, constantly reaching out to me, a
lost soul, with the promises of eternal code salvation in the form of
[F\#](http://msdn.microsoft.com/en-us/fsharp/default.aspx "F#"). I keep
meaning to check it out, but you know how that goes.

What I’ve slowly come to realize though is that the more I use and
understand the Linq extensions in C#, the more functional my
programming has become in certain cases.

**[![450px-Native\_American\_tobacco\_flower](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/LinqIsNotJustForSQL_E18F/450px-Native_American_tobacco_flower_thumb_1.jpg "450px-Native_American_tobacco_flower")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/LinqIsNotJustForSQL_E18F/450px-Native_American_tobacco_flower_4.jpg "Tobacco Flower: Photographer: William Rafti of the William Rafti Institute")It
turns out that C# is the ultimate gateway drug for functional
programming^1^.** It has just enough functional elements to give you a
taste for functional, but with enough friction at times (for example,
the type inference is not as good as F\#) that it may slowly push me
over.

Let me give you a recent concrete example using Subtext. One of the
features Subtext has is the ability to take the title of a blog post,
and automatically generate a [URL
slug](http://codex.wordpress.org/Glossary#Slug "Slug") from the title.

Slugs have to be unique, so we want to make sure that the slug we
generate doesn’t conflict. For fun, I implemented it in such a way that
we could handle up to 6 conflicts.

For example, suppose you wrote a blog post entitled “Hello World”. The
first time you published it, you would have the slug “hello-world”. When
you wrote and published a new blog post with the same title, we’d append
“again” to the end. Here’s what would happen if you kept repeating this
process.

-   hello-world
-   hello-world-again
-   hello-world-yet-again
-   hello-world-and-again
-   hello-world-once-again
-   hello-world-to-beat-a-dead-horse
-   *We throw an exception*

Yeah, it’s kind of a stupid feature. I doubt anyone would ever post more
than two blog posts with the same title. In a way, it’s a bit of an
easter egg. The original code for this was very imperative. Here’s the
gist of the code:

```csharp
string EnsureUniqueSlug(string slug, string separator) {
  Entry currentEntry = Repository.GetEntry(slug);
  int tryCount = 0;
  string newSlug;
  while (currentEntry != null) {
    switch (tryCount) {
      case 0:
        newSlug = slug + separator + "Again";
        break;
      case 1:
        newSlug = slug + separator + "Yet" + separator + "Again";
        break;
      case 2:
        newSlug = slug + separator + "And" + separator + "Again";
        break;
      case 3:
        newSlug = slug + separator + "Once" + separator + "More";
        break;
      case 4:
        newSlug = slug + separator + "To" + separator + "Beat" 
          + separator + "A" + separator + "Dead" 
          + separator + "Horse";
        break;
      case 5:
        throw new InvalidOperationException();
    }
    tryCount++;
    currentEntry = Repository.GetEntry(newslug);
  }
  return newSlug;
}
```

I was revisiting this code today, and I realized I could write this more
succinctly using Linq extensions.

When you step back a moment, what I have to start with is an enumeration
of suffixes. What I want to do is transform them into potential slugs,
and then find the first slug where there is no matching slug in the
database.

Functional languages are great for working with sets and doing
transformations over sets. At least that’s what Matt tells me. Here’s
what I ended up doing.

```csharp
string EnsureUniqueness(string originalSlug, string separator) {
  string[] suffixFormats = new[] { 
    string.Empty, "{0}Again", "{0}Yet{0}Again", "{0}And{0}Again"
      , "{0}Once{0}Again", "{0}Once{0}More"
      , "{0}To{0}Beat{0}A{0}Dead{0}Horse" };
  var slugs = suffixFormats.Select(
    s => originalSlug + String.Format(s, separator));
  return slugs.First(slug => Repository.GetEntry(slug) == null);
}
```

The first line is a static array of the potential suffixes. At this
point, I should really move this line outside of this method and perhaps
even have this list be configurable. But for this blog post, let’s leave
it here.

The second line converts the suffixes into an enumeration of slugs. I
then simply call the `First` method on that list of slugs passing in a
lambda which specifies a condition. The `First` method will return the
first element in the enumeration where the lambda returns true.

In other words, it’ll return the first slug where the repository tells
me there is no blog post with a matching slug. If there is no match, the
`First` method throws an `InvalidOperationException`.

For those who are not familiar with lambdas and and the extension
methods I used, the second code might be a bit confusing. But once you
know what’s going on, I think it’s much more readable, simple, and shows
my intent better.

It reads how I think about the problem.

1.  Convert the list of suffixes into a list of potential slugs
2.  Grab the first slug where there is no matching entry in the database

What would be really cool is if I could somehow switch to F\# inline
with a C# file. Kind of crazy, but it would be the thing that would
probably get me to actually use it in a project.

For those of you who have been doing functional programming for a long
time, you’ll probably scoff at this simple example, but for an old
imperative programmer like me, it feels like a new world opening up.

^1^After I wrote this, I realized that Ruby might actually be the
ultimate gateway drug for functional programming. But I’m kind of
focusing on static typed language afficionados, so forgive me. ;)

