---
layout: post
title: "Interesting Perf Lesson"
date: 2006-10-18 -0800
comments: true
disqus_identifier: 18094
categories: []
---
Tyler, an old friend and an outstanding contractor for
[VelocIT](http://veloc-it.com/ "My Company") recently [wrote a
post](http://selectsoftwarethoughtsfromtyler.blogspot.com/2006/10/pass-parameters-as-objects-to-ms-data.html "Pass Parameters As Objects")
suggesting one would receive better performance by passing in an array
of objects to the Microsoft Data Application Block methods rather than
passing in an array of SqlParameter instances. He cited [this
article](http://www.informit.com/guides/content.asp?g=dotnet&seqNum=339&rl=1 "Informit .NET Reference Guide").

The article suggests that instead of this:

```csharp
public void GetWithSqlParams(SystemUser aUser)
{
  SqlParameter[] parameters = new SqlParameter[]
  {
    new SqlParameter("@id", aUser.Id)
    , new SqlParameter("@name", aUser.Name)
    , new SqlParameter("@name", aUser.Email)
    , new SqlParameter("@name", aUser.LastLogin)
    , new SqlParameter("@name", aUser.LastLogOut)
  };

  SqlHelper.ExecuteNonQuery(Settings.ConnectionString
    , CommandType.StoredProcedure
    , "User_Update"
    , parameters);
}
```

You should do something like this for performance reasons:

```csharp
public void GetWithSqlParams(SystemUser aUser)
{
  SqlHelper.ExecuteNonQuery(Settings.ConnectionString
    , CommandType.StoredProcedure
    , "User_Update"
    , aUser.Id
    , aUser.Name
    , aUser.Email
    , aUser.LastLogin
    , aUser.LastLogout);
}
```

Naturally, when given such advice, I fall back to the first rule of good
performance from the perf guru himself, [Rico
Mariani](http://blogs.msdn.com/ricom/default.aspx "Rico Mariani’s blog").
**[Rule \#1 Is to
Measure](http://blogs.msdn.com/ricom/archive/2003/12/02/40779.aspx "Good Talk On Performance Culture")**.
So I mentioned to Tyler that I’d love to see metrics on both approaches.
He posted the [result on his
blog](http://selectsoftwarethoughtsfromtyler.blogspot.com/2006/10/performance-follow-up-sqlhelper-object.html "Performance Follow Up").

> Calling the methods included in a previous post, 5000 times each,
>
> ~~~~ {.console}
> With parameters took 1203.125 milliseconds.
> With objects took 1250 milliseconds. 
> Objects took -46.875 milliseconds less.
> ~~~~
>
> 20000 times each:
>
> ~~~~ {.console}
> With parameters took 4859.375 milliseconds.  
> With objects took 5015.625 milliseconds.
> Objects took -156.25 milliseconds less.
> ~~~~

The results show that the performance difference is negligible. However,
even before seeing the performance results, I would agree with the
article to choose the second approach, but for different reasons. It
results in a lot less code. As I have [said
before](http://haacked.com/archive/2006/08/09/ASP.NETSupervisingControllerModelViewPresenterFromSchematicToUnitTestsToCode.aspx "Model-View-Presenter"),
**Less code is better code**.

I tend to prefer [optimizing for
productivity](http://haacked.com/archive/2006/09/13/Premature_Optimization_Considered_Healthy.aspx "Premature Optimization Healthy")
all the time, but only optimizing for performance after carefully
measuring for bottlenecks.

There’s also a basic economics question hidden in this story. The first
approach does seem to eke out slightly better performance, but at what
cost? That’s a lot more code to write to eke out 47 milliseconds worth
of performance out of 5000 method calls. Is it really worth it?

This particular example may not be the best example of this principle of
wasting time optimizing at the expense of productivity because there is
one redeeming factor worth mentioning with the first approach.

By explicitly specifying the parameters, the parameters can be listed in
any order, whereas the second approach requires that the parameters be
listed in the same order that they are specified in the stored
procedure. Based on that, some may find the first approach preferable.
Me, I prefer the second approach because it is cleaner, easier to read,
and I don’t see keeping the order intact much more work than getting the
parameter names correct.

But that’s just me.

