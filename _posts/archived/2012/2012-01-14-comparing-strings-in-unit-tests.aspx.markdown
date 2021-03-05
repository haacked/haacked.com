---
title: Comparing Strings in Unit Tests
tags: [tdd,code]
redirect_from: "/archive/2012/01/13/comparing-strings-in-unit-tests.aspx/"
---

Suppose you have a test that needs to compare strings. Most test
frameworks do a fine job with their default equality assertion. But once
in a while, you get a case like this:

```csharp
[Fact]
public void SomeTest()
{
    Assert.Equal("Hard \tto\ncompare\r\n", "Hard  to\r\ncompare\n");
}
```

Let’s pretend the first value in the above test is the expected value
and the second value is the value you obtained by calling some method.

Clearly, this test fails. So you look at the output and this is what you
see:

[![test-output](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Comparing-Strings-in-Unit-Tests_1422A/test-output_thumb.png "test-output")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Comparing-Strings-in-Unit-Tests_1422A/test-output_2.png)

It’s pretty hard to compare those strings by looking at them. Especially
if they are two huge strings.

This is why I typically write an extension method against `string` used
to better output a string comparison. Here’s an example of a test using
my helper.

```csharp
[Fact]
public void Fact()
{
    "Hard  to\rcompare\n".ShouldEqualWithDiff("Hard \tto\ncompare\r\n");
}
```

And here’s an example of the output.

[![test-compare-output](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Comparing-Strings-in-Unit-Tests_1422A/test-compare-output_thumb.png "test-compare-output")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Comparing-Strings-in-Unit-Tests_1422A/test-compare-output_2.png)

At the very top, the assert message is the same as before. I deferred to
the existing `Assert.Equal` method in xUnit (typically `Assert.AreEqual`
in other test frameworks) to output the error message.

Underneath the existing message are headings for three columns: the
character index, the expected character, and the actual character. For
each character I print out the `int` value and the actual character.

Of course in some cases, I don’t print out the actual value. If I were
to do that for new line characters and tab characters, it’d screw up the
formatting. So instead, I special case those characters and print out
the escape sequence in C# for those characters.

This makes it easy to compare two strings and see every difference when
a test fails. Even the hidden ones.

This is a simple quick and dirty implementation available [**in a
Gist**](https://gist.github.com/1610603 "Gist"). For example, it doesn’t
do any real DIFF comparisons and try to line up similarities. That’d be
a nice improvement to make at some point. If you can improve this, feel
free to fork the gist and send me a pull request.

