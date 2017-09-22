---
layout: post
title: 'PATCH: Cannot Delete Posts In Subtext 1.9.2'
date: 2006-10-26 -0800
comments: true
disqus_identifier: 18114
categories: []
redirect_from: "/archive/2006/10/25/PATCH_Cannot_Delete_Posts_In_Subtext_1.9.2.aspx/"
---

Someone reported that they cannot delete posts in the just [released
Subtext
1.9.2](https://haacked.com/archive/2006/10/25/Subtext_1.9.2_quotShields_Upquot_Edition_Released.aspx "Subtext 1.9.2 released!").
I am mortified that we do do not have a unit test for this function!  To
our defense, we did start with 0% code coverage in unit tests and have
now reached 37.9% and rising!

I have a quick fix if this problem affects you. I am also currently
building a more permanent fix which I will release soon.

Run the following query in Query Analyzer (don’t forget to hit
`CTRL+SHIFT+M` to replace the template parameters before executing
this).

```csharp
ALTER PROC 
    [<dbUser,varchar,dbo>].[subtext_DeletePost]
(
    @ID int
    , @BlogID int = NULL
)
AS

DELETE FROM [<dbUser,varchar,dbo>].[subtext_Links] 
WHERE PostID = @ID

DELETE FROM [<dbUser,varchar,dbo>].[subtext_EntryViewCount] 
WHERE EntryID = @ID

DELETE FROM [<dbUser,varchar,dbo>].[subtext_Referrals] 
WHERE EntryID = @ID

DELETE FROM [<dbUser,varchar,dbo>].[subtext_Feedback] 
WHERE EntryId = @ID

DELETE FROM [<dbUser,varchar,dbo>].[subtext_Content] 
WHERE [ID] = @ID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON 
 [<dbUser,varchar,dbo>].[subtext_DeletePost]  
TO [public]
GO
```

Sorry for those that this affects. Like I said, we’ll have a bug fix out
soon.

