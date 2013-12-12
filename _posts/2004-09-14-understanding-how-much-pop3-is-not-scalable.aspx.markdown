---
layout: post
title: "Understanding How Much POP3 Is Not Scalable"
date: 2004-09-14 -0800
comments: true
disqus_identifier: 1207
categories: []
---
I have the lovely task of importing a POP3 mailbox with 144524 messages
into our database. I'm using a 3rd party component, but am quickly
learning more than I ever wanted to know about POP3. For example,
ideally you don't want to use POP3 for large mail boxes because POP3
isn't scalable.

Having read [RFC 1939](http://www.faqs.org/rfcs/rfc1939.html) which
specifies the Post Office Protocol Version 3, I now understand *why* it
isn't scalable. There are three phases to a POP3 session, the
AUTHORIZATION phase, the TRANSACTION phase, and the UPDATE phase.

The **AUTHORIZATION** phase is simply a login phase. The USER command
specifies the username to log in the mailbox with and the PASS command
specifies a password. Once authenticated, the server enters the next
phase.

The **TRANSACTION** phase is where the POP3 client does the real work.
For example, the STAT command returns the message count within the
mailbox. In order to retrieve an individual email, the Message Id is
required. To get that, issue the LIST command with no arguments, and the
POP3 server will list all the message ids and the size in octets.
There's no way to specify a number of messages to return. So in my case,
the command has to return the IDs and sizes for all 144524 messages. But
wait, it gets better.

Once, a POP3 client is done issuing commands to delete messages in the
TRANSACTION phase, none of those messages are actually deleted until the
client issues the QUIT command. At that point, the POP3 session enters
the **UPDATE** phase and the server starts to delete messages marked for
deletion. As you can deduce, that could be problematic for a large
mailbox.

Also, you might consider deleting messages in batches which is fine, but
there's a hitch. The QUIT command terminates your session after the
UPDATE phase is over and POP3 does not guarantee that the message ids
you listed before will be the same in a separate session. Therefore you
have to issue the LIST command at the beginning of each session to list
every message even if you only plan to process a small subset.

In any case, I'm working with the author of the POP3 component I'm using
to iron out some kinks and make this work. Once I trim this mailbox
down, it should hopefully never get so big again.

