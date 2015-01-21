---
layout: post
title: "GitHub Data In Your Website"
date: 2014-05-10 -0800
comments: true
categories: [blog github pages]
---

Software collaboration goes beyond just working on the code. In addition to writing a lot of code, software involves writing a lot of _words_. Prose shows up in documentation, tutorials, blog posts, and so on.

[GitHub Pages](https://pages.github.com/) is a great platform for sharing and working together on those words, and it keeps getting better. This is why I [host my blog using Jekyll on GitHub Pages](http://haacked.com/archive/2013/12/02/dr-jekyll-and-mr-haack/). So far, I really love it.

One recent improvement that [Reginald “Raganwald” Braithwaite](http://raganwald.com/) wrote about is the [rendered prose diffs for markdown](https://github.com/blog/1784-rendered-prose-diffs). This provides a nice prose-centric way of looking at changes to a written document.

Another recent improvement that [Ben "I am now a lawyer" Balter](http://ben.balter.com/) wrote about is [the ability to surface GitHub metadata in your site](https://github.com/blog/1833-github-pages-3) without having to make API calls. Since GitHub hosts your GitHub Pages site, this metadata is directly available.

This opens up some cool opportunities.

## Give credit to your blog collaborators

Every blog post in my blog has an edit link under the title. Even this one! That link takes you to an editor that lets you edit the blog post and submit your changes as a pull request. It's so easy anyone can do it!

I really appreciate when folks send me corrections. So much so that I just added a [page to give credit](http://haacked.com/contributors/) to those who have collaborated with me on my blog. This uses the new metadata that's available to every GitHub Pages site.

## GitHub Pages Demo Repository

If you want to understand how this works, I set up a minimal [GitHub Pages Demo repository here](https://github.com/Haacked/gh-pages-demo). It includes a bare minimum Jekyll site with a page that shows how to display GitHub metadata. The goal of this page is to help you get started with your own content.

You can visit the [actual rendered page here](http://haacked.github.io/gh-pages-demo/). It's pretty butt ugly right now. Why not help make it pretty and get your name on the page?!

The [Repository metadata on GitHub Pages](https://help.github.com/articles/repository-metadata-on-github-pages) page provides more information about what GitHub metadata is available to your blog.

Whether you are building a documentation site for your repository, a personal blog that shows off your GitHub repositories, or whatever, this metadata is extremely useful.
