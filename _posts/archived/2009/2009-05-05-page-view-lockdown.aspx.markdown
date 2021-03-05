---
title: Put Your Pages and Views on Lockdown
tags: [aspnetmvc,security]
redirect_from: "/archive/2009/05/04/page-view-lockdown.aspx/"
---

[![lockdown](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/PutYourViewsonLockdown_DB88/lockdown_3.jpg "lockdown")](http://www.sxc.hu/photo/1135166 "Lock on stock.xchng")As
I’m sure you know, we developers are very particular people and we like
to have things exactly our way. How else can you explain long winded
impassioned debates over [curly brace
placement](http://www.gskinner.com/blog/archives/2008/11/curly_braces_to.html "Curly Braces: To Cuddle or Not")? 

So it comes as no surprise that developers really care about what goes
in ([and
behind](http://stevesmithblog.com/blog/codebehind-files-in-asp-net-mvc-are-evil/ "Codebehind files are evil"))
their .aspx files, whether they be pages in Web Forms or views in
ASP.NET MVC.

For example, some developers are adamant that a page should not include
server side script blocks, while others don’t want their views to
contain Web Form controls. Wouldn’t it be great if you could have your
views reject such code constructs?

Fortunately, ASP.NET is full of lesser known extensibility gems which
can help in such situations such as the `PageParseFilter`. MSDN
describes this class as such:

> Provides an abstract base class for a page parser filter that is used
> by the ASP.NET parser to determine whether an item is allowed in the
> page at parse time.

In other words, implementing this class allows you to go along for the
ride as the page parser parses the .aspx file and gives you a chance to
hook into that parsing.

For example, here’s a very simple filter which blocks any script tags
with the `runat="server"` attribute set within a page.

```csharp
using System;
using System.Web.UI;

public class MyPageParserFilter : PageParserFilter {
  public override bool ProcessCodeConstruct(CodeConstructType codeType
    , string code) {
    if (codeType == CodeConstructType.ScriptTag) {
      throw new InvalidOperationException("Say NO to server script blocks!");
    }
    return base.ProcessCodeConstruct(codeType, code);
  }

  public override bool AllowCode {
    get {
      return true;
    }
  }

  public override bool AllowControl(Type controlType, ControlBuilder builder)   {
    return true;
  }

  public override bool AllowBaseType(Type baseType) {
    return true;
  }

  public override bool AllowServerSideInclude(string includeVirtualPath) {
    return true;
  }

  public override bool AllowVirtualReference(string referenceVirtualPath
    , VirtualReferenceType referenceType) {
    return true;
  }

  public override int NumberOfControlsAllowed {
    get {
      return -1;
    }
  }

  public override int NumberOfDirectDependenciesAllowed {
    get {
      return -1;
    }
  }
}
```

Notice that we had to override some defaults for other properties we’re
not interested in such as `NumberOfControlsAllowed` or we’d get the
default of 0 which is not what we want in this case.

To apply this filter, just specify it in the `<pages />` section of
web.config like so:

```csharp
<pages 
  pageParserFilterType="Namespace.MyPageParserFilter, AssemblyName">
```

Applying a parse filter for Views in [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC Website") is a bit trickier because
it already has a parse filter registered, `ViewTypeParserFilter`, which
handles part of the voodoo black magic in order to remove the need for
[code-behind in views when using a generic model
type](https://haacked.com/archive/2008/12/19/a-little-holiday-love-from-the-asp.net-mvc-team.aspx "Holiday Love").
Remember those particular developers I was talking about?

Suppose we want to prevent developers from using server controls which
make no sense in the context of an ASP.NET MVC view. Ideally, we could
simply inherit from `ViewTypeParserFilter` and make our change so we
don’t lose the existing view functionality.

That type is internal so we can’t simply inherit it. Fortunately, what
we can do is simply grab the ASP.NET MVC source code for that type,
rename the type and namespace, and then change it to meet our needs.
Once we’re done, we can even share those changes with others. This is
one of the benefits of having an [open source license for ASP.NET
MVC](https://haacked.com/archive/2009/04/01/aspnetmvc-open-source.aspx "Open Source License for System.Web.Mvc").

**WARNING:** The fact that we implement a `ViewTypeParserFilter` is an
implementation detail. The goal is that in the future, we wouldn’t need
this filter to provide the nice generic syntax. So what I’m about to
show you might be made obsolete in the future and should be done at your
own risk. It’s definitely [running with
scissors](http://ayende.com/Blog/archive/2008/03/09/ALT.Net-Logo.aspx "Running With Scissors").

In my demo, I copied the following files to my project:

-   `ViewTypeParserFilter`
-   `ViewTypeControlBuilder`
-   `ViewPageControlBuilder`
-   `ViewUserControlControlBuilder`

I then created a new parser filter which inherits the
ViewTypeParserFilter and overrode the `AllowControl` method like so:

```csharp
public override bool AllowControl(Type controlType, ControlBuilder builder) {
  return (controlType == typeof(HtmlHead) 
    || controlType == typeof(HtmlTitle)
    || controlType == typeof(ContentPlaceHolder)
    || controlType == typeof(Content)
    || controlType == typeof(HtmlLink));
}
```

This will block adding any control except for those necessary in
creating a typical view. You can imagine later adding some easy way of
configuring that list in case you do later allow other controls.

Once we’ve implemented this new filter, we can edit the Web.config file
within the Views directory to set the parser filter to this one.

This is a powerful tool for hooking into the parsing of a web page, so
do be careful with it. As you might expect, I have a **[very simple demo
of this feature
here](https://haacked.com/code/PageParseFilterDemo.zip "PageParserFilter Demo")**.

