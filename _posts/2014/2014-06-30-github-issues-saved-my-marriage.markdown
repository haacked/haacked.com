---
layout: post
title: "GitHub Saved My Marriage"
date: 2014-06-30 -0800
comments: true
categories: [personal github]
---

GitHub is a great tool for developers to work together on software. Though its primary focus is software, a lot of people find it useful for non-software projects. For example, a co-worker of mine has a repository where he [tracks a pet project](https://github.com/thedaniel/xl600):

> I bought a crappy 1987 Honda XR600 and I am going to turn it into something awesome

A while back, Wired ran an article about [a man who renovated his home on GitHub](http://www.wired.com/2013/01/this-old-house/). He even has a [3-D model of his beskpoke artisanal bathroom plug](https://github.com/canadaduane/house/blob/master/bathroom/bathtub-plug/plug.stl). Send a pull request why dontcha?

![bathroom-plug](https://cloud.githubusercontent.com/assets/19977/3211911/e5d38f28-ef37-11e3-9b4b-49c1da1f0ad8.png)

Another person [dedicated his genome to the public domain on GitHub](http://manu.sporny.org/2011/public-domain-genome/). Sadly, genetic technology is not quite at the point where he can merge a pull request all the way to his own body. But who knows? Someday it might be possible to hit that green Merge button and instantly sprout wings. Of course, the downside of such genetic tinkering is you'd need a new wardrobe. I'm not sure Gap sells shirts with wing holes.

Just the other day, I read a blog post about a company that uses [GitHub for everything](http://wiredcraft.com/posts/2013/09/18/github-for-everything.html).

* Internal wiki
* Recruitment process
* Day-to-day operations
* Marketing efforts
* And a lot more ...

## As for me...

Meanwhile, I used GitHub to save my marriage. Ok, that might be a tiny bit of hyperbole for dramatic effect (right honey? Right?!!!).

Let me back up a moment to provide some context.

One of the central points of [David Allen’s Getting Things Done](http://www.amazon.com/gp/product/B000WH7PKY/ref=as_li_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=B000WH7PKY&linkCode=as2&tag=youvebeenhaac-20&linkId=CCTMDNDTW52UYUWH) system is that all the lists of stuff we hold in our head uses up “psychic RAM” and that creates stress. This “psychic weight” drags us down and wears on our psyche.

When you’re a family with a house and kids, you have a lot of lists and thus need a lot of mental RAM. Things break down in the house all the time. You have to drag the kids to a myriad of events and appointments. You need to attend to recurring chores. If you’re not on top of these things, they fall through the cracks.

There’s two common approaches to deal with this.

The first is to always think about everything that needs to be done and bear the full psychic weight and stress of always being on point.

The second is what I call the squeaky wheel approach. You remain blissfully ignorant of all these demands until such a time comes when something is so bad that it forces your attention. Or, as is often the case, it gets so bad for someone else (after all, you're blissfully ignorant) that they make it a priority for you. A lot of things tend to get dropped with this approach that shouldn’t be dropped.

The second approach carries with it much less psychic weight, but it isn’t very respectful to a person who employs the first approach and leads to a lot of interpersonal tension.

I’ll let you guess which approach I tend to employ.

Part of the reason I employ the squeaky wheel approach is that I have a terrible memory and I'm quite good at not noticing things that need to get done. Worse, despite my ignorance, things were getting done and I just didn’t even notice. This reinforced the belief that everything was fine.

So my wife and I had a discussion about this. I can't will myself to a better memory and improve my ability to notice things that need to get done. At the same time, while I suck at this stuff at home, I tend to be much more conscientious at work.

So I proposed an idea. What if we ran our household chores like a software project? By that, I mean a well-run software project, not your typical death march past deadline over budget projects. At work, I run everything through GitHub issues. So let's try that at home!

The goal is that we should no longer maintain all these lists in our head. Instead, when we noticed something that needs to be done, we create an issue and we're free to forget it right then and there because we can trust the process. Every week, we review the list together and try complete what issues we can. It relieves a lot of mental stress to rely on the system instead of our own fallible memories.

I created a private repository for our household. The following screenshot shows an example of a recent issue. Notice that I take advantage of the wonderful [Task Lists feature of GitHub Flavored Markdown](https://github.com/blog/1375-task-lists-in-gfm-issues-pulls-comments). That feature has been a godsend.

I broke down the task of cleaning out the dead bugs from the light fixtures into a list of tasks. I decided to take on this one and assigned it to myself.

![A different kind of bug report for GitHub issues](https://f.cloud.github.com/assets/19977/2369372/b2eea64e-a7dc-11e3-853b-3608024ec3c2.png)

The work involved to close an issue sometimes leads to the need to create more issues. In this case, I learned a valuable lesson - don’t use a screw driver to put a glass light cover back on. I was fortunate that the explosion of glass this created didn’t get anyone hurt.

![Broken Lights](https://f.cloud.github.com/assets/19977/2369343/8ef2f264-a7db-11e3-98d6-96cccf55d20c.JPG)

My wife and I have tried Trello and other systems in the past, but this one has been very successful for us and she's been very happy with the results.

I also use Markdown documents in the repository to track kids meal ideas, lists of babysitters, weekend fun ideas, etc. It's become even better now that we have [rendered prose diffs](https://github.com/blog/1784-rendered-prose-diffs). Our household GitHub repository helps me track just about related to our household. What interesting ways do you use GitHub for non-software projects?

_UPDATE 2014-08-07_ I just learned about a service called [GitHub Reminders](http://www.github-reminders.com/).

> Get a email reminder by creating a GitHub issue comment with a emoji and a naural language date. 
Login and Signup with your GitHub.com Account to get started

This sounds like it could be very useful for a household issue tracker.
