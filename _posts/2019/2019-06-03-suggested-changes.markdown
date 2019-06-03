---
title: "Suggesting Changes on GitHub"
description: "A little known but very useful feature on GitHub that provides a lightweight way to suggest a small change."
tags: [github,oss,tip]
excerpt_image: https://user-images.githubusercontent.com/19977/58752991-f39d0880-846c-11e9-8c03-c7aded86ee9b.png 
---

When you see a small bug or error in a repository, a common refrain is to submit a pull request to fix it. To submit a pull request with a correction is an act of kindness to the maintainers. It allows the maintainers to review the change and accept it with a click.

But it's a bit of a heavyweight operation for the person submitting the fix. Chances are they don't have write access to the repository. Thus to submit a fix, the person must fork the repository to their account first. The forked repository pollutes their list of repositories (unless they delete the fork). Then they have to edit the file and create a pull request. All this effort because they wanted to correct "their" to "there."

A person might decide to forget all that and specify the change in a comment on the repository instead. But now that puts the work on the maintainer to review the comment and redo the specified change. Chances are, it's too much trouble and they don't get around to it.

It would be nice if a person could suggest the exact change in a comment. And that the maintainer could accept the change with a click or two. Well good news, there is! GitHub has a suggested Changes feature that does this. It's [been around since October 2018](https://github.blog/changelog/2018-10-16-suggested-changes/), but is still not that well-known.

To use it, go to the _Files changed_ tab of a pull request. When you hover over the line you want to fix, a blue box with a plus sign appears near the gutter on the left. Click that to display the normal line comment form.  Notice that there's a button there with a plus and minus sign. Click that button to add a suggestion. It adds a suggestion block to the comment text area with the existing text. You can see this in action with the following screenshots.

![View of the comment form with the suggested change button](https://user-images.githubusercontent.com/19977/58752991-f39d0880-846c-11e9-8c03-c7aded86ee9b.png)

Now you can make changes to the text inside the suggestion box. Note that you can add context for your suggested changes outside of the suggestion block. When you create the comment, it will show up to the maintainer as a diff.

![View of the rendered diff comment](https://user-images.githubusercontent.com/19977/58753039-b1c09200-846d-11e9-901e-699daa736bc5.png)

Now the maintainer can see what changes you are suggesting and accept them with a click. Technically, it's two clicks, but let's not quibble over clicks.

One nice aspect of suggesting changes in this way is how GitHub handles credit for the change. If the maintainer accepts the change, GitHub creates a commit with the change with the commenter as a [co-committer](https://help.github.com/en/articles/creating-a-commit-with-multiple-authors). The commenter sees their avatar in the history of the file they helped improve.

Another nice aspect of this feature is you can apply it retroactively on a comment. For example, suppose someone adds a normal comment with the suggested change. As a maintainer, you can edit their comment and add the suggestion block around the part of their comment that contains the suggested fix. Then you can accept it so that they receive credit for the change.

Suggested changes is extremely useful for small fixes. I've found it very useful when collaborating on markdown documents in a repository with others.

If you found this tip useful, there's many more like it in the [GitHub for Dummies book](https://amzn.to/2Qr31t1) that I and [my co-author Sarah Guthals wrote](https://haacked.com/archive/2019/05/30/github-for-dummies/)! I'll be blogging some more tips from the book.