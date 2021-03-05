---
title: Customizing Keyboard Settings In RSS Bandit Part 1
tags: [rss]
redirect_from: "/archive/2005/03/21/CustomizingKeyboardSettingsPart1.aspx/"
---

![Keyboard](/assets/images/Keyboard.jpg) You might not know this, but RSS
Bandit supports customizing keyboard shortcuts via an XML configuration
file. The reason you might not know this is because it is an
undocumented feature. Since I implemented adding customizability to
keyboard shortcuts, I thought I might as well document how it works as
of [version 1.3.02.26](https://haacked.com/archive/2005/03/20/rssbandit-13026-released.aspx/).

First, I'm going to delve a bit into how Keyboard shortcuts are
implemented in RSS Bandit before I highlight a tool I wrote for
modifying the settings without having to dink around with the XML. As
you read this discussion, imagine that I've placed a

using System.Windows.Forms;

at the top of this article. (Or for you VB.NET lovers out there, an
Imports System.Windows.Forms).

**Methods of Handling Keyboard Events**\
 There are two methods for handling keyboards shortcuts in RSS Bandit.
The first is by simply setting a `Shortcut` property of a `MenuItem`
instance to a proper `Shortcut` enum value. Perhaps a very simple
example will make it clear.

MenuItem item = new MenuItem();

item.Shortcut = Shortcut.AltF4;

In the above example, the key combination of *Alt* and *F4* is mapped to
a menu item. When the user presses the key combination of *ALT + F4*,
that is equivalent to the user clicking on that menu item.

The second method is by implementing the `PreFilterMessage` method of
the `IMessageFilter` interface on the main form.

This method allows us to intercept Windows messages (specifically
keystrokes) before they are dispatched to a control or the main form.
Here's a snippet of the implementation of the PreFilterMessage method:

public virtual bool PreFilterMessage(ref Message m) {

    bool processed = false;

    const int WM\_KEYDOWN = 0x100;

    const int WM\_SYSKEYDOWN = 0x104;

 

    try {

        if (m.Msg == WM\_KEYDOWN || m.Msg == WM\_SYSKEYDOWN) {

           

            Keys msgKey = ((Keys)(int)m.WParam & Keys.KeyCode); ... SNIP
...

At the end of this snippet, you'll notice there's a variable named
msgKey of type
[Keys](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfSystemWindowsFormsKeysClassTopic.asp).
This is a bitmask of the pressed keys that we'll use to determine which
shortcut is being invoked. Make note of it as we'll mention it again
later.

**Associating Settings to a Command**\
 Ok, so now that we have a rudimentary understanding of how the code can
handle a keyboard event, let's look at how we configure the settings. As
you might guess, we have two ways to configure a keyboard shortcut based
on whether it falls under method 1 or method 2. In both cases, a
keyboard shortcut is associated with a command name. For example, the
"cmdCloseExit" command closes and exits RSS Bandit. Since there's a menu
item associated with this command, we simply associate a Shortcut enum
value to it.

For the command "GiveFocusToUrlTextBox", however, we need to use the
PreFilterMessage approach. So we specify a comma separated list of Keys
enum values. In this case, we have two different key combinations mapped
to that command - F11 or Alt + D.

**App Data Path**\
 These settings are configured in a file named ShortcutSettings.xml. In
a default installation of RSS Bandit, that file is compiled into the
executable as a resource. However, you can override the default settings
by placing a file named "ShortcutSettings.xml" (in the correct format)
in the User Application Data folder for RSS Bandit. On my system the
path is

` C:\Documents and Settings\Phil\Application Data\RssBandit\`

This file is a bit fragile, so be careful if you modify it by hand. It
requires that every shortcut command have a definition. Below is an
example that shows the structure of the shortcut settings file. You'll
notice that under the root \<shortcut\> node, there are two main nodes:
\<menu\> and \<keyboardCombinations\>

```csharp
<?xml version="1.0" encoding="utf-8" ?> 
<shortcuts>
    <menu>
        <shortcut display="true">
            <command>cmdNewFeed</command>
            <shortcutEnumValue>F4</shortcutEnumValue>
        </shortcut>
        ... more shortcuts ...
    </menu>
    <keyboardCombinations>
        <shortcut>
            <command>GiveFocusToUrlTextBox</command>
            <keyCombination>F11</keyCombination>
            <keyCombination>Alt,D</keyCombination>
        </shortcut>    
        ... more shortcuts ...
    </keyboardCombinations>
</shortcuts>
```

Now we get to the reason for that whole discussion of the two types of
shortcuts. In order to configure a shortcut correctly, you need to know
which type it is, which is easily done by looking at the existing
ShortcutSettings.xml file.

**Configuring a Menu Shortcut**\
 Configuring a "menu" shortcut is very easy. Just specify a valid
Shortcut enumeration value in the \<shortcutEnumValue\> node.

Configuring a Keyboard Combination Shortcut\
 Configuring a shortcut that's invoked via PreMessageFilter's a little
more complex. First of all, it's possible to have more than one key
combination map to a single command. Hence the multiple
\<keyCombination\> elements. For each \<keyCombination\> you can specify
a comma separated list of valid Keys enumeration values.

One thing to note with the keyboard combination shortcuts is that it is
possible (in some cases) to have the same key combination mapped to two
different commands. This is because some commands are dependent on which
control has focus. Unfortunately, the dependency of a shortcut on a
control is not clearly mapped via the configuration file. That is
definitely something worth looking into for a future release. The
potential drawback to adding a controlname to the settings schema is the
performance penalty of using reflection to determine if a control has
focus. The potential benefit is that it may enable the code to be
cleaner in the PreFilterMessage method.

**Invoking a Command**\
 So now you're ready to press a key on your keyboard, what happens next?
Well in the case of a menu shortcut, that's handled by the operating
system. For the nitty gritty, check out [this blog
post](http://blogs.msdn.com/jfoscoding/archive/2005/01/24/359334.aspx).
The menu items are assigned their shortcut value via the
`ShortcutHandler` class I wrote. This class reads in the configuration
file and a menu item is mapped to its Shortcut enum value by simply
calling

public Shortcut GetShortcut(string command)

In the case of a keyboard combination shortcut (i.e. PreMessageFilter),
things are a little more tricky. We have a big chain of if else
statements that run through the commands and checks each command to see
if it was invoked and if the control associated with that command has
focus (see snippet below).

if (this.listFeedItems.Focused &&
\_shortcutHandler.IsCommandInvoked("CollapseListViewItem", m.WParam))

The `IsCommandInvoked` method first extracts a Keys enum bitmask from
the m.WParam value passed in, which represents the keys that the user
has pressed and is equivalent to the msgKey variable described earlier
(I told you I'd get back to it). Afterwards, it iterates through each
\<keyCombination\> value associated to the command being checked and
combines the comma separated values into a Keys enumeration bitmask.
This bitmask is compared to the extracted bitmask. As soon as a match is
found, it returns true, otherwise it returns false. In this manner, we
can determine which command is being invoked by a key combination.

**Ok, So How Do I Configure Keyboard Settings Without Mucking Around
With XML?**\
 Well now that I've given you this background, which probably contains
more than you'll ever want to know about how keyboard shortcuts are
implemented in RSS Bandit, I must defer to [part 2 of this
series](https://haacked.com/archive/2005/03/22/CustomizingKeyboardSettingsPart2.aspx)
where I describe a simple utility I wrote for setting up shortcuts.

