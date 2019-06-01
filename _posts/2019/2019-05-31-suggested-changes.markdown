---
title: "Suggesting Changes on GitHub"
description: "A little known but very useful feature on GitHub that provides a lightweight way to suggest a small change."
tags: [github,oss,tip]
excerpt_image: https://user-images.githubusercontent.com/19977/58752991-f39d0880-846c-11e9-8c03-c7aded86ee9b.png 
---
When you see a small bug or error in a repository, a common refrain is to submit a pull request to fix it. To submit a pull request with a correction is an act of kindness to the maintainers. It allows them to easily review the change and accept it with a click.

But it's a bit of a heavyweight operation for you. Chances are, you don't have write access to the repository. So you have to fork the repository over to your account where it will pollute your list of repositories forever (or until you delete your fork). Then you have to edit the file and create a pull request. And all you wanted to do was correct "their" to "there."

You might decide to forget all that and just specify the change in a comment on the repository. But now that puts the work on the maintainer to review your comment and redo the change you specified. Chances are, it's too much trouble and they don't get around to it.

Wouldn't it be nice if you could suggest the exact change you want in a comment and the maintainer could accept your change with a click? Well good news, there is! The Suggested Changes feature has [been around since October 2018](https://github.blog/changelog/2018-10-16-suggested-changes/), but it's still not that widely known as I learned recently.

When reviewing the changes in a pull request, click to add a comment on the line that you want to suggest a change for. It's the blue box with the plus sign you see near the gutter on the left when hovering over the line. This brings up the normal line comment form. But there's a button there with a plus and minus sign. Click that, and it will add a suggestion block prepopulated with the existing text as shown in the following screenshot.

![View of the comment form with the suggested change button](https://user-images.githubusercontent.com/19977/58752991-f39d0880-846c-11e9-8c03-c7aded86ee9b.png)

Now you can make changes to the text inside the suggestion box. Note that you can add context for your suggested changes outside of the suggestion block. When you create the comment, it will show up to the maintainer as a diff.

![View of the rendered diff comment](https://user-images.githubusercontent.com/19977/58753039-b1c09200-846d-11e9-901e-699daa736bc5.png)

Now the maintainer can see what changes you are suggesting and accept them with a click. Technically it's two clicks, but let's not quibble over clicks.

One nice aspect of suggesting changes in this way is if the maintainer accepts your change, you're listed as a [co-committer via the `Co-authored-by:` footer](https://help.github.com/en/articles/creating-a-commit-with-multiple-authors) in the commit message. You'll see your avatar in the commit history on GitHub.

Another nice aspect of this feature is you can apply it retroactively on a comment. For example, suppose someone adds a normal comment with the suggested change. As a maintainer, you can edit their comment and add the suggestion block around the part of their comment that contains the suggested fix. Then you can accept it so that they receive credit for the change.

Suggested changes is extremely useful for small fixes. I've found it very useful when collaborating on markdown documents in a repository with others.

If you found this tip useful, there's many more like it in the [GitHub for Dummies book](https://amzn.to/2Qr31t1) that I and [my co-author Sarah Guthals wrote](https://haacked.com/archive/2019/05/30/github-for-dummies/)! I'll be blogging some more tips that are in the book.