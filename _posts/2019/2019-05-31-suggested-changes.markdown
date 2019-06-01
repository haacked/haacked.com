---
title: "Suggesting Changes on GitHub"
description: ""
tags: [github,oss]
excerpt_image: 
---

Key points.

1. Sometimes it's faster to make the change yourself than to describe the change you want.
2. You might not have access to make the change directly. In that case, you have to fork the entire repository.
3. You can describe the exact change in text, but now they have to properly cut and paste it into the right place. Hopefully they don't make a mistake. Especially if whitespace is important (looking at you Pytho).
4. Wouldn't it be nice if you could suggest the exact change you want and they could resolve it with a click?
5. Good news, there is!

When you see a small bug or error in a repository, a common refrain is to submit a pull request to fix it. To submit a pull request with a correction is an act of kindness to the maintainers. It allows them to easily review the change and accept it with a click.

But it's a bit of a heavyweight operation for you. Chances are, you don't have write access to the repository. So you have to fork the repository over to your account where it will pollute your list of repositories forever (or until you delete your fork). Then you have to edit the file and create a pull request. And all you wanted to do was correct "their" to "there."

You might decide to forget all that and just specify the change in a comment on the repository. But now that puts the work on the maintainer to review your comment and redo the change you specified. Chances are, it's too much trouble and they don't get around to it.

Wouldn't it be nice if you could suggest the exact change you want in a comment and the maintainer could accept your change with a click? Well good news, there is! The Suggested Changes feature has [been around since October 2018](https://github.blog/changelog/2018-10-16-suggested-changes/), but it's still not that widely known as I learned recently.

