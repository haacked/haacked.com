---
title: Subtext Roadmap
tags: [subtext]
redirect_from: "/archive/2005/05/03/subtext-roadmap.aspx/"
---

This document describes the goals for future versions of Subtext as well
as a plan for achieving them. The goals for this roadmap are the
following:

-   Communicate to end users what features are planned for future
    releases
-   Elicit feedback from users about upcoming releases
-   Provides a prioritization of features

This document is a work in progress and feedback is welcome.

Administrative Road Map
-----------------------

1.  Documenting existing source code and features. (priority: high)
2.  Fill specific project roles (patch manager, forum manager, etc...)
    (priority: high)
3.  Set up a website and Wiki for Subtext (unfortunately subtext.com is
    taken). (priority: med)
4.  Set up an automated build process (NAnt) (priority: low)

Upcoming Releases
-----------------

As we flesh out the roadmap, we’ll divide it into sections based on
planned future individual releases. For now, this document will simply
list goals and features planned for the near and far future.

### Gotta Have It Features Immediately (priority 1)

These features will directly support the principles of the Subtext
project. *UPDATE: We are rethinking the single vs multiple blog support.
More details later.* One important "feature" that must be discussed is
the dropping of the "multiple blogs on one installation" feature. In
order to maintain Subtext’s goals of simplicity and it’s focus on the
hobbyist and individual blogger, it makes sense to focus on the scenario
where users are using Subtext to create a single blog. This will
distinguish Subtext from Community Server which is geared towards
corporations and groups that wish to host multiple blogs. Please provide
feedback on this decision.

-   **Installer for local setup**: We’ve started an installer using the
    WiX toolkit. Initially, this will be an MSI package that will
    install both a website and the database when run locally.
    Eventually, it will have to be able to upgrade an existing
    installation.
-   **Simplified configuration (single blog)**: By removing the multiple
    blogs feature, configuration can be simplified immensely.
-   **Configuration utility**: Upon first installing Subtext, the
    configuration utility will be an easy to use WinForms app used to
    set the connection string (and certain other settings if any) within
    the web.config file. This utility can be run at any time to tweak
    web.config settings without having to muck around the XML by hand.
-   **Kick ass documentation**: Can’t stress this enough. We’ll use NDoc
    to generate code and API documentation. As for user documentation,
    we’ll have both a project site and a wiki.
-   **Comments Automatically Expire**: This is currently hard-coded into
    Subtext and needs to be made configurable. Allow the user to have
    comments turned off after a configurable number of days. Existing
    comments will still be displayed, but no new comments will be
    allowed.

### Gotta Have It, But Just not yet (priority 1.5)

-   **Friendly Urls**: Currently, Subtext creates permalinks that look
    like
    [https://haacked.com/archive/2005/05/04/2953.aspx](https://haacked.com/archive/2005/05/04/2953.aspx).
    In a future version, we want the permalink to have a more human
    readable URL. For example, this might be converted to
    https://haacked.com/archive/2005/05/04/AnnouncingSubtext.aspx.
-   **Improved Usability**: One of my pet peeves about .TEXT is how hard
    it is to edit a really old post. You have to page through the data
    grid of posts till you find it. Instead, a simple option is to
    create a new admin token that skin creators can place in their skin
    where a post is rendered. When a user is logged in as an admin, the
    token is displayed as an icon with a link that the admin can click
    to edit that post. Thus, to edit an old post, simply make sure
    you’re logged in as an admin and leverage Google to find the post,
    and then click on the admin token.
-   **Replace/Upgrade FreeTextBox.dll**: Hopefully with something that
    won’t mangle HTML.
-   **Comment Moderation**: This is merely one tool in the constant
    battle to fight comment spam. Allows users to turn on and off
    comment moderation.
-   **Simple Comment Filtering Rules** Currently, haacked.com uses a
    simple trigger that filters out comments with a certain number of
    links. This exceedingly simple filter does remarkably well. To fight
    comment spam, we should start with a few simple (and configurable)
    rules for filtering comment spam. We can add more complex rules
    later.

### Important, But Maybe Next Release Features (priority 2)

-   **Membership [Provider
    Model](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnaspnet/html/asp02182004.asp)
    implementation**: This will be a very simple system that allows a
    blog owner to create accounts with certain roles (reader, author,
    admin). Thus a blog can have multiple authors for a single blog.
-   **New CSS based Templates**: These will be templates that can be
    "skinned" purely via CSS (ala [CSS Zen
    Garden](http://www.csszengarden.com/)). We’ll provide a tool for a
    blog owner to edit and switch CSS for this particular template.
-   **XHTML compliance**: Both transitional and strict.
-   **Comment Filtering Rules Engine**: This will be similar to the Junk
    Mail rules engine in Outlook. We’ll provide a web based interface
    for creating filtering rule used to combat comment spam.

### Features to dream about (priority we’re dreaming)

-   **A Spell Checker**: For all those bad spelers out there.
-   **Migration utility**: We’re not so arrogant as to believe you’ll
    never use another blogging engine again. If you do, we want to help
    you migrate your permalinks and posts to it.
-   **MySql Provider**: because not everyone wants to pay for SQL Server
    hosting and some people want to honor their license agreement for
    MSDN Universal. ;)
-   **Mono support**: This may be way down the road, but supporting Mono
    would be a nice way to introduce the Linux crowd to the beauty of
    ASP.NET and Subtext. Besides, we’ll finally get props from the
    Slashdot crowd for our
    [1337](http://www.urbandictionary.com/define.php?term=1337) sk1llz.
-   **Intelligent comment filtering**: Whether it be via Bayes filtering
    or some other means, but a more autonomous method of spam filtering
    is called for.

