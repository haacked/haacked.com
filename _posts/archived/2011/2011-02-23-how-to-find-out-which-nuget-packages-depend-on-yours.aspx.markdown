---
title: How To Find Out Which NuGet Packages Depend on Yours
date: 2011-02-23 -0800 9:00 AM
tags: [nuget,aspnet,aspnetmvc,code]
redirect_from: "/archive/2011/02/22/how-to-find-out-which-nuget-packages-depend-on-yours.aspx/"
---

Renaming a package ID is a potentially destructive action and one we
don’t recommend doing. Why? Well if any other packages depend on your
package, you’ve effectively broken them if you change your package ID.

For example, today I *wanted* to rename a poorly named package,
*MicrosoftWebMvc*, to *Mvc2Futures*. What I ended up doing is recreating
the same package with the new ID and uploading it. That way existing
packages that depend on *MicrosoftWebMvc* aren’t broken.

But now, I have two packages that have the same functionality, but
different IDs. Wouldn’t it be nice to eventually remove the old one? I
guess I could **if I knew that no other package had a dependency on
it**.

This is where the benefit of having an OData service over the packages
in the gallery comes in quite useful. It allows us to construct ad-hoc
queries we hadn’t accounted for in our API via an URL. Here’s the URL
that shows me a list of all packages that depend on *MicrosoftWebMvc*.

[http://packages.nuget.org/v1/FeedService.svc/Packages?\$filter=substringof('MicrosoftWebMvc',%20Dependencies)%20eq%20true&\$select=Id,Dependencies](http://packages.nuget.org/v1/FeedService.svc/Packages?$filter=substringof('MicrosoftWebMvc',%20Dependencies)%20eq%20true&$select=Id,Dependencies "http://packages.nuget.org/v1/FeedService.svc/Packages?$filter=substringof('MicrosoftWebMvc',%20Dependencies)%20eq%20true&$select=Id,Dependencies")

Notice that we’re searching the *Dependencies* node for the substring
“MicrosoftWebMvc” anywhere in it. If my package ID was “web”, this would
not be a good query to run, so you might need to tweak it for your use
case.

Also, this query only detects direct dependencies. It doesn’t detect
transitive dependencies. However, in this case, that’s good enough for
my needs.

With this list in hand, I can now approach the *MvcContrib* folks (who
are the only ones that depend on it), and suggest they update their
existing packages in place to point to the one with the new ID.

If they do this, am I safe to delete *MicrosoftWebMvc*?

Not necessarily.

I really need to think twice before I remove the
*MicrosoftWebMvc*package because it’s already been downloaded 939 times.
For those users who’ve installed it into their applications, they’ll
never get updates for that package.

In this particular case, this is not a problem because we never plan to
update the *Mvc2Futures* package. But for a package that’s more widely
used and frequently updated, this would be a bigger concern.

In the meanwhile, what I will do is update *MicrosoftWebMvc* to be an
empty package that depends on the correct package. That’s probably a
good plan while I wait for packages that depend on it to update.

