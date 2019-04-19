---
title: Unit Testing Security Example
date: 2007-09-21 -0800 9:00 AM
tags: [code,tdd]
redirect_from: "/archive/2007/09/20/unit-testing-security-example.aspx/"
---

This is a simple little demonstration of how to write unit tests to test
out a specific role based permission issue using
[NUnit](http://nunit.com/ "NUnit")/[MbUnit](http://mbunit.com/ "MbUnit")
and [Rhino
Mocks](http://ayende.com/projects/rhino-mocks.aspx "Rhino Mocks mocking framework").

In Subtext, we
have a class named `FileBrowserConnector` that really should only ever
be constructed by a member of the *Admins* role. Because this class can
write to the file system, we want to take extra precautions other than
simply restricting access to the URL in which this object is created.

Here are two tests I wrote to begin with.

```csharp
[Test]
[ExpectedException(typeof(SecurityException))]
public void NonAdminCannotCreateFileConnector()
{
  new FileBrowserConnector();
}

[Test]
public void AdminCanCreateFileConnector()
{
  MockRepository mocks = new MockRepository();

  IPrincipal principal;
  using (mocks.Record())
  {
    IIdentity identity = mocks.CreateMock<IIdentity>();
    SetupResult.For(identity.IsAuthenticated).Return(true);
    principal = mocks.CreateMock<IPrincipal>();
    SetupResult.For(principal.Identity).Return(identity);
    SetupResult.For(principal.IsInRole("Admins")).Return(true);
  }

  using (mocks.Playback())
  {
    IPrincipal oldPrincipal = Thread.CurrentPrincipal;
    try
    {
      Thread.CurrentPrincipal = principal;
      FileBrowserConnector connector = new FileBrowserConnector();
      Assert.IsNotNull(connector, "Could not create the connector.");
    }
    finally
    {
      Thread.CurrentPrincipal = oldPrincipal;
    }
  }
}
```

The first test is really straightforward. It simply tries to instantiate
the FileBrowserConnector class.

The second test is a bit more involved, but the concept is simple. I’m
using the Rhino Mocks mocking framework to dynamically construct
instance that implement the `IIdentity` and `IPrincipal` interfaces.

The following line...

`SetupResult.For(principal.IsInRole("Admins")).Return(true);`

Tells the dynamic principal mock to return *true* when the `IsInRole`
method is called with the parameter *"Admins"*. We then set the
`Thread.CurrentPrincipal` to this constructed principal and try and
create the instance of `FileBrowserConnector`.

Here’s the results of my first test run, trimmed down a bit.

    Found 2 tests
    [failure] FileBrowserConnectorTests.NonAdminCannotCreateFileConnector
    Exception of type 'MbUnit.Core.Exceptions.ExceptionNotThrownException' 
    was thrown. 

    [success] FileBrowserConnectorTests.AdminCanCreateFileConnector
    [reports] generating HTML report
    TestResults: file:///D:/AppData/MbUnit/Reports/UnitTests.Subtext.Tests.html

    1 passed, 1 failed, 0 skipped, took 4.37 seconds.

As expected, one test passed and one failed. Now I can go ahead and
enforce security on the `FileBrowserConnector` class.

```csharp
[PrincipalPermission(SecurityAction.Demand, Role = "Admins")]
public class FileBrowserConnector: Page
{
  //... implementation ...
}
```

That’s all there is to it. You might be wondering if this test is even
needed because all I’m really testing is that the `PrincipalPermission`
attribute does indeed work.

This test is still important to prevent regressions. You don’t want
someone coming along and removing that attribute by accident or out of
ignorance and you don’t notice it.

In codebases that I’ve worked with, I’ve seen a tendency to ignore or
forget to write test cases for security requirements. This demo
hopefully provides a starting point for myself and others to making sure
that security requirements get good test coverage.

I should probably write yet another test to make sure a principal in a
different role cannot create an instance of this class.

