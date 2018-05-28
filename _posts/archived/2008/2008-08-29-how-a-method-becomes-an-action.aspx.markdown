---
layout: post
title: How a Method Becomes An Action
date: 2008-08-29 -0800
comments: true
disqus_identifier: 18528
categories:
- asp.net mvc
- asp.net
redirect_from: "/archive/2008/08/28/how-a-method-becomes-an-action.aspx/"
---

This is one of them “[coming of
age](http://en.wikipedia.org/wiki/Coming_of_age "Coming of age")”
stories about how a lowly method becomes a full fledged Action in
ASP.NET MVC. You might think the two things are the same thing, but
that’s not the case. It is not just any method gets to take the mantle
of being an Action method.

### Routing

Like any good story, it all begins at the beginning with Routing. By
default, one of the routes defined in the MVC project template has the
following URL pattern:

> `     `
>
> {controller}/**{action}**/{id}

When a request comes in and matches that route, we populate a dictionary
of route values (accessible via the `RequestContext`) based on this
route. For example, if a request comes in for:

> /home/**list**/123

We add the key “action” with the value “list” to the route values
dictionary (*We’ve also added “home” as the value for “controller”, but
that’s for another story. This is the story of the action.*) At the
heart of it, an action is just a string. That’s how it starts out after
all, as a sub string of the URL.

Later on, when the request is handed of to MVC, MVC interprets the value
in the route values for “action” to be the action name. In this case, it
knows that the request should be handled by the action “list”.
**Contrary to popular belief, this does not necessarily mean that a
method named `List` will handle this request,**as we’ll soon see.

### Action Method Selection

Once we’ve identified the name of the action, we need to identify a
method that can respond to that action. This is the job of the
`ControllerActionInvoker`.

By default, the invoker simply uses reflection to find a public method
on a class that derives from `Controller` which has the same name (case
insensitive) as the current action.

Like many things within this framework, you can tweak this default
behavior.

### ActionNameAttribute

Introduced in [ASP.NET MVC CodePlex Preview 5 which we just
released](https://haacked.com/archive/2008/08/29/asp.net-mvc-codeplex-preview-5-released.aspx "ASP.NET MVC CodePlex Preview 5"),
applying this attribute to a method allows you to specify the action
that the method handles.

For example, suppose you want to have an action named `View`, this would
conflict with the `View` method of Controller. An easy way to work
around this issue without having to futz with routing or method hiding
is to do the following:

```csharp
[ActionName("View")]
public ActionResult ViewSomething(string id)
{
  return View();
}
```

The `ActionNameAttribute` redefines the name of this action to be
“View”. Thus this method is invoked in response to requests for
`/home/view`, but not for `/home/viewsomething`. In the latter case, as
far as the action invoker is concerned, an action method named
“ViewSomething” *does not exist*.

One consequence of this is that if you’re using our conventional
approach to locate the view that corresponds to this action, **the view
should be named after the action, not after the method**. In the above
example (assuming this is a method of `HomeController`), we would look
for the view `~/Views/Home/View.aspx` by default.

This attribute is not required on an action method. Implicitly, the name
of a public method serves as the action name for that method.

### ActionSelectionAttribute

We’re not done yet matching the action to a method. Once we’ve
identified all methods of the `Controller` class that match the current
action name, we need to whittle the list down further by looking at all
instances of the `ActionSelectionAttribute` applied to the methods in
the list.

This attribute is an abstract base class for attributes which provide
fine grained control over which requests an action method can respond
to. The API for this method is quite simple and consists of a single
method.

```csharp
public abstract class ActionSelectionAttribute : Attribute
{
  public abstract bool IsValidForRequest(ControllerContext controllerContext
    , MethodInfo methodInfo);
}
```

At this point, the invoker looks for any methods in the list which
contain attributes which derive from this attribute and calls the
`IsValidForRequest()` method on each attribute. If any attribute returns
false, the method that the attribute is applied to is removed from the
list of potential action methods for the current request.

At the end, we should be left with one method in the list, which the
invoker then invokes. If more than one method can handle the current
request, the invoker throws an exception indicating the problem. If no
method can handle the request, the invoker calls `HandleUnknownAction()`
on the controller.

The ASP.NET MVC framework includes one implementation of this base
attribute, the `AcceptVerbsAttribute`.

### AcceptVerbsAttribute

This is a concrete implementation of `ActionSelectionAttribute` which
uses the current HTTP request’s http method (aka verb) to determine
whether or not a method is the action that should handle the current
request.

This allows for having two methods of the same name (different
parameters of course) to both be actions, but respond to different HTTP
verbs.

For example, we may want two versions of the Edit method, one which
renders the edit form, and the other which handles the request when that
form is posted.

```csharp
[AcceptVerbs("GET")]
public ActionResult Edit(string id)
{
  return View();
}

[AcceptVerbs("POST")]
public ActionResult Edit(string id, FormCollection form)
{
  //Save the item and redirect…
}
```

When a POST request for `/home/edit` is received, the action invoker
creates a list of all methods of the controller that match the “edit”
action name. In this case, we would end up with a list of two methods.
Afterwards, the invoker looks at all of the `ActionSelectionAttribute`
instances applied to each method and calls the `IsValidForRequest()`
method on each. If each attribute returns true, then the method is
considered valid for the current action.

For example, in this case, when we ask the first method if it can handle
a `POST` request, it would respond with false because it only handles
`GET` requests. The second method responds with true because it can
handle the `POST` request and it is the one selected to handle the
action.

### Helpers

One consequence to keep in mind when using helpers which use our routing
API to generate URLs is that the parameters for all of these helpers
take in the *action name,*not the method name. So if I want to render
the URL to the following action:

```csharp
[ActionName("List")]
public ActionResult ListSomething()
{
  //...
}
```

Use “List” and not “ListSomething” as the action name.

```aspx-cs
<!-- WRONG! -->
<%= Url.Action("ListSomething") %>

<!-- RIGHT! -->
<%= Url.Action("List") %>
```

This is one reason you’ve seen the MVC team resistant to including
helper methods, such as `Url<T>(…)`, that use an expression to define
the URL of an action. The action is not necessarily equivalent to a
method on the class with the same name.

### Summary

So in the end, an action is a logical concept that represents an event
caused by the user (such as clicking a link or posting a form) which is
eventually mapped to a method which handles that user event.

It’s convenient to think of an action as a method of the same name, but
they are distinct concepts. A lowly method can become an action by the
power of its own name (*aka name dropping*), but in this egalitarian
framework, any method, no matter its name, can handle a particular
action, by merely using the `ActionNameAttribute`.

Technorati Tags:
[aspnetmvc](http://technorati.com/tags/aspnetmvc),[routing](http://technorati.com/tags/routing)

