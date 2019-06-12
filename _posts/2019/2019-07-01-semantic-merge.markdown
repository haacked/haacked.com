---
title: "..."
description: "..."
tags: [git]
excerpt_image: ...
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

Here's what happens when I run into this situation with gmaster.

