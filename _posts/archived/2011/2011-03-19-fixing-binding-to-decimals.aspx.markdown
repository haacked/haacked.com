---
title: Model Binding Decimal Values
tags: [aspnetmvc,aspnet,code]
redirect_from: "/archive/2011/03/18/fixing-binding-to-decimals.aspx/"
---

I’m in the beautiful country of Brazil right now (I’ll hopefully blog
more about that later) proctoring for the hands-on labs that’s part of
the Web Camps agenda.

However, the folks here are keeping me on my toes asking me to give
impromptu and deeply advanced demos. It almost feels like a form of
performance art as I create brand new demos on the fly.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Fixing-Binding-To-Decimals_9491/wlEmoticon-smile_2.png)

During this time, several people reported issues binding to a decimal
value that prompted me to write a new demo and this blog post.

Let’s look at the scenario. Suppose you have the following class
(*Jogador*is a soccer player in Portugese):

```csharp
public class Jogador {
    public int ID { get; set; }
        
    public string Name { get; set; }
        
    public decimal Salary { get; set; }
}
```

And you have two controller actions, one that renders a form used to
create a `Jogador` and another action method that receives the POST
request.

```csharp
public ActionResult Create() {
  // Code inside here is not important
  return View();
}

public ActionResult Create(Jogador player) {
  // Code inside here is not important
  return View();  
}
```

When you type in a value such as *1234567.55* into the Salary field and
try to post it, it works fine. But typically, you would want to type it
like *1,234,567.55* (or here in Brazil, you would type it as
*1.234.567,55*).

In that case, the `DefaultModelBinder` chokes on the value. This is
unfortunate because jQuery Validate allows that value just fine. I’ll
talk to the rest of my team about whether we should fix this in the next
version of ASP.NET MVC, but for now it’s good to know there’s a
workaround.

In general, we recommend folks don’t write custom model binders because
they’re difficult to get right and they’re rarely needed. The issue I’m
discussing in this post might be one of those cases where it’s
warranted.

Here’s the code for my `DecimalModelBinder`. I should probably write one
for other decimal types too, but I’m lazy.

**WARNING:** **This is sample code!** I haven’t tried to optimize it or
test all scenarios. I know it works for direct decimal arguments to
action methods as well as decimal properties when binding to complex
objects.

```csharp
using System;
using System.Globalization;
using System.Web.Mvc;

public class DecimalModelBinder : IModelBinder {
    public object BindModel(ControllerContext controllerContext, 
        ModelBindingContext bindingContext) {
        ValueProviderResult valueResult = bindingContext.ValueProvider
            .GetValue(bindingContext.ModelName);
        ModelState modelState = new ModelState { Value = valueResult };
        object actualValue = null;
        try {
            actualValue = Convert.ToDecimal(valueResult.AttemptedValue, 
                CultureInfo.CurrentCulture);
        }
        catch (FormatException e) {
            modelState.Errors.Add(e);
        }

        bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
        return actualValue;
    }
}
```

With this in place, you can easily register this in `Application_Start`
within Global.asax.cs.

```csharp
protected void Application_Start() {
    AreaRegistration.RegisterAllAreas();
    
    ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());

    // All that other stuff you usually put in here...
}
```

That registers our model binder to only be applied to `decimal` types,
which is good since we wouldn’t want model binding to try and use this
model binder when binding any other type.

With this in place, the Salary field will now accept both
*1234567.55*and *1,234,567.55*.

Hope you find this useful. I’ve had a great time in Buenos Aires,
Argentina and São Paulo, Brazil. I’ll probably be swamped when I get
back home, but I’ll try to make time to write about my time here.

