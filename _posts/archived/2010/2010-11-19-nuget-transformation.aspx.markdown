---
title: NuGet Package Transformations
tags: [code,nuget,oss]
redirect_from: "/archive/2010/11/18/nuget-transformation.aspx/"
---

*Note, this blog post applies to v1.0 of NuGet and the details are
subject to change in a future version.*

In general, when you [create a NuGet
package](http://nuget.codeplex.com/documentation?title=Creating%20a%20Package "Creating a NuGet package"),
the files that you include in the package are not modified in any way
but simply placed in the appropriate location within your solution.

However, there are cases where you may want a file to be modified or
transformed in some way during installation. NuGet supports two types of
transformations during installation of a package:

-   Config transformations
-   Source transformations

Config Transformations
----------------------

Config transformations provide a simple way for a package to modify a
*web.config* or *app.config* when the package is installed. Ideally,
this type of transformation would be rare, but it’s very useful when
needed.

One example of this is [ELMAH (Error Logging Modules and Handlers for
ASP.NET)](http://code.google.com/p/elmah/ "ELMAH"). ELMAH requires that
its http modules and http handlers be registered in the *web.config*
file.

In order to apply a config transform, add a file to your packages
content with the name of the file you want to transform followed by a
*.transform* extension. For example, in the ELMAH package, there’s a
file named *web.config.transform*.

![web.config.transform-content](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NuPack-Package-Transformations_DA82/web.config.transform-content_ff374058-650a-4f04-b399-9abb2415940d.png "web.config.transform-content")

The contents of that file looks like a *web.config* (or app.config)
file, but it only contains the sections that need to be merged into the
config file.

```csharp
<configuration>
    <system.web>
        <httpModules>
            <add name="ErrorLog" type="Elmah.ErrorLogModule, Elmah" />
        </httpModules>
        <httpHandlers>
            <add verb="POST,GET,HEAD" path="elmah.axd"              type="Elmah.ErrorLogPageFactory, Elmah" />
        </httpHandlers>
    </system.web>
    <system.webServer>
        <validation validateIntegratedModeConfiguration="false" />
        <modules>
            <add name="ErrorLog" type="Elmah.ErrorLogModule, Elmah" />
        </modules>
        <handlers>
            <add name="Elmah" verb="POST,GET,HEAD" path="elmah.axd"              type="Elmah.ErrorLogPageFactory, Elmah" />
        </handlers>
    </system.webServer>
</configuration>
```

When NuGet sees this transformation file, it attempts to merge in the
various sections into your existing *web.config* file. Let’s look at a
simple example.

Suppose this is my existing *web.config* file.

**Existing web.config File**

```csharp
<configuration>
    <system.webServer>
        <modules>
            <add name="MyCoolModule" type="Haack.MyCoolModule" />
        </modules>
    <system.webServer>
</configuration>
```

Now suppose I want my NuGet package to add an entry into the *modules*
section of config. I’d simply add a file named *web.config.transform* to
my package with the following contents.

**web.config.transform File**

```csharp
<configuration>
    <system.webServer>
        <modules>
            <add name="MyNuModule" type="Haack.MyNuModule" />
        </modules>
    <system.webServer>
</configuration>
```

After I install the package, the web.config file will look like

**Existing web.config File**

```csharp
<configuration>
    <system.webServer>
        <modules>
            <add name="MyCoolModule" type="Haack.MyCoolModule" />
            <add name="MyNuModule" type="Haack.MyNuModule" />
        </modules>
    <system.webServer>
</configuration>
```

Notice that we didn’t replace the *modules* section, we merged our entry
into the modules section.

I’m currently working on documenting the full set of rules for config
transformations which I will post to our [NuGet documentation
page](http://nuget.codeplex.com/documentation "NuGet Documentation")
once I’m done.I just wanted to give you a taste for what you can do
today.

Also, in v1 of NuGet we only support these simple transformations. If we
hear a lot of customer feedback that more powerful transformations are
needed for their packages, we may consider supporting the more powerful
[web.config transformation
language](http://vishaljoshi.blogspot.com/2009/03/web-deployment-webconfig-transformation_23.html "Web Deployment: Web.Config Transformation")
as an alternative to our simple approach.

Source Transformations
----------------------

NuGet also supports source code transformations in a manner very similar
to Visual Studio project templates. These are useful in cases where your
NuGet package includes source code to be added to the developer’s
project. For example, you may want to include some source code used to
initialize your package library, but you want that code to exist in the
target project’s namespace. Source transformations help in this case.

To enable source transformations, simply append the *.pp* file extension
to your source file within your package.

Here’s a screenshot of a package I’m currently authoring.

![Models](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NuPack-Package-Transformations_DA82/Models_c4f31cf2-5437-4429-a49d-dd908c972b19.png "Models")

When installed, this package will add four files to the target project’s
*\~/Models* directory. These files will be transformed and the .pp
extension will be removed. Let’s take a look at one of these files.

```csharp
namespace $rootnamespace$.Models {
    public struct CategoryInfo {
        public string categoryid;
        public string description;
        public string htmlUrl;
        public string rssUrl;
        public string title;
    }
}
```

Notice the highlighted section that has the token `$rootnamespace$`.
That’s a Visual Studio project property which gets replaced with the
current project’s root namespace during installation.

We expect that `$rootnamespace$` will be the most commonly used project
property, though we support any project property such as `$FileName$`.
The available properties may be specific to the current project type,
but this [MSDN documentation on project
properties](http://msdn.microsoft.com/en-us/library/vslangproj.projectproperties_properties(VS.80).aspx "MSDN Documentation ProjectProperties Properties")
is a good starting point for what might be possible.

