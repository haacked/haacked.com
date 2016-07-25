---
layout: post
title: Adding Mouse Over Row Highlighting To Tables
date: 2006-02-05 -0800
comments: true
disqus_identifier: 11683
categories: []
redirect_from: "/archive/2006/02/04/AddingMouseOverRowHighlightingToTables.aspx/"
---

Intro
-----

I have a bit of “me” time today to write some code and I thought I’d
treat myself to something light and fun by building something purely for
its flashiness and aesthetic. Something that does’t have any useful
purpose other than it looks good.

I am a big fan of adding effects to html pages by simply adding a
reference to a separate javascript file. This keeps the javascript code
separated from the HTML, which is a big help in avoiding “spaghetti
script”.

This is a technique that Jon Galloway writes about in his post on
[Markup Based Javascript Effect
Libraries](http://weblogs.asp.net/jgalloway/archive/2006/01/18/435857.aspx "Article Highlighting Several Neat Javascript Libraries")
which highlights several approaches for adding interesting behaviors to
a page without using Flash or DHTML behaviors (which only work in IE
anyways). By referencing a javascript file and adding certain semantic
markup to html elements, an author can add very interesting effects to a
page.

The Row Mouse Over Effect
-------------------------

The effect I will introduce is simple. Adding a javascript file and a
couple CSS classes will allow you to add row highlighting to any table.
It provides a mean to change the look and style of a row when you mouse
over it, and then change it back when you mouse out.

All you need to do is to reference the
[tableEffects.js](http://haacked.com/Skins/Haacked/Scripts/tableEffects.js "The table effects js file")
javascript file and add the “highlightTable” css class to a table. At
that point, each row of the table will have its CSS class changed to
“highlight” or “highlightAlt” when moused over. Its CSS class will be
reset when the mouse leaves.

In order to actually see a change when you mouse over, you’ll have to
style the rows for highlighting like so:

```css
table.highlightTable tr.highlight td
{
    background: #fefeee;
}
table.highlightTable tr.highlightAlt td
{
    background: #fafae9;
}
```

The script assumes you want to use a different color for alternating
rows. If not, you can simply style both `highlight` and `highlightAlt`
the same.

### See It In Action

<table class="highlightTable" border="0" cellpadding="3" cellspacing="0">
	<tbody>
	<tr>
		<th>col 1</th>
		<th>col 2</th>
		<th>col 3</th>
		<th>col 4</th>
	</tr>
	<tr class="alt">
	<td>Apple</td>
	<td>Orange</td>
	<td>Banana</td>
	<td>Kiwi</td>
	</tr>
	<tr>
	<td>Pinto</td>
	<td>Porsche</td>
	<td>Peugot</td>
	<td>Acura</td>
	</tr>
	<tr class="alt">
	<td>LAX</td>
	<td>SFO</td>
	<td>ANC</td>
	<td>NYC</td>
	</tr>
	<tr>
	<td>No</td>
	<td>Body</td>
	<td>Look</td>
	<td>Here</td>
	</tr>
	<tr class="alt">
	<td>Red</td>
	<td>Blue</td>
	<td>Green</td>
	<td>Alpha</td>
	</tr>
	</tbody>
</table>


### Get the file

Download the file [from
here](http://haacked.com/Skins/Haacked/Scripts/tableEffects.js "The table effects js file")
(Right click and save as).

If you have any improvements (as I am sure there will be some), please
let me know and I will keep my version updated. I named the script
“tableEffects.js” because I hope to add more interesting effects.

And remember, though I tend to preach table-less design, there are
semantic uses for tables. When using this script, it helps to make sure
that your tables are semantically marked up. For example, if you don’t
want your header row to highlight, use `<th>` tags instead of `<tr>`.
The script ignores the table header tags.

