---
layout: post
title: "Fixing Broken Jekyll URLs"
date: 2013-12-12 -0800
comments: true
categories: [jekyll]
---

Well this is a bit embarrassing.

I recently [migrated my blog to Jekyll](http://haacked.com/archive/2013/12/02/dr-jekyll-and-mr-haack/) and subsequently wrote about my painstaking work to [preserve my URLs](http://haacked.com/archive/2013/12/03/jekyll-url-extensions/).

But after the migration, I faced an onslaught of reports of broken URLs. So what happened?

![broken glass](https://f.cloud.github.com/assets/19977/1738578/074c5ca8-6387-11e3-9f3b-471445eaa5e1.jpg Broken glass by Tiago Pádua CC-BY-2.0)


Well it's silly. The [program I wrote to migrate my posts to Jekyll](https://github.com/Haacked/subtext-jekyll-exporter) had a subtle flaw. In order to verify that my URL would be correct, it made a web request using the generated file name to see if the resulting URL would be correct.