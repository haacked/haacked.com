---
title: Batch Encode Text Files
tags: [tools]
redirect_from: "/archive/2006/09/19/Batch_Encode_Text_Files.aspx/"
---

I hesitate to blog this because this tool is really really really really
rough, quick, and dirty.  As in it needs a big ol’ box of Tide.  

I needed to convert a bunch of UTF-16 text files into UTF-8 so I spent
five minutes writing a little console app to do it.

This thing literally has no exception handling etc, but it gets the job
done for my needs and I thought others might find it useful if they have
exactly the same need. 

Hey, feel free to clean up the code and send it back to me, or point me
in the direction of some free tool I should’ve used all along.

    USAGE: batchencode extension encoding [backup]
        extension: file extension with the dot. ex .sql, .txt
        encoding:  values... utf7, utf8, unicode, bigendianunicode, ascii
        backup:    optional fully qualified (sorry) backup directory.

Download [the code
here](http://tools.veloc-it.com/tabid/58/grm2id/12/Default.aspx "batch encoder").

