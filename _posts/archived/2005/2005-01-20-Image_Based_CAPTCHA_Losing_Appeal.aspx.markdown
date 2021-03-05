---
title: Image Based CAPTCHA Is Fast Losing It's Appeal
redirect_from:
- "/archive/2005/01/20/1967.aspx.html"
- "/archive/2005/01/19/Image_Based_CAPTCHA_Losing_Appeal.aspx/"
tags: [spam,captcha]
---

I read an article recently that talked about how ticket scalpers have a 10% success rate against TicketMaster's CAPTCHA controls. That might not seem like a very good rate, but when you have an automated process attacking it, 10% is plenty good.

![NSF](/assets/images/CAPTCHA.jpg)

CAPTCHA for the uninitiated stands for Computer Aided Program to Tell Humans and Computers Apart. It's a method or program used to distinguish between a computer and a human.. The most popular type out there is the letter or word warping kind you often see when signing up for a web based email account.

It turns out that character recognition programs are getting better by the second. As cool as these type of controls are, I think a simple text
based semantic approach might prove stronger. For example, asking a simple question such as "RGB Stands for Red Green and what color?". If
you can't answer that question, I probably don't mind the fact that you're not commenting on my blog. ;)

The one problem with this question approach is that you can't generate these questions automatically. You'd have to create a decently sized
database of questions. However, the benefit is that language recognition is still very difficult for a computer. Especially when dealing with
mispellings.

> What is the nomber after foure?\
>  Waht is the nmuber aeftr fuor?"

You can probably answer that easily. A computer is going to have a much more difficult time.

 

In any case, rel="nofollow" and CAPTCHA aren't going to be the final solution. At some point, our blog engines will have to learn to tell the
difference like a human would. One approach is to enlist the concept of trust. If you've been subscribed to my blog a while or I'm subscribed to
yours, I'll let your comments in no problem. Otherwise your comment will have to pass a series of heuristics to get in the door.

Humans, feel free to comment...

UPDATE: It's worth noting that Bayesian Spam Filtering is not a silver bullet. Spammers have gotten smart and are now employing a tactic called
Bayesian Filter Poisoning. By including random legitimate words along with their message, they either get their message through, or cause you
to teach your filter to regard legitimate words as suspect.

I've seen a particularly tricky approach via email where they used a font in the same color as the background. Check out the following quote.
Highlight it with your mouse and see what it says.

> This looks does like Spam to the human naked eye.
> BuyecheapodrugssandtimprovesyourasexOlife. But it doesn't to the
> computer
