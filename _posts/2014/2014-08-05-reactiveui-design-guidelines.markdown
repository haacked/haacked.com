---
layout: post
title: "ReactiveUI Design Guidelines"
date: 2014-08-05 -0800
comments: true
categories: [rx rxui akavache ghfw]
---

[GitHub for Windows](https://windows.github.com/) (often abbreviated to GHfW) is a client WPF application written in C#. I think it's beautiful.

![](https://cloud.githubusercontent.com/assets/634063/3167558/433d35cc-eb70-11e3-9d50-5dc4c1abc9a6.png)

This is a credit to our designers. I'm pretty sure if I had to design it, it would look like

![wgetgui-screenshot](https://cloud.githubusercontent.com/assets/19977/3814709/ad5fd3d2-1cbd-11e4-9fe1-3e71f71c7f02.png)

To keep our code maintainable and testable, we employ the [Model-View-ViewModel pattern](http://en.wikipedia.org/wiki/Model_View_ViewModel) (MVVM). To keep our app responsive, we use Reactive Extensions (Rx) to help us make sense of all the asynchronous code.

[ReactiveUI](http://www.reactiveui.net/) (RxUI) combines the MVVM pattern with Reactive Extensions to provide a powerful framework for building client and mobile applications. The creator of RxUI, [Paul Betts](http://log.paulbetts.org/), suffers through the work to test it on a huge array of platforms so you don't have to. Seriously, despite all his other vices, this cross-platform support alone makes this guy deserve sainthood. And I don't just say that because I work with him.

It can be tough to wrap your head around Reactive Extensions, and by extension ReactiveUI, when you start out. As with any new technology, there are some pitfalls you fall into as you learn. Over time, we've learned some hard lessons by failing over and over again as we build GHfW. All those failures are interspersed with an occasional nugget of success where we learn a better approach.

Much of this knowledge is tribal in nature. We tell stories to each other to remind each other of what to do and what not to do. However, that's fragile and doesn't help anyone else.

So we're making a more concerted attempt to record that tribal knowledge so others can benefit from what we learn and we can benefit from what others learned. To that end, we've made our [ReactiveUI Design Guidelines](https://github.com/reactiveui/rxui-design-guidelines) public.

It's a bit sparse, but we hope to build on it over time as we learn and improve. If you use ReactiveUI, I hope you find it useful as well.

Also, if you use Akavache, we have an even [sparser design guideline](https://github.com/akavache/akavache-design-guidelines). Our next step is to add a WPF specific guideline soon.
