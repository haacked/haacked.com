---
title: A Caveat with NuGet Source Code Packages
tags: [git,github,code]
redirect_from: "/archive/2013/02/09/a-caveat-with-nuget-source-code-packages.aspx/"
---

The other day I needed a simple JSON parser for a thing I worked on.
Sure, I’m familiar with JSON.NET, but I wanted something I could just
compile into my project. The reason why is not important for this
discussion (*but it has to do with world domination, butterflies, and
minotaurs*).

I found the
[*SimpleJson*](http://nuget.org/packages/SimpleJson/ "SimpleJson")
package which is [also on
GitHub](https://github.com/facebook-csharp-sdk/simple-json "SimpleJson on GitHub").

*SimpleJson* takes advantage of a neat little feature of NuGet that
allows you to include source code in a package and have that code
[transformed into the appropriate
namespace](http://docs.nuget.org/docs/creating-packages/configuration-file-and-source-code-transformations "Config and Source transformations")
for the package target. Oftentimes, this is used to install sample code
or the like into a project. But *SimpleJson* uses it to distribute the
entire library.

At first glance, this is a pretty sweet way to distribute a small single
source file utility library. It gets compiled into *my* code. No binding
redirects to worry about. No worries about different versions of the
same library pulled in by dependencies. In my particular case, it was
just what I needed.

But I started to think about the implications of such an approach on a
wider scale. What if everybody did this?

The Update Problem
------------------

If such a library were used by multiple packages, it actually could
limit the consumer’s ability to update the code.

For example, suppose I have a project that installs the *SimpleJson*
package and also the *SimpleOtherStuff* package, where
*SimpleOtherStuff* has a dependency on *SimpleJson* 1.0.0 and higher.
The following diagram outlines the NuGet package dependency graph. It’s
very simple.

[![nuget-dependency-graph](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Caveats-with-Source-Only-NuGet-Packages_B2AB/nuget-dependency-graph_thumb.png "nuget-dependency-graph")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Caveats-with-Source-Only-NuGet-Packages_B2AB/nuget-dependency-graph_2.png)

Now suppose we learn that *SimpleJson* 1.0.0 has a very bad security
issue and we need to upgrade to the just released *SimpleJson* 1.1.

So we do just that. Everything should be hunky dory as we’re now using
*SimpleJson* 1.0.0 everywhere. Or are we?

[![nuget-dependency-graph-2](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Caveats-with-Source-Only-NuGet-Packages_B2AB/nuget-dependency-graph-2_thumb.png "nuget-dependency-graph-2")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Caveats-with-Source-Only-NuGet-Packages_B2AB/nuget-dependency-graph-2_2.png)

If all the references to *SimpleJson* were assembly references, we’d be
fine. But recall, it’s a source code package. Even though we upgraded it
in our application, *SimpleOtherStuff* 1.0.0 has *SimpleJson* 1.0.0
*compiled into it*.

There’s no way to upgrade *SimpleOtherStuff*’s reference other than to
wait for the package author to do it or to manually recompile it
ourselves (assuming the source is available).

You Are in Control
------------------

A guiding principle in the design of NuGet is we try and keep you, the
consumer of the packages, in control of things. Want to uninstall a
package even though other packages reference it? We’ll prevent it by
default but then offer you a `–Force` flag so you can tell NuGet, “No
really, I know what I’m doing here and am ready to face the
consequences.”

We don’t do this perfectly in every case. Pre-release packages come to
mind. But it’s a principle we try to follow.

Source code packages are interesting in that they give you more control
in one area (you have the source), but take it away in another (upgrades
are no longer complete).

Note that I’m not picking on *SimpleJson*. As I said before, I really
needed this. In fact, I contributed back with several Pull Requests. I’m
just pointing out a caveat to consider when using such packages.

Making it Better
----------------

> So yeah, be careful. There are caveats. But couldn’t we make this
> better? Well I have an idea. Ok, it’s not *my* idea but an idea that
> some of my coworkers and I have bounced around for a while.

Imagine if you could attach a Git repository to your NuGet package. When
you install the package, you could add a flag to install it as a [Git
Submodule](http://git-scm.com/book/en/Git-Tools-Submodules "Git Submodules")
rather than the normal assembly approach. Maybe it’d look like this.

`Install-Package SimpleJson –AsSource`

What this would do is initialize a submodule, and grab the source from
GitHub. Perhaps it goes further and adds the files as linked files into
your target project based on a bit of configuration in the source tree.

There’s a lot of possibilities here to flesh out. The `Upgrade-Package`
command simply run a Git update submodule command on these submodules
and do a normal update for all the other packages.

Since Microsoft recently made it clear that [Git is the future of DVCS
as far as Microsoft is
concerned](http://blogs.msdn.com/b/bharry/archive/2013/01/30/git-init-vs.aspx "Git init VS"),
maybe now is the time to think about tighter integration with NuGet.
What do you think?

At the very least, perhaps NuGet needs a better extensibility model so
we could build this support in outside of NuGet. That’s the more prudent
approach of course, but I’m not feeling so prudent today.

