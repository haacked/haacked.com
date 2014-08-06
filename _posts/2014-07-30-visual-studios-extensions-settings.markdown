---
layout: post
title: "Settings for your Visual Studio Extension"
date: 2014-07-30 -0800
comments: true
categories: [vs vsix dev encouragement]
---

Recently I wrote what many consider to be the most important Visual Studio Extension ever shipped -  [Encourage for Visual Studio](http://haacked.com/archive/2014/06/20/encourage-vs/). It was my humble attempt to make a small corner of the world brighter with little encouragements as folks work in Visual Studio. You can get it via the Visual Studio Extension Manager.

But not everyone has a sunny disposition like I do. Some folks want to watch the world burn. What they want is _Discouragements_.

Well an idiot might write a whole other Visual Studio Extension with a set of discouragements. I may be many things, but I am no idiot. This problem is better solved by allowing users to configure the set of encouragements to be anything they want.

And that's what I did. I added an Options pane to allow users to configure the set of encouragements. It turned out to be a more confusing ordeal than I expected. But with some help from [Jared Parsons](http://blog.paranoidcoding.com/), I may now present to you, discouragements!

![Encourage options](https://cloud.githubusercontent.com/assets/19977/3739511/4d354428-174b-11e4-86a8-917e8abe300c.png)

So if you're of the masochistic inclination, you can treat yourself to custom discouragements all day long if you so choose.

![Discouragement in use](https://cloud.githubusercontent.com/assets/19977/3834589/161bd9f2-1db3-11e4-8e81-85bdf32846b6.png)

As you can see from the screenshot, it __supports native emoji!__. If you want these for yourself, I [posted them into a gist](https://gist.github.com/Haacked/1c51925deddb254a0422). 

## Challenges and Travails

So why was this challenging? Well like many things with development platforms, to do the basic thing is really easy, but when you want to deviate, things become hard.

If you follow the [Walkthrough: Creating an Options Page](http://msdn.microsoft.com/en-us/library/bb166195.aspx) you'll be able to add settings to your Visual Studio extension pretty easily. Using this approach, you can even rely on Visual Studio to generate a properties UI for you.

![basic options](https://cloud.githubusercontent.com/assets/19977/3739630/a726fd0e-174c-11e4-98b7-036f08f0c625.png)

But that's pretty rudimentary.

What I wanted was very simple, I wanted a multi-line text box that let you type in or paste in an encouragement per line. So I derived from [`DialogPage`](https://www.google.com/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#q=DialogPage) as you do, created a WPF User Control with a `TextBox`. I added the user control to an [`ElementHost`](http://msdn.microsoft.com/en-us/library/system.windows.forms.integration.elementhost\(v=vs.110\).aspx), a Windows Forms control that can host a WPF control because, apparently, the Options dialog is still hosting Windows Forms controls.

This approach was easy enough, but the text box didn't accept any of my input. I ran into the same problem as [this person writes about StackOverflow](http://stackoverflow.com/questions/835878/wpf-textbox-not-accepting-input-when-in-elementhost-in-window-forms/5315184#5315184).

I could cut and paste into the `TextBox`, but I couldn't type anything. That's not very useful.

I wasn't interested in overriding [`WndProc`](http://msdn.microsoft.com/en-us/library/system.windows.forms.control.wndproc\(v=vs.110\).aspx) mainly because I feel I shouldn't have to. Instead I gave up on WPF, and ported it over to a regular Windows Forms user control. That allowed me to type in the textbox, but if I hit the `Enter` key, instead of adding a newline, the OK button stole it. So I couldn't actually add more than one encouragement.

## UIElementDialogPage

Thankfully, Jared pointed me to the [`UIElementDialogPage`](http://msdn.microsoft.com/en-us/library/vstudio/microsoft.visualstudio.shell.uielementdialogpage.dialogkeypendingevent\(v=vs.110\).aspx).

__If you want to provide a WPF User Control for your Visual Studio Extension, derive from `UIElementDialogPage` and not `DialogPage` like all the samples demonstrate!__

It does all the necessary `WndProc` magic under the hood for you. Note that it was introduced in Visual Studio 2012 so if you take a dependency on it, your extension won't work in Visual Studio 2010. Live in the present I always say.

## Storing Settings

The other thing I learned is that `AppSettings` is _not_ the place to save your extension's settings. As Jared explained,

> The use of application settings is not version safe in a VSIX. The location of the stored setting file path in part includes the version string and hashes of the executable.  When Visual Studio installs an official update these values change and as a consequence change the setting file path.  Visual Studio itself doesn't support the use of application settings hence it makes no attempt to migrate this file to the new location and all information is essentially lost.  

> The supported method of settings is the [`WritableSettingsStore`](http://msdn.microsoft.com/en-us/library/microsoft.visualstudio.settings.writablesettingsstore.aspx).  It's very similar to application settings and easy enough to access via `SVsServiceProvider` 

```csharp
public static WritableSettingsStore GetWritableSettingsStore(this SVsServiceProvider vsServiceProvider)
{
    var shellSettingsManager = new ShellSettingsManager(vsServiceProvider);
    return shellSettingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
}
```

If this is interesting to you, I encourage (tee hee) you to read through [the Pull Request that adds settings to the Encourage pull request](https://github.com/Haacked/Encourage/pull/27). You can read through the commits to watch me flailing around, or you can read the final DIFF to see what changes I had to make.

_PS: If you liked this post [follow me on Twitter](https://twitter.com/haacked) for interesting links and my wild observations about pointless drivel_
