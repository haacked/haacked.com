---
title: "A Subtle Gotcha with Azure Deployment Slots and ASP.NET Core"
description: "Setting up a proper continuous deployment with Azure Deployment Slots and ASP.NET Core is trickier than I anticipated. Here's a particular issue I ran into and my solution."
tags: [dotnet,azure,deployment]
excerpt_image: https://user-images.githubusercontent.com/19977/94380220-81344b00-00e9-11eb-84a4-876408467978.png
---

When I deploy software, I'm lazy. Very lazy. This is why I lean heavily on Continous Deployment (CD) to automatically test and deploy software when it's merged into my `main` branch. I don't have time to deploy code by hand. So gauche!

Of course this requires a lot of trust in my automation and testing before merging code into `main`. But the overall time savings and agility gained through CD are tremendous!

![Deploying code](https://user-images.githubusercontent.com/19977/94380220-81344b00-00e9-11eb-84a4-876408467978.png)

It's possible to set up a continuous [blue-green deployment](https://martinfowler.com/bliki/BlueGreenDeployment.html) process on Azure using [deployment slots](https://docs.microsoft.com/en-us/azure/app-service/deploy-staging-slots).

I did this recently, but ran into a subtle gotcha when I tried to [set up a custom warm-up](https://docs.microsoft.com/en-us/azure/app-service/deploy-staging-slots#specify-custom-warm-up) with my ASP.NET Core site. A custom warm-up is a URL that Azure will hit after code is deployed to the offline deployment slot or slots. This gives the app a chance to run custom warm-up code before the slot is swapped into production.

The documentation mentions two app settings you can use to set up a custom warm-up:

> * `WEBSITE_SWAP_WARMUP_PING_PATH`: The path to ping to warm up your site. Add this app  setting by specifying a custom path that begins with a slash as the value. An example is `/statuscheck`. The default value is `/`.
> * `WEBSITE_SWAP_WARMUP_PING_STATUSES`: Valid HTTP response codes for the warm-up operation. Add this app setting with a comma-separated list of HTTP codes. An example is `200,202`. If the returned status code isn't in the list, the warmup and swap operations are stopped. By default, all response codes are valid.

Seems straightforward. So I set something up like this:

* `WEBSITE_SWAP_WARMUP_PING_PATH`: `/statuscheck`
* `WEBSITE_SWAP_WARMUP_PING_STATUSES`: `200`

Afterwards, all my deployments failed. I used `curl` to confirm my /statuscheck page returned a `200` code.

```bash
curl -i https://mysite.example.com/statuscheck
```

I tried running the swap manually from the Azure Portal and it gave a very unhelpful error message. First of all, the error message contains the following string:

> `Cannot swap slots for site '{0}' because the application initialization in '{1}' slot either took too long or failed.`

It looks like someone forgot to pass parameters to `string.Format`.

Second, it mentioned a `Bad Request: 400` status code.

I then looked in my logs and didn't see any `400` status code requests. Instead, I saw some `307 Temporary Redirects`. I looked more closely and noticed requests for:

`http://mysite.example.com/statuscheck`

Notice the problem? The Azure warmup ping request is making HTTP requests, not HTTPS requests. And you can't change this. The setting `WEBSITE_SWAP_WARMUP_PING_PATH` does not allow a fully qualified URL. It will always make an HTTP request.

The problem for me is that I have the following code in my app startup (`Startup.cs`).

```csharp
public void Configure(IApplicationBuilder app) {
    // Bunch of stuff
    app.UseHttpsRedirection();
    // More stuff.
}
```

You might recognize that [`UseHttpsRedirection`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.httpspolicybuilderextensions.usehttpsredirection?view=aspnetcore-3.1) method call from the default ASP.NET Core project templates. That adds middleware to redirect HTTP requests to HTTPS.

On top of that, I usually configure Azure Web Applications to be HTTPS only via the `TLS/SSL settings`. This is why the Azure swap warmup request fails. It expects a `200` but is getting a `307` and doesn't follow the redirect.

In a useful [troubleshooting blog post](https://ruslany.net/2017/11/most-common-deployment-slot-swap-failures-and-how-to-fix-them/), the author mentions using rewrite rules to solve this. Not sure that's going to work for my site since I don't even have a `web.config` file and I'm not using rewrite rules to enforce HTTPS in the first place.

Fortunately the solution is straightforward. First, I went to the `TLS/SSL settings` for my site in the Azure Portal and turned `HTTPS Only` to `Off`.

Then I branched my request pipeline using the [`UseWhen` extension method](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.usewhenextensions.usewhen?view=aspnetcore-3.1).

```csharp
public void Configure(IApplicationBuilder app) {
    // ...
    app.UseWhen(context =>
    {
        var path = context.Request.Path.Value ?? string.Empty;
        return !path.Equals("/statuscheck", StringComparison.OrdinalIgnoreCase);
    },
    mainApp =>
    {
        mainApp.UseHttpsRedirection();
    });
    // ...
}
```

In my actual code, I wrote an extension method like so:

```csharp
public static bool IsStatusCheckRequest(this HttpRequest request) {
    var path = context.Request.Path.Value ?? string.Empty;
    return path.Equals("/statuscheck", StringComparison.OrdinalIgnoreCase);
}
```

That way I could simplify the code in my startup class to:

```csharp
public void Configure(IApplicationBuilder app) {
    // ...
    app.UseWhen(context => !context.Request.IsStatusCheckRequest(),
      mainApp =>
      {
        mainApp.UseHttpsRedirection();
      });
    // ...
}
```

`UseWhen` takes two parameters. The first is the condition. In this case, when the request is NOT a status check request. The second is the code to call when that condition is true, in this case it registers the HTTPS redirect middleware.


In effect, this code forks my app pipeline into two paths. For requests to `/statuscheck` HTTP requests are allowed. For all other requests, we continue to redirect HTTP to HTTPS.

Problem solved!

What's particularly subtle about all this is if you set up `WEBSITE_SWAP_WARMUP_PING_PATH` but do not set up `WEBSITE_SWAP_WARMUP_PING_STATUSES`, the warm-up request by default accepts all status codes, including `307`. So your deploys will pass, but it may not necessarily be done with the warm-up as it's unclear if Azure follows the redirect in this case.

This was a frustrating experience, but I got it working in the end. My hope is that if you have a similar setup to me, this will save you time and headaches.

__UPDATE: My deploys still fail.__ If I specify `WEBSITE_SWAP_WARMUP_PING_STATUSES` = `200` my deploys still fail even though I can use `curl` to see the status check URLs return 200. I'm at a loss here and I've opened up a support ticket with Microsoft. I'll post back here with what I learn.

__UPDATE: Resolved the issue.__ I tried to be clever and pass a query string parameter to my warmup URL. For example, by setting `WEBSITE_SWAP_WARMUP_PING_PATH` to `/statuscheck?azure-warmup=true`. This way I could quickly distinguish my own testing of this URL from the request made by Azure in my logs. Upon reflection, of course this doesn't work because Azure URL Encodes `WEBSITE_SWAP_WARMUP_PING_PATH` before requesting it. So the request path is `/statuscheck%3Fazure-warmup=true` and not `/statuscheck` as I expected.
