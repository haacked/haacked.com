---
title: Question For You Dependency Injection Buffs
date: 2007-11-27 -0800
disqus_identifier: 18425
tags:
- code
- tdd
redirect_from: "/archive/2007/11/26/question-for-you-dependency-injection-buffs.aspx/"
---

I’m currently doing some app building with ASP.NET MVC in which I try to
cover a bunch of different scenarios. One scenario in particular I
wanted to cover is approaching an application using a Test Driven
Development approach. I especially wanted to cover using various
Dependency Injection frameworks, to make sure everything plays nice.

Since I’ve already seen demos with [Castle
Windsor](http://www.castleproject.org/container/index.html "Windsor Container")
and
[Spring.NET](http://www.springframework.net/ "Spring.net application framework"),
I wanted to give
[StructureMap](http://structuremap.sourceforge.net/Default.htm "StructureMap")
a try. Here is the problem I’ve run into.

Say I have a class like so:

```csharp
public class MyController : IController
{
  MembershipProvider membership;
  public HomeController(MembershipProvider provider)
  {
    this.membership = provider;
  }
}
```

As you can see, this class has a dependency on the abstract
`MembershipProvider` class, which is passed to this class via a
constructor argument. In my unit tests, I can use RhinoMocks to
dynamically create a mock that inherits `MembershipProvider` provider
and pass that mock to this controller class. It’s nice for testing.

But eventually, I need to use this class in a real app and I would like
a DI framework container to create the controller for me. Here is my
StructureMap.config file with some details left out.

```csharp
<?xml version="1.0" encoding="utf-8" ?>
<StructureMap>
  <PluginFamily Type="IController" DefaultKey="HomeController"
      Assembly="...">
    <Plugin Type="HomeController" ConcreteKey="HomeController"
        Assembly="MvcApplication" />
  </PluginFamily>
</StructureMap>
```

If I add an empty constructor to `HomeController`, this code allows me
to create an instance of `HomeController` like so.

```csharp
HomeController c = 
  ObjectFactory.GetNamedInstance<IController>("HomeController")
  as HomeController;
```

But when I remove the empty constructor, StructureMap cannot create an
instance of `HomeController`. I would need to tell StructureMap (via
StructureMap.config) how to construct an instance of
`MembershipProvider` to pass into the constructor for `HomeController`

Normally, I would just specify a type to instantiate as another
`PluginFamily` entry. But what I really want to happen in this case is
for StructureMap to call a method or delegate and use the value returned
as the constructor argument.

In other words, I pretty much want something like this:

```csharp
<?xml version="1.0" encoding="utf-8" ?>
<StructureMap>
  <PluginFamily Type="IController" DefaultKey="HomeController"
      Assembly="...">
    <Plugin Type="HomeController" ConcreteKey="HomeController"
        Assembly="MvcApplication">
      <Instance>
        <Property Name="provider">
          <![CDATA[
            return Membership.Provider;
          ]]>
        </Property>
      </Instance>
    </Plugin>
  </PluginFamily>
</StructureMap>
```

The made up syntax I am using here is stating that when StructureMap is
creating an instance of `HomeController`, execute the code in the CDATA
section to get the instance to pass in as the constructor argument named
provider.

**Does anyone know if something like this is possible with any of the
Dependency Injection frameworks out there?**Whether via code or
configuration?

