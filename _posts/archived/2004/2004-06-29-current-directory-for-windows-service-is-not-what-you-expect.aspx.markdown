---
title: Current Directory For Windows Service Is Not What You Expect
date: 2004-06-29 -0800
tags: [tech]
redirect_from: "/archive/2004/06/28/current-directory-for-windows-service-is-not-what-you-expect.aspx/"
---

At least it wasn't what I expected.  By default, the current directory
for your Windows service is the System32 folder.  I keep forgetting that
which causes me problems when I try to access a file or folder using a
relative path.

```csharp
System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
```

Use the above line of code to set the current directory to the same
directory as your windows service. Don't say I didn't warn you. I shall
never forget again.

