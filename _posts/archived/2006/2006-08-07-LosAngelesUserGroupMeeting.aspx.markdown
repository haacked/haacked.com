---
title: Los Angeles User Group Meeting
date: 2006-08-07 -0800
disqus_identifier: 14755
categories: []
redirect_from: "/archive/2006/08/06/LosAngelesUserGroupMeeting.aspx/"
---

Tonight I attended our local [Los Angeles .NET Developers
Group](http://www.ladotnet.org/ "Los Angeles .NET Developers Group")
meeting for the first time in years. I pretty much never go to these
meetings because I just haven’t found them worth dealing with the
congestion of rush hour traffic in the UCLA area, which is really bad.
Of course I should probably view user group meetings in the same way
[Jeff
Atwood](http://www.codinghorror.com/blog/archives/000544.html "On Conferences")
views conferences - I am not there for the talks, I am there to meet
you.

However the local group does bring in some great speakers via
[INETA](http://www.ineta.org/DesktopDefault.aspx "International .NET Association").
Tonight’s meeting featured [Rob
Howard](http://weblogs.asp.net/rhoward/ "Rob Howard"), the CEO of
[Telligent](http://telligent.com/ "Telligent"). I first met Rob at Mix06
and it was good to see him again at this meeting. He gave a great talk
on ASP.NET tips and tricks. The one trick that stood out to me had
nothing to do with his talk. I noticed at one point he had SQL code with
expandable regions much like code regions via the `#region` directive.
Instead of the pound sign, they used `--region`. I just tried that with
a .sql file and it didn’t work for me. Probably requires a database
project. I’ll have to ask him about that.

I recognized one guy in attendance who happened to be [Michael
Washington](http://dotnetnuke.com/Community/Blogs/tabid/825/BlogID/77/Default.aspx "Michael Washington"),
a member of the [DotNetNuke core
team](http://dotnetnuke.com/Development/CoreTeam/tabid/698/Default.aspx "DNN Core Team").
He patiently listened to my *constructive criticism* and we discussed
ideas for improvements to module development. One thing I hope to help
him with is incorporating more unit tests into DNN code he is working
on. [Andrew
Stopford](http://weblogs.asp.net/astopford/ "Leader of MbUnit") will be
pleased that I am trying to steer Michael towards
[MbUnit](http://mbunit.com/ "MbUnit").

The challenge will be how to integrate unit tests into the ASP.NET Web
Site model, since VS.NET Web Developer Express does not support class
library projects. This may be a no brainer, but I have never tried it.
The tests will probably just be dropped in the `App_Code` folder, but
will TD.NET run all tests by right clicking on App\_Code and selecting
*Run Tests*. I assume so, but we’ll see.

