---
title: Using TextBoxes as Labels
tags: [aspnet,design]
redirect_from: "/archive/2005/12/10/using-textboxes-as-labels.aspx/"
---

In this post, I plan to give out some rough code I hope you find useful.
But first, an introduction. Occasionally, in the search to compress more
into less space, web designers will create a form in which the text
inputs double as labels. For example, this is a login control I wrote
for a recent project.

![Login Control](https://haacked.com/assets/images/LoginControl.png)

**Usability Issues:** 
 In my mind, this idea making the standard text box perform the double
duty of being a label has several usability problems. First of all, in a
large enough form, if the user starts filling in data, and then realizes
after the fact that he or she made a mistake, the user no longer has any
context of what each field represents except the mistaken information
they typed. Believe me, it happens.

Secondly, notice that the password textbox displays the word “Password”.
Well what happens when the user starts typing his password? With a
normal text input, it is there for the world to see. If you use a
standard password text input there, then how would you display the label
text? It would look like `********`.

**A compromise** 
However, it is important to give the client what they want, and what do
I know about web design? So I created a control named `TextBoxLabel`
that helps resolve some of the usability issues. I need to give some
credit to [Jon Galloway](http://weblogs.asp.net/jgalloway/) who showed
me one implementation of this idea that spurred me to try and iterate on
it and make it even better.

**Requirements**

1. When the control receives focus, I want the user to be able to begin
   typing immediately. Originally, I cleared the textbox, but I found
   that simply selecting its contents works just as well and has the
   added benefit that you can still see the label when you give the
   control focus. Also, if you clear the field when it receives focus,
   you’ll run into problems in pages that automatically give focus to
   the first text input, since the field will be cleared before the
   user has a chance to read it.
2. When a control loses focus, and its contents are blank, I want to
   make sure we re-populate the label. Some implementations use the
   javascript property `textInput.defaultValue` to do this, but I found
   one flaw in this approach. What happens if you use this control to
   edit an existing record. The default value will be the current
   value, not your label value. To resolve this, I use a custom
   attribute, “label”. Of course, if you clear the text input, perhaps
   you do want the original value and not the label value. In my case,
   that was not the desired behavior.
3. In order to help maintain context, I made sure to add the `title`
   attribute to the textboxes. It’s a tiny improvement, but by
   highlighting a textbox with the mouse, a tooltip will show the user
   the label for the textbox.
4. Finally, if the text input is a password input, I wanted to be able
   to display the label text in the control, but as soon as the control
   gets focus, I want to switch to a password mask text input. I
   accomplished this by having two text inputs and using javascript to
   switch between the two.

**Demo** 
 I built a very simple [demo you can play around
with](/Demos/TextBoxLabelDemo.aspx) to see the control in action. Pay
attention to how the password input changes to a text mask when you
start typing in there.

**The Code** 
 Unfortunately, it is difficult to present the code in a blog post
because my web controls can’t be encapsulated in a single .cs file.
That’s primarily because I make use of embedded resources for my client
javascript (see [Using Embedded Resources for Client Script Blocks in
ASP.NET](https://haacked.com/archive/2005/04/29/2879.aspx).

So instead, I created a simple web project that contains the controls
and all supporting code files. Since I was at it, I added a few other
classes I’ve written about in the past, including one control I haven’t
written about. This is a small subset of our internal libraries. Over
time I hope to provide more polish to some of our internal controls so I
can publish more of it in this project. Here’s a listing of what you
will find in this project (in addition to the `TextBoxLabel` control):

-   [An Abstract Boilerplate
    HttpHandler](https://haacked.com/archive/2005/03/17/2394.aspx)
-   The `PairedDropDownList` control ([see
    demo](/Demos/PairedDropDownDemo.aspx)).
-   `ScriptHelper` used to [embed client
    scripts](https://haacked.com/archive/2005/04/29/2879.aspx).

It’s not a lot, but it is a start. As these are controls we use
internally, they aren’t necessarily the most polished and won’t work in
all situations. We do not provide any warranty, liability, nor support
for the controls. However, if you make improvements, feel free to let me
know and I will incorporate them. You are free to use them in any
project, commercial or otherwise.

**The Link** 
 And before I forget, as I tend to do, [here is a link to the
code](/Code/Velocit.Web.Public.zip).

**//TODO** 
 The controls do work with the ASP.NET validation controls, but not
perfectly. When I say they “work”, I mean they work perfectly when
client scripting is disabled. With client scripting enabled, you get
some weird behavior. In part, I think this is due to how we hook into
the submit event. The timing of method calls is important as the
TextBoxLabel clears its value when submitting if the value is the same
as the label. This makes sure that on the server side, you do NOT have
to compare the value to the label to determine if it is valid. The
side-effect of this approach is that if client validation prevents the
form post, the textbox is now cleared. I haven’t spent any time to look
into a better approach to this.

