---
title: Quick and Dirty Guide to Configuring Log4Net For Web Applications
redirect_from:
- "/archive/2005/03/07/2317.aspx.html"
- "/archive/2005/03/06/ConfiguringLog4NetForWebApplications.aspx/"
tags: [logging,aspnet]
---

UPDATE: I [wrote a
post](https://haacked.com/archive/2006/07/09/ConfiguringLog4NetWithASP.NET2.0InMediumTrust.aspx "Configuring Log4Net in Medium Trust")
with notes on getting this to work with ASP.NET 2.0.

Looking around, I noticed a lot of people struggling with getting [Log4Net](http://logging.apache.org/log4net/ "Log4Net website") to work with their web applications (ASP.NET 1.1). I’m not going to spend a lot of time digging into Log4Net here, as you can do a [Google search](http://www.google.com/search?sourceid=navclient&ie=UTF-8&rls=GGLB,GGLB:1969-53,GGLB:en&q=log4net "Google Search Results for Log4Net") for that. But I will give you a quick and dirty guide to quickly getting it set up for a website. Bar of soap not included.

### Using a Separate Config File

Although you can put your Log4Net configuration settings within the web.config file, I prefer to use a separate configuration file. Log4Net is a bit of an elitist. It won’t dare put a FileSystemWatcher on web.config nor App.config. However, if you tell it to use its own config file, it will gladly monitor that log file and update its settings on the fly when the file changes.

### Specifying the Log4Net Config File

If you use a separate config file, a quick and easy (and dirty) way to have your application find it is to place the config file in the webroot and add the following attribute to your AssemblyInfo.cs file.

```csharp
[assembly: log4net.Config.XmlConfigurator( 
ConfigFile="Log4Net.config",Watch=true )]
```

### Declaring the Logger {.clear}

At the top of each class that I plan to use logging in, I declare a logger like so:

```csharp
private static readonly ILog Log = LogManager.GetLogger( 
MethodBase.GetCurrentMethod().DeclaringType);
```

The reason I place a logger in each class is to scope it to that class. If you read the log4Net docs, you’ll see what I mean by this.

### Using the Logger

Once you’ve declared the logger, you can call one its logging methods. Each method is named for the logging level. For example:

```csharp
Log.Debug("This is a DEBUG level message.  
Typically your most VERBOSE level.");
```

Now whether that message shows up in your logs depends on how you’ve configured your appenders and the logging level you’ve set. Don’t
understand what that means? Read the [Log4Net introduction](http://logging.apache.org/log4net/release/manual/introduction.html "Log4Net introduction docs").

### Sample Web Solution

In order to make all this discussion very concrete, I’ve gone ahead and did all your homework for you by creating a simple ASP.NET 1.1 web solution ([Log4NetSampleSolution.zip](/assets/images/Log4NetSampleSolution.zip "Visual Studio.NET 2003 Solution")
) using Visual Studio.NET 2003. After unzipping this solution, you should be able to build it and then view the default.aspx web page. This page will log a few very interesting messages of varying levels to three appenders.

Of special note is the use of the RollingFileAppender as seen in this snippet.

```xml
<appender name="RollingLogFileAppender"    
        type="log4net.Appender.RollingFileAppender">

    <file value="..\Logs\\CurrentLog" />
    <appendToFile value="true" />
    <datePattern value="yyyyMMdd" />

    <rollingStyle value="Date" />
    <filter type="log4net.Filter.LevelRangeFilter">
        <acceptOnMatch value="true" />

        <levelMin value="INFO" />
        <levelMax value="FATAL" />
    </filter>

    <layout type="log4net.Layout.PatternLayout">
        <conversionPattern 
        value="%-5p %d %5rms %-22.22c{1} %-18.18M - %m%n" />
    </layout>

</appender>
```

Note that the file value (with backslashes escaped) points to **..\\Logs\\CurrentLog**. This specifies that Log4Net will log to a file
in a directory named *Logs* **parallel** to the webroot. You need to give the ASPNET user write permission to this directory, which is why it is generally a good idea to leave it out of the webroot. Not to mention the potential for an IIS misconfiguration that allows the average Joe to snoop through your logs.
