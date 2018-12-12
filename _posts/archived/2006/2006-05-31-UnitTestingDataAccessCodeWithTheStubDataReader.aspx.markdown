---
title: Unit Testing Data Access Code With The StubDataReader
date: 2006-05-31 -0800
disqus_identifier: 13070
categories: []
redirect_from: "/archive/2006/05/30/UnitTestingDataAccessCodeWithTheStubDataReader.aspx/"
---

In spirit, this is a follow-up to my recent post on [unit-testing email
functionality](https://haacked.com/archive/2006/05/30/ATestingMailServerForUnitTestingEmailFunctionality.aspx "Testing Mail Server").

This probably doesn’t apply to those of you who have reached O/RM
nirvana with such tools as NHibernate etc... ADO is probably just a
distant memory, not unlike those vague embarassing recollections of
soiling yourself in a public location long ago.

But I digress.

For the rest of us, we sometimes need to get knee-deep in ADO. For
example, even though
[Subtext](http://subtextproject.com/ "Subtext Website") abstracts away
the data access via the provider model, I still want to test the data
provider itself, right?

Subtext’s has a series of methods that each call a stored procedure and
return an `IDataReader`. The returned data reader is passed to another
class which populates entity objects using the data reader.

In my unit tests, it would be nice to have a means to attach a data
reader to an in-memory object structure rather than directly to the
database. That is where my `StubDataReader` class comes into play.

It implements the `IDataReader` interface and provides a quick and dirty
way to create an in-memory object structure. By *quick and dirty* I mean
that you do not need to build out a table schema first. The code to set
up the stub data reader is quite simple.

### Single Result Set

If you are dealing with a Data Reader that should only return one result
set (which seems to be the vast majority of cases), then setting it up
would look like this:

```csharp
DateTime testDate = DateTime.Now;
StubResultSet resultSet 
   = new StubResultSet("col0", "col1", "col2");
resultSet.AddRow(1, "Test", testDate);
resultSet.AddRow(2, "Test2", testDate.AddDays(1));
            
StubDataReader reader = new StubDataReader(resultSet);

//Advance to first row.
Assert.IsTrue(reader.Read());

// Assertions            
Assert.AreEqual(1, reader["col0"]);
Assert.AreEqual("Test", reader["col1"]);
Assert.AreEqual(testDate, reader["col2"]);
```

In the above snippet, I create an instance of `StubResultSet` with a
list of the column names. I then make a couple of calls to `AddRow`.
Notice that `AddRow` takes in a param array of `object` instances. This
is the *quick and dirty* part. Since the `StubDataReader` doesn’t
require setting up a schema before-hand, it will not validate that the
objects added to the columns of the rows are the correct type. It just
doesn’t have that information. But this isn’t all that important since
this class is specifically for use in unit testing scenarios.

### Multiple Result Sets

Not everyone realizes this, but you can iterate over multiple result
sets with a single data reader instance. Simulating that scenario is
quite easy.

```csharp
DateTime testDate = DateTime.Now;
StubResultSet resultSet 
   = new StubResultSet("col0", "col1", "col2");
resultSet.AddRow(1, "Test", testDate);

StubResultSet anotherResultSet 
   = new StubResultSet("first", "second");
anotherResultSet.AddRow((decimal)1.618, "Foo");
anotherResultSet.AddRow((decimal)2.718, "Bar");
anotherResultSet.AddRow((decimal)3.142, "Baz");

StubDataReader reader 
   = new StubDataReader(resultSet, anotherResultSet);

//Advance to first row.
Assert.IsTrue(reader.Read());

Assert.AreEqual(1, reader["col0"]);

//Advance to second ResultSet.
Assert.IsTrue(reader.NextResult(), "Expected next result set");

//Advance to first row.
Assert.IsTrue(reader.Read());
Assert.AreEqual((decimal)1.618, reader["first"]);
Assert.AreEqual("Foo", reader["second"]);
```

In this snippet, I create two `StubResultSet` instances and pass it to
the constructor of the `DataReader`. Afterwards, you can see that the
code makes sure to test that the `NextResult` functions properly.

The above code snippets above are excerpts from the unit tests I wrote
for this code. Although this code is more complete than the [mail server
example](https://haacked.com/archive/2006/05/30/ATestingMailServerForUnitTestingEmailFunctionality.aspx "Testing Mail Server"),
there are a couple methods that haven’t been well tested because I have
never run into a situation in which I needed them. I put in various
comments so feel free to improve this and let me know about it. This
code is within the `UnitTests.Subtext` project in the Subtext solution
in our [Subversion
repository](http://subtextproject.com/Home/About/ViewTheCode/tabid/116/Default.aspx "View the Code").

You can [download the
code](https://haacked.com/code/StubDataReader.zip "StubDataReader code")
here , but as before, I do not guarantee I will update the link to have
the latest code. You can access our Subversion repository for the
latest.

