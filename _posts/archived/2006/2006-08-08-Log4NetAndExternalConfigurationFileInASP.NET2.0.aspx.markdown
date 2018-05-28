---
layout: post
title: Log4Net And External Configuration File In ASP.NET 2.0
date: 2006-08-08 -0800
comments: true
disqus_identifier: 14774
categories: []
redirect_from: "/archive/2006/08/07/Log4NetAndExternalConfigurationFileInASP.NET2.0.aspx/"
---

Recently I wrote that I could not seem to get Log4Net to [work with an
external configuration
file](https://haacked.com/archive/2006/07/09/ConfiguringLog4NetWithASP.NET2.0InMediumTrust.aspx "Problem Configuring Log4Net")
while running ASP.NET 2.0 in Medium Trust. It turns out that I should
have been more explicit. I could not get *Subtext* to work with Log4Net
in Medium Trust, but it had **nothing to do with Medium Trust**. Mea
culpa!

My best guess is that there was a small breaking code change in Log4Net
that led to this issue since we hadn’t changed the logging code. Here is
a breakdown of what happened just in case you run into this problem.

In Subtext, we wrap the Log4Net classes in our own `Log` class which is
in the `Subtext.Framework` assembly. This is how we declare a logger
within a class.

```csharp
private readonly static ILog log 
    = new Subtext.Framework.Logging.Log();
```

In the `Subtext.Web` project, we have the following attribute in
`AssemblyInfo.cs` which specifies the location of the log4net
configuration file.

```csharp
[assembly: log4net.Config.XmlConfigurator(ConfigFile 
    = "Log4Net.config", Watch = true)]
```

This worked fine and dandy up until ASP.NET 2.0. When you use the
attribute approach, you have to make a log4net call early to jump start
the engine so to speak. An attribute just sits there until somebody is
told to look at it and do something about it. In our case, the line of
code I showed above does the trick within `Global.asax.cs`.

I started digging into the Log4Net code to figure out how it uses the
attribute to find the configuration file. I finally ended up at this
code.

```csharp
public static ILog GetLogger(Type type) 
{
    return GetLogger(Assembly.GetCallingAssembly(), 
        type.FullName);
}
```

`GetLogger` searches the attributes on the calling assembly to find out
which configuration file to use. Since the calling assembly in our case
is always `Subtext.Framework` (since we wrap all calls to Log4Net),
Log4Net searches the `Subtext.Framework` assembly for the
`XmlConfiguratorAttribute`. Well that won’t work because we have the
attribute declared on the `Subtext.Web` assembly.

My initial fix was to move the attribute declaration to AssemblyInfo.cs
within Subtext.Framework. That worked, but I felt like the proper place
was to have it within the web project, since that is the natural place
to look when you are trying to figure out where the config file is
specified. So I updated the code to call log4net directly within
Global.asax.cs like so.

```csharp
//This call is to kickstart log4net.
//log4net Configuration Attribute is in AssemblyInfo
private readonly static ILog log 
    = LogManager.GetLogger(typeof(Global));

static Global()
{
    //Wrap the logger with our own.
    log = new Subtext.Framework.Logging.Log(log);
}
```

I only point this out to show there are two ways to solve it, both with
their plusses and minuses. If you run into this problem, hopefully this
guide will help you.

