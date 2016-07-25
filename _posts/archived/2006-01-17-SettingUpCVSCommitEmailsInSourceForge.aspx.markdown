---
layout: post
title: Setting Up CVS Commit Emails In SourceForge
date: 2006-01-17 -0800
comments: true
disqus_identifier: 11526
categories: []
redirect_from: "/archive/2006/01/16/SettingUpCVSCommitEmailsInSourceForge.aspx/"
---

One key component of open source projects is getting others involved in
code review. In fact, that has always been one of the promises of open
source software that with the hundreds and thousands of eyeballs looking
at the code, the quality will be higher. In practice this doesn’t
necessarily work out because the majority of open source projects only
have a few eyeballs at work.

Also, in order to get those eyeballs reviewing code, the bar of entry
must be low. Requiring users to set up CVS and track commit logs is a
major hurdle. That’s where commit emails come in. Commit emails are
triggered by a commit to the source code repository and typically
contain the log message of the commit, which file or files were changed,
and a diff of the changes.

Having others review commit emails also keeps people honest in their log
messages and provides a means to help propagate and support project
standards.

Originally I intended to generate an RSS Feed for commits, but I found
that the approach I was taking was impossible in SourceForge.
SourceForge does not allow writing to the Web Shell Services from
scripts running within CVS. However, I have another idea brewing that
won’t require file write access to the shell web services, but
I\#&8217;ll need to brush up on Python first.

Sourceforge uses [CVS
syncmail](https://sourceforge.net/projects/cvs-syncmail/) for sending
out email notifications of commits within CVS. Each email provides the
log message and a diff (an example is at the bottom).

Setting this up is pretty well [described by the
documentation](https://sourceforge.net/docman/display_doc.php?docid=29894&group_id=1#syncmail).
The only thing I have to add is that to checkout the CVSROOT module,
follow the same instructions for checking out the project’s main module
[as I wrote in the
past](http://haacked.com/archive/2005/05/12/3178.aspx) , but enter
"CVSROOT" for the Module name at the bottom of the TortoiseCVS dialog.

This is currently set up for Subtext (click [here if you wish to
subscribe](http://lists.sourceforge.net/lists/listinfo/subtext-commits)).

### Example of a commit message

>     Update of /cvsroot/subtext/CVSROOT
>     In directory sc8-pr-cvs1.sourceforge.net:/tmp/cvs-serv11754
>
>     Modified Files:
>         loginfo
>     Log Message:
>     Changing the comment for the purpose of testing commit emails.
>
>     Index: loginfo
>     ===================================================================
>     RCS file: /cvsroot/subtext/CVSROOT/loginfo,v
>     retrieving revision 1.7
>     retrieving revision 1.8
>     diff -C2 -d -r1.7 -r1.8
>     *** loginfo 16 Jan 2006 17:02:50 -0000  1.7
>     --- loginfo 16 Jan 2006 17:07:31 -0000  1.8
>     ***************
>     *** 27,33 ****
>       #DEFAULT (echo ""; id; echo %{sVv}; date; cat) >> $CVSROOT/CVSROOT/commitlog
>       
>     ! # This line sends all changes to the CVSROOT module to the user specified ! # by USERNAME. It is recommended that someone be watching this module ! # as it shouldn't need to be modified very often.
>       CVSROOT /cvsroot/sitedocs/CVSROOT/cvstools/syncmail %{sVv} haacked@users.sourceforge.net
>       
>     --- 27,32 ----
>       #DEFAULT (echo ""; id; echo %{sVv}; date; cat) >> $CVSROOT/CVSROOT/commitlog
>       
>     ! # This line sends all changes to the CVSROOT module to the specified email address.  
>     ! # This module shouldn't need to be modified very often.
>       CVSROOT /cvsroot/sitedocs/CVSROOT/cvstools/syncmail %{sVv} haacked@users.sourceforge.net
>       

