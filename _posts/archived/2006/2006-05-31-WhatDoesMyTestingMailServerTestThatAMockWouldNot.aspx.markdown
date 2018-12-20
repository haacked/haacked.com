---
title: What Does My Testing MailServer Test That A Mock Would Not?
date: 2006-05-31 -0800
tags: []
redirect_from: "/archive/2006/05/30/WhatDoesMyTestingMailServerTestThatAMockWouldNot.aspx/"
---

[Greg Young](http://geekswithblogs.net/gyoung/ "Greg Young's Blog")
takes my [Testing Mail
Server](https://haacked.com/archive/2006/05/30/ATestingMailServerForUnitTestingEmailFunctionality.aspx "Testing Mail Server")
to task and asks the question, [what does it test that a mock provider
doesn’t](http://geekswithblogs.net/gyoung/archive/2006/05/31/80292.aspx "Unit Test???")?

It is a very good question and as he points out in his blog post on the
subject, it seems like a lot of overhead for very little benefit. For
the most part, he is right.

To my defense, and as Greg points out, I would not start with such a
test when writing email functionality. I would start with the mock email
provider and follow the typical Red-Green TDD pattern of development.
However there are cases where this approach does not test enough and
this testing server was necessitated by some real world scenarios I ran
into.

For example, in some situations, it is very important to understand the
exact format of the raw SMTP message that is sent. Some systems actually
use email from server to server to kick off automated tasks. In that
situation, it helps to know that the SMTP message is formatted as
expected by the receiving server. For example, you may want to make sure
that the appropriate headers are sent and that the message is not a
multi-part message. This approach lets you get at the raw SMTP message
in a way that the mock provider approach cannot.

A more common issue is when sending mass mailings such as newsletters to
subscribers. At one former employer, we had real difficulty getting our
emails to land in our user’s mailbox despite adhering to appropriate
SPAM laws and only mailing to subscribed users.

It turns out that actually landing a mass mailing even to users who want
the email is very tough when dealing with Hotmail, yahoo, and AOL
accounts. Something as seemingly innocuous as the `X-Mailer` header
value can trigger the spam filters.

In this case, this very much falls under the rubrik of an integration
test, as I am testing the actual mailing component in use. But I am not
only testing the particular mailing component. I am also testing that my
code uses the mailing component in a correct manner.

So in answer to the question, **Where’s the red bar?** The red bar comes
into play when I write my unit test and asser that the `X-Mailer` header
is missing. The green bar comes into play when I make sure to remove the
header. I could probably test this with a mock object as well, but I
have been burnt by mailing components that did not remove the X-Mailer
header but simply set the value to blank, when I really intended it to
be removed. That is not something a mock object would have told me.

