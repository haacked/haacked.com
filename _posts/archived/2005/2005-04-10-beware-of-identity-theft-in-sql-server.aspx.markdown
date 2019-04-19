---
title: Beware of @@Identity Theft in SQL Server
date: 2005-04-10 -0800 9:00 AM
tags: [sql]
redirect_from: "/archive/2005/04/09/beware-of-identity-theft-in-sql-server.aspx/"
---

In T-SQL, you can use the `@@IDENTITY` keyword to obtain the value of the
identity column when you insert a new record. For example, the following
query inserts a record into an imaginary table and returns a result set
containing the ID of the inserted column.

```sql
INSERT INTO SomeTable
    SELECT Value1, Value2
   

SELECT @@IDENTITY -- LAST INSERTED IDENTITY VALUE
```

There's the potential for a subtle bug here. Suppose later on, a
coworker realizes that any time a record is inserted into [SomeTable] a
record should also be inserted into the table [SomeTableAudit]. The
simplest solution would be to add a trigger to [SomeTable] that inserts
a record to [SomeTableAudit]. But in doing so, your coworker introduces
a case of @@IDENTITY theft. Your original query will now return the
value inserted into the IDENTITY column of the tabel [SomeTableAudit]
instead of the IDENTITY value of [SomeTable] as you intended.

At this point some of you are shaking your heads muttering
 

> Well I never use Triggers. Triggers are bad umkaaaay.
 

That's beside the point, you never know when someone else is going to
apply that trigger resulting in this unintended consequence. It pays to
program defensively. The issue here is that although @@IDENTITY is
constrained to the current session, it is not constrained to the current
scope. Instead, use the SCOPE\_IDENTITY() function which will return the
last IDENTITY column value inserted in the current scope, in this case
the value inserted into [SomeTable].

```sql
INSERT INTO SomeTable
    SELECT Value1, Value2
   

SELECT SCOPE\_IDENTITY() -- LAST INSERTED IDENTITY VALUE
```

As an aside, I'm fine with triggers in certain cases. One of the primary
complaints about triggers mirrors the complaints about Aspects in AOP.
Namely that triggers provide for unintended consequences that aren't
visible when examining a stored procedure. However when used sparingly
for cross-cutting functionality, I think they can add a lot of benefit.
Much like Aspects.

UPDATE: [Steven Campbell](http://dukeytoo.blogspot.com) adds a great
tip.

 

> Another tip I can offer is that you should not use ADO in combination
> with SQLOLEDB to retrieve IDENTITY values. I refer specifically to the
> technique of: \
>  myRS.AddNew\
>  ...\
>  myRS.Update\
>  myId = myRS("ID")\
>  \
>  This fails to retrieve the correct ID, because (internally) the
> SQLOLEDB driver issues a SELECT @@IDENTITY statement to retrieve the
> newly created ID.
