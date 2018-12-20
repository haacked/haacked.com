---
title: The Getting Better Moment
date: 2015-06-15 -0800
tags: [career]
redirect_from: "/archive/2015/05/14/the-getting-better-moment/"
---

The beads of sweat gathered on my forehead were oddly juxtaposed against the cool temperature of the air conditioned room. But there they were, caused by the heat of the CTO's anger. I made a sloppy mistake and now sat in his office wondering if I was about to lose my job. My first full-time job. I recently found some archival footage of this moment.

![I wore headphones everywhere back then](https://cloud.githubusercontent.com/assets/19977/8146368/361c167e-11e9-11e5-8d24-e1aa1a7360bc.png)

So why do I write about this? Unless you've been passed out drunk in a gutter for the last week (which is much more believable than living under a rock), you've heard about this amazing opus by [Paul Ford](https://twitter.com/ftrain) entitled "[What is Code?](http://www.bloomberg.com/graphics/2015-paul-ford-what-is-code/)"

If you haven't read it yet, cancel all your appointments, grab a beer, find a nice shady spot, and soak it all in. The whole piece is great, but there was one paragraph in particular that I zeroed in on. In the intro, Paul talks about his programming start.

> I began to program nearly 20 years ago, learning via oraperl, a special version of the Perl language modified to work with the Oracle database. A month into the work, I damaged the accounts of 30,000 fantasy basketball players. They sent some angry e-mails. After that, I decided to get better.

This was his "getting better moment" and like many such moments, it was the result of a coding mistake early in his career. It caused me to reminisce about the moment I decided to get better.

When I graduated from college, websites were still in black and white and connected to the net by string and cans. They pretty much worked like this.

![The Internet circa 1997 - image from Wikipedia - Public Domain](https://cloud.githubusercontent.com/assets/19977/8146357/9c0bb4ea-11e8-11e5-9706-43dd76ae6205.png)

As a fresh graduate, I was confident that I would go on to grad school and continue my studies in Mathematics. But deep in debt, I decided to get a job long enough to pay down this debt a bit before I returned to the warm comfort of academia. After all, companies were keen to hire people to work on this "Web" thing. It wouldn't hurt to dabble.

Despite my lack of experience, a small custom software shop named Sequoia Softworks hired me. It was located in the quaint beach town of Seal Beach, California. You know it's a beach town because it's right there in the name. The company is still around under the [name Solien](http://www.solien.com) and now is located in Santa Monica, California.

My first few weeks were a nervous affair as my degree in Math was pretty much useless for the work I was about to engage in. Sure, it prepared me to think logically, but I didn't know a database from a VBScript, and my new job was to build database driven websites with this hot new technology called Active Server Pages (pre .NET, we'd now call this "Classic ASP" if we call it anything).

Fortunately, the president of the company assigned a nice contractor to mentor me. She taught me VBScript, ADODB, and how to access a SQL Server database. Perhaps the most valuable lesson I learned was this:

```vbnet
Dim conn, rs
Set conn = Server.CreateObject("ADODB.Connection")
conn.Open("Driver={SQL Server};Server=XXX;database=XXX;uid=XXX;pwd=XXX")
Set rs = conn.Execute("SELECT * FROM SomeTable")
Do Until rs.EOF
  ' ...

  rs.MoveNext ' NEVER EVER EVER EVER FORGET THIS CALL!
Loop
conn.Close
```

As the comment states, never ever ever ever forget to call `rs.MoveNext`. Ever.

A benefit of working at a tiny company, it wasn't long before I got to work on important and interesting projects. One of these projects was a [music website called myLaunch](http://solien.com/client-successes/Pages/launch-media.aspx). The website was a companion to the multi-media Launch CD-ROM magazine. Cutting edge stuff, you can [still find them on Amazon](http://www.amazon.com/Launch-(CD-ROM-Magazine)/e/B000AQ4H1M). I wish I had kept [the Radiohead issue](http://www.amazon.com/LAUNCH-No-15/dp/B0000039S4/ref=ntt_mus_ep_dpi_2), it sells for $25!

![Launch magazine](https://cloud.githubusercontent.com/assets/19977/8151027/5d56cdec-12b6-11e5-81a2-4c1b3cf2e8ce.png)

It wasn't long before the CD-ROM magazine was discontinued and the website became the main product. Launch later was bought by and incorporated into [Yahoo Music](https://www.yahoo.com/music).

One of my tasks was to make some changes to the Forgot Password flow. I dove in and cranked out the improvements. This was before Extreme Programming popularized the idea of test driven development, so I didn't write any automated tests. I was so green, it hadn't even occurred to me yet that such a thing was possible.

So I manually tested my changes. At least, I'm pretty sure I did. I probably tried it a couple times, saw the record in the database, might have seen the email or not. I don't recall. You know, rigorous testing.

And that brings me to the beginning of this post. Not long after the change was deployed the CTO (and co-founder) called me into his office. Turns out that a Vice President at our client company had a daughter who used the website to read about her favorite bands and she had forgotten her password. She went to reset her password, but never got the email with the new generated password and was completely locked out. And we had no way of knowing how many people had run into this problem and were currently locked out, never to return.

When I returned to my desk and sprinkled the code with `Response.Write` statements (the sophisticated debugging technique I had at my disposal), I discovered that sure enough, the code to email the new password never ran due to a logic bug.

I soon learned there's a pecking order to finding bugs. It's better to

1. ... have the computer find the bug (compiler, static analysis, unit tests) than to find it at runtime.
2. ... find a bug at runtime yourself (or have a co-worker find it) before a user runs into it.
3. ... have a user find a bug (and report it to you) before the daughter of your client's Vice President does.

I wasn't fired right then, but it was made clear to me that wouldn't hold true if I made another mistake like that. Gulp! _And by "Gulp!" I don't mean a JavaScript build system._

Inspired by a fear of losing my job, this was my Getting Better Moment. Like Paul Ford, I decided right then to get better. Problem was, I wasn't sure exactly how to go about it. Fortunately, a co-worker had the answer. He lent me his copy of [Code Complete](http://www.amazon.com/gp/product/0735619670/ref=as_li_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=0735619670&linkCode=as2&tag=youvebeenhaac-20&linkId=RDVAZIUH22CSYWDA), and my eyes were opened.

Reading this book changed the arc of my career for the better. Programming went from a dalliance to pay off some of my student loan bills, to a profession I wanted to make a career out of. I fell in love with the practice and craft of writing code. This was for me.

The good news is I never was fired from my first job. I ended up staying there seven years, grew into a lead and then a manager of all the developers, before deciding to leave when my interests lead elsewhere. During that time, I certainly deployed more bugs, but I was much more rigorous and the impact of those bugs were small.

So there you go, that's my Getting Better Moment. What was yours like?
