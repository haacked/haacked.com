---
title: Log4Net Breaking Change in 1.2.9
date: 2006-08-07 -0800 9:00 AM
tags: [logging]
redirect_from: "/archive/2006/08/06/Log4NetBreakingChangeIn1.2.9.aspx/"
---

I am a little late in reporting this, but I hadn’t realized the problem
until I had to maintain an older project that used Log4Net 1.2.8. I
upgraded it to log4net 1.2.10 and noticed it stopped working. I then
found [this
comment](http://mail-archives.apache.org/mod_mbox/logging-log4net-user/200506.mbox/%3CDDEB64C8619AC64DBC074208B046611C7692D5@kronos.neoworks.co.uk%3E "comment")
in the log4net mailing list archives.

> There were a number of breaking changes in 1.2.9
>
> [http://logging.apache.org/log4net/release/release-notes.html\#1.2.9](http://logging.apache.org/log4net/release/release-notes.html#1.2.9 "Log4net Release Notes 1.2.9")
>
> In your config file "**log4net.spi.LevelEvaluator**" needs to be
> updated to "**log4net.Core.LevelEvaluator**".

I hope changes that would break existing config files are far and few
between.

