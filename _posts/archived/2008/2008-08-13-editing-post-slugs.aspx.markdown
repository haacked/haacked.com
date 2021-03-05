---
title: Better URLs With Subtext and Windows Live Writer
tags: [subtext,blogging]
redirect_from: "/archive/2008/08/12/editing-post-slugs.aspx/"
---

One feature of Windows Live Writer that Subtext supports is the ability
to edit your post slug? What is the URL slug associated with a blog
post? What is the URL slug?

Take a quick look in the address bar and you should notice that the URL
ends with “**editing-post-slugs**.aspx”. That bold part is the post
slug. It’s a human friendly URL portion that identifies this blog post,
as opposed to using some integer id.

For a long time, Subtext had the ability to automatically convert your
blog post title into friendlier URLs. However, as with most automatic
efforts, there are cases where it falls a bit short.

For example, suppose I started writing the following post with the
following title:

![editing-post-in-wlw](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Yallwilllikeitreally_895F/editing-post-in-wlw_3.png "editing-post-in-wlw")

When I post it, the URL ends up being a bit ugly, though Subtext does
give a good faith effort.

![ugly-url-subtext](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Yallwilllikeitreally_895F/ugly-url-subtext_3.png "ugly-url-subtext")

With Windows live writer, there’s a little double hash mark at the
bottom that you can click to expand, providing more options. In the
*Slug:* field, enter a cleaner URL.

![editing-slug-in-wlw](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Yallwilllikeitreally_895F/editing-slug-in-wlw_3.png "editing-slug-in-wlw")

Now when you publish this post, the URL will end with the slug that you
specified.

If you use the Subtext Web Admin to post, we’ve had this feature all
along in the *Advanced Options* section. It’s the *Entry Name* field
(which I think we should call *Entry Name Slug* since Slug seems to be
the standard term for this.

Of course when we come out with our MVC version, we can get rid of that
annoying *.aspx* at the end. :)
