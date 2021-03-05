---
title: Dealing With Denial of Service Attacks
tags: [security,aspnet]
redirect_from: "/archive/2008/08/21/dealing-with-denial-of-service-attacks.aspx/"
---

As Scott [wrote last
week](http://www.hanselman.com/blog/HackedAndIDidntLikeItURLScanIsStepZero.aspx "Hacked!"),
using a punny title I have to admire, he and I (among many others) were
both the subject of a DoS (Denial of Service) attack. Looking through my
logs, it looks to actually be a DDoS (Distributed Denial of Service)
attack coming from multiple IP addresses.

The attack appears to actually be an attempt at a SQL Injection attack,
but for his blog, which stores its data in XML files, that is entirely
pointless. For my blog, which doesn’t do any inline SQL, it’s also
mostly pointless. So far, the SQL injection part of the attack has
failed, but it *has* succeeded in pegging my CPU. Maybe that’s the
actual intended goal. Only the attacker knows.

### LogParser Queries

The first clue (besides my site being down) is that my log file for
today is huge at 9:00 AM.

![log-files](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DealingWithDenialofServiceAttacks_8743/log-files_3.png "log-files")

The next step is to run some queries against my logs using the fantastic
LogParser tool. This post, entitled *[Forensic Log Parsing with
Microsoft’s
LogParser](http://www.securityfocus.com/infocus/1712 "Forensic Log Parsing")*
is a great resource for constructing queries. The focus tends to be more
on investigating an actual intrusion. The queries I need are to discover
what kind of DoS attack I’m experiencing. Here’s the query I’m using so
far…

      logparser "SELECT c-ip, COUNT(*), STRLEN(cs-uri-query) as LENGTH, cs-uri-query 
      FROM C:\WINDOWS\system32\LogFiles\W3SVC1\ex080822.log 
      GROUP BY Length, cs-uri-query, c-ip 
      HAVING Length > 500 
      ORDER BY LENGTH DESC" -rtp:-1 > long-query.txt

Note that I’m running this for a single log file for the day. I could
use a wildcard and run this for all my log files. The very last snippet,
*\> long-query.txt*, pipes the output to a text file. Here’s a snippet
of one of the query strings I’m seeing:

> ?';DECLARE%20@S%20CHAR(4000);SET%20@S=CAST***…\*snip\*…***%20AS%20CHAR(4000));EXEC(@S);

The length of these query strings are all very long. Interestingly
enough, there’s no smooth transition in length. For example, there are
no query strings of length 500 – 1000.

### URL Scan

I then went and installed URLScan 3.0 Beta, which Scott wrote about, and
went into the configuration file (located at
**`C:\WINDOWS\system32\inetsrv\urlscan\UrlScan.ini`** by default and
changed the following setting near the bottom:

      MaxQueryString=2048

From its default of 2048 to another smaller value.

The other setting I changed is to allow dots in the path because I have
many URLs that contain dots.

      AllowDotInPath=1

