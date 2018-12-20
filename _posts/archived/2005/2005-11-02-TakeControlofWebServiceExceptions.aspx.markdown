---
title: Take Control of Web Service Exceptions
date: 2005-11-02 -0800
disqus_identifier: 11084
tags: []
redirect_from: "/archive/2005/11/01/TakeControlofWebServiceExceptions.aspx/"
---

[Craig Andera posts a
technique](http://pluralsight.com/blogs/craig/archive/2005/11/02/16155.aspx?Pending=true)
for handling exceptions thrown by a webservice. He takes the approach of
adding a try catch block to each method.

A while ago I tackled the same problem, but I was unhappy with the idea
of wrapping every inner method call with a try catch block. I figured
there had to be a better way. Since SOAP is simply XML being sent over a
wire, I figured there had to be a way for me to hook into the pipeline
rather than modify my code.

What I came up with is my [Exception Injection Technique Using a Custom
Soap
Extension](https://haacked.com/archive/2005/06/29/ExceptionInjectionUsingCustomSoapExtension.aspx).
This allows you to simply add an additional attribute to each web method
as in the sample below and have full control over how exceptions are
handled and sent over the wire.

[WebMethod, SerializedExceptionExtension]

public string ThrowNormalException()

{

    throw new ArgumentNullException("MyParameter", \
        "Exception thrown for testing purposes");

}

[Read about the technique
here](https://haacked.com/archive/2005/06/29/ExceptionInjectionUsingCustomSoapExtension.aspx)
and feel free to adapt it to your purposes.

