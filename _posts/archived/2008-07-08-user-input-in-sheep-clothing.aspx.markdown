---
layout: post
title: User Input In Sheep&rsquo;s Clothing
date: 2008-07-08 -0800
comments: true
disqus_identifier: 18502
categories:
- asp.net mvc
- asp.net
- code
redirect_from: "/archive/2008/07/07/user-input-in-sheep-clothing.aspx/"
---

We all know that it is bad bad bad to trust user input. I don’t care if
your users are all ascetic monks in a remote monastery, do not trust
their input. However, user input often likes to put on sheep’s clothing
and disguise itself as something else entirely, such as [the case with
ViewState](http://scottonwriting.net/sowblog/posts/3747.aspx "Don't Trust ViewState").

Another example of this is highlighted in the [latest
entry](http://weblogs.asp.net/stephenwalther/archive/2008/07/08/asp-net-mvc-tip-15-pass-browser-cookies-and-server-variables-as-action-parameters.aspx "Passing Cookie and Server Variable Values To action methods")
of his excellent series of ASP.NET MVC tips. In this post, [Stephen
Walther](http://weblogs.asp.net/stephenwalther/ "Stephen Walther's blog")
writes about how cookie values and server variables can be passed as
parameters to action methods.

Immediately, commenters understably asked whether this was safe or not.
One person went so far as to call this a [security hole in
controller](http://www.squaredroot.com/post/2008/07/08/MVC-Controller-Action-Security-Hole.aspx "Security Hole in Routing")
actions.

However, to be extremely nitpicky, the security implication isn't in
passing server variables this way. That’s perfectly safe. The security
implication is in **trusting the values passed to an action method in
the first place**. If your action method makes decisions with security
implications based on assuming that these values are accurate, then you
have a potential security problem.

Keep in mind, many of these values can be spoofed with or without
ASP.NET MVC. Many of the server variables should never be trusted no
matter how you access them, whether via this technique or a call to
`Request.ServerVariables["variable_name"]`.

In fact, right there near the top of the [MSDN documentation for the IIS
Server
Variables](http://msdn.microsoft.com/en-us/library/ms524602.aspx "IIS Server Variables on MSDN"),
it warns against trusting these values:

> Some server variables get their information from HTTP headers. It is
> recommended that you distrust information in HTTP headers because this
> data can be falsified by malicious users.

In the same way, in a typical configuration for ASP.NET MVC, the
parameter values for action methods come directly from the user in the
form of the URL or Request parameters. This makes sense after all, since
the whole point of a controller in the MVC pattern, [according to
Wikipedia](http://en.wikipedia.org/wiki/Model-view-controller "Wikipedia on MVC"),
is to:

> Processes and responds to events, **typically user actions**, and may
> invoke changes on the model.

The parameters to an action method generally correspond to user input.
It’s really asking for trouble to have parameters in an action method
that you consider to be anything but user input.

In the end, I don’t consider this a security *flaw* so much as a
security *lure*. This is the type of thing that might tempt someone to
do the wrong thing and trust these values. We will review this
particular case and consider not passing in server variables into action
methods, but doing so doesn’t really solve the fundamental issue. There
are other ways to pass data to an action method (defaults on a route)
that a developer might be tempted to trust (don’t trust it!).

Whether we change this or not, the fundamental issue is that developers
should **never trust user input** and developers should always **treat
action parameter values as user input**.

Tags: [aspnetmvc](http://technorati.com/tags/aspnetmvc/ "aspnetmvc tag")
, [security](http://technorati.com/tags/security/ "security tag")

