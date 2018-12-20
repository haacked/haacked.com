---
title: 'SQL QUIZ: The Difference Between ISNULL and COALESCE'
date: 2005-01-21 -0800
tags:
- sql
redirect_from: "/archive/2005/01/20/difference-between-isnull-and-coalesce.aspx/"
---

What will the last two lines print. Will they be the same?

```sql
DECLARE @test VARCHAR(2)
DECLARE @first VARCHAR(4)
DECLARE @second< VARCHAR(4)

SELECT @first = ISNULL(@test, 'test')
SELECT @second = COALESCE(@test, 'test')

PRINT @first
PRINT @second
```

What do you think?

