---
title: Building Plugins Resilient To Versioning
tags: [code,versioning,extensibility]
redirect_from: "/archive/2006/06/25/BuildingPluginsResilientToVersioning.aspx/"
---

UPDATE: Added a followup [part
2](https://haacked.com/archive/2006/07/01/ResilientPluginsPartDeuxGranularControl.aspx "Part 2 - Granular control")
to this post on the topic of granular control.

![white plug](https://haacked.com/assets/images/whiteplug.jpg) We have all
experienced the trouble caused by plugins that break when an application
that hosts the plugins get upgraded. This seems to happen everytime I
upgrade [Firefox](http://www.mozilla.com/firefox/ "Firefox") or
[Reflector](http://www.aisto.com/roeder/dotnet/ "Reflector").

On a certain level, this is the inevitable result of balancing stability
with innovation and improvements. But I believe it is possible to
insulate your plugin architecture from versioning so that such changes
happen very infrequently. In designing a plugin architecture for
Subtext, I hope to avoid breaking existing plugins during upgrades
except for major version upgrades. Even then I would hope to avoid
breaking changes unless absolutely necessary.

I am not going to focus on how to build a plugin architecture that
dynamically loads plugins. There are
[many](http://www.ddj.com/dept/cpp/184403942 "Dr Dobbs Journal")
[examples](http://www.divil.co.uk/net/articles/plugins/plugins.asp "Plugin-Based Apps in .NET")
[of
that](http://www.scratchprojects.com/2006/03/building_a_plugin_architecture_p01.php "Building a Plugin Architecture with C#")
[out
there](http://www.codeproject.com/csharp/C__Plugin_Architecture.asp "Plugin Architecture for C#").
The focus of this post is how to **design** plugins for change.

Common Plugin Design Pattern
----------------------------

A common plugin design defines a base interface for all plugins. This
interface typically has an initialization method that takes in a
parameter which represents the application via an interface. This might
be a reference to the actual application or some other instance that can
represent the application to the plugin on the application’s behalf
(such as an *application context*).

```csharp
public interface IPlugin
{
   void Initialize(IApplicationHost host);
}

public interface IApplicationHost
{
   //To be determined.
}
```

This plugin interface not only provides the application with a means to
initialize the plugin, but it also serves as a *marker* interface which
helps the application find it and determine that it is a plugin.

For application with simple plugin needs, this plugin interface might
also have a method that provides a service to the application. For
example, suppose we are building a SPAM filtering plugin. We might add a
method to the interface like so:

```csharp
public interface IPlugin
{
   void Initialize(IApplicationHost host);

   bool IsSpam(IMessage message);
}
```

Now we can write an actual plugin class that implements this interface.

```csharp
public class KillSpamPlugin : IPlugin
{
    public void Initialize(IApplicationHost host)
    {
    }

    public bool IsSpam(IMessage message)
    {
        //It is all spam to me!
        return true;
    }
}
```

For applications that will have many different plugins, it is common to
have multiple plugin interfaces that all inherit from `IPlugin` such as
`ISpamFilterPlugin`, `ISendMessagePlugin`, etc...

Problems with this approach
---------------------------

**This approach is not resilient to change.** The application and plugin
interface is tightly coupled. Should we want to add a new operation to
the application that this plugin can handle, we would have to add a new
method to the interface. This would break any plugins that have been
already been written to the interface. We would like to be able to add
new features to the application without having to change the plugin
interface.

Immutable Interfaces
--------------------

When discussing interfaces, you often hear that an interface is an
invariant contract. This is true when considering code that implements
the interface. Adding a method to an interface in truth creates a new
interface. Any existing classes that implemented the old interface are
broken by changing the interface.

As an example, consider our plugin example above. Suppose `IPlugin` is
compiled in its own assembly. We also compile `KillSpamPlugin` in the
assembly `KillSpamPlugin`, which references the `IPlugin` assembly. Now
in our host application, we try and load our plugin. The following
example is for demonstration purposes only.

```csharp
string pluginType  = "KillSpamPlugin, KillSpamPlugin";
Type t = Type.GetType(pluginType);
ConstructorInfo ctor = t.GetConstructor(Type.EmptyTypes);
IPlugin plugin = (IPlugin)ctor.Invoke(null);
```

This works just fine. Now add a method to `IPlugin` and just compile
that assembly. When you run this client code, you get a
`System.TypeLoadException`.

A Loophole In Invariant Interfaces?
-----------------------------------

However in some cases this invariance does not apply to the client code
that references an object via an interface. In this case, there is a bit
of room for change. Specifically, you can add new methods and properties
to that interface without breaking the client code. Of course the code
that implements the interface has to be recompiled, but at least you do
not have to recompile the client.

In the above example, did you notice that we didn’ have to recompile the
application when we changed the `IPlugin` interface? This is true for
two reasons. First, the application does not reference the new method
added to the `IPlugin` interface. If you had changed the existing
signature, there would have been problems. Second, the application
doesn’t implement the interface, so changing it doesn’t require the
application to be rebuilt.

A better approach.
------------------

So how can we apply this to our plugin design? First, we need to look at
our goal. In this case, we want to isolate changes in the application
from the plugin. In particular, we want to make it so that the plugin
interface does not have to change, but allow the application interface
to change.

We can accomplish this by creating a looser coupling between the
application and the plugin interface. One means of doing this is with
events. So rather than having the plugin define various methods that the
application can call, we return to the first plugin definition above
which only has one method, `Initialize` which takes in an instance of
`IApplicationHost`. `IApplicationHost` looks like the following:

```csharp
public interface IApplicationHost
{
    event EventHandler<CommentArgs> CommentReceived;
}
    
//For Demonstration purposes only.
public class CommentArgs : EventArgs
{
    public bool IsSpam;
}
```

Now if we wish to write a spam plugin, it might look like this:

```csharp
public class KillSpamPlugin
{
    public void Initialize(IApplicationHost host)
    {
        host.CommentReceived 
               += new EventHandler<CommentArgs>(OnReceived);
    }

    void OnReceived(object sender, CommentArgs e)
    {
        //It is still all spam to me!
        e.IsSpam = true;
    }
}
```

Now, the application knows very little about a plugin other than it has
a single method. Rather than the application calling methods on the
plugin, plugins simply choose which application events it wishes to
respond to.

This is the loose coupling we hoped to achieve. The benefit of this
approach is that the plugin interface pretty much never needs to change,
yet we can change the application without breaking existing plugins.
Specifically, we are free to add new events to the `IApplicationHost`
interface without problems. Existing plugins will ignore these new
events while new plugins can take advantage of them.

Of course it is still possible to break existing plugins with changes to
the application. By tracking dependencies, we can see that the plugin
references both `IApplicationHost` and `CommentArgs` classes. Any
changes to the signature for an existing property or method in these
classes could break an existing plugin.

Event Overload
--------------

One danger of this approach is that if your application is highly
extensible, `IApplicationHost` could end up with a laundry list of
events. One way around that is to categorize events into groups via
properties of the `IApplicationHost`. Here is an example of how that can
be done:

```csharp
public interface IApplicationHost
{
    UIEventSource UIEvents { get; }
    MessageEventSource MessageEvents { get; }
    SecurityEventSource SecurityEvents { get; }
}

public class UIEventSource
{
    event EventHandler PageLoad;
}

public class SecurityEventSource
{
    event EventHandler UserAuthenticating;
    event EventHandler UserAuthenticated;
}

public class MessageEventSource
{
    event EventHandler Receiving;
    event EventHandler Received;
    event EventHandler Sending;
    event EventHandler Sent;
}    
```

In the above example, I group events into event source classes. This
way, the `IApplicationHost` interface stays a bit more uncluttered.

Caveats and Summary
-------------------

So in the end, having the plugins respond to application events gives
the application the luxury of not having to know much about the plugin
interfaces. This insulates existing plugins from breaking when the
application changes because there is less need for the plugin interface
to change often. Note that I did not cover dealing with strongly typed
assemblies. In that scenario, you may have to take the additional step
of publishing publisher policies to redirect the version of the
application interface that the plugin expects.

