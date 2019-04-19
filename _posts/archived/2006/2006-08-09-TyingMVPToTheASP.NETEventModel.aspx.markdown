---
title: Tying MVP To the ASP.NET Event Model
date: 2006-08-09 -0800
tags: [patterns,aspnet]
redirect_from: "/archive/2006/08/08/TyingMVPToTheASP.NETEventModel.aspx/"
---

I knew this question would come up, so I figure I would address it in
its own blog post. [Mike](http://geekswithblogs.net/opiesblog "Mike")
asks a great question about my [MVP
implementation](https://haacked.com/archive/2006/08/09/ASP.NETSupervisingControllerModelViewPresenterFromSchematicToUnitTestsToCode.aspx "Model View Presenter")
(actually he asks two).

> One observation...don't you seem to be tying the presenter to the
> ASP.NET event model? If not, can you use the same presenter for a
> WinForms app?

The answer is that I am **absolutely** tying my presenter to ASP.NET.

Why?

Well when I first working on the article, I planned on creating an
abstracted `IView` and presenter that would work for both ASP.NET and
Windows Forms, but ran into a few problems. The biggest problem is that
I rarely have to write a Windows Forms applications. In fact, I almost
never do. So why spend all this time on something I won’t need? I had to
call
[YAGNI](http://en.wikipedia.org/wiki/You_Ain't_Gonna_Need_It "You Ain't Gonna Need It")
on my efforts.

## Premature Generalization

Besides, I didn’t want to run afoul of Eric Gunnerson’s \#1 deadly sin
of programming, [premature
generalization](http://blogs.msdn.com/ericgu/archive/2006/08/03/687962.aspx "Premature Generalization").
There is no point in writing an IView and Presenter to work with both
winforms and ASP.NET unless I am also implementing concrete instances of
both at the same time. Otherwise I will write it for one platform and
hope it will work for the other. If I ever do implement it for the
other, I will probably have to rewrite it anyways.

## Parity is a rarity

Secondly, even if I did need it, there are some other issues to deal
with. First, trying to write a single presenter for both ASP.NET and a
WinForms app assumes the user interaction with the application and the
view is going to be roughly the same. That is rarely the case. If I have
to go to the trouble to write a Winforms app, I will certainly take
advantage of its UI benefits.

## Leaky Abstractions Rear Their Head

Thirdly, despite all the hoops that ASP.NET jumps through to abstract
the fact that it is a web application and present an API that feels like
a desktop platform, it is **still a web application platform**. The
[abstraction is
leaky](http://www.joelonsoftware.com/articles/LeakyAbstractions.html "The Law of Leaky Abstractions")
and trying to abstract it even more causes problems.

For example, in a Winforms view, you only need to call the `Initialize`
method once because the data is persistent in memory. With an ASP.NET
view by default, you have to essentially repopulate every data field
every time a user clicks a button. Can you imagine a Winforms app
written like that?

Of course you could more closely simulate the Winforms view in ASP.NET
view by storing these fields in `ViewState` or, shudder, `Session`, but
this then becomes a constraint on your ASP.NET view in order to support
this pattern, forcing you to take a Winforms approach to a web based
app. Ideally a presenter for an ASP.NET application should not have to
assume that the ASP.NET view is going to store fields in a persistent
manner.

## Conclusion

So that is a long-winded answer to a short question. I believe if I had
to, I could get the same Presenter to work for both a Winforms App and
an ASP.NET app. These problems I mention are not insurmountable.
However, I would need to be properly motivated to do so, i.e., have a
real hard requirement to do so.

