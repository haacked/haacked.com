---
title: Single Package Version per Solution
tags: [nuget,code,oss]
redirect_from: "/archive/2011/05/01/single-package-version-per-solution.aspx/"
---

Not too long ago, I posted [a survey on my
blog](https://haacked.com/archive/2011/04/06/nuget-needs-your-input.aspx "Survey on my blog")
asking a set of questions meant to gather information that would help
the NuGet team make a decision about a rather deep change.

You can see the [results of the survey
here](http://survey.haacked.com/survey/1/results "Survey results").

If there’s one question that got to the heart of the matter, it’s this
one.

![survey-question-result](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/668154f532ae_7D5C/survey-question-result_0c4f17d9-93f0-4a5a-8cd6-43ed22abcedd.png "survey-question-result")

We’re considering a feature that would only allow a single package
version per solution. As you can see by the response to the question,
that would fit what most people need just fine, though there are a small
number of folks that might run into problems with this behavior.

One variant of this idea would allow multiple package versions if the
package doesn’t contain any assemblies (for example, a JavaScript
package like jQuery).

Thanks again for filling out the survey. We think we have a pretty good
idea of how to proceed at this point, but there’s always room for more
feedback. If you want to provide more feedback on this proposed change,
please [review the
spec](http://nuget.codeplex.com/wikipage?title=Package%20Updates%20Should%20Be%20Global "Single Package Version Spec")
here and post your thoughts in our discussion forum in [the thread
dedicated to this
change](http://nuget.codeplex.com/discussions/255734 "Single Package Version Per Solution").

The spec describes what pain point we’re trying to solve and shows a few
examples of how the behavior change would affect common scenarios, so
it’s worth taking a look at.

