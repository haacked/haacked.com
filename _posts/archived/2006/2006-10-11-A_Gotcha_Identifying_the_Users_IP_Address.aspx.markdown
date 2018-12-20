---
title: A Gotcha Identifying the User's IP Address
date: 2006-10-11 -0800
disqus_identifier: 18027
tags: []
redirect_from: "/archive/2006/10/10/A_Gotcha_Identifying_the_Users_IP_Address.aspx/"
---

Recently I wrote a .NET based [Akismet API
component](https://haacked.com/archive/2006/09/26/Subtext_Akismet_API.aspx "Subtext Akismet API")
for [Subtext](http://subtextproject.com/ "Subtext Project Website").  In
attempting to make as clean as interface as possible, I made the the
type of the property to store the commenter’s IP address of type
`IPAddress`.

This sort of falls in line with the Framework Design Guidelines, which
mention using the `Uri` class in your public interface rather than a
string to represent an URL.  I figured this advice equally applied to IP
Addresses as well.

To obtain the user’s IP Address, I simply used the `UserHostAddress`
property of the `HttpRequest` object like so.

```csharp
HttpContext.Current.Request.UserHostAddress
```

The `UserHostAddress` property is simply a wrapper around the
`REMOTE_ADDR` [server
variable](http://www.w3schools.com/asp/coll_servervariables.asp "ASP Server Variables")
which can be accessed like so.

```csharp
HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]
```

For users behind a proxy (or router), this returns only one IP Address,
the IP Address of the proxy (or router).  After some more digging, I
learned that many large proxy servers will append their IP Address to a
list maintained within another HTTP Header, `HTTP_X_FORWARDED_FOR` or
`HTTP_FORWARDED`.

For example, if you make a request from a country outside of the U.S.,
your proxy server might add the header `HTTP_X_FORWARDED_FOR` and put in
your real IP and append its own IP Address to the end. If your request
then goes through yet another proxy server, it may append its IP Address
to the end.  Note that not all proxy servers follow this convention, the
notable exception being anonymizing proxies.

Thus to get the real IP address for the user, it makes sense to check
the value of this first:

HttpContext.Current.Request.ServerVariables["HTTP\_X\_FORWARDED\_FOR"]

If that value is empty or null, then check the `UserHostAddress`
property.

So what does this mean for my Akismet implementation?  I could simply
change that property to be a string and return the entire list of IP
addresses.  That’s probably the best choice, but I am not sure whether
or not Akismet accepts multiple IPs.  Not only that, I’m really tired
and lazy, and this change would require that I change the Subtext schema
since we store the commenter’s IP in a field just large enough to hold a
single IP address.

So unless smart slap me upside the head and call me crazy for this
approach, I plan to look at the `HTTP_X_FORWARDED_FOR` header first and
take the first IP address in the list if there are any.  Otherwise I
will grab the value of `UserHostAddress`.  As far as I am concerned,
it’s really not that important that I am 100% accurate in identifying
the remote IP, I just need something consistent to pass to
[Akismet](http://akismet.com/ "Akismet").

