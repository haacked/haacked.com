---
title: Better Testers
tags: [testing]
redirect_from: "/archive/2013/04/21/better-testers.aspx/"
---

In a recent post, [Test Better](https://haacked.com/archive/2013/03/04/test-better.aspx), I suggested that developers can and ought do a better job of testing their own code. *If you haven’t read it, I recommend you read that post first. I’m totally not biased in saying this at all. GO DO IT ALREADY!*

There was some interesting pushback in the comments. Some took it to mean that we should get rid of all the testers. Whoa whoa whoa there! Slow down folks.

I can see how some might come to that conclusion. I did mention that my colleague Drew wants to destroy the role of QA. But it’s not because we want to just watch it burn.

Rather, we’re interested in something better rising from the ashes. It’s not that there’s no need for testers in a software shop. It’s that what we need is a better idea of what a tester is.

Testers Are Not Second Class Citizens
-------------------------------------

Perhaps you’ve had different experiences than me with testers. Good for you, here’s a Twinkie. For the vast majority of you, you can probably relate to the following.

At almost every position I’ve been at, developers treated testers like second class citizens in the pecking order of employees. Testers were just a notch above unskilled labor.

Not every tester mind you. There were always standouts. But I can’t tell you how many times developers would joke that testers are just wannabe developers who didn’t make the cut. The general attitude was that you could replace these folks with [Amazon’s Mechanical
Turk](https://www.mturk.com/mturk/welcome) and not know the difference.

[![mechanical turk](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BetterTesters_105E3/mechanical-turk_thumb.jpg "mechanical turk")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BetterTesters_105E3/mechanical-turk_2.jpg)*[Mechanical Turk](http://en.wikipedia.org/wiki/File:Tuerkischer_schachspieler_racknitz3.jpg "Mechanical Turk")
from Wikimedia Commons. Public Domain image.*

And in some cases, it was true. At Microsoft, for example, it’s easier to be hired as a tester than as a developer. And once your foot is in the door, you can eventually make that transition to developer if you’re half way decent.

This makes it very challenging to hire and retain good testers.

Elevate the Profession
----------------------

But it shouldn’t be this way. The problem is we need to elevate the profession of tester. Drew and I talk about these things from time to time and he told me to think of testers as folks who provide a service to developers. Developers should test their own code, but testers can provide guidance on how to better test the code, suggest usage scenarios, set up test labs, etc.

Then it hit me. We already do have testers that are well respected by developers and may serve as a model for what we mean by the concept of *better testers*.

Security Testers
----------------

By most accounts, good security testers are well respected and not seen as folks who are developer wannabes. If not respected, they are feared for what they might do to your system should you piss them off. One security expert I know mentioned developers never click on links he sends without setting up a Virtual Machine to try it in first. Either way, it works.

Perhaps by looking at some of the qualities of security testers and how they integrate into the typical development flow, we can tease out some ideas on what better testers look like.

Like regular testers, many security testers test code that’s ready to ship. Many sites hire white hat penetration testers to attempt to locate and exploit vulnerabilities in a site that’s already been deployed. These folks are experts who keep up to date on the latest in security testing. They are not folks you can just replace with a Mechanical Turk.

Of course, smart developers don’t wait till code is deployed to get a security expert involved. That’s way too late. Security testers can help in the early stages of planning. Provide guidance on what patterns to avoid, what to look out for, some good practices to follow. During the coding stages they can provide code reviews with an eye towards security or simply answer questions you may have about tricky situations.

Testing as a Service
--------------------

There are other testers that also follow a similar model. If you need to target 12 languages, you’ll definitely want to work with a
localization/internationalization tester. If you value usability you may want to work with a usability expert. The list goes on.

It’s just not possible for a developer to be an expert in all these possible areas. I’d expect developers to have a basic understanding of these areas. Perhaps be quite knowledgeable in each, but never as knowledgeable as someone who is focused on these areas all the time.

The common theme among these testers is that they are providing a service to developers. **They are sought out for their expertise**.

General feature and quality testers should be no different. Good testers spend all their time learning and thinking about better and more efficient ways to test products. They are advocates for the end users and just as concerned about shipping software as developers. They are not gate keepers. They are enablers. They enable developers to ship better code.

This idea of testers as a service is not mine. It’s something Drew told me (he seriously needs to start his blog up again) that struck me.

> By necessity, these would be folks who are great developers who have
> chosen to focus their efforts on the art and science of testing, just
> as another developer might choose to focus their efforts on the art
> and science of native clients, or reactive programming.

I love working with someone who knows way more about testing software and building in quality from the start than I do.

This is one of the motivations for me to [test my own code better](https://haacked.com/archive/2013/03/04/test-better.aspx). If I’m going to leverage the skills of a great tester, it’s a matter of pride not to embarrass myself with stupid bugs I should have caught in my own testing. I want to impress these folks with crazy hard bugs I have no idea how to test.

Ok, maybe that last bit didn’t come out the way I intended. The point is when you work with experts, you don’t want them spending all their time with softballs. You want their help with the meaty stuff.
