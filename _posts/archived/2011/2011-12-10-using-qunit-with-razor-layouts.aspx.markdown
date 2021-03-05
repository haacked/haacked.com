---
title: Using QUnit with Razor Layouts
tags: [aspnet,aspnetmvc,tdd,code,razor]
redirect_from: "/archive/2011/12/09/using-qunit-with-razor-layouts.aspx/"
---

Given how central JavaScript is to many modern web applications,  it is
important to use unit tests to drive the design and quality of that
JavaScript. But I’ve noticed that there are a lot of developers that
don’t know where to start.

There are many test frameworks out there, but the one I love is
[QUnit](http://docs.jquery.com/QUnit "QUnit homepage"), the jQuery unit
test framework.

![qunit-tests-running](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/cfa3790769b8_117F0/qunit-tests-running_3.png "qunit-tests-running")

Most of my experience with QUnit is writing tests for a client script
library such as a jQuery plugin. Here’s an example of one QUnit [test
file I
wrote](https://github.com/Haacked/jquery.undoable/blob/master/tests/index.html "QUnit test file")
a while ago (so you know it’s nasty).

You’ll notice that the entire set of tests is in a single static HTML
file.

I saw a recent blog post by [Jonathan
Creamer](http://jcreamerlive.com/ "Jonathan Creamer's Blog") that uses
[ASP.NET MVC 3 layouts for QUnit
tests](http://freshbrewedcode.com/jonathancreamer/2011/12/08/qunit-layout-for-javascript-testing-in-asp-net-mvc3/ "QUnit layout for JavaScript testing in ASP.NET MVC 3").
It’s a neat approach that consolidates all the QUnit boilerplate into a
single layout page. This allows you to have multiple test files and
duplicate that boilerplate.

But there was one thing that nagged me about it. For each new set of
tests, you need to add an action method and a corresponding view.
ASP.NET MVC does not allow rendering a view without a controller action.

Controller-Less Views
---------------------

The idea of controller-less views has been one tossed around by folks,
but there are all sorts of design issues that come up when you consider
it. For example, how do you request such a view directly? If you allow
that, what if the view is intended to be rendered by a controller
action. Now you have two ways to access that view, one of which is
probably incorrect. And so on.

However, there is another lesser known framework (at least, lesser known
to ASP.NET MVC developers) from the ASP.NET team that pretty much
provides this ability!

ASP.NET Web Pages with Razor Syntax
-----------------------------------

It’s a product called [ASP.NET Web
Pages](http://www.asp.net/web-pages "ASP.NET Web Pages website") that is
designed to appeal to developers who prefer an approach to web
development that’s more like PHP or classic ASP.

*Aside: I’d like to go on record and say I hated that name from the
beginning because it [causes so much
confusion](https://haacked.com/archive/2011/05/25/bin-deploying-asp-net-mvc-3.aspx "Bin Deploying MVC 3").
Isn’t everything I do in ASP.NET a web page?*

A Web Page in ASP.NET Web Pages (*see, confusing!*) uses Razor syntax
inline to render out the response to a request. ASP.NET Web Pages also
support layouts. This means we can create an approach very similar to
Jonathan’s, but we only need to add one file for each new set of tests.
Even better, this approach works for both ASP.NET MVC 3 and ASP.NET Web
Pages.

The Code
--------

The code to do this is straightforward. I just created a folder named
**test** which will contain all my unit tests. I added an
`_PageStart.cshtml` file to this directory that sets the layout for each
page. Note that this is equivalent to the `_ViewStart.cshtml` page in
ASP.NET MVCs.

```csharp
@{
    Layout = "_Layout.cshtml";
}
```

The next step is to write the layout file, `_Layout.cshtml`. This
contains the QUnit boilerplate along with a place holder (the
`RenderBody` call) for the actual tests.

```html
<!DOCTYPE html>

<html>
    <head>
        <title>@Page.Title</title>
        <link rel="stylesheet" href="/content/qunit.css " />
        <script src="/Scripts/jquery-1.7.1.min.js"></script>
        <script src="/scripts/qunit.js"></script>

        @RenderSection("Javascript", false)
        @* Tests are written in the body. *@
        @RenderBody()
    </head>
    <body>
        <h1 id="qunit-header">
          @(Page.Title ?? "QUnit tests")
        </h1>
        <h2 id="qunit-banner">
        </h2>
        <h2 id="qunit-userAgent"></h2>
        <ol id="qunit-tests">
        </ol>
        <p>
            <a href="/tests">Back to tests</a>
        </p>
    </body>
</html>
```

And now, one or more files that contain the actual test. Here’s an
example called `footest.cshtml`.

<pre><code>
@{
  Page.Title = "FooTests";
}
@if (false) {
  // OPTIONAL! QUnit script (here for intellisense)
  &lt;script src="/scripts/qunit.js"> </script>
}
<!-- Script we're testing -->
&lt;script src="/scripts/calculator.js"></script>

<!-- The tests -->
&lt;script>
  $(function () {
    // calculator_tests.js
    module("A group of tests get's a module");
    test("First set of tests", function () {
      var calc = new Calculator();
      ok(calc, "My caluculator is a O.K.");
      equals(calc.add(2, 2), 4, "shit broken");
    });
  });
&lt;/script>
</code></pre>

You’ll note that I have this funky if (false) block in the code. That’s
to workaround a current limitation in Razor so that JavaScript
Intellisense for QUnit works in this file. If you don’t care for
Intellisense, you don’t need it. I hope that in the future, Razor will
pick up the script in the layout and you won’t need this either way.

With this in place, to add a new test with the proper QUnit boilerplate
is very easy. Just add a .cshtml file, set the title for the tests, and
then add the script you’re testing and the test script into the same
file.

The last step is to create an index into all the tests. I wrote the
following `index.cshtml` file that creates a list of links for each set
of tests. It simply iterates through every test file and generates a
link. One nifty little perk of using ASP.NET Web Pages is you can leave
off the extension when you request the file.

```html
@using System.IO;
@{
  Layout = null;

  var files = from path in
  Directory.GetFiles(Server.MapPath("./"), "*.cshtml")
  let fileName = Path.GetFileNameWithoutExtension(path)
  where !fileName.StartsWith("_")
  && !fileName.Equals("index", StringComparison.OrdinalIgnoreCase)
  select fileName;
}

<!DOCTYPE html>
<html>
<head>
    <title></title>
</head>
<body>
    <div>
        <h1>QUnit tests</h1>
        <ul>
        @foreach (var file in files) {
            <li><a href="@file">@file</a></li>
        }
        </ul>
    </div>
</body>
</html>
```

The output of this page isn’t pretty, but it works. When I navigate to
/test I see a list of my test files:

[![qunit-tests](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/cfa3790769b8_117F0/qunit-tests_thumb.png "qunit-tests")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/cfa3790769b8_117F0/qunit-tests_2.png)

Here’s the contents of my test folder when I’m done with all this.

[![solution](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/cfa3790769b8_117F0/solution_thumb.png "solution")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/cfa3790769b8_117F0/solution_2.png)

Summary
-------

I personally haven’t used this approach yet, but I think it could be a
nice approach if you tend to have more than one QUnit test file in your
projects and you tend to customize the boilerplate for those tests.

I tend to just use a static HTML file, but so far, most of my QUnit
tests are for a single JavaScript library. But this approach might come
in handy when I get around to testing the JavaScript in the NuGet
gallery.

