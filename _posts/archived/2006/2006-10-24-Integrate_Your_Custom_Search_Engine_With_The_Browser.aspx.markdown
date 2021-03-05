---
title: Integrate Your Custom Search Engine With The Browser
tags: [tech]
redirect_from: "/archive/2006/10/23/Integrate_Your_Custom_Search_Engine_With_The_Browser.aspx/"
---

In response to question about integrating my [custom search
engine](https://haacked.com/archive/2006/10/23/My_Very_Own_Search_Engine.aspx "Custom Search Engine")
with the browser, [Oran](http://orand.blogspot.com/ "Oran's Blog") left
a comment with a [link to a
post](http://orand.blogspot.com/2006/02/fogbugz-browser-search-integration.html "Intgrate Search")
on how to implement searching your FogBugz database in your browser via
the [OpenSearch provider](http://opensearch.a9.com/ "A9 OpenSearch"),
which is supported by Firefox 2.0 and IE7.

So I went ahead and used this as a guide to implementing OpenSearch for
my custom search engine on [my blog](https://haacked.com/ "My Blog"). 
When you visit my blog, you should notice that the the search icon in
the top left corner of your browser is highlighted (screenshot from
Firefox 2).

![Open search
box.](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IntegrateYourCustomSearchEngineWithTheBr_CE08/OpenSearchBox4.png)

Click on the down arrow and you will see my own search engine *[Haack
Attack](https://haacked.com/archive/2006/10/23/My_Very_Own_Search_Engine.aspx "My Very Own Search Engine")*
in the list of search providers.

![Haack
Attack](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IntegrateYourCustomSearchEngineWithTheBr_CE08/HaackSearchProviderInList4.png)

Now you can search using Haack Attack via your browser.  Implementing
this required two easy steps.  First I created an `OpenSearch.xml` file
and dropped it in the root of my website. Here is my file with some of
the gunk removed from the url.

```csharp
<?xml version="1.0" encoding="UTF-8" ?>
<OpenSearchDescription 
    xmlns="http://a9.com/-/spec/opensearch/1.1/">
  <ShortName>Haack Attack</ShortName>
  <Tags>Software Development C# ASP.NET</Tags>
  <Description>
    Search the web for relevant 
    .NET and software development content.
  </Description>
  <Url type="text/html" 
    template="http://www.google.com/custom?
    StuffOmitted&amp;q={searchTerms}" />
</OpenSearchDescription>
```

Remember to make sure to use `&amp;` for query string ampersands, as
this is an XML file.  Also, if you are using your own Google Custom
search engine, the actual template value looks something like:

http://www.google.com/custom?cx=**016071428520527893278%3A3kvxtxmsfga**
&q=**{searchTerms}**&sa=Search&cof=GFNT%3A%23000000%3BGALT%3A%23000066%
3BLH%3A23%3BCX%3AHaack%2520 Attack%2520The%2520Web%3BVLC%
3A%23663399%3BLW%3A100%3BDIV%3A%23336699%3BFORID%3A0%3BT%3A%23000000
%3BALC%3A%23660000%3BLC %3A%23660000%3BS%3Ahttp%3A%2F%2Fhaacked%2Ecom
%2F%3BL%3Ahttp%3A%2F%2Fhaacked%2Ecom%2Fskins%2Fhaacked%2Fimages%2F
Header%2Ejpg%3BGIMP%3A%23000000%3BLP%3A1%3BBGC%3A%23FFFFFF%3BAH%3Aleft&
client=**pub-7694059317326582**

So be sure to change it appropriate to your own search engine.

The second step is to add auto-discovery. I added the following \<link
/\> tag to my blog.  The bolded sections you would obviously want to
customize for your own needs.

```csharp
<link title="Haack Attack" 
  type="application/opensearchdescription+xml" 
  rel="search" 
  href="https://haacked.com/OpenSearch.xml">
</link>
```

So give it a try and let me know what you think. Be sure to add sites
you think are relevant to this searh engine.

