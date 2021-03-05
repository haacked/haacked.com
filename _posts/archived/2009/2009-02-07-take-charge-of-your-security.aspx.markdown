---
title: Take Charge of Your Security
redirect_from:
- "/archive/0001/01/01/take-charge-of-your-security.aspx/"
- "/archive/2009/02/06/take-charge-of-your-security.aspx/"
tags: [aspnetmvc,aspnet,code]
---

Today I read something where someone was comparing Web Forms to [ASP.NET
MVC](http://asp.net/mvc "ASP.NET Website") and suggested that Web Forms
does a lot more than ASP.NET MVC to protect your site from malicious
attacks.

One example cited was that Server controls automatically handled HTML
encoding so you don’t have to really think about it. The idea here is
that Web Forms automatically protects you from XSS attacks.

My friends, I’m afraid this is just not true. Take a look at the
following page code.

```csharp
<%@ Page Language="C#" Inherits="System.Web.UI.Page" %>
<%
//For demo purposes, we have inline code here.
// Pretend the following userInput came from the database
string userInput = "<script>alert('You’ve been Haacked!');</script>";
label1.Text = userInput;
literal1.Text = userInput;
%>

<html>
<head>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="label1" runat="server" />
        <asp:Literal ID="literal1" runat="server" />
    </div>
    </form>
</body>
</html>
```

In this page, we simulate taking some some user input we got from
somewhere, whether it’s from a form post or from a database, and we
place it in a `Label` control, and in a `Literal` control.

These are two of the most common controls used to display user input.
I’ll save you the suspense of having to actually try it out, and just
show you what happens when run this page.

![Message from
webpage](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TakeChargeofYourSecurity_E67F/message-from-webpage-3.png "Message from webpage")

Contrary to popular belief, these controls do not automatically HTML
encode their output. I don’t see this as some gaping security flaw
because it may well be that the intention of these controls, and general
usage, is to display HTML markup the developer specifies, not what the
user specifies. The potential security flaw lies in using these controls
without understanding what they actually do.

The lesson here is that **you** **always have to think about security**.
There’s no silver bullet. There’s no panacea. Damien Guard has a [great
post where he lists other
signs](http://damieng.com/blog/2007/12/18/5-signs-your-aspnet-application-may-be-vulnerable-to-html-injection "Signs your application is vulnerable")
your ASP.NET application might be succeptible to injection attacks,
pointing out various ways that protection is not automatic.

The best approach is to take a more holistic approach. Create a threat
model for your website and start attacking your own site as if you were
a hacker, looking for flaws. Conduct security reviews and use any
automated tools you can find for finding potential flaws. [I recommend
taking a look at CAT.NET combined with the AntiXss
library](http://blogs.msdn.com/cisg/archive/2008/12/15/anti-xss-3-0-beta-and-cat-net-community-technology-preview-now-live.aspx "CAT.NET and AntiXSS library").

In this *particular* case, I don’t think Web Forms provides any more
automatic security than ASP.NET MVC. With MVC, we’ve swapped server
controls with our helper methods, which properly encode output. If you
don’t use our helpers, it’s roughly equivalent to not using the server
controls.

Interestingly enough, in order to get that particular user input to the
page in the first place is tricky. If you were to create a Web Form with
a text input and a button, and type that script tag into text box and
click the button, you’d be greeted by the following yellow screen of
death.

![request-validation](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TakeChargeofYourSecurity_E67F/request-validation_thumb.png "request-validation")

By default, [ASP.NET has Request
Validation](http://www.asp.net/learn/whitepapers/request-validation/ "Request Validation")
turned on, which prevents requests with suspicious looking data such as
the one I tried. Note that ASP.NET MVC *also* has Request Validation
turned on by default too. You can turn it off per Controller or Action
via the `ValidateRequestAttribute` like so.

```csharp
[ValidateInput(false)]
public ActionResult SomeAction(string someInput) {
}
```

This is not to say that I think ASP.NET MVC provides just as much
automatic protection that Web Forms does. This is not exactly the case.
There are some cases where Web Forms does provide more automatic
protection that ASP.NET MVC leaves to you, the developer.

For example, ASP.NET MVC does not have an automatic equivalent of [Event
Validation](http://odetocode.com/Blogs/scott/archive/2006/03/20/3145.aspx "Event Validation")
which was introduced in ASP.NET 2.0. Note that event validation is very
different from request validation and is very specific in to server
controls. For example, as the blog post I linked to mentions, if you add
a `DropDownList` control with three options, and a user posts a
non-existent option, you will get an exception. ASP.NET MVC doesn’t have
such automatic validation. In some cases, this is a good thing because
it makes AJAX scenarios simpler.

What ASP.NET MVC does have is a set of [Anti Forgery (CSRF)
helpers](http://blog.codeville.net/2008/09/01/prevent-cross-site-request-forgery-csrf-using-aspnet-mvcs-antiforgerytoken-helper/ "AntiForgery Helpers")
which require a bit of manual intervention.

To recap, while I do agree that Web Forms does provide a bit more
automatic security than ASP.NET MVC, the gap is not as wide as you might
think. Server controls are no more nor less secure than using the
Helpers with ASP.NET MVC. And all of that is irrelevant because it is
still up to the developer’s to take responsibility for the security of
his or her site. I’ve heard of many developers who had to turn off
Request and Event Validation for various reasons. In those cases, they
examined the attack vectors opened up by these changes and provided
alternate protections to replace the ones they turned off.
