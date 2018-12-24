---
title: Configuring Log4Net with ASP.NET 2.0 in Medium Trust
date: 2006-07-09 -0800
tags: [log4net,logging,aspnet]
redirect_from: "/archive/2006/07/08/ConfiguringLog4NetWithASP.NET2.0InMediumTrust.aspx/"
---

UPDATE: Mea Culpa! It seems like Log4Net has no problems with medium trust and an external log4net file. I [have written an updated
post](https://haacked.com/archive/2006/08/08/Log4NetAndExternalConfigurationFileInASP.NET2.0.aspx "External Config FIles and Log4Net")
that talks about the problem I *did* run into and how I solved it.

A while ago I wrote a quick and dirty guide to [configuring Log4Net for ASP.NET](https://haacked.com/archive/2005/03/07/ConfiguringLog4NetForWebApplications.aspx "Quick and Dirty").
~~Unfortunately, this technique does not work with ASP.NET 2.0 when running in medium trust.~~. This technique continues to work with medium trust!

While digging into the problem I found [this blog post](http://blogs.advantaje.com/blog/kevin/Net/2006/06/29/log4Net-and-ASP-Net-Medium-Trust.html "Log4Net and ASP.NET Medium Trust")
(from an aptly titled blog) by Kevin Jones.

This article from Microsoft discusses the ramifications of running ASP.NET 2.0 in medium trust more thoroughly. Here is a list of
constraints placed on medium trust applications.

> The main constraints placed on medium trust Web applications are:
>
> -   **`OleDbPermission`** is not available. This means you cannot use
>     the ADO.NET managed OLE DB data provider to access databases.
>     However, you can use the managed SQL Server provider to access SQL
>     Server databases.
> -   **`EventLogPermission`** is not available. This means you cannot
>     access the Windows event log.
> -   **`ReflectionPermission`** is not available. This means you cannot
>     use reflection.
> -   **`RegistryPermission`** is not available. This means you cannot
>     access the registry.
> -   **`WebPermission`** is restricted. This means your application can
>     only communicate with an address or range of addresses that you
>     define in the `<trust>` element.
> -   **`FileIOPermission`** is restricted. This means you can only
>     access files in your applicationâ€™s virtual directory hierarchy.
>     Your application is granted Read, Write, Append, and PathDiscovery
>     permissions for your applicationâ€™s virtual directory hierarchy.
>
> You are also prevented from calling unmanaged code or from using
> Enterprise Services.

Fortunately there is a way to specify that a configuration section within web.config should not require `ConfigurationPermission`. Simply add the `requirePermission="false"` attribute to the `<section>` declaration within the `<configSections>` area like so:

```xml
<configSections>
    <section name="log4net" 
      type="log4net.Config.Log4NetConfigurationSectionHandler
      , log4net"     
      requirePermission="false"/>
</configSections>
```

Unfortunately this applies to configuration sections within the web.config file. I have not found a way to specify that ASP.NET should
not require `ConfigurationPermission` on an *external* configuration file. As I stated in my post on Log4Net, I prefer to put my Log4Net configuration settings in a separate configuration file. ~~**If anyone
knows a way to do this, please let me know!**~~

So in order to get Log4Net to work, I added the declaration above to the web.config file and copied the settings within the Log4Net.config file (pretty much cut and paste everything except the top xml declaration) into the web.config file. I then removed the assembly level `XmlConfigurator` attribute from AssemblyInfo.cs as it is no longer needed. Instead, I added the following line to the `Application_Start` method in Global.asax.cs.

```csharp
protected void Application_Start(Object sender, EventArgs e)
{
    log4net.Config.XmlConfigurator.Configure();
}
```

So in summary, here are the changes I made to get Log4Net to work again in medium trust.

-   Added the log4Net section declaration in the `configSections`     section of web.config and made sure the `requirePermission`
    attribute is set to the value `false`.
-   Moved the log4Net settings into web.config.
-   Removed the assembly attribute `XmlConfigurator`
-   Added the call to `XmlConfigurator.Configure()` to the `Application_Start` method in Global.asax.cs.

I have been working on getting the version of Subtext in our Subversion trunk to run in a medium trust environment, but there have been many challenges. Some of the components we use do not appear to run in a medium trust environment such as the
[FreeTextBox](http://freetextbox.com/forums/thread/6874.aspx "FreeTextBox control"). Fortunately, we have a workaround for that issue which is to change the `RichTextEditor` node in web.config to use the `PlainTextRichTextEditorProvider` (which is a mouthful and should probably be renamed to `PlainTextEditorProvider`).
