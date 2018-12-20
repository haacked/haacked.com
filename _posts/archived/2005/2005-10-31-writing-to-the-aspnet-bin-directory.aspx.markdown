---
title: Writing to the Asp.Net Bin Directory
date: 2005-10-31 -0800
disqus_identifier: 11053
tags: []
redirect_from: "/archive/2005/10/30/writing-to-the-aspnet-bin-directory.aspx/"
---

I have a question for those of you who host a blog with a hosting
provider such as WebHost4Life. Do you make sure to remove write access
for the ASPNET user to the bin directory? If so, would you be willing to
enable write access for an installation process?

The reason I ask is that I’ve created a proof of concept for a potential
nearly no-touch upgrade tool for upgrading .TEXT to Subtext. This
particular tool is geared towards those who have .TEXT hosted with 3rd
party hosting, although even those who host their own server may want to
take advantage of it.

The way it works is that you simply drop a single upgrader assembly into
the bin directory of an existing .TEXT installation. You also drop an
`UpgradeToSubtext.aspx` file in your admin directory (This provides a
bit of safety so that only an admin can initiate the upgrade process).

Afterwards, you simply point your browser to
`Admin/UpgradeToSubtext.aspx` which initiates the upgrade proces.

The upgrad tool finds the connection string in the existing web.config
and displays a message with the actions it is about to take. After you
hit the upgrade button, it backs up important .TEXT files, unzips an
embedded zip file which contains all the binaries and content files for
Subtext. It also runs an embedded sql script to create all new subtext
tables and stored procedures and copies your .TEXT data over. It doesn’t
modify any existing tables so it is possible to rollback in case
something goes wrong. Finally, it overwrites the web.config file with a
Subtext web.config file, making sure to set the connection string
properly.

It’s a very nice and automated procedure, but it has a key flaw. It
requires write access to your bin directory.

An alternate approach that avoids writing to the bin directory is to
have the user manually deploy all the Subtext binaries to the bin
directory. The upgrade process would run the same, but it would only
need write access to the web directory to deploy the various content
files. Giving the ASPNET user write access to the web directory is not
an unreasonable request since the gallery feature of .TEXT did create
folders and require write access.

If you are considering upgrading from .TEXT to Subtext, or if you just
have an opinion, please chime in.

