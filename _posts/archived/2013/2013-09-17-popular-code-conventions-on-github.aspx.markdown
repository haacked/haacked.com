---
title: Popular Code Conventions on GitHub
tags: [code,oss,github]
redirect_from: "/archive/2013/09/16/popular-code-conventions-on-github.aspx/"
---

The [first GitHub Data
Challenge](https://github.com/blog/1118-the-github-data-challenge "GitHub data challenge")
launched in 2012 and asked the following compelling question: *what
would you do with all this data about our coding habits?*

> The GitHub public timeline is [now easy to query and
> analyze](https://github.com/blog/1112-data-at-github). With hundreds
> of thousands of events in the timeline every day, there are countless
> stories to tell.
>
> Excited to play around with all this data? We'd love to see what you
> come up with.

It was so successful, we [did it
again](https://github.com/blog/1450-the-github-data-challenge-ii "GitHub Data Challenge 2")
this past April. One of those projects really caught my eye, a site that
analysise [Popular Coding Conventions on
GitHub](http://sideeffect.kr/popularconvention/ "Popular Coding Conventions").
It ended up winning [second
place](https://github.com/blog/1544-data-challenge-ii-results "GitHub Data Challenge 2 winner").

It analyzes GitHub and provides interesting graphs on which coding
conventions are more popular among GitHub users based on analyzing the
code. This lets you fight your [ever present software religious
wars](https://haacked.com/archive/2006/02/08/OnReligiousWarsinSoftware.aspx "Religious wars in software")
with some data.

For example, here’s how the Tabs vs Spaces debate lands among Java
developers on GitHub.

[![java-tabs-vs-spaces](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/PopularCodeConventionsonGitHub_D177/java-tabs-vs-spaces_thumb.png "java-tabs-vs-spaces")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/PopularCodeConventionsonGitHub_D177/java-tabs-vs-spaces_2.png)

With that, I’m sure nobody ever will argue tabs over spaces again right?
RIGHT?!

What about C#?!
----------------

UPDATE: [JeongHyoon Byun](https://github.com/outsideris) added [C#
support](http://sideeffect.kr/popularconvention/#c#)! Woohoo!

~~Sadly, there is no support for C# yet.~~ I logged [an issue in the
repository](https://github.com/outsideris/popularconvention/issues/14 "Add C#")
about that a while back and was asked to provide examples of C#
conventions.

I finally [got around to it
today](https://gist.github.com/Haacked/6601104 "C# Code Conventions"). I
simply converted the Java examples to C# and added one or two that I’ve
debated with my co-workers.

However, to get this done faster, perhaps one of you would be willing to
add a simple CSharp convention parser to this project. Here’s a [list of
the current
parsers](https://github.com/outsideris/popularconvention/tree/master/src/parser "Parsers")
that can be used as the basis for a new one.

Please please please somebody step up and write that parser. That way I
can show my co-worker [Paul
Betts](http://twitter.com/paulcbetts "Paul on Twitter") the error of his
naming ways.

