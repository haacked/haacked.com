---
layout: post
title: "Integrate GitHub Information In Your Blog"
date: 2014-05-10 -0800
comments: true
categories: [blog github pages]
---

Software collaboration goes beyond just working on the code. In addition to writing a lot of code, software involves writing a lot of _words_. Prose shows up in documentation, tutorials, blog posts, and so on.

GitHub Pages has become a great platform for sharing those words and it keeps getting better. This is why I [host my blog using Jekyll on GitHub Pages](http://haacked.com/archive/2013/12/02/dr-jekyll-and-mr-haack/). So far, I really love it.

One benefit is that I don't have to worry about operations for my hosting. We have a world class ops team dedicated to keeping my blog alive.

But another benefit is that we keep adding very cool features to the platform that makes writing about code more collaborative.

For example, a while [Reginald “Raganwald” Braithwaite](http://raganwald.com/) wrote about the [rendered prose diffs for markdown](https://github.com/blog/1784-rendered-prose-diffs) he worked on for GitHub.

Just recently, [Ben "I am now a lawyer" Balter](http://ben.balter.com/) [blogged about some cool integrations with your GitHub data](https://github.com/blog/1833-github-pages-3). Your GitHub Pages site can now surface metadata directly from your GitHub repository and profile without having to make API calls.

## Give credit to your blog collaborators

Every blog post in my blog has an edit link under the title. Even this one! That link takes you to an editor that lets you edit the blog post and submit your changes as a pull request. It's so easy anyone can do it!

I really appreciate when folks send me corrections. So much so I just added a [page to give credit](http://haacked.com/contributors/) to those who have collaborated with me on my blog. This uses the new metadata that's available to every GitHub Pages site.

## GitHub Pages Demo Repository

If you want to understand how this works, I set up a minimal [GitHub Pages Demo repository here](https://github.com/Haacked/gh-pages-demo). It includes a bare minimum Jekyll site with a page that shows how to display GitHub metadata.

You can visit the [actual rendered page here](http://haacked.github.io/gh-pages-demo/). It could use some CSS help. Why not contribute and get your name on the page?! That should help you get started with your own content.

The [Repository metadata on GitHub Pages](https://help.github.com/articles/repository-metadata-on-github-pages) page provides more information about what GitHub metadata is available to your blog.

Whether you are building a documentation site for your repository, a personal blog that shows off your GitHub repositories, or whatever, this metadata is extremely useful.
