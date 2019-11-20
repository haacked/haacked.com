---
title: "Build an ASP.NET Core Update Panel with Vanilla JavaScript in Four Easy Steps"
description: "Sometimes you just need a simple way to post a form and update an element on your site."
tags: [aspnet, js]
excerpt_image: https://user-images.githubusercontent.com/19977/69273780-dfb9a000-0b8d-11ea-98e3-81a99d12f49f.jpg
---

Sometimes you just need to submit a form and update a portion of your web page without a lot of fuss and muss. Today, you have a lot of options for dynamically updating the DOM based on changes made on the server. You could use React, Vue, Angular, SignalR, and Blazor.

The choice you make will depend a lot on your experience and your scenario and how much complexity you can endure. For a site I'm building, I like to start as simple as possible and only add in components as the pain they solve outweighs the complexity they add. For example, the site I'm working on is very content based with pages. It's not a single page app (SPA), so React isn't necessarily a great choice for me. But it might be for you. Also, progressive enhancement and accessibility is important to me.

![Vanilla flower](https://user-images.githubusercontent.com/19977/69273780-dfb9a000-0b8d-11ea-98e3-81a99d12f49f.jpg)

In this post, I'll walk through building "Update Panel" style functionality. I'll start with an old school form that posts a field like the cavepeople used to do. Then we'll ajaxify it, like the more recent cave people did. But we'll do it all without jQuery. Yes, I'm a glutton for punishment.

For those following along at home, all the [code is in a GitHub repository](https://github.com/haacked/UpdatePanelExample/commits/master). Each commit in the repo corresponds to a step in this post.

## Step 1 - Generate the project
This assumes you have the .NET Core SDK installed.

```bash
md MyProjectName
cd MyProjectName
dotnet new webapp
```

This generates a simple ASP.NET Razor Pages website. Razor Pages are great for simple demos as they cut out a lot of ceremony.

## Step 2 - [Build a simple form](https://github.com/haacked/UpdatePanelExample/commit/b431650bb6cfb75d9560bf9caeaf360f438d7dba)

In this step, we build a super simple form. The kind that does a full round trip to render the results. The kind of form I made back when the web was still swaddled in diapers.

__/Pages/Index.cshtml__

```html
<div id="the-message" class="flash">
    @Model.TheMessage
</div>

<form method="post">
    <div asp-validation-summary="ModelOnly" class="text-danger"></div>
    <div class="form-group">
        <label asp-for="@Model.TheValue"></label>
        <input asp-for="@Model.TheValue" />
        <span asp-validation-for="@Model.TheValue" class="text-danger"></span>
    </div>
    <input type="submit" value="Submit"/>
</form> 
```

__/Pages/Index.cshtml.cs__

```html
[Required]
[BindProperty]
public string TheValue { get; set; }

public string TheMessage { get; private set; }

public IActionResult OnGet()
{
    TheMessage = "Nothing yet";
    return Page();
}

public IActionResult OnPost()
{
    if (!ModelState.IsValid)
    {
        return OnGet();
    }

    TheMessage = $"Yay, you posted '{TheValue}'";
    return Page();
}
```

This form has a single text input. Fill it out, click the submit button, and a message is displayed with your input. Groundbreaking!

![Simple form](https://user-images.githubusercontent.com/19977/69276679-c0be0c80-0b93-11ea-9516-e8fc0d9b085f.png)

## Step 3 - [Ajaxify it](https://github.com/haacked/UpdatePanelExample/commit/1cb9921f63ab14886ef0de97f131d43aef13deba)

The first thing we'll do here is get all forms marked with a `data-update-target-selector` attribute. The value of that attribute is the selector used to find the element to update when the form returns.

__/wwwroot/js/site.js__

```js
// Set up ajaxified forms
document.querySelectorAll('form[data-update-target-selector]')
    .forEach(form => {
    const document = form.ownerDocument;

    // Implementation forthcoming...

});
```

Now let's dig into the implementation. The following code is inserted where the `// Implementation forthcoming...` is located.

```js
// Set up submitter property on submit button click
// This lets us include the button value in form data.
// And lets us disable the button while submitting.
form.addEventListener('click', evt => {
    form.submitter = evt.target.closest('[type=submit]')
});
```

The first thing we do is find the submit button. This could be an `<input type="submit">` or a `<button type="submit">`. This will let us include the button value (in case that's important to form submission. It also lets us disable the button until the form post returns.

Now let's add an event listener to the form that listens to the `submit` event.

```js
form.addEventListener('submit', evt => {
    evt.preventDefault();

    const formData = new FormData(form);
    if (form.submitter) {
        // Append the button name and value to the form data.
        formData.append(form.submitter.name, form.submitter.value);
        form.submitter.setAttribute('disabled', 'disabled');
    }
    fetch(form.action, {
        method: 'POST'
        body: formData
    })
```

This uses the built-in `fetch` function to post the form to the server. But now we need to do something with the response. So append the following code to the previous block.

```js
.then(response => {
    if (form.submitter) {
        // Re-enable the submit button
        form.submitter.removeAttribute('disabled');
}
if (!response.ok) throw response;
    return response.text()
}).then(html => {
    // The update target selector might refer to an element within the form.
    // We give precedence to that one. Otherwise we search the whole document.
    const updateTarget = form.querySelector(form.dataset.updateTargetSelector)
        || document.querySelector(form.dataset.updateTargetSelector);
    if (updateTarget) {
        updateTarget.innerHTML = html
    }
    // Clear inputs
    form.reset();
})
```

The code then takes the response and updates the update target element with the contents of the response. But we have a problem here. The response is still the full web page! That's not good.

We need a way in the controller to know whether or not this is an ajax request or a full form post. So let's tweak the `fetch` method and add the following.

```js
fetch(form.action, {
    method: 'POST',
    headers: {
        'X-Requested-With': 'XmlHttpRequest'
    },
    body: formData
})
```

We add a header to let the server know we're making an Ajax request. The header could be anything. The header I chose is what old school ASP.NET MVC had support for. But we need to add that support ourselves with an extension method.

__/Pages/RequestExtension.cs__

```csharp
public static class RequestExtensions
{
    public const string XmlHttpRequest = nameof(XmlHttpRequest);

    public static bool IsAjaxRequest(this HttpRequest request)
    {
        return request.Headers["X-Requested-With"] == XmlHttpRequest;
    }
}
```

Now in our post handler, we can alter the response based on the type of request. Add this code just before the call to `return Page();`.

__/Pages/Index.cshtml.cs__

```csharp
if (Request.IsAjaxRequest())
{
    // Typically you'd return a partial here.
    return Content(TheMessage);
}
```

Then the last step is to modify the form to point to the update target.

__/Pages/Index.cshtml__

```html
<form method="post" data-update-target-selector="#the-message">
```

## Step 4 - [Support for appending](https://github.com/haacked/UpdatePanelExample/commit/2a043d8228bf4a66def5456aa8246db8bc4b2380)

Oh, we're not done yet. Suppose we want to append the response, not replace it. Let's build support for that. The following code replaces the line `updateTarget.innerHTML = html` in `site.js`.

__/wwwroot/js/site.js__

```js
const updateTarget = form.querySelector(form.dataset.updateTargetSelector)
    || document.querySelector(form.dataset.updateTargetSelector);
if (updateTarget) {
    updateTarget.innerHTML = html
    const updateType = form.dataset.updateType || 'replace';
    if (updateType === 'replace') {
        updateTarget.innerHTML = html
    }
    else /* append */ {
        const div = document.createElement('div');
        div.innerHTML = html;
        updateTarget.appendChild(div.firstChild);
    }
}
```

This code introduces a new attribute we can use on the form. Let's update the view so that we're appending to a list now.

__/Pages/Index.cshtml__

```html
<ul id="the-message" class="flash">
    <li>@Model.TheMessage</li>
</ul>

<form method="post" data-update-target-selector="#the-message" data-update-type="append">
```

Notice the new `data-update-type` attribute. If it's set to `append` the code appends the response to the element indicated by `data-update-target-selector`.

We need to make one slight change to `Index.cshtml.cs`.

```csharp
if (Request.IsAjaxRequest())
{
    // Typically you'd return a partial here.
    return Content($"<li>{TheMessage}</li>");
}
```

And we're done. You can see all the changes in order by [looking at the commits](https://github.com/haacked/UpdatePanelExample/commits/master) in order.

Or just see the final result at https://github.com/haacked/UpdatePanelExample.

![Final result](https://user-images.githubusercontent.com/19977/69278718-baca2a80-0b97-11ea-8cd9-be253868733f.png)
