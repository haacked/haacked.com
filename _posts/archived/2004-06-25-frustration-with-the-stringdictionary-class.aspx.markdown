---
layout: post
title: "Frustration with the StringDictionary class."
date: 2004-06-25 -0800
comments: true
disqus_identifier: 685
categories: []
---
Today I ran into an annoying nuance of the `StringDictionary` class
(located in the `System.Collections.Specialized` namespace so as not to
be confused with the other imposter string dictionaries). It's not a
bug, but I feel the API for it could have been slightly better on this
minor point.

Right there in the
[documentation](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfsystemcollectionsspecializedstringdictionaryclasstopic.asp)
for the class it points out that:

> The key is handled in a case-insensitive manner; it is translated to
> lower case before it is used with the string dictionary.

Thus it takes the key you give it, and changes it to a lower case value
before storing it. I verified this with
[Reflector](http://www.aisto.com/roeder/dotnet/).

``

```csharp
public virtual void Add(string key, string value)
{
      this.contents.Add(key.ToLower(CultureInfo.InvariantCulture), value);
}
```

Whoa there! Stop the presses! Call Michael Moore! Ok, ok. So in the
grand scheme of things, it's a minor issue. It's not a huge deal.
However, API usability (and language design) focuses on keeping the
minor annoyances to a minimum. Otherwise they'll add up and form a major
disturbance and soon you'll have an angry developer standing on his/her
soap box whining and ranting about it on a blog.

The issue for me is that this wasn't the behavior I expected. If an API
is going to change data you give it, I wish it would indicate that
somehow. Perhaps the method could be
`AddKeyAndValueButLowerCaseTheKey()` ;). All joking aside, it isn't
necessary to lower-case the key before storing it in order to provide
case-insensitive storage. Internally, `StringDictionary` uses a
`HashTable` to store its keys and values. The dictionary could have made
the `HashTable` use a `CaseInsensitiveHashCodeProvider` when storing
keys. That way the key doesn't have to change, but you get case
insensitivity.

In any case, like I said, it's normally not a big deal except for the
fact that I am building a Setup and Deployment project within Visual
Studio.NET for a Windows service. When building a project installer, the
`Installer` (in the namespace System.Configuration.Install) class
provides a property called `Context` of type `InstallContext`. This
class gives you a property called `Parameters` which is a
`StringDictionary` containing values you may have pulled from the user
interface.

I'm trying to look up nodes within an XML file that correspond to the
keys of the `Parameters` dictionary using XPath, hoping to update the
node values with the values from the dictionary. However, XPath is case
sensitive so I can't match these things up. Unfortunately I can't change
the XML to be all lower case, and I can't change the `Installer` class
to not use a `StringDictionary` (or better yet, correct the dictionary's
behavior), so I'm stuck with resorting to a hack until something better
comes along.

