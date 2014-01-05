---
layout: post
title: "What does it mean to quack like a duck?"
date: 2014-01-05 -0800
comments: true
categories: [code]
---

From the topic of this post and [my last post](http://haacked.com/archive/2014/01/04/duck-typing/), you might think I have some weird fascination with ducks.

![Is it a duck? - CC BY-ND 2.0 from http://www.flickr.com/photos/77043400@N00/224131630/](https://f.cloud.github.com/assets/19977/1845502/4d494752-758e-11e3-9c66-8fd6080662fe.jpg)

But I think the topic of duck typing is interesting and I'm not known for missing an opportunity to beat a dead horse (or duck for that matter).

If you search for blog posts about duck typing, you often find examples that look something like the following Python code:

```python
def func(arg):
    if hasattr(arg, 'quack'):
        arg.quack()
    elif hasattr(arg, 'woof'):
        arg.woof()
```

Here's an example in Ruby.

```ruby
def func(arg)
  if arg.respond_to?(:quack)
    arg.quack
  end
end
```

It seems to me this misses half the point of duck typing. The question duck typing asks is, does the object "quack like a duck". Both of these examples only ask if the object quacks. But it doesn't ask if it quacks __like a duck__.

Most discussions of duck typing tend to focus on whether the object has methods that match the name and argument list needed. But another important aspect of a method is its return type. As far as I know, that's not something you can test in advance with Ruby or Python.

Suppose we have the following client code.

```ruby
def func(arg)
  return unless arg.respond_to?(:quack)
  sound = arg.quack
  /quack quack/.match(sound)
end
```

And we have the following two classes.

```ruby
class Duck
  def quack
    "quacketty quack quack"
  end
end

class Scientist
  def quack
    PseudoScience.new
  end
end
```

The `Scientist` certainly quacks, but it doesn't quack like a duck.

```ruby
func(Duck.new) // Works!
func(Scientist.new) // Oops!
```

I think that's one reason why the example in my previous post actually tries and call the method to ensure that the argument indeed quacks _like a duck_.

My guess is that in practice, conflicts like this where a method has the same name, but different type, is rare enough that Ruby and Python developers don't worry about it too much.
