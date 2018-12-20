---
title: Dealing with singular plural phrasing
date: 2011-08-16 -0800
disqus_identifier: 18808
tags:
- code
redirect_from: "/archive/2011/08/15/dealing-with-singular-plural-phrasing.aspx/"
---

This is an age old problem and one that’s probably been solved countless
times before, but I’m going to write about it anyways.

Say you’re writing code like this:

```csharp
<p>You have the following themes:</p>
<ul>
@foreach(var theme in Model) {
  <li>@theme.Id</li>
}
</ul>
```

The natural inclination for the lazy developer is to leave enough alone
and stop there. It’s good enough, right? Right?

Sure, when the value of `Model.Count` is zero or larger than one. But
when it is exactly one, the phrase is incorrect English as it should be
singular “You have the following theme”.

I must fight my natural inclination here! On the NuGet team, we have a
rallying cry intended to inspire us to strive for better, “Apple
Polish!”. We tend to blurt it out at random in meetings. I’m thinking of
purchasing each member of the team a WWSJD bracelet (What Would Steve
Jobs Do?).

To handle this case, I wrote a simple extension method:

```csharp
public static string CardinalityLabel(this int count, string singular,
    string plural)
{
    return count == 1 ? singular : plural;
}
    
```

Notice that I didn’t try automatic pluralization. That’s just a pain.

With this method, I can change the markup to say:

```csharp
<p>You have the following 
  @Model.Count.CardinalityLabel("theme", "themes"):</p>
```

I’m still just not sure about the name of that method though. What
should it be?

Do you have such a method? Or are you fine with the awkward phrasing
once in a while?

