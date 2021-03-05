---
title: Prevent Forms Authentication Login Page Redirect When You Don't Want It
tags: [aspnet,aspnetmvc,code]
redirect_from: "/archive/2011/10/03/prevent-forms-authentication-login-page-redirect-when-you-donrsquot-want.aspx/"
---

[![redirect](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Prevent-Forms-Authentication-Login-Page-_C968/redirect_3.jpg "redirect")](http://www.flickr.com/photos/notjake13/2574028447/ "Construction signs in NY")
*Go that way instead - Photo by JacobEnos [CC some rights
reserved](http://creativecommons.org/licenses/by/2.0/deed.en "Creative Commons by Attribution")*

***Update: It looks like ASP.NET 4.5 adds the ability to suppress forms
authentication redirect now with the [HttpResponse.SuppressFormsAuthenticationRedirect
property](http://msdn.microsoft.com/en-us/library/system.web.httpresponse.suppressformsauthenticationredirect.aspx "Suppress Forms Auth Property").***

In an ASP.NET web application, it’s very common to write some jQuery code that makes an HTTP request to some URL (a lightweight service) in order to retrieve some data. That URL might be handled by an ASP.NET MVC controller action, a Web API operation, or even an ASP.NET Web Page or Web Form. If it can return curly brackets, it can be respond to a
JavaScript request for JSON.

One pain point when hosting lightweight HTTP services on ASP.NET is making a request to a URL that requires authentication. Let’s look at a snippet of jQuery to illustrate what I mean. The following code makes a request to */admin/secret/data*. Let’s assume that URL points to an ASP.NET MVC action with the `AuthorizeAttribute` applied, which requires that the request must be authenticated.

```js
$.ajax({
    url: '/admin/secret/data',
    type: 'POST',
    contentType: 'application/json; charset=utf-8',
    statusCode: {
        200: function (data) {
            alert('200: Authenticated');
            // Bind the JSON data to the UI
        },
        401: function (data) {
            alert('401: Unauthenticated');
            // Handle the 401 error here.
        }
    }
});
```

If the user is not logged in when this code executes, you would expect that the 401 status code function would get called. But if forms authentication (often called FormsAuth for short) is configured, that isn’t what actually happens. Instead, you get a 200  with the contents of the login page (or a 404 if you don’t have a login page). What gives?

If you crack open [Fiddler](http://www.fiddler2.com/fiddler2/ "Fiddler"), it’s easy to see the problem. Instead of the request returning an HTTP 401 Unauthorized status code, it instead returns a 302 pointing to a login page. This
causes jQuery (well actually, the `XmlHttpRequest` object) to automatically follow the redirect and issue another request to the login page. The login page handles this new request and return its contents with a 200 status code. This is not the desired result as the code expects JSON data to be returned in response to a 200 code, not HTML for the login page.

This “helpful” behavior when requesting a URL that requires authentication is a consequence of having the
`FormsAuthenticationModule` enabled, which is the default in most ASP.NET applications. Under the hood, the `FormsAuthenticationModule` hooks into the request pipeline and changes any request that returns a 401 status code into a redirect to the login page.

## Possible Solutions

I’m going to cover a few possible solutions I’ve seen around the web and then present the one that I prefer. It’s not that these other solutions are wrong, but they are only correct in some cases.

### Remove Forms Authentication

If you don’t need FormsAuth, one simple solution is to remove the forms authentication module as I found in one post (no longer around).

This is a great solution if you’re sole purpose is to use ASP.NET to host a Web API service and you don’t need forms authentication. But it’s not a great solution if your app is both a web application and a web service.

### Register an HttpModule to convert Redirects to 401

Another post (no longer around) suggests registering an HTTP Module that converts *any* 302 request to a
401. There are two problems with this approach. The first is that it breaks the case where the redirect is *legitimate* and not the result of FormsAuth. The second is that it requires manual configuration of an `HttpModule`.

### Install-Package MembershipService.Mvc

My colleague, [Steve Sanderson](http://blog.stevensanderson.com/ "Steve Sanderson's Blog"), has an even better approach with his `MembershipService.Mvc` and `MembershipService.WebForms `NuGet packages. These packages expose ASP.NET Membership as a service that you can call from multiple devices.

For example, if you want your Windows Phone application to use an ASP.NET website’s membership system to authenticate users of the application, you’d use his package. He provides the `MembershipClient.WP7` and `MembershipClient.JavaScript` packages for writing clients that call into these services.

These packages deserve a blog post in their own right, but I’m going to just focus on the `DoNotRedirectLoginModule` he wrote. His module takes a similar approach to the previous one I mentioned, but he checks for a
special value in [`HttpContext.Items`](http://msdn.microsoft.com/en-us/library/system.web.httpcontext.items.aspx "HttpContext.Items on MSDN"), a dictionary for storing data related to the current request, before reverting a redirect back to a 401.

To prevent a FormsAuth redirect, an action method (or ASP.NET page or Web API operation) would simply call the helpful method
`DoNotRedirectToLoginModule.ApplyForRequest`. This sets the special token in `HttpContext.Items` and the module will rewrite a 302 that’s redirecting to the login page back to a 401.

## My Solution

Steve’s solution is a very good one. But I’m particularly lazy and didn’t want to have to call that method on every action when I’m writing an Ajax heavy application. So what I did was write a module that hooks
in two events of the request.

The first event, `PostReleaseRequestState`, occurs after authentication, but before the `FormsAuthenticationModule` converts the status to a 302. In the event handler for this event, I check to see if the request is an Ajax request by checking that the `X-Requested-With` request header is “`XMLHttpRequest`”.

If so, I store away a token in the `HttpContext.Items` like Steve does. Then in the `EndRequest` event handler, I check for that token, just like Steve does. Inspired by Steve’s approach, I added a method to allow explicitly opting into this behavior, `SuppressAuthenticationRedirect`.

Here’s the code for this module. **Warning: Consider this “proof-of-concept” code. I haven’t tested this thoroughly in a wide
range of environments.**

```csharp
public class SuppressFormsAuthenticationRedirectModule : IHttpModule {
  private static readonly object SuppressAuthenticationKey = new Object();

  public static void SuppressAuthenticationRedirect(HttpContext context) {
    context.Items[SuppressAuthenticationKey] = true;
  }

  public static void SuppressAuthenticationRedirect(HttpContextBase context) {
    context.Items[SuppressAuthenticationKey] = true;
  }

  public void Init(HttpApplication context) {
    context.PostReleaseRequestState += OnPostReleaseRequestState;
    context.EndRequest += OnEndRequest;
  }

  private void OnPostReleaseRequestState(object source, EventArgs args) {
    var context = (HttpApplication)source;
    var response = context.Response;
    var request = context.Request;

    if (response.StatusCode == 401 && request.Headers["X-Requested-With"] == 
      "XMLHttpRequest") {
      SuppressAuthenticationRedirect(context.Context);
    }
  }

  private void OnEndRequest(object source, EventArgs args) {
    var context = (HttpApplication)source;
    var response = context.Response;

    if (context.Context.Items.Contains(SuppressAuthenticationKey)) {
      response.TrySkipIisCustomErrors = true;
      response.ClearContent();
      response.StatusCode = 401;
      response.RedirectLocation = null;
    }
  }

  public void Dispose() {
  }

  public static void Register() {
    DynamicModuleUtility.RegisterModule(
      typeof(SuppressFormsAuthenticationRedirectModule));
  }
}
```

## There’s a package for that

***Warning: The following is proof-of-concept code I’ve written. I haven’t tested it thoroughly in a production environment and I don’t provide any warranties or promises that it works and won’t kill your favorite pet. You’ve been warmed.***

Naturally, I’ve written a [NuGet](http://nuget.org/ "NuGet") package for this. Simply install the package and all Ajax requests that set that header (if you’re using jQuery, you’re all set) will not be redirected
in the case of a 401.

`Install-Package AspNetHaack`

Note that the package adds a source code file in `App_Start` that wires up the http module that suppresses redirect. If you want to turn off this behavior temporarily, you can comment out that file and you’ll be back to the old behavior.

The source code for this is in [Github](http://github.com/ "Github") as part of my broader [CodeHaacks
project](https://github.com/Haacked/CodeHaacks "CodeHaacks on Github").

## Why don’t you just fix the FormsAuthenticationModule?

We realize this is a deficiency with the forms authentication module and we’re looking into hopefully fixing this for the next version of the Framework.

__Update:__ As I stated at the beginning, a new property added in ASP.NET 4.5 supports doing this.
