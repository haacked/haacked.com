---
title: The IIS 7 Team Rocks!
date: 2007-05-21 -0800 9:00 AM
tags: [tech]
redirect_from: "/archive/2007/05/20/the-iis-7-team-rocks.aspx/"
---

I recently wrote about some [503 Service Unavailable
Errors](https://haacked.com/archive/2007/05/18/service-unavailable-errors-in-iis-7-are-killing-me.aspx "Service Unavailable in IIS 7")
with IIS 7 that had me completely stumped. I tried everything I could
think of to no avail.

Fortunately, a few of the members of the IIS 7 team stepped in to help.
First, I received an email from [Bill
Staples](http://blogs.iis.net/bills/ "Bill Staples"), the group program
manager of the IIS 7 team, kindly offered his assistance.

Meanwhile, [Mike
Volodarsky](http://mvolo.com/blogs/serverside/ "Mike Volodarsky"), a
Program Manager on the IIS team in charge of the IIS 7 Web Server engine
started offering help in my comments. Mike has a great blog with many
useful troubleshooting tips for IIS 7. Highly recommended.

Mike brought in Chun Ye, a member of the http.sys team, who helped me
get to the bottom of the problem. Here is the command he had me run.

> `netsh http show urlacl`

The result showed that I had reserved *http://+:80/* which takes
precedence over all other URLs on port 80.

>     Reserved URL : http://+:80/
>         User: METAVERSE\Phil
>            Listen: Yes       Delegate: No
>            SDDL:
>     D:(A;;GX;;;S-1-5-21-1527697538-1582451445-1978546337-1000) 

The solution was to run this command:

> `netsh http delete urlacl url=http://+:80/`

Which removed the reservation.

To be honest, I have no idea why that reservation exists. Most likely it
was something dumb I did a long time ago trying to debug some other long
forgotten problem. I probably forgot to revert my change or didn’t even
realized I had made a change.

I don’t have a real deep understanding of http.sys reservations. What I
do know about it mostly comes from [this
post](http://pluralsight.com/blogs/keith/archive/2007/04/01/46636.aspx "Http Reservations")
by security guru [Keith
Brown](http://pluralsight.com/blogs/keith/ "Keith Brown").

In any case, many thanks to the IIS 7 team for your help. You rock in my
book.

