---
layout: post
title: "WS Security and the Reason Behind Hashed Passwords"
date: 2004-11-03 -0800
comments: true
disqus_identifier: 1565
categories: []
---
I received an email in response to my post [How To Avoid ClearText
Passwords With
UsernameToken](http://haacked.com/archive/2004/09/09/1177.aspx) that
asks the following question:

> ...Thus if a hacker steals the hashed password from your database, he
> will be able to write an application that gives the hash to WSE and he
> will authenticate successfully - which is exactly what we are trying
> to avoid by storing the hashed passwords in the first place. \
> \
> ...\
> \
>  The bottom line: this approach won't really solve the real problem -
> if I steal the hash from the database, I will be able to uthenticate
> successfully. I'd love this to work the way you describe but as a
> security-conscious developer I'm still losing sleep.

Although this is a true scenario, the author makes an assumption that is
false. The purpose of storing a hashed password is NOT to stop a hacker
who obtains the hash from being able to authenticate as that user.

Think of it this way, if I'm a hacker and I am able to compromise your
user database and obtain a user's hashed password, why would I ever try
to authenticate as that user? Since I already have my grubby hands in
the cookie jar, I might as well grab all the data directly from your
compromised database.

Rather, the purpose of hashing a password with a salt value is to
provide security to the user of the system that rogue employees of the
company and hackers who compromise the database cannot use my password
to log into other sites I frequent.

Ideally your database isn't compromised very often, otherwise you have
bigger problems than whether or not passwords are hashed.

That's why a security minded developer doesn't stop at hashing
passwords. Code security is never enough and is only a small part of the
equation. The IT staff have to make sure the database itself is secure
and not likely to be compromised. Staff with access to the system must
be trained to deal with social engineering attacks. What good is a
hashed password if I can call up tech support and get any information I
need by posing as an executive?

So to the author of this email, I suggest you don't lose sleep over the
hashed password scenario. As a security conscious developer, you have a
huge number of other attack scenarios to lose sleep over. ;-)

