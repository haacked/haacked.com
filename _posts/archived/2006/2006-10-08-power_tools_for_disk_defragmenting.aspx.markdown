---
title: Power Tools For Disk Defragmenting
tags: [tools]
redirect_from: "/archive/2006/10/07/power_tools_for_disk_defragmenting.aspx/"
---

### Disk Defragmenter

For the most part, the *Disk Defragmenter* application (located at
*%SystemRoot%\\system32\\dfrg.msc*) that comes with Windows XP does a
decent enough job of defragmenting a hard drive for most users.

But if you’re a developer, you are not like most users, often dealing
with very large files and installing and uninstalling applications like
there’s no tomorrow.  For you, there are a couple of other free
utilities you should have in your utility belt.

Recently I noticed my hard drive grinding a lot.  After defragmenting my
drive, I clicked on the *View Report* button this time (I normally never
do this out of hurriedness).

![Disk Defrag
Dialog](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefragmentThatHardDrive_D71C/DefragComplete4.png)

This brings up a little report dialog.

![Defrag
Report](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefragmentThatHardDrive_D71C/DefragReport4.png)

And in the bottom, there is a list of files that *Disk Defragmenter*
could not defragment.  In this case, I think the file was simply too
large for the poor utility.  So I reached into my utility belt and
whipped out
[Contig](http://www.sysinternals.com/Utilities/Contig.html "Sysinternals Contig").

### Contig

Contig is a command line utility from SysInternals that can report on
the fragmentation of individual files and defrag an individual file.

I opened up a console window, changed directory to the Backup
directory, and ran the command:

`contig *.tib`

Which defragmented every file ending with the *tib* extension (in this
case just one).  This took a good while to complete working against a 29
Gig file, but successfully reduced the fragmens from four to two, which
made a huge difference.  I may try again to see if it can bring it down
to a single fragment. 

I ran *Disk Defragmenter* again and here are the results.

![Disk
Defragmenter](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefragmentThatHardDrive_D71C/DefragBeforeAndAfter14.png)

Keep in mind that the disk usage before this pass with the defragger
was the usage after running *Disk Defragmenter* once.  After using
contig and then defragging again, I received much better results.

### PageDefrag

Another limitation of *Disk Defragmenter* is that it cannot defragment
files open for exclusive access, such as the Page File.  Again, reaching
into my utility belt I pull yet another tool from Sysinternals (those
guys rock!),
[PageDefrag](http://www.sysinternals.com/Utilities/PageDefrag.html "Page Defrag").

Running PageDefrag brings up a list of page files, event log files,
registry files along with how many clusters and fragments make up those
files.

![Page
Defrag](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefragmentThatHardDrive_D71C/PageDefrg4.png)

This utility allows you to specify which files to defrag and either
defragment them on the next reboot, or have them defragmented at every
boot.  As you can see in the screenshot, there was only one fragmentted
file, so the need for this tool is not great at the moment.  But it is
good to have it there when I need it.

With these tools in hand, you are ready to be a defragmenting ninja.

