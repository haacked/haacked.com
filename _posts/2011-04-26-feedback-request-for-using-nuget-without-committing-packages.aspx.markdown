---
layout: post
title: "Feedback Request for using NuGet Without Committing Packages"
date: 2011-04-26 -0800
comments: true
disqus_identifier: 18782
categories: [nuget,code,open source]
---
When installing a package into a project, NuGet creates a
*packages.config* file within the project (if it doesn’t already exist)
which is an exact record of the packages that are installed in the
project. At the same time, a folder for the package is created within
the solution level *packages* folder containing the package and its
contents.

Currently, it’s expected that the *packages* folder is committed to
source control. The reason for this is that certain files from the
package, such as assemblies, are referenced from their location in the
packages folder. The benefit of this approach is that a package that is
installed into multiple projects does not create multiple copies of the
package nor the assembly. Instead, all of the projects reference the
same assembly in one location.

If you commit the entire solution and packages folder into source
contral, and another user gets latest from source control, they are in
the same state you are in. If you omitted the the packages folder, the
project would fail to build because the referenced assembly would be
missing.

### However

This approach doesn’t work for everyone. We’ve heard from many folks
that they don’t want their packages folder to be checked into their
source control.

Fortunately, you can enable this workflow today by following David
Ebbo’s approach described in his blog post, [Using NuGet without
committing
Packages](http://blog.davidebbo.com/2011/03/using-nuget-without-committing-packages.html "Using NuGet without commiting Packages").

But in NuGet 1.4 we’re planning to make it integrated into NuGet. We
will be adding a new feature to restore any missing packages and the
*packages* folder based on the *packages.config* file in each project
when you attempt to build the project. This ensures that your
application will compile even if the packages folder is missing at the
time, which might be the case if you don’t commit it to source control.

### Requirements

We have certain requirements we plan to meet with this feature.
Primarily, it has to work in a Continuous Integration (CI Server)
scenario. So it must work both within Visual Studio when you build, but
also outside of Visual Studio when you use *msbuild.exe* to compile the
solution.

For more details, please refer to:

-   The [Spec on
    CodePlex](http://nuget.codeplex.com/wikipage?title=Enabling%20Using%20NuGet%20Without%20Checking%20In%20Packages%20Folder)
-   [The discussion thread to provide
    feedback.](http://nuget.codeplex.com/Thread/View.aspx?ThreadId=236592 "Discussion thread")

If you have feedback on the design of this feature, please provide it in
[the discussion
thread](http://nuget.codeplex.com/Thread/View.aspx?ThreadId=236592 "The disucssion thread.").
Also, do keep in mind that this next release is our first iteration to
address this scenario. We think we’ll hit the primary use cases, but we
may not get everything. But don’t worry, we’ll continue to [release
often](http://haacked.com/archive/2011/04/20/release-early-and-often.aspx "Release early, Release often")
and address scenarios that we didn’t anticipate.

Thanks for your support!

