---
title: "C# List Pattern Examples"
description: "C# 11 introduces some list patterns and I'm loving them."
tags: [csharp]
excerpt_image: https://user-images.githubusercontent.com/19977/203370937-0793aeb9-4d2a-444e-bc44-dd81124105f7.png
---

We recently upgraded [Abbot](https://ab.bot/) to .NET 7 and C# 11 and I'm just loving the new language features in C#. In this post, I'll give a couple examples of list patterns.

![A shopping list in front of a treasure hoard](https://user-images.githubusercontent.com/19977/203370937-0793aeb9-4d2a-444e-bc44-dd81124105f7.png "Ancient scroll with my shopping list")

## Single Item List

There are cases where I expect up to one item in a list. Any more and I want to throw an exception. Here's one way you can deal with it:

__BEFORE__

```csharp
List<int> list = SomeMethodThatReturnsAList(someId);

var formattedItem = list.SingleOrDefault() is {} singleItem
    ? $"Formatted: {singleItem}"
    : "No items found";
```

Note that if there's no items, nothing happens. That's fine in this case. If there's two or more items, it throws an exception, but not exactly the most helpful one. Here's how I might handle this with a list pattern.

__AFTER__

```csharp
List<int> list = SomeMethodThatReturnsAList(someId);

var formattedItem = list switch {
    [] => "No items found",
    [var singleItem] => $"Formatted: {singleItem}",
    _ => throw new InvalidOperationException($"Expected 0 or 1 items, but got {list.Count} items for Id: {someId}.")
};
```

This is a bit more verbose, but it's very clear that I'm handling every possible case I care about for the list, rather than relying on the behavior of `SingleOrDefault()`. Not only that, the exception I get is much more helpful.

## Multiple Items

Here's something that comes up a lot in my code. I have a formatted string I want to split into parts. Suppose it's fine to have two or three parts, but no less and no more.

__BEFORE__

```csharp
public record Parts(string Part1, string Part2, string? Part3 = null) {
    public static Parts Parse(string formatted) {
        var parts = formatted.Split('|');
        return parts.Length switch {
            2 => new Parts(parts[0], parts[1]),
            3 => new Parts(parts[0], parts[1], parts[2]),
            var length => throw new InvalidOperationException($"Expected 3 parts, but got {length} parts for formatted string: {formatted}."),
        };
    }

    public override string ToString() => Part3 is null
        ? $"{Part1}|{Part2}"
        : $"{Part1}|{Part2}|{Part3}";
}
```

__AFTER__

```csharp
public record Parts(string Part1, string Part2, string? Part3 = null) {
    public static Parts Parse(string formatted) {
        var parts = formatted.Split('|');
        return formatted.Split('|') switch {
            [var part1, var part2] => new Parts(part1, part2),
            [var part1, var part2, var part3] => new Parts(part1, part2, part3),
            _ => throw new InvalidOperationException($"Expected 3 parts, but got {parts.Length} parts for formatted string: {formatted}."),
        };
    }

    public override string ToString() => Part3 is null
        ? $"{Part1}|{Part2}"
        : $"{Part1}|{Part2}|{Part3}";
}
```

## Conclusion

Working with lists just got easier with C# 11. It's taking all my restraint not to go through our entire codebase and refactor all the code that would be improved with list patterns. I'm sure I'll get there eventually.

For more on list patterns, check [out the docs](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#list-patterns)!
