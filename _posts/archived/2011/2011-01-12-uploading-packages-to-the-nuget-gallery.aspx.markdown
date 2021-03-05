---
title: Uploading Packages To The NuGet Gallery
tags: [nuget,oss]
redirect_from: "/archive/2011/01/11/uploading-packages-to-the-nuget-gallery.aspx/"
---

As [David Ebbo](http://blog.davidebbo.com/ "David Ebbo's Blog") blogged
today, the [NuGet Gallery is now open to the
public](http://blog.davidebbo.com/2011/01/introducing-nuget-gallery.html "Introducing the NuGet Gallery").
The goal of the NuGet Gallery is to be the hub for NuGet users and
package authors alike. Users should be able to search and discover
packages with detailed information on each one and eventually rate them.
Package authors can register for an API key and upload packages.

We’re not quite where we want to be with the gallery, but we’re moving
in the right direction. If you want to see us get there more quickly,
feel free to lend a hand. The gallery is running on [fully open source
code](http://orchardgallery.codeplex.com/ "Orchard Gallery")!

In this blog post, I wanted to cover step by step what it takes to
create and upload a package.

Create Your Package
-------------------

Well the first step is to create a package so you have something to
upload. If you’re well acquainted with creating packages, feel free to
skip this section, but you may learn a few tips if you stick with it.

I’ll start with a simple example that I did recently. The [XML-RPC.NET
library](http://www.xml-rpc.net/ "XML-RPC.NET") by Charles Cook is very
useful for implementing XML-RPC Services and clients. It powers the
[MetaWeblog API](http://www.xmlrpc.com/metaWeblogApi "MetaWeblog API")
support in [Subtext](http://subtextproject.com/ "Subtext Blog Engine").
As a courtesy, I recently asked Charles if he would mind if I created a
NuGet package for his library for him, to which he said yes!

So on my machine, I created a folder named after the latest 2.5 release,
*xmlrpcnet.2.5.0*. Here’s the directory structure I ended up with.

![package-folder-structure](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/beeb4862c29d_13D5F/package-folder-structure_3.png "package-folder-structure")

By convention, the *lib* folder is where you place assemblies that will
get added as referenced assemblies to the target project when installing
this package. Since this assembly only supports .NET 2.0 and above, I
put it in the net20 subfolder of the lib folder.

The other required file is the [.nuspec
file](http://nuget.codeplex.com/documentation?title=Nuspec%20Format "NuSpec Format"),
which contains the metadata used to build the package. Let’s take a look
at the contents of that file.

```csharp
<?xml version="1.0" encoding="utf-8"?> 
<package> 
  <metadata> 
    <id>xmlrpcnet</id> 
    <version>2.5.0</version> 
    <authors>Charles Cook</authors> 
    <owners>Phil Haack</owners>
    <description>A client and server XML-RPC library for .Net.</description> 
    <projectUrl>http://www.xml-rpc.net/</projectUrl>
    <licenseUrl>http://www.opensource.org/licenses/mit-license.php</licenseUrl>
    <tags>xml-rpc xml rpc .net20 .net35 .net40</tags>
    <language>en-US</language> 
  </metadata> 
</package>
```

There’s a couple of things I want to call out here. Notice that I
specified Charles Cook in the *authors* element, but put my own name in
the *owners* element. Authors represent the authors of the library
within the package, while the owner typically represents the person who
created the package itself. This allows people to know who to contact if
there’s a problem with the package vs a problem with the library within
the package.

In general, we hope that most of the time, the authors and the owners
are one and the same. For example, someday I’d love to help Charles take
ownership of his packages. Until that day, I’m happy to create and
upload them myself.

If somebody creates a package for a library that you authored and
uploads it to NuGet, assume it’s a favor they did to get your library
out there. If you wish to take ownership, feel free to contact them and
they can assign the packages over to you. This is the type of thing we’d
like to see resolved by the community and not via some policy rules on
the gallery site. This is a case where the gallery could do a lot to
make this sort of interaction easier, but does not have such features in
place yet.

With this in place, it’s time to create the package. To do that, we’ll
need the [NuGet.exe console
application](http://nuget.codeplex.com/releases/57303/download/197743 "NuGet Console Application").
Copy it to a utility directory and add it to your path, or copy it to
the parent folder of the package folder.

![nuget-dir](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/beeb4862c29d_13D5F/nuget-dir_3.png "nuget-dir")

Now, open a command prompt and navigate to the directory and run the
*nuget pack* command.

`nuget pack path-to-nuspec-file`

Here’s a screenshot of what I did:

[![nuget-pack](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/beeb4862c29d_13D5F/nuget-pack_thumb_1.png "nuget-pack")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/beeb4862c29d_13D5F/nuget-pack_4.png)

**Pro tip:** What I really did was add a batch file I call *build.cmd*
in the same directory that I put the *NuGet.exe* file. The contents of
the batch file is a single line:

`for /r %%x in (*.nuspec) do nuget pack "%%x" -o d:\packages\`

What that does is run the *nuget pack* command on every subdirectory of
the current directory. I have a folder that contains multiple packages
that I’m working on and I can easily rebuild them all with this batch
file.

Ok, so now we have the package, let’s publish it! But first, we have to
create an account on the NuGet Gallery website.

Register and Upload
-------------------

The first step is to register for an account at
[http://nuget.org/Users/Account/Register](http://nuget.org/Users/Account/Register "http://nuget.org/Users/Account/Register").
Once you have an account, click on the [Contribute
tab](http://nuget.org/Contribute/Index "Contribute Tab"). This page
gives you several options for managing packages (*click to enlarge*).

[![contribute-tab](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/beeb4862c29d_13D5F/contribute-tab_thumb.png "contribute-tab")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/beeb4862c29d_13D5F/contribute-tab_2.png)

To upload your package, click on the [Add New Package
link](http://nuget.org/Contribute/NewSubmission "New Submission Page").

[![upload-package](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/beeb4862c29d_13D5F/upload-package_thumb.png "upload-package")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/beeb4862c29d_13D5F/upload-package_2.png)

Notice there’s two options. At this point, you can simply browse for the
package you created and upload it and you’re done. In a matter of a few
minutes, it should appear in the public feed.

The second option allows you to host your package file in a location
other than the NuGet gallery such as
[CodePlex.com](http://codeplex.com/ "CodePlex"), [Google
Code](http://code.google.com/ "Google Code"), etc. Simply enter the the
direct URL to the package and when someone tries to install your
package, the NuGet client will redirect the download request to the
external package URL.

Submit From The Command Line
----------------------------

Ok, that’s pretty easy. But you’re a command line junky, right? Or
perhaps you’re automating package submission.

Well you’re in luck, it’s pretty easy to submit your package directly
from the command line. But first, you’re going to need an API key.

Visit the My Account page
([http://nuget.org/Contribute/MyAccount](http://nuget.org/Contribute/MyAccount "http://nuget.org/Contribute/MyAccount"))
and make a note of your API key (click image below to enlarge it).

[![nuget-gallery-api-key](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/beeb4862c29d_13D5F/nuget-gallery-api-key_thumb.png "nuget-gallery-api-key")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/beeb4862c29d_13D5F/nuget-gallery-api-key_2.png)

Be sure to keep that API key secret! Don’t give it out like I just did.
If you do happen to accidentally leak your API key, you can click the
*Generate New Key* button, again like I just did. *You didn’t really
think I’d let you know my API key, did you?*

Now, using the same *NuGet.exe* command line tool we downloaded earlier,
we can push the package to the gallery using the *nuget push* command.

`   `

nuget push path-to-nupkg api-key –source http://packages.nuget.org/v1/

Here’s a screenshot of the exact command I ran.

![publishing-nupkg](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/beeb4862c29d_13D5F/publishing-nupkg_3.png "publishing-nupkg")

Shoot! There I go showing off my secret API key again! I better
regenerate that.

As you can see, this command uploaded my package and published it to the
feed. I can login and visit the [Manage My
Contributions](http://nuget.org/Contribute/MyPackages "My Packages")
page to see this package and even make changes to it if necessary.

Moving Forward
--------------

We’re still working out the kinks in the site and hopefully, by the time
you read this blog post, this particular issue will be fixed. Also,
we’re planning to update the NuGet.exe client and make the NuGet gallery
be the default source so that the `–source` flag is not required.

As David mentioned, the site was primarily developed as a CodePlex.com
project by the Nimble Pros in a very short amount of time. There’s two
major components to the site. There’s the [front-end Orchard
Gallery](http://orchardgallery.codeplex.com/ "Orchard Gallery") built as
an [Orchard](http://orchard.codeplex.com/ "Orchard") module. This powers
the gallery website that you see when you visit
[http://nuget.org/](http://nuget.org/). There’s also [the back-end
gallery
server](http://galleryserver.codeplex.com/ "Gallery Server Project")
which hosts the OData feed used to browse and search for packages as
well as the WCF service endpoint for publishing packages.

Each of these components are open source projects which means if you
really wanted to, you could take the code and host your own gallery
website. Orchard will be using the same code to host its own gallery of
Orchard modules.

Also, these projects accept contributions! I personally haven’t spent
much time in the code, but I hope to find some free time to chip in
myself.

