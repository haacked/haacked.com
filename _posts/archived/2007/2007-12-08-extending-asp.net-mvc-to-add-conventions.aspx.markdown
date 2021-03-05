---
title: Extending ASP.NET MVC To Add Conventions
tags: [aspnet,code,aspnetmvc]
redirect_from: "/archive/2007/12/07/extending-asp.net-mvc-to-add-conventions.aspx/"
---

UPDATE: Much of this post is out-of-date with the latest versions of
MVC. We long sinced removed the `ControllerAction` attribute.

*Note: If you hate reading and just want the code, it is at the bottom.*

Eons ago, I was a youngster living in
[Spain](https://haacked.com/archive/2005/11/29/highlights-from-spain.aspx "Highlights from Spain")
watching my Saturday morning cartoons when my dad walked in bearing
freshly made taquitos and a small cup of green stuff. The taquitos
looked delicious, but I was appalled at the green stuff.

*Was this some kind of joke?* My dad wanted me to simply *just taste it*
but I refused because I absolutely knew it would suck just by looking at
it. The green stuff, of course, was guacamole, which I love by the
truckload now.

![Guacamole](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/235087d3d642_11FF7/Guacamole_3.jpg)

With all the code samples and blog posts published about the ASP.NET MVC
Framework, there’s been some debate about the big picture stuff.

-   Should I be looking to migrate to this? (*Depends*)
-   Will this replace Web Forms? (*NO!*)
-   Is this feature even necessary? (*I sure think so!*)

Interestingly enough, the most passionate debate I’ve seen is not around
these big picture questions, but is centered around very specific
detailed design decisions. After all, we are software developers, and if
there’s one thing software developers love to do, it’s debate the design
and architecture of code.

I’m no exception to this rule and admit I rather enjoy it, sometimes to
the point of absurdity and pointlessness. At the end of the day,
however, the framework developer has to make a decision and move forward
so he or she can go home. These choices will never make *everyone* happy
because [there is no such thing as a perfect
design](https://haacked.com/archive/2005/05/31/ThereIsNoPerfectDesign.aspx "There is no perfect design")
that satisfies everyone.

**That doesn’t mean we give up trying though!**

Hopefully the quality of feedback the framework designer receives pushes
that designer to reevaluate assumptions and either reinforces the
decision, or provides insight for an even better decision.

One design decision in particular that seems to have drawn somewhat
disproportionate amount of attention is the decision to require a
`[ControllerAction]` attribute on public methods within a controller
class that are meant to be a web visible action. There’s been much
discussion in various mailing lists and in some blog posts before the
CTP has been released.

-   [Configuration over
    Convention](http://www.ayende.com/Blog/archive/2007/12/08/Configuration-over-Convention.aspx "Configuration over Convention")
-   [Phil Haack posts about ASP.NET
    MVC](http://www.lostechies.com/blogs/sean_chambers/archive/2007/12/08/phil-haack-posts-about-asp-net-mvc.aspx "A criticism of the controller action attribute")

This post is not going to rehash these concerns nor address these
concerns. If I think it would help, perhaps I will have a follow-up post
in which I explain some of the reasoning behind this decision. This
would give those who feel all *that strongly* about this a chance to
make a well-reasoned point by point refutation should they wish.

I worry about focusing too much on this *one issue*. I don’t want it to
become such a hang up that it disproportionately dominates discussion
and feedback at the expense of gathering valuable feedback on other
areas of the framework. There is much more to the framework than this
one issue.

At the same time, I do understand this issue is about more than a single
attribute. It’s about applying a design philosophy centered around
*convention over configuration*and where it works well and where it
doesn’t.

Like guacamole, I hope that critics of this particular issue don’t judge
it by sight alone and give it a real honest try. See if it makes as big
a difference as you think. Maybe it does. Maybe it doesn’t. At least
you’ve *tasted it*. **Feedback based on trying it out for a while is
more valuable and potent than feedback based on just on seeing sample
code**.

Please understand that I’m not dismissing feedback based on what you
*have* seen. It certainly is valuable and much of it has been
incorporated and discussed in our design meetings. Some of it has led to
changes. All I am saying is that as valuable as that feedback is,
feedback based on usage is even *more* valuable.

### The Convention Controller

However, if you still hate it, I have a little workaround for you :).
I’ve written a custom controller base class that allows for a more
“convention over configuration” approach to building your controllers
named `ConventionController`.

By inheriting from this controller instead of the vanilla `Controller`
class, you no longer need to add the [ControllerAction] attribute to
every public method. You also don’t need to call `RenderView` if you
name your view the same name as the action. Of course this means you
cannot use strongly typed views and must use the `ViewData` property bag
to pass data to the view.

So instead of writing your controller like this:

```csharp
public class HomeController : Controller
{
  [ControllerAction]
  public void Index()
  {
    //Your action logic
    RenderView("Index");
  }
}
```

Using my class you could write it like this

```csharp
public class HomeController : ConventionController
{
  public void Index()
  {
    //Your action logic
  }
}
```

The key point in posting this code is to demonstrate how *easy* it is to
override the behavior we bake in with something more to your liking.
When you look at the code, you will see it wasn’t rocket science to do
what I did. Extending the framework is quite easy.

We may not provide the *exact* out of box experience that you want, but
we do try and give you the tools to control your own destiny with this
framework and provide you with the power of *choice*.

I will look into adding this to the [MVC Contrib
project](http://www.codeplex.com/MVCContrib "MVC Contrib") started by
some community members. In the meanwhile, if you like this approach or
style of building controllers, you can either add the
*ConventionController.cs* class to your own project or compile it into a
separate assembly and drop it into your own projects.

To get to this class, [**download the *example*
code**](https://haacked.com/code/ConventionControllerDemo.zip "ConventionController Demo").

