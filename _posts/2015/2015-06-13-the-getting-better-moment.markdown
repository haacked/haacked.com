---
layout: post
title: "The Getting Better Moment"
date: 2015-05-14 -0800
comments: true
categories: [code bugs software]
---

Unless you've been passed out drunk in a gutter for the last week (which is much more likely than you living under a rock), you've probably heard about this amazing opus by [Paul Ford](https://twitter.com/ftrain) entitled "[What is Code?](http://www.bloomberg.com/graphics/2015-paul-ford-what-is-code/)"

If you haven't read it yet, cancel all your appointments, grab a beer, find a nice shady spot, and soak it all in. Tell your friends to read it too. If you can, definitely read it in a proper browser and not on a mobile device.

I'm not going to write about the whole work. It stands on its own. Instead, I want to focus on one paragraph in particular. In the intro, Paul talks about his programming start.

> I began to program nearly 20 years ago, learning via oraperl, a special version of the Perl language modified to work with the Oracle database. A month into the work, I damaged the accounts of 30,000 fantasy basketball players. They sent some angry e-mails. After that, I decided to get better.

This was his "getting better moment" and like many such moments, it was the result of a coding mistake early in his career. It got me thinking about my "getting better moment" which happened a long time ago when websites were still in black and white and connected to the net by string and cans.

![The Internet circa 1997 - image from Wikipedia - Public Domain](https://cloud.githubusercontent.com/assets/19977/8146357/9c0bb4ea-11e8-11e5-9706-43dd76ae6205.png)

Upon graduating from college, I got a job with a small custom software shop named Sequoia Softworks, which was at the time located in the quaint town of Seal Beach, California. You know it's a beach town because it's right there in the name. The company is still around under the [name Solien](https://solien.com).

I was understandably nervous my first week. I was a fresh-fashed recent graduate with a Math degree that pretty much was useless for the work I was about to engage in. Sure, it prepared me to think logically, but I didn't know a database from an Active Server Page, which was the technology we used. Now we call it "Classic" ASP, but at the time, it was the hot new thing.

Fortunately, a nice contractor took me under her wing and showed me the ropes. She taught me VBScript and how to access a SQL Server database. Perhaps the most valuable lesson I learned was this:

```vb
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

The important bit is the line with the comment. Never ever ever ever forget to call `rs.MoveNext`.

Being a tiny company, it wasn't long before I was working on some important projects. I recall working on a [music site called myLaunch](http://solien.com/client-successes/Pages/launch-media.aspx) which later became Launch which later was bought and incorporated into Yahoo Music.

One of my tasks was to make some changes to the Forgot Password flow. Like any good developer, I dove in and cranked out the improvements. This was before Extreme Programming popularized the idea of test driven development, so I didn't write any automated tests.

But I tested my changes. At least, I'm pretty sure I did. I probably tried it a couple times, saw the record in the database, might have seen the email or not. I don't recall. You know, rigorous testing.

It wasn't long after the change was deployed that I was called into the CTO (and co-founder's) office. Turns out that a VP at our customer had a daughter who used the site to read about her favorite bands and she had forgotten her password. She went to reset her password, but never got the email with the new generated password.

I quickly learned there's a pecking order to finding bugs. It's better to

1. ... find a bug in the spec than in the code.
2. ... have the compiler find a bug than find it at runtime.
3. ... find a bug at runtime yourself before a customer runs into it.
4. ... have a user find a bug than the daughter of your client's Vice President.

The meeting with my boss was pretty uncomfortable. I found some archival footage of me as I sat in the hot seat getting reamed out.

![I wore headphones everywhere back then](https://cloud.githubusercontent.com/assets/19977/8146368/361c167e-11e9-11e5-8d24-e1aa1a7360bc.png)

I don't remember the specifics, but he made it clear to me that if I made another mistake like that, I would be fired from my first full-time job. Gulp! _And by "Gulp!" I don't mean a JavaScript build system._

That was my Getting Better Moment, inspired by a fear of losing my job. Fortunaly, a colleague lent me his copy of [Code Complete](http://www.amazon.com/gp/product/0735619670/ref=as_li_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=0735619670&linkCode=as2&tag=youvebeenhaac-20&linkId=RDVAZIUH22CSYWDA) at this time.

Reading this book changed the arc of my career for the better. Up until that point, programming was a job I took to help pay off my student loan bills while I contemplated going to graduate school. After reading this book, I fell in love with the practice and craft of writing code. This was for me. It inspired me to dedicate myself to a lifelong path of improving the way I build software. And to hopefully helping others do the same through the products I build and the things I write.

And the good news is I wasn't fired from my first job. I ended up staying their seven years becoming the manager of all the developers before deciding to leave to try my hand at other things.

So there you go, that's my Getting Better Moment. What was yours?
