---
title: Customizing Keyboard Settings In RSS Bandit Part 2
tags: [rss]
redirect_from: "/archive/2005/03/21/CustomizingKeyboardSettingsPart2.aspx/"
---

[In part
1](https://haacked.com/archive/2005/03/22/CustomizingKeyboardSettingsPart1.aspx),
I bored you with excrutiating detail about how we keyboard shortcuts are
implemented in RSS Bandit. In this second more exciting part, I'll
outline how to use a tool I wrote for editing shortcut settings. You can
consider it sort of an unofficial RSS Bandit Power Toy. When we really
nail down a nice UI for editing shortcuts, we'll possibly add this as a
configuration dialog within RSS Bandit. But for now, this is an
experimental utility, use at your own risk.

To get started with the utility, download a [debug version
here](https://haacked.com/code/ShortcutsEditor.zip).

**First Time Usage**\
 Figure 1 shows a screenshot of the editor as it looks when you first
run it (assuming you don't already have a ShortcutSettings.xml file in
your application data path). When you launch the editor, it
automatically checks the default application data folder for a file
named ShortcutSettings.xml. In this case, none was found so everything
is blank.

![Shortcut Editor - First Time](/assets/images/rssbandit_ShortcutEditor.gif)
 **Figure 1:** Shortcuts Editor, no file specified.

**Create a ShortcutSettings File**\
 The next step is quite simple, click on the Create button to specify a
location to save a copy of the default settings file. The Save File
dialog opens in the application data directory (see figure 2). This is
where you'll want to save the settings file unless you've modified your
RSSBandit.exe.config file and specified a different location for RSS
Bandit's application data by adding an appSetting entry for
"AppDataFolder" (generally not recommended).

![Shortcut Editor Save Settings Dialog](/assets/images/rssbandit_ShortcutEditorSaveSettings.gif)
 **Figure 2:** Shortcut Editor Save Settings Dialog.

**Modify Settings**\
 Once you click the "Save" button, you'll see that the "Menu Shortcuts"
and "Filter Shortcuts" are populated. Setting a menu shortcut is as
simple as selecting the command you want to change in the "Command"
droppdown and selecting a new value in the "Shortcut" dropdown and then
clicking the "Set" button.

Setting Filter Shortcuts (PreFilterMessage method) works a bit
differently. Next to the "Command" dropdown you'll see a list of "Keys".
Each entry in this list corresponds to a \<keyCombination\> entry in the
config file (as discussed in part 1). To add a new entry, click on the
"Add" button which brings up a Shortcut Entry Form as in figure 3.

![Shortcut Editor Entry
Form](/assets/images/rssbandit_ShortcutEditorEntryForm.gif) \
 **Figure 3:** Shortcut Editor Entry Form.

This little form implements IMessageFilter itself, so when you click in
the text area, it interprets your keystrokes and displays the keys
you've pressed. You can specify a key combination by pressing the
individual keys one at a time. For example, if you wanted a command to
be invoked by pressing *ALT+F4+F5* all at the same time, you'd type
*ALT* followed by *F4* followed by *F5*.

When you are satisfied with your key combination, click "OK". This will
result in a new key combination being mapped to the command as shown in
the closeup in Figure 4.

![Shortcut Editor Detail With New Filter
Entry](/assets/images/rssbandit_ShortcutEditorAfterAdd.gif) \
 **Figure 4:** Shortcut Editor Detail With New Filter Entry.

**Saving Your Settings**\
 Finally, don't forget to click the "Save" button to save your new
settings. This will bring up a dialog (like the Create button did)
allowing you to specify where to save the file. Feel free to save
multiple versions of these settings files and share them with friends
and family.

If for some odd reason, a change you make causes RSS Bandit to crash a
horrible flaming death, merely delete the ShortcutSettings file or
Create a new one and start from scratch.

**Conclusion**\
 If you're a keyboard control maniac, I hope this temporarily satisfies
your thirst for being able to configure keyboard settings within RSS
Bandit. As this is an ongoing project, there will be improvements and
your suggestions will be taken into consideration.

