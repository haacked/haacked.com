---
title: Running Open Source In A Distributed World
date: 2010-10-12 -0800
tags:
- nuget
- code
- oss
redirect_from: "/archive/2010/10/11/running-open-source-in-a-distributed-world.aspx/"
---

When it comes to running an open source project, the book [Producing
Open Source Software - How to Run a Successful Free Software
Project](http://www.amazon.com/gp/product/0596007590?ie=UTF8&tag=youvebeenhaac-20&link_code=as3&camp=211189&creative=373489&creativeASIN=0596007590)
by Karl Fogel (free [pdf
available](http://producingoss.com/producingoss.pdf)) is my bible (see
my [review and summary of the
book](https://haacked.com/archive/2006/01/16/RunningAnOpenSourceProject.aspx "Running an Open Source Project")).

The book is based on Karl Fogel’s experiences as the leader of the
Subversion project and has heavily influenced how I run the projects I’m
involved in. Lately though, I’ve noticed one problem with some of his
advice. It’s so Subversion-y.

Take a look at this snippet on Committers.

> As the only formally distinct class of people found in all open source
> projects, committers deserve special attention here. Committers are an
> unavoidable concession to discrimination in a system which is
> otherwise as non-discriminatory as possible. But “discrimination” is
> not meant as a pejorative here. The function committers perform is
> utterly necessary, and I do not think a project could succeed without
> it.

A Committer in this sense is someone who has direct commit access to the
source code repository. This makes sense in a world where your source
control is completely centralized as it would be with a Subversion
repository. But what about a world in which you’re using a completely
decentralized version control like Git or Mercurial? What does it mean
to be a “committer” when anyone can clone the repository, commit to
their local copy, and then send a pull request?

In the book, [Mercurial: The Definitive
Guide](http://hgbook.red-bean.com/read/ "Mercurial: The Definitive Guide"),
Bryan O’Sullivan discusses [different collaboration
models](http://hgbook.red-bean.com/read/collaborating-with-other-people.html "Collaboration Models").
The one the Linux kernel uses for example is such that Linus Torvalds
maintains the “master” repository and only pulls from his “trusted
lieutenants”.

At first glance, it might seem reasonable that a project could allow
anyone to send a pull request to main and thus focus the
“discrimination”, that Karl mentions, on the technical merits of each
pull request rather than the history of a person’s involvement in the
project.

One one level, that seems even more merit based egalitarian, but you
start to wonder if that is scalable. Based on the Linux kernel model, it
clearly is not scalable. As Karl points out,

> Quality control requires, well, control. There are always many people
> who feel competent to make changes to a program, and some smaller
> number who actually are. The project cannot rely on people’s own
> judgement; it must impose standards and grant commit access only to
> those who meet them.

Many projects make a distinction between who may contribute a bug fix as
opposed to who may contribute a feature. Such projects may require
anyone contributing a feature or a non-trivial bug fix to sign a
[Contributor License
Agreement](http://en.wikipedia.org/wiki/Contributor_License_Agreement "Contributor License Agreement on Wikipedia").
This agreement becomes the gate to being a contributor, which leaves me
with the question, do we go through the process of getting this
paperwork done for anyone who asks? Or do we have a bar to meet before
we even consider this?

On one hand, if someone has a great feature idea, wouldn’t it be nice if
we could just pull in their work without making them jump through hoops?
On the other hand, if we have a hundred people go through this paperwork
process, but only one actually ends up contributing anything, what a
waste of our time. I would love to hear your thoughts on this.

[NuGet](http://nuget.codeplex.com/ "NuGet Package Manager"), a package
manager project I work on is currently following the latter approach as
described in our [guide to becoming a core
contributor](http://nuget.codeplex.com/documentation?title=Becoming%20a%20Core%20Contributor "Becoming a core contributor"),
but we’re open to refinements and improvements. I should point out that
a hosted Mercurial solution does support the centralized committer model
where we provide direct commit access. It just so happens that while
some developers in the NuGet project have direct commit access, most
don’t and shouldn’t make use of it per project policy as we’re still
following a distributed model. We’re not letting the technical
abilities/limitations of our source control system or project hosting
define our collaboration model.

I know I’m late to the game when it comes to distributed source control,
but it’s really striking to me how it’s turned the concept of committers
on its head. In the centralized source control world, being a
contributor was enforced via a technical gate, either you had commit
access or you didn’t. With distributed version control it’s become more
a matter of social contract and project policies.

