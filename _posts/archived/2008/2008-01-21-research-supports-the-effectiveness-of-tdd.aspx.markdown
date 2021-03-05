---
title: Research Supports The Effectiveness of TDD
tags: [tdd,science,methodologies]
redirect_from: "/archive/2008/01/20/research-supports-the-effectiveness-of-tdd.aspx/"
---

In a [recent
post](http://weblogs.asp.net/fbouma/archive/2008/01/11/the-waterfall-which-makes-agile-pundits-go-blind.aspx "The Waterfall which makes agile pundits go blind"),
[Frans Bouma](http://weblogs.asp.net/fbouma/ "Frans Bouma") laments the
lack of computer science or reference to research papers by agile
pundits in various mailing lists (I bet this really applies to nearly
all mailing lists, not just the ones he mentions).

> What surprised me to no end was the total lack of any reference/debate
> about computer science research, papers etc. ... in the form of CS
> research. Almost all the debates focused on tools and their direct
> techniques, not the computer science behind them. In general asking
> ’Why’ wasn’t answered with: "research has shown that..." but with
> replies which were pointing at techniques, tools and patterns, not the
> reasoning behind these tools, techniques and patterns.

[![science-in-action](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ResearchSupportsTheEfficacyofTDD_15041/science-in-action_3.jpg)](http://www.sxc.hu/photo/419554 "Two students conducting an experiment - from stock exchange")As
a fan of the scientific method, I understand the difference between a
hypothesis and a theory/law. Experience and anecdotal evidence do not a
theory make, as anyone who’s participated in a memory experiment will
learn that memory itself is easily manipulated. At the same time though,
if a hypothesis works for you every time you’ve tried it, you start to
have confidence that the hypothesis holds weight.

So while the lack of research was a slight itch sitting there in the
back of my mind, I’ve never been overly concerned because I’ve always
felt that the efficacy of TDD would hold weight when put to the test
(get it?). It is simply a young hypothesis and it was only a matter of
time before experimentation would add evidence to bolster the many
claims I’ve made on my blog.

I’ve been having a lot of interesting discussions around TDD internally
here lately and wanted to brush up on the key points when I happened
upon [this
paper](http://iit-iti.nrc-cnrc.gc.ca/publications/nrc-47445_e.html "On the Effectiveness of Test-first Approach to Programming")
published in the Proceedings of the IEEE Transactions on Software
Engineering entitled [On the Effectiveness of Test-first Approach to
Programming](http://iit-iti.nrc-cnrc.gc.ca/publications/nrc-47445_e.html "Test First Programming").

The researchers put together an experiment in which an experiment group
and a control group implemented a set of features in an incremental
fashion. The experiment group wrote tests first, the control group
applied a more conventional approach of writing the tests after the
fact. As a result, they found...

> We found that test-first students on average wrote more tests and, in
> turn, students who wrote more tests tended to be more productive. We
> also observed that the minimum quality increased linearly with the
> number of programmer tests, independent of the development strategy
> employed.

The interesting result here is that quality is more a factor of the
number of tests you write, and not whether you write them first. TDD
just happens to be a technique in which the natural inclination (path of
least resistance) is to write more tests than less **in the same time
span**. The lesson here is even if you don’t buy TDD, you should still
be writing automated unit tests for your code.

Obviously such a controlled experiment on undergraduate students leaves
one wondering if the conclusions drawn can really be applied to the
workplace. The researches do address this question of validity...

> The external validity of the results could be limited since the
> subjects were students. Runeson [21] compared freshmen, graduate, and
> professional developers and concluded that similar improvement trends
> persisted among the three groups. Replicated experiments by Porter \
> and Votta [22] and Höst et al. [23] suggest that students may provide
> an adequate model of the professional population.

Other evidence they refer to even suggests that in the case of advance
techniques, the benefit that professional developers gain outweighs that
of students, which could bolster the evidence they present.

My favorite part of the paper is the section in which they offer their
explanations for why they believe that Test-First programming might
offer productivity benefits. I won’t dock them for using the word
*synergistic*.

> We believe that the observed productivity advantage of Test-First
> subjects is due to a number of synergistic effects:
>
> -   *Better task understanding*. Writing a test before implementing
>     the underlying functionality requires \
>     the programmer to express the functionality unambiguously.
> -   *Better task focus*. Test-First advances one test case at a time.
>     A single test case has a limited scope. Thus, the programmer is
>     engaged in a decomposition process in which larger pieces of
>     functionality are broken down to smaller, more manageable chunks.
>     While developing the functionality for a single test, the
>     cognitive load of the programmer is lower.
> -   *Faster learning*. Less productive and coarser decomposition
>     strategies are quickly abandoned in favor of more productive,
>     finer ones.
> -   *Lower rework effort*. Since the scope of a single test is
>     limited, when the test fails, rework is easier. When rework
>     immediately follows a short burst of testing and implementation
>     activity, the problem context is still fresh in the programmer’s
>     mind. With a high number of focused tests, when a test fails, the
>     root cause is more easily pinpointed. In addition, more frequent
>     regression testing shortens the feedback cycle. When new
>     functionality interferes with old functionality, this situation is
>     revealed faster. Small problems are detected before they become
>     serious and costly.
>
> Test-First also tends to increase the variation in productivity. This
> effect is attributed to the relative difficulty of the technique,
> which is supported by the subjects’ responses to the
> post-questionnaire and by the observation that higher skill subjects
> were able to achieve more significant productivity benefits.

So while I don’t expect that those who are resistant or disparaging of
TDD will suddenly change their minds on TDD, I am encouraged by this
result as it jives with my own experience. The authors do cite several
other studies (future reading material, woohoo!) that for the most part
support the benefits of TDD.

So while I’m personally convinced of the benefits of TDD and feel the
evidence thus far supports that, I do agree that the evidence is not yet
overwhelming. More research is required.

I prefer to take a provisional approach to theories, ready to change my
mind if the evidence supports it. Though in this case, I find TDD a
rather pleasant fun enjoyable method for writing code. There would have
to be a massive amount solid evidence that TDD is a colossal waste of
time for me to drop it.

