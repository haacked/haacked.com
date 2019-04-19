---
title: Tried of Typing C# Properties?
date: 2005-03-25 -0800
tags: [code]
redirect_from: "/archive/2005/03/24/tried-of-typing-c-properties.aspx/"
---

Saw this post on [Consult Utah's
Weblog](http://consultutah.com/weblog.aspx?id=92)...

> Is anyone else getting tired of writting:
>
>     ...
>     private bool _autoAccept;
>     ...
>     public bool AutoAccept 
>     { 
>       get {return _autoAccept;} 
>       set {_autoAccept = value;} 
>     } 
>
> I've pretty much decided that it is just preferable in these cases
> to just make the member variable public.  I'm am being bad?  ;-) 

My answer? I think he should be publicly flogged, smeared with tar and
feather, and paraded around town in a pink tutu.

Ok, that might be a bit extreme. I don't really believe that.

I think using a public field is fine in some cases. If the interface is
unlikely to change. If you're not writing library code. etc... If you
can easily refactor it later if necessary. I'm not a complete and total
purist about this.

Now before you OO purists get on my case, I'd like to point out that I
always use full properties because I HATE FxCop warnings in my code. ;)
Also, I have a nice little code expansion template in
[Resharper](http://www.jetbrains.com/resharper/). I simply type

    prop<TAB>

and I get a full expansion of the property, allowing me to fill in the
type and private member name. You can see a [description
here](https://haacked.com/archive/2004/08/20/954.aspx).

Resharper also has an ALT+INSERT shortcut for generating code snippets,
but I find my little expansion to be faster for me.

Although I'm not an OO purist nazi, I do think that the encapsulation
benefits of properties are worth the trouble, especially when they
aren't much trouble.

