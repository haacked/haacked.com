---
title: 'A Case Study In Design Tradeoffs: Usability vs Discoverability'
tags: [aspnet,code,aspnetmvc]
redirect_from: "/archive/2008/11/05/usability-vs-discoverability.aspx/"
---

Usability and Discoverability (also referred to as Learnability) are
often confused with one another, but they really are distinct concepts.
In Joel Spolsky’s wonderful [User Interface Design for
Programmers](http://www.amazon.com/gp/product/1893115941?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=1893115941)
(go read it!), Joel provides an metaphor to highlight the difference.

> It takes several weeks to learn how to drive a car. For the first few
> hours behind the wheel, the average teenager will swerve around like
> crazy. They will pitch, weave, lurch, and sway. If the car has a stick
> shift they will stall the engine in the middle of busy intersections
> in a truly terrifying fashion. \
> If you did a usability test of cars, you would be forced to conclude
> that they are simply unusable.

[![Scary
Driver](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ACaseStudyInDesignTradeoffsUsabilityvsDi_11DB9/learning-to-drive_thumb.jpg "Scary Driver")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ACaseStudyInDesignTradeoffsUsabilityvsDi_11DB9/learning-to-drive_2.jpg)

> This is a crucial distinction. When you sit somebody down in a typical
> usability test, you’re really testing how learnable your interface is,
> not how usable it is. Learnability is important, but it’s not
> everything. Learnable user interfaces may be extremely cumbersome to
> experienced users. If you make people walk through a fifteen-step
> wizard to print, people will be pleased the first time, less pleased
> the second time, and downright ornery by the fifth time they go
> through your rigamarole.
>
> Sometimes all you care about is learnability: for example, if you
> expect to have only occasional users. An information kiosk at a
> tourist attraction is a good example; almost everybody who uses your
> interface will use it exactly once, so learnability is much more
> important than usability.

Rick Osborne in his post, [Usability vs
Discoverability](http://rickosborne.org/blog/index.php/2007/04/19/usability-vs-discoverability/ "Usability vs Discoverability"),
also covers this distinction, while Scott Berkun points out in his post
on [The Myth of
Discoverability](http://www.scottberkun.com/essays/26-the-myth-of-discoverability/ "The Myth of Discoverability")
that you can’t have everything be discoverable.

These are all exmaples of the principle that there is [no such thing as
a perfect
design](https://haacked.com/archive/2005/05/31/ThereIsNoPerfectDesign.aspx "There is no perfect design").
Design *always* consists of trade-offs.

Let’s look at an example using a specific feature of ASP.NET Routing
that illustrates this trade-off. One of the things you can do with
routes is specify constraints for the various URL parameters via the
`Constraints` property of the `Route` class.

The type of this property is `RouteValueDictionary` which contains
string keys mapped to `object` values. Note that by having the values of
this dictionary be of type `object`, the value type isn’t very
descriptive of what the value should be. This hurts learnability, but
let’s dig into why we did it this way.

One of the ways you can specify the value of a constraint is via a
regular expression string like so:

```csharp
Route route = new Route("{foo}/{bar}", new MyRouteHandler());
route.Constraints = 
  new RouteValueDictionary {{ "{{" }}"foo", "abc.*"}, {"bar", "\w{4}"}};
RouteTable.Routes.Add(route);
```

This route specifies that the *foo* segment of the URL must start with
“abc” and that the *bar* segment must be four characters long. Pretty
dumb, yeah, but it’s just an example to get the point across.

We figure that in 99.9% of the cases, developers will use regular
expression constraints. However, there are several cases we identified
in which a regular expression string isn’t really appropriate, such as
constraining the HTTP Method. We could have hard coded the special case,
which we originally did, but decided to make this extensible because
more cases started cropping up that were difficult to handle. This is
when we introduced the `IRouteConstraint` interface.

At this point, we had a decision to make. We could have changed the the
type of the `Constraints` property to something where the values are of
type `IRouteConstraint `rather than `object` in order to aid
discoverability. Doing this would require that we then implement and
include a `RegexConstraint` along with an `HttpMethodConstraint`.

Thus the above code would look like:

```csharp
Route route = new Route("{foo}/{bar}", new MyRouteHandler());
route.Constraints = 
  new RouteConstraintDictionary {{ "{{" }}"foo", new RegexConstraint("abc.*")}, 
    {"bar", new RegexConstraint("\w{4}")}};
RouteTable.Routes.Add(route);
```

That’s definitely more discoverable, but at the cost of usability in the
general case (note that I didn’t even include other properties of a
route you would typically configure). For most users, who stick to
simple regular expression constraints, we’ve just made the API more
cumbersome to use.

It would’ve been really cool if we could monkey patch an implicit
conversion from `string` to `RegexConstraint` as that would have made
this much more usable. Unfortunately, that’s not an option.

So we made the call to favor usability in this one case at the expense
of discoverability, and added the bit of hidden magic that if the value
of an item in the constraints dictionary is a string, we treat it as a
regular expression. But if the value is an instance of a type that
implements `IRouteConstraint`, we’d call the `Match` method on it.

It’s not quite as discoverable the first time, but after you do it once,
you’ll never forget it and it’s much easier to use every other time you
use it.

### Making Routing with MVC More Usable

Keep in mind that Routing is a separate feature from ASP.NET MVC. So
what I’ve covered applies specifically to Routing.

When we looked at how Routing was used in MVC, we realized we had room
for improving the usability. Pretty much every time you define a route,
the route handler you’ll use is `MvcRouteHandler` it was odd to require
users to always specify that for every route. Not only that, but once
you got used to routing, you’d like a shorthand for defining defaults
and constraints without having to go through the full [collection
initializer
syntax](https://haacked.com/archive/2008/01/06/collection-initializers.aspx "Collection Initializers")
for `RouteValueDictionary`.

This is when we created the set of `MapRoute` extension methods specific
to ASP.NET MVC to provide a façade for defining routes. Note that if you
prefer the more explicit approach, we did not remove the
`RouteCollection`’s `Add` method. We merely layered on the `MapRoute`
extensions to `RouteCollection` to make defining routes simpler. Again,
a trade-off in that the arguments to the `MapRoute` methods are not as
discoverable as using the explicit approach, but they are usable once
you understand how they work.

### Addressing Criticisms

We spent a lot of time thinking about these design decisions and
trade-offs, but it goes without saying that it will [invite
criticisms](http://ayende.com/Blog/archive/2008/11/05/a-case-study-of-bad-api-design-asp.net-mvc-routing.aspx "Bad API").
Fortunately, part of my job description is to have a thick skin. ;)

In part, by favoring usability in this case, we’ve added a bit of
friction for those who are just starting out with ASP.NET MVC, just like
in Joel’s example of the teenager learning to drive. However, after
multiple uses, it becomes second nature, which to me signifies that it
is usable. Rather than a flaw in our API, I see this more as a
deficiency in our documentation and Intellisense support, but we’re
working on that. This is an intentional trade-off we made based on
feedback from people building multiple applications.

But I understand it won’t please everyone. What would be interesting for
me to hear is whether these usability enhancements work. After you
struggle to define constraints the first time, was it a breeze the next
time and the time after that, especially when compared to the
alternative?

