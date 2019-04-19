---
title: VS.NET Add-In For Source Code Formatting as HTML
tags: [tools]
redirect_from: "/archive/2004/10/12/vsnet-add-in-for-source-code-formatting-as-html.aspx/"
---

[Colin](http://www.jtleigh.com/people/colin/blog/) sent me an email
pointing me to an [add-in he
wrote](http://www.jtleigh.com/people/colin/blog/archives/2004/10/visual_studio_a.html)
for VS.NET that allows you to copy selected source code to the clipboard
as syntax highlighted HTML.

By selecting some code and right clicking the code editor, you'll see an
option to Copy Source as HTML.

Selecting that menu item brings up a dialogue where you can configure
some options. It's based on my favorite [code to HTML formatter by
Manoli](http://www.manoli.net/csharpformat/).

Below is an example of a code snippet using this tool:

```csharp
/// <SUMMARY>
/// Sets the stack trace for the given lock target 
/// if an error occurred.
/// </SUMMARY>
/// <PARAM name="lockTarget">Lock target.</PARAM>
public static void ReportStackTrace(object lockTarget)
{
  lock(_failedLockTargets)
  {
    if(_failedLockTargets.ContainsKey(lockTarget))
    {
        ManualResetEvent waitHandle = 
            _failedLockTargets[lockTarget] 
                 as ManualResetEvent;
        if(waitHandle != null)
        {
            waitHandle.Set();
        }
        _failedLockTargets[lockTarget] = new StackTrace();
        //TODO: Now's a good time to use your
        //favorite logging framework.
    }
  }
}
```

Colin, thanks for pointing me to this. This is freakin' **awesome!**

Now, if I could have a short-cut that would use the default options and
immediately put the selected source in the clip-board, that would just
rock my world. Also, one minor niggle I've had with the Manoli formatter
is that the xml comments tags and the triplle slashes (such as /// )
should be gray to mimic VS.NET instead of all green. How hard would it
be to fix that?

