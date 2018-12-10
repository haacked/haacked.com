---
layout: post
title: Another Marginally Useful Tool - BatchConcat
date: 2006-09-20 -0800
comments: true
disqus_identifier: 16952
categories: [tips tdd utilities]
redirect_from: "/archive/2006/09/19/another_marginally_useful_tool__batchconcat.aspx/"
---

UPDATE: In one comment, szeryf points out something I didn’t know and
invalidates the need for the tool I wrote. This is why I post this
stuff, so someone can tell me about a better way! Thanks szeryf! I’ve
updated the post to point out the better technique.

Based on my recent spate of posts, you can probably guess that I am
working on improving a particular build process. 

In this situation, I have a pre-build step to concatenate a bunch of
files into a single file.  I tried to do this with a simple command like
so:

> `FOR %%A in (*.sql) do CALL COPY /Y Output.sql + %%A Output.sql `

Yeah, that would work, but it is so **sloooooow**.

Szeryf points out that I can simply pass `*.sql` to the `COPY` command
and get the same result.

> `copy *.sql output.sql`

This ends up running plenty fast as it doesn’t dumbly iterate over every
file calling `COPY` once per file. Instead it lets `COPY` handle that
internally and more efficiently. How did I not know about this?

~~So I wrote a one minute app by simply scavenging the code from
[BatchEncode](https://haacked.com/archive/2006/09/20/Batch_Encode_Text_Files.aspx)
and concatenating text files instead.~~

    USAGE: batchconcat EXTENSION ENCODING OUTPUT
         sourcedir: source directory path
         extension: examples... .sql, .txt
         output:             the resulting file.
         encoding:  optional: utf7, utf8, unicode, 
                            bigendianunicode, ascii

         All paths must be fully qualified.
         USE AT YOUR OWN RISK! NO WARRANTY IS GIVEN NOR IMPLIED

~~This ended up being mighty fast!~~

~~I figure someone out there might need to do this exact same thing in
their build process and won’t mind using such crappy code.~~
