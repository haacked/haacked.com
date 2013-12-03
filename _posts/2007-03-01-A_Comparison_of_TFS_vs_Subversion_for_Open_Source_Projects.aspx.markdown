---
layout: post
title: "A Comparison of TFS vs Subversion for Open Source Projects"
date: 2007-03-01 -0800
comments: true
disqus_identifier: 18222
categories: []
---
We’ve been having an internal debate within the
[Subtext](http://subtextproject.com/ "Subtext Project Website") mailing
list over the merits of
[SourceForge](http://sourceforge.net/ "SourceForge") vs [Google Code
Project Hosting](http://code.google.com/ "Google Code") vs
[Codeplex](http://www.codeplex.com/ "CodePlex"). Much of the discussion
hinges around the benefits of Subversion for Open Source projects when
compared to Team Foundation System (TFS).

Before I begin, I do not mean for this to devolve into a religious
argument. This is merely my critique from the perspective of running an
Open Source project. I personally think both are fine products and both
probably work equally well in the corporate environment.

### TFS Advantages

-   **Easy of use.** For developers with a background in using Visual
    Source Safe or Sourcegear Vault, the interface into TFS will be
    familiar. Subversion requires more of a learning curve for these
    developers, though this is mitigated by my suspicion that a large
    percentage of Open Source developers tend to use CVS and SVN
    already. 
-   **Work Item integration is sweet.** I’ve been contributing some code
    to the Subsonic project and I actually love the work item
    integration in VS.NET. It’s pretty nice to be able to review and
    close work items while working on the code.
-   **Shelving is great.** Certainly nothing stops you from doing
    something like this in Subversion by using conventions, but I like
    the ~~syntactic~~ workflow sugar this provides.

### Subversion Advantages

-   **Anonymous access.** Users who want to look at the code, view the
    change history of the code, and update their local code to the
    latest version can do so form the convenience of their favorite
    Subversion client. This is much more cumbersome with TFS.
-   **Patch Submission.** This goes hand in hand with anonymous access.
    Users without commit access can have Subversion generate patch files
    consisting of their changes and submit these files. **This makes it
    really easy for the casual contributor to quickly submit a patch as
    well as makes it easy for the Open Source development team to apply
    contributions to the source. This is a huge benefit to the
    project.** Unfortunately with CodePlex, you either give commit
    access or you don’t. If you don’t, it’s a pain for users to submit
    patches and a pain for the project team to apply patches. Just ask
    [Rob Conery](http://blog.wekeroad.com/ "Rob Conery") what happens if
    you give commit access too freely.
-   **Offline Support.** Regardless of what [Jeff
    says](http://www.codinghorror.com/blog/archives/000787.html "Does Offlien Mode Still Matter"),
    offline mode does matter for many applications. For example,
    sometimes I have to connect to an obnoxious VPN that destroys my
    general internet connectivity. It’s nice to be able to connect, get
    latest, disconnect, work, connect, commit changes, disconnect. Try
    that with TFS.

Again, as source control systems, I believe they are both great systems.
But for the needs of an open source project, I feel that Subversion has
advantages. **As far as I understand, TFS was designed as an enterprise
source control system. However, the needs of the enterprise are often
different from the needs of an Open Source team.**

**Subversion, itself open source, was used during its own development
(when it became stable enough). So it is well suited to open source
development.**

If Codeplex supported Subversion, I would probably want to move Subtext
over in a heartbeat. If you feel the same way I do, please vote for the
work item entitled [Subversion Support
(SVN)](http://www.codeplex.com/CodePlex/WorkItem/View.aspx?WorkItemId=7082 "Subversion Support").

It looks like a lot of people would like to see this as well as it is
the top vote getter on the [Codeplex work item
site](http://www.codeplex.com/CodePlex/WorkItem/List.aspx "Codeplex work items").

And before you rail on me asking, *Why Microsoft would ever consider
such a move? Isn't Codeplex a showcase for TFS and Microsoft Technology
Open Source projects?*

A member of the Codeplex team informed me that **Codeplex is the home
for any Open Source project - on any and all platforms**. In fact, they
do now host a few non-Microsoft projects. Of course their dependency on
TFS does naturally limit the types of projects that would host there.

