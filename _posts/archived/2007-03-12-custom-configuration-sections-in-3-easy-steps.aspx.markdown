---
layout: post
title: Custom Configuration Sections in 3 Easy Steps
date: 2007-03-12 -0800
comments: true
disqus_identifier: 18236
categories: []
redirect_from: "/archive/2007/03/11/custom-configuration-sections-in-3-easy-steps.aspx/"
---

Are you tired of seeing your configuration settings as an endless list
of key value pairs?

```xml
<add key="key0" value="value0" />
<add key="key1" value="value1" /> 
<add key="key2" value="value2" />
... 
```

Would you rather see something more like this?

```xml
<MySetting
  fileName="c:\temp"
  password="pencil"
  someOtherSetting="value" />
```

Join the club. Not only is the first approach prone to typos
(`AppSettings["tire"]` or `AppSettings["tier] `anyone?), too many of
these things all bunched together can cause your eyes to glaze over. It
is a lot easier to manage when settings are grouped in logical bunches.

A while back [Craig Andera](https://sites.google.com/site/craigandera/ "Craig Andera") solved this problem with the [Last Configuration Section Handler](https://sites.google.com/site/craigandera/craigs-stuff/clr-workings/the-last-configuration-section-handler-i-ll-ever-need "Last Configuration Section Handler") he’d ever need. This basically made it easy to specify a custom strongly
typed class to represent a logical group of settings using XML Serialization. It led to a much cleaner configuration file.

But that was then and this is now. With ASP.NET 2.0, **there’s an even easier way** which I didn’t know about until [Jeff
Atwood](http://codinghorror.com/blog/ "Jeff Atwood") recently turned me on to it.

So here is a quick run through in three easy steps.

### Step one - Define your Custom Configuration Class

In this case, we’ll define a class to hold settings for a blog engine. We just need to define our class, inherit from
`System.Configuration.ConfigurationSection`, and add a property per setting we wish to store.

```csharp
using System;
using System.Configuration;

public class BlogSettings : ConfigurationSection
{
  private static BlogSettings settings 
    = ConfigurationManager.GetSection("BlogSettings") as BlogSettings;
        
  public static BlogSettings Settings
  {
    get
    {
      return settings;
    }
  }

  [ConfigurationProperty("frontPagePostCount"
    , DefaultValue = 20
    , IsRequired = false)]
  [IntegerValidator(MinValue = 1
    , MaxValue = 100)]
  public int FrontPagePostCount
  {
      get { return (int)this["frontPagePostCount"]; }
        set { this["frontPagePostCount"] = value; }
  }


  [ConfigurationProperty("title"
    , IsRequired=true)]
  [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\"
    , MinLength=1
    , MaxLength=256)]
  public string Title
  {
    get { return (string)this["title"]; }
    set { this["title"] = value; }
  }
}
```

Notice that you use an indexed property to store and retrieve each property value.

I also added a static property named `Settings` for convenience.

### Step 2 - Add your new configuration section to web.config (or app.config).

```xml
<configuration>
  <configSections>
      <section name="BlogSettings" type="Fully.Qualified.TypeName.BlogSettings,   
      AssemblyName" />
  </configSections>
  <BlogSettings
    frontPagePostCount="10"
    title="You’ve Been Haacked" />
</configuration>
```

### Step 3 - Enjoy your new custom configuration section {.clear}

```csharp
string title = BlogSettings.Settings.Title;
Response.Write(title); //it works!!!
```

What I covered is just a very brief overview to get you a taste of what is available in the Configuration API. I wrote more about configuration in the book I’m cowriting with [Jeff Atwood](http://codinghorror.com/blog/ "Jeff Atwood"), [Jon Galloway](http://weblogs.asp.net/jgalloway/ "Jon Galloway"), and [K. Scott Allen](http://odetocode.com/blogs/scott/default.aspx "K. Scott Allen").

If you want to get a more comprehensive overview and the nitty gritty, I
recommend reading [Unraveling the Mysteries of .NET 2.0
Configuration](http://www.codeproject.com/dotnet/mysteriesofconfiguration.asp# ".NET 2.0 Configuration article on CodeProject") by Jon Rista.
