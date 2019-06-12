---
title: "Banish Merge Conflicts With Semantic Merging"
description: "Merge conflicts are the bane of developers everywhere. What if we could use semantic understanding of code to reduce the time we spend resolving them."
tags: [git,semantic]
excerpt_image: https://user-images.githubusercontent.com/19977/59380002-f7eae080-8d0c-11e9-8b1d-b4ff5e764b8b.PNG
---

Raise your hand if you enjoy merge conflicts. I'll go out on a limb and guess that nobody has a hand up. If you do have your hand up - first, you look silly right now. I can't see you. And second, you're being contrarian. Nobody likes merge conflicts. They're a hassle.

I know the data backs me up here. When I started at GitHub, I worked on a Git client. If you can avoid it, never work on a Git client. It's painful. The folks that build these things are true heroes in my book. All of them.

Anyways, the most frequent complaint we heard from our users had to do with merge conflicts. It trips up so many developers, whether new or experienced. We ran some surveys and we'd often hear things along the lines of this...

> When I run into a merge conflict on GitHub, I flip my desk, set it all on fire, and `git reset HEAD --hard` and just start over.

## Conflict Reduction

Here's the dirty little secret of Git. Git has no idea what you're doing. As far as Git is concerned, you just tappety tap a bunch of random characters into a file. Ok, that's not fair to Git. It does understand a little bit about the structure of text and code. But not a lot.

If it did understand the structure and semantics of code, it could reduce the number of merge conflicts by a significant amount. Let me provide a few examples. We'll assume two developers are collaborating on each example, Alice and Bob. Bob only works on `master` and Alice works in branches. Be like Alice.

In each of these examples, I try to keep them as simple as possible. They're all single file, though the concepts work if you work in separate files too. 

## Method Move Situation

In this scenario, Bob creates an interface for a socket server. He just jams everything into a single interface.

__Bob: Initial Commit on `master`__

```diff
+public interface ISocket
+{
+    string GetHostName(IPAddress address);
+    void Listen();
+    void Connect(IPAddress address);
+    int Send(byte[] buffer);
+    int Receive(byte[] buffer);
+}
```

Now Alice comes along and thinks, we should separate this into an interface for clients and another for servers. So she creates branch `separate-client-server` and creates the `IServerSocket` interface. She then renames `ISocket` to `IClientSocket`. She also moves the methods `Listen` and `Receive` into the `IServerSocket` interface.

__Alice: Commit on `separate-client-server`__

```diff
-public interface ISocket
+public interface IClientSocket
 {
     string GetHostName(IPAddress address);
-    void Listen();
     void Connect(IPAddress address);
     int Send(byte[] buffer);
-    int Receive(byte[] buffer);
}
+
+public interface IServerSocket
+{
+    void Listen();
+    int Receive(byte[] buffer);
+}
```

Meanwhile, back on the `master` branch. Bob moves `GetHostName` into a new interface, `IDns`

```diff
 public interface ISocket
 {
-    string GetHostName(IPAddress address);
     void Listen();
     void Connect(IPAddress address);
     int Send(byte[] buffer);
     int Receive(byte[] buffer);
 }
+
+public interface IDns
+{
+    string GetHostName(IPAddress address);
+}
```

Now Bob tries to merge the `separate-client-server` branch into `master` and Git reports a merge conflict. Boo hoo.

```diff
 using System.Net;

-public interface ISocket
+public interface IClientSocket
 {
+<<<<<<< HEAD
     void Listen();
+=======
+    string GetHostName(IPAddress address);
+>>>>>>> separate-client-server
     void Connect(IPAddress address);
     int Send(byte[] buffer);
-    int Receive(byte[] buffer);
 }
 
+<<<<<<< HEAD
 public interface IDns
 {
     string GetHostName(IPAddress address);
 }
+=======
+public interface IServerSocket
+{
+    void Listen();
+    int Receive(byte[] buffer);
+}
+>>>>>>> separate-client-server
```

You see, as far as Git is concerned, both developers just changed some text in the same place. It doesn't understand that Alice and Bob are extracting interfaces and moving methods around.

But what if it did? This is where semantic diff and semantic merge come into play. I'm an advisor to Códice Software who are deep in this space. One of their products, [gmaster](https://gmaster.io/) is a Git client that incorporates their [Semantic Merge technology](https://semanticmerge.com/).

Here's what happens when I run into this situation with gmaster. In my opinion, the UI is a bit confusing at first, but it's very powerful and you get used to it.

![gmaster with a merge conflict](https://user-images.githubusercontent.com/19977/59379528-b4dc3d80-8d0b-11e9-886f-db6926a582fe.PNG)

1. First, gmaster recognizes that Git reports a merge conflict. It doesn't resolve it automatically. That is an intentional action.
2. Down below, you get a semantic diff showing the method moved to a new interface. It already knows what's going on here.
3. Click the "Launch Merge Tool" to see the magic happen. This launches the semantic merge tool.

![gmaster automatically resolves the conflict](https://user-images.githubusercontent.com/19977/59380002-f7eae080-8d0c-11e9-8b1d-b4ff5e764b8b.PNG)

1. As you can see, the tool was able to automatically resolve the conflict. No manual intervention necessary.
2. All you have to do is click Commit and you have a merge conflict.

With Git and any other diff/merge tool, you would have to manually resolve the conflict.

## Summary

I'll be honest, my favorite Git client is still [GitHub Desktop](https://desktop.github.com/). I think it is very well designed, usable, and fits my workflow. But I'm adding gmaster to my toolbelt when I run into merge conflicts. I'd rather let it handle resolving them than doing it by hand.

Gmaster is unfortunately only available on Windows but you can't beat the price, free!

I plan to write another post or two about merge conflict scenarios and how semantic approaches can help save developers a lot of time.

__DISCLAIMER: I am a paid advisor to the makers of gmaster, but the content on my blog is my own. They did not pay for this post, in the same way all my previous employers did not pay for any content on my blog.__