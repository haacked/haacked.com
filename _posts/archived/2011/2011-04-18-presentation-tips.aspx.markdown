---
title: Presentation Tips Learned From My (Many) Mistakes
tags: [tips,speaking,conferences]
redirect_from: "/archive/2011/04/17/presentation-tips.aspx/"
---

One aspect of my job that I love is being able to go in front of other
developers, my peers, and give presentations on the technologies that my
team and I build. I’m very fortunate to be able to do so, especially
given the intense stage fright I used to have.

![phil-mvc-talk](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Presentation-Tips-Learned-From-My-Mistak_D766/phil-mvc-talk_3.png "phil-mvc-talk")

But over time, through giving multiple presentations, the stage fright
has subsided to mere abject horror levels. Even so, I’m still nowhere
near the numbers of much more polished and experienced speakers such as
my cohort, [Scott
Hanselman](http://hanselman.com/ "Scott Hanselman's Blog").

Always looking for [the silver
lining](http://en.wikipedia.org/wiki/Silver_lining_(idiom) "Silver lining on Wikipedia"),
I’ve found that my lack of raw talent in this area has one great
benefit, I make a lot of mistakes. A crap ton of them. But as Byron
Pulsifer says, every mistake is a an “opportunity to learn”, which means
I’m still cramming for that final exam.

At this [past Mix
11](https://haacked.com/archive/2011/04/16/a-look-back-at-mix-11.aspx "A look back at Mix 11"),
I made several ~~mistakes~~learning opportunities in [my first
talk](http://channel9.msdn.com/events/MIX/MIX11/FRM03 "ASP.NET MVC 3 @:The Time Is Now")
that I was able to capitalize on by the time [my second talk came
around](http://channel9.msdn.com/events/MIX/MIX11/FRM09 "NuGet in Depth").

I thought it might be helpful for my future self (and perhaps other
budding presenters) if I jotted down some of the common mistakes I’ve
made and how I attempt to mitigate them.

### Have a Backup For Everything!

An alternative title for this point could be *worry more*! I tend to be
a complete optimist when it comes to preparing for a talk. I assume
things will just work and it’ll generally work itself out and this
attitude drives Mr. Hanselman crazy when we give a talk together. This
attitude is also a setup for disaster when it comes to giving a talk.

During my talk, there were several occasions where I fat-fingered the
code I was attempting to write on stage in front of a live audience. For
most of my demos, I had snippets prepared in advance. But there were a
couple of cases where I thought the code was simple enough that I could
hack it out live.

Bad mistake!

You never know when nervousness combined with navigating a method that
takes a Func-y lambda expression of a generic type can get you so lost
in angle brackets you think you’re writing XML. I had to delete the
method I was writing and start from scratch because I didn’t create a
snippet for it, which was my backup for other code samples. This did not
create a smooth experience for people attending the talk.

Another example of having a backup in place is to always have a finished
version of your demo you can switch to and explain in case things get
out of control with your fat fingers.

For every demo you give, think about how it could go wrong and what your
backup plan will be when it does go wrong.

### Minimize Dependencies Not Under Your Control

In my ASP.NET MVC 3 talk at Mix, I tried to publish a web application to
the web that I had built during the session. This was meant to be the
finale for the talk and would allow the attendees to visit the site and
give it a spin.

It’s a risky move for sure, made all the more risky in that I was
publishing over a wireless network that could be a bit creaky at times.

Prior to the talk, I successfully published multiple times in
preparation. But I hadn’t set up a backup site (see previous mistake).
Sure enough, when the time came to do it live with a room full of people
watching, the publishing failed. The network *inside* the room was
different than the one *outside* the room.

If I had a backup in place, I could have apologized for the failure and
sent the attendees to visit the backup site in order for them to play
with the finished demo. Instead, I sat there, mouth agape, promising
attendees that it worked just before the talk. I swear!

Your audience will forgive the occasional demo failure that’s not in
your control as long as the failure doesn’t distract from the overall
flow of the presentation too much and as long as you can continue and
still drive home the point you were trying to make.

### Mock Your Dependencies

This tip is closely related to and follows up on the last tip. While at
Mix, I learned how big keynotes, such as the one at Mix, are produced.
These folks are Paranoid with a capital “P”! I listened intently to them
about the level of fail safes they put in place for a conference
keynote.

For example, they often re-create local instances of all aspects of the
Internet and networking they might need on their machine through the use
of local web servers, HOST files, local fake instances of web services,
etc.

Not only that, but there is typically a backup person shadowing what the
presenter is doing on another machine. But this person is following
along the demo script carefully. If something goes wrong with the
presenter’s demo, they are able to switch a KVM script so that the main
presenter is now displaying and controlling the backup machine, while
the shadow presenter now has the presenter’s original machine and can
hopefully fix it and continue shadowing. **Update:**[Scott
Hanselman](http://hanselman.com/blog/ "Scott's Blog") posted [a video of
behind-the-scenes footage from
Mix11](http://channel9.msdn.com/posts/Hanselminutes-on-9-Raw-Backstage-footage-before-the-Mix11-keynote-with-Jonathan-Carter "Behind the scenes mix11")
where he and [Jonathan
Carter](http://lostintangent.com/ "Jonathan's Blog") discuss keynote
preparations and how the mirroring works.

It’s generally a single get-out-of jail free card for a keynote
presenter.

I’m not suggesting you go that far for a standard break-out session. But
faking some of your tricky dependencies (and having backups) is a very
smart option.

### Sometimes, a little smoke and mirrors is a good backup

In our following NuGet talk the next day, Scott and I prepared a demo in
which I would create a website to serve up NuGet packages, and he would
going visit the site to install a package.

We realized that publishing the site on stage was too risky and was
tangential to the point of our talk, so we did something very simple. I
created the site online in advance at a known location,
[http://nuget.haacked.com/](http://nuget.haacked.com/). This site would
be an exact duplicate of the one I would create on stage.

During the presentation, I built the site on my local machine and
casually mentioned that I had made the site available to him at that
URL. We switched to his computer, he added that URL to his list of
package sources, and installed the package.

The point here is that while we didn’t technically lie, we also didn’t
tell the full story because it wasn’t relevant to our demo. A few people
asked me afterwards how we did that, and this is how.

I would advise against using smoke and mirrors for your primary demo
though! Your audience is very smart and they probably wouldn’t like it
the key technology you’re demoing is fake.

### Prepare and Practice, Practice, Practice

This goes without saying, but is sometimes easier said than done. I
highly recommend at least one end-to-end walkthrough of your talk and
practice each demo multiple times.

Personally, I don’t try to memorize or plan out exactly what I will say
in between demos (except for the first few minutes of the talk). But I
do think it’s important to memorize and nail the demos and have a rough
idea of the key points that I plan to say in between demos.

The following screenshot depicts a page of chicken scratch from Scott
Hanselman’s notebook where we planned out the general outline of our
talk.

[![IMG\_1165](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Presentation-Tips-Learned-From-My-Mistak_D766/IMG_1165_thumb.jpg "IMG_1165")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Presentation-Tips-Learned-From-My-Mistak_D766/IMG_1165.jpg)

I took these notes, typed them up into an orderly outline, and printed
out a simple script that we referred to during the talk to make sure we
were on the right pace. Scott also makes a point to mark certain
milestones in the outline. For example, we knew that around the 45
minute mark, we had better be at the *AddMvcToWebForms* demo or we were
falling behind.

Writing the script is my way of preparing as I end up doing the demos
multiple times each when writing the script. But that’s definitely not
enough.

For my first talk, I never had the opportunity to do a full dry-run. I
can make a lot of excuses about being busy leading up to the conference,
but in truth, there is no excuse for not practicing the talk end to end
at least once.

When you do a dry run, you’ll find so many issues you’ll want to
streamline or fix for the actual talk. Trust me, it’s a lot better to
find them during a practice run than during a live talk.

### Don’t change anything before the talk

Around the Around 24:40 mark in our joint [NuGet in Depth
session](http://channel9.msdn.com/events/MIX/MIX11/FRM09 "NuGet in Depth"),
you can see me searching for a menu option in the Solution Explorer. I’m
looking for the “Open CMD Prompt Here” menu, but I can’t find it.

It turns out, this is a feature of the [Power Commands for Visual Studio
2010](http://visualstudiogallery.msdn.microsoft.com/e5f41ad9-4edc-4912-bca3-91147db95b99 "VS 2010")
VSIX extension. An extension I had just uninstalled on the suggestion
from my speaking partner, Mr. Hanselman. Just prior to our talk, he
suggested I disable some Visual Studio extensions to “clean things up”

I had practiced my demos with that extension enabled so it threw me off
a bit during the talk (*Well played Mr. Hanselman!*). The point of this
story is you should practice your demo in the same state as you plan to
give the demo and don’t change a single thing with your machine before
giving the actual talk.

I know it’s tempting to install that last Window Update just before a
talk because it keeps annoying you with its prompting and what could go
wrong, right? But resist that temptation. Wait till after your talk to
make changes to your machine.

### Conclusion

This post isn’t meant to be an exhaustive list of presentation tips.
These are merely tips I learned recently based on mistakes I’ve made
that I hope and plan to never repeat.

For more great tips, check out [Scott Hanselman’s Tips for a Successful
MSFT
Presentation](http://www.hanselman.com/blog/content/radiostories/2003/01/22/scotthanselmanstipsforasuccessfulmsftpresentation.html "Technical Presentation")
and Venkatarangan’s [Tips for doing effective
Presentations](http://www.venkatarangan.com/blog/PermaLink.aspx?guid=dab57735-2976-40d7-a5d0-2e641ddea515 "Tips for doing effective presentations").

