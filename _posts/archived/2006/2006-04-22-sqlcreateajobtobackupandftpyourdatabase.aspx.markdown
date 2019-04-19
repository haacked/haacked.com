---
title: "[SQL] Create a Job to Backup and FTP Your Database"
date: 2006-04-22 -0800 9:00 AM
tags: [sql]
redirect_from: "/archive/2006/04/21/sqlcreateajobtobackupandftpyourdatabase.aspx/"
---

In two earlier posts I presented a couple of SQL stored procedures that
I promised to tie together. The [first
procedure](https://haacked.com/archive/2006/04/21/SQLFunctionToGenerateRandomTimeOfDay.aspx "Generate a Random Time Of Day")
generates a random time of day and [the
second](https://haacked.com/archive/2006/04/21/SQLStoredProcedureToFTPFilesFromSQLServer.aspx "Ftp Files From SQL Server")
can be used to FTP a file from within SQL Server.

Well in this post I put these two together to create a stored procedure
for creating a database job that will take a nightly backup of a
database and FTP it to another location. I hope this isn’t terribly
anti-climactic for you.

This script is perfect for a quick and dirty backup plan for a database.
If you are dealing with enterprise level (and sized) databases, your DBA
will probably scoff at this script, and rightly so. Such a situation
really needs a more robust database backup plan probably with
differential backups etc...

But for smaller operations, this will save your butt. I use this to
backup my blog database and some of our development databases every
night.

The parameters are pretty much a combination of the two previous
parameters.

<table class="spec">
    <tbody>
        <tr>
            <th>Parameter</th>
            <th>Datatype</th>
            <th>Description</th>
        </tr>
        <tr>
            <td>@databaseName</td>
            <td>nvarchar(128)</td>
            <td>Name of the database to backup</td>
        </tr>
        <tr>
            <td>@jobName</td>
            <td>nvarchar(255)</td>
            <td>The name of this Job as it would be listed in Enterprise Manager under <em>Management</em> | <em>Sql Server Agent</em> | <em>Jobs</em></td>
        </tr>
        <tr>
            <td>@scheduleName</td>
            <td>nvarchar(255)</td>
            <td>A descriptive name for the schedule.</td>
        </tr>
        <tr>
            <td>@timeOfDay</td>
            <td>int</td>
            <td>Time of day as an int (<em>HHMMSS</em>). Default is -1 which indicates a random time of day between midnight and @maxHour</td>
        </tr>
        <tr>
            <td>@maxHour</td>
            <td>int</td>
            <td>If generating a random time of day, the upper bound for the hour in which the backup job can run. By default 4 am.</td>
        </tr>
        <tr>
            <td>@owner</td>
            <td>nvarchar(128)</td>
            <td>The database user account that this job runs under. By default the <em>sa</em> account.</td>
        </tr>
        <tr>
            <td>@backupDirectory</td>
            <td>nvarchar(256)</td>
            <td>The directory on the database server to backup the file.</td>
        </tr>
        <tr>
            <td>@ftpServer</td>
            <td>nvarchar(128)</td>
            <td>FTP host name.</td>
        </tr>
        <tr>
            <td>@ftpUser</td>
            <td>nvarchar(128)</td>
            <td>FTP user.</td>
        </tr>
        <tr>
            <td>@ftpPassword</td>
            <td>nvarchar(128)</td>
            <td>FTP user password.</td>
        </tr>
        <tr>
            <td>@ftpPath</td>
            <td>nvarchar(128)</td>
            <td>FTP path.</td>
        </tr>
    </tbody>
</table>

### Why the randomness?

Notice that if you set `@timeOfDay` to -1, this script will create a
schedule for the job at a random hour of the day which is constrained by
`@maxHour`. This is really there to help stagger the times at which the
database backups run. All too often administrators make the mistake of
scheduling the backups of all the backups at the same time. Of course,
you can override this randomness by simply specifying a `@timeOfDay`.

Typical usage of the query looks like

```sql
exec CreateBackupJob @databaseName='MCTDb'
    , @ftpServer='example.com'
    , @ftpUser='UserName'
    , @ftpPassword='Supersecret'
```

When you obtain this script, I recommend modifying the script when you
create it on a server to have give the ftp parameters default values, if
that makes sense in your environment. That can save you time when
creating new backups.

You can download the full script here. This script will drop (if they
exist) and recreate all three stored procedures I mentioned. Let me know
if you found this useful.

[Download it from
http://tools.veloc-it.com/](http://tools.veloc-it.com/tabid/58/grm2id/2/Default.aspx "Download it from http://tools.veloc-it.com")

