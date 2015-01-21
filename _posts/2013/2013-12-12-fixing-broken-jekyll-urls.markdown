---
layout: post
title: "Fixing Broken Jekyll URLs"
date: 2013-12-12 -0800
comments: true
categories: [jekyll]
---
Well this is a bit embarrassing.

I recently [migrated my blog to Jekyll](http://haacked.com/archive/2013/12/02/dr-jekyll-and-mr-haack/) and subsequently wrote about my painstaking work to [preserve my URLs](http://haacked.com/archive/2013/12/03/jekyll-url-extensions/).

But after the migration, despite all my efforts, I faced an onslaught of reports of broken URLs. So what happened?

![Broken glass by Tiago Pádua CC-BY-2.0](https://f.cloud.github.com/assets/19977/1738578/074c5ca8-6387-11e3-9f3b-471445eaa5e1.jpg)

Well it's silly. The [program I wrote to migrate my posts to Jekyll](https://github.com/Haacked/subtext-jekyll-exporter) had a subtle flaw. In order to verify that my URL would be correct, it made a web request to my old blog (which was still up at the time) using the generated file name.

This was how I verified that the Jekyll URL would be correct. The problem is that Subtext had this stupid feature where the date part of the URL didn't matter so much. It only cared about the slug at the end of the URL.

Thus requests for the following two URLs would receive the same content:

* `http://haacked.com/archive/0001/01/01/some-post.aspx`
* `http://haacked.com/archive/2013/11/21/some-post.aspx`

![Picard Face Palm](https://f.cloud.github.com/assets/19977/1738673/ebae7ec0-6388-11e3-8736-a4243298a963.jpg)

This "feature" masked a timezone bug in my exporter that was causing many posts to generate the wrong date. Unfortunately, my export script had no idea these were bad URLs.

## Fixing it!

So how'd I fix it? First, I updated my 404 page with information about the problem and where to report the missing file. You can set a 404 page by adding a `404.html` file at the root of your Jekyll repository. GitHub pages will serve this file in the case of a 404 error.

I then panicked and started fixing errors by hand until my helpful colleagues [Ben Balter](http://ben.balter.com/) and [Joel Glovier](http://joelglovier.com/) reminded me to try Google Analytics and [Google Webmaster Tools](https://www.google.com/webmasters/tools/home?hl=en).

If you haven't set up Google Webmaster Tools for your website, you really should. There are some great tools in there including the ability to export a CSV file containing 404 errors.

So I did that and wrote a new program, [Jekyll URL Fixer](https://github.com/Haacked/jekyll-url-fixer), to examine the 404s and look for the corresponding Jekyll post files. I then renamed the affected files and updated the YAML front matter with the correct date.

Hopefully this fixes most of my bad URLs. Of course, if anyone linked to the broken URL in the interim, they're kind of hosed in that regard.

I apologize for the inconvenience if you couldn't find the content you were looking for and am happy to refund anyone's subscription fees to Haacked.com (_up to a maximum of $0.00 per person_).   
