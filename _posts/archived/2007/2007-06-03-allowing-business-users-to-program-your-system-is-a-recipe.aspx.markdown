---
title: Allowing Business Users To Program Your System Is A Recipe For Disaster
tags: [product-management]
redirect_from: "/archive/2007/06/02/allowing-business-users-to-program-your-system-is-a-recipe.aspx/"
---

I don’t know about you, but every company I’ve ever worked at had a Fort
Knox like system in place for deploying code to the production server.
Typically, deployment looks something like this (some with more steps,
some with less):

1.  Grab the labeled (tagged) code from the version control system.
2.  Obviously, ensure that the application must compile.
3.  Another developer other than the author must review the code on some
    level and sign off on it.
4.  Automated unit tests must pass.
5.  If they exist, the automated system and integration tests must pass.
6.  The QA team tests the application and approves it.
7.  The deployment engineer (typically a developer or QA person) very
    carefully deploys the application attempting to avoid any downtime.

**Interestingly enough, many of these companies didn’t have the same
procedures for other documents and systems used to run the business**.
For example, one could in theory login to their CMS system and change
the home page of the site to contain every expletive in the book just
for fun and it would show up immediately.

[![Spreasheet photo by
http://www.flickr.com/photos/caterina/](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TheChallengeWithBringingProgrammingToThe_11600/15821634_563b5f83fb_1.jpg)](http://www.flickr.com/photos/12037949663@N01/15821634/ "Stewart spreadsheets the pizza order")
There are a lot of people who want to make it so that the business user
can write code by [connecting
legos](http://secretgeek.net/lego_software.asp "Can Software Be Like Building Legos").
The typical examples include dynamic rules engines and their ilk. Yeah,
let’s let Joe the finance guy tweak the rules on the rules engine on the
fly by drawing lines and connecting boxes.

The problem with approaches like this is that it ignores the fact that
the effect of these changes is no different than writing code, but often
with much fewer checks on quality before it gets deployed to where it
can do damage.

These systems often are lacking:

-   Version Control
-   Backup and Restore procedures
-   Quality Assurance testing
-   Formal Deployment procedures

A [recent
report](http://eusprig.org/stories.htm "Spreadsheet Mistakes News Stories")
([via
Reddit](http://programming.reddit.com/info/1vopw/comments/ "Reddit Link to story"))
illustrates this point with a list of news stories on how errors in
spreadsheets have cost businesses millions of dollars. A couple of
telling snippets (emphasis mine). This one on the lack of version
control and auditing:

> [http://www.namibian.com.na/2005/October/national/05E0F49179.html](http://www.namibian.com.na/2005/October/national/05E0F49179.html)\
> The Agricultural Bank of Namibia (Agribank) is teetering on the edge
> of bankruptcy. "**There is no system of control on which the auditors
> can rely nor were there satisfactory auditing procedures that could be
> performed to obtain reasonable assurance that the provision for
> doubtful debts is adequate and valid,**" note the auditors. Auditors
> found that its loan amount to the now defunct !Uri !Khubis abattoir
> changed from N\$59,5 million on one spreadsheet to N\$50,4 million on
> another, while the total arrears was decreased from a whopping N\$9,8
> million to only N\$710 000.

And this one on the lack of training and Quality Assurance.

> [Only a matter of time before the spreadsheets hit the
> fan](%20www.telegraph.co.uk/money/main.jhtml?xml=/money/2005/06/30/ccspread30.xml&menuId=242&sSheet=/money/2005/06/30/ixcoms.html "Only a matter of time before the spreadsheets hit the fan")
> - Telegraph (UK), 30 June 2005\
> In his paper "The importance and criticality of spreadsheets in the
> City of London" presented to Eusprig 2005, Grenville Croll of
> Frontline Systems (UK) Ltd. reported on a survey of 23 professionals
> in the £13Bn financial services sector. The interviewees said that
> spreadsheets were pervasive, and many were key and critical. There is
> almost no spreadsheet software quality assurance and people who create
> or modify spreadsheets are almost entirely self-taught. Two each
> disclosed a recent instance where material spreadsheet error had led
> to adverse effects involving many tens of millions of pounds.

**The solution is not to make programming more like the way business
users work now. The solution is to apply the lessons learned from
software development into other business processes.**

In the same way that companies rely on heavily trained developers and
rigid deployment procedures in place for code, companies should make
sure their business people are just as heavily trained in the software
*they* use on a day to day basis. After all, million dollar decisions
are based on the content of these systems daily.

For example, spreadsheets should be version controlled. Changes to rules
within a rules engine should have to pass some automated tests and
manual QA before being deployed. All of these should be peer reviewed.

