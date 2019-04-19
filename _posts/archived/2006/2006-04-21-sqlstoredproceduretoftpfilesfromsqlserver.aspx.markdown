---
title: "[SQL] Stored Procedure To FTP Files From SQL Server"
tags: [sql]
redirect_from: "/archive/2006/04/20/sqlstoredproceduretoftpfilesfromsqlserver.aspx/"
---

[This
is](http://www.nigelrivett.net/FTP/s_ftp_PutFile.html "Ftp Stored Proc")
another useful Sql Server Stored Procedure I found on the net written by
[Nigel Rivett](http://www.nigelrivett.net/ "Nigel Rivett’s Blog").

The procedure uses the `xp_cmdshell` extended stored procedure to shell
out an FTP command. You can use this procedure to ftp a file from one
place to another. Of course, you will need to make sure that your
command runs in the proper security context.

I made some very slight modifications in my own version of this
procedure. I changed some of the parameters to be of type `nvarchar`
instead of varchar for my international friends. I also changed the name
to suit my own naming conventions.

It takes in the following parameters.

Parameter    | Data Type     | Description                    | Example
-------------|---------------|--------------------------------|--------
@FTPServer   | varchar(128)  | The host name.                 | *ftp.example.com*
@FTPUser     | nvarchar(128) | The username for the FTP site. | *Haacked*
@FTPPWD      | nvarchar(128) | The password for the FTP site. | *Ha!_AsIfIWouldTellYou!*
@FTPPath     | nvarchar(128) | The subfolder within the FTP site to place the file. Make sure to use forward slashes and leave a trailing slash. | */*
@FTPFileName | nvarchar(128) | The filename to write within FTP. Typically the same as the source file name. | *ImportantFile.zip*
@SourcePath  | nvarchar(128) | The path to the directory that contains the source file. Make sure to have a trailing slash. | *c:\\projects\\*
@SourceFile  | nvarchar(128) | The source file to ftp.        | *ImportantFile.zip*
@workdir     | nvarchar(128) | The working directory. This is where the stored proc will temporarily write a command file containing the FTP commands it will execute.| *c:\\temp\\*

Here is an example of the usage.

```csharp
exec FtpPutFile     
    @FTPServer = 'ftp.example.com' ,
    @FTPUser = n'username' ,
    @FTPPWD = n'password' ,
    @FTPPath = n'/dir1/' ,
    @FTPFileName = n'test2.txt' ,
    @SourcePath = n'c:\vss\mywebsite\' ,
    @SourceFile = n'MyFileName.html' ,
    @workdir = n'c:\temp\'
```

I will soon combine this and my [random time of day generator sql](https://haacked.com/archive/2006/04/21/SQLFunctionToGenerateRandomTimeOfDay.aspx "Generate Random Time Of Day")
into a very useful stored procedure for you.

