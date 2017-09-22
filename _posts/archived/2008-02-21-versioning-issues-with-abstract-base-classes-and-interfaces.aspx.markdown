---
layout: post
title: Versioning Issues With Abstract Base Classes and Interfaces
date: 2008-02-21 -0800
comments: true
disqus_identifier: 18457
categories:
- asp.net
- code
redirect_from: "/archive/2008/02/20/versioning-issues-with-abstract-base-classes-and-interfaces.aspx/"
---

[Eilon
Lipton](http://weblogs.asp.net/leftslipper/ "Eilon Litpon's blog")
recently wrote a bit about [context objects in ASP.NET
MVC](http://weblogs.asp.net/leftslipper/archive/2008/02/18/mvc-context.aspx "MVC Context")
and in an “Oh by the way” moment, tossed out the fact that we changed
the `IHttpContext` interface to the `HttpContextBase` abstract base
class (ABC for short).

Not long after, this spurred debate among the Twitterati. *Why did you
choose an Abstract Base Class in this case?* The full detailed answer
would probably break my keyboard in length, so I thought I would try to
address it in a series of posts.

In the end, I hope to convince the critiques that the real point of
contention is about maintaining backwards compatibility, not about
choosing an abstract base class in this one instance.

### Our Constraints

All engineering problems are about optimizing for constraints. As I’ve
written before, there is [no perfect
design](https://haacked.com/archive/2005/05/31/ThereIsNoPerfectDesign.aspx "There is no perfect design"),
partly because we’re all optimizing for different constraints. The
constraints you have in your job are probably different than the
constraints that I have in my job.

For better or worse, these are the constraints my team is dealing with
in the long run. You may disagree with these constraints, so be it. We
can have that discussion later. I only ask that for the time being, you
evaluate this discussion in light of these constraints. In logical
terms, these are the premises on which my argument rests.

-   ***Avoid Breaking Changes at all costs***
-   ***Allow for future changes***

Specifically I mean breaking changes in our public API once we RTM. We
can make breaking changes while we’re in the CTP/Beta phase.

### You Can’t Change An Interface

The first problem we run into is that you cannot change an interface.

Now some might state, “*Of course you can change an interface. Watch me!
Changing an interface only means some clients will break, but I still
changed it.*”

The misunderstanding here is that after you ship an assembly with an
interface, any changes to that interface result in a new interface.
[Eric Lippert](http://blogs.msdn.com/ericlippert/ "Eric Lippert") points
this out in this old [Joel On Software forum
thread](http://discuss.fogcreek.com/dotnetquestions/default.asp?cmd=show&ixPost=290 "Interfaces vs Abstract Base Classes")...

> The key thing to understand regarding "changing interfaces" is that an
> interface is a \_type\_.  A type is logically bound to an assembly,
> and an assembly can have a strong name.
>
> This means that if you correctly version and strong-name your
> assemblies, there is no "you can’t change this interface" problem.  An
> interface updated in a new version of an assembly is a \_different\_
> interface from the old one.
>
> (This is of course yet another good reason to get in the habit of
> strong-naming assemblies.)

Thus trying to make even one tiny change to an interface violates our
first constraint. It is a breaking change. You can however add a new
virtual method to an abstract base class without breaking existing
clients of the class. Hey, it’s not pretty, but it works.

### Why Not Use An Interface And an Abstract Base Class?

Why not have a corresponding interface for every abstract base class?
This assumes that the purpose of the ABC is simply to provide the
default implementation of an interface. This isn’t always the case.
Sometimes we may want to use an ABC in the same way we use an interface
(all methods are abstract...revised versions of the class may add
virtual methods which throw a `NotImplementedException`).

The reason that having a corresponding interface doesn’t necessarily buy
us anything in terms of versioning, is that you can’t expose the
interface. Let me explain with a totally contrived example.

Suppose you have an abstract base class we’ll randomly call
`HttpContextBase`. Let’s also suppose that `HttpContextBase` implements
an `IHttpContext` interface. Now we want to expose an instance of
HttpContextBase via a property of another class, say `RequestContext`.
The question is, what is the type of that property?

Is it...

```csharp
public IHttpContext HttpContext {get; set;}
```

? Or is it...

```csharp
public HttpContextBase HttpContext {get; set;}
```

If you choose the first option, then we’re back to square one with the
versioning issue. If you choose the second option, we don’t gain much by
having the interface.

### What Is This Versioning Issue You Speak Of?

The versioning issue I speak of relates to clients of the property.
Suppose we wish to add a new method or property to IHttpContext. We’ve
effectively created a new interface and now all clients need to
recompile. Not only that, but any components you might be using that
refer to IHttpContext need to be recompiled. This can get ugly.

You could decide to add the new method to the ABC and not change the
interface. What this means is that new clients of this class need to
perform an interface check when they want to call this method every
time.

```csharp
public void SomeMethod(IHttpContext context)
{
  HttpContextBase contextAbs = context as HttpContextBase;
  if(contextAbs != null)
  {
    contextAbs.NewMethod();
  }
    
  context.Response.Write("Score!");
}
```

In the second case with the ABC, you can add the method as a virtual
method and throw `NotImplementedException`. You don’t get compile time
checking with this approach when implementing this ABC, but hey, thems
the breaks. Remember, no perfect design.

Adding this method doesn’t break older clients. Newer clients who might
need to call this method can recompile and now call this new method if
they wish. This is where we get the versioning benefits.

### So Why Not Keep Interfaces Small?

It’s not being small that makes an interface resilient to change. What
you really want is an interface that is small ***and cohesive***with
very little reason to change. This is probably the best strategy with
interfaces and versioning, but even this can run into problems. I’ll
address this in more detail in an upcoming post, but for now will
provide just one brief argument.

Many times, you want to divide a wide API surface into a group of
distinct smaller interfaces. The problem arises when a method needs
functionality of several of those interfaces. Now, a change in any one
of those interfaces would break the client. In fact, all you’ve really
done is spread out one large interface with many reasons to change into
many interfaces each with few reasons to change. Overall, it adds up to
the same thing in terms of risk of change.

### Alternative Approaches

These issues are one of the trade-offs of using statically typed
languages. One reason you don’t hear much about this in the Ruby
community, for example, is there really aren’t interfaces in Ruby,
though some have [proposed
approaches](http://blade.nagaokaut.ac.jp/cgi-bin/scat.rb/ruby/ruby-talk/60616 "Interfaces in Ruby")
to provide something similar. Dynamic typing is really great for
resilience to versioning.

One thing I̻’d love to hear more feedback from others is why, in .NET
land, are we so tied to interfaces? If the general rule of thumb is to
keep interfaces small (I’ve even heard some suggest interfaces should
only have one method), why aren’t we using delegates more instead of
interfaces? That would provide for even looser coupling than interfaces.

The proposed dynamic keyword and duck-typing features in future versions
of C\# might provide more resilience. As with dynamically typed
languages such as Ruby, the trade-off in these cases is that you forego
compile time checking for run-time checking. Personally, I think the
evidence is mounting that this may be a worthwhile tradeoff in many
cases.

### For More On This

The Framework Design Guidelines highlights the issues I covered here
well in chapter 4 (starting on page 11). You can read [chapter 4 from
here](http://www.theserverside.net/tt/articles/content/FrameworkDesign/Chapter4.pdf "Chapter 4 - Framework Design Guidelines").
In particular, I found this quote quite interesting as it is based on
the experience from other Framework developers.

> Over the course of the three versions of the .NET Framework, I have
> talked about this guideline with quite a few developers on our team.
> Many of them, including those who initially disagreed with the
> guideline, have said that they regret having shipped some API as an
> interface. I have not heard of even one case in which somebody
> regretted that they shipped a class.

Again, these guidelines are specific to Framework development (for
statically typed languages), and not to other types of software
development.

### What’s Next?

### 

If I’ve done my job well, you by now agree with the conclusions I put
forth in this post, *given the constraints* I laid out. Unless of course
there is something I missed, which I would love to hear about.

My gut feeling is that most disagreements will focus on the premise, the
constraint, of avoiding breaking changes at all costs. This is where you
might find me in some agreement. After all, before I joined Microsoft, I
wrote a blog post asking, [Is Backward Compatibility Holding Microsoft
Back?](https://haacked.com/archive/2006/10/01/Is_Backward_Compatibility_Holding_Microsoft_Back.aspx "Backwards Compatibility and Microsoft")
Now that I am on the inside, I realize the answer requires more nuance
than a simple yes or no answer. So I will touch on this topic in an
upcoming post.

**Other topics I hope to cover:**

-   On backwards compatibility and breaking changes.
-   Different criteria for choosing interfaces and abstract base
    classes.
-   Facts and Fallacies regarding small interfaces.
-   Whatever else crosses my mind.

My last word on this is to keep the feedback coming. It may well turn
out that based on experience, `HttpContextBase` should be an interface
while `HttpRequest` should remain an abstract base class. Who knows?!
Frameworks are best extracted from real applications, not simply from
guidelines. The guidelines are simply that, a guide based on past
experiences. So keep building applications on top of ASP.NET MVC and let
us know what needs improvement (and also what you like about it).

Technorati Tags:
[ASP.NET](http://technorati.com/tags/ASP.NET),[Design](http://technorati.com/tags/Design),[Framework](http://technorati.com/tags/Framework)

