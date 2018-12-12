---
title: Absolutely Nothing. Say It Again! (Configuration Section Handlers)
date: 2004-06-27 -0800
disqus_identifier: 699
categories: []
redirect_from: "/archive/2004/06/26/absolutely-nothing-say-it-again-configuration-section-handlers.aspx/"
---

[Ian
Griffiths](http://www.interact-sw.co.uk/iangblog/2004/06/28/configsections)
points out why he doesn't like configuration section handlers much, and
one of the primary reasons is how they require extra "cruft" in order to
tell .NET about the handler. I have to agree for the most part. I've
always wanted a \#region tag for the App.config file just to hide that
junk away, but that's pretty much a cop out.
[Cyrus](http://blogs.msdn.com/cyrusn/) points out how the C\# team [was
ambivalent](http://blogs.msdn.com/cyrusn/archive/2004/06/23/163390.aspx)
about even having a region tag in the IDE.

One good point Ian and Craig (via email) brought up is that
configuration sections are often misused. I should have stated this in
my article, but I'll state it here for the record. Perhaps I'll even use
an H2 tag and all caps. Here goes.

DO NOT STORE USER SETTINGS IN AN APPLICATION CONFIGURATION FILE!
----------------------------------------------------------------

That's not what it's there for. Remember, the application configuration
file is stored in your application's directory of the Program Files
directory (by default). If you're a proper windows logo programmer,
you'll know that the typical user should not have write access there.
Otherwise you don't deserve that shiny "[Designed forWindows
XP](http://www.microsoft.com/winlogo/software/windowsxp-sw.mspx)" icon.

I tend to write a lot of Windows Services. I try to build an installer
for each one that provides a GUI for the user to enter in some
configuration data which gets written to the config file upon
installation. That's pretty much the only time my config files get
modified unless we need to adminstratively change something or other.
Note the use of an administrator and not a user.

For more information about persisting user settings, check out:

-   [Manage User Settings in Your .NET App with a Custom Preferences
    API](http://msdn.microsoft.com/msdnmag/issues/04/07/CustomPreferences/default.aspx)
-   [Persisting Application Settings in the .NET
    Framework](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dndotnet/html/persistappsettnet.asp)
-   [Designed forWindows
    XP](http://www.microsoft.com/winlogo/software/windowsxp-sw.mspx)


