---
layout: post
title: "ReactiveUI Design Guidelines"
date: 2014-08-4 -0800
comments: true
categories: [rx rxui akavache ghfw]
---

[GitHub for Windows](https://windows.github.com/) (often abbreviated to GHfW) is a client WPF application written in C#. To keep our code maintainable and testable, we employ the Model-View-ViewModel pattern (MVVM). To keep our app responsive, we use Reactive Extensions (Rx) to help us make sense of all the asynchronous code.

[Reactive UI](http://www.reactiveui.net/) (RxUI) combines the MVVM pattern with Reactive Extensions to provide a powerful framework for building client and mobile applications. The creator of RxUI, [Paul Betts](http://log.paulbetts.org/), suffers through testing it on a huge array of platforms so you don't have to. Seriously, despite all his other vices, this cross-platform support alone makes this guy deserve sainthood. And I'm not just saying that because I work with him.

Initially, wrapping your head around Reactive Extensions, and by extension, Reactive UI can be tough. As with any new technology, there are some pitfalls you fall into as you learn. In building GitHub for Windows, we've certainly learned some hard lessons by failing over and over again. All those failures are interspersed with an occasional nugget of success where we learn a better approach.

Much of this knowledge is tribal in nature. We tell stories to each other to remind each other of what to do and what not to do. However, that's really fragile and doesn't help anyone else.

So we're making a more concerted attempt to record that tribal knowledge so others can benefit from what we learn and we can benefit from what others learned. To that end, we've made our [Reactive UI Design Guidelines](https://github.com/reactiveui/rxui-design-guidelines) public.

It's a bit sparse, but we hope to build on it over time as we learn and improve. If you use ReactiveUI, I hope you find it useful as well.

Also, if you use Akavache, we have an even [sparser design guideline](https://github.com/akavache/akavache-design-guidelines). Our next step is to add a WPF specific guideline soon.