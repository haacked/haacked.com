---
title: Writing a NuGet Package That Adds A Command To The PowerShell Console
tags: [code,oss,nuget]
redirect_from: "/archive/2011/04/18/writing-a-nuget-package-that-adds-a-command-to-the.aspx/"
excerpt_image: https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Writing-a-NuGet-Package-That-Adds-A-Comm_C56B/magic-8-ball_3.jpg
---

The [Magic 8-ball
toy](http://en.wikipedia.org/wiki/Magic_8-Ball "Magic 8-ball") is a toy
usually good for maybe one or two laughs before it quickly gets boring.
Even so, some have been known to make all their important life/strategic
decisions using it, or an equivalent mechanism.

The way the toy works is you ask it a question, shake it, and the answer
to your question appears in a little viewport. What you’re seeing is one
side of an icosahedron (20-sided polyhedron, or for you D&D folks, a
d20). On each face of the d20 is a potential answer to your yes or no
question.

[![magic-8-ball](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Writing-a-NuGet-Package-That-Adds-A-Comm_C56B/magic-8-ball_3.jpg "magic-8-ball")](http://www.flickr.com/photos/greeblie/2426454804/ "Magic 8-Ball")

I thought it would be fun to write a
[NuGet](http://nuget.codeplex.com/ "NuGet") package that emulates this
toy as one of my demos for the [NuGet talk at
Mix11](https://haacked.com/archive/2011/04/16/a-look-back-at-mix-11.aspx "Mix11").
Yes, I am odd when it comes to defining what I think is fun. When you
install the package, it adds a new command to the Package Manager
Console.

The command I wrote didn’t have twenty possible answers, because I was
lazy, but it followed the same general format. This command also
includes support for tab expansions, which feel a lot like Intellisense.

The following screenshot shows an example of this new command,
`Get-Answer`, in use. Note that when you hit tab after typing the
command, you can see a tab expansion suggesting a set of questions. It’s
important to note that unlike Intellisense, you are free to ignore the
tab expansion here and type in any question you want.

![magic-eight-ball](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Writing-a-NuGet-Package-That-Adds-A-Comm_C56B/magic-eight-ball_3.png "magic-eight-ball")

In this blog post, I will walk through how I wrote and packaged that
command. I must warn you, I’m no PowerShell expert. I wrote this as a
learning experience with the help of other PS experts.

The first thing to do is write an *init.ps1* file. As described in the
[NuGet documentation for creating a package on
CodePlex](http://nuget.codeplex.com/documentation?title=Creating%20a%20Package "Creating a Package"):

> *Init.ps1* runs the first time a package is installed in a solution.
> If the same package is installed into additional projects in the
> solution, the script is not run during those installations. The script
> also runs every time the solution is opened. For example, if you
> install a package, close Visual Studio, and then start Visual Studio
> and open the solution, the *Init.ps1*script runs again.

This script is useful for packages that need to add commands to the
console because they’ll run each time the solution is opened. Here’s
what my *init.ps1* file looks like:

```csharp
param($installPath, $toolsPath, $package)

Import-Module (Join-Path $toolsPath MagicEightBall.psm1)
```

The first line declares the set of parameters to the script. These are
the parameters that NuGet will pass into the *init.ps1* script (note
that *install.ps1*, a **different** script that can be included in NuGet
packages, receives a fourth \$project parameter).

-   **`$installPath`** is the path to your package install
-   **`$toolsPath`** is the path to the tools directory under the
    package
-   `$package` is a reference to your package

The second line of the script is used to import a PowerShell module. In
this case, we specify a script named *MagicEightBall.psm1* by its full
path. We could write the entire script here in *init.ps1*, but I’ve been
told it’s good form to simply write scripts as modules and then import
them via *init.ps1*and I have no reason to not believe my source. I
suppose *init.ps1* could also import multiple modules rather than one.

Let’s look at the code for *MagicEightBall.psm1*. It’s pretty brief!

```csharp
$answers =  "As I see it, yes", 
            "Reply hazy, try again", 
            "Outlook not so good"

function Get-Answer($question) {
    $answers | Get-Random
}

Register-TabExpansion 'Get-Answer' @{
    'question' = { 
        "Is this my lucky day?",
        "Will it rain tonight?",
        "Do I watch too much TV?"
    }
}

Export-ModuleMember Get-Answer
```

The first line of code simply declares an array of answers. The real
Magic Eight Ball has 20 in all, so feel free to add them all there.

I then define a function named `Get-Answer`. The implementation
demonstrates one of the cool things I like about Powershell. I can
simply pipe it into the `Get-Random` method and it returns a random
answer from the array.

Skipping to the end, the last line of code calls `Export-Module` on this
function, which makes it available in the Package Manager Console.

So what about that middle bit of code that calls
`Register-TabExpansion`? Glad you asked. That function provides the
Intellisense-like behavior for our function by registering a tab
expansion.

It takes two parameters, the first is the name of the function, in this
case `Get-Answer`. The second is a dictionary where the keys are the
names of the parameters of the function, and the values contain an array
of expansion options for that function. Since are function only has one
parameter named `question`, we add `'question'` as the key to the
dictionary and supply an array of potential questions as the value.

With these two files in place, I simply opened up [Package
Explorer](http://nuget.codeplex.com/releases "NuGet Releases") and
selected *File* \> *New* from the menu to start a new package and
dragged both of the script files into the *Package contents*window.
NuGet recognized the files as being PowerShell scripts and offered to
put them in the *Tools* folder.

I then selected *Edit* \> *Edit Package Metadata* from the menu to enter
the NuSpec metadata for the package and clicked *OK* at the bottom.

[![magic-eight-ball-pkg](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Writing-a-NuGet-Package-That-Adds-A-Comm_C56B/magic-eight-ball-pkg_thumb.png "magic-eight-ball-pkg")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Writing-a-NuGet-Package-That-Adds-A-Comm_C56B/magic-eight-ball-pkg_2.png)

With all that done, I selected the *File* \> *Save As…* menu to save the
package on disk so I could test it out. Once I was done testing, I
selected *File* \> *Publish* to publish the package to the real NuGet
feed.

It’s really that simple to write a package that adds a command to the
Package Manager console complete with tab expansions.

In a future blog post, I’ll write about how I wrote *MoodSwings*, a
package that can automate Visual Studio from within the Package Manager
Console. If you have the NuGet Package Manager Console open, you can try
out this package by running the command:

    Install-Package MagicEightBall

