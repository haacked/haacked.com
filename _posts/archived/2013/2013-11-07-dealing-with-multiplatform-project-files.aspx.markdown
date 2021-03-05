---
title: Dealing with Multiplatform Project Files
tags: [oss,xplat,octokit]
redirect_from: "/archive/2013/11/06/dealing-with-multiplatform-project-files.aspx/"
---

[Octokit.net](https://github.com/octokit/octokit.net "Octokit.net on GitHub")
targets multiple platforms. This involves a large risk to my sanity. You
can see the general approach here in the [Octokit
directory](https://github.com/octokit/octokit.net/tree/master/Octokit "Octokit.net Octokit directory.")
of our project:

 [![octokit-projects](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/d78f9a3929bc_93D7/octokit-projects_thumb.png "octokit-projects")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/d78f9a3929bc_93D7/octokit-projects_2.png)

Mono gets a project! MonoAndroid gets a project file! Monotuch gets a
project file! **Everybody gets a project file!**

Each of these projects references the same set of `.cs` files. When I
add a file to Octokit.csproj, I have to remember to add that file to the
other four project files. As you can imagine, this is easy to forget.

It’s a real pain. So I opened up a [feature request on
FAKE](https://github.com/fsharp/FAKE/issues/216 "Multi-project file verification"),
the tool we use for our build (more on that later) and asked them for a
task that would fail the build if another project file in the same
directory was missing a file from the “source” project file. I figured
this would be something easy for F\# to handle.

The [initial
response](https://github.com/fsharp/FAKE/issues/216#issuecomment-27754167)
from the maintainer of FAKE, [Steffen
Forkman](http://www.navision-blog.de/blog/ "Steffen's blog"), was this:

> What you need is a better project system ;-)

Touché!

This problem (along with so many project file merge conflicts) would
almost completely go away with file patterns in project files. I’ve been
asking for this for a long time (*I asked the Visual Studio team for
this the day I joined Microsoft, or maybe it was the first month, I
don’t recall*). **There’s a User Voice item requesting this,**[**go vote
it
up**](http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/4512873-vs-ide-should-support-file-patterns-in-project-fil "Support file patterns")**!**
(*Also, go vote up this*[*platform restriction
issue*](http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/4494577-remove-the-platform-restriction-on-microsoft-nuget)*that’s
affecting Octokit.net as well*)

In any case, sorry to say unlimited chocolate fountains don’t exist and
I don’t have the project system I want. So let’s deal with it.

A few days later, I get [this PR to
octokit.net](https://github.com/octokit/octokit.net/pull/192). When I
ran the build, I see the following snippet in the build output.

    Running build failed.
    Error:
    System.Exception: Missing files in  D:\Octokit\Octokit-MonoAndroid.csproj:
    Clients\OrganizationMembersClient.cs

That’s telling me that somebody forgot to add the class
`OrganizationMembersClient.cs` to the `Octokit-MonoAndroid.csproj`. Wow!
Isn’t open source grand?

A big thanks to Steffen and other members of the FAKE community who
pitched in to build a small but very useful feature. In a follow-up
post, I’ll write a little bit about why we moved to using FAKE to build
Octokit.net.

**Update**

I [opened an
issue](https://github.com/octokit/octokit.net/issues/197 "Issue to generate platform project files")
to take this to the next step. Rather than just verify the project
files, I want some way to automatically modify or generate them.

**Update 2**

FAKE just got even better with the new `FixProjects task`! For now,
we’ve added this as an explicit command.

    .\build FixProjects

Over time, we may just integrate this into the Octokit.net build
directly.

