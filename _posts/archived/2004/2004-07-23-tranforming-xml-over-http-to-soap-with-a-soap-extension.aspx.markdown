---
title: Tranforming Xml over Http to SOAP with a Soap Extension
tags: [web]
redirect_from: "/archive/2004/07/22/tranforming-xml-over-http-to-soap-with-a-soap-extension.aspx/"
---

![Soap](/assets/images/soap.jpg) In my last
[post](https://haacked.com/archive/2004/07/23/why-should-i-care-about-the-wire-format-in-soap.aspx/) I discussed a
client who requires that we build a web service using a proprietary XML
format (lets call it MyXML) so his mobile devices can interact with our
platform.

Naturally, I didn't want to limit ourselves to one client, but looked at
the big picture and decided I should build a standard Web Service using
SOAP, but provide some sort of facade that would translate his MyXML
requests to SOAP and translate the SOAP responses back to MyXML.

My first attempt was to write a Soap Extension. I was planning to do
something like this (some code ommitted):

```csharp
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Services.Protocols;

    /// <summary>
    /// Soap Extension that transforms incoming MyXml to 
    /// SOAP and outgoing SOAP to MyXml.
    /// 
    public class MyXmlToSoapExtension : SoapExtension
    {
        Stream _soapStream;
        Stream _tempStream;

        /// <summary>
        /// Transforms incoming MyXml to SOAP and outgoing SOAP to 
        /// MyXml
        /// 
        /// <param name="message">
        public override void ProcessMessage(SoapMessage message)
        {
            switch (message.Stage)
            {
                case SoapMessageStage.BeforeDeserialize:
                {
                    // Code to transform incoming _soapStream
                    // into the chained _tempStream via XSLT.
                }
                break;

                case SoapMessageStage.AfterSerialize:
                {
                    // Code to transform chained 
                    // _tempStream and write result to 
                    // the outgoing _soapStream via XSLT
                }
                break;
            }
        }

        /// <summary>
        /// When overridden in a derived class, allows a 
        /// SOAP extension access to the memory buffer 
        /// containing the SOAP request or response.
        /// 
        /// <param name="stream">
        /// <returns>
        public override Stream ChainStream(Stream stream)
        {
            // by overriding ChainStream we can
            // cause the ASP.NET system to use
            // our stream for buffering SOAP messages
            // rather than the default stream.
            // we will store off the original stream
            // so we can pass the data back down to the 
            // ASP.NET system in original stream that 
            // it created.
            _soapStream = stream;
            _tempStream = new MemoryStream();
            return _tempStream;
        }
    }
```

And man, it was working like a charm in my unit tests. I was converting
straight up garbage into SOAP. The beauty of this scheme was that SOAP
requests and MyXML requests were happily going to the exact same URL.
Everybody was getting along. All I had to do was examine the request. If
it was a SOAP request, I didn't change anything. If it was a MyXML
request, I ran my transformations. For a moment, I was daydreaming about
the articles I would write about how brilliant a solution I had created
(not realizing there were other problems as well such as maintaining the
transformations between MyXML and SOAP) until I noticed that my unit
test was cheating a bit. When making the HTTP request, the test did the
following sneaky thing:

```csharp
    HttpWebRequest request 
        = (HttpWebRequest)HttpWebRequest.Create("http://localhost/Svc.asmx");
    //...Code Omitted...
    request.Headers.Add("SOAPAction", "http://mynamespace/MethodName"); 
```

You see, a SOAP request is more than just the contents of the SOAP
envelope (especially when using doc/literal/bare), there's also crucial
information in the HTTP headers. So I removed that line in my test, and
tried to add that line within my Soap Extension like so:

    HttpRequest request = HttpContext.Current.Request;
    request.Headers.Add("SOAPAction", "http://mynamespace/MethodName"); 

Not going to happen, my tests failed. By the time the HTTP headers reach
my web server, they are READ ONLY. They won't let me get my grubby hands
in there and change them. I might be able to convince my client to add
this header to his clients for kicks, but I don't think he'd go for it.
Why would he? He doesn't want anything to do with SOAP.

Now, unless someone comes along and shows me how to modify incoming HTTP
headers from an ASP.NET service, I am going to resort to plan B. I will
write an HttpHandler that takes in the MyXML, does the authentication
etc..., figures out which method to call, and then call the appropriate
Web Service method. I've put the code that implements my web service in
another assembly like so:

```csharp
<%@ WebService Language="c#" Class="Svc,MyAssembly" %>
```

That way my HttpHandler doesn't have to make a second HTTP request to
the Web Service, but just use the underlying logic (assuming my methods
don't access such things as the SoapContext etc...). I was hoping to
avoid this type of duplication of efforts, but oh well.

UPDATE: As my friend Ben points out, I can modify the HTTP headers with
an ISAPI filter, but that's a lot more work and I prefer to work within
the ASP.NET model and not have to deal with ISAPI.
