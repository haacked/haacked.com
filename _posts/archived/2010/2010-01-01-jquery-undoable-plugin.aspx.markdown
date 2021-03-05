---
title: "Death to confirmation dialogs with jquery.undoable"
tags: [code,jquery]
---
Confirmation dialogs were designed by masochists intent on making users
of the web miserable. At least that’s how I feel when I run into too
many of them. And yes, if you take a look at Subtext, you can see I’m a
perpetrator.

Well no longer!

I was managing my Netflix queue recently when I accidentally added a
movie I did not intend to add (click on the image for a larger view).

[![netflix-queue](https://haacked.com/images/haacked_com/WindowsLiveWriter/UndoableactionsWithjQuery.undoable_D616/netflix-queue_thumb.png "netflix-queue")](https://haacked.com/images/haacked_com/WindowsLiveWriter/UndoableactionsWithjQuery.undoable_D616/netflix-queue_2.png)
Naturally, I clicked on the blue “x” to remove it from the queue and saw
this.

[![netflix-queue-deleted](https://haacked.com/images/haacked_com/WindowsLiveWriter/UndoableactionsWithjQuery.undoable_D616/netflix-queue-deleted_thumb.png "netflix-queue-deleted")](https://haacked.com/images/haacked_com/WindowsLiveWriter/UndoableactionsWithjQuery.undoable_D616/netflix-queue-deleted_2.png)
Notice that there’s no confirmation dialog that I’m most likely to
ignore questioning my intent requiring me to take yet one more action to
remove the movie. No, the movie is removed immediately from my queue
just as I requested. I love it when software does what I tell it to do
and doesn’t second guess me!

But what’s great about this interface is that it respects that I’m human
and am likely to make mistakes, so it offers me an out. The row becomes
grayed out, shows me a status message, and provides a link to undo the
delete. So if I did make a mistake, I can just click undo and everything
is right with the world. Very nicely done!

I started to get curious about how they did it and did not find any
existing jQuery plugins for building this sort of undoable interface, so
I decided this would be a fun second jQuery plugin for me to write, my
first being the [live preview
plugin](https://haacked.com/archive/2009/12/15/live-preview-jquery-plugin.aspx "jquery.livepreview plugin").

### The Plugin

Much like my [jQuery hide/close
link](https://haacked.com/archive/2009/12/25/jquery-hide-close-link.aspx "jQuery hide close link"),
the default usage of the plugin relies heavily on conventions. As you
might expect, all the conventions are easily overriden. Here’s the
sample HTML for a table of comments you might have in the admin section
of a blog.

```html
<table>
    <thead>
        <tr>
            <th>title</th>
            <th>author</th>
            <th>date</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>This is an interesting plugin</td>
            <td>Bugs Bunny</td>
            <td>12/31/2009</td>
            <td><a href="#1" class="delete">delete</a></td>
        </tr>
        <tr>
            <td>No, it's a bit derivative. But nice try.</td>
            <td>Derrida</td>
            <td>1/1/2010</td>
            <td><a href="#2" class="delete">delete</a></td>
        </tr>
        <tr>
            <td>Writing sample data is no fun at all.</td>
            <td>Peter Griffin</td>
            <td>1/2/2010</td>
            <td><a href="#3" class="delete">delete</a></td>
        </tr>
    </tbody>
</table>
```

And the following is the code to enable the behavior.

```csharp
$(function() {
    $('a.delete').undoable({url: 'http://example.com/delete/'});
});
```

By convention, when one of the *delete* links is clicked, the value in
`href` attribute is posted as form encoded data with the key “id” to the
specified URL, in this case *http://example.com/delete/*.

If you have more form data to post, it’s quite easy to override how the
form data is put together and send whatever you want. The following
examples pulls the id from a hidden input and sends it with the form key
“commentId”.

```csharp
$(function() {
  $('a.delete').undoable({
    url: 'http://example.com/delete/',
    getPostData: function(clickSource, target) {
      return {
        commentId: target.find("input[type='hidden'][name='id']").value(),
        commentType: 'contact'
      };
    }
  });
});
```

When the data is posted to the server, the server must respond with a
JSON object having two properties, `subject` and `predicate`. For
example, in ASP.NET MVC you might post to an action method which looks
like:

```csharp
public ActionResult Delete(int id) {
  // Delete it
  return Json(new {subject = "The comment", predicate = "was deleted"});
}
```

The only reason I broke up the response message into two parts is to
enable nice formatting like Netflix’s approach.

This of course is is easily overridden. For example, it may be simpler
to simply return  I can override the `formatStatus` method to expect
other properties in the response from the server. For example, suppose
you simply want the server to respond with one message property, you
might do this.

```csharp
$(function() {
  $('a.delete').undoable({
    url: 'http://example.com/delete/',
    formatMessage: function(response) {
      return response.message;
    }
  });
});
```

I wrote the plugin with the `TABLE` scenario in mind as I planned to use
it in the comment admin section of Subtext, but it works equally well
with other elements such as `DIV` elements. For example, the user facing
comments of a blog are most likely in a list of `DIV` tags. All you need
to do to make this work is make sure the `DIV` has a class of “target”
or override the `getTarget` method.

If you want to see more examples of how to use the plugin in various
scenarios, check out the **[jquery.undoable demos
page](http://demo.haacked.com/jquery.undoable/ "jQuery undoable plugin demos")**.

### I need your help!

I really hope some of you out there find this plugin useful. Writing
these plugins has been a great learning experience for me. I found the
following two resources extremely valuable.

-   [jQuery Plugin Authoring
    Guide](http://docs.jquery.com/Plugins/Authoring "Plugins/Authoring")
    This is the beginners guide on the jQuery documentation page. I
    found it to be very helpful in learning the basics of plugin
    development.
-   [A Plugin Development
    Pattern](http://www.learningjquery.com/2007/10/a-plugin-development-pattern "Tips for writing a jQuery plugin")
    by Mike Alsup. Mike outlines a pattern for writing jQuery plugins
    that has worked well for him based on his extensive experience. I
    tried to follow this pattern as much as I could.

However, I still feel there’s room to improve. I’m not sure that I fully
grasped all the tips and wrote a truly idiomatic usable extensible clean
jQuery plugin. So if you have experience writing jQuery plugins, please
do enumerate all the ways my plugin sucks.

If you simply use this plugin, please tell me what does and doesn’t work
for you and how it could be better. I’m really having fun writing these
plugins and would find your constructive feedback very helpful.

### The Source

As with my last plugin, the source is **[hosted on
GitHub](http://github.com/Haacked/jquery.undoable "jQuery.undoable on GitHub")**.
Git is another tool I’m learning. I can’t really make a judgment until I
use it on a project where I’m collaborating with others \*hint\*
\*hint\*. :) Please do fork it and provide patches and educate me on
writing better plugins. :)
