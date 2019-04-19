---
title: "An adventure in CSS with column lists"
description: "How do you render an unordered list into columns in a responsive manner without losing your mind?"
tags: [css,design]
excerpt_image: https://user-images.githubusercontent.com/19977/49349901-a8db3900-f661-11e8-81e5-7b19a0f3d2d7.png
---

Sit back and relax as I regale you with a harrowing account of trying to do something straightforward with CSS. Ha! Straightforward. How silly was I to think that. As they say,

> Fool me once. Shame on you. Fool me twice, CSS.

To give credit where credit is due, my blog has a [contributors page](/contributors). This page lists the folks who have submitted a pull request to my blog with corrections such as typo and spelling fixes. Lots and lots of fixes. Because I make a lot of mistakes.

The first version of this page was a simple bulleted unordered list. It was fugly.

What I wanted to accomplish was something more like this.

![The end goal - list of contributors in columns](https://user-images.githubusercontent.com/19977/49349901-a8db3900-f661-11e8-81e5-7b19a0f3d2d7.png)

And if you resize the browser to be more narrow, I wanted the list to collapse to fewer columns.

![Four columns](https://user-images.githubusercontent.com/19977/49349977-fc4d8700-f661-11e8-8909-afae0fa3f688.png)

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

![Ugly list](https://user-images.githubusercontent.com/19977/49350009-1be4af80-f662-11e8-9562-97a6a248e499.png)

It evokes a [Mosaic-era design](https://en.wikipedia.org/wiki/Mosaic_(web_browser)). This is what my contributors page used to look like.

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

![Four column layout](https://user-images.githubusercontent.com/19977/49350034-3585f700-f662-11e8-85f1-be68837ab6e5.png)

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

![Four columns, each centered](https://user-images.githubusercontent.com/19977/49350070-5ea68780-f662-11e8-8f25-5cb249a6fa0d.png)

Wow, that worked just fine. I seem to be on a roll. You would be forgiven if you think we're close to being done here. But remember...

![THIS... IS... CSS!!!](https://user-images.githubusercontent.com/19977/49350100-8138a080-f662-11e8-9b6c-3ef7302464a3.png)

Let's make sure this continues to work as we add more band members. Fortunately with Earth Wind & Fire, there are many to choose from.

![Still four columns, but why is the third longer](https://user-images.githubusercontent.com/19977/49350137-ab8a5e00-f662-11e8-9996-f0f1876f7fd0.png)

Well that's unexpected. Notice how Larry's name is in the next column. It seems that the columnization factors in each image and anchor element. But if that's so, it's unclear why the first two columns have four elements and the last two have five.

Let's add one more list item.

![Shit's gone bonkers](https://user-images.githubusercontent.com/19977/49350161-c4930f00-f662-11e8-841f-2c20544879a6.png)

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

![Looks better](https://user-images.githubusercontent.com/19977/49350189-e7252800-f662-11e8-8a14-42a7cbd67679.png)

Much better. Let's see what happens if I add one more element.

![That loks great](https://user-images.githubusercontent.com/19977/49350220-00c66f80-f663-11e8-9c45-b40233268c40.png)

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

![Just like my blog](https://user-images.githubusercontent.com/19977/49350268-2c495a00-f663-11e8-90f8-86068b80a49b.png)

And that's pretty close to how it looks on my blog.

## Rethink the assumptions

In the end, I realized I was going about this all wrong. One benefit of the column approach is the column rule. If you can't change the market, that perfectly spaced rule is difficult with unordered list.

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

![Nice columnar layout](https://user-images.githubusercontent.com/19977/49350296-4a16bf00-f663-11e8-8718-df331084722f.png)

And look how it responds when I narrow the window.

![Three column layout](https://user-images.githubusercontent.com/19977/49350326-66b2f700-f663-11e8-9000-e63f24e262c6.png)

Even better, this approach lists contributors in a more natural order for those of us who read left to right.

## An even better approach

And of course, not long after I posted the original version of this post, two commenters mention two better approaches, [CSS Flexbox](https://css-tricks.com/snippets/css/a-guide-to-flexbox/) and [CSS Grid](https://css-tricks.com/snippets/css/complete-guide-grid/).

This is why I'm happy to put my CSS ignorance on display to the world - people with more knowledge than me provide better solutions than the ones I came up with.

I ended up using CSS grid because the second commenter, Jonathan, provided the exact CSS I needed and it worked. This meant I could simplify the CSS.

```css
ul {
  list-style-type: none;
  display: grid;
  grid-template-columns: repeat(auto-fit,minmax(132px, 1fr)); 
}

li {
  text-align: center;
  padding-bottom: 20px;
}
```

No changes to the image or anchor CSS were needed.

## Listing contributors

You might wonder how am I listing contributors to my blog? Am I editing a list every time I merge a pull request? Ha! I'm too lazy for that.

Fortunately, GitHub Pages provides access to GitHub data for your hosted Jekyll-based site.

I [wrote a detailed post](https://haacked.com/archive/2014/05/10/github-pages-tricks/) a while ago with [a nice demo site](https://haacked.github.io/gh-pages-demo/) that explains how to do this. So feel free to give your contributors a shout out with all the columns you need.
