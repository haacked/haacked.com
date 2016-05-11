---
layout: post
title: "A Billion Is Cool"
date: 2016-05-11 -0800
comments: true
categories: [nuget]
---

Yesterday, the NuGet team announced that [NuGet.org](https://nuget.org/) reached [one billion package downloads](http://blog.nuget.org/20160510/The-1st-Billion.1.html)!

![With apologies to everybody for drudging up this tired old meme.](https://cloud.githubusercontent.com/assets/19977/15195972/16db77ae-177f-11e6-8837-9819e8047f15.png)

It's exciting to see NuGet still going strong. As part of the original team that created NuGet, we always had high hopes for its future but were also cognizant of all the things that could go wrong. So seeing hope turn into reality is a great feeling. At the same time, there is still so much more to do. One billion is just a number, albeit a significant and praiseworthy one.

I love that the post calls out the original name for NuGet, aka NuPack. I loved that name and there was a lot of feels about the change at the time, but I learned an important lesson about naming. Not only is it hard, it takes time for people to adjust to _any_ name. Every name we chose felt wrong, but over time, the name and the identity of the product start to mesh together and now, I can't imagine any other name other than NuGet.

Just recently I was cleaning out some long neglected DropBox folders and found an old PowerPoint presentation about a design change to the license acceptance flow for packages. Yes, it's boring stuff but it's the only remnant I have from the design process back in the day. Also, I find it interesting.

We had a goal to make installing packages as frictionless as possible. To that end, we didn't make license acceptance explicit, but instead we noted that by installing the package, you accept its license and we told you where to find the license. It was a more implicit license acceptance flow.

However, this didn't work for everybody. We had to deal with the reality that some package authors (especially large corporations) required explicit acceptance of the license before they could install it. So we made this an opt-in feature for package authors which represented itself like so in the GUI.

![nupack-license-acceptance](https://cloud.githubusercontent.com/assets/19977/15196311/8d4f4ee6-1780-11e6-9240-0b476effaca9.png)

As you can see, I used [Balsamiq](https://balsamiq.com/) to mock up the UI. I used Balsamiq a lot back then. This mock-up is from the time when the project was still called NuPack. It's a fun (to me at least) bit of history.

These days I'm not as involved with NuGet as I used to be, but I have no shortage of opinions on what I hope to see in its future. I may not be contributing to NuGet directly anymore, but I'm still a NuGet package user and author. All of useful repositories [have corresponding packages on NuGet.org](https://www.nuget.org/profiles/haacked).
