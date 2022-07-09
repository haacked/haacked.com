---
title: "Tag Helper for Display Templates"
description: "ASP.NET Core doesn't include a declarative way to call a display template. Let's fix that."
tags: [aspnetcore, aspnetmvc, csharp]
---

I was minding my own business when [@dahlbyk](https://github.com/dahlbyk) (you may know him from such hits as PoshGit) dropped this comment on an already merged pull request.

![Conversation with Keith where he nerd snipes me into building a display for tag helper](https://user-images.githubusercontent.com/19977/178084800-cb7e5127-6129-48fc-aa02-e60e092cb598.png)

Display Templates? Now there's a name I haven't heard in a long time. As a refresher, [Display and Editor Templates](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/display-templates?view=aspnetcore-6.0) were first introduced as part of ASP.NET MVC. You could place partial views in the `Views/Shared/DisplayTemplates` folder (as well as a Controller specific subfolder) named after the type that the partial view is meant to render. So if you wanted all your booleans rendered in a particular way, You'd add a `Boolear.cshtml` partial view. Then make sure to call the `Html.DisplayFor(m => m.BooleanProperty)` helper.

Now you may think I'm being a bit precious with my complaint about the lack of a non-declarative way to call a display template. And you're right, I am precious.

Nevertheless, it bothers me that when you call a display template in a loop, you build an expression that doesn't use the expression variable. If that doesn't make sense, let me clarify with a code sample.

Suppose you want to use a display helper for the current model in a view. You can do something like this.

```razor
@model Member

@Html.DisplayFor(m => m.Welcomed)
```

That's pretty straightforward. The `m` in the expression is the current `Model` which is a `Member` type.

But if you're in a loop, you have to build an expression that doesn't use the expression variable.

```razor
@model IEnumerable<Member>

@foreach (var member in Model.Members) {
    <li>@Html.DisplayFor(_ => member.Welcomed)</li>
}
```

What if we could do this instead?

```razor
@model IEnumerable<Member>

@foreach (var member in Model.Members) {
    <li><display for="@member.Welcomed" /></li>
}
```

That would be pretty cool, right?

To do so, we need to build a [Tag Helper](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/intro?view=aspnetcore-6.0). Tag Helpers take advantage of the code model that the Razor parser builds when it parses your Razor template.

Here's how I started.

```csharp
[HtmlTargetElement("display")]
public class DisplayForTagHelper : TagHelper
{
    readonly IHtmlHelper _htmlHelper;

    /// <summary>
    /// An expression to be evaluated against the current model.
    /// </summary>
    [HtmlAttributeName("for")]
    public ModelExpression? For { get; set; }
}
```

The real interesting part here is the `For` property. Note that it's bound to the `for` attribute of the `display` tag and is a `ModelExpression`. So when we call the tag helper passing in `@member.Welcomed`, we're not getting the value of `member.Welcomed`, we're getting so much more!

But, I've run into a problem here. All the `Html.DisplayFor` methods take in an `Expression<Func<TModel, TResult>>`. And the `Html.Display` methods take in a string. I could not find a method that takes a `ModelExpression`. However, digging into the ASP.NET Core source code, I did discover that all the `Html.Display*` methods eventually delegate to a `protected virtual GenerateDisplay` method which takes a `ModelExplorer` to actually render a display template.

Unfortunately, that method is not public, so I can't just call it. But, since it's `protected virtual`, and the ASP.NET Core team rarely want to break consumers of their APIs, it's relatively safe to call that method with Reflection (_now I have two problems?_).

Let's complete the code.

```csharp
using System;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Serious.Abbot.Infrastructure.TagHelpers;

[HtmlTargetElement("display")]
public class DisplayForTagHelper : TagHelper
{
    readonly IHtmlHelper _htmlHelper;
    const string ForAttributeName = "for";

    /// <summary>
    /// An expression to be evaluated against the current model.
    /// </summary>
    [HtmlAttributeName(ForAttributeName)]
    public ModelExpression? For { get; set; }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public DisplayForTagHelper(IHtmlHelper htmlHelper)
    {
        _htmlHelper = htmlHelper;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(output);

        if (For is null)
        {
            return;
        }

        (_htmlHelper as IViewContextAware)?.Contextualize(ViewContext);

        // I could not find a way to call `DisplayFor` with a ModelExplorer parameter. It all expects an Expression.
        var generateDisplayMethod = _htmlHelper
            .GetType()
            .GetMethod("GenerateDisplay", BindingFlags.Instance | BindingFlags.NonPublic);
        var content = generateDisplayMethod?.Invoke(_htmlHelper, new[] {For.ModelExplorer, (object?)null, null, null})
            as IHtmlContent;

        output.TagName = null;
        output.Content.SetHtmlContent(content);
    }
}
```

Perhaps an even better approach is to write a class that inherits `HtmlHelper` and makes exposes a public version of `GenerateDisplay`. IIRC, there's a way to replace the built in `IHtmlHelper` with your own, but I forget how to do it. I'll leave that as an exercise to the reader.

Likewise, I'll probably write an `<editor for="..." />` tag helper as well. Till next time.