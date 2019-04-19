---
title: "[SQL] Stored ProcedureTo Generate Random Time of Day"
date: 2006-04-21 -0800 9:00 AM
tags: [sql]
redirect_from: "/archive/2006/04/20/sqlfunctiontogeneraterandomtimeofday.aspx/"
---

Here is a function that will generate a random time of day. Later I will
show why I am posting this particular query and how I am using it. It
comes in useful when trying create random scheduled jobs in SQL Server.
I made use of a technique for [generating random
dates](http://weblogs.asp.net/jgalloway/archive/2004/03/18/92498.aspx "Random Dates / Data for Testing")
via [Jon
Galloway](http://weblogs.asp.net/jgalloway/archive/2004/03/18/92498.aspx "Jon Galloway's Blog").

Parameter  | DataType |  Extra      | Description
------------|----------|-------------|------------
@timeOfDay |int       |OUTPUT       | Represents a time of day using the format HHMMSS. For example, midnight is represented as 0 and 1:52:01 PM is represented as 135201.
@maxHour   |int       |Default = 24 | Upper bound for the hour of the day. So to generate a random time of day
between midnight and 3 AM, specify 3.

Here is an example of its usage.

```sql
-- Generate time of day between midnight and 3
DECLARE @timeOfDay int
exec GetRandomTimeOfDay @timeOfDay OUTPUT, 24
PRINT @timeOfDay
```

And here is the stored procedure declaration itself. Notice I am
creating this procedure within the *master* database.

```sql
USE [master]
GO

CREATE PROCEDURE [dbo].GetRandomTimeOfDay
( 
    @timeOfDay int = 0 OUTPUT
    , @maxHour int = 24 -- Upper bound for the hour.
)
AS
BEGIN
    IF @maxHour > 24 OR @maxHour < 1
        RAISERROR ('Choose value between 1 and 24', 16, 1)   
    
    DECLARE @randomHours int
    SELECT @randomHours = 
        (@maxHour - 1) * 
RAND(CAST(CAST(newid() as binary(8)) as INT))
    
    DECLARE @randomMinutes int
    SELECT @randomMinutes = 
        60 * RAND(CAST(CAST(newid() as binary(8)) as INT))
    
    DECLARE @timeOfDayDate DateTime
    SET @timeOfDayDate = '00:00:00'
    
    SET @timeOfDayDate = 
DATEADD(hh, @randomHours, @timeOfDayDate)
    SET @timeOfDayDate = 
DATEADD(mi, @randomMinutes, @timeOfDayDate)
    
    DECLARE @timeAsString varchar(8)
    DECLARE @timeWithoutColons varchar(6)
    
    SET @timeAsString = CONVERT(varchar(8), @timeOfDayDate, 8)
    SET @timeWithoutColons = REPLACE(@timeAsString, ':', '')
    
    SET @timeOfDay = ( CAST(@timeWithoutColons as int) )
END
GO
```

In a later installment, I will show you why this is useful.

You can [download the SQL](http://tools.veloc-it.com/tabid/58/grm2id/1/Default.aspx "Tools")
in a zip file

