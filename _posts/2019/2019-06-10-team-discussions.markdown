---
title: "Discuss amongst yourselves on GitHub"
description: "The Team Discussions feature is a great way for your team to have open ended discussions on GitHub without having to leave GitHub."
tags: [github,oss,tip]
excerpt_image: https://user-images.githubusercontent.com/19977/71785251-436b2180-2fb2-11ea-91b1-c9747700e6d4.jpg
---

When I ran the Client Apps team at GitHub, I wrote a weekly "newsletter" to the team. I named it the CACAW which stood for Completely Awesome Client Apps Weekly. The name gave me an excuse to highlight each letter with a crow themed image.

![Am I taking this crow thing too far?](https://user-images.githubusercontent.com/19977/71785251-436b2180-2fb2-11ea-91b1-c9747700e6d4.jpg)

I tried to invoke every possible pun on crows I could find. It did not take long to run out of these. The purpose of this newsletter wasn't to show off my skill at finding funny punny images.

Distributed teams have a tendency to feel like a collection of seeds strewn about by the wind. This newsletter gave me a channel to relay information important to my team. It was also a place to celebrate our successes as a team and give out kudos. And at the end of each one, I asked for feedback through public or private channels. It was my attempt to reinforce a shared identity as a team with a common mission.

I felt like the newsletter was a success, and a part of its success was that I didn't send it out as an email. I had a better tool at my disposal.

## Coordinating Distributed Teams With Discussions

How do you organize the activities of a distributed group? The group might be a team within a company, an open source project, or even an open source foundation. Without the right tools, it is difficult.

I'm in a group like this right now. We started off with email as our communication tool. Email is terrible for this. So terrible that we _weren't_ sending emails to each other and thus not making any progress.

I managed to convince the group to give [the team discussions feature on GitHub](https://help.github.com/en/articles/about-team-discussions) a try. Many of these folks were longtime GitHub users, yet they had no idea this feature existed. I don't blame them of course. The feature is only available if you're a member of an [organization on GitHub](https://help.github.com/en/articles/about-organizations).

If you're an administrator of an org, you can go to the Teams page to see the list of teams in your org. The URL is always `https://github.com/orgs/ORG-NAME/teams` (be sure to replace `ORG-NAME` with the actual organization name).

Click on a team name to navigate to the team discussion page. The format of the URL for the team discussion page is: `https://github.com/orgs/ORG-NAME/teams/TEAM-NAME`

![Discussion page with no discussions](https://user-images.githubusercontent.com/19977/58995077-99bb7c00-87a7-11e9-85b6-4a57270e1c95.png)

When you start a discussion, you can specify whether it should be public to the entire org or private to the specific team.

![Starting a discussion](https://user-images.githubusercontent.com/19977/58995127-cb344780-87a7-11e9-9c0a-cd242bc2f81e.png)

This is the feature I used for the CACAW newsletter. I posted each newsletter as a new discussion. This allowed people to provide feedback in comments. They could also follow up with me privately through other channels of course. I found this a very handy approach to team communications.

## Discussions vs Issues

Team Discussions work a lot like issues. A natural question to ask is what's the difference? When should you use one over the other? There's no hard science to this, but here's the rule of thumb I use.

An issue is a well understood task that someone owns. It's clear to everyone when that task is complete. If an issue is a bug report, it's complete when someone fixes the bug. Or it's complete when the team decides they won't fix it, or it's a behavior by design. The key thing here is there's a clear resolution to issues.

Discussions are useful in all other situations where people want to discuss a topic. In contrast to issues, discussions often don't have a clear owner. There's often no clear call to action. A discussion might  be a one-way announcement, a design session, or a discussion of a hot tv show.

Often, discussions are the precursor to one or more issues. The participants of an open-ended discussion may identify specific actionable tasks. Those tasks become issues and might link back to the source discussion.

Another important distinction between issues and discussions is the scope. Issues are specific to a repository. A discussion is specific to a team and may span many repositories. A discussion may be more suitable to coordinate work across many projects than an issue.

If you found this tip useful, there's many more like it in the [GitHub for Dummies book](https://amzn.to/2Qr31t1) that I and [my co-author Sarah Guthals wrote](https://haacked.com/archive/2019/05/30/github-for-dummies/)! I'll be blogging some more tips from the book.
