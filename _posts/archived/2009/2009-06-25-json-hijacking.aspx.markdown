---
title: JSON Hijacking
tags: [security,json]
redirect_from:
- "/archive/0001/01/01/json-hijacking.aspx/"
- "/archive/2009/06/24/json-hijacking.aspx/"
---

A while back I wrote about [a subtle JSON
vulnerability](https://haacked.com/archive/2008/11/20/anatomy-of-a-subtle-json-vulnerability.aspx "Anatomy of a Subtle JSON Vulnerability")
which could result in the disclosure of sensitive information. That
particular exploit involved overriding the JavaScript `Array`
constructor to disclose the payload of a JSON array, something which
most browsers do not support now.

However, there’s another related exploit that seems to affect many more
browsers. It was brought to my attention recently by someone at
Microsoft and [Scott
Hanselman](http://hanselman.com/ "Scott Hanselman's Blog") and I
demonstrated it at the Norwegian Developers Conference last week, though
it has been [demonstrated against Twitter in the
past](http://hackademix.net/2009/01/13/you-dont-know-what-my-twitter-leaks/ "You don't know what my twitter leaks").

[![hijack](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/JSONHijacking_B386/hijack_3.jpg "hijack")](http://www.sxc.hu/photo/676972 "Car Theft by alicja_M on stock.xchng")

Before I go further, let me give you the punch line first in terms of
what this vulnerability affects.

This vulnerability requires that you are exposing a JSON service which…

-   …returns sensitive data.
-   …returns a JSON array.
-   …responds to GET requests.
-   …the browser making the request has JavaScript enabled (very likely
    the case)
-   …the browser making the request supports the `__defineSetter__`
    method.

Thus if you never send sensitive data in JSON format, or you only send
JSON in response to a POST request, etc. then your site is probably not
vulnerable to *this particular vulnerability* (though there could be
others).

I’m terrible with Visio, but I thought I’d give it my best shot and try
to diagram the attack the best I could. In this first screenshot, we see
the unwitting victim logging into the vulnerable site, and the
vulnerable site issues an authentication cookie, which the browser holds
onto.

![Json-Hijack-1](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/JSONHijacking_B386/Json-Hijack-1_3.png "Json-Hijack-1")

At some point, either in the past, or the near future, the bad guy spams
the victim with an email promising a hilariously funny video of a
hamster on a piano.

![Json-Hijack-2](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/JSONHijacking_B386/Json-Hijack-2_3.png "Json-Hijack-2")

But the link actually points to the bad guy’s website. When the victim
clicks on the link, the next two steps happen in quick succession.
First, the victim’s browser makes a request for the bad guy’s website.

![Json-Hijack-3](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/JSONHijacking_B386/Json-Hijack-3_6.png "Json-Hijack-3")

The website responds with some HTML containing some JavaScript along
with a `script` tag. When the browser sees the script tag, it makes
*another GET request* back to the vulnerable site to load the script,
*sending the auth cookie along*.

![Json-Hijack-4](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/JSONHijacking_B386/Json-Hijack-4_3.png "Json-Hijack-4")

The bad guy has tricked the victim’s *browser* to issue a request for
the JSON containing sensitive information using the browser’s
credentials (aka the auth cookie). This loads the JSON array as
executable JavaScript and now the bad guy has access to this data.

To gain a deeper understanding, it may help to see actual code (which
you can download and run) which demonstrates this attack.

Note that the following demonstration is not specific to ASP.NET or
ASP.NET MVC in any way, I just happen to be using [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC Website") to demonstrate it.
Suppose the Vulnerable Website returns JSON with sensitive data via an
action method like this.

```csharp
[Authorize]
public JsonResult AdminBalances() {
  var balances = new[] {
    new {Id = 1, Balance=3.14}, 
    new {Id = 2, Balance=2.72},
    new {Id = 3, Balance=1.62}
  };
  return Json(balances);
}
```

Assuming this is a method of `HomeController`, you can access this
action via a GET request for `/Home/AdminBalances` which returns the
following JSON:

> `[{"Id":1,"Balance":3.14},{"Id":2,"Balance":2.72},{"Id":3,"Balance":1.62}]`

Notice that I’m requiring authentication via the `AuthorizeAttribute` on
this action method, so an anonymous GET request will not be able to view
this sensitive data.

The fact that this is a JSON array is important. It turns out that a
script that contains a JSON array is a valid JavaScript script and can
thus be executed. A script that just contains a JSON object is not a
valid JavaScript file. For example, if you had a JavaScript file that
contained the following JSON:

> {"Id":1, "Balance":3.14}

And you had a script tag that referenced that file:

> `<script src="http://example.com/SomeJson"></script>`

You would get a JavaScript error in your HTML page. However, through an
unfortunate coincidence, if you have a script tag that references a file
only containing a JSON array, that would be considered valid JavaScript
and the array gets executed.

Now let’s look at the HTML page that the bad guy hosts on his/her own
server:

```csharp
<html> 
...
<body> 
    <script> 
        Object.prototype.__defineSetter__('Id', function(obj){alert(obj);});
    </script> 
    <script src="http://example.com/Home/AdminBalances"></script> 
</body> 
</html>
```

What’s happening here? Well the bad guy is changing the prototype for
`Object` using the special `__defineSetter__` method which allows
overriding what happens when a property setter is being called.

In this case, any time a property named `Id` is being set on any object,
an anonymous function is called which displays the value of the property
using the alert function. Note that the script could just as easily post
the data back to the bad guy, thus disclosing sensitive data.

As mentioned before, the bad guy needs to get you to visit his malicious
page shortly after logging into the vulnerable site while your session
on that site is still valid. Typically a phishing attack via email
containing a link to the evil site does the trick.

If by blind bad luck you’re still logged into the original site when you
click through to the link, the browser will send your authentication
cookie to the website when it loads the script referenced in the script
tag. As far as the original site is concerned, you’re making a valid
authenticated request for the JSON data and it responds with the data,
which now gets executed in your browser. This may sound familiar as it
is really a variant of a [Cross Site Request Forgery (CSRF)
attack](https://haacked.com/archive/2009/04/02/anatomy-of-csrf-attack.aspx "Anatomy of a CSRF attack")
which I wrote about before.

If you want to see it for yourself, you can grab the [**CodeHaacks
solution from
GitHub**](https://github.com/Haacked/CodeHaacks "CodeHaacks on GitHub")
and run the *JsonHijackDemo* project locally (right click on the project
and select *Set as StartUp Project*. Just follow the instructions on the
home page of the project to see the attack in action. It will tell you
to visit
[http://demo.haacked.com/security/JsonAttack.html](http://demo.haacked.com/security/JsonAttack.html).

Note that this attack does not work on IE 8 which will tell you that
`__defineSetter__` is not a valid method. Last I checked, it *does* work
on Chrome and Firefox.

The mitigation is simple. Either never send JSON arrays OR always
require an HTTP POST to get that data (except in the case of
non-sensitive data in which case you probably don’t care). For example,
with ASP.NET MVC, you could use the `AcceptVerbsAttribute` to enforce
this like so:

```csharp
[Authorize]
[AcceptVerbs(HttpVerbs.Post)]
public JsonResult AdminBalances() {
  var balances = new[] {
    new {Id = 1, Balance=3.14}, 
    new {Id = 2, Balance=2.72},
    new {Id = 3, Balance=1.62}
  };
  return Json(balances);
}
```

One issue with this approach is that many JavaScript libraries such as
jQuery request JSON using a GET request by default, not POST. For
example, `$.getJSON` issues a GET request by default. So when calling
into this JSON service, you need to make sure you issue a POST request
with your client library.

ASP.NET and WCF JSON service endpoints actually wrap their JSON in an
object with the “d” property as I [wrote about a while
back](https://haacked.com/archive/2008/11/20/anatomy-of-a-subtle-json-vulnerability.aspx "Json Vulnerability").
While it might seem odd to have to go through this property to get
access to your data, this awkwardness is eased by the fact that the
generated client proxies for these services strip the “d” property so
the end-user doesn’t need to know it was ever there.

With ASP.NET MVC (and other similar frameworks), a significant number of
developers are not using client generated proxies (we don’t have them)
but instead using jQuery and other such libraries to call into these
methods, making the “d” fix kind of awkward.

### What About Checking The Header?

Some of you might be wondering, “why not have the JSON service check for
a special header such as the `X-Requested-With: XMLHttpRequest` or
`Content-Type: application/json` before serving it up in response to a
GET request?” I too thought this might be a great mitigation because
most client libraries send one or the other of these headers, but a
browser’s GET request in response to a script tag would not.

The problem with this (as a couple of co-workers pointed out to me) is
that at some point in the past, the user may have made a legitimate GET
request for that JSON in which case it may well be cached in the user’s
browser or in some proxy server in between the victim’s browser and the
vulnerable website. In that case, when the browser makes the GET request
for the script, the request might get fulfilled from the browser cache
or proxy cache. You could try setting `No-Cache` headers, but at that
point you’re trusting that the browser and all proxy servers correctly
implement caching and that the user can’t override that accidentally.

Of course, this particular caching issue isn’t a problem if you’re
serving up your JSON using SSL.

### The real issue?

There’s a post at the Mozilla Developer Center which states that object
and array initializers [should not invoke setters when
evaluated](https://developer.mozilla.org/web-tech/2009/04/29/object-and-array-initializers-should-not-invoke-setters-when-evaluated/ "initializers should not invoke setters"),
which at this point, I tend to agree with, though a comment to that post
argues that perhaps browsers really shouldn’t execute scripts regardless
of their content type, which is also a valid complaint.

But at the end of the day, assigning blame doesn’t make your site more
secure. These type of browser quirks will continue to crop up from time
to time and we as web developers need to deal with them. Chrome
2.0.172.31 and [Firefox
3.0.11](http://www.oldapps.com/firefox.php?old_firefox=59 "Download FireFox 3.0.11")
were both vulnerable to this. IE 8 was not because it doesn’t support
this method. I didn’t try it in IE 7 or IE 6.

It seems to me that to be secure by default, the default behavior for
accessing JSON should probably be POST and you should opt-in to GET,
rather than the other way around as is done with the current client
libraries. What do you think? And how do other platforms you’ve worked
with handle this? I’d love to hear your thoughts.

In case you missed it, here are the repro steps again: grab the
[**CodeHaacks solution from
GitHub**](https://github.com/Haacked/CodeHaacks "CodeHaacks on GitHub")
and run the *JsonHijackDemo* project locally (right click on the project
and select *Set as StartUp Project*. Just follow the instructions on the
home page of the project to see the attack in action. To see a
successful attack, you’ll need to do this in a vulnerable browser such
as [Firefox
3.0.11](http://www.oldapps.com/firefox.php?old_firefox=59 "Download FireFox 3.0.11").

I followed up this post with [a proposal to fix
JSON](https://haacked.com/archive/2009/06/26/too-late-to-change-json.aspx "Too late to change JSON?")
to prevent this particular issue.

