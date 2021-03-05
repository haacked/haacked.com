---
title: Flickr Does Search Well With Clustering
tags: [tech]
redirect_from: "/archive/2005/11/14/flickr-does-search-well-with-clustering.aspx/"
---

![Rock](https://haacked.com/assets/images/rock.jpg) Recently, two posts have
given me an increased appreciation for what [Flickr](http://flickr.com/)
has accomplished with their clustering feature.

In his post [Random Acts of Sensless
Tagging](http://donxml.com/allthingstechie/archive/2005/11/14/2272.aspx),
DonXML talks about the weakness of simple tagging schemes to understand
the semantics of a tag. Today, [Jeff
Atwood](http://www.codinghorror.com/blog/archives/000445.html) follows
up this thought by illustrating how a Google search for a single word
also returns results that ignore other possible meanings of the word. He
presents eBay as a better example with its “quasi-hierarchical” category
results in the side bar.

Jeff even suggests a better approach using Markov chain probabilities to
automatically suggest alternat semantics. For example, you search on
“Jaguar” and in the search results you get a suggestion, “Did you mean:
Jaguar cat, Jaquar Automobile, OSX Jaguar...”

Well this is exactly what Flickr does when searching for tags. Try
searching for the tag
“[rock](http://www.flickr.com/photos/tags/rock/clusters/)”. Flickr
returns a set of clusters around the term. In the top cluster (at the
time of this writing), there are pictures of bands music bands and
guitarists. The other clusters involve stones, ocean, beaches as one
might expect. The last one interestingly enough associates “rock” with
“hard” and “cafe”.

