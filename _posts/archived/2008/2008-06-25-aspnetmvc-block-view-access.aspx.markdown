---
layout: post
title: 'Security Tip: Blocking Access to ASP.NET MVC Views Using Alternative View
  Engines'
date: 2008-06-25 -0800
comments: true
disqus_identifier: 18498
categories: [tips aspnetmvc security]
redirect_from: "/archive/2008/06/24/aspnetmvc-block-view-access.aspx/"
---

When you create a new ASP.NET MVC project using our default templates,
one of the things you might notice is that there is a `web.config` file
within the *Views* directory. This file is there specifically to block
direct access to a view.

Let’s look at the relevant sections.

**For IIS 6 (and Cassini)**

```csharp
<add path="*.aspx" verb="*" 
  type="System.Web.HttpNotFoundHandler"/>
 
```

**For IIS 7**

```csharp
<add name="BlockViewHandler" path="*.aspx" verb="*" 
  preCondition="integratedMode" type="System.Web.HttpNotFoundHandler"/>
```

What these sections do is block all access to any file with the *.aspx*
extension within the *Views* directory (or subdirectories). Note that
access is blocked by mapping these file extensions to the
`HttpNotFoundHandler`, so that a 404 error is returned when trying to
access a view directly.

This works great if you are using all the ASP.NET MVC defaults, but what
if you’re using an alternative view engine such as NHaml or NVelocity?
It turns out you can directly request the view and see the code.

You’ll want to make sure to add extra lines within this `web.config`
file with the appropriate file extensions. For example, if I were using
NVelocity, I might add the following (in bold).

**For IIS 6 (and Cassini)**

```csharp
<add path="*.aspx" verb="*" 
  type="System.Web.HttpNotFoundHandler"/>
<add path="*.vm" verb="*" 
  type="System.Web.HttpNotFoundHandler"/>
 
```

**For IIS 7**

```csharp
<add name="BlockViewHandler" path="*.aspx" verb="*" 
  preCondition="integratedMode" type="System.Web.HttpNotFoundHandler"/>
<add name="BlockViewHandler" path="*.vm" verb="*" 
  preCondition="integratedMode" type="System.Web.HttpNotFoundHandler"/>
```

You might be wondering why we don’t block access to all files within the
*Views* directory. Excellent question! In part, because the *Views*
directory is a common place to put other static content such as images,
pdfs, audio files, etc… and we decided not to prevent that option.

However, we’re open to suggestions. We do have a *content* directory in
the default template. We could consider requiring putting static files
in there or in another directory other than *Views*. I’m not sure how
people would feel if they couldn’t put assets intended to support Views
within the *Views*folder.

