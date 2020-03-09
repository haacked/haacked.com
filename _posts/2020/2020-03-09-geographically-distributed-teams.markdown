---
title: "Geographically Distributed Teams"
description: "Some lessons learned from leading geographically distributed teams across multiple time zones."
tags: [management,leadership,remote]
excerpt_image: https://user-images.githubusercontent.com/19977/76013609-11e8d680-5ecd-11ea-9176-f0d69087a111.jpg
---

[Facebook, Microsoft, Google, and Amazon just told its Seattle area employees to work from home](https://www.npr.org/2020/03/05/812173963/coronavirus-amazon-facebook-google-microsoft-urge-seattle-workers-to-stay-home) for the next three weeks to reduce the risk of spreading the coronavirus. Lucky for them, I just wrote two posts that will help.

1. [How to work from home](https://haacked.com/archive/2020/03/03/how-to-work-from-home/)
2. [How to lead from home](https://haacked.com/archive/2020/03/05/how-to-lead-from-home/)

The tips in those posts are useful to any remote and distributed team, but they don't cove the unique issues a geographically distributed team faces. In this post, I'll turn the knob up to 11 and focus on teams where members work in multiple time-zones based on my own experience.

![The Earth at Night](https://user-images.githubusercontent.com/19977/76013609-11e8d680-5ecd-11ea-9176-f0d69087a111.jpg "Photo of my former team at work - Image by NASA is in the public domain")

## My Experience

I joined GitHub in 2011 and worked there just shy of seven years. GitHub is famous for being a remote friendly company. At the time I joined, it was around 60 to 70% remote. It was also a time when [GitHub didn't have managers](https://www.fastcompany.com/3020181/inside-githubs-super-lean-management-strategy-and-how-it-drives-innovation). It was like the wild west.

My team at the time was small team (three engineers and one designer). Three of the four of us worked from GitHub headquarters (HQ 2.0 as we referred to it).

As GitHub grew, it struggled with several big challenges. For example, one challenge was it did not ship as fast as it wanted. To address these challenges [GitHub added management](https://github.com/holman/ama/issues/800). At the time, there were several hundred fiercely independent employees, many of whom were attracted by the managerless workstyle. You sprinkle in management all of a sudden and, well, it initially went about as well as you might expect. But that's a story for another time.

Not long after that, I became the manager of the Desktop team.

![Bill Lumbergh from the Office Space Movie](https://user-images.githubusercontent.com/19977/76010448-c253dc00-5ec7-11ea-8b6b-28d5b92a4edb.png "Ummm, I'm gonna need you to go ahead come in tomorrow.")

My team worked on GitHub for Mac and Windows (GUI clients to GitHub) before the [two apps were combined into a single Electron App](https://github.blog/2017-09-19-announcing-github-desktop-1-0/). We had folks in Australia, Canada, New Zealand, Sweden, Ohio, Washington, New York, England, San Francisco, and probably some places I'm forgetting. We pretty much had a 24-hour development cycle. At the end of the day in the U.S., a developer could push changes up to the repository and go home. At the same time, the developer in Australia would start the day ready to review the code.

Some time after that, I became a director. This meant I managed up to five managers. Each manager was in charge of a distributed team. Of those five managers, only one regularly worked from HQ. My organization, Client Applications, consisted of over thirty people at its height.

We had folks in all the aforementioned places, but also Japan, Italy, Washington DC, and so on. Despite being so distributed, we worked hard to create cohesive and tight-knit teams.

## What Works For Distributed Is Good for Co-located

One theme you'll see throughout my posts on remote leadership is that these are good practices whether you are distributed or colocated. Some tips are specific to distributed teams, but most of them apply to all teams.

The reason I emphasize these good practices is the negative consequences of bad practices are amplified when a teams is remote and distributed. For example, suppose a team relies on oral tradition and is lax about writing down the plan of record. In an office environment, teams that head in the wrong direction are more likely to be noticed by a hands on manager and a course correction can be applied. For a distributed team, the wrong direction might not be noticed until it's too expensive to correct.

In both scenarios, it would be better to have the plan of record written down in a known location so the manager can spend their time doing higher value work than babysitting teams.

## Embrace Asynchronous Workflows and Communication

One of the best posts on the subject (that I mentioned in a previous post too but will mention again), is [Working Asynchronously](https://medium.com/@jspahrsummers/working-asynchronously-c4f4acd289ac) by Justin Spahr-Summers. It's no coincidence that Justin and I worked together at GitHub.

If something can be resolved in an email, chat, or online discussion, do that instead of setting up a meeting. If a regular meeting is absolutely necessary, rotate the time of the meeting to account for all the different timezones.

## One for all and all for one

If one member of a team is remote, then the entire team should behave as if it's remote. When a team has a meeting, and one member is remote, but the rest of the team is co-located in a meeting room, that creates a large imbalance in the remote person's ability to participate. Ideally everyone would join the videoconference meeting indivdually. It puts everyone on a level playing field.

## Communicate Well

In a previous post, I mentioned the importance of communicating often. I went so far as to say you should overcommunicate. But when you do, respect the the time and attention of your audience. Embrace the asynchronicity of your communication mediums. For example, avoid messages like this.

![Old CRT screen with the text, "@haacked: Hey, you around?"](https://user-images.githubusercontent.com/19977/72849475-99e78980-3c5b-11ea-8a4f-9019333c6892.png)

This is just noise. The message expects that the recepient is available to respond and continue the conversation right then. Instead, include context so the recipient can respond meaningfully on their own time without you having to be there.

![Old CRT screen with the text, "@haacked: Hey, could you clarify what meant when you said we should close issue #123?"](https://user-images.githubusercontent.com/19977/72849690-1aa68580-3c5c-11ea-8c4e-15d0a6e4c510.png)

## Give Time For Feedback

On some of the teams I worked, we had a 24 hour Pull Request rule. We would not merge a pull request for 24 hours to make sure everyone in every time zone had a chance to review it. In practice, it was more like the overnight rule. The key point of the rule was not the 24 hours but the fact that we gave enough time for everyone to provide feedback.

This ensures that you have the benefit of insightful feedback from your teammate in New Zealand. They may know something that's critical to the success of your work. Likewise, when it comes to decision making, it gives them the opportunity to be heard before the team commits to key decisions.

## Clarify Decision Making

Making decisions asynchronously can be difficult and drawn out. I gave one example [in my last post](https://haacked.com/archive/2020/03/05/how-to-lead-from-home/) of how open-ended questions can fall flat and lack engagement.

Another problem is when a discussion has plenty of engagement, but goes on and on without a resolution. Sometimes you don't reach consensus in an asynchronous discussion. Often, the default response in such a situation is to do nothing. But doing nothing is also a choice. And it may not be the right or safe choice in every situation.

It's important to solicit feedback on important decisions, but do so in a structured clear way. When soliciting feedback, make it clear up front who are the final decision makers and _when_ they will make that decision. Up until that point, everyone is free to provide input and feedback.

When you do so, I recommend that you apply a [Responsibility Assignment Matrix](https://en.wikipedia.org/wiki/Responsibility_assignment_matrix) such as [RACI](https://www.teamgantt.com/blog/raci-chart-definition-tips-and-example) or [DACI](https://www.atlassian.com/team-playbook/plays/daci) comes in real handy.

## What about the effect of time zones on throughput?

One concern many people express with geographically distributed work is that progress on a project will be slower. This is a valid concern.

In my experience, it often is the case that a single thread of work does take longer when the people working on it are in very different timezones. It's unavoidable. People need feedback on their work. If I (living in Bellevue, Washington) finish a piece of code and push it up into a PR, I'm going to have to wait till the next day if my teammate is in Australia.

So how do you collaborate effectively with people in different time zones? The truth is, it's not easy. I've worked closely with employees who were awake while I was asleep and asleep while I was awake. The answer isn't a giant pile of caffeine and work all the damn time. That's unsustainable.

The way to compensate for this is the same way computers compensate for the slow down in Moore's law. Parallelize your work. In a complex software project, there's no end of work to be done. Often, I'll hit an impasse on one thread of work. Maybe I'm stuck and I know my teammate has more expertise with this section of code. So I push the code to GitHub, add a comment describing my confusion and the problem in as much detail as possible, then move no to another thread of work.

With this approach, a single thread of work might take longer, but the overall throughput doesn't have to suffer too much. Also, it discourages too much siloing of work within a team as folks in different time zones should be able to help with any areas of discussion.

Hope these tips are helpful for those of you working with geographically distributed teams.
