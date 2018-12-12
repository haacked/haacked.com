---
title: The Very Last Configuration Section Handler I'll Ever Need
date: 2004-06-24 -0800
disqus_identifier: 679
categories: [aspnet config]
redirect_from: "/archive/2004/06/23/verylastconfigurationsectionhandler.aspx/"
---

UPDATE: In ASP.NET 2.0, there’s an even easier approach that supercedes this one. I wrote [about it here](https://haacked.com/archive/2007/03/12/custom-configuration-sections-in-3-easy-steps.aspx "configuration in asp.net 2.0").

A while back [Craig Andera](http://pluralsight.com/blogs/craig/default.aspx "Craig Andera’s Blog") wrote an excellent article entitled [The Last Configuration Section Handler I’ll Ever Need](http://www.pluralsight.com/wiki/default.aspx/Craig/XmlSerializerSectionHandler.html) which outlines a really nice and flexible configuration section handler. It is certainly a big time saver and was the leading contender as the last one I would ever need as well. That is until I realized it lacked one minor feature I need: the ability to reload itself when the config file changes.

Before continuing, please do go and read his [article](http://www.pluralsight.com/wiki/default.aspx/Craig/XmlSerializerSectionHandler.html "The Last Configuration Section Handler") for background. It’s pretty short and well written. No really, go read it while I listen to

_American Dream (Joey Negro Club Mix) - Jakatta - Essential Mix - Mixed By Pete Tong (5:22)_

Having read his article, it should be apparent why reloading the settings when the file changes is a challenge. Mainly because you most
likely will have a reference to your simple settings object (such as the `MyStuff` class in the article) which has to somehow be notified that a change has occurred. As you can see from the sample below, classes used to hold settings (such as the `MyStuff` class) are deliberately kept very simple in order to facilitate writing many of them. There’s no facility to watch for changes to the config file.

```csharp
public class MyStuff {
  public float Foo {
    get;
    set;
  }

  public string Bar {
    get;
    set;
   }
}
```

So after a bit of thought and a bit more beer, I came up with a solution that I believe still retains the simplicity of his approach, while adding the ability to have the settings dynamically get updated when the config file changes. My approach is also backwards compatible with existing setting objects created using his approach. Existing setting objects will not need any changes to work, though they won’t get the benefit of this new feature.

I’ve extended Craig’s solution with the addition of the `XmlSectionSettingsBase` abstract class. The purpose of this class is to
act as the base class for your simple setting object. To add the ability to reload itself, just have your class inherit from
`XmlSectionSettingsBase` and call the `UpdateChanges()` method before each property getter. As an example, I’ve modified the above settings object:

```csharp
public class MyStuff : XmlSectionSettingsBase {
  private float foo;
  private string bar;

  public float Foo {
    get {
      UpdateChanges();
      return foo;
    }
    set { foo = value ; }
  }

  public string Bar {
    get {
      UpdateChanges();
      return bar;
    }
    set { bar = value;
  }
}
```

That’s it! That is the total amount of changes to the settings class needed so that it will now track changes. I’ve also updated the
`XmlSerializerSectionHandler` class as well. It’s now pretty small:

```csharp
public class XmlSerializerSectionHandler : IConfigurationSectionHandler {
  public object Create(object parent, object context, XmlNode section)  {
    return XmlSectionSettingsBase.LoadSettings(section);
  }
}
```

Which leads us to the `XmlSectionSettingsBase` class which is the workhorse of my scheme. You’ll notice that I’ve removed all the logic
from the section handler for loading the settings object from the config file. The reason for this is that the instance of the settings object has to know how to load itself from the config file if its going to watch it for changes. Since I didn’t want to duplicate logic, I moved that code to this class. I’ll outline the class with some broad strokes and let you download the source code for the complete picture.

First, let’s start with the static `LoadSettings` method.

```csharp
public static object LoadSettings(XmlNode section) {
  object settings = DeserializeSection(section);
  XmlSectionSettingsBase xmlSettings = settings as XmlSectionSettingsBase;
  if(xmlSettings != null) {
    xmlSettings._rootName = section.Name;
    ((XmlSectionSettingsBase)settings).WatchForConfigChanges();
  }
  return settings;
}
```

This method deserializes an instance of your settings object and checks to see if it inherits from `XmlSectionSettingsBase`. If not, it just returns, keeping this approach backwards compatible. Otherwise it calls `WatchForConfigChanges` which sets up a `FileSystemWatcher` to monitor for changes to the config file. Also notice that it stores the name of the config section node in a private member (`_rootName`) of the settings class. This becomes important later when we need to reload the settings.

```csharp
void WatchForConfigChanges() {
  FileInfo configFile = new FileInfo(
    AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
  try {
    _watcher = new FileSystemWatcher(configFile.DirectoryName);
    _watcher.Filter = configFile.Name;
    _watcher.NotifyFilter = NotifyFilters.LastWrite;
    _watcher.Changed += new FileSystemEventHandler(OnConfigChanged);
    _watcher.EnableRaisingEvents = true;
  }
  catch(Exception ex) {
    Log.Error("Configuration problem.", ex);
    throw new ConfigurationException("An error occurred while attempting to watch for file system changes.", ex);
  }
}
```

When a change occurs, the `OnConfigChanged` method is called. This method simply updates a boolean flag. This is the flag that the `UpdateChanges` method mentioned earlier checks before actually applying changes to the settings object.

```csharp
void OnConfigChanged(object sender, FileSystemEventArgs e) {
  _isDataValid = false;
}

protected void UpdateChanges() {
  if(!_isDataValid)
    ReloadSettings();
}
```

Now we hit upon the `ReloadSettings` method. This method uses the name of the node containing the config section we stored earlier
(`_rootName`) in order to load an XmlDocument containing the new settings from the config file.

```csharp
void ReloadSettings() {
  XmlDocument doc = new XmlDocument();
  doc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
  XmlNodeList nodes = doc.GetElementsByTagName(_rootName);
  if(nodes.Count > 0) {
    //Note: newSettings should not watch for config changes.
    XmlSectionSettingsBase newSettings =
      DeserializeSection(nodes[0]) as XmlSectionSettingsBase;
    newSettings._isDataValid = true;
    CopySettings(newSettings);
  }
  else
    throw new System.Configuration.ConfigurationException("Configuration section " + _rootName + " not found.");
}
```

The path to the config file is retrieved via a the `AppDomain.CurrentDomain.SetupInformation.ConfigurationFile` property.
Once we have the section, we can call `DeserializeSection` to get an instance of our settings class with the new settings. This method is pretty is the same as the `Create` method in Craig’s version.

```csharp
static object DeserializeSection(XmlNode section) {
  XPathNavigator navigator = section.CreateNavigator();
  string typename = (string)navigator.Evaluate("string(@type)");
  Type type = Type.GetType(typename);
  if(type == null)
    throw new ConfigurationException("The type ’" + typename + "’ is not a valid type. Double check the type parameter.");
  XmlSerializer serializer = new XmlSerializer(type);
  return serializer.Deserialize(new XmlNodeReader(section));
}
```

After loading this new settings instance, I overwrite the current instance’s property values with the values from the new settings
instance via a call to `CopySettings`.

```csharp
void CopySettings(object newSettings) {
  if(newSettings.GetType() != this.GetType())
    return;
  PropertyInfo[] properties = newSettings.GetType().GetProperties();
  foreach(PropertyInfo property in properties) {
    if(property.CanWrite && property.CanRead) {
      property.SetValue(this, property.GetValue(newSettings, null), null);
    }
  }
}
```

`CopySettings` uses reflection to iterate through all the public get properties of the new settings object and sets the corresponding
property of the current instance if the current property can be written to.

Unfortunately this post is very old and I don't have the source code for it anymore. In any case, there are better approaches you can use [with ASP.NET 2.0 and above](https://haacked.com/archive/2007/03/12/custom-configuration-sections-in-3-easy-steps.aspx).
