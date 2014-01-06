---
layout: post
title: "Duck Typing Is More Than Quackery"
date: 2014-01-04 00:32 -0800
comments: true
categories: [code]
---

[Eric Lippert](http://ericlippert.com/) writes one of my all time favorite tech blogs. Sadly, the [purple font he was famous for](http://www.codinghorror.com/blog/2006/12/eric-lipperts-purple-crayon.html) is no longer, but the technical depth is still there.

In a recent post, he asks the question, "[What is Duck Typing?](http://ericlippert.com/2014/01/02/what-is-duck-typing/)" His post provides a thoughtful critique and deconstruction of the [Wikipedia entry on the subject](http://en.wikipedia.org/wiki/Duck_typing). Seriously, go read it, but please come back here afterwards!

For those of you too lazy to read it, I'll try and summarize crudely. He starts off with his definitions of "typing":

> A __compile-time type system__ provides three things: first, rules for defining types, like "array of pointers to integer". Second, rules for logically deducing what type is associated with every expression, variable, method, property, and so on, in a program. And third, rules for determining what programs are legal or illegal based on the type of each entity. These types are used by the compiler to determine the legality of the program.
> 
> A __run-time type system__ provides three similar things: first, again, rules for defining types. Second, every object and storage location in the system is associated with a type. And third, rules for what sorts of objects can be stored in what sorts of storage locations. These types are used by the runtime to determine how the program executes, including perhaps halting it in the event of a violation of the rules.

He continues with a description of structural typing that sounds like what he always thought "duck typing" referred to, but notes that his idea differs from the Wikipedia definition. As far as he can tell, the Wikipedia definition sounds like it's just describing Late Binding.

> But this is not even typing in the first place! We already have a name for this; this is late binding. "Binding" is the association of a particular method, property, variable, and so on, with a particular name2 in a particular context; if done by the compiler then it is "early binding", and if it is done at runtime then it is "late binding".3 Why would we even need to invent this misleadingly-named idea of "duck typing" in the first place??? If you mean "late binding" then just say "late binding"!

I agree that the Wikipedia definition is a bit unclear, but I think there's more to it than simple late binding. Also, I think some of the confusion lies in the fact that duck typing isn't so much a type system as it is a fuzzy approach to treating objects as if they are certain types (or close enough) based on their behavior rather than their declared type. This is a subtle distinction to late binding.

To back this up, I looked at the original [Google Group post](https://groups.google.com/forum/?hl=en#!msg/comp.lang.python/CCs2oJdyuzc/NYjla5HKMOIJ) where the Alex Martelli first described this concept.

> In other words, don't check whether it IS-a duck: check whether it QUACKS-like-a duck, WALKS-like-a duck, etc, etc, depending on exactly what subset of duck-like behaviour you need to play your language-games with. 

This was a response to a question that asked the question (I'm paraphrasing), _how do you handle method overloading with a single parameter in a dynamic language?_ Specifically, the question was in reference to the Python language.

To illustrate, in a static typed language like C#, you might have the following three methods of a class (_forgive me if the example seems contrived. I lack imagination._):

```csharp

public class PetOwner {
  public void TakeCareOf(Duck duck) {...}
  public void TakeCareOf(Robot robot) {...}
  public void TakeCareOf(Car car) {...}
}
```

In C#, the method that gets called is resolved at compile time depending on the type of the argument passed to it.

```csharp
var petOwner = new PetOwner();
petOwner.TakeCareOf(new Duck()); // calls first method.
petOwner.TakeCareOf(new Robot()); // calls second method.
petOwner.TakeCareOf(new Car()); // calls third method.
```

But in a dynamic language, such as Python, you can't have three methods with the same name each with a single argument. Without a type declared for the method argument, there is no way to distinguish between the methods. Instead, you'd need a single method and do something else.

One approach is you could switch based on the runtime type of the argument passed in, but Alex points out that would be inappropriate in Python. I assume because it conflicts with Python's dynamic nature. Keep in mind that I'm not a Python programmer so I'm basing this on my best attempt to interpret Alex's words:

> In other words, don't check whether it IS-a duck: check whether it QUACKS-like-a duck, WALKS-like-a duck, etc, etc, depending on exactly what subset of duck-like behaviour you need to play your language-games with.

As I said before, I don't know a lick of Python, so I'll pseuducode what this might look like.

```python
class PetOwner:
    def take_care_of(arg):
        if behaves_like_duck(arg):
            #Pout lips and quack quack
        elif behaves_like_robot(arg):
            #Domo arigato Mr. Roboto
        elif behaves_like_car(arg):
            #Vroom vroom vroom farfegnugen
``` 

So rather than check if the arg IS A duck, you check if it behaves like a duck. The question is, how do you do that?

Alex notes this could be tricky.

> On the other hand, this can be a considerable amount of work, depending on how you go about it (actually, it need not be that bad if you "just go ahead and try", of course catching the likely exceptions if the try does not succeed; but still, greater than 0).

One proposed approach is to simply treat it like a duck, and if it fails, start treating it like a fish. If that fails, try treating it like a dog.

I'd guess that code would look something like:

```python
class PetOwner:
  def take_care_of(arg):
    try:
      arg.walk()
      arg.quack()
    except:
      try:
        arg.sense_danger_will_robinson()
        arg.dance_in_staccato_manner()
      except:
        arg.drive()
        arg.drift()
``` 

Note that this is not exactly the same as late binding as Eric proposes. Late binding is involved, but that's not the full picture. It's late binding combined with the branching based on the set of methods and properties that make up "duck typing."

What's interesting is that this was not the only possible solution that Alex proposed. In fact, he concludes it's not the optimal approach.

> Besides, "explicit is better than implicit", goes one of Python's mantras.  Just let the client-code explicitly TELL you which kind of argument they are passing you (and doing so through a named argument is simple and readable), and your work drops to zero, while removing no useful functionality whatever from the client.

He goes on to state that this implicit duck typing approach to method overloading seems to have dubious benefit.

> The "royal-road" alternative route to overloading would, I think, be the use of suitable named-arguments.  A rockier road, perhaps preferable in some cases, but more work for dubious benefit, would be the try/except approach to see if an argument supplies the functionalities you require.

The Python approach would be to pass in a discriminator. Even so, the object passed in would have to fulfill the set of requirements for the selected branch of code indicated by the discriminator. With the discriminator, it does feel more like we're just talking about late binding, but applied to a set of methods and properties, not just each one individually as you might do with late binding.

One observation I've heard is that "duck typing" sounds kind of like "duct taping." Not sure if there's anything to that, but if you forgive a bit of a stretch, I think there it may be an apt analogy.

On the Apollo 13 mission, the crew was faced with a situation where carbon dioxide levels were rising to dangerous levels in the Lunar Module. They had plenty of filters, [but their square filters would not fit in the round  barrels that housed the filters](http://www.universetoday.com/63673/13-things-that-saved-apollo-13-part-10-duct-tape/). In other words, their square filters were the wrong type (whether dynamic or static). Their solution was to use duct tape to cobble something together that would work. It wasn't the solution intended by the original design, but as long as the final contraption acts like an air filter (duck typing), they would survive. And they did. Like I said, the analogy is a bit of as stretch, but I think it embodies the duck typing approach.

Perhaps a better term is typing by usage. With explicit typing, you explicitly declare an object to be one type or another (whether at compile time or run time). With typing by usage, if it just happens to meet the needs of the consumer, then hey! It's a duck!

For static typed languages, I really like the idea of structural typing. It provides a nice combination of type safety and flexibility. [Mark Rendle](http://blog.markrendle.net/), in the comments to Eric's blog post provides this observation:

> Structural Typing may be thought of as a kind of compiler-enforced subset of Duck Typing.

In other words, it's duck typing for static typed languages.

Also in the comments to Eric's post, someone linked to [my blog post about duck typing](http://haacked.com/archive/2007/08/19/why-duck-typing-matters-to-c-developers.aspx/). At the time I wrote that, "structural typing" wasn't in my vocabulary. If it had been, I could have been more precise in my post. For static languages, I find structural typing to be very compelling.



What do you think? Did I nail it? Or did I drop the ball and get something wrong or misrepresent an idea? Let me know in the comments.

__UPDATE:__ Sam Livingston-Gray, also known as [@geeksam](https://twitter.com/geeksam) notes [another key difference between late binding that I completely missed](https://twitter.com/geeksam/status/419541821283237888):

> @haacked method_missing illustrates the disconnect between binding and typing: an obj can choose how and whether to respond to a message

Recall that Eric defines "Late Binding" as:

> "Binding" is the association of a particular method, property, variable, and so on, with a particular name in a particular context; if done by the compiler then it is "early binding", and if it is done at runtime then it is "late binding".

You could argue that `method_missing` is another form of late binding where the name is bound to `method_missing` because there is no other name to bind to. But conceptually, it feels very different to me. With binding, you usually think of the caller determining which method to call by name. And whether it's bound early or late is no matter, it's still the caller's choice. With `method_missing` it's the object in control of whethere it's going to respond to the method call (message).
