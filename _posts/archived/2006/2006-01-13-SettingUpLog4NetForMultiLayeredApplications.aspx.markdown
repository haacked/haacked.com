---
title: Setting Up Log4Net For Multi Layered Applications
tags: [logging]
redirect_from: "/archive/2006/01/12/SettingUpLog4NetForMultiLayeredApplications.aspx/"
---

A while ago I wrote up a [Quick and Dirty Guide to Configuring Log4Net
for Web
Applications](https://haacked.com/archive/2005/03/07/ConfiguringLog4NetForWebApplications.aspx/ "Guide to Log4Net").
Today I received an email asking how to set up logging for a web
application that also consists of a business layer and a data access
layer.

### The Situation

This person had the following three projects setup as part of his VS.NET
Solution:

-   ASP.NET Web Application Project
-   Business Layer Class Library Project
-   Data Access Layer Class Library Project

Note that the Web Application project has a project reference (or
assembly reference) to the Business Layer, which in turn probably has a
project reference to the Data Access layer. These assemblies will be
deployed with the web application and will not be hosted on separate
servers, thus remoting does not come into play here.

The developer added a Log4Net.config file to each project as well as the
AssemblyInfo directive described in my post. The goal was to get all
three projects logging to the same file. For the two class library
assemblies, the developer specified the full system path to the log
file.

### The Explanation

To understand why this doesn’t necessarily work, we have to step back
and look at how configuration settings are picked up by a .NET
application in general. Suppose we were’t dealing with Log4Net for a
second, but wanted to configure some app settings? Would it require that
we add an App.config file to the Business Layer and Data Access layer
project? Indeed no. These are class libraries. They do not contain an
execution entry point as an executable does. We simply need to add a
web.config file to the Web Application Project and we’re set.

The main reason for this is that configuration settings apply to the
executable application (in this case a web app). You can certainly
include code within the business layer assembly to read app settings,
but it reads the settings from the web.config or App.config file in the
execution startup path.

*Note: I am doing a bit of hand waving here. Technically, the ASP.NET
web application assembly is not an executable, it is a class library.
However due to how the ASP.NET runtime works, it exhibits some of the
behavior of being an executable and for the purposes of this discussion
we’ll leave it at that. One key difference though is that for
executables, the config file must be named the same as the assembly with
a config extension and put in the same directory as the executable
(typically bin), whereas with an ASP.NET application, the config file is
always named “web.config” and placed in the web root, not in the bin
directory.*

This is also how Log4Net configuration works. Remember that when you
build the web application, your business layer and data access layer
assemblies will be copied to the bin directory of the web application.
Thus all three assemblies are in the same location, so there is no need
to specify a different Log4Net.config file for each assembly.

When you think of it, this makes sense. Your business layer assembly is
a class library, thus it can be used and re-used in more than one
project. It is not an execution starting point, but is called into by
another executable. You wouldn’t want that assembly to specifiy where it
logs its messages. You would rather have the consumer of the assembly do
that.

### The Answer

So the answer to the question is to make sure that both the business
layer and data access layer projects do NOT include a Log4Net.config
file nor the AssemblyInfo directive. They do not need it. It will be up
to the consumer of these assemblies (the execution starting point) to
configure logging.

All you need to do in these assemblies is to add an assembly reference
to the Log4Net assembly and make calls to its logging methods in your
code just as you would in the web application layer.

Then configure your web application as mentioned in my [dirty
guide](https://haacked.com/archive/2005/03/07/ConfiguringLog4NetForWebApplications.aspx/ "Guide to Log4Net")
and you are all set. Log messages from all three assemblies should
funnel nicely to your log file.

To demonstrate this, I set up [another sample VS.NET 2003
solution](https://haacked.com/assets/images/Log4NetSampleSolution2.zip "Sample Log4Net Solution").
It is based on the same project that I included in my previous article
on the subject, but includes a business layer class library. The web
application references the class library and makes a method call that
logs a message. The class library references Log4Net, but does not
include the Assembly directive nor the Log4Net.config file.

Download it, set up the IIS directory, and visit the default page.
You’ll see log messages from within the business layer as well as the
web application in the log file.

