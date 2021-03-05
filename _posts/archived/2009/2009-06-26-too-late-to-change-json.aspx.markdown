---
title: Is It Too Late To Change JSON?
tags: [json,security]
redirect_from: "/archive/2009/06/25/too-late-to-change-json.aspx/"
---

In my last post, I wrote about the [hijacking of JSON
arrays](https://haacked.com/archive/2009/06/25/json-hijacking.aspx "JSON Hijacking").
Near the end of the post, I mentioned a comment whereby someone suggests
that what really should happen is that browsers should be more strict
about honoring content types and not execute code with the content type
of `application/json`.

I totally agree! But then again, browsers haven’t had a good track
record with being strict with such standards and it’s probably too much
to expect browsers to suddenly start tightening ship, not to mention
potentially breaking the web in the process.

Another potential solution that came to mind was this: Can we simply
change JSON? Is it too late to do that or has that boat left the harbor?

[![boat-left-harbor](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IsItTooLateToChangeJSON_117AE/boat-left-harbor_thumb.jpg "boat-left-harbor")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IsItTooLateToChangeJSON_117AE/boat-left-harbor_2.jpg)

Let me run an idea by you. What if everyone got together and decided to
version the JSON standard and change it in such a way that when the
entire JSON response is an array, the format is no longer executable
script. Note that I’m not referring to an array which is a property of a
JSON object. I’m referring to the case when the entire JSON response is
an array.

One way to do this, and I’m just throwing this out there, is to make it
such that the JSON package must always begin and end with a curly brace.
JSON objects already fulfill this requirement, so their format would
remain unchanged.

But when the response is a JSON array, we would go from here:

> `[{"Id":1,"Amt":3.14},{"Id":2,"Amt":2.72}]`

to here:

> `{[{"Id":1,"Amt":3.14},{"Id":2,"Amt":2.72}]}`

Client code would simply check to see if the JSON response starts with
`{[` to determine whether it’s an array, or an object. There many
alternatives, such as simply wrapping ALL JSON responses in some new
characters to keep it simple.

It’d be possible to do this without breaking every site out there by
simply giving all the client libraries a head start. We would update the
JavaScript libraries which parse JSON to recognize this new syntax, but
still support the old syntax. That way, they’d work with servers which
haven’t yet upgraded to the new syntax.

As far as I know, most sites that make use of JSON are using it for Ajax
scenarios so the site developer is in control of the client and server
anyways. For sites that provide JSON as a cross-site service, upgrading
the server before the clients are ready could be problematic, but not
the end of the world.

So what do you think? Is this worth pursuing? Not that I have any idea
on how I would convince or even who I would need to convince. ;)

UPDATE: 6/26 10:39 AM [Scott Koon](http://lazycoder.com "friend met")
points out this idea is not new (I didn’t think it would be) and points
to a great post that gives more detail on the [specifics of executable
JSON](http://robubu.com/?p=25 "Conflating JSON with JavaScript") as it
relates to the ECMAScript Specification.

