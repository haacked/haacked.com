---
layout: post
title: HTML Stripping Challenge
date: 2008-11-11 -0800
comments: true
disqus_identifier: 18552
categories: [code html]
redirect_from: "/archive/2008/11/10/html-stripping-challenge.aspx/"
---

UPDATE: I added three new unit tests and one interesting case in which
the three browser render something differently.

Well I’m back at it, but this time I want to strip all HTML from a
string. Specifically:

-   Remove all HTML opening and self-closing tags: Thus \<foo\> and
    \<foo /\> should be stripped.
-   Remove all HTML closing tags such as \</p\>.
-   Remove all HTML comments.
-   Do not strip any text in between tags that would be rendered by the
    browser.

This may not sound all that difficult, but I have a feeling that many
existing implementations out there would not pass the set of unit tests
I wrote to verify this behavior.

I’ll present some pathological cases to demonstrate some of the odd edge
cases.

We’ll start with a relative easy one.

`<foo title=">" />`

Notice that this tag contains the closing angle bracket within an
attribute value. That closing bracket should not close the tag. Only the
one outside the attribute value should close it. Thus, the method should
return an empty string in this case.

Here’s another case:

`<foo =test>title />`

This is a non quoted attribute value, but in this case, the inner angle
bracket should close the tag, leaving you with “*title /\>*”.

Here’s a case that surprised me.

`<foo<>Test`

That one strips everything except “*\<\>Test*”.

It gets even better…

`<foo<!>Test`

strips out everything except “*Test*”.

And finally, here’s one that’s a real doozy.

    <foo<!--foo>Test

Check out how FireFox, IE, and Google Chrome render this same piece of
markup.

