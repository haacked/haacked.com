---
title: 'TIP: Decorate Custom Exception Classes With the Serializable Attribute'
date: 2004-02-24 -0800
tags: [tips]  
redirect_from: "/archive/2004/02/23/decorate-custom-exceptions-with-serializable-attribute.aspx/"
---

When writing a custom Exception class, don’t forget to mark the class as
Serializable? Why? If the exception is ever used in a remoting context,
exceptions on the server are serialized and remoted back to the client
proxy. The proxy then throws the exception on the client. By default,
.NET types are not serializable. But by adding a simple `Serializable`
attribute decoration on your class, .NET is able to serialize your
exception.

```csharp
[Serializable]
public class MyException : Exception
{
  // Custom Code
  
}
```

