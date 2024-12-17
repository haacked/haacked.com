---
title: "Deserializing JSON to a string or a value"
description: "You may want to deserialize JSON to strongly typed values, but sometimes you run into a situation where the API doesn't comply, until now."
tags: [csharp json]
excerpt_image: https://github.com/user-attachments/assets/15a13230-644f-4e5e-827e-c4c35051c77e...
---

I love using [Refit](https://github.com/reactiveui/refit) to call web APIs in a nice type-safe manner. Sometimes though, APIs don't want to cooperate with your strongly-typed hopes. For example, you might run into an API written by a hipster in a beanie, aka a dynamic-type enthusiast. I don't say that pejoratively. Some of my closest friends write Python and Ruby.

For example, I came across an API that returned a value like this:

```json
{
  "important": true
}
```

No problem, I defined a class like this to deserialize it to:

```csharp
public class ImportantResponse
{
    public bool Important { get; set; }
}
```

And life was good. Until that awful day that the API returned this:

```json
{
  "important": "What is important is subjective to the viewer."
}
```

Damn! This philosophy lesson broke my client. One workaround is to do this:

```csharp
public class ImportantResponse
{
    public JsonElement Important { get; set; }
}
```

It works, but it's not great. It doesn't communicate to the consumer that this value can only be a `string` or a `bool`. That's when I remembered an old blog post from my past.

![A ball of string on the left, "or" in the middle, a present on the right](https://github.com/user-attachments/assets/15a13230-644f-4e5e-827e-c4c35051c77e "Is it one or is it the other?")

## April Fool's Joke to the Rescue

When I was the Program Manager (PM) for ASP.NET MVC, my colleague and lead developer, Eilon, wrote a blog post entitled ["The String or the Cat: A New .NET Framework Library](https://asp-blogs.azurewebsites.net/leftslipper/the-string-or-the-cat-a-new-net-framework-library) where he introduced the class `StringOr<TOther>`. This class could represent a dual-state value that's either a string or another type.

> The concepts presented here are based on a thought experiment proposed by scientist Erwin Schrödinger. While an understanding of quantum physics will help to understand the new types and APIs, it is not required.

It turned out his blog post was an April Fool's joke. But the idea stuck with me. And now, here's a case where I need a real implementation of it. But I'm going to name mine, `StringOrValue<T>`.

## A modern StringOrValue&lt;T&gt;

One nice thing about implementing this today is we can leverage modern C# features. Here's the starting implementation:

```csharp
[JsonConverter(typeof(StringOrValueConverter))]
public readonly struct StringOrValue<T> : IStringOrObject {
    public StringOrValue(string stringValue) {
        StringValue = stringValue;
        IsString = true;
    }

    public StringOrValue(T value) {
        Value = value;
        IsValue = true;
    }

    public T? Value { get; }
    public string? StringValue { get; }

    [MemberNotNullWhen(true, nameof(StringValue))]
    public bool IsString { get; }

    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsValue { get; }
}

/// <summary>
/// Internal interface for <see cref="StringOrValue{T}"/>.
/// </summary>
/// <remarks>
/// This is here to make serialization and deserialization easy.
/// </remarks>
[JsonConverter(typeof(StringOrValueConverter))]
internal interface IStringOrObject
{
    bool IsString { get; }

    bool IsValue { get; }

    string? StringValue { get; }

    object? ObjectValue { get; }
}
```

We can use the `MemberNotNullWhen` attribute to tell the compiler that when `IsString` is true, `StringValue` is not null. And when `IsValue` is true, `Value` is not null. That way, code like this compiles just fine without raising null warnings:

````csharp
var value = new StringOrValue<string>("Hello");
if (value.IsString) {
    Console.WriteLine(value.StringValue.Length);
}
````

and

```csharp
var value = new StringOrValue<SomeType>(42);
if (value.IsValue) {
    Console.WriteLine(value.ToString());
}
```

It also is decorated with the `JsonConverter` attribute to tell the JSON serializer to use the `StringOrValueConverter` class to serialize and deserialize this type. I wanted this type to Just Work™. I didn't want consumers of this class have to bother with registering a `JsonConverterFactory` for this type.

This also explains why I introduced the internal `IStringOrObject` interface. We can't implement the `JsonConverter` attribute on a open generic type, so we need a non-generic interface to apply the attribute to. It also makes it easier to write the converter as you'll see.

```csharp
/// <summary>
/// Value converter for <see cref="StringOrValue{T}"/>.
/// </summary>
internal class StringOrValueConverter : JsonConverter<IStringOrObject>
{
    public override bool CanConvert(Type typeToConvert)
        => typeToConvert.IsGenericType
           && typeToConvert.GetGenericTypeDefinition() == typeof(StringOrValue<>);

    public override IStringOrObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var targetType = typeToConvert.GetGenericArguments()[0];

        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            return stringValue is null
                ? CreateEmptyInstance(targetType)
                : CreateStringInstance(targetType, stringValue);
        }

        var value = JsonSerializer.Deserialize(ref reader, targetType, options);

        return value is null
            ? CreateEmptyInstance(targetType)
            : CreateValueInstance(targetType, value);
    }

    static ConstructorInfo GetEmptyConstructor(Type targetType)
    {
        return typeof(StringOrValue<>)
                   .MakeGenericType(targetType).
                   GetConstructor([])
               ?? throw new InvalidOperationException($"No constructor found for StringOrValue<{targetType.Name}>.");
    }

    static ConstructorInfo GetConstructor(Type targetType, Type argumentType)
    {
        return typeof(StringOrValue<>)
            .MakeGenericType(targetType).
            GetConstructor([argumentType])
            ?? throw new InvalidOperationException($"No constructor found for StringOrValue<{targetType.Name}>.");
    }

    static IStringOrObject CreateEmptyInstance(Type targetType)
    {
        var ctor = GetEmptyConstructor(targetType);
        return (IStringOrObject)ctor.Invoke([]);
    }

    static IStringOrObject CreateStringInstance(Type targetType, string value)
    {
        var ctor = GetConstructor(targetType, typeof(string));
        return (IStringOrObject)ctor.Invoke([value]);
    }

    static IStringOrObject CreateValueInstance(Type targetType, object value)
    {
        var ctor = GetConstructor(targetType, targetType);
        return (IStringOrObject)ctor.Invoke([value]);
    }

    public override void Write(Utf8JsonWriter writer, IStringOrObject value, JsonSerializerOptions options)
    {
        if (value.IsString)
        {
            writer.WriteStringValue(value.StringValue);
        }
        else if (value.IsValue)
        {
            JsonSerializer.Serialize(writer, value.ObjectValue, options);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
```

In the actual implementation of `StringOrValue<T>`, I implemented IEquatable&lt;T&gt;, IEquatable&lt;StringOrValue&lt;T&gt;&gt; and overrode the implicit operators:

```csharp
public static implicit operator StringOrValue<T>(string stringValue) => new(stringValue);
public static implicit operator StringOrValue<T>(T value) => new(value);
```

This allows you to write code like this:

```csharp
StringOrValue<int> valueAsString = "Hello";
StringOrValue<int> valueAsNumber = 42;

Assert.Equals("Hello", valueAsString);
Assert.Equals(42, valueAsNumber);
```

So with this implementation in place, I can go back to the original example and write this:

```csharp
public class ImportantResponse
{
    public StringOrValue<bool> Important { get; set; }
}
```

And now I can handle both cases:

```csharp
var response = JsonSerializer.Deserialize<ImportantResponse>(json)
    ?? throw new InvalidOperationException("Deserialization failed.");

if (response.Important.IsValue) {
    if (response.Important.Value) {
        Console.WriteLine("It's important!");
    }
    else {
        Console.WriteLine("It's not important.");
    }
}
else {
    Console.WriteLine(response.Important.StringValue);
}
```

It's time to go shopping for a beanie!

Here's [the full implementation](https://gist.github.com/haacked/2fd1f8f0818c27184f2d08704f6f06f6) for those interested in using this in your own projects!

<script src="https://gist.github.com/haacked/2fd1f8f0818c27184f2d08704f6f06f6.js"></script>
