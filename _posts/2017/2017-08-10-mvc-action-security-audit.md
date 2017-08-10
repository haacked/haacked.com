---
layout: post
title: "Auditing ASP.NET MVC Actions"
date: 2017-08-10 -0800
comments: true
categories: [aspnetmvc security]
---

> Phil Haack is writing a blog post about ASP.NET MVC? What is this, 2011?

No, do not adjust your calendars. I am indeed writing about ASP.NET MVC.

It's true, my day job these days consists of asking people to put cover sheets on TPS reports. It's been a long time since I've had to write C# to put food on the table.

But, every year I spend a little time on a side project I built for a friend to run a yearly soccer tournament. It seems I spend more time updating all my NuGet packages than I do adding new features. At the moment, the project is on ASP.NET MVC 5.2.3.

I'm not ready to share the full code for that project, but I plan to share some interesting pieces of it. The first piece is [a little something I wrote](https://github.com/Haacked/aspnetmvc-action-checker/) to help make sure I secure controller actions.

### The Problem

It's a good idea to protect your users from potential [Cross Site Request Forgery attacks](http://haacked.com/archive/2009/04/02/anatomy-of-csrf-attack.aspx/). ASP.NET MVC includes helpers for this purpose, but it's up to you to apply them.

By way of review, there are two steps to this. The first step is to update the view and add the anti-forgery hidden input to your HTML form via the `Html.AntiForgeryToken()` method. The second step is to validate that token in the action that receives the form post. Do this by decorating that action method with the   [`[ValidateAntiForgeryToken]`](https://msdn.microsoft.com/en-us/library/system.web.mvc.validateantiforgerytokenattribute.aspx) attribute.

If you have actions that modify data, you may want to ensure that the user is authorized to make that change via the [`[Authorize]`](https://msdn.microsoft.com/en-us/library/system.web.mvc.authorizeattribute.aspx) attribute.

This can be a lot to keep track of. Especially if you're in a hurry to build out a site. On this side project, I noticed I forgot to apply some of these attributes where they belong. I didn't feel like checking every single action manually, so I automated it. I wrote a simple controller action that reflects over every controller action. It then displays all the actions that might need one of these attributes.

Here's a screenshot of it in action.

![Screenshot of Site Checker in action](https://user-images.githubusercontent.com/19977/29151000-0fea13e0-7d33-11e7-8f36-bfb57e0fef94.png)

There's a couple of important things to note.

### Which actions are checked?

The checker looks for all actions that might modify an HTTP resource. In other words, any action that responds to the following HTTP verbs: `POST`, `PUT`, `PATCH`, `DELETE`. In code, these correspond to action methods decorated with the following attributes: `[HttpPost]`, `[HttpPut]`, `[HttpPatch]`, `[HttpDelete]` respectively. The presence of these attributes are good indicators that the action method might modify data. Action methods that respond to GET requests should never modify data.

### Do all these need to be secured?

No.

For example, it wouldn't make sense  to decorate your `LogOn` action with `[Authorize]` as that violates causality. You don't want to require users to be already authenticated before the log in to your site. That's just silly sauce.

There's no way for my checker to understand  the semantics of your action method code. So it just lists everything it finds. It's up to you to figure out if there's any action required.

### How do I deploy it

All you have to do is copy and paste [this `SystemController.cs` file](https://raw.githubusercontent.com/Haacked/aspnetmvc-action-checker/master/SystemController.cs) into your ASP.NET MVC project. It just makes it easier to compile this into the same assembly where your controller actions exist.

Next, make sure there's a route that'll hit the `Index` action of the `SystemController`. If you have the default route that ASP.NET MVC project templates include present, you would visit this at _/system/index_.

And that's it.

### How's it work?

The code is pretty straightforward. It's a little ugly since I tried to keep it all in a single file.

The key part of the code is how I obtain all the controllers.

```csharp
var assembly = Assembly.GetExecutingAssembly();

var controllers = assembly.GetTypes()
    .Where(type => typeof(Controller).IsAssignableFrom(type)) //filter controllers
    .Select(type => new ReflectedControllerDescriptor(type));
```

The first part looks for all types in the currently executing assembly. But notice that I wrap each type with a [`ReflectedControllerDescriptor`](https://msdn.microsoft.com/en-us/library/system.web.mvc.reflectedcontrollerdescriptor.aspx). That type contains the useful `GetCanonicalActions()` methods. The benefit of that is it handles cases such as when an action method is named differently from the underlying class method via the `[ActionName("SomeOtherMethod")]` attribute.

### What's Next?

There's so many improvements we could make (notice how I'm using "we" in a bald attempt to pull you into this?) to this. For example, the code only looks at the `HTTP*` attributes. But to be completely correct, it should also check the [`[AcceptVerbs]`](https://msdn.microsoft.com/en-us/library/system.web.mvc.acceptverbsattribute.aspx) attribute for completeness. I didn't bother because I never use that attribute, but maybe you have some legacy code that does.

Also, there might be other things you want to check. For example, what about [mass assignment attacks](http://odetocode.com/blogs/scott/archive/2012/03/11/complete-guide-to-mass-assignment-in-asp-net-mvc.aspx)? I didn't bother because I tend to use input models for my action methods. But if you use the [`[Bind]`](https://msdn.microsoft.com/en-us/library/system.web.mvc.bindattribute.aspx) attribute, you might want this checker to look for issues there.

Well that's great. I don't plan to spend a lot of time on this, but I'd be happy to accept your contributions! The [source is on GitHub](https://github.com/Haacked/aspnetmvc-action-checker).
