---
title: Preventing CSRF With Ajax
tags: [aspnetmvc,aspnet,code]
redirect_from: "/archive/2011/10/09/preventing-csrf-with-ajax.aspx/"
---

A long while ago I wrote about the potential [dangers of Cross-site
Request
Forgery](https://haacked.com/archive/2009/04/02/anatomy-of-csrf-attack.aspx "Anatomy of a CSRF attack")
attacks, also known as CSRF or XSRF. These exploits are a form of
[confused deputy
attack](http://en.wikipedia.org/wiki/Confused_Deputy "Confused Deputy Attack").

[![police-academy](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/45c6ec1c5059_11263/police-academy_thumb.jpg "police-academy")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/45c6ec1c5059_11263/police-academy_2.jpg)*Screen
grab from The Police Academy movie.*In that post, I covered how ASP.NET
MVC includes a set of anti-forgery helpers to help mitigate such
exploits. The helpers include an HTML helper meant to be called in the
form that renders a hidden input, and an attribute applied to the
controller action to protect. These helpers work great when in a typical
HTML form post to an action method scenario.

But what if your HTML page posts JSON data to an action instead of
posting a form? How do these helpers help in that case?

You can try to apply the `ValidateAntiForgeryTokenAttribute` attribute
to an action method, but it will fail every time if you try to post JSON
encoded data to the action method. On one hand, the most secure action
possible is one that rejects every request. On the other hand, that’s a
lousy user experience.

The problem lies in the fact that the under the hood, deep within the
call stack, the attribute peeks into the `Request.Form` collection to
grab the anti-forgery token. But when you post JSON encoded data, there
is no form collection to speak of. We hope to fix this at some point and
with a more flexible set of anti-forgery helpers. But for the moment,
we’re stuck with this.

This problem became evident to me after I wrote a proof-of-concept
library to  [ASP.NET MVC action methods from
JavaScript](https://haacked.com/archive/2011/08/18/calling-asp-net-mvc-action-methods-from-javascript.aspx "Calling ASP.NET MVC action methods from JavaScript")
in an easy manner. The JavaScript helpers I wrote post JSON to action
methods in order to call the actions. So I set out to fix this in my
[CodeHaacks
project](https://github.com/Haacked/CodeHaacks "CodeHaacks on Github").

There are two parts we need to tackle this problem. The first part is on
the client-side where we need to generate and send the token to the
server. To generate the token, I just use the existing
`@Html.AntiForgeryToken` helper in the view. A little bit of jQuery code
grabs the value of that token.

```csharp
var token = $('input[name=""__RequestVerificationToken""]').val();
```

That’s easy. Now that I have the value, I just need a way to post it to
the server. I choose to add it to the request headers. In vanilla jQuery
(mmmm, vanilla), that looks similar to:

```csharp
var headers = {};
// other headers omitted
headers['__RequestVerificationToken'] = token;

$.ajax({
  cache: false,
  dataType: 'json',
  type: 'POST',
  headers: headers,
  data: window.JSON.stringify(obj),
  contentType: 'application/json; charset=utf-8',
  url: '/some-url'
});
```

Ok, so far so good. This will generate the token in the browser and send
it to the server, but we have a problem here. As I mentioned earlier,
the existing attribute which validates the token on the server won’t
look in the header. It only looks in the form collection. Uh oh! It’s
Haacking time! I’ll write a custom attribute called
`ValidateJsonAntiForgeryTokenAttribute`.

This attribute will call into the underlying anti-forgery code, but we
need to get around that form collection issue I mentioned earlier.

Peeking into Reflector, I looked at the implementation of the regular
attribute and followed its call stack. It took me deep into the bowels
of the *System.Web.WebPages.dll* assembly, which contains a method with
the following signature that does the actual work to validate the token:

```csharp
public void Validate(HttpContextBase context, string salt);
```

Score! The method takes in an instance of type `HttpContextBase`, which
is an abstract base class. That means we can can intercept that call and
provide our own instance of `HttpContextBase `to validate the
anti-forgery token. Yes, I provide a forgery of the request to enable
the anti-forgery helper to work. Ironic, eh?

Here’s the custom implementation of the HttpContextBase class. I wrote
it as a private inner class to the attribute.

```csharp
private class JsonAntiForgeryHttpContextWrapper : HttpContextWrapper {
  readonly HttpRequestBase _request;
  public JsonAntiForgeryHttpContextWrapper(HttpContext httpContext)
    : base(httpContext) {
    _request = new JsonAntiForgeryHttpRequestWrapper(httpContext.Request);
  }

  public override HttpRequestBase Request {
    get {
      return _request;
    }
  }
}

private class JsonAntiForgeryHttpRequestWrapper : HttpRequestWrapper {
  readonly NameValueCollection _form;

  public JsonAntiForgeryHttpRequestWrapper(HttpRequest request)
    : base(request) {
    _form = new NameValueCollection(request.Form);
    if (request.Headers["__RequestVerificationToken"] != null) {
      _form["__RequestVerificationToken"] 
        = request.Headers["__RequestVerificationToken"];
    }
}

  public override NameValueCollection Form {
    get {
      return _form;
    }
  }
}
```

In general, you can get into all sorts of trouble when you hack around
with the http context. But in this case, I’ve implemented a wrapper for
a tightly constrained scenario that defers to default implementation for
most things. The only thing I override is the request form. As you can
see, I copy the form into a new `NameValueCollection` instance and if
there is a request verification token in the header, I copy that value
in the form too. I then use this modified collection as the `Form`
collection.

Simple, but effective.

The custom attribute follows the basic implementation pattern of the
regular attribute, but uses these new wrappers.

```csharp
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, 
    AllowMultiple = false, Inherited = true)]
public class ValidateJsonAntiForgeryTokenAttribute : 
    FilterAttribute, IAuthorizationFilter {
  public void OnAuthorization(AuthorizationContext filterContext) {
    if (filterContext == null) {
      throw new ArgumentNullException("filterContext");
    }

    var httpContext = new JsonAntiForgeryHttpContextWrapper(HttpContext.Current);
    AntiForgery.Validate(httpContext, Salt ?? string.Empty);
  }

  public string Salt {
    get;
    set;
  }
  
  // The private context classes go here
}
```

With that in place, I can now decorate action methods with this new
attribute and it will work in both scenarios, whether I post a form or
post JSON data. I updated the client script library for calling action
methods to accept a second parameter, `includeAntiForgeryToken`, which
causes it to add the anti-forgery token to the headers.

As always, the [source code is up on
Github](https://github.com/Haacked/CodeHaacks "CodeHaacks on Github")
with a sample application that demonstrates usage of this technique and
the assembly is in NuGet with the package id “MvcHaack.Ajax”.

