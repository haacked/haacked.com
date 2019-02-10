---
title: "Managing Risk"
description: "Managing risk should not just come at the beginning or end of a project. It should be an ongoing part of any project."
date: 2019-02-08 -0800 09:04 AM PDT
tags: [work,consulting]
excerpt_image: https://user-images.githubusercontent.com/19977/52507123-7e4bb600-2ba5-11e9-8aa4-aee681f38c10.png
---

Every project risks failure to some degree or other. There's the risk of delivering late. The risk of not being able to deliver at all. Or the risk that you do deliver in the end, but it solves the wrong problem. It's a risky business, but not the kind with [Tom Cruise lip synching in his underwear](https://www.youtube.com/watch?v=UuQZfwWyTWY). When you work on a project, it's important to be aware of and manage risk. There are several good tools for doing this.

![Driving while changing tire](https://user-images.githubusercontent.com/19977/52507123-7e4bb600-2ba5-11e9-8aa4-aee681f38c10.png)


## Postmortem (Retrospectives)

One of the most widely talked about tools is the postmortem (or retrospective for the squeamish). This term comes from the medical field. It refers to an examination of a dead body to determine the cause of death. Hopefully your software project failure isn't likely to cause any deaths, but [it's been known to happen](https://royal.pingdom.com/10-historical-software-bugs-with-extreme-consequences/).

When applied to a software project, it refers to examining a project after it's complete and determining what went well and what didn't go well. Unlike a medical postmortem, the project doesn't need to fail to run a retrospective. There's always something to learn from every project, even the successful ones.

This may not seem like a risk management tool since it happens a bit too late for the cadaver and we don't have time machines. Regardless, it is related to risk management in that it can help reduce the risk of the subsequent projects by applying lessons learned from the previous project.

When it comes to retrospectives, I highly recommend an approach as described in [Blameless PostMortems and a Just Culture](https://codeascraft.com/2012/05/22/blameless-postmortems/).

## Premortem

Another popular tool at your disposal is the [project premortem](https://hbr.org/2007/09/performing-a-project-premortem). A premortem occurs _before_ the project starts with a hypothetical thought exercise. Imagine that the project is already complete and failed. The task of your team is to generate plausible reasons that the project failed. So apart from the hypothetical nature and advanced timing of the premortem, the process is very similar to a postmortem. This is also a powerful tool in considering potential risks of a project ahead of time.

You don't need to ask Ms. Cleo to predict the future. A premortem should draw upon the lessons of previous retrospectives to help generate ideas of what could go wrong. The premortem and postmortem work well together.

## Top Ten Risk List

The risk (_sorry_) of the premortem and postmortem approaches is that a team does it once and then doesn't continue to follow up on the risks they identified during a project. This is why I am a fan of also  maintaining a top ten risk list throughout the lifecycle of a project. This is an approach I learned from the Steve McConnell book, [Rapid Development](https://amzn.to/2SBzHDx). Developers might know Steve better as the author of [Code Complete](https://amzn.to/2tbqDH8).

There's a lot of variations of this tool, but I prefer to keep it real simple. Every week, we would review the list of our top risks. While the book recommends maintaining a list of the top ten risks, the ten is arbitrary and you can adapt it something that fits your team's needs. I tend to focus on the top risks that meet a certain threshold of risk and impact and like to keep the number relatively small.

Here's the simplest possible format for a top ten risk list where the priority is based on the order in the list.

Risk                         | Plan/Mitigations      | Owner
---------------------------- | --------------------- | --------------------------
We'll miss the hard deadline | Identify scope we can cut. Identify areas where additional headcount will help and not hurt. | Diana
Client's keep changing requirements | Short iterations. Constant communication. A requirements chage process | Bruce
Data centers are running hot | Submerge them underwater | Clark
Leadership doesn't see the value of the project | Implement metrics and data to show possible monetary value of the features. Compile list of customer requests. Regular communication of progress | Carol

In my view, this provides the most essential information:

1. What is the risk?
2. What plans are in place to reduce or mitigate the risk?
3. Who will drive these plans? They might not be the one to do the mitigations, but they are responsible for making sure they get done.

Sometimes though, it helps to have a bit more detail. Just note the more details you add, the more cumbersome it is to maintain. The value in the risk list is the planning that it inspires. It's only valuable if you maintain it!

Risk | Impact | Weeks on list | Priority last week | Plan/Mitigations | Owner
---- | ------ | ------------- | ------------------ | ---------------- | -----
Contaminated water may poison our communities | Medium | 5 | 2 | Test the water and make sure the world learns about the cover up. | Erin
Aliens destroy the white house | Hmmmm... | 3 | 4 | Write a computer virus. Train fighter pilots. | Will Smith
If we stay isolated, the war outside our boundaries might spill over | Medium | 2 | 3 | Send Diana to help end the war. | Diana
Asteriod hits earth | Big | 647 Million | 1 | Build an early warning detection system. Send a team of drillers. | Bruce Willis

## Managing risk as a leadership tool

This approach is useful beyond the scope of a project. For example, when I was a director of engineering at GitHub, I would maintain a top risk list (it doesn't need to be ten) with my direct team of managers. We would meet weekly to identify risks to the health of our teams and their ability to be effective and work in a great environment. This helped us to support our people better. It also helped managers support each other. It's not easy being a manager and it helps to share our challenges.

In this context, it made more sense to not just identify risks, but to identify concerns and opportunities. For short, I summarize all these as challenges.

For example, if someone mentions they are interested in moving into management, that's a risk in the sense that if you cannot provide them a path to management, they may leave. But it's more positive to look at that as an opportunity to help someone in their career growth.

Challenges | Plan/Mitigations | Owner
------------------ | ---------------- | ------
Missandei is interested in moving into management | Provide her more leadership tasks. Connect her with management training. | Daenerys
Jon is showing signs of burnout | Bring it up in next 1:1 and look into what he needs and how we can support him best. Suggest he take a vacation? | Jeor
Arya feels disconnected and is struggling to adapt to a distributed remote team | Talk to Brienne about pairing with her more often. Consider setting up a video conf meeting just for hanging out with the team. See if the team is interested in a team on-site.| Syrio
Team doesn't understand how our work aligns with the mission and the rest of the company | Update our mission, vision, OKRs, etc. Write a weekly newsletter that covers these topics and highlights our accomplishments. | Sansa

__NOTE:__ the bond of trust between a person and their manager is sacred and important. Sometimes a challenge is deeply personal. Managers should make sure they have permission to share if they need feedback from their peers.

## Rising above the day to day

The day to day of our work can often keep us in a frenzy as we scurry from one fire to another. What I love about these tools is they provide a time and place to pull our heads out of our...predicaments...and be more proactive about preventing fires in the first place. It provides a time and place to be more mindful and proactive about our work, rather than be stuck in a reactive mode all the time.
