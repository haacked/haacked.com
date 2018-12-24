---
title: Developing Custom Skins
date: 2006-08-26 -0800
tags: [subtext]
redirect_from: "/archive/2006/08/25/Developing_Custom_Skins.aspx/"
---

This is my third post about Skinning in Subtext. Previously I talked
about some [breaking
changes](https://haacked.com/archive/2006/08/26/Subtext_Skinning_Changes.aspx). 
Then I gave a high level overview of [skinning in
Subtext](https://haacked.com/archive/2006/08/26/Mile_High_Overview_Of_Subtext_Skinning.aspx). 
In this post I want to mention one new feature for those who use custom
skins.

Subtext 1.9 actually *reduces* the the number of pre-packaged skins that
come with it out of the box.  That’s right, we got rid of the skins that
screamed, "*Hey! I was designed by a developer who wears plaid
pants with flannel shirts!*".  Over time, we hope to add more polished
designs.

Of course we don’t want to leave developers with custom developer
designed skins out in a lurch.  Taking an informal poll I found that a
majority of Subtext users deploy a custom skin typically based on one of
the out-of-the-box skins. 

As I described in [the
overview](https://haacked.com/archive/2006/08/26/Mile_High_Overview_Of_Subtext_Skinning.aspx),
skins are configured via a file named **`Skins.config`**.  One problem
with having all the skin definitions in this file is that any
customizations a user might make are overwritten when upgrading to a new
version of Subtext.

It is incumbent upon the user to merge new changes in.  We thought we
could make this better so we have introduced the new
**`Skins.User.config`** file.

The format for this file is **exactly the same** as the format for
`Skins.config`.  The only difference is that we do not include such a
file in the Subtext distribution.  Thus you can place your custom skin
definitions in this file and it will not get overwritten when upgrading.

From now on, it is recommended that if you customize an existing skin,
you should rename the folder and place your skin definition in
Skins.User.config.

