---
title: Uploading a File (Or Files) With ASP.NET MVC
tags: [aspnetmvc]
redirect_from: "/archive/2010/07/15/uploading-files-with-aspnetmvc.aspx/"
---

I wanted to confirm something about how to upload a file or set of files with ASP.NET MVC and the first search result for the phrase “uploading a file with asp.net mvc” is [Scott Hanselman’s blog post](http://www.hanselman.com/blog/ABackToBasicsCaseStudyImplementingHTTPFileUploadWithASPNETMVCIncludingTestsAndMocks.aspx "Implementing HTTP File Upload") on the topic.

His blog post is very thorough and helps provide a great understanding of what’s happening under the hood. The only quibble I have is that the code could be much simpler since we’ve made improvements to the ASP.NET MVC 2. I write this blog post in the quixotic hopes of knocking his post from the #1 spot.

### Uploading a single file

Let’s start with the view. Here’s a form that will post back to the current action.

```csharp
<form action="" method="post" enctype="multipart/form-data">
  
  <label for="file">Filename:</label>
  <input type="file" name="file" id="file" />

  <input type="submit" />
</form>
```

Here’s the action method that this view will post to which saves the file into a directory in the `App_Data` folder named “uploads”.

```csharp
[HttpPost]
public ActionResult Index(HttpPostedFileBase file) {
            
  if (file.ContentLength > 0) {
    var fileName = Path.GetFileName(file.FileName);
    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
    file.SaveAs(path);
  }
            
  return RedirectToAction("Index");
}
```

Notice that the argument to the action method is an instance of `HttpPostedFileBase`. ASP.NET MVC 2 introduces a new value providers feature which [I’ve covered before](https://haacked.com/archive/2010/04/15/sending-json-to-an-asp-net-mvc-action-method-argument.aspx "Sending Json").

> Whereas model binders are used to bind incoming data to an object
> model, value providers provide an abstraction for the incoming data
> itself.

In this case, there’s a default value provider called the `HttpFileCollectionValueProvider` which supplies the uploaded files to the model binder.Also notice that the argument name, *file*, is the same name as the name of the file input. This is important for the model binder to match up the uploaded file to the action method argument.

Uploading multiple files
------------------------

In this scenario, we want to upload a set of files. We can simply have multiple file inputs all with the same name.

```csharp
<form action="" method="post" enctype="multipart/form-data">
    
  <label for="file1">Filename:</label>
  <input type="file" name="files" id="file1" />
  
  <label for="file2">Filename:</label>
  <input type="file" name="files" id="file2" />

  <input type="submit"  />
</form>
```

Now, we just tweak our controller action to accept an `IEnumerable` of `HttpPostedFileBase` instances. Once again, notice that the argument name matches the name of the file inputs.

```csharp
[HttpPost]
public ActionResult Index(IEnumerable<HttpPostedFileBase> files) {
  foreach (var file in files) {
    if (file.ContentLength > 0) {
      var fileName = Path.GetFileName(file.FileName);
      var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
      file.SaveAs(path);
    }
  }
  return RedirectToAction("Index");
}
```

Yes, it’s that easy. :)
