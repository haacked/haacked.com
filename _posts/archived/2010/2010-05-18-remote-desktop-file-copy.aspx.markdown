---
title: Copying Files Over Remote Desktop
tags: [tips,tech]
redirect_from: "/archive/2010/05/17/remote-desktop-file-copy.aspx/"
---

Here’s a handy tip I just recently learned from the new intern on our
team (*see, you can learn something from anyone on any given day*). I’ve
long known you could access your local drives from a remote machine.

For example, start up a remote desktop dialog.

![Remote Desktop Dialog](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CopyingFilesOverRemoteDesktop_10331/remote-desktop-dialog_3.png "remote-desktop-dialog")

Then expand the dialog by clicking on *Options*, then check the
*Local**Resources* tab.

[![Remote Desktop Local Resources Options](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CopyingFilesOverRemoteDesktop_10331/remote-desktop-options_thumb.png "remote-desktop-options")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CopyingFilesOverRemoteDesktop_10331/remote-desktop-options_2.png)

Make sure *Clipboard* is checked, and then hit the *More…* button.

![Remote Desktop Drives](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CopyingFilesOverRemoteDesktop_10331/remote-desktop-drives_3.png "remote-desktop-drives")

Now you can select a local disk to be shared with the remote machine.
For example, in this case I selected my *C:* drive.

[![Local Drive shared on Remote Desktop](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CopyingFilesOverRemoteDesktop_10331/local-drive-on-rd_thumb.png "local-drive-on-rd")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CopyingFilesOverRemoteDesktop_10331/local-drive-on-rd_2.png)As
you can see in the screenshot, the file explorer has another drive named
“*C on HAACKBOOK*” which can be used to copy files back and forth from
my local machine to the remote machine.

But here’s the part I didn’t know. Let’s take a look at the desktop of
my remote machine, which has a text file named *info.txt*.

[![Remote Desktop](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CopyingFilesOverRemoteDesktop_10331/remote-desktop_thumb_2.png "remote-desktop")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CopyingFilesOverRemoteDesktop_10331/remote-desktop_6.png)

One way I can get that file to my local machine is to copy it to the mapped
drive we saw in the previous screenshot.

Or, **I can simply drag and drop the info.txt from my remote desktop
machine to a folder on my local machine.**

![stuff](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CopyingFilesOverRemoteDesktop_10331/stuff_3.png "stuff")

So all this time, I had no idea cut and paste operations for files work
across remote desktop. This may be obvious for many of you, but it
wasn’t to me. :)

