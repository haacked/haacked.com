Microsoft made some remarkable announcements around the next generation of ASP.NET this week.

In summary,

* ASP.NET vNext builds on NuGet as unit of reference instead of assemblies.
* Roslyn-based runtime hackable compilation model.
* Dependency Injection from the ground up.
* No Strong-Naming! (See [this discussion](https://github.com/octokit/octokit.net/issues/405) for the headache strong-naming has been)

But most exciting to me is that all of this is open source, accepts contrtibutions, and [hosted on GitHub](https://github.com/aspnet).

In some ways, it feels very sudden, but efforts like this take a long time to reach fruition which makes the result all the sweeter.

## The Baby Steps Era

Several years ago, when I worked at Microsoft, folks like Scott Hanselman and I would often use the "Baby Steps" metaphor in describing our nascent efforts to make the platform more open.

Here's a snippet from a post Scott wrote five years ago when we [first released ASP.NET MVC 1.0 under a permissive open source license](http://www.hanselman.com/blog/MicrosoftASPNETMVC10IsNowOpenSourceMSPL.aspx):

> These are all baby steps, but more and more folks at The Company are starting to "get it." We won't rest until we've changed the way we do business.

I'd say the get it now.

Here's my use of the phrase in my [notes about the release a year prior](http://haacked.com/archive/2008/03/21/a-few-notes-about-the-mvc-codeplex-source-code-release.aspx/).

> As I mentioned before, routing is not actually a feature of MVC which is why it is not included. It will be part of the .NET Framework and thus its source will eventually be available much like the rest of the .NET Framework source. It’d be nice to include it in CodePlex, but as I like to say, baby steps.

Well look! [Routing is now on GitHub](https://github.com/aspnet/Routing).

We would use that phrase because we believed we were headed in the right direction, but we also recognized the challenges of changing a 90,000 (back then) person company. Things would take time. We begged for patience.

But it's amazing what a leadership change at the top can do to change baby steps into full grown-up steps. Many of these efforts had their beginning long before Satya Nadella became CEO and Scott Guthrie became a President. But (and this is my speculative opinion), I believe a lot of the change was precipitated by a leadership team that knew they had to change how they did business.

## The unsung heroes

A lot of what we're learning about now is the result of years of work by many folks who don't share the same visibility to the outside world as Scott Guthrie and Scott Hanselman.

At Microsoft, there's another Scott who's been a big driver of this change, my old manager Scott Hunter.

We used to conspire in frustration about all the challenges with maintaining backwards compatibility with a framework that was over a decade old. It was stratified with layers of hacks upon hacks upon hacks.

We used to joke about wanting to burn it all down and build something new from the ground up. I half-jokingly coined the idea of calling it ASP2.NET. But it was a pipe dream at the time. Backwards compatibility and leveraging the existing platform was monarch.

I left the company around then, but Scott Hunter and others pursued the dream. Folks like David Fowler, Louis De Jardin, and Damian Edwards among others. A lot of credit goes to Hunter for driving this because the climate suddenly changed and the possibility of this becoming a reality grew by leaps and bounds.

Another set of unsung heroes are the ASP.NET MVPs and Insiders who provided feedback. I remember one meeting when the ASP.NET team very honestly asked the Insiders what they should do. They were at a cross roads. Should they continue to build upon the existing aging, but widely used, platform? Or should they make a break, risk alienating a lot of existing customers, but build something for the future?

Many Insiders spoke up and told them to take a risk. Build something great. It was a choice between working hard to decline as slowly as possible, or making a clean break and building software for the next generation of developers. These influencers deserve some praise for their part in this.

And of course, there are the folks that built the open source software that inspired new ideas for this new stack. Projects like Owin, itself inspired by Rack, which in turn was inspired by WSGI. Ruby on Rails and NodeJs inspired many .NET open source web frameworks which in turn inspired ideas for ASP.NET vNext. Every project builds and extends on ideas of the projects that came before. ASP.NET vNext is no different. These projects all deserve credit for serving as inspiration.

## What's next?

It's definitely a new Microsoft. I don't know what's next, but I hope to see more of this. I'd like to see the rest of the .NET Framework deployable as NuGet packages. I'd like to other teams at Microsoft adopt this model.
