---
layout: post
title: "Simulating Http Context For Unit Tests Without Using Cassini nor IIS"
date: 2005-06-11 -0800
comments: true
disqus_identifier: 4617
categories: []
---
UPDATE: I have recently posted a [newer and better version of this code](http://haacked.com/archive/2007/06/19/unit-tests-web-code-without-a-web-server-using-httpsimulator.aspx "HttpSimulator")
on my blog.

As I’ve [stated before](http://haacked.com/archive/2004/11/30/1687.aspx), I’m a big fan of completely self-contained unit tests. When unit testing ASP.NET pages, base classes, controls and such, I use the technique [outlined by Scott Hanselman in this post](http://www.hanselman.com/blog/PermaLink.aspx?guid=944a5284-6b8d-4366-81e8-2e241401e1b3).
In fact, several of the unit tests for [RSS Bandit](http://www.rssbandit.org/) use this technique in order to test RSS auto-discovery and similar features.

However, there are cases when I want a “lightweight” quick and dirty way to test library code that will be used by an ASP.NET project. For example, take a look at this very contrived example that follows a common pattern...

``` csharp
/// <summary>
/// Obtains some very important information.
/// </summary>
public SomeInfo GetSomeInfo()
{
  SomeInfo info = HttpContext.Current.Items["CacheKey"] as SomeInfo;
  if(info == null)
  {
    info = new SomeInfo();
    HttpContext.Current.Items["CacheKey"] = info;
  }
  return info;
}
```

The main purpose of this method is to get some information in the form of the `SomeInfo` class. Normally, this would be very straightforward to
unit test except for one little problem. This method has a side-effect. Apparently, it’ll cost you to obtain this information, so the method
checks the Context’s Items dictionary (which serves as the current request’s cache) first before paying the cost to create the `SomeInfo`
instance. Afterwards it places that instance in the `Items` dictionary.

If I try and test this method in NUnit, I’ll run into a `NullReferenceException` when attempting to access the static `Current` property of `HttpContext`.

One option around this is to factor the logic between the caching into its own method and test that. But in this case, I want to test that the
caching works and doesn’t cause any unintended consequences.

Another option is to fire up Cassini in my unit test, create a website that uses this code, and test the method that way, but that’s a “heavy”
(and potentially very indirect) way to test this method.

As I stated before, I wanted a “lightweight” means to test this method. There wouldn’t be a problem if `HttpContext.Current` was a valid
instance of `HttpContext`. Luckily, the static `Current` property of `HttpContext` is both readable and writeable. All it takes is to set
that property to a properly created instance of `HttpContext`. However, creating that instance wasn’t as straightforward as my first attempt.
I’ll spare you the boring details and just show you what I ended up with.

I wrote the following static method in my `UnitTestHelper` class. All the write statements to the console shows the values for commonly
accessed properties of the `HttpContext` Note that this method could be made more general for your use. This is the version within Subtext.

```csharp
/// <summary>
/// Sets the HTTP context with a valid simulated request
/// </summary>
/// <param name="host">Host.</param>
/// <param name="application">Application.</param>
public static void SetHttpContextWithSimulatedRequest(string host,
string application)
{
  string appVirtualDir = "/";
  string appPhysicalDir = @"c:\\projects\\SubtextSystem\\Subtext.Web\\";
  string page = application.Replace("/", string.Empty) + "/default.aspx";
  string query = string.Empty;
  TextWriter output = null;

  SimulatedHttpRequest workerRequest = new SimulatedHttpRequest(appVirtualDir, appPhysicalDir, page, query, output, host);
  HttpContext.Current = new HttpContext(workerRequest);
  
  Console.WriteLine("Request.FilePath: " + HttpContext.Current.Request.FilePath);
  Console.WriteLine("Request.Path: " + HttpContext.Current.Request.Path);

  Console.WriteLine("Request.RawUrl: " + HttpContext.Current.Request.RawUrl);

  Console.WriteLine("Request.Url: " + HttpContext.Current.Request.Url);

  Console.WriteLine("Request.ApplicationPath: " + HttpContext.Current.Request.ApplicationPath);

  Console.WriteLine("Request.PhysicalPath: " + HttpContext.Current.Request.PhysicalPath);
}
```

You’ll notice this code makes use of a class named `SimulatedHttpRequest`. This is a class that inherits from
`SimpleWorkRequest` which itself inherits from `HttpWorkerRequest`. Using [Reflector](http://www.aisto.com/roeder/dotnet/), I spent a bit of
time looking at how the HttpContext class implements certain properties. This allows me to tweak the `SimulatedHttpRequest` to mock up the type
of request I want. The code for this class is...

```csharp
/// <summary>
/// Used to simulate an HttpRequest.
/// </summary>
public class SimulatedHttpRequest : SimpleWorkerRequest
{
    string _host;

    /// <summary>
    /// Creates a new <see cref="SimulatedHttpRequest"/> instance.
    /// </summary>
    /// <param name="appVirtualDir">App virtual dir.</param>
    /// <param name="appPhysicalDir">App physical dir.</param>
    /// <param name="page">Page.</param>
    /// <param name="query">Query.</param>
    /// <param name="output">Output.</param>
    /// <param name="host">Host.</param>
    public SimulatedHttpRequest(string appVirtualDir, string
appPhysicalDir, string page, string query, TextWriter output, string
host) : base(appVirtualDir, appPhysicalDir, page, query, output)
    {
        if(host == null || host.Length == 0)
            throw new ArgumentNullException("host", "Host cannot be null
nor empty.");
        _host = host;
    }

    /// <summary>
    /// Gets the name of the server.
    /// </summary>
    /// <returns></returns>
    public override string GetServerName()
    {
        return _host;
    }

    /// <summary>
    /// Maps the path to a filesystem path.
    /// </summary>
    /// <param name="virtualPath">Virtual path.</param>
    /// <returns></returns>
    public override string MapPath(string virtualPath)
    {
        return Path.Combine(this.GetAppPath(), virtualPath);
    }
}
```

Within the `SetUp` method of my `TestFixture`, I call this method like so...

```csharp
[SetUp]
public void SetUp()
{
    _hostName = UnitTestHelper.GenerateUniqueHost();

    UnitTestHelper.SetHttpContextWithBlogRequest(_hostName, "MyBlog");
}
```

Unfortunately, this so called “lightweight” approach has its limits. Any call in your code to `HttpContext.Currert.Request.MapPath` will throw an
exception. I tried working around this, but it looks like I’m at an impasse. The MapPath method makes use of the
`HttpRuntime.AppDomainAppPath` property. Unfortunately, I cannot simulate the HttpRuntime in a lightweight manner. There is a way to run
the code being tested within an HttpRuntime, but that, of course, is the heavyweight [Cassini method](http://www.hanselman.com/blog/PermaLink.aspx?guid=944a5284-6b8d-4366-81e8-2e241401e1b3)
mentioned above.
