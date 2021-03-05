---
title: Visual Studio.NET New Project Template Folders
tags: [visualstudio]
redirect_from: "/archive/2005/03/29/visual-studionet-new-project-template-folders.aspx/"
---

I'm sure you're constantly asking yourself this, because I certainly
wake up every morning in a cold sweat wondering. When you add a new
project in Visual Studio.NET 2003 (friends call her VS.NET), you get the
following dialog

![Add New Dialog](/assets/images/AddProjectDialog.gif)
 The beginnings of another bug ridden coding section...

Now looking at that familiar dialog underneath the "Project Types:"
section, you probably noticed the usual suspects are there: "Visual C#
Projects", "Visual Basic Projects", etc... But did you also notice
there's a few other folders there such as "Visual C# Projects for
DotNetNuke 3"?

Now for the million dollar question: **How do you add your own folder
there?**

Reading through what I could find online, I understand how to use your
.vsdir and .vsz files to create a new template, but I couldn't find
anything that described how to create your own project type grouping.

So I did a little digging through the registry and found the following
registry location:

```
HKEY_LOCAL_MACHINE\
    SOFTWARE\
        Microsoft\
            VisualStudio\
                7.1\
                    NewProjectTemplates\
                        TemplateDirs\
```

Just to make sure that adding keys to this location in the registry was
sufficient, I brashly took Regedit (without even backing up my registry,
an incredibly stupid thing to do), and created a new sub key, using SQL
Query Analyzer and the newid() function to generate a new GUID for me.
Under that key I added a sub key named "/1". Under that key I set the
following three values as seen in this screen shot.

![Registry Settings](/assets/images/VSTemplateDirRegistrySettings.gif) \
 Must this egotistical idiot use his last name in everything?

And here you can see the registry keys structure. The one I added is at
bottom.

![Registry Keys](/assets/images/VSTemplateDirRegistryKeys.gif) \
 Ahh, "Haack" is nowhere to be seen. Forturnately it's not a proper
GUID.

So the next step is to create a VS.NET Setup and Deployment project to
package my templates and add this registry setting automatically. Hope
you can sleep peacefully now.
