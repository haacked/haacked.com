---
title: Write Plugins Easily Using The Dynamic Template Plugin For Windows Live Writer
tags: [blogging]
redirect_from: "/archive/2007/08/11/write-plugins-easily-using-the-dynamic-template-plugin-for-windows.aspx/"
---

[Joe Cheng](http://jcheng.wordpress.com/ "Joe Cheng"), member of the
Windows Live Writer team, [just
unveiled](http://jcheng.wordpress.com/2007/08/10/new-plugin-dynamic-template/ "New plugin: Dynamic Template")
his first publicly available Windows Live Writer plugin...

> I’ve just released my first (publicly available) Windows Live Writer
> plugin: **Dynamic Template**. It lets you write mini-plugins from
> snippets of C# and HTML, and reuse them within Windows Live Writer.

It’s sort of a meta-plugin plugin. He has a screencast on how to use it,
but I’ll post a few screenshots of it in action here.

The plugin adds a new *Insert Template* option in the sidebar.

![Insert Template
Option](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BetterBloggingUsingTheDynamicTemplatePlu_A1F5/sshot-1_1.png)

Clicking on this brings up a dialog with a list of templates.

![Insert
Template](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BetterBloggingUsingTheDynamicTemplatePlu_A1F5/Insert%20Template_1.png)

Click *New* to bring up the template editor. I’m going to create one for
wrapping sections of html with the \<code\>\</code\> tags.

First, I’ll name the template.

![Naming the New
Template](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BetterBloggingUsingTheDynamicTemplatePlu_A1F5/New%20Template_1.png)

Then I’ll edit the template. Since this template just adds HTML around a
selection and doesn’t require that I ask for user input, I don’t need to
create a variable.

![Editing the
Template](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BetterBloggingUsingTheDynamicTemplatePlu_A1F5/Edit%20Template%20Code_1.png)

And now I can just select some text, click on *Insert Template...* and
double click *Code*. Nice!

Another useful template is one that Joe calls *Htmlize*.

![Edit Template
Htmlize](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BetterBloggingUsingTheDynamicTemplatePlu_A1F5/Edit%20Template%20Htmlize_1.png)

As you can see, you can call some functions from within a template. This
one is useful for converting something like \<sup\>blah\</sup\> into
^blah^ while in the Web Layout or Normal mode.

Watch [Joe’s
Screencast](http://www.joecheng.com/code/DynamicTemplate/screencasts/level4a.swf "Dynamic Template Screencast")
for a demo of a template that takes in user input as part of the
template. There’s also some documentation [located
here](http://www.joecheng.com/code/DynamicTemplate/ "Dynamic Template Documentation"). This
is a pretty handy plugin that’ll be great for automating a variety of
html snippets I use often.
