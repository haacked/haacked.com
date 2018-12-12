---
title: Matching HTML With Regular Expressions Redux
date: 2005-04-22 -0800
disqus_identifier: 2784
categories: []
redirect_from: "/archive/2005/04/21/Matching_HTML_With_Regex.aspx/"
---

UPDATE: Mea culpa! [Maurice](http://www.bluedoglimited.com/) pointed out
that (except for the casing) my original expression WAS correct. I only
needed the RegexOptions.SingleLine option. I didn't need to add the
(\\s|\\n) everywhere. Here's a corrected post. Thanks Maurice!

Last time I [talked about matching
HTML](https://haacked.com/archive/2004/10/25/1471.aspx) with regular
expressions, I published a regular expression with a couple small bugs.
The first bug was not my fault, but rather the fault of the rich text
editor that comes with .TEXT. It was being overly “helpful” when I tried
to edit the post and uppercased some of the code. As you know, “\\S” is
much different than “\\s” to a regular expression.

The second bug is entirely my fault and I write this as a confession and
to provide a fix. You see, I assumed (and you know what happens when we
assume) that complete tags tend to be on a single line. Well that's not
the always the case. You might encounter something ugly like this:

```csharp
<div     id = "blah" alt=" man    this is ugly html "     >     fire this guy... </div> 
```

The expression I had posted wouldn't have matched the div tag sitting in
plain sight there ~~so I went in there and corrected that sucker~~ all
by itself. It requires using the RegexOptions.SingleLine option so that
the . character matches \\n. Here's the expression reprinted (with
correct casing) for your reference.

> \</?\\w+((\\s+\\w+(\\s\*=\\s\*(?:".\*?"|'.\*?'|[\^'"\>\\s]+))?)+\\s\*|\\s\*)/?\>

~~The main difference is now I'm including \\n anywhere I'm matching
whitespace (via \\s). In order for this to work, you need to use the
RegexOption SingleLine.~~ Here's a code snippet that uses this
expression to match the above html.

string html = "\<div\\n\\tid=\\"blah\\" alt=\\" man\\n"

    + "\\tthis is ugly html \\"\\n"

    + "\\t\>"

    + "fire this guy...\\n"

    + "\</div\>";

 

Regex regex = new
Regex(@"\</?\\w+((\\s+\\w+(\\s\*=\\s\*(?:"".\*?""|'.\*?'|[\^'""\>\\s]+))?)+\\s\*|\\s\*)/?\>",
RegexOptions.Singleline);

 

MatchCollection matches = regex.Matches(html);

Console.WriteLine(".....Original Html......");

Console.WriteLine(html + Environment.NewLine);

Console.WriteLine(".....Each Tag......");

foreach(Match match in matches)

{

    Console.WriteLine("TAG: " + match.Value.Replace("\\n", " "));

}

This produces the output:

>     .....Original Html......<div    id="blah" alt=" man   this is ugly html "   >fire this guy...</div>.....Each Tag......TAG: <div     id="blah" alt=" man     this is ugly html "     >TAG: </div>

So sorry about that. Hope this one treats you better.

