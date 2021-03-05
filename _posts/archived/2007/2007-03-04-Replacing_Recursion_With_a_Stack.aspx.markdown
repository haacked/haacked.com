---
title: Replacing Recursion With a Stack
tags: [code]
redirect_from: "/archive/2007/03/03/Replacing_Recursion_With_a_Stack.aspx/"
---

![Stack of
Paper](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ReplacingRecursionWithaStack_124F7/251979_paper_stack10.jpg)In
[Jeff Atwood’s](http://codinghorror.com/blog/ "Jeff Atwood") infamous
[FizzBuzz
post](http://www.codinghorror.com/blog/archives/000781.html "Why Can’t Programmers Program?"),
he quotes Dan Kegel who mentions.

> Less trivially, I’ve interviewed many candidates who can’t use
> recursion to solve a real problem.

A programmer who doesn’t know how to use recursion isn’t necessarily
such a bad thing, assuming the programmer is handy with the `Stack` data
structure. Any recursive algorithm can be replaced with a non-recursive
algorithm by using a `Stack`.

*As an aside, I would expect any developer who knew how to use a stack
in this way would probably have no problem with recursion.*

After all, what is a recursive method really doing under the hood but
implicitely making use of the call stack?

I’ll demonstrate a method that removes recursion by explicitely using an
instance of the `Stack` class, and I’ll do so using a common task that
any ASP.NET developer might find familiar. I should point out that I’m
not recommending that you *should* or *shouldn’t* do this with methods
that use recursion. I’m merely pointing out that you *can* do this.

In ASP.NET, a Web Page is itself a control (i.e. the `Page` class
inherits from `Control`), that contains other controls. And those
controls can possibly contain yet other controls, thus creating a tree
structure of controls.

**So how do you find a control with a specific ID that could be nested
at any level of the control hierarchy?**

Well the recursive version is pretty straightforward and similar to
other methods [I've written
before](https://haacked.com/archive/2006/06/13/ProperWayToFindTheForm.aspx "Proper Way To Find The Form").

```csharp
public Control FindControlRecursively(Control root, string id)
{
  Control current = root;

  if (current.ID == id)
    return current;

  foreach (Control control in current.Controls)
  {
    Control found = FindControlRecursively(control, id);
    if (found != null)
      return found;
  }
  return null;
}
```

The recursion occures when we call `FindControlRecursively` within this
method. Essentially what is happening (and this is a simplification)
when we call that method is that our current execution point is pushed
onto the call stack and the runtime starts executing the code for the
internal method call. When that method finally returns, we pop our place
from the stack and continue executing.

Rather than try to explain, let me just show you the non-recursive
version of this method using a `Stack.`

```csharp
public Control FindControlSansRecursion(Control root
    , string id)
{
  //seed it.
  Stack<Control> stack = new Stack<Control>();
  stack.Push(root);
    
  while(stack.Count > 0)
  {
    Control current = stack.Pop();
    if (current.ID == id)
      return current;
        
    foreach (Control control in current.Controls)
    {
      stack.Push(control);
    }
  }
  
  //didn’t find it.
  return null;
}
```

One thing to keep in mind is that both of these implementations assume
that we won’t run into a circular reference problem in which a child
control contains an ancestor node.

For the `System.Web.UI.Control` class we safe in making this assumption.
If you try and create a circular reference, a `StackOverflowException`
is thrown. The following code demonstrates this point.

```csharp
Control control = new Control();
control.Controls.Add(new Control());
// This line will throw a StackOverflowException.
control.Controls[0].Controls.Add(control); 
```

If the hierarchical structure you are using *does* allow circular
references, you’ll have to keep track of which nodes you’ve already seen
so that you don’t get caught in any infinitel loops.

