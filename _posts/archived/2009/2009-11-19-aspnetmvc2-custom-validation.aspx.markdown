---
title: ASP.NET MVC 2 Custom Validation
tags: [aspnetmvc,validation]
redirect_from: "/archive/2009/11/18/aspnetmvc2-custom-validation.aspx/"
---

UPDATE: I’ve updated this post to cover changes to client validation
made in ASP.NET MVC 2 RC 2.

This is the third post in my series ASP.NET MVC 2 Beta and its new
features.

1.  [ASP.NET MVC 2 Beta
    Released](https://haacked.com/archive/2009/11/17/asp.net-mvc-2-beta-released.aspx "Release Announcement")
    (Release Announcement)
2.  [Html.RenderAction and
    Html.Action](https://haacked.com/archive/2009/11/18/aspnetmvc2-render-action.aspx "Html.RenderAction and Html.Action")
3.  **ASP.NET MVC 2 Custom Validation**

In this post I will cover validation.

[![storage.canoe](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NETMVC2CustomValidationAttributeWith_13136/storage.canoe_3.jpg "storage.canoe")](http://en.wikipedia.org/wiki/Stuart_Smalley "Stuart Smalley on Wikipedia")
No, not that kind of validation, though I do think you’re good enough,
you’re smart enough, and doggone it, people like you.

Rather, I want to cover building a custom validation attribute using the
base classes available in `System.ComponentModel.DataAnnotations`.
ASP.NET MVC 2 has built-in support for data annotation validation
attributes for doing validation on a server. For details on how data
annotations work with ASP.NET MVC 2, check out [Brad’s
blog](http://bradwilson.typepad.com/blog/2009/04/dataannotations-and-aspnet-mvc.html "Data Annotations and ASP.NET MVC")
post.

But I won’t stop there. I’ll then cover how to hook into ASP.NET MVC 2’s
client validation extensibility so you can have validation logic run as
JavaScript on the client.

Finally I will cover some of changes we still want to make for the
release candidate.

Of course, the first thing I need is a contrived scenario. Due to my
lack of imagination, I’ll build a `PriceAttribute` that validates that a
value is greater than the specified price and that it ends in 99 cents.
Thus \$20.00 is not valid, but \$19.99 is valid.

Here’s the code for the attribute:

```csharp
public class PriceAttribute : ValidationAttribute {
  public double MinPrice { get; set; }
    
  public override bool IsValid(object value) {
    if (value == null) {
      return true;
    }
    var price = (double)value;
    if (price < MinPrice) {
      return false;
    }
    double cents = price - Math.Truncate(price);
    if(cents < 0.99 || cents >= 0.995) {
      return false;
    }
       
    return true;
  }
}
```

Notice that if the value is null, we return true. This attribute is not
intended to validate required fields. I’ll defer to the
`RequiredAttribute` to validate whether the value is required or not.
This allows me to place this attribute on an optional value and not have
it show an error when the user leaves the field blank.

We can test this out quickly by creating a view model and applying this
attribute to the model. Here’s an example of the model.

```csharp
public class ProductViewModel {
  [Price(MinPrice = 1.99)]
  public double Price { get; set; }

  [Required]
  public string Title { get; set; }
}
```

And let’s quickly write a view (`Index.aspx`) that will display an edit
form which we can use to edit the product.

```aspx-cs
<%@ Page Language="C#" Inherits="ViewPage<ProductViewModel>" %>

<% using (Html.BeginForm()) { %>

  <%= Html.TextBoxFor(m => m.Title) %>
    <%= Html.ValidationMessageFor(m => m.Title) %>
  <%= Html.TextBoxFor(m => m.Price) %>
    <%= Html.ValidationMessageFor(m => m.Price) %>
    
    <input type="submit" />
<% } %>
   
```

Now we just need a controller with two actions, one which will render
the edit view and the other which will receive the posted
`ProductViewModel`. For the sake of demonstration, these methods are
exceedingly simple and don’t do anything useful really.

```csharp
[HandleError]
public class HomeController : Controller {
  public ActionResult Index() {
    return View();
  }

  [HttpPost]
  public ActionResult Index(ProductViewModel model) {
    return View(model);
  }
}
```

We haven’t enabled client validation yet, but let’s see what happens
when we view this page and try to submit some values.

![price-invalid](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NETMVC2CustomValidationAttributeWith_13136/price-invalid_3.png "price-invalid")

As expected, it posts the form to the server and we see the error
messages.

### Making It Work In The Client

Great, now we have it working on the server, but how do we get this
working with client validation?

The first step is to reference the appropriate scripts. In Site.master,
I’ve added the following two script references.

```html
<script src="/Scripts/MicrosoftAjax.js"></script>
<script src="/Scripts/MicrosoftMvcAjax.js"></script>
<script src="/Scripts/MicrosoftMvcValidation.js"
></script>
```

The next step is to enable client validation for the form by calling
`EnableClientValidation` *before*****we call BeginForm. Under the hood,
this sets a flag in the new `FormContext` which lets the `BeginForm`
method know that client validation is enabled. That way, if you set an
id for the form, we’ll know which ID to use when hooking up client
validation. If you don’t, the form will render one for you.

```aspx-cs
<%@ Page Language="C#" Inherits="ViewPage<ProductViewModel>" %>

<% Html.EnableClientValidation(); %>
<% using (Html.BeginForm()) { %>

  <%= Html.TextBoxFor(m => m.Title) %>
    <%= Html.ValidationMessageFor(m => m.Title) %>
  <%= Html.TextBoxFor(m => m.Price) %>
    <%= Html.ValidationMessageFor(m => m.Price) %>
    
    <input type="submit" />
<% } %>
   
```

If you try this now, you’ll notice that the *Title* field validates on
the client, but the *Price* field doesn’t. We need to take advantage of
the validation extensibility available to hook in a client validation
function for the price validation attribute we wrote earlier.

The first step is to write a `ModelValidator` associated with the
attribute. Since the attribute is a data annotation, I can simply derive
from `DataAnnotationsModelValidator<PriceAttribute> `like so.

```csharp
public class PriceValidator : DataAnnotationsModelValidator<PriceAttribute> 
{
  double _minPrice;
  string _message;

  public PriceValidator(ModelMetadata metadata, ControllerContext context
    , PriceAttribute attribute)
    : base(metadata, context, attribute) 
  {
    _minPrice = attribute.MinPrice;
    _message = attribute.ErrorMessage;
  }

  public override IEnumerable<ModelClientValidationRule>   GetClientValidationRules() 
  {
    var rule = new ModelClientValidationRule {
      ErrorMessage = _message,
      ValidationType = "price"
    };
    rule.ValidationParameters.Add("min", _minPrice);

    return new[] { rule };
  }
}
```

The method `GetValidationRules` returns an array of
`ModelClientValidationRule` instances. Each of these instances
represents metadata for a validation rule that is written in JavaScript
and will be run in the client. This is purely metadata at this point and
the array will get converted into JSON and emitted in the client so that
client validation can hook up all the correct rules.

In this case, we only have one rule and we are calling its validation
type “price”. This fact will come into play later.

The next step is for us to now register this validator. Since we wrote
this as a Data Annotations validator, we can register it in
`Application_Start` as demonstrated by the following code snippet. If
you you’re using another model validation provider such as [the one for
the Enterprise Library’s Validation
Block](http://bradwilson.typepad.com/blog/2009/10/enterprise-library-validation-example-for-aspnet-mvc-2.html "Enterprise Library Validation for ASP.NET MVC"),
it might have its own means of registration.

```csharp
protected void Application_Start() {
  RegisterRoutes(RouteTable.Routes);
  DataAnnotationsModelValidatorProvider
    .RegisterAdapter(typeof(PriceAttribute), typeof(PriceValidator));
}
```

At this point, we still need to write the actual JavaScript validation
logic as well as the hookup to the JSON metadata. For the purposes of
this demo, I’ll put the script inline with the view.

```html
<script>
  Sys.Mvc.ValidatorRegistry.validators["price"] = function(rule) {
    // initialization code can go here.
    var minValue = rule.ValidationParameters["min"];

    // we return the function that actually does the validation 
    return function(value, context) {
      if (value > minValue) {
        var cents = value - Math.floor(value);
        if (cents >= 0.99 && cents < 0.995) {
          return true; /* success */
        }
      }

      return rule.ErrorMessage;
    };
  };
</script>
```

Now when I run the demo, I can see validation take effect as I tab out
of each field. Note that to get the required field validation to fire,
you’ll need to type something in the field and then clear it before
tabbing out.

Let’s pause for a moment and take a deeper look at what’s going on the
code above. At a high level, we’re adding a client validator to a
dictionary of validators using the key “price”. You may recall that
“price” is the validation type we defined when writing the
`PriceValidator`. That’s how we hook up this client function to the
server validation attribute.

You’ll notice that the function we add to the validators itself returns
a function which does the actual validation. Why is there this seemingly
extra level of indirection? Why not simply add a function that does the
validation directly to the dictionary?

This approach allows us to run some initialization code at the time the
validator is being hooked up to the metadata (as opposed to every time
validation occurs). This is helpful if you have expensive initialization
logic. The validate method of the object we return in that
initialization method may get called multiple times when the form is
being validated.

Notice that in this case, the initialization code grabs the min value
from the `ValidationParameters` dictionary. This is the same dictionary
created in the `PriceValidator` class, but now living on the client.

We then run through similar logic as we did in the server side
validation code. The difference here is we return null to indicate that
no error occurred and we return an array of error messages if an error
occurred.

### Validation using jQuery Validation?

Client validation in ASP.NET MVC is meant to be extremely extensible. At
the core, we emit some JSON metadata describing what fields to validate
and what type of validation to perform. This makes it possible to build
adapters which can hook up any client validation library to ASP.NET MVC.

For example, if you’re a fan of using jQuery it’s quite easy to use our
adapter to hook up jQuery Validation library to perform client
validation.

First, reference the following scripts.

```html
<script src="/Scripts/jquery-1.4.1.js"></script>
<script src="/Scripts/jquery.validate.js"></script>
<script src="/Scripts/MicrosoftMvcJQueryValidation.js">
</script>
```

When we emit the JSON in the page, we define it as part of an array
which we declare inline. If you view source you’ll see something like
this (truncated for brevity):

```html
<script> 
//<![CDATA[
if (!window.mvcClientValidationMetadata) {
  window.mvcClientValidationMetadata = []; 
}
window.mvcClientValidationMetadata.push({
  {"Fields":[{"FieldName":"Title",...,
    "ValidationRules":
      [{"ErrorMessage":"This field is required",...,        "ValidationType":"required"}]},
   ...);
//]]>
</script>
```

Note the array named `window.mvcClientValidationMetadata`.

Simply by referencing the `MicrosoftMvcJQueryValidation` script, you’ve
hooked up jQuery validation to that metadata. Both of the validation
adapter scripts look for the existence of the special array and consumes
the JSON within the array.

### How about a demo?

And before I forget, here’s a [**demo
application**](http://code.haacked.com/mvc-2/CustomValidationAttributeDemo.zip "Custom Validation Demo")
demonstrating the attribute described in this post.

