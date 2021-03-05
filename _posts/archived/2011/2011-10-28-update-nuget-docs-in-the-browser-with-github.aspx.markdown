---
title: Update NuGet Docs in the Browser with Github
tags: [nuget,oss]
redirect_from: "/archive/2011/10/27/update-nuget-docs-in-the-browser-with-github.aspx/"
---

We made a recent change to make it easy to update the [NuGet
documentation](http://docs.nuget.org/ "NuGet Documentation Site"). In
this post, I’ll cover what the change was, why we made it, and how it
makes it easier to contribute to our documentation.

Our docs run as a simple [ASP.NET Web
Pages](http://www.asp.net/web-pages "ASP.NET Web Pages") application
that renders documentation written in the
[Markdown](http://daringfireball.net/projects/markdown/ "Markdown")
format. The Markdown text is not stored in a database, but live as files
that are part of the application source code. That allows us to use
source control to version our docs.

We used to host the source for the docs site in Mercurial (hg) on
[CodePlex.com](http://codeplex.com/ "CodePlex"). Under the old system,
it took the following to contribute docs.

1.  Install Mercurial (TortoiseHG for example) if you didn’t already
    have it.
2.  Fork our repository and clone it to your local machine.
3.  Open it up in Visual Studio.
4.  Make and commit your changes.
5.  Push your changes.
6.  Send a pull request.

It’s no surprise that we don’t get a lot of pull requests for our
documentation. Oh, and I didn’t even mention all the steps once we
received such a pull request.

As anyone who’s ever run an open source product knows, it’s hard enough
to get folks to contribute to documentation in the first place. Why add
more roadblocks?

To improve this situation, we moved [our documentation repository to
Github](https://github.com/NuGet/NuGetDocs "NuGet Docs on Github") for
three reasons:

1.  In-browser editing of files with MarkDown preview.
2.  Pull requests can be merged at the click of a button.
3.  Support for [deploying to
    AppHarbor](https://github.com/blog/961-deploy-to-appharbor-from-github "Deploy to AppHarbor from Github")
    (which CodePlex [also
    has](http://blogs.msdn.com/b/codeplex/archive/2011/08/26/integration-with-appharbor.aspx "CodePlex Integration with AppHarbor"))

With this in place, it’s now easy to be a “drive-by” contributor to our
docs. Let’s walk through an example to see what I mean. In this example,
I’m posing as a guy named “FakeHaacked” with no commit access to the
NuGetDocs repository.

Here’s a sample page from our docs (click for larger). The words at the
end of the first paragraph should be links! Doh! I should fix that.

[![nuget-docs-page](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/nuget-docs-page_thumb.png "nuget-docs-page")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/nuget-docs-page_2.png)

First, I’ll visit the [NuGet Docs
repository](https://github.com/NuGet/NuGetDocs "NuGet Docs") (TODO: Add
a link to each page with the path to the Github repository page).

[![nuget-docs-github](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/nuget-docs-github_thumb.png "nuget-docs-github")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/nuget-docs-github_2.png)

Cool, I’m logged in as **FakeHaacked**. Now I just need to navigate to
the page that needs the correction. All of the documentation pages are
in the **site** folder.

Pro tip, type the letter “t” while on this page to use incremental
search to search for the page you want to edit.

Here’s the page I want to edit.

[![page-with-fork-this-button](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/page-with-fork-this-button_thumb.png "page-with-fork-this-button")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/page-with-fork-this-button_2.png)

Since this file is a Markdown file, I can see a view of the file that’s
a nice approximation of what it will look like when it’s deployed. It’s
not exactly the same since we have different CSS styles on the
production site.

See that blue button just above the content and to the right? That
allows me to “fork” this page and edit the file. Forking it, for those
not familiar with distributed version control, means it will create a
clone of the main repository. I’m free to work and do whatever I want in
that clone without worry that I will affect the real thing.

Let’s click that button and see what happens.

[![github-markdown-editor](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/github-markdown-editor_thumb.png "github-markdown-editor")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/github-markdown-editor_2.png)

Cool, I get a nice editor with preview for editing the page right here
in the browser. I’ll go ahead and make those last two references into
Markdown formatted links.

When I’m done, I can scroll down, type in a commit message describing
the change, and click the blue *Propose File Change* button.

[![enter-commit-message](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/enter-commit-message_thumb_1.png "enter-commit-message")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/enter-commit-message_4.png)

Once you’re happy with the set of changes you’ve made, click the button
to send a pull request. This lets the folks who maintain the
documentation to know you have changes that are ready for them to pull
in.

[![send-a-pull-request](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/send-a-pull-request_thumb.png "send-a-pull-request")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/send-a-pull-request_2.png)

And that’s it. You’ve done your part. Thank you for your contribution to
our docs! Now let’s look at what happens on the other side. I’ll put on
my project maintainer hat and visit the site. Notice I’m logged in as
**Haacked** now and I see there’s an outstanding pull
request.[![pull-requests-nuget-docs](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/pull-requests-nuget-docs_thumb.png "pull-requests-nuget-docs")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/pull-requests-nuget-docs_2.png)

Cool, I can take a look at it, quickly see a diff, and comment on it.
Notice that Github was able to determine that this file is safe to
automatically merge into the master branch.

[![merge-pull-request](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/merge-pull-request_thumb_1.png "merge-pull-request")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/merge-pull-request_4.png)

All I have to do is click the big blue button, enter a message, and I’m
done!

[![confirm-merge](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/confirm-merge_thumb.png "confirm-merge")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Update-NuGet-Docs-in-the-Browser-with-Gi_D96F/confirm-merge_2.png)

It’s that easy for me to merge in your changes.

Summary
-------

You might ask why we don’t use the [Github
Pages](http://pages.github.com/ "Github Pages") feature (or even
[Git-backed
wikis](https://github.com/blog/699-making-github-more-open-git-backed-wikis "Git-backed wikis")).
We started the docs site before we were on Github and didn’t know about
the pages feature.

If I were to start over, I’d probably just use that. Maybe we’ll migrate
in the future. One benefit of our current implementation is we get that
nice Table of Contents widget generated for us dynamically (which we can
probably do with [Github Pages and
Jekyll](https://github.com/mojombo/jekyll "Jekyll")) and we can use
Razor for our layout template.

The downside of our current approach is that we can’t create new doc
pages this way, but I’ll submit a feature request to the Github team and
see what happens.

So if you are reading the NuGet docs, and see something that makes you
think, “That ain’t right!”, please go fix it! It’s easy and contributing
to open source documentation makes you a good person. It’s how I got
started in open source.

Oh, and if you happen to be experienced with Git, you can always use the
traditional method of cloning the repository to your local machine and
making changes. That gives you the benefit of running the site to look
at your change.

