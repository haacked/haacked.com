---
title: Globalizing ASP.NET MVC Client Validation
tags: [aspnetmvc,validation]
redirect_from: "/archive/2010/05/09/globalizing-mvc-validation.aspx/"
---

One of my favorite features of ASP.NET MVC 2 is the support for client
validation. I’ve covered a bit about validation in the following two
posts:

-   [ASP.NET MVC 2 Custom
    Validation](https://haacked.com/archive/2009/11/19/aspnetmvc2-custom-validation.aspx "Custom Validation")
    covers writing a custom client validator.
-   [Localizing ASP.NET MVC
    Validation](https://haacked.com/archive/2009/12/07/localizing-aspnetmvc-validation.aspx "Localizing Validation")
    covers localizing error messages.

However, one topic I haven’t covered is how validation works with
globalization. A common example of this is when validating a number, the
client validation should understand that users in the US enter periods
as a decimal point, while users in Spain will use a comma.

For example, let’s assume I have a type with the `RangeAttribute`
applied. In this case, I’m applying a range from 100 to 1000.

```csharp
public class Product
{
    [Range(100, 1000)]
    public int QuantityInStock { get; set; }

    public decimal Cost { get; set; }
}
```

And in a strongly typed view, we have the following snippet.

```aspx-cs
<% Html.EnableClientValidation(); %>
<% using (Html.BeginForm()) {{ "{%" }}>

    <%: Html.LabelFor(model => model.QuantityInStock) %>
    <%: Html.TextBoxFor(model => model.QuantityInStock)%>
    <%: Html.ValidationMessageFor(model => model.QuantityInStock)%>

<% } %>
```

Don’t forget to reference the necessary ASP.NET MVC scripts. I’ve done
it in the master page.

```html
<script src="/Scripts/MicrosoftAjax.debug.js"></script>
<script src="/Scripts/MicrosoftMvcAjax.debug.js"></script>
<script src="/Scripts/MicrosoftMvcValidation.debug.js"></script>
```

Now, when I visit the form, type in *1,000* into the text field, and hit
the TAB key, I get the following behavior.

![valid-range](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GlobalizingASP.NETMVCClientValidation_A13C/valid-range_3.png "valid-range")

Note that there is no validation message because in the US, *1,000 ==
1000* and is within the range. Now let’s see what happens when I type
*1.000*.

![invalid-range](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GlobalizingASP.NETMVCClientValidation_A13C/invalid-range_6.png "invalid-range")

As we can see, that’s not within the range and we get an error message.

Fantastic! That’s exactly what I would expect, unless I was a Spaniard
living in Spain (*¡Hola mis amigos!*).

In that case, I’d expect the opposite behavior. I’d expect *1,000* to be
equivalent to *1* and thus not in the range, and I’d expect *1.000* to
be *1000* and thus in the range, because in Spain (as in many European
countries), the comma is the decimal separator.

### Setting up Globalization for ASP.NET MVC 2

Well it turns out, we can make ASP.NET MVC support this. To demonstrate
this, I’ll need to change my culture to *es-ES*. There are many blog
posts that cover how to do this automatically based on the request
culture. I’ll just set it in my Global.asax.cs file for demonstration
purposes.

```csharp
protected void Application_BeginRequest() {
  Thread.CurrentThread.CurrentCulture     = CultureInfo.CreateSpecificCulture("es-ES");
}
```

The next step is to add a call to the `Ajax.GlobalizationScript` helper
method in my Site.master.

```aspx-cs
<head runat="server">
  <%: Ajax.GlobalizationScript() %>
  <script src="/Scripts/MicrosoftAjax.debug.js">
  </script>
  <script src="/Scripts/MicrosoftMvcAjax.debug.js">
  </script>
  <script src="/Scripts/MicrosoftMvcValidation.debug.js">
  </script>
</head>
```

What this will do is render a script tag pointing to a globalization
script named according to the current locale and placed in
*scripts/globalization* directory by convention. The idea is that you
would place all the globalization scripts for each locale that you
support in that directory. Here’s the output of that call.

```html
<script src="~/Scripts/Globalization/es-ES.js">
</script>
```

As you can see, the script name is *es-ES.js* which matches the current
locale that we set in Global.asax.cs. However, there’s something odd
with that output. Do you see it? Notice that tilde in the *src*
attribute? Uh oh! That there is a bona fide bug in ASP.NET MVC.

Not to worry though, there’s an easy workaround. Knowing how
discriminating our ASP.NET MVC developers are, we knew that people would
want to place these scripts in whatever directory they want. Thus we
added a global override via the `AjaxHelper.GlobalizationScriptPath`
property.

Even better, these scripts are now available on the CDN as of this
morning (*thanks to
[Stephen](http://stephenwalther.com/ "Stephen Walther") and his team for
getting this done!*), so you can specify the CDN as the default
location. Here’s what I have in my *Global.asax.cs*.

```csharp
protected void Application_Start()
{
  AjaxHelper.GlobalizationScriptPath =     "http://ajax.microsoft.com/ajax/4.0/1/globalization/";
            
  AreaRegistration.RegisterAllAreas();
  RegisterRoutes(RouteTable.Routes);
}
```

With that in place, everything now just works. Let’s try filling out the
form again.

This time, *1,000* is not within the valid range because that’s
equivalent to *1* in the es-ES locale.

![invalid-range-es-ES](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GlobalizingASP.NETMVCClientValidation_A13C/invalid-range-es-ES_3.png "invalid-range-es-ES")

Meanwhile, *1.000* is within the valid range as that’s equivalent to
1,000.

![valid-range-es-ES](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GlobalizingASP.NETMVCClientValidation_A13C/valid-range-es-ES_3.png "valid-range-es-ES")

So what are these scripts?

They are simply a JavaScript serialization of all the info within a
`CultureInfo` object. So the information you can get on the server, you
can now get on the client with these scripts.

In Web Forms, these scripts are emitted automatically by serializing the
culture at runtime. However this approach doesn’t work for ASP.NET MVC.

One reason is that the scripts themselves changed from ASP.NET 3.5 to
ASP.NET 4. ASP.NET MVC is built against the ASP.NET 4 version of these
scripts. But since MVC 2 runs on both ASP.NET 3.5 and ASP.NET 4, we
couldn’t rely on the script manager to emit the scripts for us as that
would break when running on ASP.NET 3.5 which would emit the older
version of these scripts.

As usual, I have very **[simple sample you can
download](http://code.haacked.com/mvc-2/GlobalizationDemo.zip "Globalization Sample")**
to see the feature in action.

