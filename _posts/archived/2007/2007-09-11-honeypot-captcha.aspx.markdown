---
title: Honeypot Captcha
tags: [spam,captcha,blogging]
redirect_from: "/archive/2007/09/10/honeypot-captcha.aspx/"
---

I was thinking about alternative ways to block comment spam the other
day and it occurred to me that there’s potentially a simpler solution
than the [Invisible
Captcha](https://haacked.com/archive/2006/09/26/Lightweight_Invisible_CAPTCHA_Validator_Control.aspx "Lightweight Invisible CAPTCHA Validator Control")
approach I wrote about.

The Invisible Captcha control plays upon the fact that most comment spam
bots don’t evaluate javascript. However there’s another particular
behavioral trait that bots have that can be exploited due to the bots
inability to support another browser facility.

![honeypot image from
http://www.cs.vu.nl/\~herbertb/misc/shelia/](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HoneypotCaptcha_14E35/honeypot_1.gif)
You see, comment spam bots love form fields. When they encounter a form
field, they go into a berserker frenzy (+2 to strength, +2 hp per level,
etc...) trying to fill out each and every field. It’s like watching
someone toss meat to piranhas.

At the same time, spam bots tend to ignore CSS. For example, if you use
CSS to hide a form field (especially via CSS in a separate file), they
have a really hard time knowing that the field is not supposed to be
visible.

To exploit this, you can create a *honeypot* form field that *should be
left blank*and then use CSS to hide it from human users, but not bots.
When the form is submitted, you check to make sure the value of that
form field is blank. For example, I’ll use the form field named *body*
as the honeypot. Assume that the actual body is in another form field
named *the-real-body* or something like that:

```csharp
<div id="honeypotsome-div">
If you see this, leave this form field blank 
and invest in CSS support.
<input type="text" name="body" value="" />
</div>
```

Now in your code, you can just check to make sure that the honeypot
field is blank...

```csharp
if(!String.IsNullOrEmpty(Request.Form["body"]))
  IgnoreComment();
```

I think the best thing to do in this case is to act like you’ve accepted
the comment, but really just ignore it.

I did a Google search and discovered I’m not the first to come up with
this idea. It turns out that Ned Batchelder wrote about [honeypots as a
comment spam fighting
vehicle](http://nedbatchelder.com/text/stopbots.html "Stopping spambots with hashes and honeypots")
a while ago. Fortunately I found that post after I wrote the following
code.

For you ASP.NET junkies, I wrote a Validator control that encapsulates
this honeypot behavior. Just add it to your page like this...

```csharp
<sbk:HoneypotCaptcha ID="body" ErrorMessage="Doh! You are a bot!"
  runat="server"  />
```

This control renders a text box and when you call
`Page.Validate`, validation fails if the textbox is *not empty*.

This control has no display by default by setting the `style` attribute
to `display:none`. You can override this behavior by setting the
`UseInlineStyleToHide` property to false, which makes you responsible
for hiding the control in some other way (for example, by using CSS
defined elsewhere). This also provides a handy way to test the
validator.

To get your hands on this validator code and see a demo, download the
latest
[Subkismet](http://www.codeplex.com/subkismet "Subkismet - The Cure For Comment Spam")
source from CodePlex. You’ll have to get the code from source control
because this is not yet part of any release.

