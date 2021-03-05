---
title: Strongly Typed Stored Procedures Using Subsonic
tags: [sql,orm]
redirect_from: "/archive/2007/05/28/strongly-typed-stored-procedures-using-subsonic.aspx/"
---

I don’t know about you, but I find it a pain to call stored procedures
from code. Either I end up writing way too much code to specify each
`SqlParameter` explicitly, or I use a tool like Microsoft’s Data Access
Application Block’s `SqlHelper` classj to pass in the parameter values,
which requires me to remember the correct parameter order (it actually
supports both methods of calling a stored procedure). What a pain!

**What I need is a strongly typed stored procedure**. Something that’ll
tell me which parameters to pass and will break at compile time if the
parameters change in some way.

Subsonic can help with that. In general, Subsonic is most productive
when combining its code generation with its dynamic query engine and
Active Record. But sometimes, your stuck with Stored Procedures and want
to make the best of it. Subsonic, via the sonic.exe command line tool,
can generate strongly typed stored procedure wrappers saving you from
writing a lot of boilerplate code.

I recently just finished updating Subtext to call all its stored
procedures using Subsonic generated code. **This post will walk you
through setting up a toolbar button in Visual Studio.NET 2005 to do
this, using Subtext as the example**. This pretty much follows the
example that Rob set [in this
post](http://blog.wekeroad.com/archive/2007/01/13/SubSonic-Console-Groovy-VS-Shortcuts.aspx "Groovy VS Shortcuts").

First, I made sure to put the latest and greates sonic.exe and
SubSonic.dll in a known location. In Subtext, this is the dependencies
folder, which on my machine is located:

`d:\projects\Subtext\trunk\SubtextSolution\Dependencies\`

The next step is to create a new External Tool button by selecting
*External Tools...*from the Tools Menu.

![External
Tools...](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/VS.NETShortcutForGeneratingStoredProcCal_145CA/vsexternaltools4.png)

This will bring up the following dialog.

![External Tools
Dialog](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/VS.NETShortcutForGeneratingStoredProcCal_145CA/ExternalTools24.png)

I filled in the fields like so:

-   **Title:** Subtext Subsonic SPs
-   **Command:**
    D:\\Projects\\Subtext\\trunk\\SubtextSolution\\Dependencies\\sonic.exe
-   **Arguments:** generatesps /config "\$(SolutionDir)Subtext.Web" /out
    "\$(SolutionDir)Subtext.Framework\\Data\\Generated"
-   **Initial Directory:** \$(SolutionDir)

This tells Sonic.exe to find the Subsonic configuration within the
*Subtext.Web* folder, but generate the stored procedure wrappers in a
subfolder of the *Subtext.Framework* project.

With that in place, I then created a new Toolbar by selecting
*Customize* from the *Tools* menu which brings up the following dialog.

![Customize
Dialog](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/VS.NETShortcutForGeneratingStoredProcCal_145CA/Customize4.png)

Click on the *New...* button to create a new toolbar.

![New
Toolbar](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/VS.NETShortcutForGeneratingStoredProcCal_145CA/NewToolbar5.png)

I called mine *Subsonic*. This adds a new empty toolbar to VS.NET. Now
all I need to do is add my *Subtext Stored Procedures* button to it.
Just click on the *Commands* tab.

![Customize
Commands](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/VS.NETShortcutForGeneratingStoredProcCal_145CA/vssubsoniccommandsdialog4.png)

Unfortunately, the External Tools command is not named in this dialog.
However, since I know the first command is the one I want (it’s the same
order as it is listed in the Tools Menu), I drag *External Command 1* to
my new Subsonic toolbar.

![Subtext SPs
button](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/VS.NETShortcutForGeneratingStoredProcCal_145CA/subtextsptoolbarbutton4.png)

So now when I make a change to a stored procedure, or add/delete a
stored procedure, I can just click on that button to regenerate the code
that calls my stored procedures.
