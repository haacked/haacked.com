---
layout: post
title: "Is It Always Bad To Swallow Exceptions?"
date: 2005-08-09 -0800
comments: true
disqus_identifier: 9293
categories: []
---
Reading [this
post](http://jaysonknight.com/blog/archive/2005/08/10/1736.aspx) from
Jayson’s blog caught my attention for two reasons. First, his very
strong reaction to some code that swallows an exception. Second, the
fact that I’ve written such code before.

Here is the code in question.

public bool IsNumeric(string s)

{

    try

    {

        Int32.Parse(s);

    }

    catch

    {

        return false;

    }

    return true;

}

Jayson’s proposed solution is...

> I personally would use double.TryParse() (and downcast accordingly
> depending on the result) at the very least. At the very most I'd break
> the string down to a char array, and walk the array calling one of the
> (very) useful static char.Is\<whatever\> methods…first non\<whatever\>
> value, break out of the loop and return false. I've posted before
> about the speed at which the framework can process char data…it's very
> fast and effecient (sic).

For the sake of this discussion, let’s assume the method was intended to
be `IsInteger()`. Using `Int.Parse()` to test if a string is a number
doesn’t make sense since it immediately chokes on 3.14 (get it? Chokes
on Pi. Get it? Damn. No sense of humor). If indeed this method was
intended to be `IsNumeric` then I would suggest using double.TryParse
and the discussion is finished.

Now in general, I agree with Jayson and often raise fits when I see an
exception blindly swallowed. However, when you only deal in absolutes,
you start to become a robot (yes, I am resisting the offhand political
joke here). For every absolute rule you find in programming (or anywhere
for that matter), there is often an example case that is the exception
to the rule. As they say, the exception proves the rule.

The problem with simply parsing the string character by character is
that it’s quite easy to make a mistake. For example, if you simply
called `char.IsNumber()` on each character, your code would choke on
"-123". That’s certainly an integer.

Also, what happens when you want to extend this to handle hex numbers
and thousands separators. For example, this code snippet shows various
ways to parse an integer.

Console.WriteLine(int.Parse("07A", NumberStyles.HexNumber));

Console.WriteLine(int.Parse("-1234", NumberStyles.AllowLeadingSign));

Console.WriteLine(int.Parse("1,302,312", NumberStyles.AllowThousands));

Console.WriteLine(int.Parse("-1302312"));

This is one of those cases where the API failed us, and was corrected in
the upcoming .NET 2.0. In .NET 2.0, this is a moot point. But for those
of us using 1.1, I think this is a case where it can be argued that
swallowing an exception is a valid workaround for a problem with the
API. However, we should swallow the correct exception.

Since there is no `int.TryParse()` method, I’d still rather rely on the
API to do number parsing than rolling my own. It’s not that I don’t
think I am capable of it, but I have a much smaller base of testers than
the framework team. Here’s how I might rewrite this method.

public bool IsInteger(string s, NumberStyles numberStyles)

{

    if(s == null)

        throw new ArgumentNullException("s", "Sorry, but I don't do
null.");

 

    try

    {

        Int32.Parse(s, numberStyles);

        return true;

    }

    catch(System.FormatException)

    {

        //Intentionally Swallowing this.

    }

    return false;

}

So in 99.9% of the cases, I agree with Jayson that you should generally
not swallow exceptions, but there are always the few cases where it
might be appropriate. When in doubt, throw it. In the rewrite of this
method, notice that I don’t catch ALL exceptions, only the expected one.
I wouldn’t want to swallow a `ThreadAbortException`,
`OutOfMemoryException`, etc...

I would also put a //TODO: in there so that as soon as the polish is put
on .NET 2.0, I would rewrite this method immediately to use
`int.TryParse()` and make everybody more comfortable.

This is a case where I do feel uneasy using an exception to control the
flow, but that uneasiness is ameliorated in that it is encapsulated in a
tight small method. Also, one objection to this post I can anticipate is
that it is freakin’ easy to parse an integer, so why not roll your own?
While true, the principle remains. What if we were discussing parsing
something much more difficult? For exampe, suppose we were instead
discussing a method `IsGuid()`. Now you have to deal with the fact there
isn’t even a `Guid.Parse()` method. You have to pass the string to the
constructor of the Guid which will throw an exception if the string is
not in a valid format. Yikes! I thought constructors were *never*
supposed to throw exceptions.

In this case, I’d probably prefer not to roll my own Guid parsing
algorithm, instead relying on the one provided. Why write code that
already exists?

So Jayson, in general you are right, but please don’t beat me to death
with a wet noodle if you see something like this in my code. ;)

