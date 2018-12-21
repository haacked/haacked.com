---
title: Does Holding A Delegate Reference Keep The Owning Object Alive?
date: 2004-07-06 -0800
tags: [dotnet]
redirect_from: "/archive/2004/07/05/does-holding-a-delegate-reference-keep-the-owning-object-alive.aspx/"
---

I have a big question that can probably be best elucidated via some
code:

```csharp
public class SomeClass
{
    // This guy will raise an important event.
    private EventSource _source = new EventSource();

    public void AttachEventHandler()
    {
        // This guy will handle an important event...
    BigEventListener listener = new BigEventListener();
    _source.BigEvent += new EventHandler(listener.OnBigEvent);

    //What happens to listener instance here?
    //Will it be garbage collected?

    }
}
```

So what happens after the method `AttachEventHandler()` is called? I am
assuming that the EventHandler delegate's reference to the OnBigEvent
method of the listener instance is a hard reference. In other words,even
though listener is a local instance and would normally go out of scope
when AttachEventHandler ends, that the listener instance is not
collected because of the delegate reference. Is this correct?

