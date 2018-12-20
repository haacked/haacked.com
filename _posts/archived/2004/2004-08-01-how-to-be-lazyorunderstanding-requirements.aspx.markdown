---
title: How To Be Lazy...or...Understanding Requirements
date: 2004-08-01 -0800
disqus_identifier: 875
tags: [product-management]
redirect_from: "/archive/2004/07/31/how-to-be-lazyorunderstanding-requirements.aspx/"
---

If you’re a lazy mofo like me, there’s one thing you’re not lazy about,
getting out of having to do a lot of work. An important skill that
should be on every lazy developer’s utility belt is the ability to
understand requirements.

However, there’s a subtle technique to this. When I say, “understand
requirements”, I don’t necessarily mean understanding what the client
tells you the requirement is, but taking a moment to dig deeper and get
to the real purpose of the requirement. Let me tell you a story based on
a true story.

> WeLikeCustomers.com has a two email systems for communicating to its
> clients. One system sends news letters and “Please come back” emails
> to clients who have opted in. The other system sends emails welcoming
> newly registered users as well as password reminders etc... The second
> system is designed to send only the most important emails so as not to
> get blacklisted.
>
> Millard in Marketing decides that the second system should route
> emails through an email delivery service that guarantees delivery. He
> tells Damien the developer that only emails in a certain category
> should be sent through the new delivery service, and all others should
> be sent in the current manner.
>
> Well Damien thinks about it and tells Millard it will take a day or so
> to add this routing within the second system. Currently the system
> only supports sending emails to one configured SMTP server. Now he has
> to add the ability to send some emails to another SMTP server based on
> certain criteria. He dives right into designing a general purpose
> email routing system. After a bit of time, he has the sudden thought,
>
> “Now why the hell are they routing only a portion of emails to this
> new service anyways?”
>
> So he asks Millard, who replies,
>
> “To make it easier for you of course.”
>
> After some discussion, Damien finds out that Millard would prefer to
> send all emails to the delivery service. So he changes one setting in
> his config file pointing to the service’s SMTP address, closes his
> office door, and takes a nap. Meanwhile, Millard is sending out emails
> complimenting Damien for going above and beyond the call of duty.

Be like Damien.

