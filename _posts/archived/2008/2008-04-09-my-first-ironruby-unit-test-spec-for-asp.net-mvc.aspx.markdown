---
title: My First IronRuby Unit Test Spec For ASP.NET MVC
tags: [aspnet,aspnetmvc,code,tdd]
redirect_from: "/archive/2008/04/08/my-first-ironruby-unit-test-spec-for-asp.net-mvc.aspx/"
---

Way down the road, it would be nice to be able to build ASP.NET MVC
applications using a
[DLR](http://en.wikipedia.org/wiki/Dynamic_Language_Runtime "Dynamic Language Runtime on Wikipedia")
language such as [IronRuby](http://www.ironruby.net/ "IronRuby").
However, enabling DLR language support isn’t free.

There are going to be places in our design that are specific to
statically typed languages (such as Attribute based filters) that just
wouldn’t work (or would be too unnatural) with a dynamic language.

Ideally we can minimize those cases, and for the ones we can’t, we need
to make sure the extensibility of the framework allows for extending the
system in such a way that we can provide a DLR friendly version of that
feature.

How do we identify and minimize such hot spots? Design reviews help, but
only goes so far. There is nothing like executing code to highlight
issues. So in collaboration with some of the DLR team members, I’ve been
exploring the minispec framework used to test IronRuby and wrote my
first ~~test~~spec tonight. Check it out. (NOTE: line breaks added in
the require statements so it fits within the width of my blog)

    require File.dirname(__FILE__) + '/../../spec_helper'
    require 'System.Web.Abstractions
    , Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
    require 'System.Web.Routing
    , Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
    require 'System.Web.Mvc
    , Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'

    describe "Route#<<" do
     
      it "can create RouteCollection which is empty" do
        rc = System::Web::Routing::RouteCollection.new
        rc.count.should == 0
      end
      
      it "can add route to RouteCollection" do
        rc = System::Web::Routing::RouteCollection.new
        r = System::Web::Routing::Route.new "", nil
        rc.add "route-name", r
        
        rc.count.should == 1
      end

    end

And here is the result so far.

![Administrator
CWindowsSystem32cmd.exe](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TestingMVCRoutingWithIronRuby_14B31/Administrator%20CWindowsSystem32cmd.exe_3.png)

Yay! Two passing tests.

Yeah, the tests are really really simple so far, but hey, this is just
my first step. I need to get familiar with the minispec framework. Not
only that, but I haven’t written any Ruby code in a long while.
Fortunately I do have a copy of [The Ruby
Way](http://www.amazon.com/gp/product/0672328844?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=0672328844 "The Ruby Way")
on my shelf, which should help.

I probably have much higher priority items on my plate that I could be
working on, but sometimes you have to treat yourself to a little fun.
Besides, I am doing this on my own time right now. :)

