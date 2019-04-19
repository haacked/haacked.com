---
title: Tiny Trick For ViewState Backed Properties
tags: [aspnet]
redirect_from:
- "/archive/2006/08/06/tinytrickforviewstatebackedproperties.aspx/"
- "/archive/2006/08/07/TinyTrickForViewStateBackedProperties.aspx/"
---

This might be almost too obvious for many of you, but I thought I’d
share it anyways. Back in the day, this was the typical code I would
write for a value type property of an ASP.NET `Control` that was backed
by the `ViewState`.

```csharp
public bool WillSucceed
{
    get
    {
        if (ViewState["WillSucceed"] == null)
            return false;
        return (bool)ViewState["WillSucceed"];
    }
    set
    {
        ViewState["WillSucceed"] = value;
    }
}
```

I have seen code that tried to avoid the null check in the getter by
initializing the property in the constructor. But since the getters and
setters for the `ViewState` are virtual, this violates the warning
against calling virtual methods in the constructor. You also can’t
initialize it in the `OnInit` method because the property might be set
declaratively which happens before `Init`.

With C# 2.0 out, I figured I could use the null coalescing operator to
produce cleaner code. Here is what I naively tried.

```csharp
public bool WillSucceed
{
    get
    {
        return (bool)ViewState["WillSucceed"] ?? false;
    }
    set
    {
        ViewState["WillSucceed"] = value;
    }
}
```

Well of course that won’t compile. It doesn’t make sense to apply the
null coalescing operator on a value type that is not nullable. Now if I
had stopped to think about it for a second, I would have realized how
simple the fix would be, but I was in a hurry and quickly moved on and
dropped the issue. What an eeediot! All I had to do was move the cast
outside of the expression.

```csharp
public bool WillSucceed
{
    get
    {
        return (bool)(ViewState["WillSucceed"] ?? false);
    }
    set
    {
        ViewState["WillSucceed"] = value;
    }
}
```

I am probably the last one to realize this improvement and everyone
reading this is thinking, “well duh!”. But in case there is someone out
there even slower than me, here you go!

And if I spend this much time trying to write a property, you gotta
wonder how I get anything done. ;)

