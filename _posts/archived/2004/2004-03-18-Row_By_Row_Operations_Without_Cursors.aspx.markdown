---
title: 'TIP: Row by Row operations without cursors'
tags: [code,sql]
redirect_from: "/archive/2004/03/17/Row_By_Row_Operations_Without_Cursors.aspx/"
---

![](/assets/images/cursor.jpg)In general, cursors suck ass! Ok, that’s a bit
extreme, but I have a long and ugly history with cursors. Let me diverge
here and tell you a true story.

A while ago, a friend of mine recommended me to a company in serious
need of senior developers for full-time or contract work. After talking
to the dev manager over the phone, he felt my rate was too high, but
wanted me to come in anyways. He set his top three developers in the
room with me and left as they began to drill me with technical
questions.

Now, I don’t mind being asked difficult technical questions in an
interview. In fact, I think it’s a necessary part of an interview. But
it was clear from the outset that these three hadn’t set their egos
aside and they were quite antagonistic. One of them asked me the
following question.

Suppose you have a table named `tblUser` with column named *FirstName*.
On the whiteboard, construct a query that will select all the first
names into a single varchar with commas. I asked if I may assume that
the list of names will fit in a VARCHAR 8000, to which they replied yes.
So I promptly wrote the following on the board.

```sql
DECLARE @FirstNames VARCHAR(8000)
SET @FirstNames = '' 
SELECT @FirstNames = FirstNames + ',' + @FirstNames
FROM tblUser
```

With disdain on their faces, they shook their heads and said no. No!
Well of course they did, they were looking for a cursor answer. They
wanted to know if I could write a cursor. Needless to say, I didn’t get
the job, but after the fact, I couldn’t help sending the manager an
email informing him that not only was my answer correct, but it was 1000
times faster than the answer they wrote on the board. Yes, I’m still
bitter. ;)

Which brings me back to the point of this post. If you can avoid a
cursor solution, by all means do. The following
[article](http://www.sql-server-performance.com/dp_no_cursors.asp "Row level operations without cursors")
describes a technique for performing row by row operations without using
a cursor. It makes several assumptions about your table, but for the
most part, this is very useful.

