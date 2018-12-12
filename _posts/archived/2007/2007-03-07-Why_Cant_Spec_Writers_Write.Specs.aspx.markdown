---
title: Why Can't Spec Writers Write...Specs?
date: 2007-03-07 -0800
disqus_identifier: 18228
categories: []
redirect_from: "/archive/2007/03/06/Why_Cant_Spec_Writers_Write.Specs.aspx/"
---

I know, I know, you’d like to see the [FizzBuzz](http://www.codinghorror.com/blog/archives/000781.html "Why Can’t Programmers...Program") discussion die a quick death, but trust me, this is an interesting point, or at least mildly amusing.

Sorry to revive the dead horse, but a comment in my blog brought up a very good point. In fact, I’m kicking myself for not noticing this myself, having been a math major and I love pointing out this type of minutiae.

In the original Fizz Buzz test, the functional spec asks the programmer ***to print the numbers from 1 to 100***.

But as a [commenter points out](https://haacked.com/archive/2007/02/27/Why_Cant_Programmers._Read.aspx/#comment-747518745 "Comment")...

> **Why can’t spec writers write? Unless you mean integers, there are an
> infinite number of real numbers ’from 1 to 100’**

Exactly! There *are* an infinite range of numbers between 1 and 100. The specification is technically not clear enough. Writing a program to spec exactly would... well be impossible.

This is exactly why I said the following in a [another comment](https://haacked.com/archive/2007/02/27/Why_Cant_Programmers._Read.aspx/#comment-747518745 "Comment")...

> I still need to gather requirements! What platform must this FizzBuzz
> program support? Any performance requirements? Does the output need to
> be available over the web?...

Unfortunately, I missed the most important question I should have asked.

> I assume you mean all intergers from 1 to 100 inclusive, is that
> correct?

I know what you’re thinking. In cases like this, developers should be able to intuit what the client means. If a developer asks *Do you mean Integers or Real Numbers?*, that developer is being a smart ass.

But my point is still valid. If a client says, I want a CRM system, you may know exactly what a CRM system is, but it may be totally different from what they think a CRM system is.

This really highlights the difficulty of writing good requirements and a good spec. You don’t know the background of the person you’re handing off the document to.

What makes perfect sense in your mind might mean something different to the reader.

Perhaps it’s situations like this that lead 37Signals to advocate
[getting rid of functional specs altogether](http://www.37signals.com/svn/archives/001050.php "No Functional Specs").

Whether you go that extreme or not is not so important as keeping the lines of communication open with your client. Never accept a requirement and functional spec at face value. **Specs are always a poor approximation of what the client really wants. All specs are broken to one degree or another (though that doesn’t mean they are all useless).** Ask for clarification. Keep the dialog going.

This is also one reason why Big Design Up Front (BDUF) can really nail you in the butt. These subtle things are missed all the time. Having an iterative process where you’re not on the hook for requirements gathered months ago gathering dust helps mitigate the risk of incomplete and inaccurate requirements.

Even by thousands of software developers reading blogs.
