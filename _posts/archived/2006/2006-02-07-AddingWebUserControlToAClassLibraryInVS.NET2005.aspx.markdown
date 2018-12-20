---
title: Adding Web User Control To A Class Library In VS.NET 2005
date: 2006-02-07 -0800
disqus_identifier: 11728
tags: []
redirect_from: "/archive/2006/02/06/AddingWebUserControlToAClassLibraryInVS.NET2005.aspx/"
---

If you’ve started on module development with DotNetNuke 4.0 and above in
Visual Studio.NET 2005, you might run into a problem with trying to add
a Web User Control (\*.ascx file) to a class library.

The fix is similar to what you had to do with Visual Studio.NET 2003.

For C\#, follow these steps.

1.  Close VS.NET 2005.
2.  Open the directory **C:\\Program Files\\Microsoft Visual Studio
    8\\Web\\WebNewFileItems\\CSharp** (assuming a default installation
    of VS.NET).
3.  Open the **CSharpItems.vsdir** file in Notepad. Select the text and
    copy it to the clipboard.
4.  Now open up the file **C:\\Program Files\\Microsoft Visual Studio
    8\\VC\#\\CSharpProjectItems\\CSharpItems.vsdir** and paste the
    contents of the clipboard underneath the existing text.
5.  Now copy the contents of **C:\\Program Files\\Microsoft Visual
    Studio 8\\Web\\WebNewFileItems\\CSharp** (excluding
    CSharpItems.vsdir) into the folder **C:\\Program Files\\Microsoft
    Visual Studio 8\\VC\#\\CSharpProjectItems**.

Now “Web User Control” should be an option when you select *Add | New
Item*.

