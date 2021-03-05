---
title: Resilient Plugins Part Deux - Granular Control
tags: [code,extensibility]
redirect_from: "/archive/2006/06/30/ResilientPluginsPartDeuxGranularControl.aspx/"
---

![Plug](https://haacked.com/assets/images/plug2.jpg) I got a lot of great
feedback from my post on [Building Plugins Resilient to
Versioning](https://haacked.com/archive/2006/06/26/BuildingPluginsResilientToVersioning.aspx "Building Resilient Plugins"),
which proposes an event-based self-subscription model to plugins.

[Craig Andera](http://pluralsight.com/blogs/craig/ "Craig Andera")
points out that we can get many of the same benefits by having plugins
implement an abstract base class instead of an interface. This is
definitely a workable solution and is probably isomorphic to this event
based approach.

[Dimitri Glazkov](http://glazkov.com/ "Subtext Contributor") was the
voice of dissent in the comments to the post pointing out that the
application loses granular control over plugins in this approach. I was
not convinced at the time as I was focused on keeping the surface area
of the plugin interface that is exposed to the application very small.
When the surface area is small, there is less reason for the interface
to change and the less reason to break the interface.

However a simple thought experiment makes me realize that we do need to
have the application retain granular control over which plugins can
respond to which events. This is the scenario.

Suppose our plugin framework defines three events, `MessageSending`,
`MessageSent`, `MessageReceiving` and someone writes a plugin that
responds to all three events. Later, someone else writes a plugin that
only responds to `MessageReceiving`. If the blog user wants to chain the
functionality of that plugin to the existing plugin, so that both fire
when a message is received, then all is well.

But suppose this new plugin’s handling of the `MessageReceiving` event
should replace the handling of the old plugin. How would we do this? We
can’t just remove the old plugin because then we lose its handling of
the other two events. Dimitri was right all along on this point, we need
more granular control.

It makes sense to have some sort of admin interface in which we can
check and uncheck individual plugins and whether or not they are allowed
to respond to specific events. However, this is not too difficult with
the event based approach.

.NET’s event pattern is really an implementation of the Observer
pattern, but using delegates rather than interfaces. After all, what is
a delegate under the hood but yet another class? When any code attaches
a method to an event, it is in effect registering a callback method with
the event source. This is the step where we can obtain more granular
information about our plugins.

In the Application that hosts the plugin, events that require this
granular control (not every event will) could be defined like so.

```csharp
private event EventHandler messageReceived;

public event EventHandler MessageReceived
{
    add
    {
        RegisterPlugin(value.Method.DeclaringType);
        AddEvent(value);
    }
    
    remove
    {
        UnRegisterPlugin(value.Method.DeclaringType);
        RemoveEvent(value);
    }
}
```

So when adding and removing the event, we register the plugin with the
system and then we add the event to some internal structure. For the
purposes of this discussion, I’ll present some simple implementations.

```csharp
void AddEvent(EventHandler someEvent)
{
    //We could choose to add the event 
    //to a hash table or some other structure
    this.messageReceived += someEvent;
}

void RemoveEvent(EventHandler someEvent)
{
    this.messageReceived -= someEvent;
}
                
private void RegisterPlugin(Type type)
{
    //using System.Diagnostics;
    StackTrace stack = new StackTrace();
    StackFrame currentFrame = stack.GetFrame(1);
    Console.WriteLine("Registering: " + type.Name 
         + " to event " + currentFrame.GetMethod().Name);
}

private void UnRegisterPlugin(Type type)
{
    StackTrace stack = new StackTrace();
    StackFrame currentFrame = stack.GetFrame(1);

    Console.WriteLine("UnRegistering: " + type.Name 
        + " to event " + currentFrame.GetMethod().Name);
}
```

As stated in the comments, the `AddEvent` method attaches the event
handler in the standard way. I could have chosen to put it in a hash
table or some other structure. Perhaps in a real implementation I would.

The `RegisterPlugin` method examines the call stack so that it knows
which event to register. In a real implementation this would probably
insert or update some record in a database somewhere so the application
knows about it. Note that this should happen when the application is
starting up or sometime before the user can start using the plugin.
Otherwise there is no point to having access control.

```csharp
public void OnMessageReceived()
{
    EventHandler messageEvent = this.messageReceived;
    if(messageEvent != null)
    {
        Delegate[] delegates = messageEvent.GetInvocationList();
        foreach(Delegate del in delegates)
        {
            if (EnabledForEvent(del.Method.DeclaringType, 
                "MessageReceived"))
            {
                del.DynamicInvoke(this, EventArgs.Empty);
            }
        }
    }
}
```

Now, when we invoke the event handler, instead of simply invoking the
event, we examine the delegate chain (depending on how we store the
event handlers) and dynamically invoke only the event handlers that we
allow. How is that for granular control?

In this approach, the implementation for the application host is a bit
more complicated, but that complexity is totally hidden from the plugin
developer, as it should be.

