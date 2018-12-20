---
title: Model Metadata and Validation Localization using Conventions
date: 2011-07-14 -0800
tags: [localization,aspnetmvc,validation]
redirect_from: "/archive/2011/07/13/model-metadata-and-validation-localization-using-conventions.aspx/"
---

By default, ASP.NET MVC leverages Data Annotations to provide validation. The approach is easy to get started with and allows the
validation applied on the server to “float” to the client without any extra work.

However, once you get localization involved, using Data Annotations can really clutter your models. For example, the following is a simple model class with two properties.

```csharp
public class Character {
  public string FirstName { get; set; }
  public string LastName { get; set; }
}
```

Nothing to write home about, but it is nice, clean, and simple.  To make it more useful, I’ll add validation and format how the properties are displayed.

```csharp
public class Character {
  [Display(Name="First Name")]
  [Required]
  [StringLength(50)]]
  public string FirstName { get; set; }
  
  [Display(Name="Last Name")]
  [Required]
  [StringLength(50)]]
  public string LastName { get; set; }
}
```

That’s busier, but not horrible. It sure is awful Anglo-centric though.
I’ll fix that by making sure the property labels and error messages are
pulled from a resource file.

```csharp
public class Character {
  [Display(Name="Character_FirstName",
    ResourceType=typeof(ClassLib1.Resources))]
  [Required(ErrorMessageResourceType=typeof(ClassLib1.Resources), 
    ErrorMessageResourceName="Character_FirstName_Required")]
  [StringLength(50, ErrorMessageResourceType = typeof(ClassLib1.Resources),
    ErrorMessageResourceName = "Character_FirstName_StringLength")]
  public string FirstName { get; set; }

  [Display(Name="Character_LastName",
    ResourceType=typeof(ClassLib1.Resources))]
  [Required(ErrorMessageResourceType=typeof(ClassLib1.Resources), 
    ErrorMessageResourceName="Character_LastName_Required")]
  [StringLength(50,
    ErrorMessageResourceType = typeof(ClassLib1.Resources),
    ErrorMessageResourceName = "Character_LastName_StringLength")]
  public string LastName { get; set; }
}
```

Wow! I don’t know about you, but I feel a little bit dirty typing all that in. Allow me a moment as I go wash up.

So what can I do to get rid of all that noise? Conventions to the rescue! By employing a simple set of conventions, I should be able to
look up error messages in resource files as well as property labels without having to specify all that information. In fact, by convention I shouldn’t even need to use the `DisplayAttribute`.

I wrote a custom **PROOF OF CONCEPT** `ModelMetadataProvider` that supports this approach. More specifically, mine is derived from the
`DataAnnotationsModelMetadataProvider`.

What Conventions Does It Apply?
-------------------------------

The nice thing about this convention based model metadata provider is it allows you to specify as little or as much of the metadata you need and it fills in the rest.

### Providing minimal metadata

For example, the following is a class with one simple property.

```csharp
public class Character {
  [Required]
  [StringLength(50)]
  public string FirstName {get; set;}
}
```

When displayed as a label, the custom metadata provider looks up the resource key, *{ClassName}\_{PropertyName},*and uses the resource value as the label. For example, for the `FirstName` property, the provider uses the key `Character_FirstName` to look up the label in the resource file. I’ll cover how resource type is specified later.

If a value for that resource is not found, the code falls back to using the property name as the label, but splits it using Pascal/Camel casing as a guide. Therefore in this case, the label is “*First Name*”.

The error message for a validation attribute uses a resource key of *{ClassName}\_{PropertyName}\_{AttributeName}*. For example, to locate the error message for a `RequiredAttribute`, the provider finds the resource key `Character_FirstName_Required`.

### Partial Metadata

There may be cases where you can provide some metadata, but not all of it. Ideally, the metadata that you don’t supply is inferred based on the conventions. Going back to previous example again:

```csharp
public class Character {
  [Required(ErrorMessageResourceType=typeof(MyResources))]
  [StringLength(50, ErrorMessageResourceName="StringLength_Error")]
  [Display(Name="First Name")]
  public string FirstName {get; set;}
}
```

Notice that the first attribute only specifies the error message resource type. In this case, the specified resource type will override
the default resource type. But the resource key is still inferred by convention (aka `Character_FirstName_Required`).

In contrast, notice that the second `StringLengthAttribute`, only specifies the resource name, and doesn’t specify a resource type. In
this case, the specified resource name is used to look up the error message using the default resource type. As you might expect, if the `ErrorMessage` property is specified, that takes precedence over the conventions.

The `DisplayAttribute` works slightly differently. By default, the `Name` property is used as a resource key if a resource type is also
specified. If no resource type is specified, the `Name` property is used directly. In the case of this convention based provider, an attempt to lookup a resource value using the `Name` property as a resource always occurs before falling back to the default behavior.

Configuration
-------------

One detail I haven’t covered yet is what resource type is used to find these messages? Is that determined by convention?

Determining this by convention would be tricky so it’s the one bit of information that must be explicitly specified when configuring the provider itself. The following code in *Global.asax.cs* shows how to configure this.

```csharp
ModelMetadataProviders.Current = new ConventionalModelMetadataProvider(
  requireConventionAttribute: false,
  defaultResourceType: typeof(MyResources.Resource)
);
```

The model metadata provider’s constructor has two arguments used to configure it.

Some developers will want the conventions to apply to every model, while others will want to be explicit and have models opt in to this behavior. The first argument, `requireConventionAttribute`, determines whether the conventions only apply to classes with the
`MetadataConventionsAttribute` applied.

The explicit folks will want to set this value to true so that only classes with the `MetadataConventionsAttribute` applied to them (or
classes in an assembly where the attribute is applied to the assembly) will use these conventions.

The attribute can also be used to specify the resource type for resource strings.

The second property specifies the default resource type to use for resource strings. Note that this can be overridden by any attribute that specifies its own resource type.

Caveats, Issues, Potholes
-------------------------

This code is something I hacked together and there are a few issues to consider that I could not easily work around. First of all, the
implementation has to mutate properties of attributes. In general, this is not a good thing to do because attributes tend to be global. If other code relies on the attributes having their original values, this could cause issues.

I think for most ASP.NET MVC applications (in fact most web applications period) this will not be an issue.

Another issue is that the conventions don’t work for implied validation. For example, if you have a property of a simple value type (such as int), the `DataAnnotationsValidatorProvider` supplies a `RequiredValidator` to validate the value. Since this validator didn’t
come from an attribute, it won’t use my convention based lookup for its error messages.

I thought about making this work, but it the hooks I need to do this without a large amount of code don’t appear to be there. I’d have to write my own validator provider (as far as I can tell) or register my own validator adapters in place of the default ones. I wasn’t up to the task just yet.

Try it out
----------

-   **NuGet Package:** To try it in your application, install it using NuGet: `Install-Package ModelMetadataExtensions`
-   **Source Code:**The source code is [up on GitHub](https://github.com/Haacked/mvc-metadata-conventions "GitHub").
