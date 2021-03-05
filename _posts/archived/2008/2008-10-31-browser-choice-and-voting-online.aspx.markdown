---
title: Browser Choice and Voting Online
tags: [personal,code]
redirect_from: "/archive/2008/10/30/browser-choice-and-voting-online.aspx/"
---

In [my last
post](https://haacked.com/archive/2008/10/28/hot-new-presentation-tip.aspx "Hot New Presentation Tip"),
I joked that the reason that someone gave me all 1s in my talk was a
misunderstanding of the evaluation form. In truth, I just assumed that
someone out there really didn’t like what I was showing and that’s
totally fine. It was something I found funny and I didn’t really care
too much.

But I received a message from someone that they tried to evaluate the
session from the conference hall, but the evaluation form was really
screwy on their iPhone. For example, here’s how it’s supposed to look in
IE.

[![survey
ie](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/9b59a59d6655_7693/survey-ie_thumb.png "survey ie")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/9b59a59d6655_7693/survey-ie_2.png)

I checked it out with Google Chrome which uses
[WebKit](http://webkit.org/ "Webkit"), the same rendering engine that
Safari, and thus the iPhone, uses.

Here it is (click to see full size).

[![survey
webkit](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/9b59a59d6655_7693/survey-webkit_thumb.png "survey webkit")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/9b59a59d6655_7693/survey-webkit_2.png)

Notice anything different? :)

The good news here is that nothing really at stake here for me, as
speaking is a perk of my job, not a requirement. It doesn’t affect my
reviews. I’d bet this form has been in use for years and was built long
before the iPhone.

However, if we ever start deciding elections online, this highlights one
of the subtle design issues the designers of such a ballot would need to
address.

It’s not just an issue of testing for the current crop of browsers, it’s
also about anticipating what future browsers might do.

Such a form would really need to have simple semantic standards based
markup and be rendered in such a way that if CSS were disabled,
submitting the form would still reflect the voter’s intention.

For example, it may be hard to anticipate every possible browser
rendering of a form. In this case, one fix would be to change the label
for the radio buttons to reflect the intention. Thus rather than the
number “1” the radio button label would be “1 – Very Dissatisfied”.
Sure, it repeats the column information, but no matter where the radio
buttons are displayed, it reflects the voter’s intention.

In any case, I think the funny part of this whole thing is when I
mentioned this one evaluation score, several people I know all laid
claim to being the one who hated my talk. They all want to take credit
for hating on my talk, without going through all the trouble of actually
submitting bad scores. ;)

If you were at the conference and saw my talk, be **[sure to evaluate
it](https://sessions.microsoftpdc.com/wizard/eval_session/wp1.aspx?objectid=1a28169e-1e5d-4c50-9ac8-007e4a2d98c9 "ASP.NET MVC Eval Form")**.
And do be kind. :)

UPDATE: Be sure to read [John Lam’s account of the
PDC](http://www.iunknown.com/2008/10/pdc-2008-wrap-up.html "PDC 2008 Wrap-up")
as well. He has some great suggestions for conference organizers to
improve the evaluation process.

