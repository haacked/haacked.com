---
title: "The Bot That Helps You Merge"
description: "..."
tags: [git,semantic]
excerpt_image: ...
---

Developer tools that understand code semantics have a lot of potential. They have potential to make developers more productive while reducing some of the friction and drudgery of our craft. But it can be difficult to put these tools to use in practice. Many of them require a bit of a learning curve to use. It would be nice if we could automate the benefits of some of these tools. You can see where I'm going with this.

For example, a while back, I [wrote a post on how semantic diff and merge tools can reduce the number of merge conflicts a developer needs to resolve manually](https://haacked.com/archive/2019/06/17/semantic-merge/). The tool I featured is created by a [company](https://www.plasticscm.com/company) that retains me as an advisor. It's a great tool, but it still requires a lot of manual steps.

This is where mergedroid comes in. The concept is simple. Install mergedroid onto your GitHub repository. It then monitors pull requests. When GitHub reports that a pull request would result in a merge conflict, mergedroid goes to work. It examines the pull request and if it can resolve it semantically, it pushes a commit that does that.

How useful is this? Well that depends. On a project with a lot of contributors, it could be very useful. Consider the amount of time it takes to resolve a merge conflict. Each conflict can take seconds, minutes, or even hours depending on the complexity. If a bot could resolve just 20% of those conflicts, that saves a LOT of developer hours doing drudge work.

Codice published a repository analyzer that can tell you how effective mergedroid would be for your repository. Enter the URL for a public repository (or login with your GitHub account to try it on a private repository), and it analyzes it and sends you a report.

Here's [a report I ran](https://gmaster.io/mergedroid/analyze/report/aspnet/aspnetcore) on the [ASP.NET Core repository](https://github.com/aspnet/aspnetcore).

I'll break down each section. The first part looks at full merges that can be automated.

![Full merges that can be automated: 18.94%, 170.25 hours saved](https://user-images.githubusercontent.com/19977/61732340-40e17c00-ad32-11e9-96fb-32e540457864.png)

Nearly 20% of the merges with conflicts could be resolved automatically. And using some back of the envelope calculations, they estimate that mergedroid would save 170 hours of human developer time.

The next part of the report looks at conflicts at the file level.

![Individual file merges that can be automated: 44.79%, 352.35 hours saved](https://user-images.githubusercontent.com/19977/61732337-40e17c00-ad32-11e9-9825-9bb52940ce67.png)

Sometimes mergedroid can't automatically resolve every conflict in a merge. If it can't resolve every conflict, it can't resolve the merge. But it could in theory resolve many of the conflicts. This report shows you how many file level conflicts it could resolve for you.
