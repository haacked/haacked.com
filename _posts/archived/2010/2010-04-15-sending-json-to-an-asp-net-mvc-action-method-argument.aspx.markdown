---
title: Sending JSON to an ASP.NET MVC Action Method Argument
tags: [json,aspnetmvc]
redirect_from: "/archive/2010/04/14/sending-json-to-an-asp-net-mvc-action-method-argument.aspx/"
---

UPDATE: The `JsonValueProviderFactory` is now registered by default in ASP.NET MVC 3. So if you’re using ASP.NET MVC 3, you can ignore that part of this blog post.

[Javier “G Money” Lozano](http://lozanotek.com/blog/ "Javier Lozano's Blog"), one of the
good folks involved with [C4MVC](http://www.c4mvc.net/ "Community for MVC.NET"), recently wrote a
[blog post](http://lozanotek.com/blog/archive/2010/04/16/posting_json_data_to_mvc_controllers.aspx "JSON Data")
on posting [JSON](http://json.org/ "Introducing JSON") (JavaScript
Object Notation) encoded data to an MVC controller action. In his post,
he describes an interesting approach of using a custom model binder to
bind sent JSON data to an argument of an action method. Unfortunately,
his sample left out the custom model binder and only demonstrates how to
*retrieve*JSON data sent from a controller action, not how to send the
JSON to the action method. Honest mistake. :)

His post reminds me of how remiss I’ve been in blogging recently because
a while back, we added something to our [ASP.NET MVC 2 Futures
library](http://aspnet.codeplex.com/releases/view/41742#DownloadId=110348 "ASP.NET MVC 2 Futures Library")
that handles sending JSON to an action method but I just never found
time to blog about it.

There’s one key problem with using a model binder to accept JSON. By
writing a custom model binder, you miss out on validation. Using his
example, if you type “abc” for the `Age` field, you will get a
serialization failure when attempting to serialize the JSON into the
`PersonInputModel` object because Age is an `Int32 `and the
serialization will fail.

### Value Providers to the rescue!

This is where value providers, a new feature of ASP.NET MVC 2, enters to
save the day. Whereas model binders are used to bind incoming data to an
object model, value providers provide an abstraction for the incoming
data itself.

When the ASP.NET MVC feature team first implemented value providers,
[Jonathan Carter](http://lostintangent.com/ "Lost in Tangent") and I
were working on a client templating sample which sent JSON to an action
method. Rather than write a custom model binder which was the approach I
took, Jonathan had the unique insight to write a custom value provider
which received JSON data and serialized it to a dictionary rather than
the target object. The beauty of his approach is that this dictionary
data is then passed to the default model binder which binds it to the
final object **with validation**!

I took is his prototype and added the `JsonValueProviderFactory` to our
[ASP.NET MVC 2 Futures
library](http://aspnet.codeplex.com/releases/view/41742#DownloadId=110348 "ASP.NET MVC 2 Futures Library on CodePlex")
and then totally didn’t write about it. Yes, I suck.

### Setting it up

To get started, download the ASP.NET MVC 2 Futures Library and reference
the `Microsoft.Web.Mvc.dll` assembly. Then, in your Global.asax.cs file,
add the following call to register the `JsonValueProviderFactory`.

```csharp
protected void Application_Start() 
{
  RegisterRoutes(RouteTable.Routes);
  ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
}
```

That’s it! You’re done!

This value provider will handle requests that are encoded as
`application/json`. There’s no need to specify a model binder on classes
that accept JSON input.

### See it in action

I took the liberty of updating Javier’s sample to use this new value
provider and to actually post JSON to the action method.

It turns out, sending JSON encoded data to an action method with jQuery
was not as straightforward as I hoped. If you know of a more
straightforward way, let me know. I ended up using [a JSON plug-in for
jQuery](http://www.overset.com/2008/04/11/mark-gibsons-json-jquery-updated/ "JSON jQuery Plugin")
I found on the Internets. This provides a `$.toJSON` method I could use
to serialize an object into a JSON encoded string. Here’s the updated
client script code.

UPDATE: Per [Dave Ward’s comment
here](https://haacked.com/archive/2010/04/15/sending-json-to-an-asp-net-mvc-action-method-argument.aspx#77068 "Useful comment")
I should be using [json2.js](http://www.json.org/js.html) and its
`JSON.stringify(...)` method instead because it matches an API that some
browsers implement and will use the native implementation if it exists.
Nice! I’ll update this blog post later when I have a moment.

```csharp
$(function () {
    $("#personCreate").click(function () {
        var person = getPerson();

        // poor man's validation
        if (person == null) {
            alert("Specify a name please!");
            return;
        }

        var json = $.toJSON(person);

        $.ajax({
            url: '/home/save',
            type: 'POST',
            dataType: 'json',
            data: json,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // get the result and do some magic with it
                var message = data.Message;
                $("#resultMessage").html(message);
            }
        });
    });
});

function getPerson() {
    var name = $("#Name").val();
    var age = $("#Age").val();

    // poor man's validation
    return (name == "") ? null : { Name: name, Age: age };
}
```

Notice that we use the `$.ajax` method to specify both the JSON data and
the JSON content type for the request.

A quick check in Fiddler confirms that the data in the POST request is
properly JSON encoded.

![json-request-fiddler](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/SendingJSONtoanASP.NETMVCActionMethod_7E01/json-request-fiddler_3.png "json-request-fiddler")

Now, within my action method, I can actually check to see if the model
state is valid and if not, return an error message.

```csharp
[HttpPost]
public ActionResult Save(PersonInputModel inputModel) {
  if (ModelState.IsValid)
  {
    string message = string.Format("Created user '{0}' aged '{1}' in the system."
      , inputModel.Name, inputModel.Age);
    return Json(new PersonViewModel { Message = message });
  }
  else {
    string errorMessage = "<div class=\"validation-summary-errors\">" 
      + "The following errors occurred:<ul>";
    foreach (var key in ModelState.Keys) {
      var error = ModelState[key].Errors.FirstOrDefault();
      if (error != null) {
        errorMessage += "<li class=\"field-validation-error\">" 
         + error.ErrorMessage + "</li>";
      }
    }
    errorMessage += "</ul>";
    return Json(new PersonViewModel { Message = errorMessage });
  }
}
```

And as you can see in the Fiddler screenshot, I sent an invalid Age to
the server and yet, it all still works.

![validation-with-json](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/SendingJSONtoanASP.NETMVCActionMethod_7E01/validation-with-json_3.png "validation-with-json")

Whew! I can finally cross this off of my immense blog backlog. :)
Hopefully soon, I’ll blog a more detailed write-up of value providers.

We have plans to add the `JsonValueProviderFactory` to ASP.NET MVC 3 so
that it’s a built-in feature. I hope you find this useful and as always,
let me know if there are ways we can improve it!

Oh, and here’s **[Javier’s updated sample with the value
provider](http://code.haacked.com/mvc-2/JSONMvc.zip "Sending JSON to ASP.NET MVC Controller Sample")**.

