---
title: Is Twitter vs Jaiku a Replay of Friendster vs MySpace
tags: [tech]
redirect_from: "/archive/2007/05/19/is-twitter-vs-jaiku-a-replay-of-friendster-vs-myspace.aspx/"
---

[Jeff Atwood](http://codinghorror.com/blog/ "CodingHorror") tells me
he’s thinking of leaving [Twitter](http://twitter.com/ "Twitter") for
Jaiku. Scoble wrote about how [Leo Laporte already
left](http://scobleizer.com/2007/04/06/leo-laporte-leaves-twitter-for-jaiku/ "Leo Laporte Leaves Twitter").

Strangely enough, I’m having a [sense of deja vu](http://friendster.com/ "Friendster").

![twitter
logo](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IsTwittervsJaikuaReplayofFriendstervsMyS_129E3/twitter_thumb.png)
Twitter would do well to study the lessons learned in the history of Friendster vs MySpace. Building a Twitter clone is not rocket science. There is no huge barrier to entry. The only thing that keeps twitter on top of other services is their large user base.

As [danah boyd](http://www.zephoria.org/thoughts/ "Danah’s Blog") points out in [this essay](http://www.danah.org/papers/FriendsterMySpaceEssay.html "Friendster vs MySpace") which compares Friendster vs MySpace, **People use the social technologies that all their friends are using**. I personally am hesitant to switch, because everyone I know is on Twitter, not on some other platform.

However, too many days of showing users [this damn cat](https://haacked.com/archive/2007/05/20/how-to-build-twitter-in-one-line-of-code.aspx "My Post On Twitter") (yes, I’m a dog person) instead of their friends and it won’t be long before they leave in droves.

Atwood [is convinced](http://www.codinghorror.com/blog/archives/000838.html "Twitter: Service vs Platform") that Twitter needs to switch to a platform other than Rails. As danah points out in the essay, **it is not about technological perfection**.
Sticking to Rails because of the beauty of the code doesn’t matter to the masses. Especially when the service is always down.

I am more ambivalent on the question of whether they should leave Rails since I don’t fully understand if Rails is the bottleneck.

My experience with is that most scaling problems are the result of poorly written code, not the platform. More specifically, data access code is where I would look first. Simple mistakes like making a database call per item when loading a collection of hundreds or thousands of items can kill the scaling of a site. We recently fixed a bug like that in [Subtext](http://subtextproject.com/ "Subtext Project") when displaying hundreds of comments. It can happen to anyone.

Twitter’s CEO blogged about yesterday’s outages in a post entitled, [The Devil’s in the
Details](http://twitter.com/blog/2007/05/devils-in-details.html "The Devil is in the Details"). Indeed. In any case, I’ll stick with Twitter a little while longer. But if they don’t do something soon, I’ll see you on
[Jaiku](http://jaiku.com/ "Jaiku").
