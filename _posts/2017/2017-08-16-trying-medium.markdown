---
title: "Trying Medium"
date: 2017-08-16 -0800 2:14 PM PDT
categories: [personal blogging]
---

I started my first blog at haack.org some time in the year 2000. You can still see pieces of it [in the Internet Archive Wayback machine](https://web.archive.org/web/20010220192058/http://haack.org:80/).

![My first blog](https://user-images.githubusercontent.com/19977/29385835-0c9bdf74-828e-11e7-889a-a14a9ef2bf31.png)

You have to love this part...

![IE 4 Only yo!](https://user-images.githubusercontent.com/19977/29386286-14ebc28c-8290-11e7-97bb-578db26aaa33.PNG)

Ah, the bad old days of the internet.

Back then I could probably count the number of folks who read my blog with the fingers of one hand. Perhaps not even counting the thumb. It was just an outlet for me to share inside jokes with other friends who had their own blogs.

I started this before I knew what a weblog or blog was. I wrote this with a bespoke artisanal classic ASP (Active Server Pages without the ".NET" part. We lived like savages back then.) site I built. It was terrible. No database. Just me writing HTML for every post. I let that blog die due to neglect and didn't start blogging again [until around 2004](https://haacked.com/archive/2004/02/03/the-new-digs.aspx/).

The new blog ran on Subtext, an open source ASP.NET blog engine I ported from an older .TEXT platform. It was a real labor of love. Four years ago, I switched to hosting by blog on [GitHub Pages with Jekyll](https://haacked.com/archive/2013/12/02/dr-jekyll-and-mr-haack/).

The point of this stroll down memory lane is to say that I've always felt it was important to host my blog on something under my control with my own domain name. My blog has always been primarily an outlet for me.

When I first started, my blog was more of an online diary. I'd write about my day, movie reviews, etc. When I restarted my blog, I tended to write more technical pieces in the hopes of helping others out.

My friends who weren't programmers would ask what language my blog was written in. It was all gibberish to them. However, it was important to me that haacked.com represented the full me. One day I might write about [playing soccer against Vinnie Jones](https://haacked.com/archive/2006/12/16/Played_Soccer_Against_The_Juggernaut.aspx/) or [with Agent Coulson](https://haacked.com/archive/2005/05/16/ForTheLoveOfSoccer.aspx/). On another day I might [write about parenting](https://haacked.com/archive/2013/05/27/reflective-parenting.aspx/). And yet another day I might write about [auditing ASP.NET MVC actions](https://haacked.com/archive/2017/08/10/mvc-action-security-audit/).

The point is, I wrote what I wanted to write and didn't worry about what others wanted to read too much.

But there are consequences. After a million  posts about the intricacies of [Git aliases](https://haacked.com/archive/2014/07/28/github-flow-aliases/), it's inevitable that my friends who aren't techies got bored. And I have to say, I missed their involvement with my writing. I enjoy the interactions and feedback that came of it and I was sad that they were excluded from the blogging community I had become a part of.

### Enter Medium

When Medium first came on the scene, I ignored it. I've ignored it for a long while.

But not too long ago, my wife [started a Medium blog](https://medium.com/@akumi). I may be biased, but I think she's a beautiful writer who writes beautifully. And that got me more interested in the platform.

That lead me to learn that if you import a blog post into Medium, it sets the original post as [the canonical source](https://help.medium.com/hc/en-us/articles/217991468-SEO-and-duplicate-content) via a `link` tag. Here's an example of the `link` tag for a post I imported into Medium from haacked.com. This ensures that search engines aren't confused by multiple sources of content and sees your original blog as the ultimate authority.

```html
<link rel="canonical"
  href="https://haacked.com/archive/2017/08/16/the-moment/">
```

This alleviates my concerns about being in control of my blog. The canonical source is still haacked.com which is in a Git repository that is hosted on GitHub, but is cloned to my machine. If Medium and GitHub were to go down, I'd be sad and unemployed, but I'd have the free time available to move my blog to another host and keep it up at haacked.com.

Importing into Medium is quick and easy. Visit [https://medium.com/p/import](https://medium.com/p/import) and paste in the URL to the post you want to import. That's it!

It plucks the contents of my post without of all the extra navigation and header/footer material like magic.

So now, I'm experimenting with Medium as my blog for my non-programmer friends. When I write something that isn't deeply technical on haacked.com, I'll cross-post it to Medium. But for my [Git posts](https://haacked.com/archive/2015/06/29/git-migrate/), I'll keep those here only.

I'll revisit this idea down the road to see if it works for me. I'm curious to hear your thoughts in the comments.
