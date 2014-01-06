---
layout: post
title: "But does it quack like a duck?"
date: 2014-01-06 -0800
comments: true
categories: [code]
---

From the topic of this and [my last post](http://haacked.com/archive/2014/01/04/duck-typing/), you would be excused if you think I have some weird fascination with ducks. In fact, I'm starting to question it myself.

![Is it a duck? - CC BY-ND 2.0 from http://www.flickr.com/photos/77043400@N00/224131630/](https://f.cloud.github.com/assets/19977/1845502/4d494752-758e-11e3-9c66-8fd6080662fe.jpg)

I just find the topic of duck typing (and structural typing) to be interesting and I'm not known to miss an opportunity to beat a dead horse (or duck for that matter).

Often, when we talk about duck typing, code examples only ask if the object quacks. But shouldn't it also ask if it quacks _like a duck_?

For example, here are some representative examples you might find if you Google the terms "duck typing". The first example is Python.

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

Does this miss half the [point of duck typing](https://groups.google.com/forum/?hl=en#!msg/comp.lang.python/CCs2oJdyuzc/NYjla5HKMOIJ)?

> In other words, don't check whether it IS-a duck: check whether it QUACKS-like-a duck, WALKS-like-a duck, etc, etc,...

Note that this doesn't just suggest that we check whether the object quacks and stop there, as these examples do. It suggests we should go further and ask if it quacks __like a duck__.

Most discussions of duck typing tend to focus on whether the object has methods that match a given name. I haven't seen examples that also check the argument list. But yet another important aspect of a method is its return type. As far as I know, that's not something you can test in advance with Ruby or Python.

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
func(Duck.new) // returns a MatchData object
func(Scientist.new) // returns nil (match failed)
```

I think that's one reason why the example in my previous post actually tries and call the method to ensure that the argument indeed quacks _like a duck_.

My guess is that in practice, conflicts like this where a method has the same name, but different type, is rare enough that Ruby and Python developers don't worry about it too much.

Also, with such dynamic languages, it's possible to monkey patch an object to conform to the implicit contract if it doesn't match it exactly. Say if you have a `RobotDuck` you got from another library you didn't write and want to pass it in as a duck.

_Thanks to [GeekSam](https://github.com/geeksam) for reviewing my Ruby and Python idioms_.