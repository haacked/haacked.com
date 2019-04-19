---
title: Test Secure Class Instantiation Helper Method
date: 2007-09-21 -0800
tags: [code,tdd]
redirect_from: "/archive/2007/09/20/test-secure-class-instantiation-helper-method.aspx/"
---

This is a quick follow-up [to my last
post](https://haacked.com/archive/2007/09/21/unit-testing-security-example.aspx "Unit Testing Security Example").
That seemed like such a common test situation I figured I’d write a
quick generic method for encapsulating those two tests.

I’ll start with usage.

```csharp
[Test]
public void FileBrowserSecureCreationTests()
{
  AssertSecureCreation<FileBrowserConnector>(new string[] {"Admins"});
}
```

And here’s the method.

```csharp
/// <summary> 
/// Helper method. Makes sure you can create an instance  
/// of a type if you have the correct role.</summary> 
/// <typeparam name="T"></typeparam> 
/// <param name="allowedRoles"></param> 
public static void AssertSecureCreation<T>(string[] allowedRoles
  , params object[] constructorArguments)
{
  try   
  {     
    Activator.CreateInstance(typeof (T), constructorArguments);
    Assert.Fail("Was able to create the instance with no security.");
  }
  catch(TargetInvocationException e)
  {
    Assert.IsInstanceOfType(typeof(SecurityException)
      , e.InnerException
      , "Expected a security exception, got something else.");
  }

  MockRepository mocks = new MockRepository();

  IPrincipal principal;
  using (mocks.Record())
  {
    IIdentity identity = mocks.CreateMock<IIdentity>();
    SetupResult.For(identity.IsAuthenticated).Return(true);
    principal = mocks.CreateMock<IPrincipal>();
    SetupResult.For(principal.Identity).Return(identity);
    Array.ForEach(allowedRoles, delegate(string role) 
    {
      SetupResult.For(principal.IsInRole(role)).Return(true);
    });
  }

  using (mocks.Playback())
  {
    IPrincipal oldPrincipal = Thread.CurrentPrincipal;
    try
    {       
      Thread.CurrentPrincipal = principal;       
      Activator.CreateInstance(typeof(T), constructorArguments);
      //Test passes if no exception is thrown.
    }     
    finally
    {       
      Thread.CurrentPrincipal = oldPrincipal;     
    }   
  } 
}
```

There are definite improvements we can make, but this is a nice quick
way to test the basic permission level for a class.

