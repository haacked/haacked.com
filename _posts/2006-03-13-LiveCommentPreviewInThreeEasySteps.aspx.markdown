---
layout: post
title: "Live Comment Preview In Three Easy Steps"
date: 2006-03-13 -0800
comments: true
disqus_identifier: 12072
categories: []
---
Several people have complimented the live comment preview used in my
skin. Try leaving a comment and notice the preview mode underneath. It
now even supports a few HTML tags. Unfortunately I haven’t updated the
comment page to tell you which tags are supported. Doh!

I did not write the original script. It was borrowed from the [Asual
Theme for
blojsom](http://wiki.blojsom.com/wiki/display/blojsom/Available+Themes "Asual Theme")
and used in our Piyo skin.

However as I like to do, I spent a little bit of time trying to improve
the script and turn it into a [Markup Based Javascript Effect
Library](http://weblogs.asp.net/jgalloway/archive/2006/01/18/435857.aspx "Using Markup Base Javascript Effect Libraries").

Now, by simply [referencing this
script](http://haacked.com/code/LiveCommentPreview.zip "Live Comment Preview Script"),
you can add live comment preview to any blog in three easy steps.

1.  Reference the script.
2.  Add the CSS class `livepreview` to a `TextArea`
3.  Add the CSS class `livepreview` to a `div`

The `textarea` is of course the form input into which the user enters a
comment. In ASP.NET it would be a `TextBox` control with the `TextMode`
property set to `MultiLine` like so:

\<asp:TextBox id="tbComment" runat="server"\
 Rows="10" Columns="40" width="100%" Height="193px"\
TextMode="MultiLine" class="livepreview"\>\</asp:TextBox\>

The `<div>` is the tag used to display the preview. There is a good
reason to choose a div as opposed to allowing a `<p>` which I will talk
about later. In my blog, that `div` already had a CSS class applied so I
simply added the `livepreview` class like so:

```csharp
<div class="commentText livepreview"></div>
```

And that’s it!

Well not exactly. I fibbed just slightly. There is actually a fourth
step for the discriminating blog author. If you crack open the script,
you’ll notice the following section on top:

    var subtextAllowedHtmlTags = new Array(7);
    subtextAllowedHtmlTags[0] = 'a';
    subtextAllowedHtmlTags[1] = 'b';
    subtextAllowedHtmlTags[2] = 'strong';
    subtextAllowedHtmlTags[3] = 'blockquote';
    subtextAllowedHtmlTags[4] = 'i';
    subtextAllowedHtmlTags[5] = 'em';
    subtextAllowedHtmlTags[6] = 'u';

In the next version of
[Subtext](http://subtextproject.com/ "Subtext Website"), that snippet is
actually generated within an ASP.NET page (specifically DTP.aspx) as it
is a list of HTML tags allowed by the blog engine. Since this is
configured on the server, I needed some easy way to pass that
information to the javascript. I chose to dynamically render javascript.
I could have used an AJAX approach, but why bother at this point?

You can edit that array to specify your own tags. Note the preview only
currently renders tags that contain something between a start and end
tag. So for example, `<b></b>` won’t show up, but `<b>Text</b>` will.

For example if you add `hr` to your list of allowed tags, `<hr />` won’t
get rendered properly in the live comment preview. It will get rendered
properly when it is actually posted as a comment. This may change in a
future release.

Now it is up to you to apply some CSS styling to actually make the
preview look good.

