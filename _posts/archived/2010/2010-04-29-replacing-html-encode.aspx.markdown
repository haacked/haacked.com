---
title: 'Tip: Replacing Html.Encode Calls With New Html Encoding Syntax'
tags: [aspnetmvc,aspnet,code]
redirect_from: "/archive/2010/04/28/replacing-html-encode.aspx/"
---

Like the well disciplined secure developer that you are, when you built
your ASP.NET MVC 1.0 application, you remembered to call `Html.Encode`
every time you output a value that came from user input. Didn’t you?

Well, in ASP.NET MVC 2 running on ASP.NET 4, those calls can be replaced
with the new HTML encoding syntax (aka code nugget). I’ve written a
three part series on the topic.

-   [Html Encoding Code Blocks With ASP.NET
    4](https://haacked.com/archive/2009/09/25/html-encoding-code-nuggets.aspx "Html encoding code nugget")
-   [Html Encoding Nuggets With ASP.NET MVC
    2](https://haacked.com/archive/2009/11/03/html-encoding-nuggets-aspnetmvc2.aspx "Html Encoding Nuggets with ASP.NET MVC 2")
-   [Using AntiXss as the default encoder for
    ASP.NET](https://haacked.com/archive/2010/04/06/using-antixss-as-the-default-encoder-for-asp-net.aspx "Using AntiXSS")

But dang, going through all your source files cleaning up these calls is
a pretty big pain. Don’t worry, I have your back. Just bring up the Find
an Replace dialog (`CTRL + SHIFT + H`) and expand the *Find options*
section and check the checkbox labeled *Use* and make sure *Regular
expressions*is selected.

Then enter the following in the *Find what* textbox.

`\<\%:b*=:b*Html.Encode\({[^%]*}\):b*\%\>`

And enter the following in the *Replace with* textbox.

`   `

\<%: \\1 %\>

Here’s a screenshot of what the dialog should look like (though yours
won’t have the red box :P).

[![find-and-replace](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UsefulMVC2UpgradeTip_EC18/find-and-replace_thumb.png "find-and-replace")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UsefulMVC2UpgradeTip_EC18/find-and-replace_2.png)Note
that this regular expression I’m giving you is not foolproof. There are
some very rare edge cases where it might not work, but for the vast
majority of cases, it should work fine. At least, it works on my
machine!

![works-on-my-machine](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UsefulMVC2UpgradeTip_EC18/works-on-my-machine_3.png "works-on-my-machine")

Now that I’m finally done with updates to [Professional ASP.NET MVC
2](http://www.amazon.com/gp/product/0470643188?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=390957&creativeASIN=0470643188 "Professional ASP.NET MVC 2 at Amazon"),
I hope to get back to my regular blogging schedule. This will be only my
third blog post this month, a new record low! And I love to blog! It’s
been a busy past few months.

