---
title: Lightweight Invisible CAPTCHA Validator Control
tags: [spam,aspnet,validation,captcha]
redirect_from:
- "/archive/2006/09/25/lightweight_invisible_captcha_validator_control.aspx/"
- "/archive/2006/09/26/Lightweight_Invisible_CAPTCHA_Validator_Control.aspx/"
---

UPDATE: This code is now hosted in the Subkismet project on CodePlex.

[![Source:
http://www.dpchallenge.com/image.php?IMAGE\_ID=138743](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/LightweightInvisibleCAPTCHAControl_281C/138743_thumb.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/LightweightInvisibleCAPTCHAControl_281C/1387432.jpg)
Not too long ago I wrote about [using
heuristics](https://haacked.com/archive/2006/08/29/Comment_Spam_Heuristics.aspx)
to fight comment spam.  A little later I [pointed to the NoBot
control](https://haacked.com/archive/2006/09/19/Atlas_Comment_Spam_Heuristics.aspx)
as an independent implementation of the ideas I mentioned using Atlas.

I think that control is a great start, but it does suffer from a few
minor issues that prevent me from using it immediately.

1.  It requires Atlas and Atlas is pretty heavyweight.
2.  Atlas is pre-release right now.
3.  We’re waiting on [a bug
    fix](https://haacked.com/archive/2006/09/19/Please_Vote_On_This_Atlas_Javascript_Bug.aspx)
    in Atlas to be implemented.
4.  It is not accessible as it doesn’t work if javascript is enabled.

Let me elaborate on the first point.  In order to get the NoBot control
working, a developer needs to add a reference to two separate
assemblies, Atlas and the Atlas Control Toolkit, as well as make a few
changes to Web.config.  Some developers will simply want a control they
can simply drop in their project and start using right away.

I wanted a control that meets the following requirements.

1.  Easy to use. Only one assembly to reference.
2.  Is invisible.
3.  Works when javascript is disabled.

The result is the `InvisibleCaptcha` control which is a validation
control (inherits from `BaseValidator)`so it can be used just like any
other validator, only this validator is invisible and should not have
the `ControlToValidate` property set.  The way it works is that it
renders some javascript to perform a really simple calculation and write
the answer into a hidden text field using javascript.

*What!  Javascript?  What about accessibility!?* Calm down now, I’ll get
to that.

When the user submits the form, we take the submitted value from the
hidden form field, combine it with a secret salt value, and then hash
the whole thing together.  We then compare this value with the hash of
the expected answer, which is stored in a hidden form field base64
encoded.

The whole idea is that most comment bots currently don’t have the
ability to evaluate javascript and thus will not be able to submit the
form correctly.  Users with javascript enabled browsers have nothing to
worry about.

**So what happens if javascript is disabled?**

If javascript is disabled, then we render out the question as text
alongside a visible text field, thus giving users reading your site via
non-javascript browsers (think Lynx or those text-to-speech browsers for
the blind) a chance to comment.

![Accessible version of the Invisible CAPTCHA
control](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/LightweightInvisibleCAPTCHAControl_281C/AccessibleInvisibleCaptcha4.png)

This should be sufficient to block a lot of comment spam.

***Quick Aside:** As [Atwood](http://codinghorror.com/) tells me, the
idea that CAPTCHA has to be really strong is a big fallacy.  His blog
simply asks you to type in **orange** every time and it blocks 99.9% of
his comment spam.*

*I agree with Jeff on this point when it comes to websites and blogs
with small audiences. Websites and blogs tend to implement different
CAPTCHA systems from one to another and beating each one brings
diminishing margins of returns.*

*However, for a site with a huge audience like Yahoo! or Hotmail, I
think strong CAPTCHA is absolutely necessary as it is a central place
for spammers to target.  (By the way, remind me to write a bot to post
comment spam on Jeff’s blog)*

If you do not care for accessibility, you can turn off the rendered form
so that only javascript enabled browsers can post comments by setting
the `Accessible` property to false.

I developed this control as part of the `Subtext.Web.Control.dll`
assembly which is part of the [Subtext
project](http://subtextproject.com/), thus you can grab this assembly
from our Subversion repository.

To make things easier, I am also providing a link to a zip file that
contains the assembly as well as the source code for the control. You
can choose to either reference the assembly in order to get started
right away, or choose to add the source code file and the javascript
file (make sure to mark it as an embedded resource) to your own project.

Please not that if you add this control to your own assembly, you will
need to add the following assembly level `WebResource` attribute in
order to get the [web resource
handler](http://aspnet.4guysfromrolla.com/articles/080906-1.aspx)
working.

```csharp
[assembly: WebResource("YourNameSpace.InvisibleCaptcha.js", 
    "text/javascript")]
```

You will also need to find the call to
`Page.ClientScript.GetWebResourceUrl` inside InvisibleCaptcha.cs and
change it to match the namespace specified in the `WebResource`
attribute.

If you look at the code, you’ll notice I make use of several hidden
input fields. I didn’t use `ViewState` for values the control absolutely
needs to work because Subtext disables ViewState.  Likewise, I could
have chosen to use `ControlState`, but that can also be disabled.  I
took the most defensive route.

[[**Download InvisibleCaptcha
here**](http://www.codeplex.com/subkismet)].

