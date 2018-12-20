---
title: Resharper Pet Peeve
date: 2004-08-10 -0800
tags: []
redirect_from: "/archive/2004/08/09/resharper-pet-peeve.aspx/"
---

One pet peeve I have is how the auto completion works regarding methods.
I know this is nitpicky, but I'm wondering how other IDE's that try to
help you handle this. Suppose I type out the following and am about to
hit the open parenthesis character.

```csharp
public void SomeMethod
```

Resharper automatically adds a closing parenthesis and puts your cursor
in between them.

```csharp
public void SomeMethod(Cursor Is Here)
```

Now the reason I don't like this is after I'm done typing the arguments,
I still have to type a character (either a closing parenthesis which it
absorbs or a right arrow key) and then type my open brace. I'd prefer it
if it didn't add the closing parenthesis and instead, when I did close
the parenthesis, it would automatically add the open and close brace.
Thus the sequence would look like:

```csharp
public void SomeMethod(object someParam 
[about to type closing parens]
```

to

```csharp
public void SomeMethod(object someParam)
{
    Braces added automatically and Cursor is Here
}
```

This makes more sense to me as this is where I'm going to be spending
more of my time. In any case, if I'm just being a whiner, I can live
with that. I haven't played enough with Whidbey or other IDE
enhancements to know how they handle this situation. It's the little
things that count.

