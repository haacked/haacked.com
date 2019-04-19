---
title: "Semver Deep Links"
tags: [semver]
excerpt_image: https://cloud.githubusercontent.com/assets/19977/12462595/04521dda-bf74-11e5-95ca-ec7ec0b5e6e4.jpg
---

A [long time request](https://github.com/mojombo/semver.org/issues/27) of http://semver.org/ (just shy of five years!) is to be able to link to specific headings and clauses of the Semver specification. For example, want to win that argument about PATCH version increments? Link to that [section directly](http://semver.org/#spec-item-6).

Today I pushed a change to semver.org that implements this. Go [try it out](http://semver.org) by hovering over any section heading or list item in the main specification section! Sorry for the long delay. I hope to get the next feature request more promptly, like in four years.

In this post, I discuss some of the interesting non-obvious challenges in the implementation, some limitations of the implementation, and my hope for the future.

<img title="Linked by Ian Sane - CC By 2.0" src="https://cloud.githubusercontent.com/assets/19977/12462595/04521dda-bf74-11e5-95ca-ec7ec0b5e6e4.jpg" width="260" align="right" />

## Implementation

The Semver specification is hosted in a [different GitHub repository](https://github.com/mojombo/semver) than [the website](https://github.com/mojombo/semver.org).

The specification itself is a [markdown file named `semver.md`](https://github.com/mojombo/semver/blob/master/semver.md). When I publish a new release, I take that one file, rename it to `index.md`, and replace [this `index.md` file](https://github.com/semver/semver.org/blob/gh-pages/index.md) with it. Actually, I do a lot more, but that's the simplified view of it.

The semver.org site is a statically generated [Jekyll](https://jekyllrb.com/) site hosted by the [GitHub Pages system](https://pages.github.com/). I love it because it's so simple and easy to update.

So one of my requirements was to require zero changes to `semver.md` when publishing a new version to the web. I wanted to make all transformations outside of the document to make it web friendly.

However, this meant that I couldn't easily control adding HTML `id` attributes to relevant elements. If you want add links to specific elements of an HTML page, giving elements an ID gives you a nice anchor target.

Fortunately, there's a Markdown renderer supported by GitHub that generates IDs for headings. Up until now, Semver.org was using rdiscount. I [switched it](https://github.com/mojombo/semver.org/commit/88271c8bd715b2634a78ab2f08d9fa76fc729f98) to use [Kramdown](http://kramdown.gettalong.org/). Kramdown generates heading IDs by default.

But there's a problem. It doesn't generate IDs for list items. Considering the meat of the spec is in the section with list items, you would guess people would want to be able to link to a specific list item.

I explored using [AnchorJs](https://github.com/bryanbraun/anchorjs) which is a really wonderful library for adding deep anchor links to any HTML page. You give the library a CSS selector and it'll both generate IDs for the elements and add a nice hover link to link to that anchor.

Unfortunately, I couldn't figure out a nice way to control the generated IDs. I wanted a nice set of sequential IDs for the list items so you could easily guess the next item.

I thought about changing the list items to headings, but I didn't want to change the original markdown file just for the sake of its rendering as a website. I think the ordered list is the right approach.

My solution was to implement a Semver.org [specific implementation in JavaScript](https://github.com/mojombo/semver.org/commit/2af9fc3d40ac71cbb2c747d47241ccf46c8db9be) to add IDs to the relevant list items and then add a hover link to all elements in the document that has an ID.

This solves things in the way I want, but it has one downside. If a user has JavaScript disabled, deep links to the list items won't work. I can live with that for now.

My hope is that someone will add support for generated list item IDs in Kramdown. I would do it, but all I really wanted to do was add deep links to this document. Also, my Ruby skills are old Ford mustang sitting on the lawn on concrete blocks rusty.

If you have concerns or suggestions about the current implementation, please [log an issue here](https://github.com/mojombo/semver.org/issues).

## Future

In 2016, I hope to release Semver 3.0. But I don't want to do it alone. I'm going to spend some time thinking about the best way to structure the project moving forward so those with the most skin in the game are more involved. For example, I'd really like to have a representative from NPM, NuGet, Ruby Gems, etc. work closely with me on it.

I unfortunately have very little time to devote to it. On one level, that's a feature. I believe stability is a feature for a specification like this and constant change creates a rough moving target. On the other hand, the world changes and I don't want Semver to become completely irrelevant to those who depend and care about it most.

Anyways, this change is a small thing, but I hope it works well for you.
