---
layout: post
title: "Log4Net Troubleshooting"
date: 2006-09-27 -0800
comments: true
disqus_identifier: 17270
categories: []
---
When Log4Net doesn’t work, it can be a very frustrating experience. 
Unlike your typical application library, log4net doesn’t throw
exceptions when it fails.  Well that is to be expected and makes a lot
of sense since it is a logging library.  I wouldn’t want my application
to fail because it had trouble logging a message.

Unfortunately, the downside of this is that problems with log4net aren’t
immediately apparent.  99.9% of the time, when Log4Net doesn’t work, it
is a configuration issue.  Here are a couple of troubleshooting tips
that have helped me out.

### Enable Internal Debugging

[This
tip](http://logging.apache.org/log4net/release/faq.html#internalDebug)
is straight from the [Log4Net
FAQ](http://logging.apache.org/log4net/release/faq.html), but
not everyone notices it. To enable internal debugging, add the following
app setting to your App.config (or Web.config for web applications)
file.

```csharp
<add key="log4net.Internal.Debug" value="true"/>
```

This will write internal log4net messages to the console as well as the
[`System.Diagnostics.Trace`](http://msdn2.microsoft.com/en-us/library/system.diagnostics.trace.aspx)
system.  You can easily output the log4net internal debug messages by
adding a trace listener.  The following snippet is taken from the
log4net FAQ and goes in your \<configuration\> section of your
application config file.

```csharp
<system.diagnostics>
  <trace autoflush="true">
    <listeners>
      <add 
        name="textWriterTraceListener" 
        type="System.Diagnostics.TextWriterTraceListener" 
        initializeData="C:\tmp\log4net.txt" />
    </listeners>
  </trace>
</system.diagnostics>
```

### Passing Nulls For Value Types Into AdoNetAppender {.clear}

Another common problem I’ve dealt with is logging using the
AdoNetAppender. In particular, attempting to log a null value into an
int parameter (or other value type), assuming your stored procedure
allows null for that parameter.

The key here is to use the
[RawPropertyLayout](http://logging.apache.org/log4net/release/sdk/log4net.Layout.RawPropertyLayout.html)
for that parameter. Here is a snippet from a log4net.config file that
does this.

```csharp
<parameter>
  <parameterName value="@BlogId" />
  <dbType value="Int32" />
  <layout type="log4net.Layout.RawPropertyLayout">
    <key value="BlogId" />
  </layout>
</parameter>
```

Hopefully this helps you with your log4net issues.

### Related Posts {.clear}

-   [Quick and Dirty Guide to Configuring Log4Net for Web
    Applications](http://haacked.com/archive/2005/03/07/ConfiguringLog4NetForWebApplications.aspx)
-   [Setting Up Log4Net For Multi Layered
    Applications](http://haacked.com/archive/2006/01/13/SettingUpLog4NetForMultiLayeredApplications.aspx)
-   [Log4Net and External Configuration File In ASP.NET
    2.0](http://haacked.com/archive/2006/08/08/Log4NetAndExternalConfigurationFileInASP.NET2.0.aspx)
-   [Log4Net Breaking Change in
    1.2.9](http://haacked.com/archive/2006/08/07/Log4NetBreakingChangeIn1.2.9.aspx)

tags: [Log4Net](http://technorati.com/tag/Log4Net)

