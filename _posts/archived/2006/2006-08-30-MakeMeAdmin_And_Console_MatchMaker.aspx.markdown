---
title: MakeMeAdmin And Console MatchMaker
tags: [tools]
redirect_from: "/archive/2006/08/29/MakeMeAdmin_And_Console_MatchMaker.aspx/"
---

I am still continuing [my
experiment](https://haacked.com/archive/2006/04/28/YouveBeenHaacked1KTimes.aspx "My 1000th post")
in running as a LUA (aka Non-Admin).  Let me tell you, it has been a
total pain in the ass and now I totally understand why more developers
do not do this, which feeds into the vicious cycle in which apps are
developed that do not run well under least user privileges.  When I have
some time, I will write up my experiences.

One tool that has been invaluable in this experiment is the
[MakeMeAdmin](http://blogs.msdn.com/aaron_margosis/archive/2004/07/24/193721.aspx "MakeMeAdmin - temporary admin for your limited user account")
batch file used to temporarily elevate your privilegs in a command
window.  This has worked nicely for me for a while.

![Console
Screenshot](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/MakeMeAdminAndConsole_A6BC/MakeMeAdminConsole4.gif)

Then [Scott Hanselman](http://computerzen.com/blog/) points out
[Console](http://www.hanselman.com/blog/ABetterPROMPTForCMDEXEOrCoolPromptEnvironmentVariablesAndANiceTransparentMultiprompt.aspx "A Better Prompt")
that takes cmd.exe and adds transparency and tabs.  Just pure geek
hotness that I gotta have.

However, the only command shell I normally keep open is my MakeMeAdmin
shell.  It’d be a shame to install Console and never see its sleek
hotness.  So I decided to play matchmaker and see if I could marry these
two wonderful utilities.

I modified the MakeMeAdmin.bat file to use Console instead.  It was a
one line change (note file paths should be changed to fit your setup and
the line break in there is for formatting purposes. There shouldn’t be a
line break.).

```csharp
set _Prog_="console.exe c:\console_admin.xml 
    -t """*** %* As Admin ***""""
```

I also created a new admin config file named `console_admin.xml` that
specifies transparency and a red tint which lets me know that *this*
console window is not like the others. It will run commands as an admin.

I’ve uploaded my modified MakeMeAdmin.bat file as well as the console
configuration file to my company’s [tool site
here](http://tools.veloc-it.com/tabid/58/grm2id/11/Default.aspx "Tools Tools Tools for Developers Developers Developers"). 
Hopefully all five of you out there also running as a non-admin will
find this useful.

