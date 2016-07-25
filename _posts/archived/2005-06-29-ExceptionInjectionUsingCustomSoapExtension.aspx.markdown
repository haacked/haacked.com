---
layout: post
title: Exception Injection Using a Custom SOAP Extension
date: 2005-06-29 -0800
comments: true
disqus_identifier: 7392
categories: []
redirect_from: "/archive/2005/06/28/ExceptionInjectionUsingCustomSoapExtension.aspx/"
---

You kind of get the feeling that [Keith
Brown](http://pluralsight.com/blogs/keith/) has a beef with soap
exceptions when he writes that [SoapException
Sucks](http://pluralsight.com/blogs/keith/archive/2005/06/02/9712.aspx).
I won’t rehash everything he says here, but the gist of his complaint is
that when throwing an exception from within a web service, the exception
gets wrapped by a `SoapException`. What’s so bad about that? As Keith
relates, the `Message` property of the `SoapException` class
intersperses your fault string with a load of other crap you really
don’t care about. Also, the `InnerException` doesn’t get serialized into
the SOAP fault packet, so it is always null on the client side.

A couple solutions proposed within his comments require putting a
`try/catch` around the body of every method and construct a suitable
`SoapException` by hand. This just didn’t sit well with me (neither did
the burrito I just ate) as it seemed quite repetitive. I figured there
had to be a better way. If only there were some way to inject code after
a SOAP method is called and before the XML payload is delivered to the
client. Fortunately there is. SOAP Extensions!

The solution I hacked together here is to build a custom
`SoapExtensionAttribute` used to mark up a method. If that method throws
an exception, the original exception information is serialized into the
detail element of the soap exception.

The key here is to remember that SOAP is at its core simply XML text
messages being sent back and forth between computers. A `SoapExtension`
lets you peek under the hood and manipulate the actual messages going in
and out.

There are three classes involved in this solution,
`SerializedExceptionExtensionAttribute`, `SerializedExceptionExtension`
and `SoapOriginalException`. I’ll briefly go over each one.

`SerializedExceptionExtensionAttribute` is a very simple `Attribute`
class that inherits from `SoapExtensionAttribute`. When applied to a
target, this attribute has a property that indicates what type of
`SoapExtension` to use for that target.

`SerializedExceptionExtension` inherits from `SoapExtension` and for the
most part looks like your typical MSDN example of a soap extension in
which you override `ChainStream`, store the old stream in a member
variable, and replace it with a new stream. For the sake of
illustration, I will highlight a few methods that make this extension
somewhat interesting (at least for me)...

```csharp
public override void ProcessMessage(SoapMessage message)
{
    if(message.Stage == SoapMessageStage.AfterSerialize)
    {
        _newStream.Position = 0;
        if(message.Exception != null && message.Exception.InnerException != null)
        {
            InsertDetailIntoOldStream(message.Exception.InnerException);
        }
        else
        {
            CopyStream(_newStream, _oldStream);   
        }
    }
    else if(message.Stage == SoapMessageStage.BeforeDeserialize)
    {
        CopyStream(_oldStream, _newStream);
        _newStream.Position = 0;
    }
}

void InsertDetailIntoOldStream(Exception exception)
{
    XmlDocument doc = new XmlDocument();
    doc.Load(_newStream);
    XmlNode detailNode = doc.SelectSingleNode("//detail");

    try
    {
        detailNode.InnerXml = GetXmlExceptionInformation(exception);
    }
    catch(Exception exc)
    {
        detailNode.InnerXml = exc.Message;
    }

    XmlWriter writer = new XmlTextWriter(_oldStream, Encoding.UTF8);
    doc.WriteTo(writer);
    writer.Flush();
}
```

In the method `ProcessMessage`, you can see that the code waits till
after the method has been serialized to XML (represented by
`SoapMessageStage.AfterSerialize`) and is ready to be sent back to the
client. That’s where the exception detail is injected into the stream,
assuming an exception did occur.

`InsertDetailIntoOldStream` finds the detail node within the serialized
stream and inserts exception information into that node. In order to
make this information useful to both non .NET clients and .NET clients,
the exception information is formatted as XML. However, in one of the
nodes of that XML, I serialize the exception using a `BinaryFormatter`.
That way, a .NET client can gain access to the full original exception.

```csharp
string GetXmlExceptionInformation(Exception exception)
{
    string format = "<Message>{0}</Message>"
        + "<Type>{1}</Type>"
        + "<StackTrace>{2}</StackTrace>"
        + "<Serialized>{3}</Serialized>";
    return string.Format(
        format,
        exception.Message,
        exception.GetType().FullName,
        exception.StackTrace,
        SerializeException(exception));
}

string SerializeException(Exception exception)
{
    MemoryStream stream = new MemoryStream();
    IFormatter formatter = new  BinaryFormatter();
    formatter.Serialize(stream, exception);
    stream.Position = 0;
    return Convert.ToBase64String(stream.ToArray());
}
```

**Security Note!**, for a production system, you probably don’t want to
serialize the original exception as it will contain a stack trace and
could give out more information than you wish clients to the service to
have. For debugging, however, this is quite useful.

Allow me to walk through how you can apply these classes in your own
code. Below, I’ve written a method that simply throws an exception. You
can see that I marked it with the `SerializedExceptionExtension`
attribute.

```csharp
[WebMethod, SerializedExceptionExtension]
public string ThrowNormalException()
{
    throw new ArgumentNullException("MyParameter", "Exception thrown for testing purposes");
}
```

Now on the client, I simply make a call to the web service within a
try/catch clause. In the snippet below, you’ll notice that I wrap the
thrown exception with the `SoapOriginalException` class. That class is a
helpful wrapper that knows how to deserialize an exception serialized
using this technique. The original exception is accessed via the
`InnerException` property.

```csharp
void CallService()
{
    TestService proxy = new TestService();
    try
    {
        proxy.ThrowNormalException();
    }
    catch(SoapException e)
    {
        SoapOriginalException realException = new SoapOriginalException(e);
        Console.WriteLine(realException.InnerException.Message);
    }
}
```

If you’d like to try this technique out yourself and provide a critique,
download the
[ExceptionInjectionWithSoapExtension.zip](http://haacked.com/code/ExceptionInjectionWithSoapExtension.zip)
source files here. There are certainly some enhancements that could be
made to the code to make it even more useful. Let me know if you make
improvements.
