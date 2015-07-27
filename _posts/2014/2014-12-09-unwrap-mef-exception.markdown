---
layout: post
title: "Unwrap MEF composition exceptions"
date: 2014-12-09 -0800
comments: true
categories: [csharp mef]
---

There are times when the Managed Extensibility Framework (aka MEF, the close relative of "Meh") cannot compose a part. In those cases it'll shrug (¯\\_(ツ)_/¯) and then take a dump on your runtime execution by throwing a  [`CompositionException`](http://msdn.microsoft.com/en-us/library/system.componentmodel.composition.compositionexception%28v=vs.110%29.aspx).

There are many reasons a composition will fail. There are two I run into the most often. The first is that I simply forgot to export a type. The `CompositionException` in this case is actually helpful.

But the other case is when an imported type throws an exception in its constructor. Here, the exception message is pretty useless. Here's an example taken from a real application, [GitHub for Windows](https://windows.github.com/). The actual exception is contrived for the purposes of demonstration. I changed the constructor of `CreateNewRepositoryViewModel` to throw an exception with the message "haha".

> System.ComponentModel.Composition.CompositionException: The composition produced a single composition error. The root cause is provided below. Review the CompositionException.Errors property for more detailed information.

> 1) haha

> Resulting in: An exception occurred while trying to create an instance of type 'GitHub.ViewModels.CreateNewRepositoryViewModel'.

> Resulting in: Cannot activate part 'GitHub.ViewModels.CreateNewRepositoryViewModel'.
Element: GitHub.ViewModels.CreateNewRepositoryViewModel -->  GitHub.ViewModels.CreateNewRepositoryViewModel -->  AssemblyCatalog (Assembly="GitHub, Version=2.7.0.2, Culture=neutral, PublicKeyToken=null")

> ...

It goes on and on. But note that the one piece of information I _really really_ want, the piece of information that would actually help me figure out what's going on, is not present. __What is the stack trace of the exception that caused this cascade of failures in the first place?!__

This exception stack trace is pretty useless. Note that you do see the root cause exception message "haha", but nothing else. You don't even know the exception type.

But don't worry, I wouldn't be writing this blog post if I didn't have an answer for you. It may not be a good answer, but it's something that seems to work for me. I wrote a method that unwraps the composition exception and tries to retrieve the actual original exception.

```csharp
/// <summary>
/// Attempts to retrieve the real cause of a composition failure.
/// </summary>
/// <remarks>
/// Sometimes a MEF composition fails because an exception occurs in the ctor of a type we're trying to
/// create. Unfortunately, the CompositionException doesn't make that easily available, so we don't get
/// that info in haystack. This method tries to find that exception as that's really the only one we care
/// about if it exists. If it can't find it, it returns the original composition exception.
/// </remarks>
/// <param name="exception"></param>
/// <returns></returns>
public static Exception UnwrapCompositionException(this Exception exception)
{
    var compositionException = exception as CompositionException;
    if (compositionException == null)
    {
        return exception;
    }

    var unwrapped = compositionException;
    while (unwrapped != null)
    {
        var firstError = unwrapped.Errors.FirstOrDefault();
        if (firstError == null)
        {
            break;
        }
        var currentException = firstError.Exception;

        if (currentException == null)
        {
            break;
        }

        var composablePartException = currentException as ComposablePartException;

        if (composablePartException != null
            && composablePartException.InnerException != null)
        {
            var innerCompositionException = composablePartException.InnerException as CompositionException;
            if (innerCompositionException == null)
            {
                return currentException.InnerException ?? exception;
            }
            currentException = innerCompositionException;
        }

        unwrapped = currentException as CompositionException;
    }

    return exception; // Fuck it, couldn't find the real deal. Throw the original.
}
```

What this method does is search through the `CompositionException` structure looking for an exception that is the root cause of the failure. Basically an exception that isn't a `CompositionException` nor `ComposablePartException`.

It seems to work fine for me, but I would love to have any MEF experts look at it and let me know if I'm missing anything. For example, I only look at the first error in each `CompositionException` because I've never seen more than one. But that could be an implementation detail.

Even if that strategy is incomplete, the code should be safe because if it can't find the root cause exception, it'll just return the original exception, so you're no worse off than before.

Here's an example of our log file with this code in place, emphasis mine.

> __System.InvalidOperationException: haha
   at GitHub.Api.ApiClient.Throw() in c:\dev\Windows\GitHub.Core\Api\ApiClient.cs:line 57__
   at GitHub.Api.ApiClient..ctor(HostAddress hostAddress, IObservableGitHubClient gitHubClient, Func`2 twoFactorChallengeHandler) in c:\dev\Windows\GitHub.Core\Api\ApiClient.cs:line 52
   at GitHub.Api.ApiClientFactory.Create(HostAddress hostAddress) in...

Now I can see the actual stack trace. In cases where an exception in the constructor is the cause, I really don't care about all the composition errors. This is what I really want to see. I hope you find this useful.
