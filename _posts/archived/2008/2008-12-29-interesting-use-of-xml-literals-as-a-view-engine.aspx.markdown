---
title: Interesting use of XML Literals as a View Engine
tags: [aspnet,aspnetmvc]
redirect_from: "/archive/2008/12/28/interesting-use-of-xml-literals-as-a-view-engine.aspx/"
---

[Dmitry](http://blogs.msdn.com/dmitryr/ "Dmitry"), who’s the PUM for
ASP.NET, recently wrote a blog post about an interesting approach he
took [using VB.NET XML Literals as a view
engine](http://blogs.msdn.com/dmitryr/archive/2008/12/29/asp-net-mvc-view-engine-using-vb-net-xml-literals.aspx "ASP.NET MVC View Engine using VB.NET XML Literals")
for ASP.NET MVC.

Now before you VB haters dismiss this blog post and leave, bear with me
for just a second. Dmitry and I had a conversation one day and he noted
that there are a lot of similarities between our view engine hierarchy
and and normal class hierarchies.

For example, a master page is not unlike a base class. Content
placeholders within a master page are similar to abstract methods.
Content placeholders with default content are like virtual methods. And
so on…

So he thought it would be interesting to have a class be a view rather
than a template file, and put together this VB demo. One thing he left
out is what the view class actually looks like in Visual Studio, which I
think is kinda cool (click on image for larger view).

[![VB.NET XML Literal
View](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/a5a5c5b59d6c_BB42/vb-xml-literal-view_thumb.png "VB.NET XML Literal View")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/a5a5c5b59d6c_BB42/vb-xml-literal-view_2.png)

Notice that this looks pretty similar to what you get in the default
Index.aspx view. One advantage of this approach is that you’re always
using VB rather than switching over to writing markup. So if you forget
to close a tag, for example, you get immediate compilation errors.

Another advantage is that your “view” is now compiled into the same
assembly as the rest of your code. Of course, this could also be a
disadvantage depending how you look at it.

