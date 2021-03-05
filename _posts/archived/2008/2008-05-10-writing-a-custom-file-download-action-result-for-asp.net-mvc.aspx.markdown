---
title: Writing A Custom File Download Action Result For ASP.NET MVC
tags: [aspnetmvc]
redirect_from: "/archive/2008/05/09/writing-a-custom-file-download-action-result-for-asp.net-mvc.aspx/"
---

NEW UPDATE: There is no longer need for this custom ActionResult because
ASP.NET MVC now includes one in the box.

UPDATE: I’ve updated the sample to include a new [lambda based action
result](https://haacked.com/archive/2008/05/11/delegating-action-result.aspx "Delegating Action Result").
This also fixes an issue with the original download in which I included
the wrong assembly.

The [April CodePlex source drop of ASP.NET
MVC](http://www.codeplex.com/aspnet/Release/ProjectReleases.aspx?ReleaseId=12640 "April CodePlex release")
introduces the concept of returning an `ActionResult` instance from
action methods.
[ScottGu](http://weblogs.asp.net/scottgu/ "Scott Guthrie") wrote about
this [change on his
blog](http://weblogs.asp.net/scottgu/archive/2008/04/16/asp-net-mvc-source-refresh-preview.aspx "ASP.NET MVC Source Refresh Preview").

In this post, I’ll walk through building a custom action result for
downloading files. As you’ll see, they are extremely easy to build.
Let’s start at the end and see what the end-user behavior of this new
result will be.

Here's a page that contains a link to an action method named
`Download`*.*This action method returns this new `DownloadResult` action
result.

![File Download
HomePage](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WritingACustomFileDownloadActionR.NETMVC_E009/FileDownloadHome_3.png)

Clicking on the link then pops up this dialog, prompting you to download
and save the file.

![File
Download](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WritingACustomFileDownloadActionR.NETMVC_E009/File%20Download_3.png)

The code for this action is pretty simple.

```csharp
public ActionResult Download() 
{
  return new DownloadResult 
    { VirtualPath="~/content/site.css", FileDownloadName = "TheSiteCss.css" };
}
```

Notice that you just need to give the result two pieces of information,
the virtual path to the file to send to the browser and the default
filename to save the file as on the browser.

The virtual path is set via `VirtualPath` property (surprise surprise!).
Note that I could have chosen to make this parameter accept the full
file path instead of a virtual path, but I didn’t want to force users of
this class to fake out a `Server.MapPath` call in a unit test. In any
case, the change is trivial for those who prefer that approach. I might
add overloads that accept a Stream, etc...

The file download name is set via the `FileDownloadName` property.
Notice that this is the filename that the user is prompted with.

If the `FileDownloadName` property is set, the `ExecuteResult` method
makes sure to add the correct [content-disposition
header](http://www.ietf.org/rfc/rfc2183.txt "RFC 2183 Content-Disposition Header")
which causes the browser to prompt the user to save the file.

For those familiar with Design Patterns, action results follow the
pattern commonly known as the [Command
Pattern](http://en.wikipedia.org/wiki/Command_pattern "Command Pattern on Wikipedia").
An action method returns an instance that embodies an command that the
framework needs to perform next. This provides a means for delaying the
execution of framework/pipeline code until after your action method is
complete, rather than from within your action method, which makes unit
testing much nicer.

Speaking of unit tests, here’s the unit test for that download action
method I wrote. As you can see, it is quite simple.

```csharp
[TestMethod]
public void DownloadActionSendsCorrectFile() {
  var controller = new HomeController();

  var result = controller.Download() as DownloadResult;

  Assert.AreEqual("TheSiteCss.css", result.FileDownloadName);
  Assert.AreEqual("~/content/site.css", result.VirtualPath);
}
```

Here’s the code for the `DownloadResult` class. This is the class that
does all the work (not that there is much work to do). I do have unit
tests of this class in the included source code which demonstrate how to
unit test a custom action result.

```csharp
public class DownloadResult : ActionResult {

  public DownloadResult() {
  }

  public DownloadResult(string virtualPath) {
    this.VirtualPath = virtualPath;
  }

  public string VirtualPath {
    get;
    set;
  }

  public string FileDownloadName {
    get;
    set;
  }

  public override void ExecuteResult(ControllerContext context) {
    if (!String.IsNullOrEmpty(FileDownloadName)) {
      context.HttpContext.Response.AddHeader("content-disposition", 
        "attachment; filename=" + this.FileDownloadName)
    }

    string filePath = context.HttpContext.Server.MapPath(this.VirtualPath);
    context.HttpContext.Response.TransmitFile(filePath);
    }
}
```

*I removed the download since this code is no longer needed nor
relevant.*

