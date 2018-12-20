---
title: "A Billion Is Cool"
date: 2016-05-11 -0800
tags: [nuget]
---

Yesterday, the NuGet team announced that [NuGet.org](https://nuget.org/) reached [one billion package downloads](http://blog.nuget.org/20160510/The-1st-Billion.1.html)!

![With apologies to everybody for drudging up this tired old meme.](https://cloud.githubusercontent.com/assets/19977/15195972/16db77ae-177f-11e6-8837-9819e8047f15.png)

It's exciting to see NuGet still going strong. As part of the original team that created NuGet, we always had high hopes for its future but were also cognizant of all the things that could go wrong. So seeing hope turn into reality is a great feeling. At the same time, there is still so much more to do. One billion is just a number, albeit a significant and praiseworthy one.

I love that the post calls out the original name for NuGet, aka NuPack. I loved the original name and there was a lot of upset feelings about the change at the time, but the experience taught me an important lesson about naming. Not only is it hard, there will always be a lot of people who will immediately hate whatever name you choose. It takes time for people to adjust to _any_ name. Unless the name is truly terrible like Qwikster. What was that about?

At the time, every name we chose felt wrong, but over time, the name and the identity of the product start to mesh together and now, I can't imagine any other name other than NuGet. Except for HaackGet. I would have totally been all over that.

Just recently I cleaned out some long neglected DropBox folders and found an old PowerPoint presentation about a design change to the license acceptance flow for packages. Yes, license acceptance sounds like boring stuff but it's the only remnant I have from the design process back in the day and it's more interesting than you think if you're a licensing nerd like me.

We had a goal to make installing packages as frictionless as possible. To that end, we didn't make license acceptance explicit, but instead we noted that by installing the package, you accept its license and we told you where to find the license. It was a more implicit license acceptance flow that we felt was unintrusive and would serve most package authors fine.

However, this didn't work for everybody. We had to deal with the reality that some package authors (especially large corporations such as Microsoft) required explicit acceptance of the license before they could install it. So we made this an opt-in feature for package authors which represented itself like so in the GUI.

![NuPack License Acceptance Flow Mockup](https://cloud.githubusercontent.com/assets/19977/15196311/8d4f4ee6-1780-11e6-9240-0b476effaca9.png)

As you can see, I used [Balsamiq](https://balsamiq.com/) to mock up the UI. I used Balsamiq a lot back then to play around with UI mockups. This mockup is from the time when the project was still called NuPack. It's a fun (to me at least) bit of history.

These days I'm not as involved with NuGet as I used to be, but I have no shortage of opinions on what I hope to see in its future. I may not be contributing to NuGet directly anymore, but I'm still a NuGet package user and author. All of my useful repositories [have corresponding packages on NuGet.org](https://www.nuget.org/profiles/haacked).
