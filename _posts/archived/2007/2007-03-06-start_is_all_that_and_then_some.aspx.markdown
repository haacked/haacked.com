---
title: Start++ Is All That And Then Some
tags: [tips,tools]
redirect_from: "/archive/2007/03/05/start_is_all_that_and_then_some.aspx/"
---

Update: I have an even better startlet for stopping and starting
services in my comments.

If you’re running Vista, run, don’t walk, and go [download and install
Start++](http://brandonlive.com/startplusplus/download "Start++ Download")
(thanks to [Omar
Shahine](http://www.shahine.com/omar/ "Omar Shahine’s Blog") for
[turning me on to
this](http://www.shahine.com/omar/Start.aspx "Start++")). Make it the
first thing you do. Many thanks to [Brandon
Paddock](http://brandonlive.com/ "Brandon Paddock’s Blog") who developed
this nice little tool. He describes [the tool in this
post](http://brandonlive.com/2007/02/22/new-tool-i-made-for-vista-start/ "New Tool For Vista").

I have a message for Start++ from the Start menu. “You complete me!”.

Ok, terribly corny jokes aside, **it’s the little things that save me
lots of time in the long run**. For example, starting and stopping SQL
server is kind of annoying for me on Vista. Here’s my the typical
workflow.

1.  Hit *Windows Key*, type in *cmd*
2.  type *net stop mssql*
3.  Doh! System error 1060 occurred. Right, I need to be an
    administrator.
4.  Grab the trackball
5.  Click on the *Start* menu
6.  Right click *Command Prompt*
7.  Click *Run as administrator*.
8.  Now type *net stop mssql*.

Is your hand hurting by now? Because mine is.

Of course, I’m an idiot. Or, I *was* an idiot. Now, I’ve mapped the
Start++ keywords *startsql* and *stopsql* to automatically run the
commands I need with elevated privileges.

*Click for larger image.*

[![Start++
Menu](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/StartIsAllThatAndThenSome_ABF8/Start++_thumb%5B3%5D.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/StartIsAllThatAndThenSome_ABF8/Start++%5B5%5D.png)

Notice you can check the *Run elevated* checkbox for any command. Yes, I
get the UAC prompt (Yes, I still have that sucker on), but that’s not
such a big deal to me. Now my workflow is reduced to:

1.  Hit *Windows Key*, type in *stopsql*
2.  Hit the *Left Arrow Key* and *Enter* when the UAC prompt comes up.

Booya!

For your convenience, I’ve exported the *startsql* and *stopsql*
“startlets” and put them on [my company’s tools site
here](http://tools.veloc-it.com/tabid/58/grm2id/22/Default.aspx "Start and Stop SQL Start++ Startlet").
I figure this one alone saves me a few seconds every half hour.

If you are using a named instance of SQL Server, you will need to change
the argument in the *Arguments* column like so:

`/C "net start mssql$NameOfInstance"`

I have a few hundred or so startlets I can think of adding. Happy
shortcutting!
