---
layout: post
title: Batch SVN Rename
date: 2006-09-20 -0800
comments: true
disqus_identifier: 16929
categories: []
redirect_from: "/archive/2006/09/19/Batch_SVN_Rename.aspx/"
---

[Jon Galloway](http://weblogs.asp.net/jgalloway/) is my batch file
hero.  He’s the one who introduced me to the `FOR %%A in ...` syntax.

Today I needed to rename a bunch of files.  On one project, we haven’t
kept our file extensions consistent when creating a stored procedure
file in a Database project. Some of them had `.prc` extensions and
others have `.sql` extensions.

I wanted to rename every file to use the .sql extension.  I couldn’t
simply use a batch rename program because I wanted these files renamed
**within** Subversion, which requires running the `svn rename` command.

So using a batch file Jon sent me, I wrote the following.

```csharp
FOR %%A in (*.prc) do CALL :Subroutine %%A

GOTO:EOF

:Subroutine
svn rename %~n1.prc %~n1.sql
GOTO:EOF
```

Pretty nifty.  For each file in the current directory that ends in the
`.prc` extension, I call a subroutine.  That subroutine makes use of the
**`%~n1`** argument which provides the filename without the extension.

For help in writing your batch files, type **`help call`** in the
command prompt.

I can see using this technique all over the place. I will leave it to my
[buddy
Tyler](http://selectsoftwarethoughtsfromtyler.blogspot.com/2006/09/anatomy-of-powershell-script.html)
to provide the Powershell version.

