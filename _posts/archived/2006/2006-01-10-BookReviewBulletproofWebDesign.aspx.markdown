---
title: 'Book Review: Bulletproof Web Design'
tags: [web,design]
redirect_from: "/archive/2006/01/09/BookReviewBulletproofWebDesign.aspx/"
---

[![](/assets/images/Amazon/0321346939.01._AA_SCMZZZZZZZ_.jpg)](http://www.amazon.com/exec/obidos/redirect?link_code=as2&path=ASIN/0321346939&tag=youvebeenhaac-20&camp=1789&creative=9325)
As I mentioned in my last post, my redesign was inspired by some of the
lessons in this book, “[Bulletproof Web
Design](http://www.amazon.com/exec/obidos/redirect?link_code=as2&path=ASIN/0321346939&tag=youvebeenhaac-20&camp=1789&creative=9325)
by [Dan Cederholm](http://simplebits.com/ "Dan Cederholm's Website")”.

The main focus of this book is how to use CSS and semantic (X)HTML
markup to create flexible websites. By flexible, the author is referring
to a web site’s ability to deal with the different ways a user may
choose to view a site. For the most part, he covers how to make your
site more accessible.

For example, many sites do not deal well with the change caused by a
user resizing the text. Some sites do not deal well with this, totally
breaking the design. If you specify font sizes in pixels for example, IE
won’t allow text resizing at all, which gives the designer control, but
at the cost of accessibility for those with high resolution monitors or
poor eyesight.

Cederholm instructs the reader on several ways to make sites deal with
text resizing in a more flexible manner while retaining control. For the
most part though, the designer has to give up pixel perfect control in
exchange for a better user experience.

The book also delves into accessibility tips such as making sure the
sight is readable when images are turned off and when CSS is turned off
for those with slow connections or using text to speech readers
respectively.

Each chapter presents a sample of a website design that is not flexible.
Most of the samples come from real-world sites, though some were made
up. He then walks through the steps to recreate the design element using
clean semantic xhtml and CSS. One key benefit of this approach, apart
from the increased flexibility, is that the amount of markup is greatly
reduced in most cases as 1 pixel images and empty table cells are no
longer needed.

Lest one think Cederholm is an anti-table zealot, he points out that
there are situations where using a table is correct and semantic: when
displaying tabular data of course. He then demonstrates how to use
tables and CSS properly to get the proper layout without resorting to
nested tables and empty table cells. The key is that the table should
model the data, not the layout and he succeeds.

In the end, Bulletproof is a quick and worthwhile read with clear
diagrams and plenty of css examples. There were some examples I wish he
had taken further. For example, he mentions several uses for the
dictionary list element (`<dl>)` to semantically mark up code, but only
presents one example of styling a dictionary list. Understandable since
this was not meant to be a complete compendium of CSS examples. Even so,
I found plenty of good advice which I ended up applying to this site.
The site responds well to enlargening the text (to a limit).

If you are a fan of “[CSS Zen
Garden](http://www.amazon.com/exec/obidos/redirect?link_code=as2&path=ASIN/0321303474&tag=youvebeenhaac-20&camp=1789&creative=9325 "Css Zen Garden")”,
this book would serve as a nice complement. “CSS Zen Garden” inspires
designers with what is possible to do with CSS. “Bulletproof Web Design”
provides some of the tools to get there.

