---
title: "Comments for Jekyll Blogs"
description: "A comment system that stores comments in your Jekyll blog's repository."
date: 2018-06-24 -0800
tags: [jekyll,blogging,serverless]
excerpt_image: https://user-images.githubusercontent.com/19977/41829489-e2745840-77ef-11e8-9ec5-f1d7385bb190.jpg
---

If you are a long time reader of my blog, you might notice something different starting today. No, the content hasn't gotten any better. What's new is the comment system.

![The Graffiti Tunnel 38 by Greger Ravik - CC BY 2.0](https://user-images.githubusercontent.com/19977/41829489-e2745840-77ef-11e8-9ec5-f1d7385bb190.jpg)

A long time ago, I [migrated comments on my blog to Disqus](https://haacked.com/archive/2012/12/25/migrating-comments-to-disqus.aspx/) using this technique to [preserve the existing comments](https://haacked.com/archive/2013/12/09/preserving-disqus-comments-with-jekyll/). Overall, I've been pretty happy with Disqus.

However, they've made some recent changes that lead me to consider other options. For one thing, they started adding ads to the free version of Disqus. That doesn't bother me too much, they are a business after all and they need to make money. I'm just not a fan of the type of clickbait ads warning you about what information the internet has about you, better click here and search your name to find out! No thank you.

It's possible to pay for an ad-free version of Disqus, but if I'm going to pay, I might as well consider my options. Not only that, but I've long had a nagging worry about storing comments outside of my blog. Disqus has a nice export feature, but will they always?

## A Jekyll-based solution

That's when [Damien Guard](https://damieng.com/) mentioned to me that he's working on a Jekyll-based comment system.

But wait, you say. Jekyll is a static-site generator. How can you build a comment system? So astute. What Damien realized is we can use [data files](https://jekyllrb.com/docs/datafiles/) in Jekyll to store comments and some liquid templates to render them. That all can be static.

The dynamic part is the bit of code that'll receive a comment form submission and create the appropriate data file in your Jekyll repository. Fortunately, that's pretty easy using something like an Azure Function or AWS Lambda combined with the GitHub API.

This is what Damien built. He [wrote an Azure function](https://damieng.com/blog/2018/05/28/wordpress-to-jekyll-comments) that calls the GitHub API using [Octokit.net](https://github.com/octokit/octokit.net) (`Install-Package octokit` when using NuGet) to create a Pull Request that contains a data file with the content info rendered as Yaml.

## Making it Haacked.com

I wrote [an importer](https://github.com/haacked/disqus-importer) that takes the Disqus export file and creates all the Jekyll data files. In my case, that ended up creating 25,381 files since there are that many comments on my blog. Wow!

You can see the work I did on haacked.com to implement Damien's comment system for my blog in [this Pull Request](https://github.com/Haacked/haacked.com/pull/316). It's hard to look at the changes because there are so many file changes, but this PR contains four commits:

1. The [changes to my Jekyll templates](https://github.com/Haacked/haacked.com/pull/316/commits/881fa955e08dd988fd34c60537084016359a7e58).
2. [Import all of the comments](https://github.com/Haacked/haacked.com/pull/316/commits/ee3fe87cb43386b02ed2ed9cf4a6d1b63e0ef9d4).
3. This [blog post announcing the change](https://github.com/Haacked/haacked.com/pull/316/commits/4b4085a1ba79e3f6ea2ca29f4122cafae353ec82).
4. A commit where I update the post to link to the third commit. Obviously I can't link to this fourth commit without creating a fifth commit unless I can break SHA1 and guess the commit SHA of the blog post before I commit.

The first commit is the most interesting for anyone looking to implement this system for their own blog.

The other part you'll need is to set up an Azure Function. You can pretty much [fork my repository](https://github.com/Haacked/jekyll-blog-comments-azure) and make that the source for your Azure Function. In your Jekyll site's `_config.yml` make sure the `comments.receiver` setting points to your function not mine.

## Comment Spam

One thing we lose with this approach is a robust comment spam filter. There are two things that mitigate this - first, when you click to send a comment, the button asks you to click again to confirm sending the comment. This is a poor person's implementation of spam filtering, but seems to get the job done.

The other part of it is by the very design, all comments are moderated because I have to merge the Pull Request created any time someone submits a comment. However, once I have robust comment spam filters implemented in the Azure function, I could decide to auto-merge those pull requests or even bypass the creation of a pull request. That would simply require that I change the Azure function.

## Feedback

As you can see by all the effort I've put in over the years to preserve the comments you've made on my blog, I value your input. Well, most of it. So next time you leave a comment, consider how important they are to me. Let me know what you think about this new comment system in the comments. And in the off chance you can't leave a comment because of a bug, [open an issue on GitHub](https://github.com/haacked/haacked.com/).
