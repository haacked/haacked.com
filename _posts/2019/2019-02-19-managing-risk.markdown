---
title: "Managing Risk"
description: "Managing risk should not just come at the beginning or end of a project. It should be an ongoing part of any project. It can also be a tool for managing risks to team health, not just the project."
tags: [work,consulting,management]
excerpt_image: https://user-images.githubusercontent.com/19977/58293037-b9e74600-7d78-11e9-93b6-fe31580c8344.jpg
---

Every project risks failure to some degree or other. There's the risk of delivering late. The risk of not being able to deliver at all. Or the risk that when you do deliver, it solves the wrong problem. It's a risky business, but not the kind with [Tom Cruise lip-syncing in his underwear](https://www.youtube.com/watch?v=UuQZfwWyTWY). When you work on a project, it's important to be aware of and manage risk. There are several good tools for doing this.

![Driving while changing a tire](https://user-images.githubusercontent.com/19977/58293037-b9e74600-7d78-11e9-93b6-fe31580c8344.jpg)

## Postmortem (Retrospectives)

One well-known procedure is the postmortem (or retrospective for the squeamish). This term comes from the medical field. It refers to an examination of a dead body to determine the cause of death. The good news is most software projects are not matters of life and death. The bad news is this is not true for [all software projects](https://royal.pingdom.com/10-historical-software-bugs-with-extreme-consequences/).

With software projects, a postmortem refers to examining a project, not a cadaver. It is a process where participants examine what went well and what didn't go well at the end of a project. Unlike a medical postmortem, the project doesn't need to fail to prompt a retrospective. There's always something to learn from every project, even the successful ones.

This may not seem like a risk management tool since it happens a bit too late for the cadaver. Yet, it can reduce the risk of later projects. It reduces risk when people apply lessons from a postmortem to the next project.

For a good approach to retrospectives, read [Blameless PostMortems and a Just Culture](https://codeascraft.com/2012/05/22/blameless-postmortems/).

## Premortem

Another popular tool at your disposal is the [project premortem](https://hbr.org/2007/09/performing-a-project-premortem). A premortem occurs _before_ the project starts with a hypothetical thought exercise. Imagine that the project is already complete and failed. The task of your team is to generate plausible reasons that the project failed.

A premortem looks a lot like a postmortem. The main difference is it deals in hypotheticals and occurs before a project. This is a powerful tool to help consider the potential risks of a project ahead of time.

You don't need to ask Ms. Cleo to predict the future and generate ideas of what could go wrong. The team should draw upon the lessons of previous retrospectives. The premortem and postmortem work well together.

## Top Ten Risk List

One of the drawbacks of the premortem and postmortem is a lack of follow-up during a project. How does a team continue to consider the risks they identified as a project progresses? One way is to maintain a top ten risk list. This is an approach I learned from the Steve McConnell book, [Rapid Development](https://amzn.to/2SBzHDx). Developers might know Steve better as the author of [Code Complete](https://amzn.to/2tbqDH8).

There's a lot of variations of this tool, but I prefer to keep it real simple. Every week, we would review the list of our top risks. The book recommends ten risks, but the ten is arbitrary. Adapt it to something that fits your team's needs.

Here's the simplest possible format for a top ten risk list where the priority is based on the order in the list.

Risk                         | Plan/Mitigations      | Owner
---------------------------- | --------------------- | --------------------------
We'll miss the hard deadline | Identify scope we can cut. Identify areas where additional headcount will help and not hurt. | Diana
Clients keep changing requirements | Short iterations. Constant communication. A requirements-change process | Bruce
Data centers are running hot | Submerge them underwater | Clark
Leadership doesn't see the value of the project | Implement metrics and data to show the possible monetary value of the features. Compile a list of customer requests. Regular communication of progress | Carol

In my view, this provides the most essential information:

1. What is the risk?
2. What plans are in place to reduce or mitigate the risk?
3. Who will drive these plans? They might not be the one to do the mitigations, but they are responsible for making sure they get done.

Sometimes though, it helps to have a bit more detail. Just note the more details you add, the more cumbersome it is to maintain. The value in the risk list is the planning that it inspires. It's only valuable if you maintain it!

Risk | Impact | Weeks on list | Priority last week | Plan/Mitigations | Owner
---- | ------ | ------------- | ------------------ | ---------------- | -----
Contaminated water may poison our communities | Medium | 5 | 2 | Test the water and make sure the world learns about the cover-up. | Erin
Aliens destroy the white house | Hmmmm... | 3 | 4 | Write a computer virus. Train fighter pilots. | Will Smith
If we stay isolated, the war outside our boundaries might spill over | Medium | 2 | 3 | Send Diana to help end the war. | Diana
Asteroid hits earth | Big | 647 Million | 1 | Build an early warning detection system. Send a team of drillers. | Bruce Willis

## Managing risk as a leadership tool

This approach is useful beyond the scope of a project. For example, I used this tool as a director of Engineering at GitHub. My direct team and I met weekly to identify risks to the health of our teams. We looked for anything that might hinder their work environment or effectiveness. This helped us to support our people better. It also helped managers support each other. It's not easy being a manager and it helps to share our challenges.

In this context, it made sense to not just identify risks, but also concerns and opportunities. For brevity, I label all these things "challenges."

For example, if someone mentions they want to move into management, that might not seem like a risk. But there is the risk that if you cannot provide them with a path to management, they may leave. A more positive way to look at it is this is an opportunity to help someone in their career growth.

Challenges | Plan/Mitigations | Owner
------------------ | ---------------- | ------
Missandei is interested in moving into management | Provide her more leadership tasks. Connect her with management training. | Daenerys
Jon is showing signs of burnout | Bring it up in next 1:1 and look into what he needs and how we can support him best. Suggest that he takes a vacation? | Jeor
Arya feels disconnected and is struggling to adapt to a distributed remote team | Talk to Brienne about pairing with her more often. Consider setting up a video conf meeting just for hanging out with the team. See if the team is interested in a team on-site.| Syrio
The team doesn't understand how our work aligns with the mission and the rest of the company | Update our mission, vision, OKRs, etc. Write a weekly newsletter that covers these topics and highlights our accomplishments. | Sansa

__NOTE:__ the bond of trust between a person and their manager is sacred and important. Sometimes a challenge is deeply personal. Managers should make sure they have permission to share if they need feedback from their peers.

## Rising above the day to day

The day to day of our work can often keep us in a frenzy as we scurry from one fire to another. These tools provide a time and place to pull our heads out of our...predicaments...and be more proactive. They prompt us to prevent fires rather than spend all our time putting out fires.
