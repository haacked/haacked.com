---
title: What&rsquo;s the Difference Between a Value Provider and Model Binder?
tags: [aspnet,aspnetmvc,code]
redirect_from: "/archive/2011/06/29/whatrsquos-the-difference-between-a-value-provider-and-model-binder.aspx/"
---

ASP.NET MVC 3 introduced the ability to bind an incoming JSON request to an action method parameter, which is something I [wrote about
before](https://haacked.com/archive/2010/04/15/sending-json-to-an-asp-net-mvc-action-method-argument.aspx "Sending JSON to an action method").

For example, suppose you have the following class defined (keeping it really simple here):

```csharp
public class ComicBook
{
  public string Title { get; set; }
  public int IssueNumber { get; set; }
}
```

And you have an action method that accepts an instance of ComicBook:

```csharp
[HttpPost]
public ActionResult Update(ComicBook comicBook)
{
  // Do something with ComicBook and return an action result
}
```

You can easily post a comic book to that action method using JSON.

Under the hood, ASP.NET MVC uses the `DefaultModelBinder` in combination with the `JsonValueProviderFactory` to bind that value.

A question on an internal mailing list recently asked the question (and I’m paraphrasing here), **“Why not cut out the middle man (the value provider) and simply deserialize the incoming JSON request directly to the model (`ComicBook` in this example)?”**

Great question! Let me provide a bit of background to set the stage for the answer.

Posting Content to an Action
----------------------------

There are a couple of different content types you can use when posting data to an action method.

### application/x-www-form-urlencoded

You may not realize it, but when you submit a typical HTML form, the content type of that submission is `application/x-www-form-url-encoded`.

As you can see in the screenshot below from Fiddler, the contents of the form is posted as a set of name value pairs separated by ampersand characters. The name and value within each pair are separated by an
equals sign.

By the time you typically interact with this data (outside of model binding), it’s in the form of a dictionary like interface via the
`Request.Form` name value collection.

The following screenshot shows what such a request looks like using Fiddler.

![form-encoded-post](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Why-Isnt-The-JsonValueProviderFactory_EADC/form-encoded-post_3.png "form-encoded-post")

When content is posted in this format, the `DefaultModelBinder` calls into the `FormValueProvider` asking for a value for each property of the model. The `FormValueProvider` is a very thin abstraction over the
`Request.Form` collection.

### application/json

Another content type you can use to post data is `application/json`. As you might guess, this is simply JSON encoded data.

Here’s an example of a bit of JavaScript I used to post the same content as before but using JSON. Note that this particular snippet requires jQuery and a browser that natively supports the `JSON.stringify` method.

```html
<script>
    $(function() {
        var comicBook = { Title: "Groo", IssueNumber: 101 }
        var comicBookJSON = JSON.stringify(comicBook);
        $.ajax({
            url: '/home/update',
            type: 'POST',
            dataType: 'json',
            data: comicBookJSON,
            contentType: 'application/json; charset=utf-8',
        });
    });
</script>
```

When this code executes, the following request is created.

![json-encoded-post](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Why-Isnt-The-JsonValueProviderFactory_EADC/json-encoded-post_3.png "json-encoded-post")

Notice that the content is encoded as JSON rather than form url encoded.

JSON is a serialization format so it’s in theory possible that we could straight deserialize that post to a `ComicBook` instance. Why don’t we do that? Wouldn’t it be more efficient?

To understand why, let’s suppose we did use serialization and walk through a common scenario. Suppose someone submits the form and they enter a string instead of a number for the field `IssueNumber`. You’d
probably expect to see the following.

[![form-validation-error](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Why-Isnt-The-JsonValueProviderFactory_EADC/form-validation-error_thumb.png "form-validation-error")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Why-Isnt-The-JsonValueProviderFactory_EADC/form-validation-error_2.png)

Notice that the model binding was able to determine that the Title was submitted correctly, but that the `IssueNumber` was not.

If our model binder deserialized JSON into a `ComicBook` it would not be able to make that determination because **serialization is an all or nothing affair**. When serialization fails, all you know is that the format didn’t match the type. You don’t have access to the granular details we need to provide property level validation. So all you’d be
able to show your users is an error message stating something went wrong, good luck figuring out what.

The Solution
------------

Instead, what we really want is a way bind each property of the model one at a time so we can determine which of the fields are valid and which ones are in error. Fortunately, the `DefaultModelBinder` already knows how to do that when working with the dictionary-like
`IValueProvider` interface.

So all we need to do is figure out how to expose the posted JSON encoded content via the `IValueProvider` interface. As I wrote before, [Jonathan Carter](http://lostintangent.com/ "Jonathan Carter's Blog") had the bit of insight that provided the solution to this problem. He realized that you could have the JSON value provider deserialize the incoming JSON post to a dictionary. Once you have a dictionary, it’s pretty easy to implement `IValueProvider` and the `DefaultModelBinder` already knows how to bind those values to a type while providing property level validation. Score!

Value Provider Aggregation
--------------------------

The answer I provided only tells part of the story of why this is implemented as a value provider. There’s another aspect that was
illustrated by my co-worker Levi. Sadly, for someone so gifted intellectually, he has no blog, so I’ll paraphrase his words here (with
a bit of verbatim copying).

As I mentioned earlier, value providers provide an abstraction over where values actually come from. Value providers are responsible for aggregating the values that are part of the current request, e.g. from Form collection, the query string, JSON, etc.  They basically say “I don’t know what a ‘*FirstName*’ is for or what you can do with it, but if you ask me for a ‘*FirstName*’ I can give you what I have.”

Model binders are responsible for querying the value providers and building up objects based on those results.  They basically say “I don’t know where directly to find a ‘*FirstName*’, ‘*LastName*’, or ‘*Age*’, but if the value provider is willing to give them to me then I can create a Person object from them.”

Since model binders aren’t locked to individual sources (with some necessary exceptions, e.g. `HttpPostedFile`), they can build objects **from an aggregate of sources**. If your `Person` type looks like this:

```csharp
public class Person
{
  int Id { get; set; }
  [NonNegative]
  int Age { get; set; }
  string FirstName { get; set; }
  string LastName { get; set; }
}
```

And a client makes a JSON POST request to an action method (say with the url */person/edit/1234* with the following content:

```csharp
{ 
  "Age": 30, 
  "FirstName": "John", 
  "LastName": "Doe" 
} 
```

The `DefaultModelBinder` will pull the `Id` value from the `RouteData` and the `Age`, `FirstName`, and `LastName` values from the JSON when building up the `Person` object. Afterwards, it’ll perform validation without having to know that the various values came from different sources.

Even better, if you wrote a custom `Person` model binder and made it agnostic as to the current `IValueProvider`, you’d get the correct behavior on incoming JSON requests without having to change your model binder code one tiny iota.  Neither of these is possible if the model binder is hard-coded to a single provider.

TL;DR Summary
-------------

The goal of this post was to provide a bit of detail around an interesting aspect of how ASP.NET MVC turns strings sent to a web server into strongly typed objects passed into your action methods.

Going back to the original question, the answer is simply, **we use a value provider for JSON to enable property level validation of the incoming post and also so that model binding can build up an object by aggregating multiple sources of data without having to know anything about those sources**.

