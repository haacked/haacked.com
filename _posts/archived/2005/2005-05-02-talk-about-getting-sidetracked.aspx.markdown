---
title: Talk About Getting Sidetracked
date: 2005-05-02 -0800 9:00 AM
tags: [code]
redirect_from: "/archive/2005/05/01/talk-about-getting-sidetracked.aspx/"
---

I started off planning to write a quick installer for a little something
I'm working on. Unhappy with the limited abilities of Visual Studio's
Setup and Deployment project, I finally started playing with
[WiX](http://sourceforge.net/projects/wix/).

After a good deal of time reading through the documentation, I have a
pretty good handle on it. However, it's difficult to apply the WiX
philosophy (build your setup project as you go along) to an existing
project that already has several hundred files.

Along comes Tallow to the rescue (part of the WiX toolkit) which can
generate a fragment containing components themselves containing
references to your files. Sweet! But now you have to reference all the
generated components within your Feature element.

So that drags me further into the hole where I'm performing minor
surgery on the Tallow source code. I soon discover a list of other
feature requests (auto-generate Guids, etc...) and I soon catch myself.
Hey, I'm just here to build a setup project, not rewrite the whole damn
setup toolkit.

So I now have a customized version of Tallow.exe that will generate
Guids for components. It will also generate a commented section of
ComponentRef statements like so.

```html
<!--
 <ComponentRef Id='component0' /<
 <ComponentRef Id='component1' />
 <ComponentRef Id='component2' />
 -->
 ```

You can manually (eww!) cut and paste that section into your main .wxs
file in order to reference all the components in the generated fragment.

Now back to my initial task...
