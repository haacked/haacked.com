---
title: Put Your Views (and Pages) On a Diet
tags: [aspnetmvc]
redirect_from: "/archive/2009/08/03/views-on-a-diet.aspx/"
---

One of the complaints I often here with our our default view engine and Pages is that there’s all this extra cruft in there with the whole page directive and stuff. But it turns out that you can get rid of a lot of it. Credit goes to [David Ebbo](http://blogs.msdn.com/davidebb/ "David Ebbo"), the oracle of all hidden gems within the inner workings of ASP.NET, for pointing me in the right direction on this.

First, let me show you what the before and after of our default *Index* view (*reformatted to fit the format for this blog*).

## Before

```aspx-cs
<%@ Page Language="C#" 
  MasterPageFile="~/Views/Shared/Site.Master" 
  Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" 
  ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" 
  ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" 
        title="ASP.NET MVC Website">http://asp.net/mvc</a>.
    </p>
</asp:Content>
```

## After

```aspx-cs
<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" 
        title="ASP.NET MVC Website">http://asp.net/mvc</a>.
    </p>
</asp:Content>
```

That ain’t your pappy’s Web Form view. I can see your reaction now:

> *Where’s the page declaration!? Where’s all the Content IDs!? Where’s
> the Master Page declaration!? Oh good, at least runat="server" is
> still there to anchor my sanity and comfort me at night.*

It turns out that ASP.NET provides ways to set many of the defaults within Web.config. What I’ve done here (and which you can do in an ASP.NET MVC project or Web Forms project) is to set several of these defaults.

In the case of ASP.NET MVC, I opened up the *Web.config* file hiding away in the *Views* directory, not to be confused with the *Web.config* in your application root.

![views-webconfig](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/PutYourViewsandPagesOnaDiet_F0BD/views-webconfig_3.png "views-webconfig")

This *Web.config* is placed here because it is the default for all
Views. I then made the following changes:

1. Set the `compilation` element’s `defaultLanguage` attribute to “C#”.
2.  Set the `pages` element’s `masterPageFile` attribute to point to `~/Views/Shared/Site.master`.
3.  Set the `pages` element’s `pageBaseType` attribute to `System.Web.Mvc.ViewPage` (in this case, it was already set as part of the default ASP.NET MVC project template).

Below is what the *web.config* file looks like with my changes (*I
removed some details like other elements and attributes just to show the
gist*):

```xml
<configuration>
  <system.web>
    <compilation defaultLanguage="C#" />
    <pages
        masterPageFile="~/Views/Shared/Site.master"
        pageBaseType="System.Web.Mvc.ViewPage"
        userControlBaseType="System.Web.Mvc.ViewUserControl">
    </pages>
  </system.web>
</configuration>
```

With this in place, as long as my views don’t deviate from these settings, I won’t have to declare the *Page* directive.

Of course, if you’re using strongly typed views, you’ll need the *Page* directive to specify the `ViewPage` type, but that’s it.

Also, don’t forget that you can get rid of all them ugly `Register` declarations by [registering custom controls in Web.config](2006-11-14-register_custom_controls_in_web.config.aspx "Register custom controls").

You can also get rid of those ugly Import directives by importing namespaces in *Web.config*.

```xml
<configuration>
  <system.web>
    <pages>
      <namespaces>
        <add namespace="Haack.Mvc.Helpers" />
      </namespaces>
    </pages>
  </system.web>
</configuration>
```

By following these techniques, you can get rid of a *lot* of cruft within your pages and views and keep them slimmer and fitter. Of course, what I’ve shown here is merely putting your views on a syntax diet. The more important diet for your views is to keep the amount of code minimal and restricted to presentation concerns, but that’s a post for another day as Rob [has already covered it](http://blog.wekeroad.com/blog/asp-net-mvc-avoiding-tag-soup/ "Avoiding Tag Soup").

Sadly, there is no getting rid of `runat="server"` yet short of switching [another view engine](https://haacked.com/archive/2008/12/08/asp.net-mvc-northwind-demo-using-the-spark-view-engine.aspx "Northwind on Spark"). But at this point, I like to think of him as that obnoxious friend from high school your wife hates but you still keep around to remind you of your roots. ;)

Hope you enjoy these tips and use them to put your views on a diet. After all, isn’t it the view’s job to look good for the beach?
