---
title: IronRuby ASP.NET MVC With Filters
tags: [aspnetmvc,aspnet,languages]
redirect_from: "/archive/2009/02/16/aspnetmvc-ironruby-with-filters.aspx/"
---

Last July, I blogged about an [IronRuby ASP.NET MVC
prototype](https://haacked.com/archive/2008/07/20/ironruby-aspnetmvc-prototype.aspx "IronRuby with ASP.NET MVC Working Prototype")
Levi and I put together with [John
Lam](http://www.iunknown.com/ "John Lam") and [Jimmy
Schementi](http://blog.jimmy.schementi.com/ "Jimmy Schementi") of the
DLR team. It was really rough around the edges (and still is!)

![IronRuby on ASP.NET MVC
Demo](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IronRubyWithASP.NETMVCWorkingPrototype_BDF3/IronRuby%20on%20ASP.NET%20MVC%20Demo%20-%20Windows%20Internet%20Explorer_3.png "IronRuby on ASP.NET MVC Demo")One
of the benefits of doing that prototype was that it inspired all the
work around action and controller descriptors in [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC Website") (something I need to
write more about later) which decoupled us from exposing reflection in
our public API and improved the overall design of ASP.NET MVC greatly.
This had the nice side-effect of making the implementation of IronRuby
on top of ASP.NET MVC much cleaner.

In this [updated
prototype](https://haacked.com/code/IronRuby-for-aspnetmvc-rc.zip "IronRuby prototype"),
I’ve now implemented support for ASP.NET MVC filters. You can define
action filters and authentication filters (I need to test the other
filter types). Keep in mind, this is a very rough prototype code still.
I’ve just been swamped up to my neck lately and this is a spare-time
labor of love.

I’ve only implemented one type of filter so far. You can specify a class
to apply to an action method and the class implements a specific filter
interface. I haven’t done anything like the more rails-y filter\_before
and filter\_after thing.

Here’s an example of an action filter in IronRuby. This one simply
writes something to the response in the *before* method, and does
nothing in the *after* method.

```csharp
class MyFilter < IronRubyMvc::Controllers::RubyActionFilter
    def on_action_executing(context)
      context.http_context.response.write 'MyFilter '
    end
    
    def on_action_executed(context)
      # noop
    end
    
    def method_missing(name, *args)
        show_action_info name, args
    end
end
```

(Gee, I wish I had a ruby syntax highlighter plug-in for WLW)

And here’s the use of that filter within a controller.

```csharp
require 'HomeModel'
require 'MyFilter'

class HomeController < Controller
  filter :index, MyFilter

  def index
    view nil, 'layout', HomeModel.new
  end  
end
```

Notice that the way you define a filter on the index action is:

> `filter :action_name, FilterClassName`

In the sample code I uploaded, you can see the effects of the filter at
the top of the page. :) Hopefully I’ll find more time to update this,
but as I said, it’s a labor of love, but time is in short supply.

In the meanwhile, I also need to look into whether there’s enough
interest to make this a CodePlex project. There’s a bit of due diligence
I have to do before I put code up on CodePlex, which is why I haven’t
done it already because I’ve been busy.

~~And before I forget, here’s the **[download location for the
sample](https://haacked.com/code/IronRuby-for-aspnetmvc-rc.zip "IronRuby for ASP.NET MVC")**.~~

Ivan Porto Carerra has taken this prototype and is running with it. To
download the latest, check out his [IronRubyMVC GitHub
project](http://github.com/casualjim/ironrubymvc "IronRuby ASP.NET MVC").

