---
title: Hosting a Simple &ldquo;Read-Only&rdquo; NuGet Package Feed on the Web
tags: [nuget,aspnet,code]
redirect_from: "/archive/2011/03/30/hosting-simple-nuget-package-feed.aspx/"
---

As you may know, NuGet supports aggregating packages from multiple
package sources. You can simply point NuGet at a folder containing
packages or at a NuGet OData service.

A while back I wrote up a guide to [hosting your own NuGet
feed](https://haacked.com/archive/2010/10/21/hosting-your-own-local-and-remote-nupack-feeds.aspx "Host your own NuGet Feed").
Well, we’ve made it ***way easier*** to set one up now! And, surprise
surprise, it involves NuGet.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hosting-a-Simple-Package-Feed_9169/wlEmoticon-smile_2.png)
I’ll provide step by step instructions here. But first, make sure you’re
running NuGet 1.2!

### Step 1: Create a new Empty Web Application in Visual Studio

Go to the *File* | *New* | *Project* menu option (or just hit CTRL +
SHIFT + N) which will bring up the new project dialog and select
“ASP.NET Empty Web Application” as in the following screenshot (*click
to enlarge*).

[![new-project-dialog](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hosting-a-Simple-Package-Feed_9169/new-project-dialog_thumb.png "new-project-dialog")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hosting-a-Simple-Package-Feed_9169/new-project-dialog_2.png)

This results in a very empty project template.

![empty-web-project](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hosting-a-Simple-Package-Feed_9169/empty-web-project_5fd751ab-21a9-44ee-a4a4-3b43abe0b85c.png "empty-web-project")

### Step 2: Install the *NuGet.Server* Package

Now right click on the *References* node and select *Add Library Package
Reference* to launch the NuGet dialog (alternatively, you can use the
Package Manager Console instead and type
`Install-Package NuGet.Server`).

Click the **Online** tab and then type *NuGet.Server* in the top right
search box. Click **Install** on the *NuGet.Server* package as shown in
the following image (*click to enlarge*).

[![nuget-dialog](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hosting-a-Simple-Package-Feed_9169/nuget-dialog_thumb.png "nuget-dialog")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hosting-a-Simple-Package-Feed_9169/nuget-dialog_2.png)

### Step 3: Add Packages to the Packages folder

That’s it! The *NuGet.Server* package just converted your empty website
into a site that’s ready to serve up the OData package feed. Just add
packages into the Packages folder and they’ll show up.

In the following screenshot, you can see that I’ve added a few packages
to the *Packages* folder.

![packages-folder](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hosting-a-Simple-Package-Feed_9169/packages-folder_5f23a929-4273-4724-baf7-df5d06db247a.png "packages-folder")

### Step 4: Deploy and run your brand new Package Feed!

I can hit CTRL + F5 to run the site and it’ll provide some instructions
on what to do next.

![package-site](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hosting-a-Simple-Package-Feed_9169/package-site_5660dc27-389d-449c-8097-fd09a24483f1.png "package-site")

Clicking on “here” shows the OData over ATOM feed of packages.

![package-feed](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hosting-a-Simple-Package-Feed_9169/package-feed_64a7dc6d-3449-42c2-9bc9-d052b13f6e8b.png "package-feed")

Now all I need to do is deploy this website as I would any other site
and then I can click the Settings button and add this feed to my set of
package sources as in the following screenshot (click to enlarge).

[![Options](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hosting-a-Simple-Package-Feed_9169/Options_thumb.png "Options")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hosting-a-Simple-Package-Feed_9169/Options_2.png)

Note that the URL you need to put in is
[*http://yourdomain/nuget/*](http://yourdomain/nuget/)** **depending on
how you deploy the site.

Yes, it’s that easy! Note that this feed is “read-only” in the sense
that it doesn’t support publishing to it via the NuGet.exe command line
tool. Instead, you need to add packages to the *Packages* folder and
they are automatically syndicated.

