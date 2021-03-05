---
title: Cluster F*ck. When Good Hard Drives Go Bad.
tags: [tech]
redirect_from: "/archive/2004/09/26/cluster-fuck-when-good-hard-drives-go-bad.aspx/"
---

Yesterday was a crazy day. Our production site runs on an older Dell
disk array with three logical volumes each set up in a RAID 1+0
configuration. For you non geeks out there, RAID stands for
***R**edundant **A**rray of **I**ndependent **D**isks* (though some
claim the I stands for ***I**nexpensive*, I take no stand on this
issue).

![Data Center](/assets/images/DataCenter.jpg) \
The matrix has you.

The point of a RAID 1+0 array is to provide high performance fault
tolerance. Unfortunately, it seems that when one disk goes down in our
array, others follow in its wake. Yesterday we had three physical disks
report failures and one reported that it would probably fail in the near
future. I appreciate the one disk giving us a heads up.

"Umm yeah, those other guys failed you. I think I'll hum along a bit and
fail...say...sixish?"

Luckily for us, one of the failures was a misreport and we were able to
immediately bring it online. The other two failures were on separate
volumes, thus we could rebuild each of the drives. My coworker and I
headed over to the data center to meet with a network engineer from our
former parent company to take care of the situation.

![Data Center Networking](/assets/images/DataCenterNetwork.jpg) \
Networking cables galore.

The entrance to the data center has one of them double lock chambers.
Swiping a card provides access into the vertical glass tube. At this
point you half expect all the air to be sucked out like a physics
experiment gone awry. Once the door closes behind you, you swipe the
card again in order to exit the tube on the opposite side. It was
reminiscent of every episode of ALIAS where Sydney has to infiltrate a
data center, only there were no paramilitary guards with machine guns.

The inside was volumnious, with several cages here and there humming
with the sound of murmuring server racks. It sort of reminded me of the
Core in the Matrix series. Posted prominently in the entrance was a sign
forbidding the use of photographic equipment, so we had to place our
cameras back in the car. However, my coworker had a phone cam with him
and took a couple of pics of our servers. Don't tell anyone.

