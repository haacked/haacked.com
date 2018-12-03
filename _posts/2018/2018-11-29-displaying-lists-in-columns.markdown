---
layout: post
title: "Responsive lists in columns with CSS"
description: "How do you render an unordered list into columns in a responsive manner?"
date: 2018-11-29 -0800 09:30 AM PDT
comments: true
categories: [css design]
---

I like to give credit where credit is due, hence my blog sports a [contributors page](/contributors). This page lists the folks who have submitted a pull request to my blog with corrections and fixes.

The first version of this page was a simple bulleted unordered list. It was fugly.

What I wanted to accomplish was something more like this.

![The end goal - list of contributors in columns](https://user-images.githubusercontent.com/19977/49240817-026e0a00-f3bb-11e8-8ce6-e0df9afa517a.png)

And if you resize the browser to be more narrow, I wanted the list to collapse to fewer columns.

![Four columns](https://user-images.githubusercontent.com/19977/49240740-d18dd500-f3ba-11e8-8fdd-36a21f82c961.png)

Turns out, this is easier said than done.

## The Setup

First, let's look at my initial conditions. I have an unordered list of images and names from the original 1972 members of Earth Wind & Fire, who have contributed a lot to music.

```html
<ul>
  <li><img src="…"/><a href="…">Maurice</a></li>
  <li><img src="…"/><a href="…">Verdine</a></li>
  <li><img src="…"/><a href="…">Don</a></li>
  <li><img src="…"/><a href="…">Philip</a></li>
  <li><img src="…"/><a href="…">Roland</a></li>
  <li><img src="…"/><a href="…">Jessica</a></li>
  <li><img src="…"/><a href="…">Larry</a></li>
  <li><img src="…"/><a href="…">Ralph</a></li>
</ul>
```

Without any styling, it'll look like more or less like this.

![Ugly list](https://user-images.githubusercontent.com/19977/49332826-27a07b00-f569-11e8-86ab-13fd99a0119b.png)

It evokes a Mosaic-era design. This is what my contributors page used to look like.

The `column-count` css attribute allows you to specify the number of columns. To keep things simple, I'll focus on Chrome and ignore the vendor specific prefixes until the end.

I'll also add the `column-rule` attribute so we can see where the columns begin and end. And lets get rid of the bullet points with `list-style-type`.

```css
ul {
  column-count: 4;
  column-rule: dotted 1px #333;
  list-style-type: none;
}
```

This results in:

![Four column layout](https://user-images.githubusercontent.com/19977/49332875-eb214f00-f569-11e8-9b39-28b146724999.png)

Ok, not too bad. To put the name on its own line, we'll set it to be a block display. We'll also center everything in each list item.

```css
ul {
  column-count: 4;
  column-rule: dotted 1px #333;
  list-style-type: none;
}

li {
    text-align: center;
}

a {
    display: block;
}
```

![Four columns, each centered](https://user-images.githubusercontent.com/19977/49332904-6f73d200-f56a-11e8-804f-77ea13a7ff3f.png)

Wow, that worked just fine. I seem to be on a roll. You would be forgiven if you think we're close to being done here. But remember...

![THIS... IS... CSS!!!](https://user-images.githubusercontent.com/19977/49347907-e6869480-f656-11e8-87e4-7ed824fe35b4.png)

Let's make sure this continues to work as we add more band members. Fortunately with Earth Wind & Fire, there are many to choose from.

![Still four columns, but why is the third longer](https://user-images.githubusercontent.com/19977/49347976-66146380-f657-11e8-9154-b2ff4d87accd.png)

Well that's unexpected. Notice how Larry's name is in the next column. It seems that the columnization factors in each image and anchor element. But if that's so, it's unclear why the first two columns have four elements and the last two have five.

Let's add one more list item.

![Shit's gone bonkers](https://user-images.githubusercontent.com/19977/49348011-9cea7980-f657-11e8-8181-2b5baf402d82.png)

WTF CSS?!

I thought maybe if I applied `break-inside: avoid;` to the list item, it would fix things, but that would make too much sense, so it doesn't work.

Instead, I found [this StackOverflow answer](https://stackoverflow.com/questions/12332528/how-to-display-list-items-as-columns/12332549#12332549) that notes `display: inline-block;` is necessary. But it turned out that wasn't enough. I also need to set the width of the list items to 100%.

```css
li {
  text-align: center;
  display: inline-block;
  width: 100%
}
```

![Looks close](https://user-images.githubusercontent.com/19977/49348233-23ec2180-f659-11e8-9156-4d25be54a4a5.png)

Much better. Let's see what happens if I add one more element.

![That looks great](https://user-images.githubusercontent.com/19977/49348288-6e6d9e00-f659-11e8-8b87-fba704bee2bd.png)

Now it looks great! So as the list grows, there's a few intermediate stages where things look off, but overall it works.

Let's polish it up. I'll also add in the vendor specific prefixes. I'm going to remove the column rule because NO PARENTS, NO RULES!

```css
ul {
  -webkit-column-count: 4;
     -moz-column-count: 4;
          column-count: 4;
       list-style-type: none;
}

li {
  text-align: center;
  display: inline-block;
  width: 100%;
  padding-bottom: 20px;
}

a {
  display: block;
  font-size: 0.9em;
  text-decoration: none;
  font-family: "Open Sans", helvetica, arial, sans-serif;
}

img {
  width: 64px;
  -webkit-border-radius: 50%;
     -moz-border-radius: 50%;
          border-radius: 50%;
}
```

![Just like my blog](https://user-images.githubusercontent.com/19977/49348559-f7390980-f65a-11e8-8335-ffc83623401f.png)

And that's pretty close to how it looks on my blog.

## Rethink the assumptions

In the end, I realized I was going about this all wrong. One benefit of the column approach is the column rule. If you can't change the market, that perfectly spaced rule is is difficult with unordered list.

But as I mentioned before, I don't care about the rules. I also don't care about how many columns the images are in. I just care that they are aligned and responsive to size changes.

So I can get rid of the column stuff on the list and set a specific width for each list item, I can get a nice columnar layout (the styles for anchor and image are unchanged).

```css
ul {
  list-style-type: none;
}

ul:after {
  content: '';
  width 100%;
  display: inline-block;
}

li {
  text-align: center;
  display: inline-block;
  padding-bottom: 20px;
  width: 200px;
}
```

Notice that there's a weird `ul:after` hack in there. That's to ensure there's always an extra line, otherwise the very last line won't be justified. I found [that trick here](https://css-tricks.com/equidistant-objects-with-css/).

![Nice columnar layout](https://user-images.githubusercontent.com/19977/49348851-bf32c600-f65c-11e8-87f5-31b918eefd7e.png)

And look how it responds when I narrow the window.

![Three column layout](https://user-images.githubusercontent.com/19977/49348914-ff924400-f65c-11e8-8d9e-a7c141468ce8.png)

Even better, this approach lists contributors in a more natural order for those of us who read left to right.

## Listing contributors

You might wonder how am I listing contributors to my blog? Am I editing a list every time I merge a pull request? Ha! I'm too lazy for that.

Fortunately, GitHub Pages provides access to GitHub data for your hosted Jekyll-based site.

I [wrote a detailed post](https://haacked.com/archive/2014/05/10/github-pages-tricks/) a while ago with [a nice demo site](https://haacked.github.io/gh-pages-demo/) that explains how to do this. So feel free to give your contributors a shout out with all the columns you need.