![weird-markup-chrome](https://haacked.com/images/haacked_com/WindowsLiveWriter/HTMLStrippingChallenge_12C01/weird-markup-chrome_3.png "weird-markup-chrome")

![weird-markup-ff](https://haacked.com/images/haacked_com/WindowsLiveWriter/HTMLStrippingChallenge_12C01/weird-markup-ff_3.png "weird-markup-ff")

![weird-markup-ie](https://haacked.com/images/haacked_com/WindowsLiveWriter/HTMLStrippingChallenge_12C01/weird-markup-ie_3.png "weird-markup-ie")

*One of these kids is doing his own thing!* ;) For my unit test, I
decided to go with majority rules here (I did not test with Opera) and
went with the behavior of the two rather than Firefox.

### The Challenge

The following is the shell of a method for stripping HTML from a string
based on the requirements listed above.

```csharp
public static class Html {
  public static string StripHtml(string html) {
    throw new NotImplementedException("implement this");
  }
}
```

Your challenge, should you choose to accept it, is to implement this
method such that the following unit tests pass. I apologize for the
small font, but I used some long names and wanted it to fit.

```csharp
[TestMethod]
public void NullHtml_ThrowsArgumentException() {
    try {
        Html.StripHtml(null);
        Assert.Fail();
    }
    catch (ArgumentNullException) {
    }
}

[TestMethod]
public void Html_WithEmptyString_ReturnsEmpty() {
    Assert.AreEqual(string.Empty, Html.StripHtml(string.Empty));
}

[TestMethod]
public void Html_WithNoTags_ReturnsTextOnly() {
    string html = "This has no tags!";
    Assert.AreEqual(html, Html.StripHtml(html));
}

[TestMethod]
public void Html_WithOnlyATag_ReturnsEmptyString() {
    string html = "<foo>";
    Assert.AreEqual(string.Empty, Html.StripHtml(html));
}

[TestMethod]
public void Html_WithOnlyConsecutiveTags_ReturnsEmptyString() {
    string html = "<foo><bar><baz />";
    Assert.AreEqual(string.Empty, Html.StripHtml(html));
}

[TestMethod]
public void Html_WithTextBeforeTag_ReturnsText() {
    string html = "Hello<foo>";
    Assert.AreEqual("Hello", Html.StripHtml(html));
}

[TestMethod]
public void Html_WithTextAfterTag_ReturnsText() {
    string html = "<foo>World";
    Assert.AreEqual("World", Html.StripHtml(html));
}

[TestMethod]
public void Html_WithTextBetweenTags_ReturnsText() {
    string html = "<p><foo>World</foo></p>";
    Assert.AreEqual("World", Html.StripHtml(html));
}

[TestMethod]
public void Html_WithClosingTagInAttrValue_StripsEntireTag() {
    string html = "<foo title=\"/>\" />";
    Assert.AreEqual(string.Empty, Html.StripHtml(html));
}

[TestMethod]
public void Html_WithTagClosedByStartTag_StripsFirstTag() {
    string html = "<foo <>Test";
    Assert.AreEqual("<>Test", Html.StripHtml(html));
}

[TestMethod]
public void Html_WithSingleQuotedAttrContainingDoubleQuotesAndEndTagChar_StripsEntireTag() { 
    string html = @"<foo ='test""/>title' />";
    Assert.AreEqual(string.Empty, Html.StripHtml(html));
}

[TestMethod]
public void Html_WithDoubleQuotedAttributeContainingSingleQuotesAndEndTagChar_StripsEntireTag() {
    string html = @"<foo =""test'/>title"" />";
    Assert.AreEqual(string.Empty, Html.StripHtml(html));
}

[TestMethod]
public void Html_WithNonQuotedAttribute_StripsEntireTagWithoutStrippingText() {
    string html = @"<foo title=test>title />";
    Assert.AreEqual("title />", Html.StripHtml(html));
}

[TestMethod]
public void Html_WithNonQuotedAttributeContainingDoubleQuotes_StripsEntireTagWithoutStrippingText() {
    string html = @"<p title = test-test""-test>title />Test</p>";
    Assert.AreEqual("title />Test", Html.StripHtml(html));
}

[TestMethod]
public void Html_WithNonQuotedAttributeContainingQuotedSection_StripsEntireTagWithoutStrippingText() {
    string html = @"<p title = test-test""- >""test> ""title />Test</p>";
    Assert.AreEqual(@"""test> ""title />Test", Html.StripHtml(html));
}

[TestMethod]
public void Html_WithTagClosingCharInAttributeValueWithNoNameFollowedByText_ReturnsText() {
    string html = @"<foo = "" />title"" />Test";
    Assert.AreEqual("Test", Html.StripHtml(html));
}

[TestMethod]
public void Html_WithTextThatLooksLikeTag_ReturnsText() {
    string html = @"<çoo = "" />title"" />Test";
    Assert.AreEqual(html, Html.StripHtml(html));
}

[TestMethod]
public void Html_WithCommentOnly_ReturnsEmptyString() {
    string s = "<!-- this go bye bye>";
    Assert.AreEqual(string.Empty, Html.StripHtml(s));
}

[TestMethod]
public void Html_WithNonDashDashComment_ReturnsEmptyString() {
    string s = "<! this go bye bye>";
    Assert.AreEqual(string.Empty, Html.StripHtml(s));
}

[TestMethod]
public void Html_WithTwoConsecutiveComments_ReturnsEmptyString() {
    string s = "<!-- this go bye bye><!-- another comment>";
    Assert.AreEqual(string.Empty, Html.StripHtml(s));
}

[TestMethod]
public void Html_WithTextBeforeComment_ReturnsText() {
    string s = "Hello<!-- this go bye bye -->";
    Assert.AreEqual("Hello", Html.StripHtml(s));
}

[TestMethod]
public void Html_WithTextAfterComment_ReturnsText() {
    string s = "<!-- this go bye bye -->World";
    Assert.AreEqual("World", Html.StripHtml(s));
}

[TestMethod]
public void Html_WithAngleBracketsButNotHtml_ReturnsText() {
    string s = "<$)*(@&$(@*>";
    Assert.AreEqual(s, Html.StripHtml(s));
}

[TestMethod]
public void Html_WithCommentInterleavedWithText_ReturnsText() {
    string s = "Hello <!-- this go bye bye --> World <!--> This is fun";
    Assert.AreEqual("Hello  World  This is fun", Html.StripHtml(s));
}

[TestMethod]
public void Html_WithCommentBetweenNonTagButLooksLikeTag_DoesStripComment() {
    string s = @"<ç123 title=""<!bc def>"">";
    Assert.AreEqual(@"<ç123 title="""">", Html.StripHtml(s));
}


[Test]
public void Html_WithTagClosedByStartComment_StripsFirstTag()
{
    //Note in Firefox, this renders: <!--foo>Test
    string html = "<foo<!--foo>Test";
    Assert.AreEqual("Test", HtmlHelper.RemoveHtml(html));
}

[Test]
public void Html_WithTagClosedByProperComment_StripsFirstTag()
{
    string html = "<FOO<!-- FOO -->Test";
    Assert.AreEqual("Test", HtmlHelper.RemoveHtml(html));
}

[Test]
public void Html_WithTagClosedByEmptyComment_StripsFirstTag()
{
    string html = "<foo<!>Test";
    Assert.AreEqual("Test", HtmlHelper.RemoveHtml(html));
}
```

What’s the moral of this story, apart from “Phil has way too much time
on his hands?” In part, it’s that parsing HTML is fraught with peril. I
wouldn’t be surprised if there are some cases here that I’m missing. If
so, let me know. I used FireFox’s DOM Explorer to help verify the
behavior I was seeing.

I think this is also another example of the challenges of software
development in general along with the 80-20 rule. It’s really easy to
write code that handles 80% of the cases. Most of the time, that’s good
enough. But when it comes to security code, even 99% is not good enough,
as hackers will find that 1% and exploit it.

In any case, I think I’m really done with this topic for now. I hope it
was worthwhile. And as I said, I’ll post my code solution to this later.
Let me know if you find missing test cases.
