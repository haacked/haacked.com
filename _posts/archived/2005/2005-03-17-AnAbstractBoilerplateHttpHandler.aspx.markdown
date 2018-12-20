---
title: An Abstract Boilerplate HttpHandler
date: 2005-03-17 -0800
disqus_identifier: 2394
categories: [code,dotnet]
redirect_from:
  - "/archive/2005/03/16/AnAbstractBoilerplateHttpHandler.aspx/"
  - "/archive/2005/03/17/2394/"
---

My last project involved writing a lot of HttpHandlers to respond to
HTTP requests originating from a cell phone. To simplify my life, I
created an abstract base handler that handled a lot of the repetitive
tasks in writing an HTTP handler.

So today, I read [Scott Hanselman’s](http://www.hanselman.com/blog/)
post about the [boilerplate
HttpHandler](http://www.hanselman.com/blog/PermaLink,guid,5c59d662-b250-4eb2-96e4-f274295bd52e.aspx)
he uses. He says one day he'll get more organized and make an abstract
base class to handle this kind of boilerplate stuff.

I've got your back Scott.

I went ahead and took my base class and quickly (about 10 minutes)
incorporated some of the things he has in his boilerplate and voila! An
abstract base class! Enjoy.

```csharp
/// <summary>
/// An abstract base Http Handler for all your
/// <see cref="IHttpHandler"/> needs.
/// </summary>
/// <remarks>
/// <p>
/// For the most part, classes that inherit from this
/// class do not need to override <see cref="ProcessRequest"/>.
/// Instead implement the abstract methods and
/// properties and put the main business logic
/// in the <see cref="HandleRequest"/>.
/// </p>
/// <p>
/// HandleRequest should respond with a StatusCode of
/// 200 if everything goes well, otherwise use one of
/// the various "Respond" methods to generate an appropriate
/// response code.  Or use the HttpStatusCode enumeration
/// if none of these apply.
/// </p>
/// </remarks>
public abstract class BaseHttpHandler : IHttpHandler
{
    /// <summary>
    /// Creates a new <see cref="BaseHttpHandler"/> instance.
    /// </summary>
    public BaseHttpHandler() {}
 
    /// <summary>
    /// Processs the incoming HTTP request.
    /// </summary>
    /// <param name="context">Context.</param>
    public void ProcessRequest(HttpContext context)
    {
        SetResponseCachePolicy(context.Response.Cache);
 
        if(!ValidateParameters(context))
        {
            RespondInternalError(context);
            return;
        }
 
        if(RequiresAuthentication
            && !context.User.Identity.IsAuthenticated)
        {
            RespondForbidden(context);
            return;
        }
 
        context.Response.ContentType = ContentMimeType;
 
        HandleRequest(context);
    }
 
    /// <summary>
    /// Indicates whether or not this handler can be
    /// reused between successive requests.
    /// </summary>
    /// <remarks>
    /// Return true if this handler does not maintain
    /// any state (generally a good practice).  Otherwise
    /// returns false.
    /// </remarks>
    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
 
    /// <summary>
    /// Handles the request.  This is where you put your
    /// business logic.
    /// </summary>
    /// <remarks>
    /// <p>This method should result in a call to one 
    /// (or more) of the following methods:</p>
    /// <p><code>context.Response.BinaryWrite();</code></p>
    /// <p><code>context.Response.Write();</code></p>
    /// <p><code>context.Response.WriteFile();</code></p>
    /// <p>
    /// <code>
    /// someStream.Save(context.Response.OutputStream);
    /// </code>
    /// </p>
    /// <p>etc...</p>
    /// <p>
    /// If you want a download box to show up with a 
    /// pre-populated filename, add this call here 
    /// (supplying a real filename).
    /// </p>
    /// <p>
    /// </p>
    /// <code>Response.AddHeader("Content-Disposition"
    /// , "attachment; filename=\"" + Filename + "\"");</code>
    /// </p>
    /// </remarks>
    /// <param name="context">Context.</param>
    public abstract void HandleRequest(HttpContext context);
 
    /// <summary>
    /// Validates the parameters.  Inheriting classes must
    /// implement this and return true if the parameters are
    /// valid, otherwise false.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <returns><c>true</c> if the parameters are valid,
    /// otherwise <c>false</c></returns>
    public abstract bool ValidateParameters(HttpContext context);
 
    /// <summary>
    /// Gets a value indicating whether this handler
    /// requires users to be authenticated.
    /// </summary>
    /// <value>
    ///    <c>true</c> if authentication is required
    ///    otherwise, <c>false</c>.
    /// </value>
    public abstract bool RequiresAuthentication {get;}
 
    /// <summary>
    /// Gets the content MIME type.
    /// </summary>
    /// <value></value>
    public abstract string ContentMimeType {get;}
 
    /// <summary>
    /// Sets the cache policy.  Unless a handler overrides
    /// this method, handlers will not allow a respons to be
    /// cached.
    /// </summary>
    /// <param name="cache">Cache.</param>
    public virtual void SetResponseCachePolicy
        (HttpCachePolicy cache)
    {
        cache.SetCacheability(HttpCacheability.NoCache);
        cache.SetNoStore();
        cache.SetExpires(DateTime.MinValue);
    }
 
    /// <summary>
    /// Helper method used to Respond to the request
    /// that the file was not found.
    /// </summary>
    /// <param name="context">Context.</param>
    protected void RespondFileNotFound(HttpContext context)
    {
        context.Response.StatusCode 
            = (int)HttpStatusCode.NotFound;
        context.Response.End();
    }
 
    /// <summary>
    /// Helper method used to Respond to the request
    /// that an error occurred in processing the request.
    /// </summary>
    /// <param name="context">Context.</param>
    protected void RespondInternalError(HttpContext context)
    {
        // It's really too bad that StatusCode property
        // is not of type HttpStatusCode.
        context.Response.StatusCode =
            (int)HttpStatusCode.InternalServerError;
        context.Response.End();
    }
 
    /// <summary>
    /// Helper method used to Respond to the request
    /// that the request in attempting to access a resource
    /// that the user does not have access to.
    /// </summary>
    /// <param name="context">Context.</param>
    protected void RespondForbidden(HttpContext context)
    {
        context.Response.StatusCode 
            = (int)HttpStatusCode.Forbidden;
        context.Response.End();
    }
}
```

You can also [download the
file](http://tools.veloc-it.com/tabid/58/grm2id/3/Default.aspx "Abstract Boilerplate")
from [http://tools.veloc-it.com/](http://tools.veloc-it.com/ "Tools")

