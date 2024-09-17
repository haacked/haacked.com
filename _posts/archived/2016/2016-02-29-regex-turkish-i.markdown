---
title: "A Subtle Case Sensitivity Gotcha with Regular Expressions"
tags: [regex]
excerpt_image: https://imgs.xkcd.com/comics/regular_expressions.png
---

> Some people, when confronted with a problem, think “I know, I'll use regular expressions.” Now they have two problems. - Jamie Zawinski

For other people, when confronted with writing a blog post about regular expressions, think "I know, I'll quote that Jamie Zawinski quote!"

It's the go to quote about regular expressions, but it's probably no surprise that it's often taken out of context. Back in 2006, Jeffrey Friedl tracked down the original context of this statement [in a fine piece of "pointless" detective work](http://regex.info/blog/2006-09-15/247). The original point, as you might guess, is a warning against trying to shoehorn Regular Expressions to solve problems they're not appropriate for.

As XKCD noted, regular expressions used in the right context can save the day!

![XKCD - CC BY-NC 2.5 by Randall Munroe](https://imgs.xkcd.com/comics/regular_expressions.png)

If Jeffrey Friedl's name sounds familiar to you, it's probably because he's the author of the definitive book on regular expressions, [Mastering Regular Expressions](http://www.amazon.com/gp/product/0596528124?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=0596528124).
After reading this book, I felt like the hero in the XKCD comic, ready to save the day with regular expressions.

## The Setup

This particular post is about a situation where Jamie's regular expressions prophecy came true. In using regular expressions, I discovered a subtle unexpected behavior that could have lead to a security vulnerability.

To set the stage, I was working on a regular expression to test to see if potential GitHub usernames are valid. A GitHub username may only consist of alphanumeric characters. (_The actual task I was doing was a bit more complicated than what I'm presenting here, but for the purposes of the point I'm making here, this simplification will do._)

For example, here's my first take at it `^[a-z0-9]+$`. Let's test this expression against the username `shiftkey` (a fine co-worker of mine). Note, these examples assume you import the `System.Text.RegularExpressions` namespace like so: `using System.Text.RegularExpressions;` in C#. You can run these examples online using [CSharpPad](http://csharppad.com/), just be sure to output the statement to the console. Or you can use [RegexStorm.net](http://regexstorm.net/tester) to test out the .NET regular expression engine.

```csharp
Regex.IsMatch("shiftkey", "^[a-z0-9]+$"); // true
```

Great! As expected, `shiftkey` is a valid username.

You might be wondering why GitHub restricts usernames to the latin alphabet a-z. I wasn't around for the initial decision, but my guess is to protect against confusing lookalikes. For example, someone could use a character that looks like an `i` and make me think they are `shiftkey` when in fact they are `shıftkey`. Depending on the font or whether someone is in a hurry, the two could be easily confused.

So let's test this out.

```csharp
Regex.IsMatch("shıftkey", "^[a-z0-9]+$"); // false
```

Ah good! Our regular expression correctly identifies that as an invalid username. We're golden.

But no, we have another problem! Usernames on GitHub are case insensitive!

```csharp
Regex.IsMatch("ShiftKey", "^[a-z0-9]+$"); // false, but this should be valid
```

Ok, that's easy enough to fix. We can simply supply an option to make the regular expression case insensitive.

```csharp
Regex.IsMatch("ShiftKey", "^[a-z0-9]+$", RegexOptions.IgnoreCase); // true
```

Ahhh, now harmony is restored and everything is back in order. Or is it?

## The Subtle Unexpected Behavior Strikes

Suppose our resident `shiftkey` imposter returns again.

```csharp
Regex.IsMatch("ShİftKey", "^[a-z0-9]+$", RegexOptions.IgnoreCase); // true, DOH!
```

Foiled! Well that was entirely unexpected! What is going on here? It's the Turkish İ problem all over again, but in a unique form. I wrote about this problem in 2012 in the post [The Turkish İ Problem and Why You Should Care](https://haacked.com/archive/2012/07/05/turkish-i-problem-and-why-you-should-care.aspx/). That post focused on issues with Turkish İ and string comparisons.

> The tl;dr summary is that the uppercase for i in English is I (note the lack of a dot) but in Turkish it’s dotted, İ. So while we have two i’s (upper and lower), they have four.

This feels like a bug to me, but I'm not entirely sure. It's definitely a surprising and unexpected behavior that could lead to subtle security vulnerabilities. I tried this with a few other languages to see what would happen. Maybe this is totally normal behavior.

Here's the regular expression literal I'm using for each of these test cases: `/^[a-z0-9]+$/i` The key thing to note is that the `/i` at the end is a regular expression option that specifies a case insensitive match.

```js
/^[a-z0-9]+$/i.test('ShİftKey'); // false
```

The same with Ruby. Note that the double negation is to force this method to return `true` or `false` rather than `nil` or a `MatchData` instance.

```ruby
!!/^[a-z0-9]+$/i.match("ShİftKey")  # false
```

And just for kicks, let's try Zawinski's favorite language, Perl.

```perl
if ("ShİftKey" =~ /^[a-z0-9]+$/i) {
  print "true";    
}
else {
  print "false"; # <--- Ends up here
}
```

As I expected, these did not match `ShİftKey` but did match `ShIftKey`, contrary to the C# behavior. I also tried these tests with my machine set to the Turkish culture just in case something else weird is going on.

It seems like .NET is the only one that behaves in this unexpected manner. Though to be fair, I didn't conduct an exhaustive experiment of popular languages.

## The Fix

Fortunately, in the .NET case, there's two simple ways to fix this.

```csharp
Regex.IsMatch("ShİftKey", "^[a-zA-Z0-9]+$"); // false
Regex.IsMatch("ShİftKey", "^[a-z0-9]+$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant); // false
```

In the first case, we just explicitly specify capital A through Z and remove the `IgnoreCase` option. In the second case, we use the `CultureInvariant` regular expression option.

Per [the documentation](https://msdn.microsoft.com/en-us/library/yd1hzczs\(v=vs.110\).aspx#Invariant),

> By default, when the regular expression engine performs case-insensitive comparisons, it uses the casing conventions of the current culture to determine equivalent uppercase and lowercase characters.

The documentation even notes the Turkish I problem.

> However, this behavior is undesirable for some types of comparisons, particularly when comparing user input to the names of system resources, such as passwords, files, or URLs. The following example illustrates such as scenario. The code is intended to block access to any resource whose URL is prefaced with FILE://. The regular expression attempts a case-insensitive match with the string by using the regular expression $FILE://. However, when the current system culture is tr-TR (Turkish-Turkey), "I" is not the uppercase equivalent of "i". As a result, the call to the Regex.IsMatch method returns false, and access to the file is allowed.

It may be that the other regular expression engines are culturally invariant by default when ignoring case. That seems like the correct default to me.

While writing this post, I used several helpful online utilities to help me test the regular expressions in multiple languages.

## Useful online tools

* https://replit.com/templates provides a REPL for multiple languages such as Ruby, JavaScript, C#, Python, Go, and LOLCODE among many others.
* https://www.tutorialspoint.com/execute_perl_online.php is a Perl REPL since that last site did not include Perl.
* http://regexstorm.net/tester is a regular expression tester that uses the .NET regex engine.
* https://regex101.com/#javascript allows testing regular expressions using PHP, JavaScript, and Python engines.
* https://rubular.com/ allows testing using the Ruby regular expression engine.
* [https://pythonium.net/regex](https://pythonium.net/regex) allows to visualize and test regular expressions using the Python engine.
