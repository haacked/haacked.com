---
title: An Obsessive Compulsive Guide To Source Code Formatting
tags: [oss,nuget]
redirect_from: "/archive/2011/05/21/an-obsessive-compulsive-guide-to-source-code-formatting.aspx/"
---

Most developers I know are pretty anal about the formatting of their
source code. I used to think I was pretty obsessive compulsive about it,
but then I joined Microsoft and faced a whole new level of OCD
([Obsessive Compulsive
Disorder](http://en.wikipedia.org/wiki/Obsessive%E2%80%93compulsive_disorder "OCD on Wikipedia")).
For example, many require all `using` statements to be sorted and unused
statements to be removed, which was something I never cared much about
in the past.

There’s no shortcut that I know of for removing unused using statements.
Simply right click in the editor and select **Organize Usings \> Remove
and Sort****in the context menu.

![SubtextSolution - Microsoft Visual Studio (Administrator)
(2)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Formatting-All-Source-Files-With-NuGet_DE7D/SubtextSolution%20-%20Microsoft%20Visual%20Studio%20(Administrator)%20(2)_3.png "SubtextSolution - Microsoft Visual Studio (Administrator) (2)")

In Visual Studio, you can specify how you want code formatted by
launching the Options dialog via **Tools**\> **Options** and then select
the **Text Editor** node. Look under the language you care about and
there are multiple formatting options providing hours of fun fodder for
[religious
debates](https://haacked.com/archive/2006/02/08/OnReligiousWarsinSoftware.aspx "Religious debates in software").

[![Options](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Formatting-All-Source-Files-With-NuGet_DE7D/Options_thumb.png "Options")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Formatting-All-Source-Files-With-NuGet_DE7D/Options_2.png)

Once you have the settings just the way you want them, you can select
the **Edit \> Advanced \> Format Document** (or simply use the shortcut
`CTRL + K, CTRL + D` ) to format the document according to your
conventions.

The problem with this approach is it’s pretty darn manual. You’ll have
to remember to do it all the time, which if you really have OCD, is
probably not much of a problem.

However, for those that keep forgetting these two steps and would like
to avoid facing the wrath of nitpicky code reviewers (try submitting a
patch to NuGet to experience the fun), you can install the [Power
Commands for Visual
Studio](http://visualstudiogallery.msdn.microsoft.com/e5f41ad9-4edc-4912-bca3-91147db95b99 "Power Commands")
via the Visual Studio Extension manager which provides an option to both
format the document and sort and remove using statements every time you
save the document.

I’m actually not a fan of having `using` statements removed on every
save because I save often and it tends to remove namespaces containing
extension methods that I will need, but haven’t yet used, such as
`System.Linq`.

Formatting Every Document
-------------------------

Also, if you have a large solution with many collaborators, the source
code can start to drift away from your OCD ideals over time. **That’s
why it would be nice to have a way of applying formatting to every
document in your solution.**

One approach is to purchase ReSharper, which I’m pretty sure can
reformat an entire solution and adds a lot more knobs you can tweak for
the formatting.

But for you cheap bastards, there are a couple of free approaches you
can make. One approach is to write a Macro, [like Brian Schmitt
did](http://www.brianschmitt.com/2009/09/quickly-reformat-your-project-files.html "A Macro to reformat docs").
His doesn’t sort and remove using statements, but it’s a one line
addition to add that.

Of course, the approach I was interested in trying was to use Powershell
to do it within the NuGet Package Manager Console. A couple nights ago I
was chatting with my co-worker and hacker extraordinaire, [David
Fowler](http://weblogs.asp.net/davidfowler/ "David's Blog"), way too
late at night about doing this and we decided to have a race to see who
could implement it first.

I knew I had no chance unless I cheated so I [wrote this
monstrosity](https://gist.github.com/984353 "Formatting documents") (I
won’t even post it here I’m so ashamed). David calls it “PM code”, which
in this case was well deserved as it was simply a proof of concept, but
also because it’s wrong. It doesn’t traverse the files recursively. But
hey, I was first! But I at least gave him the code needed to actually
format the document.

It was very late and I went to sleep knowing in the morning, I’d see
something elegant from David. I was not disappointed as he [posted this
gist](https://gist.github.com/984358).

He wrote a generic command named `Recurse-Project` that recursively
traverses every item in every project within a solution and calls an
action against each item.

That allowed him to easily write `Format-Document` which leverages
`Recurse-Project` and automates calling into Visual Studio’s Format
Document command.

```csharp
function Format-Document {
  param(
    [parameter(ValueFromPipelineByPropertyName = $true)]
    [string[]]$ProjectName
  )
  Process {
    $ProjectName | %{ 
      Recurse-Project -ProjectName $_ -Action {
        param($item)
        if($item.Type -eq 'Folder' -or !$item.Language) {
          return
        }
    
        $win = $item.ProjectItem.Open('{7651A701-06E5-11D1-8EBD-00A0C90F26EA}')
        if ($win) {
          Write-Host "Processing `"$($item.ProjectItem.Name)`""
          [System.Threading.Thread]::Sleep(100)
          $win.Activate()
          $item.ProjectItem.Document.DTE.ExecuteCommand('Edit.FormatDocument')
          $item.ProjectItem.Document.DTE.ExecuteCommand('Edit.RemoveAndSort')
          $win.Close(1)
        }
      }
    }
  }
}
```

Adding Commands to NuGet Powershell Profile
-------------------------------------------

Great! He did the work for me. So what’s the best way to make use of his
command? I could add it to a NuGet package, but that would then require
that I install the package first any time I wanted to use the package.
That’s not very usable. NuGet doesn’t yet support installing PS scripts
at the machine level, though it’s something we’re considering.

To get this command available on my machine so I can run it no matter
which solution is open, I need to set up my NuGet-specific Powershell
profile as [documented
here](http://docs.nuget.org/docs/start-here/using-the-package-manager-console#Setting_up_a_NuGet_Powershell_Profile "NuGet PS Profile").

The NuGet Powershell profile script is located at:

    %UserProfile%\Documents\WindowsPowerShell\NuPack_profile.ps1

The easiest way to find the profile file is to type `$profile` within
the NuGet Package Manager Console. The profile file doesn't necessarily
exist by default, but it’s easy enough to create it. The following
screenshot shows a session where I did just that.

![nuget-ps-profile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Formatting-All-Source-Files-With-NuGet_DE7D/nuget-ps-profile_3.png "nuget-ps-profile")

The `mkdir –force (split-path $profile)` command creates the
*WindowsPowershell* directory if it doesn’t already exist.

Then simply attempting to open the script in Notepad prompts you to
create the file if it doesn’t already exist. Within the profile file,
you can change PowerShell settings or add new commands you might find
useful.

For example, you can cut and paste the code in [David’s
gist](https://gist.github.com/984358 "David's Gist") and put it in here.
Just make sure to omit the first example line in the gist which simply
prints all project items to the console.

When you close and re-open Visual Studio, the `Format-Document` command
will be available in the NuGet Package Manager Console. When you run the
command, it will open each file and run the format command on it. It’s
rather fun to watch as it feels like a ghost has taken over Visual
Studio.

The script has a `Thread.Sleep` call for 100ms to work around a timing
issue when automating Visual Studio. It can take a brief moment after
you open the document before you can activate it. It doesn’t hurt
anything to choose a lower number. It only means you may get the
occasional error when formatting a document, but the script will simply
move to the next document.

The following screenshot shows the script in action.

![formatting-documents](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Formatting-All-Source-Files-With-NuGet_DE7D/formatting-documents_3.png "formatting-documents")

With this in place, you can now indulge your OCD and run the
Format-Document command to clean up your entire solution. I just ran it
against Subtext and now can become the whitespace Nazi I’ve always
wanted to be.

