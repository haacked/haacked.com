---
title: IronRuby With ASP.NET MVC Working Prototype
tags: [aspnetmvc,ruby]
redirect_from: "/archive/2008/07/19/ironruby-aspnetmvc-prototype.aspx/"
---

UPDATE 02.17.2009: I [posted about a newer version of this
prototype](https://haacked.com/archive/2009/02/17/aspnetmvc-ironruby-with-filters.aspx "ASP.NET MVC with IronRuby and Filters")
for ASP.NET MVC RC

Update: I updated the source today. It now has minimal support for
layouts. It needs more improvement for sure.

In June, [John Lam](http://www.iunknown.com/ "John Lam") wrote about a
demo [he gave at Tech-Ed
2008](http://www.iunknown.com/2008/06/ironruby-and-aspnet-mvc.html "IronRuby and ASP.NET MVC")
where he showed IronRuby running on ASP.NET MVC. He posted the code for
the demo online, but it relied on an unreleased version of MVC, so the
code didn’t actually work.

![IronRuby on ASP.NET MVC
Demo](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IronRubyWithASP.NETMVCWorkingPrototype_BDF3/IronRuby%20on%20ASP.NET%20MVC%20Demo%20-%20Windows%20Internet%20Explorer_3.png "IronRuby on ASP.NET MVC Demo")
Now that Preview 4 is out, I revisited the prototype and got it working
again. I use the term *working* loosely here. Yeah, it works, but it is
really rough around the edges. As in, get a bunch of splinters rough. At
least it *looks* better as I did take a moment to use a CSS layout from
[Free CSS
Templates](http://www.free-css-templates.com/ "Free CSS Templates")
slightly tweaked by me.

### Getting the Prototype Up And Running

The IronRuby assemblies are based on an internal build, so they aren’t
the same version as the publicly available ones. As such, they are not
fully signed so you might get an ugly error message when you try to run
the demo. You’ll need to add a verification entry into the GAC using the
`sn –Vr assemblyname.dll` command.

I was lazy and just ran the following to add a verification entry for
any assembly (*note: there is a security risk in doing this*):

`sn –Vr *,*`

And when I was done, I ran:

`   `

sn –Vu \*,\*

to remove that catch-all verification entry.

### Other Notes

I made a few small improvements to the project since John posted the
code. I added helper overloads that accept ruby hashes, for example.
This allows you to do the following within the view:

```aspx-cs
<%= $html.ActionLink("Home", {:controller => "Home", :action => "index"}) %>
```

I also implemented more of the application, including the ability to
edit an item. That forced me to get some more of the form helpers
working with IronRuby.

As I mentioned in my previous post on this topic, [IronRuby and ASP.NET
MVC BFFs
Forever](https://haacked.com/archive/2008/06/12/ironruby-and-asp.net-bffs-forever.aspx "IronRuby and MVC"):

> **Disclaimer:** This is all a very rough prototype that we’ve been
> doing in our spare time for fun. We just wanted to prove this could
> work at all.

If this sort of stuff interests you, have fun with it. I’ll try and post
updates as we take this prototype further. It has definitely been a
worthwhile exercise as we’ve found many areas for improvement in our
extensibility layer. I believe ASP.NET MVC will be better overall
because we spent the time to do this.

And before I forget, here’s the **[DOWNLOAD
LINK](https://haacked.com/code/IronRubyMvcDemo.zip "IronRubyMVC Demo")**.

Attributions: The CSS layout and ruby logo are both licensed under the
[Creative Creative Commons Attribution-ShareAlike
2.5](http://creativecommons.org/licenses/by-sa/2.5/ "Creative Commons Share Alike 2.5")
License. Full attribution is within the source.
