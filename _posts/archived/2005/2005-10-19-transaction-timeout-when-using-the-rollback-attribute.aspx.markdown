---
title: Transaction Timeout When Using the RollBack Attribute
date: 2005-10-19 -0800 9:00 AM
tags: [tools]
redirect_from: "/archive/2005/10/18/transaction-timeout-when-using-the-rollback-attribute.aspx/"
---

I noticed a recent check-in has added a `TimeOut` property to the
`RollBack` attribute in MbUnit. Woohoo!

A while ago I [presented the source
code](https://haacked.com/archive/2005/06/10/4580.aspx) for a `RollBack`
attribute for NUnit based on [Roy
Osherove’s](http://weblogs.asp.net/rosherove/) work in the area. Well I
found a little problem with using the RollBack attribute that affects
the one I presented along with the one that comes packaged with MbUnit.

I uncovered the problem while running a particularly long running unit
test. Every time I ran the test, it failed at just about exactly 61
seconds into it (I know, a unit taking that long is kind of useless for
TDD, but I’ll get that time down to something manageable. I promise!).

I reran the test multiple times and the line of code it failed on would
be different, but MbUnit was showing me that it was failing at 61
seconds every time. To prove it, I removed the RollBack attribute and
ran the test and it succeeded after around 90 seconds (yeah, I have some
heavy perf work to do, but it is a BIG test).

The error message I got each time was *Distributed transaction
completed. Either enlist this session in a new transaction or the NULL
transaction*.

Not a helpful message because I wasn’t attempting to complete the
transaction. But the timing of the matter made it obvious to me I was
running into a timeout issue.

The RollBack attribute works by enlisting a COM+ 1.5 transaction, which
allows you to use Enterprise Services without inheriting from
`ServicedComponent` using a feature called Services Without Components
or SWC for short (gotta love them TLAs). To work around the issue in
MbUnit, I simply removed the RollBack attribute and added the code to
start a COM+ transaction directly to the method. The one change I made
was to set the `TransactionTimeout` property which takes an integer
timeout value in seconds.

[Test]

public void MyTest()

{

    ServiceConfig config = new ServiceConfig();

    config.TransactionTimeout = 120;

    config.Transaction = TransactionOption.RequiresNew;

    ServiceDomain.Enter(config);

    try

    {

        //Run my test code...

    }

    finally

    {

        if(ContextUtil.IsInTransaction)

        {

            //Abort the transaction.

            ContextUtil.SetAbort();

        }

        ServiceDomain.Leave();

    }

}

At the same time, I revisited the `RollBack` attribute I put together
for NUnit and added a `TransactionTimeout` property to the attribute.
That way you can mark up a test like so...

[Test]

[RollBack(120)]

public void MyTest()

{

    //Run my test code...

}

You can [download the new version of the attribute for NUnit
here](https://haacked.com/code/RollbackAttribute.zip).

As for MbUnit, I’ll mention this to the maintainers and we’ll hopefully
see a fix soon.

