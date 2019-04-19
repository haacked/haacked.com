---
title: Monkey Patching CLR Objects
tags: [languages]
redirect_from: "/archive/2008/04/17/monkey-patching-clr-objects.aspx/"
---

In [my last
post](https://haacked.com/archive/2008/04/18/dynamic-language-dsl-vs-xml-configuration.aspx "Dynamic Language vs XML")
I set the stage for this post by discussing some of my personal opinions
around integrating a dynamic language into a .NET application. Using a
DSL written in a dynamic language, such as IronRuby, to set up
configuration for a .NET application is an interesting approach to
application configuration.

With that in mind, I was playing around with some IronRuby interop with
the CLR recently. Ruby has this concept called *[Monkey
Patching](http://en.wikipedia.org/wiki/Monkey_patch "Monkey Patching on wikipedia")*.
You can read the definition in the Wikipedia link I provided, but in
short, it is a way to modify the behavior of a class or instance of a
class at runtime without changing the source of that class or instance.
Kind of like extension methods in C#, but more powerful. Let me provide
a demonstration.

I want to pass a C# object instance that happens to have an indexer to
a Ruby script via IronRuby. In C#, you can access an indexer property
using square brackets like so:

```csharp
object value = indexer["key"];
```

Being able to use braces to access this property is merely syntactic
sugar by the C# language. Under the hood, this gets compiled to IL as a
method named `get_Item`.

So when passing this object to IronRuby, I need to do the following:

```csharp
value = $indexer.get_Item("key");
```

That’s not soooo bad (ok, maybe it is), but we’re not taking advantage
of any of the power of Ruby. So what I did was monkey patch the
`method_missing` method onto my object and used the method name as the
key to the dictionary. This method allows you to handle unknown method
calls on an object instance. You can [read this
post](http://blog.mauricecodik.com/2005/12/more-ruby-methodmissing.html "More Ruby: method_missing")
for a nice brief explanation.

So this allows me now to access the indexer from within Ruby as if it
were a simple property access like so:

```csharp
value = $indexerObject.key
```

The code for doing this is the following, based on the [latest IronRuby
code in
RubyForge](http://rubyforge.org/projects/ironruby "IronRuby in RubyForge").

```csharp
ScriptRuntime runtime = IronRuby.CreateRuntime();
ScriptEngine rubyengine = IronRuby.GetEngine(runtime);
RubyExecutionContext ctx = IronRuby.GetExecutionContext(runtime);

ctx.DefineGlobalVariable("indexer", new Indexer());
string requires = 
@"require 'My.NameSpace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=...'

def $indexer.method_missing(methodname)
  $indexer.get_Item(methodname.to_s)
end
";

//pretend we got the ruby script I really want to run from somewhere else
string rubyScript = GetRubyCode();

string script = requires + rubyScript;
ScriptSource source = rubyengine.CreateScriptSourceFromString(script);
runtime.ExecuteSourceUnit(source);
```

What’s going on here is that we instantiate the IronRuby runtime and
script engine and context (I still need to learn exactly what each of
these things are responsible for apart from each other). I then set a
global variable and set it to an instance of a CLR object written in
C#.

After that, I start constructing a string that contains the beginning of
the Ruby script I want to execute. I will pre-append this beginning
section with the actual script I want to run.

The beginning of the Ruby script imports the .NET namespace that
contains my CLR type to IronRuby (*I believe that by default you don’t
need to import mscorlib and System*).

I then added a `missing_method` method to that CLR instance within the
Ruby code via this snippet.

```csharp
def $indexer.method_missing(methodname);
  $indexer.get_Item(methodname.to_s)
end
```

At that point now, when I execute the rest of the ruby script, any calls
from within Ruby to this CLR object can take advantage of this new
method we patched onto the instance.

Pretty nifty, eh?

In my next post, I will show you the concrete instance of using this and
supply source code.
