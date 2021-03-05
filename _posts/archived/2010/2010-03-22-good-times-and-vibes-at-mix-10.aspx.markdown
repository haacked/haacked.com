---
title: Good Times and Vibes at Mix 10
tags: [conferences]
redirect_from: "/archive/2010/03/21/good-times-and-vibes-at-mix-10.aspx/"
---

Last week I spent a few days in Las Vegas attending the [Mix 10
conference](http://live.visitmix.com/ "Mix 10 website"). Mix is billed
as …

> A 3 day conference for web designers and developers building the
> world's most innovative web sites.

Which certainly reflects its origins as a conference focused on the web
and web standards. But this year, it seemed that the scope for Mix was
expanded to be about, well, a *Mix* of technologies as the Windows Phone
7 series figured prominently at the conference.

[![shanselman-haacked-jeresig](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Mix10_E26B/shanselman-haacked-jeresig_thumb.jpg "shanselman-haacked-jeresig")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Mix10_E26B/shanselman-haacked-jeresig.jpg)
*Scott Hanselman and I are seen here attempting to tutor this young man
about a language called “JavaScript”*

### Mix of communities

One aspect I love about Mix is it’s also a Mix of communities. Sure,
it’s heavily Microsoft dominated, since it is, well, Microsoft that puts
it on, but this conference has never been shy about bringing in people
outside of the Microsoft community to speak.

One of the speakers I was excited to finally meet in person was [John
Resig](http://ejohn.org/ "John Resig"), the creator of the very popular
jQuery JavaScript library seen in the above photograph to the right.
Yes, for those of you amongst the humor impaired, the caption is a joke
(thanks to [Peter Kellner](http://peterkellner.net/ "Peter Kellner") who
took the photo).

Also speaking was [Douglas
Crockford](http://www.crockford.com/ "Douglas Crockford") (inventor of
JSON) who had the best slide of the show with the words…

> IE 6 Must Die!

Preach it brother Crockford! Since I was able to chat with him in
person, I confirmed it was really Douglas who left [this
comment](https://haacked.com/archive/2009/06/26/too-late-to-change-json.aspx#72568 "Is it too late to change JSON")
on my JSON post suggesting that requiring POST was a good change to the
JsonResult in ASP.NET MVC 2. I really enjoyed having the opportunity to
chat with him about security and JSON.

### Sessions

If you’re interested in watching the keynotes, check out [Day
1](http://live.visitmix.com/MIX10/Sessions/KEY01 "Day 1 keynote")
(Silverlight, Windows Phone 7 series) and [Day
2](http://live.visitmix.com/MIX10/Sessions/KEY02 "Day 2 keynote") (IE9,
Web Development, OData).

I presented two sessions (click on the title for the video).

> **[What’s new in ASP.NET MVC
> 2](http://live.visitmix.com/MIX10/Sessions/FT04 "Session FT04 Video")** 
>
> Come see and hear about the latest innovations in ASP.NET MVC 2 and
> the tooling support in Microsoft Visual Studio 2008 and 2010. We
> introduce you to a range of productivity (and extensibility)
> enhancements such as template helpers, model validation, and the new
> "Areas" feature, which enhances the team development of large
> websites. With template helpers you can get your website up and
> running for any data entity type without having to create UI. With
> improved server side validation and brand new client side validation
> support, your business data model can define the behavior of your
> application automatically. All this and more!
>
> **[The HaaHa Show: Microsoft ASP.NET MVC Security with Haack and
> Hanselman](http://live.visitmix.com/MIX10/Sessions/FT05 "Session FT05 Video")**
>
> The HaaHa brothers take turns implementing features on an ASP.NET MVC
> website. Scott writes a feature, and Phil exploits it and hacks into
> the system. We analyze and discuss the exploits live on stage and then
> close them one by one. Learn about XSS, CSRF, JSON Hijacking and more.
> Is \*your\* site safe from the Haack?

For those interested in seeing the decks, trying out the code, and
perhaps reading the checklist I use for my demos (the checklist is there
to help in case I freeze from stage fright), I’ve made them all
available for [these two talks in a single zip
file](http://demo.haacked.com/presentations/phil-mix10-demos.zip "My Mix10 Demos").

For the most part, I thought the talks went well, despite some technical
difficulties. During my first talk, despite my preparation, I had a demo
go wrong due to what I later realized was a comedic chain of errors.

I had a pre-baked attribute I just needed to add to my project
containing my entities. However, I accidentally added it to my web
project instead, which references the entities project. I then proceeded
to import the namespace for the attribute on a class in the entities
project. At least that’s what I thought I was doing, but since my
entities project didn’t reference the web project (where I dragged said
attribute), I accidentally ran the Generate from Usage command adding a
new blank attribute class to the project.

You can probably see the surprise and then concern on my face as my big
TADA moment where I show the feature working fails to materialize. ;) At
least I was able to recover from this demo failure with the help of the
audience. Scott and I had a demo failure where I had the wrong version
of an app in our machines so we had to tap dance around that failure.

### jQuery and Microsoft

If you missed it, one of the big announcements (at least big to me as an
open source guy) was that Microsoft is going to focus on investing in
jQuery as our primary technology for client browser scripting. Part of
this includes contributing back to jQuery, though any contributions we
supply will go through the same approval process as any contribution
from any other contributor would. No special treatment as far as I know.

I’m very excited about this as it’s been a long time coming. Stephen
Walther has more details on his blog post, [Microsoft, jQuery, and
Templating](http://stephenwalther.com/blog/archive/2010/03/16/microsoft-jquery-and-templating.aspx "Microsoft, jQuery, Templating").

### The Attendee Party

The attendee party this year was held at LAX in the Luxor. It was a nice
venue except it didn’t have any sort of outdoor patio you could escape
to get a breath of fresh air like there was at TAO.

Even so, we had a great time there and you can see many of the pics from
the [Mix10 flickr
set](http://www.flickr.com/photos/mixevent/sets/72157622942879062/).
Afterwards, several of us went to Pure at Ceasars. When we got there,
there was a huge line of beautiful people. However, we were able to go
up to the rope, show the stamps from the attendee party, and the
bouncers waved us right in. It was the total rockstar treatment, which
was a lot of fun. I can only imagine the thoughts going through the
heads of all those people waiting in line wondering who the heck are
these nerds and why are they getting the VIP treatment? :)

### Summary

All in all, it was a great conference. I always manage to have a good
time in Las Vegas, even when losing a bit of money at Poker. I met
countless people, many with interesting questions on ASP.NET MVC. If I
forget your name the next time I see you, I apologize in advance. Don’t
be shy in reminding me. :)

