---
title: 'Databinding Tips: Nesting Eval Statements'
date: 2007-04-12 -0800 9:00 AM
tags: [data,aspnet]
redirect_from: "/archive/2007/04/11/databinding-tips-nesting-eval-statements.aspx/"
---

Maybe this is obvious, but it wasn’t obvious to me. I’m binding some
data in a repeater that has the following output based on two numeric
columns in my database. It doesn’t matter why or what the data
represents. It’s just two pieces of data with some formatting:

`42, (123)`{.console}

Basically these are two measurements. Initially, I would databind this
like so:

```csharp
<%# Eval("First") %>, (<%# Eval("Second") %>)
```

The problem with this is that if the first field is null, I’m left with
this output.

`, (123)`{.console}

Ok, easy enough to fix using a format string:

```csharp
<%# Eval("First", "{0}, ") %>(<%# Eval("Second") %>)
```

But now I’ve learned that if the first value is null, the second one
should be blank as well. Hmm... I started to do it the ugly way:

```csharp
<%# Eval("First", "{0}, ") %> <%# Eval("First").GetType() == 
  typeof(DBNull) ? "" : Eval("Second", "({0})")%>
```

\*Sniff\* \*Sniff\*. You smell that too? Yeah, stinky and hard to read.
Then it occured to me to try this:

```csharp
<%# Eval("First", "{0}, " + Eval("Second", "({0})")) %>
```

Now that code smells much much better! I put the second `Eval` statement
as part of the format string for the first. Thus if the first value is
null, the whole string is left blank. It’s all or nothing baby! Exactly
what I needed.

