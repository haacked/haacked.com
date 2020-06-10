---
title: Everything You Wanted To Know About MVC and MVP But Were Afraid To Ask
tags: [aspnet,aspnetmvc,code,patterns]
redirect_from: "/archive/2008/06/15/everything-you-wanted-to-know-about-mvc-and-mvp-but.aspx/"
---

Or, as my recent inbox tells me, you’re *not afraid* to ask. ;)

A coworker recently asked for some good resources on getting up to speed on the Model View Controller (MVC) pattern. Around the same time, I received another email talking about how people are confused around the difference between MVC and the Model View Presenter (MVP) pattern.

![mvc](https://haacked.com/images/haacked_com/WindowsLiveWriter/MVCandMVPPatternResources_71CE/mvc_3.png "mvc")No
better opportunity to apply the [DRY
principle](http://en.wikipedia.org/wiki/Don't_repeat_yourself "Don't Repeat Yourself")
by answering some of these questions with a blog post.

MVC
---

The first place to start digging into the MVC pattern is to look at the [Wikipedia entry](http://en.wikipedia.org/wiki/Model-view-controller "Model View Controller"). That’ll get you a nice brief summary of the pattern along with a list of resources.

> In MVC, the Model represents the information (the data) of the
> application and the business rules used to manipulate the data, the
> View corresponds to elements of the user interface such as text,
> checkbox items, and so forth, and the Controller manages details
> involving the communication to the model of user actions such as
> keystrokes and
> [mouse](http://en.wikipedia.org/wiki/Mouse_%28computing%29) movements.

![trygve](https://haacked.com/images/haacked_com/WindowsLiveWriter/MVCandMVPPatternResources_71CE/trygve_3.jpg "trygve")
If you’re really into it, you can go [directly to the source](http://heim.ifi.uio.no/~trygver/themes/mvc/mvc-index.html "MVC Index")
and read the original papers from [Trygve Reenskaug](http://heim.ifi.uio.no/~trygver/ "Trygve M. H. Reenkaug"),
the inventor of the pattern.

There you’ll learn from [the original paper (pdf)](http://heim.ifi.uio.no/~trygver/1979/mvc-1/1979-05-MVC.pdf "Thing Model View Editor - The Original Paper") that the initial name for the pattern was *Thing-Model-View-Editor*. The pattern was baked via a process of extracting, improving, and articulating existing command and control patterns used in the operation
of Norwegian ship yards in order to streamline and improve operations.

MVP
---

That ought to get you going with the MVC pattern. Now onto *Model View Presenter*, which was a response to the inadequacies of the MVC pattern when applied to modern component based graphical user interfaces. In modern GUI systems, GUI components themselves handle user input such as mouse movements and clicks, rather than some central controller.

MVP was popularized by Taligent in [this paper on the subject (pdf)](http://www.wildcrest.com/Potel/Portfolio/mvp.pdf "MVP: Model View Presenter"). More recently, [Martin Fowler](http://martinfowler.com/ "Martin Fowler's Site") suggested [retiring](http://martinfowler.com/eaaDev/ModelViewPresenter.html "MVP Retirement")
this pattern in favor of two variants: [Supervising Controller](http://martinfowler.com/eaaDev/SupervisingPresenter.html "Supervising Controller") and [Passive View](http://martinfowler.com/eaaDev/PassiveScreen.html "Passive View Pattern").

The Difference?
---------------

So what’s the diff between MVC and MVP? Using GitHub, here it is!

![246](https://cloud.githubusercontent.com/assets/19977/4980474/6af411ae-6900-11e4-8959-a80072db3054.png)

Sorry, bad joke but I couldn’t resist.

The two patterns are similar in that they both are concerned with separating concerns and they both contain Models and Views. Many
consider the MVP pattern to simply be a variant of the MVC pattern. The key difference is in how both patterns solve the following question: Who handles the user input?

With MVC, it’s always the controller’s responsibility to handle mouse and keyboard events. With MVP, GUI components themselves initially handle the user’s input, but delegate to the interpretation of that input to the presenter. This has often been called “[Twisting the Triad](http://aviadezra.blogspot.com/2007/07/twisting-mvp-triad-say-hello-to-mvpc.html "Twisting the Triad")”,
which refers to rotating the three elements of the MVC triangle and replacing the “C” with “P” in order to get *MVP*.

What About The Web?
-------------------

If you were playing close attention, most of these articles focus on rich client applications. Applying these patterns to the web is a very different beast because of the stateless nature of the web.

ASP.NET WebForms, for example, attempts to emulate the rich client development paradigm via the use of `ViewState`. This is why many
attempts to apply patterns to ASP.NET focus on the MVP pattern because the MVP pattern is more appropriate for a rich client application with GUI components. A while back I even tossed [my Supervising Controller sample](https://haacked.com/archive/2006/08/09/ASP.NETSupervisingControllerModelViewPresenterFromSchematicToUnitTestsToCode.aspx "Supervising Controller") into the ring. The Patterns and Practices group at Microsoft ship an
[MVP Bundle for ASP.NET](http://www.pnpguidance.net/Tag/MVPBundle.aspx "MVP Bundle").

However, many web platforms embrace the stateless nature of the web and forego attempting to simulate a state-full rich client development environment. In such systems, a tweaked MVC pattern is more applicable.

> This pattern has been adjusted for the Web for your application
> development enjoyment.

AFAIK, [Struts](http://struts.apache.org/ "Struts"), a Java web framework, is one of the first widely used web frameworks employing the MVC pattern, though most people now look at [Rails](http://betterexplained.com/articles/intermediate-rails-understanding-models-views-and-controllers/ "Rails and MVC") as being the tipping point that really brought MVC to the web and popularized the MVC pattern for web applications.

[ASP.NET MVC](http://www.asp.net/mvc/ "ASP.NET MVC Site") is a new (in development) alternative framework for ASP.NET developers that makes it easy for developers to follow the MVC pattern. This framework employs the MVC pattern rather than the MVP pattern because it does not attempt to emulate rich client development, and thus the MVC pattern is more
appropriate.

More Reading
------------

There’s a lot of good content out there that puts these patterns into historical and categorical perspective. Besides the links already mentioned in this paper, I recommend checking out…

-   [GUI Architectures](http://www.martinfowler.com/eaaDev/uiArchs.html "GUI Architectures") – Martin Fowler presents the most influential ways to organize code for a rich client system.
-   [Interactive Application Architecture Patterns](http://aspiringcraftsman.com/2007/08/25/interactive-application-architecture/ "Interactive App Patterns") – Derek Greer provides an overview of a whole slew of application patterns. **Highly recommend!**
-   [PEAA     Catalog](http://www.martinfowler.com/eaaCatalog/index.html "P of EAA") – A catalog of patterns in Fowler’s *Patterns of Enterprise Application Architecture* book. This covers more than just MVC and MVP.
-   [An Architectural View of the ASP.NET MVC Framework](http://dotnetslackers.com/articles/aspnet/AnArchitecturalViewOfTheASPNETMVCFramework.aspx "Architectural view of ASP.NET MVC") – Dino Esposito compares and contrasts ASP.NET MVC with classic ASP.NET.
