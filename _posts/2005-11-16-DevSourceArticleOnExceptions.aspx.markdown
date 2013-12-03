---
layout: post
title: "DevSource Article on Exceptions"
date: 2005-11-16 -0800
comments: true
disqus_identifier: 11207
categories: []
---
I am now a published DevSource article author. :)

Well actually, [Bob Reselman (aka Mr. Coding
Slave)](http://codingslave.blogspot.com/) did nearly all of the writing.
I merely provided technical editing and proofing along with clarifying
some sections. In return, he graciously gave me a byline.

[The article is a beginners guide to
exceptions](http://www.devsource.com/article2/0,1895,1888558,00.asp).NET.
and Bob did a bang-up job especially given the
[circumstances](http://codingslave.blogspot.com/2005/10/q-how-do-you-know-that-you-are-getting.html#comments).
He writes in a very approachable manner.

Hopefully, we can follow-up with a more in-depth version to take
beginners to the next level in understanding best practices. One thing I
wish we had discussed in the article is the guidelines around
re-throwing exceptions. For example, I’ve seen many beginning developers
make the following mistake...

public void SomeMethod()

{

    try

    {

        SomeOtherMethod(null);

    }

    catch(ArgumentNullException e)

    {

        //Code here to do something

 

        throw e; //Bad!

    }

The problem with this approach is the code in the catch clause is
throwing the exception it caught. The runtime generates a stack trace
from the point at which an exception is thrown. In this case, the stack
trace will show the stack starting from the line `throw e` and not the
line of code where the exception actually occurred.

If this is confusing, consider that the runtime doesn’t exactly
distinguish between these three cases...

Exception exc = new Exception();

throw exc;

\

throw new Exception();

\

throw SomeExceptionBuilderFunction();

If you really intend to rethrow an exception without wrapping it in
another exception, then the proper syntax is to use the `throw` keyword
without specifying an exception. The original exception will propagate
up with its stack trace intact.

public void SomeMethod()

{

    try

    {

        SomeOtherMethod(null);

    }

    catch(ArgumentNullException e)

    {

        //Code here to do something

 

        throw; //Better!

    }

However, even this can be improved on depending on why we are catching
the exception in the first place. If we are performing cleanup code in
the catch clause, it would be better to move that code to the finally
block and not even catch the exception in the first place.

Also, using the `throw` syntax as illustrated above can affect the
debuggability of a system. Christopher Blumme points this out in his
annotation in the book [Framework Design
Guidelines](http://www.amazon.com/gp/product/0321246756/103-9411210-6787060?v=glance&n=283155&v=glance)
(highly recommended) where he notes that attaching a debugger works by
detecting exceptions that are left unhandled. If there is a series of
catch and throw segments up the stack, the debugger might only attach to
the last segment, far away from the actual point at which the exception
occurred.

public void SomeMethod()

{

    try

    {

        SomeOtherMethod(null);

    }

    finally

    {

        CleanUp(); //Possibly even better

    }

}

Bob and I plan to follow-up with hopefully more articles covering
exceptions. This is just a start.

