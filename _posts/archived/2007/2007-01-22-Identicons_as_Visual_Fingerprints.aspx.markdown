---
title: Identicons as Graphical Digital Fingerprints
tags: [tech,code]
redirect_from:
- "/archive/2007/01/20/Identicons_as_Visual_Fingerprints.aspx/"
- "/archive/2007/01/21/Identicons_as_Visual_Fingerprints.aspx/"
---

**[![Fingerprint via public domain clipart](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IdenticonsasVisualFingerprints_CB0/fingerprint_thumb1.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IdenticonsasVisualFingerprints_CB0/fingerprint3.png)
How do you uniquely identify a person, without divulging the identity of that person? **For example, given a set of personal artifacts, how would I arrange the set of artifacts grouped by the person to which they belonged?

The answer is quite easy, isn’t it (especially given the title of this blog post and the image to the right)? You can look at the fingerprints on the items.

Unless you happened to have a file that mapped the fingerprints to individuals, you won’t know who the comb and mirror belong to, for example, only that they do belong to the same person and not to the person who owns the scissors.

**The analogous structure in the world of information theory and computer science is the digital fingerprint, often created via a **[**hash function**](http://en.wikipedia.org/wiki/Hash_function "Hash Functions on Wikipedia"). MD5 and SHA1 are two of the most commonly used hash functions.

A hash function is a one way algorithm for converting data into a number which for the most part can be used to identify the data without revealing the data. This is why it is common to hash passwords before storing them in a database.

In order to authenticate a user, I don’t need to know the user’s password, I just need to know that the hash I’ve stored corresponds to the password you typed.

Don Park recently invented a system for representing IP Addresses without divulging the actual IP Address via a system of glyphs, which he calls [Identicons](https://blog.codinghorror.com/identicons-for-net "Identicon Explained").
This serves as a nice means of identifying commenters on a blog, without divulging their actual IP address, which could be a privacy concern. The following image are some examples of identicons.

![Some Identicon Glyphs](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IdenticonsasVisualFingerprints_CB0/identiconsamples_thumb1.png)

What’s interesting to me about identicons is that they have wider uses beyond representing IP addresses. As Don states,

> I think identicons have many use cases. One use is embedding them in
> wiki pages to identify authors. Another is using them in CRM to
> identify customers. I can go on and on. It’s not just about IP
> addresses but information that tends to move in ’herds’.

**One way to look at identicons is that they are a visual representation of a hash value.** They sort of add even more weight to the fingerprint analogy by being visual like a real fingerprint.

For example, for security reasons, many free software providers provide an MD5 checksum of their software binaries. You can see an example of this from the [PuTTY download page](http://www.chiark.greenend.org.uk/~sgtatham/putty/download.html "PuTTY Download Page").

[![Putty Download Page. Shows links to MD5 Checksums](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IdenticonsasVisualFingerprints_CB0/image0_thumb5.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IdenticonsasVisualFingerprints_CB0/image07.png) 

The next screenshot shows some actual hash values of exes.

[![Hash Values](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IdenticonsasVisualFingerprints_CB0/image0_thumb9.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IdenticonsasVisualFingerprints_CB0/image013.png)

Looking at the first listing, we can see that for *pageant.exe*, the hash value is *01d89c3cbf5a9fd2606ba5fe3b59a982*, kind of a mouthful, right? Another way that could be represented is via an Identicon, which would be more readily recognizable.

Of course, in this situation, the security minded person would use an automated MD5 checksum checker rather than manually confirming the binary. But do you trust your md5 checksum checker? A quick visual confirmation would be a nice additional vote of confidence in this scenario.

If you’re interested in playing with Identicons in .NET, I recommend taking a look at [this .NET
port](http://www.codinghorror.com/blog/archives/000774.html "Identicons for .NET") of Don Park’s implementation written by [Jeff Atwood](http://www.codinghorror.com/blog/ "Jeff Atwood’s blog, Coding Horror") and [Jon Galloway](http://weblogs.asp.net/jgalloway/ "Jon Galloway’s Blog").

I had the pleasure of reviewing the code with Jeff and he’s quite proud of his caching optimization. Rather than caching the `Bitmap` object, he caches the PNG output as a `MemoryStream` instance. That ends up saving a ton of memory.
