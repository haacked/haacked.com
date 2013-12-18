---
layout: post
title: "QUIZ: What's Wrong With This Code?"
date: 2004-11-17 -0800
comments: true
disqus_identifier: 1634
categories: []
---
This is a simplified version of a sneaky bug I ran into today (I’m fine
thank you, but the bug is dead). ~~The only prize I can offer is a GMail
account if you want one.~~

Imagine that the method *HandleRedirect* actually does something
interesting and if all the conditions pass, the user is redirected to
*special.aspx*. This is the source code for an HttpHandler implemented
as a `.ashx` file.

```csharp
<%@ WebHandler Language="C#" Class="MyHandler" %>
using System;
using System.Web;
 
public class MyHandler : IHttpHandler
{
    /// <summary>
    /// Processs an incoming request.
    /// </summary>
    public void ProcessRequest(HttpContext ctx)
    {
        try
        {
            HandleRedirect(ctx);
        }
        catch(Exception)
        {
            ctx.Response.Redirect("/default.aspx");
        }
    }
 
    void HandleRedirect(HttpContext ctx)
    {
        ctx.Response.Redirect("/special.aspx");
    }
 
    public bool IsReusable
    {
        get { return true; }
    }
}
```

