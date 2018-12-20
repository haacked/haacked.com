---
title: Making Microformats More Visible - Announcing The XFN Highlighter Script
date: 2006-04-05 -0800
disqus_identifier: 12276
tags: []
redirect_from: "/archive/2006/04/04/MakingMicroformatsMoreVisibleAnnouncingTheXFNHighlighterScript.aspx/"
---

UPDATE: The script now uses regular expressions. This fixes the problem
where it translated *met* to *me*.

You’ve [heard
BillG](http://microformats.org/blog/2006/03/20/bill-gates-at-mix06-we-need-microformats/ "We Need Microformats")
say that we need Microformats. Do you catch yourself asking **But Why?**

Good question. Right now the Microformats movement is dealing with a bit
of a chicken-egg problem due to a lack of tool support. Without tools to
make microformat creation simple for content publishers and to make
microformats more usable and visible to content consumers, it is
difficult to see the point of the effort. The effort / reward scale is
currently tipped heavily towards the effort side.

That may soon change as Microformats start taking over the web. In
preparation for an article I am writing on the topic, I have been doing
some thinking and reading up on Microformats. I won’t spoil the article
by discussing Microformats in much detail here, but instead will
highlight one microformat and my effort to make it more visible.

### Do not reinvent the wheel!

Remember, Microformats are not about trying to reinvent the wheel. In
fact, it is a key principle of the Microformat philosophy to build on
what already exists. For example, even before microformats there was an
initiative called XFN (or XHTML Friends Network). The idea is to add
semantic information to web links in the form of the `rel` attribute to
signify relationships.

This existing format has been adopted as a microformat. When linking to
a friend’s blog or website, for example, you might add the following
`rel` attribute.

```csharp
<a href="https://haacked.com/" rel="friend met">...
```

This incidentally creates a network that is indexed by XFN crawler. But
how does the average visitor to your site even notice this? Unless they
view source, they won’t. This sort of goes against the Microformat
principle of focusing on humans first and machines second. Better tools
are needed to highlight interesting microformats to end users.

### So let’s expose our friends

Well that is where my XFN Highlighter script comes in to help in a very
small way. This is yet another [Markup Based Javascript Effect
Libraries](http://weblogs.asp.net/jgalloway/archive/2006/01/18/435857.aspx "Article Highlighting Several Neat Javascript Libraries")
in the style of my [table mouse over
script](/archive/2006/02/05/AddingMouseOverRowHighlightingToTables.aspx "Adding MouseOver Row Highlighting To Tables"),
and [Lightbox
JS](http://www.huddletogether.com/projects/lightbox/ "Script to Display Pics In a Neat Way").
As more web publishers start adding microformatted content to their
sites, I think we’ll see a proliferation of these type of scripts
targetting this content.

Note that this script is a bit rough around the edges ~~(for example, I
need to replace `indexOf` with regular expressions)~~. I slapped it
together quickly one evening and there are many improvements that could
be made. But the current version works well enough and I figure it is
time to share it so I can generate some feedback (hopefully!).

What the script does is look through your html for links using the XFN
microformat. It then places a little icon next to links that express a
relationship as well as a special tooltip that lists the relationships
info. But rather than talking about it, I should give a demo. Again, I
will have to ask you to try this out in a browser since most aggregators
will not display my javascript and CSS. Here are a list of a few people
I know. Go ahead and move your mouse over them. Go on now.

### A few friends and acquaintances

-   [Jon](http://weblogs.asp.net/jgalloway/ "Jon's Blog")
-   [Micah](http://www.micahdylan.com/ "Micah's Blog")
-   [Scott Hanselman](http://www.hanselman.com/blog/ "Scott's Blog")
-   [Jeff Atwood](http://codinghorror.com/blog/ "Jeff's Blog")

### How to Use

#### Setup

​1. Add the following Javascript declaration to the header.

```html
<script type="text/javascript" 
    src="scripts/XFNHighlighter.js"></script>
```

​2. Include the XFNHighlighter CSS file (or cut and paste these styles
into your own stylesheet).

```csharp
<link rel="stylesheet" href="css/XFNHighlighter.css" 
    type="text/css" media="screen" />
```

​3. The CSS references an image *friends.png* in the images directory.
Make sure that image exists or change the CSS to point to an appropriate
image. This image is placed next to the link.

#### Activate

​1. Add an appropriate `rel="value"` when linking to a friend or
acquaintance. Check out the list of relationships from the [XFN
quickstart page](http://gmpg.org/xfn/join "Join the XFN").

#### Download

Grab the files (neatly organized) [from
here](http://tools.veloc-it.com/tabid/58/grm2id/6/Default.aspx "XFN Highlighter code").

