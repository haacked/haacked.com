---
title: Contributors
permalink: /contributors/
include_nav: true
---

These lovely people have contributed a fix to my blog. If you want to see yourself in this list, send me a pull request!

Every post in my blog has an edit link that lets you edit the blog post directly in the browser and automatically sends me a pull request.

Or [visit my repository]({{site.github.repository_url}}) and send me a pull
request the old fashioned way.

<ul class="contributor-list">
{% if site.github.contributors.length > 0 %}

{% for contributor in site.github.contributors %}
  <li>
    <img src="{{ contributor.avatar_url }}" /> <a href="{{ contributor.html_url }}">{{ contributor.login }}</a>
  </li>
{% endfor %}

{% else %}
  <li>
    <img src="{{ site.avatar_url }}" /><a href="#">Nobody Yet</a>
  </li>
{% endif %}
</ul>
