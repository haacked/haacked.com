---
title: The Turkish İ Problem and Why You Should Care
tags: [code]
redirect_from: "/archive/2012/07/04/turkish-i-problem-and-why-you-should-care.aspx/"
---

Take a look at the following code.

```csharp
const string input = "interesting";
bool comparison = input.ToUpper() == "INTERESTING";
Console.WriteLine("These things are equal: " + comparison);
Console.ReadLine();
```

Let’s imagine that `input` is actually user input or some value we get from an API. That’s going to print out `These things are equal: True` right? Right?!

Well not if you live in Turkey. Or more accurately, not if the current culture of your operating system is `tr-TR` (which is likely if you live in Turkey).

To prove this to ourselves, let’s force this application to run using the Turkish locale. Here’s the full source code for a console
application that does this.

```csharp
using System;
using System.Globalization;
using System.Threading;
internal class Program
{
    private static void Main(string[] args)
    {      
        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
        const string input = "interesting";
        
        bool comparison = input.ToUpper() == "INTERESTING";

        Console.WriteLine("These things are equal: " + comparison);
        Console.ReadLine();
    }
}
```

Now we’re seeing this print out `These things are equal: False`.

To understand why this is the case, I recommend reading much more detailed treatments of this topic:

-   [Internationalization for Turkish: Dotted and Dotless Letter “I”](http://www.i18nguy.com/unicode/turkish-i18n.html "Internationalization Guy on Turkish I")
-   [What’s Wrong With Turkey](http://www.codinghorror.com/blog/2008/03/whats-wrong-with-turkey.html "What's Wrong With Turkey")

The tl;dr summary summary is that the uppercase for `i` in English is `I` (note the lack of a dot) but in Turkish it’s dotted, `İ`. So while we have two i’s (upper and lower), they have four.

My app is English only. AMURRICA!
---------------------------------

Even if you have no plans to translate your application into other languages, your application can be affected by this. After all, the
sample I posted is English only.

Perhaps there aren’t going to be that many Turkish folks using your app, but why subject the ones that do to easily preventable bugs? If you don’t pay attention to this, it’s very easy to end up with a costly security bug as a result.

The solution is simple. In most cases, when you compare strings, you want to compare them using
`StringComparison.Ordinal or StringComparison.OrdinalIgnoreCase`. It just turns out there are so many ways to compare strings. It’s not just `String.Equals`.

Code Analysis to the rescue
---------------------------

I’ve always been a fan of FxCop. At times it can seem to be a nagging nanny constantly warning you about crap you don’t care about. But hidden among all those warnings are some important rules that can prevent some of these stupid bugs.

If you have the good fortune to start a project from scratch in Visual Studio 2010 or later, I highly recommend enabling Code Analysis (FxCop has been integrated into Visual Studio and is now called Code Analysis). My recommendation is to pick a set of rules you care about and make sure that the **build breaks if any of the rules are broken**. Don’t turn them on as warnings because warnings are pointless noise. If it’s not important enough to break the build, it’s not important enough to add
it.

Of course, many of us are dealing with existing code bases that haven’t enforced these rules from the start. Adding in code analysis after the fact is a daunting task. Here’s an approach I took recently that helped me retain my sanity. At least what’s left of it.

First, I manually created a file with the following contents:

```csharp
<?xml version="1.0" encoding="utf-8"?>
<RuleSet Name="PickAName" Description="Important Rules" ToolsVersion="10.0">
  <Rules AnalyzerId="Microsoft.Analyzers.ManagedCodeAnalysis"
      RuleNamespace="Microsoft.Rules.Managed">

    <Rule Id="CA1309" Action="Error" />    
  
  </Rules>
</RuleSet>
```

You could create one per project, but I decided to create one for my solution. It’s just a pain to maintain multiple rule sets. I named this file *SolutionName.ruleset* and put it in the root of my solution (the name doesn’t matter. Just make the extension `.ruleset`)

I then configured each project that I cared about in my solution (I ignored the unit test project) to enable code analysis using this
ruleset file. Just go to the project properties and select the *Code Analysis* tab.

[![CodeAnalysisRuleSet](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/70bb1ffd73a5_C2A0/CodeAnalysisRuleSet_thumb.png "CodeAnalysisRuleSet")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/70bb1ffd73a5_C2A0/CodeAnalysisRuleSet_2.png)

I changed the selected Configuration to “All Configurations”. I also checked the “Enable Code Analysis…” checkbox. I then clicked “Open” and selected my ruleset file.

At this point, every time I build, Code Analysis will only run the one rule, [CA1309](http://msdn.microsoft.com/en-us/library/bb385972.aspx "Use ordinal StringComparison"), when I build. This way, adding more rules becomes manageable. Every time I fixed a warning, I’d add that warning to this file one at a time. I
went through the following lists looking for important rules.

-   [Design Warnings](http://msdn.microsoft.com/en-us/library/ms182125 "Code Analysis Design Rules")
-   [Globalization Warnings](http://msdn.microsoft.com/en-us/library/ms182184 "Code Analysis Globalization Rules")
-   [Interoperability Warnings](http://msdn.microsoft.com/en-us/library/ms182193 "Code Analysis Interoperability Rules")
-   [Maintainability Warnings](http://msdn.microsoft.com/en-us/library/ms182211 "Code Analysis Maintainability Rules")
-   [Mobility Warnings](http://msdn.microsoft.com/en-us/library/ms182218 "Code Analysis Mobility Rules")
-   [Naming Warnings](http://msdn.microsoft.com/en-us/library/ms182232 "Code Analysis Naming Rules")
-   [Performance Warnings](http://msdn.microsoft.com/en-us/library/ms182260 "Code Analysis Performance Rules")
-   [Portability Warnings](http://msdn.microsoft.com/en-us/library/ms182282 "Code Analysis Portability Rules")
-   [Reliability Warnings](http://msdn.microsoft.com/en-us/library/ms182287 "Code Analysis Reliability Rules")
-   [Security Warnings](http://msdn.microsoft.com/en-us/library/ms182296 "Code Analysis Security Rules")
-   [Usage Warnings](http://msdn.microsoft.com/en-us/library/ms182324 "Code Analysis Usage Rules")

I didn’t add every rule from each of these lists, only the ones I thought were important.

At some point, I reached the point where I was including a large number of rules and it made sense for me to invert the list so rather than listing all the rules I want to include, I only listed the ones I wanted to exclude.

```csharp
<?xml version="1.0" encoding="utf-8"?>
<RuleSet Name="PickAName" Description="Important Rules" ToolsVersion="10.0">
  <IncludeAll Action="Error" />
  <Rules AnalyzerId="Microsoft.Analyzers.ManagedCodeAnalysis"
      RuleNamespace="Microsoft.Rules.Managed">

    <Rule Id="CA1704" Action="None" />    
  
  </Rules>
</RuleSet>
```

Notice the `IncludeAll` element now makes every code analysis warning into an error, but then I turn CA1704 off in the list.

Note that you don’t have to edit this file by hand. If you open the ruleset in Visual Studio it’ll provide a GUI editor. I prefer to simply edit the file.

[![RuleSetEditor](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/70bb1ffd73a5_C2A0/RuleSetEditor_thumb.png "RuleSetEditor")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/70bb1ffd73a5_C2A0/RuleSetEditor_2.png)

One other thing I did was for really important rules where there were too many issues to fix in a timely manner, I would simply use Visual Studio to suppress all of them and commit that. At least that ensured that no new violations of the rule would be committed. That allowed me to fix the existing ones at my leisure.

I’ve found this approach makes using code analysis way more useful and less painful than simply turning on every rule and hoping for the best. Hope you find this helpful as well. May you never ship a bug with the Turkish I problem again!
