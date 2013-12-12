---
layout: post
title: "SQL QUIZ: The Difference Between ISNULL and COALESCE"
date: 2005-01-21 -0800
comments: true
disqus_identifier: 1975
categories: [sql]
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

