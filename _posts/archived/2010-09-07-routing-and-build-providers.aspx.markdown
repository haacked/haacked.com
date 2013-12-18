---
layout: post
title: "Overriding a .svc Request With Routing"
date: 2010-09-07 -0800
comments: true
disqus_identifier: 18720
categories: [asp.net,asp.net mvc,code]
---
I was drawn to an [interesting question on
StackOverflow](http://stackoverflow.com/questions/3662555/how-can-i-override-a-svc-file-in-my-routing-table "Override .svc file")
recently about how to override a request for a non-existent .svc request
using routing.

One useful feature of routing in ASP.NET is that requests for files that
exist on disk are ignored by routing. Thus requests for static files and
for .aspx and .svc files don’t run through the routing system.

In this particular scenario, the developer wanted to replace an existing
.svc service with a call to an ASP.NET MVC controller. So he deletes the
.svc file and adds the following route:

```csharp
routes.MapRoute(
  "UpdateItemApi",
  "Services/api.svc/UpdateItem",
  new { controller = "LegacyApi", action = "UpdateItem" }
);
```

Since *api.svc* is not a physical file on disk, at first glance, this
should work just fine. But I tried it out myself with a brand new
project, and sure enough, it doesn’t work.

Baffling!

So I started digging into it. First, I looked in event viewer and saw
the following exception.

*`System.ServiceModel.EndpointNotFoundException: The service '/Services/api.svc' does not exist.`*

Ok, so there’s probably something special about the .svc file extension.
So I opened up the machine web.config file located here on my machine:

`   `

*C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\Config\\web.config*

And I found this interesting entry within the `buildProviders` section.

```csharp
<add extension=".svc" 
  type="System.ServiceModel.Activation.ServiceBuildProvider, 
  System.ServiceModel.Activation,
  Version=4.0.0.0, Culture=neutral, 
  PublicKeyToken=31bf3856ad364e35" 
/>
```

Ah! There’s a default build provider registered for the .svc extension.
And as we all know, build providers allow for runtime compilation of
requests for ASP.NET files and occur very early in response to a
request.

The fix I came up with was to simply remove this registration within my
application’s web.config file.

```csharp
  <system.web>
    <compilation debug="true" targetFramework="4.0">
      <buildProviders>
        <remove extension=".svc"/>            
      </buildProviders>
    ...
```

Doing that now allowed my route with the *.svc* extension to work. Of
course, if I have other .svc services that should continue to work, I’ve
pretty much disabled all of them by doing this. However, if those
services are in a common subfolder (for example, a folder named
*services*), we may be able to get around this by adding the build
provider in a web.config file within that common subfolder.

In any case, I thought the question was interesting as it demonstrated
the delicate interplay between routing and build providers.

