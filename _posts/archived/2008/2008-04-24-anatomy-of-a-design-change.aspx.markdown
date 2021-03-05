---
title: Anatomy of a &quot;Small&quot; Software Design Change
tags: [aspnetmvc,design]
redirect_from: "/archive/2008/04/23/anatomy-of-a-design-change.aspx/"
---

File this one away for the next time your boss comes in and
asks,[![lumberg[1]](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/AnatomyofaDesignChange_7EB2/lumberg%5B1%5D_thumb.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/AnatomyofaDesignChange_7EB2/lumberg%5B1%5D_2.jpg)

> Yeaaah, I’m going to need you to make that little change to the code.
> It’ll only take you a couple hours, right?

Software has this deceptive property in which some changes that seem
quite big and challenging to the layman end up being quite trivial,
while other changes that seem quite trivial, end up requiring a lot of
thought, care, and work.

**Often, little changes add up to a lot.**

I’m going to walk through a change we made that seemed like a no-brainer
but ended up having a lot of interesting consequences that were
invisible to most onlookers.

The Setup
---------

I’ll provide just enough context to understand the change. The project I
work on, ASP.NET MVC allows you to call methods on a class via the URL.

For example, a request for **/product/list** might call a method on a
class named `ProductController` with a method named `List`. Likewise a
request for **/product/index** would call the method named `Index`. We
call these “web callable” methods, *Actions*. Nothing new here.

There were a couple rules in place for this to happen:

-   The controller class must inherit from the Controller class.
-   The action method must be annotated with the `[ControllerAction]`
    attribute applied to it.

We received a lot of feedback on the requirement for having that
attribute. There were a lot of good reasons to have it there, but there
were also a lot of good reasons to remove it.

Removing that requirement should be pretty simple, right? Just find the
line of code that checks for the existence of the attribute and change
it to not check at all.

The Consequences
----------------

Ahhh, if only it were that easy my friend. There were many consequences
to that change. The solutions to these consequences were mostly easy.
The hard part was making sure we caught all of them. After all, you
don’t know what you don’t know.

**Base Classes**

So we removed the check for the attribute and the first thing we noticed
is “*Hey! Now I can make a request for **/product/gethashcode**, Cool!*”

Not so cool. Since every object ultimately inherits from
`System.Object`, every object has several public methods: `ToString()`,
`GetHashCode()`, `GetType()`, and `Equals()`, and so on... In fact, our
Controller class itself has a few public methods.

The solution here is conceptually easy, we only look at public methods
on classes that derive from our Controller class. In other words, we
ignore methods on `Controller` and on `Object`.

**Controller Inheritance**

One of the rationales for removing the attribute is that in general,
there isn’t much of a reason to have a public method on a controller
class that isn’t available from the web. But that isn’t always true.
Let’s look at one situation. Suppose you have the following abstract
base controller.

```csharp
public abstract class CoolController : Controller
{
  public virtual void Smokes() {...}

  public virtual void Gambles() {...}

  public virtual void Drinks() {...}
}
```

It is soooo cool!

You might want to write a controller that uses the `CoolController` as
its base class rather than `Controller` because `CoolController` does
some cool stuff.

However, you don’t think smoking is cool at all. Too bad, `Smokes()` is
a public method and thus an action, so it is callable. At this point, we
realized we need a `[NonAction]` attribute we can apply to an action to
say that even though it is public, it is not an action.

With this attribute, I can do this:

```csharp
public class MyReallyCoolController : CoolController
{
  [NonAction]
  public override void Smokes()
  {
    throws new NotImplementedException();
  }
}
```

Now `MyReallyCoolController` doesn’t smoke, which is really cool.

**Interfaces**

Another issue that came up is interfaces. Suppose I implement an
interface with public methods.  Should those methods by default be
callable? A good example is `IDisposable`. If I implement that
interface, suddenly I can call Dispose() via a request for
**/product/dispose**.

Since we already implemented the `[NonAction]` attribute, we decided
that yes, they are callable if you implicitly implement them because
they are public methods on your class and you have a means to make them
not callable.

We also decided that if you explicitly implement an interface, those
methods would not be callable. That would be one way to implement an
interface without making every method an action and would not require
you to annotate every interface method.

**Special Methods**

Keep in mind that in C# and VB.NET, property setters and getters are
nothing more than syntactic sugar. When compiled to IL, they end up
being methods named `get_PropertyName()` and `set_PropertyName()`. The
constructor is implemented as a method named `.ctor()`. When you have an
indexer on a class, that gets compiled to `get_Item()`.

I’m not saying it was hard to deal with this, but we did have to
remember to consider this. We needed to get a list of methods on the
controller that are methods in the “typical” sense and not in some funky
compiler-generated or specially named sense.

**Edge Cases**

Now we started to get into various edge cases. For example, what if you
inherit a base controller class, but use the `new` keyword on your
action of the same name as an action on the base class? What if you have
multiple overloads of the same method? And so on. I won’t bore you with
all the details. The point is, it was interesting to see all these
consequences bubble up for such a simple change.

Is This Really Going To Help You With Your Boss?
------------------------------------------------

Who am I kidding here? Of course not. :) Well..., maybe.

If your boss is technical, it may be a nice reminder that software is
often like an iceberg. It is easy to see the 10% of work necessary, but
the other 90% of work doesn’t become apparent till you dig deeper.

If your boss is not technical, we may well be speaking a different
language here. I need to find an analogy that a business manager would
understand. A situation in their world in which something that seems
simple on the surface ends up being a lot of work in actuality. If you
have such examples, be a pal and post them in the comments. The goal
here is to find common ground and a shared language for describing the
realities of software to non-software developers.
