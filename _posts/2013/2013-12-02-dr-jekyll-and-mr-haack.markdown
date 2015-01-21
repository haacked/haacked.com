---
layout: post
title: "Dr. Jekyll and Mr. Haack"
date: 2013-12-02 08:18:39 -0800
comments: true
categories: [blogging,jekyll]
---

The older I get, the less I want to worry about hosting my own website. Perhaps this is the real reason for the rise of cloud hosting. All of us old fogeys became too lazy to manage our own infrastructure.

For example, a while back my blog went down and as I frantically tried to fix it, I received this helpful piece of advice from [Zach Holman](http://zachholman.com/).

> @haacked the ops team gets paged when http://zachholman.com  is down. You still have a lot to learn, buddy.

Indeed. Always be learning.

What Zach refers to is the fact that his blog is hosted as a [GitHub Pages](http://pages.github.com/) repository. So when his blog goes down (ostensibly because GitHub Pages is down), the amazing superheroes of the GitHub operations team jumps into action to save the day. These folks are amazing. Why not benefit from their expertise?

So I did.

One of the beautiful things about GitHub Pages is that it supports [Jekyll](http://jekyllrb.com/), a simple blog aware static site generator.

If you can see this blog post, then the transition of my blog over to Jekyll is complete and (mostly) successful. The GitHub repository for this blog is located at [https://github.com/haacked/haacked.com](https://github.com/haacked/haacked.com). Let me know if you find any issues. Or better yet, click that edit button and send me a pull request!

![Screen grab from the 1931 movie Dr. Jekyll and Mr. Hide public domain](https://f.cloud.github.com/assets/19977/1656110/a3f8b280-5b6d-11e3-818d-c06ab05bd613.jpg)

There are two main approaches you can take with Jekyll. In one approach, you can use something like [Octopress](http://octopress.org/) to generate your site locally and then deploy the locally generated output to a `gh-pages` branch. Octopress has a nice set of themes (my new design is based off of the [Greyshade theme](https://github.com/shashankmehta/greyshade)) and plugins you can take advantage of with this approach. The downside of that approach is you can't publish a blog post solely through GitHub.com the website.

Another approach is to use raw Jekyll with GitHub pages and let GitHub Pages generate your site when your content changes. The downside of this approach is that for security reasons, you have a very limited set of Jekyll plugins at your disposal. Even so, there's quite a lot you can do. My blog is using this approach.

This allows me to create and edit blog posts directly from the web interface. For example, every blog post has an "edit" link. If you click on that, it'll fork my blog and take you to an edit page for that blog post. So if you're a kind soul, you could fix a typo and send me a pull request and I can update my blog simply by clicking the Merge button.

## Local Jekyll

Even with this latter approach, I found it useful to have Jekyll running locally on my Windows machine in order to test things out. I just followed the helpful instructions on this [GitHub Help page](https://help.github.com/articles/using-jekyll-with-pages). If you are on Windows, you will inevitably run into some weird UTF Encoding issue. The solution is [fortunately very easy](http://joseoncode.com/2011/11/27/solving-utf-problem-with-jekyll-on-windows/).

## Migrating from Subtext

Previously, I hosted my blog using Subtext, a database driven ASP.NET application. In migrating to Jekyll, I decided to go all out and convert all of my existing blog posts into Markdown. I wrote a hackish ugly console application, [Subtext Jekyll Exporter](https://github.com/Haacked/subtext-jekyll-exporter), to grab all the blog post records from my existing blog database.

The app then shells out to [Pandoc](http://johnmacfarlane.net/pandoc/) to convert the HTML for each post into Markdown. This isn't super fast, but it's a one time only operation.

If you have a blog stored in a database, you can probably modify the Subtext Jekyll Exporter to create the markdown post files for your Jekyll blog. I apologize for the ugliness of the code, but I have no plans to maintain it as it's done its job for me.

## The Future of Subtext

It's with heavy heart that I admit publicly what everyone has known for a while. Subtext is done. None of the main contributors, myself included, have made a commit in a long while.

I don't say dead because the [source code](https://github.com/haacked/subtext) is available on GitHub under a permissive open source license. So anyone can take the code and continue to work on it if necessary. But the truth is, there are much better blog engines out there.

I started Subtext with high hopes [eight years ago](http://haacked.com/archive/2005/05/04/announcing-subtext.aspx). Despite a valiant effort to tame the code, what I learned in that time was that I should have started from scratch.

I was heavily influenced by this blog post from Joel Spolksy, [Things You Should Never Do](http://www.joelonsoftware.com/articles/fog0000000069.html).

> Well, yes. They did. They did it by making the single worst strategic mistake that any software company can make:
>
> They decided to rewrite the code from scratch.

Perhaps it is a strategic mistake for a software company, but I'm not so sure the same rules apply to an open source project done in your spare time.

So much time and effort was sacrificed at the altar of backwards compatibility as we moved mountains to make the migration from previous versions to next continue to work while trying to refactor as much as possible. All that time dealing with the past was time not spent on innovative new features. I was proud of the engineering we did to make migrations work as well as they did, but I'm sad I never got to implement some of the big ideas I had.

Despite the crap ton of hours I put into it, so much so that it strained my relationship at times, I don't regret the experience at all. Working on Subtext opened so many doors for me and sparked many lifelong friendships.

So long Subtext. I'll miss that little submarine.
