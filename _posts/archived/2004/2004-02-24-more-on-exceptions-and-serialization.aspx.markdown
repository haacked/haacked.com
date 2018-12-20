---
title: 'TIP: More on Exceptions and Serialization'
date: 2004-02-24 -0800
disqus_identifier: 208
tags: []
redirect_from: "/archive/2004/02/23/more-on-exceptions-and-serialization.aspx/"
---

I just left to do some thinking where I get my best thinking done. While
washing my hands, I realized I should mention a few issues with my
[previous
recommendation](https://haacked.com/archive/2004/02/24/decorate-custom-exceptions-with-serializable-attribute.aspx "Decorate custom exceptions with serializable attribute").

Adding the `SerializableAttribute` to a class indicates to .NET that the
class may be automatically serialized via reflection. When the class is
being serialized, .NET uses reflection to obtain the values of every
private, protected, and public member. What this means for your
exception class, is that any properties it exposes should themselves be
serializable. Should .NET attempt to serialize your class, and your
class contains a member that cannot be serialized, it will throw a
`SerializationException` during run-time

Now, I’m not sure if this is the best design, but I often *expand* any
object parameters to my custom exception constructors rather than
storing a reference to the object. For example:

```csharp
[Serializable]
public class MyException : ApplicationException
{
  public readonly int ObjectID;
  public readonly string ObjectName;
    
  /// <summary>   
  /// Constructor stores the properties of sourceObject instead   
  /// of a reference to sourceObject itself.   
  /// </summary>   
  public MyException(string message, MyObject sourceObject)
  {
    ObjectID = sourceObject.ID;
    ObjectName = sourceObject.Name;
  }
}
```

This allows me not to have to worry about whether or not `MyObject` is
serializable.

Another way to deal with this is to mark any members that are not
serializable with the `NonSerializedAttribute` like so:

```csharp
 
```

```csharp
[Serializable]
public class MyException : ApplicationException
{
  [NonSerialized]
  private MyObject sourceObject;
    
  /// <summary>   
  /// Constructor stores the properties of sourceObject instead   
  /// of a reference to sourceObject itself.   
  /// </summary>   public MyException(string message, MyObject sourceObject)
  {
    this.sourceObject = sourceObject;
  }
}
```

The .NET runtime will ignore any members with the `NonSerialized`
attribute during serialization. After deserialization, the member will
have its default value (null for reference types).

Finally, you can forego automatic serialization and provide your own
serialization by having your class implement `ISerializable` If you do
so, you must still mark your class as serializable with the
`SerializableAttribute`

```csharp
 [Serializable]
public class MyException : ApplicationException
{
  [NonSerialized]
  private MyObject sourceObject;
    
  /// <summary>   
  /// Constructor stores the properties of sourceObject instead   
  /// of a reference to sourceObject itself.   
  /// </summary>   public MyException(string message, MyObject sourceObject)
  {
    this.sourceObject = sourceObject;
  }
}
```

What do you think?

