---
title: Calling ASP.NET MVC Action Methods from JavaScript
tags: [aspnet,aspnetmvc,code]
redirect_from: "/archive/2011/08/17/calling-asp-net-mvc-action-methods-from-javascript.aspx/"
---

In a recent blog post, I wrote a [a controller
inspector](https://haacked.com/archive/2011/08/10/writing-an-asp-net-mvc-controller-inspector.aspx "Controller Inspector")
to demonstrate Controller and Action Descriptors. In this blog post, I
apply that knowledge to build something more useful.

One pain point when you write Ajax heavy applications using ASP.NET MVC
is managing the URLs that Routing generates on the server. These URLs
aren’t accessible from code in a static JavaScript file.

There are techniques to mitigate this:

1.  Generate the URLs in the view and pass them into the JavaScript API.
    This approach has the drawback that it isn’t unobtrusive and
    requires some script in the view.
2.  If you prefer the unobtrusive approach, embed the URLs in the HTML
    in a logical and semantic manner and the script can read them from
    the DOM.

Both approaches get the job done, but they start to break down when you
have a list. For example, suppose I have a page that lists comic books
retrieved from the server, each with a link to edit the comic book. Do I
then need to generate a URL for each comic on the server and pass it
into the script?

That’s not necessarily a bad idea. After all, isn’t that how Hypertext
is supposed to work? When you render a set of resources, shouldn’t the
response include a navigation URL for each resource?

On the other hand, you might prefer your services to return just comic
books and infer the URLs by convention.

How does MvcHaack.Ajax help?
----------------------------

Thinking about this problem led me to build up a quick
**proof-of-concept prototype** based on something [David
Fowler](http://weblogs.asp.net/davidfowler/ "David Fowler's Blog")
showed me a long time ago.

The library provides a base controller class, I tentatively named
`JsonController` (I could extend it to support other formats, but I
wanted to keep this prototype focused on one common scenario). This
class sets up a custom action invoker which does a lot of the work.

With this library in place, a `<script>` reference pointing to the
controller itself generates a jQuery based JavaScript API with methods
for calling controller actions.

This API enables passing JSON objects from the client to the server,
taking advantage of ASP.NET MVC’s argument model binding.

Perhaps an illustration is in order.

Lets see some codez!
--------------------

The first step is to write a controller. I’ll start simple and step it
up a notch later.

The controller has a single action method that returns an enumeration of
anonymous objects. Since I’m inheriting from `JsonController`, I don’t
need to specify an action result return type. I could have returned real
objects too, but for the sake of simplicity, I wanted to start here.

```csharp
public class ComicsController : JsonController {
  public IEnumerable List() {
    return new[] {
      new {Id = 1, Title = "Groo"},
      new {Id = 1, Title = "Batman"},
      new {Id = 1, Title = "Spiderman"}
    };
  }
}
```

The next step is to make sure I have a route to the controller, and not
to the controller’s action. The special invoker I wrote handles action
method selection. **This prototype lets you use a regular route**, but
the `JsonRoute` ensures correctness.

```csharp
public static void RegisterRoutes(RouteCollection routes) {
  // ... other routes
  routes.Add(new JsonRoute("json/{controller}"));
  // ... other routes ...
}
```

As a reminder, this second step with **the `JsonRoute` is not
required**!

With this in place, I can add a script reference to the controller from
an HTML page and call methods on it from JavaScript. Let’s do that and
display each comic book.

First, I’ll write the HTML markup.

```html
<script src="/json/comics?json"></script>
<script src="/scripts/comicsdemo.js"></script>
<ul id="comics"></ul>
```

The first script tag references an interesting
URL,***/json/comics?json***. That URL points to the controller (not an
action of the controller), but passes in a query string value. This
value indicates that the controller descriptor should short circuit the
request and generate a JavaScript with methods to call each action of
the controller using the same technique I [wrote about
before](https://haacked.com/archive/2011/08/10/writing-an-asp-net-mvc-controller-inspector.aspx "ASP.NET MVC Controller Inspector").

Here’s an example of the generated script. It’s very short. In fact,
most of it is pretty statick. The generated part is the array of actions
passed to the `$.each` block and the URL.

```csharp
if (typeof $mvc === 'undefined') {
    $mvc = {};
}
$mvc.Comics = [];
$.each(["List","Create","Edit","Delete"], function(action) {
    var action = this;
    $mvc.Comics[this] = function(obj) {
        return $.ajax({
            cache: false,
            dataType: 'json',
            type: 'POST',
            headers: {'x-mvc-action': action},
            data: JSON.stringify(obj),
            contentType: 'application/json; charset=utf-8',
            url: '/json/comics?invoke&action=' + action
        });
    };
});
```

For those of you big into REST, you’re probably groaning right now with
the RPC-ness of this API. It wouldn’t be hard to extend this prototype
to take a more RESTful approach. For now, I stuck with this because it
more closely matches the conceptual model for ASP.NET MVC out of the
box.

Reference the script, and I can now call action methods on the
controller from JavaScript. For example, in the following code listing,
I call the `List` action of the `ComicsController` and append the
results to an unordered list. Since I didn’t need to mix client and
server code to write this script, I can place it in a static script
file, *comicsdemo.js*.

```csharp
$(function() {
    $mvc.Comics.List().success(function(data) {
        $.each(data, function() {
            $('#comics').append('<li>' + this.Title + '</li>');
        });

    });
});
```

One more thing
--------------

It’s easy to call a parameter-less action method, but what about an
action that takes in a type? Not a problem. To demonstrate, I’ll create
a type on the server first.

```csharp
public class ComicBook {
    public int Id { get; set; }
    public string Title { get; set; }
    public int Issue { get; set; }
}
```

Great! Now let’s add an action method that accepts a `ComicBook` as an
action method parameter. For demonstration purposes, the method just
returns the comic along with a message. The invoker serializes the
return value to JSON for you. There is no need to wrap the return value
in a `JsonResult`. The invoker handles that for us.

```csharp
public object Save(ComicBook book) {
    return new { message = "Saved!", comic = book };
}
```

I can now call that action method from JavaScript and pass in a a comic
book. I just need to pass in an anonymous JavaScript object with the
same shape as a `ComicBook`. For example:

```csharp
$mvc.Comics.Save({ Title: 'Adventurers', Issue: 123 })
  .success(function(data) {
      alert(data.message + ' Comic: ' + data.comic.Title);
  });
```

The code results in the alert pop up. This proves I posted a comic book
to the server from JavaScript.

![Message from webpage
(2)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Calling-Action-Methods-Using-Icing.Ajax_95C2/Message%20from%20webpage%20(2)_3.png "Message from webpage (2)")

Get the codez!
--------------

Ok, enough talk! If you want to try this out, I have [**a live demo
here**](http://mvchaackajaxdemo.apphb.com/AjaxDemo/Home/ "Live Demo").
One [of the
demos](http://mvchaackajaxdemo.apphb.com/AjaxDemo/Home/KnockoutDemo "Knockout Demo")
shows how this can nicely fit in [with
KnockoutJS](http://knockoutjs.com "KnockoutJS").

If you want to try the code yourself, it’s available in NuGet under the
ID **MvcHaack.Ajax**.

The [source code is up at
Github](https://github.com/Haacked/CodeHaacks "Bitbucket"). Take a look
and let me know what you think. Should we put something like this in
ASP.NET MVC 4?

