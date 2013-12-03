---
layout: post
title: "Added Contstraint Based Expressions To Subsonic"
date: 2007-05-22 -0800
comments: true
disqus_identifier: 18326
categories: []
---
NUnit 2.4 introduces a really nice programming model they call
[Constraint-Based Assert
Model](http://nunit.com/index.php?p=constraintModel&r=2.4 "Constraint-Based Assert Model").
I believe [MbUnit 2.4](http://mbunit.com/ "MbUnit") will also have this.
I really like this approach to building asserts because it reads almost
like English.

```csharp
Assert.That( myString, Is.EqualTo("Hello") );
```

Look at that fine piece of prose!

I’m so enamored of this approach I thought I’d try to bring it to
Subsonic. Here is an example of two existing approaches to create a
Query in Subsonic.

```csharp
new Query("Products").WHERE("ProductId < 5");
new Query("Products").WHERE("ProductId", Comparison.LessThan, 5);
```

Now what don’t I like about these? Well in the first one, there’s no
intellisence to guide me on making sure I choose a valid operator. Not
only that, if that 5 is a variable instead, I’m doing some string
concatenation, which I find to be ugly and harder to read such as this:

```csharp
new Query("Products").WHERE("ProductId < " + productId);
```

The second one is much better in that I get the benefit of Intellisense
and it is pretty readable and understandable. But can we do better? I
mean, who talks like that? “Hand me the nails where the length is
comparison greater than five.”

This is where I find the Constraint Based model to be very elegant and
readable.

```csharp
new Query("Products").WHERE("ProductId", Is.LessThan(5));
```

Now if you’re looking at this and wondering, *where is the intellisense
for the table name and column name?* Don’t worry, it’s there. I used
strings here for brevity. But here’s the final query with everything
strongly typed.

```csharp
new Query(Tables.Product, "Northwind")
  .WHERE(Product.Columns.ProductID, Is.LessThan(5))
```

This is just my first pass at this for Subsonic. I need to get a better
understanding of how these queries are being built so I can add the
following syntax next:

```csharp
new Query("Products").WHERE("ProductId", Is.In(1,2,3,4,5);
// AND
new Query("Products").WHERE("ProductId", Is.In(new int[]{1,2,3,4});
```

This code has been committed to the trunk and is not yet in any release.
It is pretty simple so far.

I wonder if I should propose the following syntax helper:

```csharp
Select.From("Products").WHERE("ProductId", Is.LessThan(5));

//Where Select.From is defined as:

public static class Select
{
  public static Query From(string tableName)
  {
    return new Query(tableName);
  }
}
```

Or is that taking this too far. Thoughts?

