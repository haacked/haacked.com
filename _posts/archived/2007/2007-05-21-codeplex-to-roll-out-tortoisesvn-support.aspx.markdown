---
title: CodePlex To Roll Out TortoiseSVN Support
tags: [oss]
redirect_from: "/archive/2007/05/20/codeplex-to-roll-out-tortoisesvn-support.aspx/"
---

This just in. CodePlex is planning to roll out TortoiseSVN support!

[![](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CodePlexToRollOutTortoiseSVNSupport_CD86/codeplexworkitemdetails_thumb1.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CodePlexToRollOutTortoiseSVNSupport_CD86/codeplexworkitemdetails3.png "Codeplex SVN Work Item Detail")
A little while ago I wrote a [comparison of TFS vs Subversion for Open
Source
projects](https://haacked.com/archive/2007/03/02/A_Comparison_of_TFS_vs_Subversion_for_Open_Source_Projects.aspx "TFS vs Subversion").
I’ll spare you the suspense by telling you that Subversion wins hands
down, primarily because it itself is open source and is designed with
open source in mind.

It turned out that there was already a [work item for SVN
support](http://www.codeplex.com/CodePlex/WorkItem/View.aspx?WorkItemId=7082 "Subversion Support")
and it was the highest vote getter. On Friday, Jim Newkirk commented
within the work item that they are adding support for
[TortoiseSVN](http://tortoisesvn.tigris.org/ "TortoiseSVN"). You can see
in the work Item Details (click to enlarge) that it is set to release on
June 5, 2007.

Jim’s comment mentions TortoiseSVN support specifically, but I assumed
he meant Subversion support. Unless they are building some sort of
Subversion bridge to TFS.

As I wrote in a [recent
post](https://haacked.com/archive/2007/05/13/is-fighting-open-source-with-patents-a-smart-move-by.aspx "Fighting Open Source With Patents"),
there appears to be two camps at Microsoft in regards to Open Source
- the old guard who are threatened by it, and the new guard who are
embracing it as the opportunity it really is for Microsoft. CodePlex is
emblematic of the new guard. They are listening to their users and
building something that really will benefit the .NET OSS community. My
heartfelt kudos to the CodePlex team!

Now it’s time for me to approach the rest of the Subtext devs to talk
about a potential move after June 5. Of course, being pragmatic. I’ll
want to test it out first. Kick the tires before making any commitments.

UPDATE: It looks like the CodePlex team has created a Subversion bridge
to TFS. In other words, from the outside, it looks just like Subversion
so you can connect using TortoiseSVN and SVN.exe, but under the hood, it
is TFS. Technologically speaking, that sounds pretty cool. It couldn’t
have been trivial. However, I’m a bit skeptical it will have (and
maintain) feature parity, but I’m looking forward to trying it out. Not
exactly what I hoped for, but I won’t sit around and bitch about it if
it works.

