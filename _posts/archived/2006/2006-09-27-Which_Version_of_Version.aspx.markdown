---
title: Which Version of Version?
date: 2006-09-27 -0800
tags: []
redirect_from: "/archive/2006/09/26/Which_Version_of_Version.aspx/"
---

As developers, I think we tend to take the definition of *Version* for
granted.  What are the components of a version?  Well that's easy, it
is:

> Major.Minor.Build.Revision

Where the Build and Revision numbers are optional.  At least that is the
definition given my the MSDN documentation for the [`Version`
class](http://msdn2.microsoft.com/en-us/library/system.version.aspx "Version Class Documentation").

But look up Version in Wikipedia and you get a [different
answer](http://en.wikipedia.org/wiki/Version#Numeric "Numeric Version on Wikipedia").

> The most common software versioning scheme is a scheme in which
> different major releases of the software each receive a unique
> numerical identifier. This is typically expressed as three numbers,
> separated by periods, such as version 2.4.13. One very commonly
> followed structure for these numbers is:
>
> *major.minor[.revision[.build]]*
>
> or
>
> *major.minor[.maintenance[.build]]*

Notice that this scheme differs from the Microsoft scheme in that it
places the **build** number at the very end, rather than the revision
number.

Other versioning schemes such as the [Unicode
Standard](http://unicode.org/versions/ "Unicode Versioning") and
[Solaris/Linux](http://www.usenix.org/publications/library/proceedings/als00/2000papers/papers/full_papers/browndavid/browndavid_html/ "Library Interface Versioning")
figure that three components is enough for a version with Major, Minor,
and Update (for Unicode Standard) or Micro (for Solaris/Linux).

According to the MSDN documentation, the build number represents a
recompilation of the same source, so it seems to me that it belongs at
the end of the version, as it is the least significant element.

In Subtext, we roughly view the version as follows, though it is not set
in stone:

-   *Major*: Major update.  If a library assembly, probably not
    backwards compatible with older clients.  This would include major
    changes. Most likely will include database schema changes and
    interface changes.
-   *Minor*: Minor change, may introduce new features, but backwards
    compatibility is mostly retained.  Likely will include schema
    changes.
-   *Revision*: Minor bug fixes, no significant new features
    implemented, though a few small improvements may be included.  May
    include a schema change.
-   *Build*: A recompilation of the code in progress towards a
    revision.  No schema changes.

Internally, we may have schema changes between build increments, but
when we are prepared to release, a schema change between releases would
require a revision (or higher) increment.

I know some developers like to embed the date and counter in the build
number.  For example, *20060927002* would represent compilation \#2 on
September 27, 2006.

What versioning schemes are you fans of and why?

