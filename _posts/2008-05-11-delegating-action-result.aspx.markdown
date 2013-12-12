---
layout: post
title: "Delegating Action Result"
date: 2008-05-11 -0800
comments: true
disqus_identifier: 18485
categories: [asp.net,asp.net mvc,code]
---
In [my last
post](http://haacked.com/archive/2008/05/10/writing-a-custom-file-download-action-result-for-asp.net-mvc.aspx "Download Action Result"),
I walked through a simple example of an `ActionResult` that you can use
to transmit a file to the user’s browser along with a download prompt.

The MVC framework will include several useful action results for common
tasks. However, we might not cover all results you might want to return.
In this post, I walk through a simple result that will cover all
remaining cases. With the `DelegatingResult`, you simply pass it a
delegate. This provides ultimate control. Let’s see it in action.

```csharp
public ActionResult Hello() {
  return new DelegatingResult(context => {
    context.HttpContext.Response.AddHeader("something", "something");
    context.HttpContext.Response.Write("Hello World!");
  });
}
```

Notice that we pass in a lambda to the constructor of the action result.
This lambda is a delegate of type `Action<ControllerContext>`. By doing
this, the lines of code within that block (`Response.AddHeader` and
`Response.Write`) are deferred till later.

Here’s the code for this action result.

```csharp
public class DelegatingResult : ActionResult {
    
  public Action<ControllerContext> Command {
    get;
    private set;
  }
    
  public DelegatingResult(Action<ControllerContext> command) {
    this.Command = command;
  }

  public override void ExecuteResult(ControllerContext context) {
    if (context == null) {
      throw new ArgumentNullException("context");
    }
        
    Command(context);
  }
}
```

I updated the sample I wrote in my last post to include this demo.
[**Download the
source**](http://haacked.com/code/CustomActionResultDemo.zip "Custom Action Result Demo").

Technorati Tags:
[aspnetmvc](http://technorati.com/tags/aspnetmvc),[actionresult](http://technorati.com/tags/actionresult),[lambda](http://technorati.com/tags/lambda)

