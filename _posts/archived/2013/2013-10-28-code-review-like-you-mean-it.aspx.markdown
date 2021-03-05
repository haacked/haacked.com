---
title: Code Review Like You Mean It
tags: [oss,github,code-review]
redirect_from: "/archive/2013/10/27/code-review-like-you-mean-it.aspx/"
---

If I had to pick just one feature that embodies GitHub (besides [emoji support](http://www.emoji-cheat-sheet.com/ "Emoji") of course ![](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CodeReviewLikeABoss_D074/thumbsup_thumb.png), I’d easily choose the **Pull Request** (aka PR). According to [GitHub’s help docs](https://help.github.com/articles/using-pull-requests "Pull Requests") (emphasis mine),

> Pull requests let you tell others about changes you’ve pushed to a
> GitHub repository. Once a pull request is sent, **interested parties
> can review the set of changes, discuss potential modifications, and
> even push follow-up commits if necessary**.

Some folks are confused by the name “pull request.” Just think of it as a request for the maintainer of the project to “pull” your changes into their repository.

Here’s a screenshot of a pull request for GitHub for Windows where [Paul Betts](http://log.paulbetts.org "Paul Betts blog") patiently explains why my code might result in the total economic collapse of the world economy.

[![sample code review](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CodeReviewLikeABoss_D074/-395_thumb.png "sample code review")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CodeReviewLikeABoss_D074/-395_2.png)

A co-worker code review is a good way to avoid the [Danger Zone](http://www.youtube.com/watch?v=8vuZ8jSVNUI "Danger Zone") (*slightly NSFW*).

Code review is at the heart of the GitHub collaboration model. And for good reason! There’s a rich set of research about the efficacy of code reviews.

In one of my favorite software books, [Facts and Fallacies of Software Engineering](http://www.amazon.com/gp/product/0321117425/ref=as_li_ss_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=0321117425&linkCode=as2&tag=youvebeenhaac-20 "Facts and Fallacies on Amazon.com") by Robert Glass, Fact 37 points out,

> Rigorous inspections can remove up to 90 percent of errors from a
> software product before the first test case is run.

And the best part is, that reviews are cost effective!

> Furthermore, the same studies show that the cost of inspections is
> less than the cost of the testing that would be necessary to find the
> same errors.

One of my other favorite software books, [Code Complete](http://www.amazon.com/gp/product/0735619670/ref=as_li_ss_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=0735619670&linkCode=as2&tag=youvebeenhaac-20 "Code Complete") by Steve McConnell, points out that,

> the average defect detection rate is only 25 percent for unit testing,
> 35 percent for function testing, and 45 percent for integration
> testing. In contrast, **the average effectiveness of design and code
> inspections are 55 and 60 percent**.

Note that McConnell is referring to evidence for the average effectiveness while Glass refers to evidence for the peak effectiveness.

The best part though, is that Code Review isn’t just useful for finding defects. It’s a great way to spread information about coding standards and conventions to others as well as a great teaching tool. I learn a lot when my peers review my code and I use it as an opportunity to teach others who submit PRs to my projects.

Effective Code Review
---------------------

You’ll notice that Glass and McConnell use the term “code inspection” and not review. A lot of time, when we think of code review, we think of simply looking the code up and down a bit, making a few terse comments about obvious glaring errors, and then calling it a day.

I know I’ve been guilty of this “drive-by” code review approach. It’s especially easy to do with pull requests.

But what these gentlemen refer to is a much more thorough and rigorous approach to reviewing code. I’ve found that when I do it well, a proper code review is just as intense and mentally taxing as writing code, if not more so. I usually like to take a nap afterwards.

Here are a few tips I’ve learned over the years for doing code reviews well.

## Review a reasonable amount of code at a time

This is one of the hardest tips for me to follow. When I start a review of a pull request, I am so tempted to finish it in one sitting because I’m impatient and want to get back to my own work. Also, I know that others are waiting on the review and I don’t want to hold them up.

But I try and remind myself that code review *is my work*! Also, a poorly done review is not much better than no review at all. When you realize that code reviews are important, you understand that it’s worth the extra time to do it well.

So I usually stop when I reach that point of review exhaustion and catch myself skipping over code. I just take a break, move onto something else, and return to it later. What better time to catch up on Archer episodes?!

## Focus on the code and not the author

This has more to do with the social aspect of code review than defect finding. I try to do my best to focus my comments on the code and not the ability or the mental state of the author. For example, instead of asking “What the hell were you thinking when you wrote this?!” I’ll say, “I’m unclear about what this section of code does. Would you explain it?”.

See? Instead of attacking the author, I’m focusing on the code and my understanding of it.

Of course, it’s possible to follow this advice and still be insulting, “This code makes me want to gouge my eyes out in between my fits of vomiting.” While this sentence focuses on the code and how it makes me feel, it’s still implicitly insulting to the author. Try to avoid that.

## Keep a code review checklist

A code review checklist is a really great tool for conducting an effective code review. The checklist should be a gentle reminder of
common issues in code you want to review. It shouldn’t represent the *only* things you review, but a minimal set. You should always be
engaging your brain during a review looking for things that might not be on your checklist.

I’ll be honest, as I started writing this post, I only had a mental checklist I ran through. In an effort to avoid being a hypocrite and leveling up my code review, I created a [checklist gist](https://gist.github.com/Haacked/7204241 "Code Review Checklist").

My checklist includes things like:

1.  **Ensure there are unit tests and review those first looking for
    test gaps.** Unit tests are a fantastic way to grasp how code is
    meant to be used by others and to learn what the expected behavior
    is.
2.  **Review arguments to methods.** Make sure arguments to methods make
    sense and are validated. Consider what happens with boundary conditions.
3.  **Look for null reference exceptions.** [Null references are a
    bitch](https://haacked.com/archive/2013/01/05/mitigate-the-billion-dollar-mistake-with-aspects.aspx "Mitigate the billion dollar mistake")
    and it’s worth looking out for them specifically.
4.  **Make sure naming, formatting, etc. follow our conventions and are
    consistent.**I like a codebase that’s fairly consistent so you know
    what to expect.
5.  **Disposable things are disposed.** Look for usages of resources
    that should be disposed but are not.
6.  **Security.**There is a whole threat and mitigation review process
    that falls under this bucket. I won’t go into that in this post. But do ask yourself how the code can be exploited.

I also have separate checklists for different platform specific items. For example, if I’m reviewing a WPF application, I’m looking out for cases where we might update the UI on a non-UI thread. Things like that.

## Step Through The Code

You’ll note that I don’t mention making sure the code compiles and that the tests pass. I already know this through the magic of the [commit status API](https://github.com/blog/1227-commit-status-api "Commit Status API") which is displayed on our pull requests.

[![-396](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CodeReviewLikeABoss_D074/-396_thumb.png "-396")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CodeReviewLikeABoss_D074/-396_2.png)

However, for more involved or more risky code changes, I do think it’s worthwhile to actually *try the code* and step through it in the debugger. Here, GitHub has your back with a relatively new feature that makes it easy to get the code for a specific pull request down to your machine.

If you have GitHub for Windows or GitHub for Mac installed and you scroll down to the bottom of any pull request, you’ll see a curious new button.

[![clone-pr-in-desktop](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CodeReviewLikeABoss_D074/clone-pr-in-desktop_thumb.png "clone-pr-in-desktop")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CodeReviewLikeABoss_D074/clone-pr-in-desktop_2.png)

Click on that button and we’ll clone the pull request code to your local machine so you can quickly and easily try it out.

Note that in Git parlance, this is not the original pull request branch, but reference (usually named something like `pr/42` where `42` is the pull request number) so you should treat it as a read-only branch. But you can always create a branch from that reference and push it to GitHub if you need to.

I often like to do this and run Resharper analysis on the code to highlight things like places where I might want to convert code to use a LINQ expression and things like that.

## Sign Off On It

After a few rounds of review, when the code looks good, make sure you let the author know! Praise where praise is due is an important part of code reviews.

At GitHub, when a team is satisfied with a pull request, we tend to comment it and include the ship it squirrel emoji
(`:shipit:`) ![](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CodeReviewLikeABoss_D074/shipit_thumb.png). That indicates the review is complete, everything looks good, and you are free to ship the changes and merge it to master.

Every team is different, but on the GitHub for Windows team we tend to let the author merge the code into master after someone else signs off on the pull request.

This works well when dealing with pull requests from people who also have commit access. On my open source projects, I tend to post a thumbs up reaction gif to show my immense appreciation for their contribution. I then merge it for them.

Here’s one of my favorites for a very good contributions.

![Bruce Lee gives a thumbs up](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CodeReviewLikeABoss_D074/thumbs-up-bruce-lee_thumb.gif)

Be Good To Each Other
---------------------

Many of my favorite discussions happen around code. There’s something about having working code to focus a discuss in a way that hypothetical discussions do not.

Of course, even this can [break down on occasion](https://github.com/twbs/bootstrap/issues/3057 "Semicolongate"). But for the most part, if you go into a code review with the idea of both being taught as well as teaching, good things result.
