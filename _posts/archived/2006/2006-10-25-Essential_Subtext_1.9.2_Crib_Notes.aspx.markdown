---
title: Essential Subtext 1.9.2 Crib Notes
tags: [subtext]
redirect_from: "/archive/2006/10/24/Essential_Subtext_1.9.2_Crib_Notes.aspx/"
---

UPDATE: A bug was reported that blog posts could not be deleted. We
have updated the release files [with a fixed
version](https://haacked.com/archive/2006/10/26/Subtext_1.9.2_Bugfix_Update.aspx "Subtext 1.9.2 Bugfix Update"). 
There’s also a [quick and dirty
workaround](https://haacked.com/archive/2006/10/26/PATCH_Cannot_Delete_Posts_In_Subtext_1.9.2.aspx "Quick and Dirty Workaround").

Reading over my [last blog
post](https://haacked.com/archive/2006/10/25/Subtext_1.9.2_quotShields_Upquot_Edition_Released.aspx "Subtext Shields Up Released!"),
I realized I can be a bit long winded when describing the latest release
of Subtext. Can you blame me? I pour my blood, sweat, and tears (minus
the blood) into this project.  Doesn’t that give me some leeway to be a
total blowhard?
![Wink](https://haacked.com/assets/images/emotions/smiley-wink.gif)

This post is for those who just want the meat. Subtext 1.9.2 has the
goal of making the world safe for trackbacks and comments.  It adds
significant comment spam blocking support.  Here are the key take-aways
for upgrading.

-   As always, backup your database and site first before upgrading.  We
    implemented a major schema change which requires that the upgrade
    process move some data to a new table.
-   If you are upgrading from Subtext 1.5 or earlier, [read
    this](https://haacked.com/archive/2006/08/31/Important_Note_On_Upgrading_to_Subtext_1.9.aspx "Important note on upgrading to 1.9").
-   [Instructions for
    upgrading](http://subtextproject.com/Home/Docs/Upgrading/tabid/147/Default.aspx "Upgrading Subtext").
-   [Instructions for a clean
    installation.](http://subtextproject.com/Home/Docs/Installation/tabid/111/Default.aspx "Installation")
    This is easier than upgrading.
-   When upgrading, make sure to merge your customizations to web.config
    into the new web.config.
-   If you use Akismet, make sure not to use ReverseDOS until we resolve
    some issues.
-   After upgrading, login to the admin and select the correct timezone
    that **you** are located in.

[Download it
here](https://sourceforge.net/project/showfiles.php?group_id=137896&package_id=181920&release_id=458502 "Download Subtext").

