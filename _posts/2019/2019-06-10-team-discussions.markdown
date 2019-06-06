---
title: "Discuss amongst yourselves on GitHub"
description: "The Team Discussions feature is a great way for your team to have open ended discussions on GitHub without having to leave GitHub."
tags: [github,oss,tip]
excerpt_image: https://user-images.githubusercontent.com/19977/58995077-99bb7c00-87a7-11e9-85b6-4a57270e1c95.png
---

How do you organize the activities of a distributed group? The group might be a team within a company, an open source project, or even an open source foundation. Without the right tools, it is difficult.

I was in a group like this. We started off with email as our communication tool. Email is terrible for this. So terrible that we _weren't_ sending emails to each other and thus not making any progress.

I managed to convince the group to give [the team discussions feature on GitHub](https://help.github.com/en/articles/about-team-discussions) a try. Many of these folks were longtime GitHub users, yet they had no idea this feature existed. I don't blame them of course. The feature is only available if you're a member of an [organization on GitHub](https://help.github.com/en/articles/about-organizations).

If you're an administrator of an org, you can go to the Teams page to see the list of teams in your org. The URL is always `https://github.com/orgs/ORG-NAME/teams` (be sure to replace `ORG-NAME` with the actual organization name).

Click on a team name to navigate to the team discussion page. The format of the URL for the team discussion page is: `https://github.com/orgs/ORG-NAME/teams/TEAM-NAME`

![Discussion page with no discussions](https://user-images.githubusercontent.com/19977/58995077-99bb7c00-87a7-11e9-85b6-4a57270e1c95.png)

When you start a discussion, you can specify if it should be public to the entire org or private to the specific team.

![Starting a discussion](https://user-images.githubusercontent.com/19977/58995127-cb344780-87a7-11e9-9c0a-cd242bc2f81e.png)

## Discussions vs Issues

Team Discussions work a lot like issues. A natural question to ask is what's the difference? When should you use one over the other. There's no hard science to this, but here's the rule of thumb I use.

An issue is a well understood task that someone owns. It's clear to everyone when that task is complete. If an issue is a bug report, it's complete when someone fixes the bug. Or it's complete when the team decides they won't fix it, or it's a behavior by design. The key thing here is there's a clear resolution to issues.

Discussions are useful in all other situations where people want to discuss a topic. In contrast to issues, discussions often don't have a clear owner. There's often no clear call to action. A discussion might  be a one-way announcement, a design session, or a discussion of a hot tv show.

Often, discussions are the precursor to one or more issues. The participants of an open-ended discussion may identify specific actionable tasks. Those tasks become issues and might link back to the source discussion.

Another important distinction between issues and discussions is the scope. Issues are specific to a repository. A discussion is specific to a team and may span many repositories. A discussion may be more suitable to coordinate work across many projects than an issue.

When I ran the Client Apps team at GitHub, I would post a weekly "newsletter" via the team discussions feature. I named it the CACAW which stood for Completely Awesome Client Apps Weekly. The name gave me an excuse to highlight each letter with a crow themed image. I ran out of these in short order.

![Am I taking this crow thing too far?](https://user-images.githubusercontent.com/19977/37788571-fa91d55e-2dbe-11e8-961d-f8f13ad65b2e.jpg)

This newsletter gave me a channel to relay information important to my team. It was also a place to celebrate our successes as a team and give out kudos. And at the end of each one, I asked for feedback either by a comment on the discussion or through private channels. It was my way of keeping the team informed and aligned.

The use of team discussions helped us feel like a coherent team. Distributed teams have a tendency to feel like a collection of seeds strewn about by the wind. We used discussions to reinforce our identity as a team with a common mission. This is one way this feature cane be useful. There are many other great uses for it.
