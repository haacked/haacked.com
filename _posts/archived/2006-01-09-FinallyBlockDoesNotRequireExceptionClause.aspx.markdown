---
layout: post
title: 'Exception Handling Mistakes: Finally Block Does Not Require The Catch Block'
date: 2006-01-09 -0800
comments: true
disqus_identifier: 11449
categories:
- csharp
- code
redirect_from: "/archive/2006/01/08/FinallyBlockDoesNotRequireExceptionClause.aspx/"
---

While reviewing some code this weekend, I had the thought to do a search
for the following string throughout the codebase, "catch(Exception"
(using the regular expression search of course it looked more like
"catch\s*\(\s*Exception\s*\)".

My intent was to take a look to see how badly `Catch(Exception...)` was
being abused or if it was being used correctly. One interesting pattern
I noticed frequently was the following snippet...

```csharp
try
{
    fs = new FileStream(filename, FileMode.Create);

    //Do Something
}
catch(Exception ex)
{
    throw ex;
}
finally
{
    if(fs != null)
        fs.Close();
}
```

My guess is that the developer who wrote this didn’t realize that you
don’t need a catch block in order to use a finally block. The finally
block will ALWAYS fire whether or not there is an exception block. Also,
this code is resetting the callstack on the exception as I’ve [written
about before](https://haacked.com/archive/2005/11/17/DevSourceArticleOnExceptions.aspx).

This really just should be.

```csharp
try
{
    fs = new FileStream(filename, FileMode.Create);

    //Do Something
}
finally
{
    if(fs != null)
        fs.Close();
}
```

Another common mistake I found is demonstrated by the following code
snippet.

```csharp
try
{
    //Do Something.
}
catch(Exception e)
{
    throw e;
}
```

This is another example where the author of the code is losing stack
trace information. Even worse, there is no reason to even perform a try
catch, since all that the developer is doing is rethrowing the exact
exception being caught. I ended up removing the try/catch blocks
everywhere I found this pattern.
