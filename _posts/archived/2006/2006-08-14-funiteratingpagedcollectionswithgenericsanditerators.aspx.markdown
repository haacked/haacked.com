---
title: Fun Iterating PagedCollections With Generics and Iterators
tags: [tips,tdd,csharp]
redirect_from: "/archive/2006/08/13/funiteratingpagedcollectionswithgenericsanditerators.aspx/"
---

![Book](https://haacked.com/assets/images/IteratingABookofPagedCollectionsUsingIte_55B/BlankBook_thumb.jpg)
Oh boy are you in for a roller coaster ride now!

Let me start with a question, *How do you iterate through a large
collection of data without loading the entire collection into memory?*

The following scenario probably sounds quite familiar to you. You have a
lot of data to present to the user. Rather than slapping all of the data
onto a page, you display one page of data at a time.

One technique for this approach is to define an interface for paged
collections like so...

```csharp
/// <summary>
/// Base interface for paged collections.
/// </summary>
public interface IPagedCollection
{
    /// <summary>
    /// The Total number of items being paged through.
    /// </summary>
    int MaxItems
    {
        get;
        set;
    }
}

/// <summary>
/// Base interface for generic paged collections.
/// </summary>
public interface IPagedCollection<T> 
    : IList<T>, IPagedCollection
{ 
}
```

The concrete implementation of a generic paged collection is really
really simple.

```csharp
/// <summary>
/// Concrete generic base class for paged collections.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedCollection<T> : List<T>, IPagedCollection<T>
{
    private int maxItems;

    /// <summary>
    /// Returns the max number of items to display on a page.
    /// </summary>
    public int MaxItems
    {
        get { return this.maxItems; }
        set { this.maxItems = value; }
    }
}
```

A method that returns such a collection will typically have a signature
like so:

```csharp
public IPagedCollection<DateTime> GetDates(int pageIndex
    , int pageSize)
{
    //Some code to pull the data from database 
    //for this page index and size.
    return new PagedCollection<DateTime>();
}
```

A `PagedCollection` represents one page of data from the data source
(typically a database). As you can see from the above method, the
consumer of the `PagedCollection` handles tracking the current page to
display. This logic is not encapsulated by the `PagedCollection` at all.
This makes a lot of sense in a web application since you will only show
one page at a time.

But there are times when you might wish to iterate over *every page* as
in a streaming situation.

For example, suppose you need to perform some batch transformation of a
large number of objects stored in the database, such as serializing
every object into a file.

Rather than pulling every object into memory and then iterating over the
huge collection ending up with a really big call to `Flush()` at the end
(or calling flush after each iteration, ending up in too much flushing),
a better approach might be to page through the objects calling the
`Flush()` method after each *page* of objects.

The `CollectionBook` class is useful just for that purpose. **It is a
class that makes use of iterators to iterate over every page in a set of
data without having to load every record into memory.**

You instantiate the `CollectionBook` with a `PagedCollectionSource`
delegate. This delegate is used to populate the individual pages of the
data we are iterating over.

```csharp
public delegate IPagedCollection<T> 
    PagedCollectionSource<T>(int pageIndex, int pageSize);
```

When iterating over the pages of a `CollectionBook` instance, each
iteration will call the delegate to retrieve the next page (an instance
of `IPagedCollection<T>`) of data. This uses the new **[iterators
feature of C#
2.0](http://msdn2.microsoft.com/en-us/library/dscyy5s0.aspx "Iterators on MSDN").

Here is the code for the enumerator.

```csharp
///<summary>
///Iterates through each page one at a time, calling the 
/// PagedCollectionSource delegate to retrieve the next page.
///</summary>
public IEnumerator<IPagedCollection<T>> GetEnumerator()
{
  if (this.pageSize <= 0)
    throw new InvalidOperationException
      ("Cannot iterate a page of size zero or less");

  int pageIndex = 0;
  int pageCount = 0;

  if (pageCount == 0)
  {
    IPagedCollection<T> page 
      = pageSource(pageIndex, this.pageSize);
    pageCount = (int)Math.Ceiling((double)page.MaxItems / 
      this.pageSize);
    yield return page;
  }

  //We've already yielded page 0, so start at 1
  while (++pageIndex < pageCount)
  {
    yield return pageSource(pageIndex, this.pageSize);
  }
}
```

The following is an example of instantiating a CollectionBook using an
anonymous delegate.

```csharp
CollectionBook<string> book = new CollectionBook<string>(
    delegate(int pageIndex, int pageSize)
    {
        return pages[pageIndex];
    }, 3);
```

I wrote some source code and a unit test [you can
download](http://tools.veloc-it.com/tabid/58/grm2id/8/Default.aspx "CollectionBook Code Sample")
that demonstrates this technique. I am including a C# project library
that contains these classes and one unit test. To get the unit test to
work, simply reference your unit testing assembly of choice and
uncomment a few lines.
