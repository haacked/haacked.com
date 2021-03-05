---
title: Delegating Decorators
tags: [aspnetmvc]
redirect_from: "/archive/2008/06/17/delegating-decorators.aspx/"
---

When approaching an extensibility model, I often find cases in which I
want to merely tweak the existing behavior of the default implementation
and wish I didn’t have to create a whole new specific type to do so.

[![paint](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Delegating-Decorators_DE74/paint_3.jpg "paint")](http://www.sxc.hu/photo/61224/ "Paint and brush by Pam Roth")

Instead of creating a specific type, I tend to write a decorator class
that implements the interface and takes in both the default instance of
that interface and a delegate (specified using a lambda of course).

Let’s look at a quick example to make all this abstract talk more
concrete. I’m playing around with the NVelocity View Engine for ASP.NET
MVC from the [MvcContrib
project](http://www.codeplex.com/MVCContrib "MvcContrib on CodePlex").
Rather than have each controller specify the view engine, I’d really
like to specify this in just one place.

I could take the time to setup a DI container, but I’m feeling lazy and
this is just a simple prototype application so I planned to just
implement the `IControllerFactory` interface and have it set the view
engine after creating the controller.

For those not intimately acquainted with ASP.NET MVC, you can override
how the framework instantiates controllers by implementing the
`IControllerFactory` interface.

However, at this point, I realized I was creating these one-off
controller factories all the time. What I really wanted was some way to
decorate the *existing* controller factory with a bit of extra logic.
What I did was write an extension method that allowed me to do the
following in my Global.asax.cs file.

```csharp
ControllerBuilder.Current.Decorate(
  (current, context, controllerName) => 
  {
    var controller = current.CreateController(context, controllerName) 
      as Controller;
    if (controller != null) {
      controller.ViewEngine = new NVelocityViewFactory();
    }
    return controller;
  }
);
```

It’s a little funky looking, but what is happening here is that I am
calling a new `Decorate` method and passing it a lambda. That lambda
will be used to decorate or wrap the current controller factory and will
get called when it is time to instantiate a `Controller` instance.

However, the lambda always receives the *original* controller factory so
it can use it if needed. So in effect, the lambda *wraps* or *decorates*
whatever the current controller factory happens to be.

In this case, all my lambda is doing is using the default controller to
create the controller, and then it sets a property of that controller
after the fact. However, if I wanted to, I could have had my lambda
completely override creating the controller.

Here is the the code for the extension method to `ControllerBuilder`.

```csharp
public static class ControllerFactoryExtensions {
  public static void Decorate(this ControllerBuilder builder
  ,Func<IControllerFactory, RequestContext, string, IController> decorator) {
    IControllerFactory current = builder.GetControllerFactory();
    builder.SetControllerFactory(
      new DelegatingControllerFactory(current, decorator));
  }
}
```

As you can see, under the hood, I am actually replacing the current
controller factory with a new one called `DelegatingControllerFactory`.
But the implementation for this new factory is really simple. It simply
calls a delegate that you supply. As far as the user of the Decorate
method is concerned, this class doesn’t really exist.

```csharp
internal class DelegatingControllerFactory : IControllerFactory {
  Func<IControllerFactory, RequestContext, string, IController> _factory;
  IControllerFactory _wrappedFactory;

  public DelegatingControllerFactory(IControllerFactory current
    , Func<IControllerFactory
    , RequestContext, string, IController> factoryDelegate) {
    if (factoryDelegate == null) {
      throw new ArgumentNullException("factoryDelegate");
    }
    _wrappedFactory = current;
    _factory = factoryDelegate;
  }

  IController IControllerFactory.CreateController(RequestContext context
  , string controllerName) {
      return _factory(_wrappedFactory, context, controllerName);
  }

  void IControllerFactory.DisposeController(IController controller) {
    if (controller is IDisposable) {
      ((IDisposable)controller).Dispose();
    }
  }
}
```

With this in place, the next time I need to implement a one-off
controller factory, I can simply decorate the current controller factory
instead.

I’m starting to find myself using this pattern in a lot of places. The
potential downside of this approach is that if someone else comes along
who has to maintain it, they might find it difficult to understand if
they’re not well acquainted with lambdas and delegates.

So it is a bit of a tradeoff between convenience for the code author and
readability for the code reader.
