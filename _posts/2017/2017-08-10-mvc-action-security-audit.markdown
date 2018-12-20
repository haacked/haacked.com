---
title: "Auditing ASP.NET MVC Actions"
date: 2017-08-10 -0800
categories: [aspnetmvc,security]
---

> Phil Haack is writing a blog post about ASP.NET MVC? What is this, 2011?

No, do not adjust your calendars. I am indeed writing about ASP.NET MVC in 2017.

It's been a long time since I've had to write C# to put food on the table. My day job these days consists of asking people to put cover sheets on TPS reports. And only one of my teams even uses C# anymore, the rest moving to JavaScript and Electron. On top of that, I'm currently on an eight week leave (more on that another day).

But I'm not completely disconnected from ASP.NET MVC and C#. Every year I spend a little time on a side project I built for a friend. He uses the site to manage and run a yearly soccer tournament.

Every year, it's the same rigmarole. It starts with updating all of the NuGet packages. Then fixing all the breaking changes from the update. Only then do I actually add any new features. At the moment, the project is on ASP.NET MVC 5.2.3.

I'm not ready to share the full code for that project, but I plan to share some interesting pieces of it. The first piece is [a little something I wrote](https://github.com/Haacked/aspnetmvc-action-checker/) to help make sure I secure controller actions.

### The Problem

You care about your users. If not, at least pretend to do so. With that in mind, you want to protect them from potential [Cross Site Request Forgery attacks](https://haacked.com/archive/2009/04/02/anatomy-of-csrf-attack.aspx/). ASP.NET MVC includes helpers for this purpose, but it's up to you to apply them.

By way of review, there are two steps to this. The first step is to update the view and add the anti-forgery hidden input to your HTML form via the `Html.AntiForgeryToken()` method. The second step is to validate that token in the action that receives the form post. Do this by decorating that action method with the   [`[ValidateAntiForgeryToken]`](https://msdn.microsoft.com/en-us/library/system.web.mvc.validateantiforgerytokenattribute.aspx) attribute.

You also care about your data. If you have actions that modify that data, you may want to ensure that the user is authorized to make that change via the [`[Authorize]`](https://msdn.microsoft.com/en-us/library/system.web.mvc.authorizeattribute.aspx) attribute.

This is a lot to track. Especially if you're in a hurry to build out a site. On this project, I noticed I forgot to apply some of these attributes where they should be placed. When I fixed the few places I happened to notice, I wondered what places did I miss?

It would be tedious to check every action by hand. So I automated it. I wrote a simple controller action that reflects over every controller action. It then displays all the actions that might need one of these attributes.

Here's a screenshot of it in action.

![Screenshot of Site Checker in action](https://user-images.githubusercontent.com/19977/29151000-0fea13e0-7d33-11e7-8f36-bfb57e0fef94.png)

There's a few important things to note.

### Which actions are checked?

The checker looks for all actions that might modify an HTTP resource. In other words, any action that responds to the following HTTP verbs: `POST`, `PUT`, `PATCH`, `DELETE`. In code, these correspond to action methods decorated with the following attributes: `[HttpPost]`, `[HttpPut]`, `[HttpPatch]`, `[HttpDelete]` respectively. The presence of these attributes are good indicators that the action method might modify data. Action methods that respond to GET requests should never modify data.

### Do all these need to be secured?

No.

For example, it wouldn't make sense  to decorate your `LogOn` action with `[Authorize]` as that violates causality. You don't want to require users to be already authenticated before the log in to your site. That's just silly sauce.

There's no way for the checker to understand the semantics of your action method code to determine whether an action should be authorized or not. So it just lists everything it finds. It's up to you to figure out if there's any action (no pun intended) required on your part.

### How do I deploy it?

All you have to do is copy and paste [this `SystemController.cs` file](https://raw.githubusercontent.com/Haacked/aspnetmvc-action-checker/master/SystemController.cs) into your ASP.NET MVC project. It just makes it easier to compile this into the same assembly where your controller actions exist.

Next, make sure there's a route that'll hit the `Index` action of the `SystemController`. If you have the default route that ASP.NET MVC project templates include present, you would visit this at _/system/index_.

Be aware that if you accidentally deploy `SiteController`, it will only responds to local requests (requests from the hosting server itself) and not to public requests. You really don't want to expose this information to the public. That would be an open invitation to be hacked. You may like being Haacked, it's no fun to be hacked.

And that's it.

### How's it work?

I kept all the code in a single file, so it's a bit ugly, but should be easy to follow.

The key part of the code is how I obtain all the controllers.

```csharp
var assembly = Assembly.GetExecutingAssembly();

var controllers = assembly.GetTypes()
    .Where(type => typeof(Controller).IsAssignableFrom(type)) //filter controllers
    .Select(type => new ReflectedControllerDescriptor(type));
```

The first part looks for all types in the currently executing assembly. But notice that I wrap each type with a [`ReflectedControllerDescriptor`](https://msdn.microsoft.com/en-us/library/system.web.mvc.reflectedcontrollerdescriptor.aspx). That type contains the useful `GetCanonicalActions()` method to retrieve all the actions.

It would have been possible for me to get all the action methods without using `GetCanonicalActions` by calling `type.GetMethods(...)` and filtering the methods myself. But `GetCanonicalActions`is a much better approach since it encapsulates the same logic ASP.NET MVC uses to locate actions.

As such, it handles cases such as when an action method is named differently from the underlying class method via the `[ActionName("SomeOtherMethod")]` attribute.

### What's Next?

There's so many improvements we could make (notice how I'm using "we" in a bald attempt to pull you into this?) to this. For example, the code only looks at the `HTTP*` attributes. But to be completely correct, it should also check the [`[AcceptVerbs]`](https://msdn.microsoft.com/en-us/library/system.web.mvc.acceptverbsattribute.aspx) attribute. I didn't bother because I never use that attribute, but maybe you have some legacy code that does.

Also, there might be other things you want to check. For example, what about [mass assignment attacks](http://odetocode.com/blogs/scott/archive/2012/03/11/complete-guide-to-mass-assignment-in-asp-net-mvc.aspx)? I didn't bother because I tend to use input models for my action methods. But if you use the [`[Bind]`](https://msdn.microsoft.com/en-us/library/system.web.mvc.bindattribute.aspx) attribute, you might want this checker to look for issues there.

Well that's great. I don't plan to spend a lot of time on this, but I'd be happy to accept your contributions! The [source is on GitHub](https://github.com/Haacked/aspnetmvc-action-checker).

Let me know if this is useful to you or if you use something better.
