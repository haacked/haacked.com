---
title: Bulletproof Sql Change Scripts Using INFORMATION_SCHEMA Views
tags: [sql]
redirect_from: "/archive/2006/07/04/bulletproofsqlchangescriptsusinginformation_schemaviews.aspx/"
---

![Bullet](https://haacked.com/assets/images/singlebullet.jpg) Working as a team
against a common database schema can be a real challenge. Some teams
prefer to have their local code connect to a centralized database, but
this approach can create many headaches. If I make a schema change to a
shared database, but am not ready to check in my code, that can break
the site for another developer. For a project like Subtext, it is just
not feasible to have a central database.

Instead, I prefer to work on a local copy of the database and propagate
changes via versioned change scripts. That way, when I check in my code,
I can let others know which scripts to run on their local database when
they get latest source code. Of course this can be also be a big
challenge as the number of scripts starts to grow and developers are
stuck bookkeeping which scripts they have run and which they haven’t.

That is why I always recommend to my teams that we script schema and
data changes in an
[idempotent](http://en.wikipedia.org/wiki/Idempotent "An operation applied more than once is the same as if applied once")
manner whenever possible. That way, it is much easier to simply batch
updates together in a single file (per release for example) and a
developer simply runs that single script any time an update is made.

As an example, suppose we have a `Customer` table and we need to add a
column for the customer’s favorite color. I would script it like so:

```sql
IF NOT EXISTS 
(
    SELECT * FROM [information_schema].[columns] 
    WHERE   table_name = 'Customer' 
    AND table_schema = 'dbo'
    AND column_name = 'FavoriteColorId'
)
BEGIN
    ALTER TABLE [dbo].[Customer]
    ADD FavoriteColorId int
END
```

This script basically checks for the existence of the `FavoriteColorId`
column on the table `Customer` and if it doesn’t exist, it adds it. You
can run this script a million times, and it will only make the schema
change once.

You’ll notice that I didn’t query against the system tables, instead
choosing to lookup the information in an INFORMATION\_SCHEMA view named
*Columns*. This is the Microsoft recommendation as they reserve the
right to change the system tables at any time. The information views are
part of the SQL-92 standard, so they are not likely to change.

There are 20 schema views in all, listed below with their purpose
(aggregated from SQL Books). Note that in all cases, only data
accessible to the user executing the query against the
information\_schema views is returned.

<table class="highlightTable">
    <tbody>
        <tr>
            <th>Name</th>
            <th>Returns</th>
        </tr>
        <tr class="alt">
            <td>CHECK_CONSTRAINTS</td>
            <td>Check Constraints</td>
        </tr>
        <tr>
            <td>COLUMN_DOMAIN_USAGE</td>
            <td>Every column that has a user-defined data type.</td>
        </tr>
        <tr class="alt">
            <td>COLUMN_PRIVILEGES</td>
            <td>Every column with a privilege granted to or by the current user in the current database.</td>
        </tr>
        <tr>
            <td>COLUMNS</td>
            <td>Lists every column in the system</td>
        </tr>
        <tr class="alt">
            <td>CONSTRAINT_COLUMN_USAGE</td>
            <td>Every column that has a constraint defined on it.</td>
        </tr>
        <tr>
            <td>CONSTRAINT_TABLE_USAGE</td>
            <td>Every table that has a constraint defined on it.</td>
        </tr>
        <tr class="alt">
            <td>DOMAIN_CONSTRAINTS</td>
            <td>Every user-defined data type with a rule bound to it.</td>
        </tr>
        <tr>
            <td>DOMAINS</td>
            <td>Every user-defined data type.</td>
        </tr>
        <tr class="alt">
            <td>KEY_COLUMN_USAGE</td>
            <td>Every column that is constrained as a key</td>
        </tr>
        <tr>
            <td>PARAMETERS</td>
            <td>Every parameter for every user-defined function or stored procedure in the datbase. For functions this returns one row with return value information.</td>
        </tr>
        <tr class="alt">
            <td>REFERENTIAL_CONSTRAINTS</td>
            <td>Every foreign constraint in the system.</td>
        </tr>
        <tr>
            <td>ROUTINE_COLUMNS</td>
            <td>Every column returned by table-valued functions.</td>
        </tr>
        <tr class="alt">
            <td>ROUTINES</td>
            <td>Every stored procedure and function in the database.</td>
        </tr>
        <tr>
            <td>SCHEMATA</td>
            <td>Every database in the system.</td>
        </tr>
        <tr class="alt">
            <td>TABLE_CONSTRAINTS</td>
            <td>Every table constraint.</td>
        </tr>
        <tr>
            <td>TABLE_PRIVILEGES</td>
            <td>Every table privilege granted to or by the current user.</td>
        </tr>
        <tr class="alt">
            <td>TABLES</td>
            <td>Every table in the system.</td>
        </tr>
        <tr>
            <td>VIEW_COLUMN_USAGE</td>
            <td>Every column used in a view definition.</td>
        </tr>
        <tr class="alt">
            <td>VIEW_TABLE_USAGE</td>
            <td>Every table used in a view definition.</td>
        </tr>
        <tr>
            <td>VIEWS</td>
            <td>Every View</td>
        </tr>
    </tbody>
</table>

When selecting rows from these views, the table must be prefixed with
*information\_schema* as in *SELECT \* FROM information\_schema.tables*.

Please note that the information schema views are based on a SQL-92
standard so some of the terms used in these views are different than the
terms in Microsoft SQL Server. For example, in the example above, I set
`table_schema = 'dbo'`. The term *schema* refers to the owner of the
database object.

Here is another code example in which I add a constraint to the
`Customer` table.

```sql
IF NOT EXISTS(
    SELECT * 
    FROM [information_schema].[referential_constraints] 
    WHERE constraint_name = 'FK_Customer_Color' 
      AND constraint_schema = 'dbo'
)
BEGIN
  ALTER TABLE dbo.Customer WITH NOCHECK 
  ADD CONSTRAINT
  FK_Customer_Color FOREIGN KEY
  (
    FavoriteColorId
  ) REFERENCES dbo.Color
  (
    Id
  )
END
```

I generally don’t go to all this trouble for stored procedures, user
defined functions, and views. In those cases I will use Enterprise
manager generate a full drop and create script. When a stored procedure
is dropped and re-created, you don’t lose data as you would if you
dropped and re-created a table that contained some data.

With this approach in hand, I can run an update script with new schema
changes confident that I any changes in the script that I have already
applied will not be applied again. The same approach works for lookup
data as well. Simply check for the data’s existence before inserting the
data. It is a little bit more work up front, but it is worth the trouble
and schema changes happen less frequently than code or stored procedure
changes.

