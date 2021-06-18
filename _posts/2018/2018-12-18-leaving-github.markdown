---
title: "Phil Haack is no longer a GitHubber"
description: "Some reflections on my time working at GitHub."
tags: [personal,work]
excerpt_image: https://user-images.githubusercontent.com/19977/50171388-29777780-02a7-11e9-8605-bcec3469c7f0.jpg
---

It used to be a tradition at GitHub to announce new hires with a blog post with the pattern, "So and so is a GitHubber." Each post would be accompanied by an image.

On December 7, 2011, [I got my very own](https://web.archive.org/web/20140421212110/https://github.com/blog/1002-phil-haack-is-a-githubber) with a photo of me with giant robot arms. The arms are a bald attempt to cozy up to the one who really ran the show at GitHub, [Hubot](https://hubot.github.com/).

Over time, the images became animated gifs. And [these](https://blog.github.com/2013-11-08-rob-rix-is-a-githubber/) became [more](https://blog.github.com/2013-12-21-amy-palamountain-is-a-githubber/) and more [elaborate](https://blog.github.com/2013-09-06-jd-maturen-is-a-githubber/). As you can imagine, this practice did not scale as GitHub grew. You'd need a full-time film crew to keep this up as the company is now over 950 or so people.

Since my time at GitHub started with such a post, it seems fitting that I use the same format for a post that announces I am no longer at GitHub. However, I don't have a clever animated gif to go along with it, so here's an image of Sad Keanu eating a sandwich.

![Sad Keanu](https://user-images.githubusercontent.com/19977/50171388-29777780-02a7-11e9-8605-bcec3469c7f0.jpg)

If you follow me on Twitter, [this is old news](https://twitter.com/haacked/status/1053296117176184834). My last day was October 12, 2018. I've just been busy (or lazy) and haven't gotten around to writing about it till now.

## The Early Days

I hope you'll indulge me as I reflect back on my time at GitHub. It's relevant to what I'll be doing next.

I joined GitHub as employee number 50 (I believe I was the 54th employee if you count the four co-founders).

Part of what attracted me to GitHub was its unorthodox approach to running a business. They [optimized for happiness](http://tom.preston-werner.com/2010/10/18/optimize-for-happiness.html), felt that [set work hours are bullshit](https://zachholman.com/posts/how-github-works-hours/), [felt meetings are toxic](https://zachholman.com/posts/how-github-works-asynchronous/), was distributed all over the world, and [had no managers](https://www.fastcompany.com/3020181/inside-githubs-super-lean-management-strategy-and-how-it-drives-innovation).

These ideas are by no means a playbook for other companies. As the company grew, it had to mature, evolve, and even discard some of these approaches. But for the time, it was an exciting and new approach.

## My Role

My offer letter was a beautifully designed personalized website. They offered me the title of "Windows Badass." That should give you some idea how much GitHub cared about titles back then.

My initial project was to be a developer on GitHub for Windows, a GUI client for GitHub written at the time in C# and WPF.

![GitHub for Windows](https://user-images.githubusercontent.com/19977/50172223-66dd0480-02a9-11e9-9e7f-51675e32798b.PNG)

That product was part of a larger role for me. The big reason GitHub hired me was to [help make GitHub appeal to developers who code for Windows and the .NET platform](https://haacked.com/archive/2011/12/07/hello-github.aspx/). In retrospect, I think the Microsoft acqusition of GitHub suggests we were successful.

## Choose a License

Around this time, GitHub received a lot of criticism for how few projects have a license that made clear how the code could be used. The prevailing sentiment was that GitHub should add a license dropdown when creating a new repository to spur people to choose a license.

I pushed for this internally and remember the thoughtful feedback a colleague gave me. Those who understand open source licenses already know how to choose one and add it to their repository.

But for those who don't, a drop down list of obscure license names doesn't really help them. In fact, it could be dangerous as we're encouraging them to make a choice they don't understand.

I appreciate this level of deep thinking about the user experience.

This lead me to create (along with a fantastic designer, [Jason Long](https://github.com/jasonlong)) [https://choosealicense.com](https://choosealicense.com) which I [wrote about back then](https://haacked.com/archive/2013/07/17/license-your-code.aspx/). This is a simple, opinionated, guide to how to choose a license. Once this was in place, we felt more comfortable adding a license drop down.

And based on this [third-party blog post](http://250bpm.com/blog:82), it had an effect! You can see a sharp uptick around the time the site launched.

After the uptick, the percentage continues to go down. It's hard to draw conclusions from that because the rate of growth of GitHub is so immense and most repos are not open source projects in the first place. They may be code samples, people futzing around, etc.

## Management

As GitHub grew, it reached a point where the company decided it needed managers to continue to scale.

Not long after, I became one of those manager types. I started off leading the Desktop team. Later on, I would become a Director and lead a group called Client Apps which consisted of four teams:

1. Atom
2. Electron
3. Desktop
4. Editor Tools.

My org was heavily distributed. We had people in nearly every timezone. We embraced asynchronous workflows. We may have even started to miss human contact.

What I most enjoyed about my org was that I received a lot of feedback that people felt a sense of belonging and were safe to do their best work.

This wasn't an accident. I was profoundly influenced by Project Aristotle, [Google's research into what makes teams effective](https://www.nytimes.com/2016/02/28/magazine/what-google-learned-from-its-quest-to-build-the-perfect-team.html).

It became my mission to create a culture that promoted psychological safety not only because I believed it was the right thing to do, but also because I believed it made for teams that performed at a higher level.

I couldn't do this alone of course. I needed a diverse team of managers who also believed in this mission. At the time I left, my team consisted of three women (two of them women of color) and one man. They were amazing to work with.

I don't mention this because I expect a cookie, this should be the status quo. What I expect is for more managers to stop making excuses when it comes to building diverse and inclusive teams. It's not only a just thing to do, but it [makes good business sense](https://www.mckinsey.com/business-functions/organization/our-insights/why-diversity-matters).

## The Excuses We Make

Studies show that most interview techniques are [no better than flipping a coin](https://www.wired.com/2015/04/hire-like-google/). So if a coin comes up heads enough times, it's prudent to test if the coin is weighted. Likewise, if hiring practices lead to a homogenous group, it's prudent to suss out the hidden biases that may contribute.

Many leaders seem more intent on making excuses than putting in the work to learn and improve in these areas. They blame the pipeline. They talk about not lowering the bar (ironic given how low a bar hiring practices are today). And _their_ leaders tolerate the excuses. Which is also ironic because we'd never tolerate making excuses when it comes to availability. [Who owns your availability?](https://www.whoownsmyavailability.com/) The same applies to your team's make up.

It does take extra work to overcome systemic biases and build a diverse and inclusive team, but it's worth it. The folks I got to work with were my greatest pleasure at GitHub.

If you or your company need help in this area, [enlist the services of this company](https://vayaconsulting.com/).

## What's Next?

My six years and ten months at GitHub were an intense learning experience. I had the pleasure to work with some amazing people. It was wonderful.

And I have to admit, I was really excited about Microsoft acquiring GitHub. It's the only company I could think of that would make a good steward. I think GitHub's in good hands with Nat as CEO.

But I'm also excited to [take a break from full-time work](https://haacked.com/archive/2004/10/16/work-life-balance.aspx/). My family had a [rough time in 2017](https://haacked.com/archive/2017/12/27/darkest-timeline/) from a mental health perspective, and while we're doing much better, staying that way takes a lot of work and vigilance. For example, we're homeschooling my son which is time consuming, but rewarding.

But I do have a little something something cooking that I'll write about soon.
