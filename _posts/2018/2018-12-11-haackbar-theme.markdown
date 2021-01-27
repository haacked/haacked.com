---
title: "Steal My Blog Design"
description: "Want a blog design similar to mine? My design is now encapsulated in a theme you can reference remotely."
tags: [jekyll,design,meta]
excerpt_image: https://user-images.githubusercontent.com/19977/49748094-ac6e5180-fc59-11e8-93a5-1faee3d1aa61.png
---

A name like Haack does not make me destined to win awards as an outstanding designer. I've come to grips with that. I'm not terrible, mind you. I'd say my skill level is somewhere in the ballpark of slightly above Geocities and closely approaching the aesthetics of Craigslist, on a good day.

If you too lack the knack (like Haack) for design, then you know it's painful to craft a decent look and feel for your website. It's arduous. The [quirks of CSS](http://haacked.com/archive/2018/12/03/css-column-list-adventure/) will drive you to pull out your hair in frustration at some point.

Well not to fear, my friend, I pulled out my hair so you don't have to! That hair extraction lead to the [Haackbar Jekyll theme](https://github.com/haacked/haackbar).

![The haackbar Jekyll theme](https://user-images.githubusercontent.com/19977/49748094-ac6e5180-fc59-11e8-93a5-1faee3d1aa61.png)

The name is a nod to Admiral Ackbar, famous for his "It's a trap!" exclamation. Indeed, deciding to create a Jekyll theme is a trap. One that sucked many hours of my life into a dark void. But guess what?! You are the benefactor, if you happen to use [Jekyll](https://jekyllrb.com/) for your site.

Not all the hair pulled was mine, though. The theme is a loose mash-up of the [Minima theme](https://github.com/jekyll/minima) for Jekyll and the [Greyshade theme](https://github.com/shashankmehta/greyshade) for Octopress

## Some background

A while back, I [switched to a static site generator, Jekyll, for my blog](https://haacked.com/archive/2013/12/02/dr-jekyll-and-mr-haack/). I also host my blog on [GitHub Pages](https://pages.github.com).

Hosting on GitHub provides some nice benefits. I don't need to set up any third-party build systems, which keeps things simple. I push a commit and my site builds.

I have [access to GitHub data](https://haacked.com/archive/2014/05/10/github-pages-tricks/) in my site so I can build things like a page that [recognizes contributors to my blog based on commit data](https://haacked.com/contributors/).

I'm so bought into that static generated life that I even host the comments for my [blog in Jekyll as static data files](https://haacked.com/archive/2018/06/24/comments-for-jekyll-blogs/).

## Development Cycle

And so far, I'm happy with this approach. The only problem is that I've been at this a long time. My blog has around 2,000 posts and something like 40,000 comments. With that much content, it takes a long time to generate the site locally.

This makes iterating on the look and feel for my site glacial. If only I could work on the look and feel of my site separate from the content.

That's where [Jekyll themes](https://jekyllrb.com/docs/themes/) hold some promise.

## Themes

Jekyll themes make it easy to separate the design of your site from the content. In the past, themes required that you copy the theme files into your own site. That doesn't really solve the problem I have.

Later on, Jekyll [added support for gem-based themes](https://blog.github.com/2016-08-23-github-pages-now-runs-jekyll-3-2/). That was an improvement as you could just install a gem containing a theme and reference the theme from your site without having to copy a bunch of files.

However, the development cycle for that is still a bit clunky. Make a change to the theme, package up the theme as a gem, deploy it to rubygems.org, then update the gem on the site that uses it.

Fortunately, Jekyll introduced an even simpler approach, [GitHub hosted themes](https://blog.github.com/2017-11-29-use-any-theme-with-github-pages/).

This enables any Jekyll site to reference any GitHub hosted Jekyll theme.

## Haacking on the theme

I took advantage of this feature to separate my theme into another repository. The theme repository happens to be a valid Jekyll site in its own right. To work on the theme, I run the following commands (make sure you have ruby in your `PATH`).

```bash
git clone https://github.com/haacked/haackbar
cd haackbar
script/bootstrap
script/server
```

Note that `script/bootstrap` only needs to be run once in this context.

Now I can work on the design of my site (this theme) in tight iterations separate from my blog content.

If you've seen my blog before, you know that this new theme doesn't look that different from my blog before. Most of the work is behind the scenes.

I spent a lot of time cleaning up the CSS and tightening up the markup. It's much better structured and a lot smaller than before.

## Customization

When you run the `haackbar` theme, you'll notice it has a different color scheme from my site. I did that on purpose. Feel free to steal my design, but not my color and font scheme please. Tweak those to match your own tastes.

It's easy to override the theme's styles and layouts. For example, here's my [`assets/css/main.scss`](https://github.com/Haacked/haacked.com/blob/main/assets/css/main.scss) file where I override some of the colors and CSS styles from the theme.

If you use my theme, you should do the same to make it your own. Note that there are more customization details in the [theme's README](https://github.com/Haacked/haackbar/blob/main/README.md).

## Try it out

If this appeals to you, try it out and let me know! The theme is open source (MIT license) so feel free to fork it and suggest improvements, ideally via pull requests. If you run into any problems, [file an issue](https://github.com/Haacked/haackbar/issues/new).