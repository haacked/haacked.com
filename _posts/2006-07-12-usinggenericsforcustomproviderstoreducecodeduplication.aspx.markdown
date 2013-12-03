---
layout: post
title: "Using Generics For Custom Providers To Reduce Code Duplication"
date: 2006-07-12 -0800
comments: true
disqus_identifier: 13917
categories: []
---
Here is a quick little nugget for you custom provider implementers. I
recently scanned through [this
article](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnaspp/html/ASPNETProvMod_Prt8.asp "Custom Provider-Based Services")
on MSDN that describes how to implement a custom provider and found some
areas for improvement.

Reading the section *Loading and Initializing Custom Providers* I soon
encountered a bad smell. No, it was not my upper lip, but rather a [code
smell](http://www.codinghorror.com/blog/archives/000589.html "code smell").
Following the samples when implementing custom providers would lead to a
lot of duplicate code.

It seemed to me that much of that code is very generic. Did I just say
*generics*?

[Simone (*blog in
Italian*)](http://blogs.ugidotnet.org/piyo/ "Simone Chiaretta"), a
Subtext developer, recently refactored all our Providers to inherit from
the Microsoft `ProviderBase` class.

One of the first things he did was to create a generic provider
collection:

```csharp
using System;
using System.Configuration.Provider;

public class GenericProviderCollection<T> 
    : ProviderCollection 
    where T : System.Configuration.Provider.ProviderBase
{

    public new T this[string name]
    {
        get { return (T)base[name]; }
    }

    public override void Add(ProviderBase provider)
    {
        if (provider == null)
            throw new ArgumentNullException("provider");

        if (!(provider is T))
            throw new ArgumentException
                ("Invalid provider type", "provider");

        base.Add(provider);
    }
}
```

That relatively small bit of code should keep you from having to write a
bunch of cookie cutter provider collections. But there is more that can
be done. Take a look at the `LoadProviders` in Listing 6 of [that
article](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnaspp/html/ASPNETProvMod_Prt8.asp "Custom Provider-Based Services").

There are two things that bother me about that method listing. First is
the unnecessary double check locking, which Richter poo poos in his book
*[CLR via
C\#](http://www.microsoft.com/MSPress/books/6522.asp "CLR via C#")*. The
second is the fact that this method is begging for code re-use. I
created a static helper class with the following method to encapsulate
this logic (apologies for the weird formatting. I want it to fit
width-wise):

```csharp
/// <summary>
/// Helper method for populating a provider collection 
/// from a Provider section handler.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <returns></returns>
public static GenericProviderCollection<T> 
        LoadProviders<T>(string sectionName, out T provider) 
            where T : ProviderBase
{
    // Get a reference to the provider section
    ProviderSectionHandler section = 
        (ProviderSectionHandler)WebConfigurationManager
              .GetSection(sectionName);

    // Load registered providers and point _provider
    // to the default provider
    GenericProviderCollection<T> providers = new 
          GenericProviderCollection<T>();
    ProvidersHelper.InstantiateProviders
          (section.Providers, providers, typeof(T));

    provider = providers[section.DefaultProvider];
    if (provider == null)
        throw new ProviderException(
            string.Format(
                  "Unable to load default '{0}' provider", 
                        sectionName));
    
    return providers;
}
```

This method returns a collection of providers for the specified section
name. It also returns the default provider via an `out` parameter. So
now, within my custom provider class, I can let the static constructor
instantiate the provider collection and set the default provider in one
fell swoop like so:

```csharp
public abstract class SearchProvider : ProviderBase
{
    private static SearchProvider provider = null;
    private static GenericProviderCollection<SearchProvider> 
       providers = ProviderHelper.LoadProviders<SearchProvider>
           ("SearchProvider", out provider);
}
```

By employing the power of generics, writing new custom providers with a
minimal amount of code is a snap. Hope you find this code helpful.

