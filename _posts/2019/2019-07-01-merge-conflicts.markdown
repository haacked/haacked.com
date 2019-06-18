---
title: "..."
description: "..."
tags: [git]
excerpt_image: ...
---

Some merge conflicts that Git cannot resolve automatically can be resolved by tools that understand the semantics of code. In a recent post, I [gave one simple example](https://haacked.com/archive/2019/06/17/semantic-merge/).

What about the other case? Are there situations where Git automatically resolves changes that it shouldn't? Of course the answer is yes, or I wouldn't pose the question and write this post.

## Multiple usings

Let's start with something that you're likely to run into if you're a .NET developer. Here we have a very simple initial commit by Bob.

```csharp
using System;
using System.Collections;
using System.Console;

public class Main
{
}
```

Alice creates a branch named `diagnostics` and adds a couple using statements.

```diff
 using System;
+using System.Text;
 using System.Collections;
+using System.Diagnostics;
 using System.Console;

 public class Main
 {}
```

Meanwhile, on `master`, Bob adds `using System.IO` in the same place where Alice added `System.Text`. Bob also adds `using System.Diagnostics` to the end of the usings section.

```diff
 using System;
+using System.IO;
 using System.Collections;
 using System.Console;
+using System.Diagnostics;

 public class Main
 {
 }
```

Now Bob tries to merge Alice's branch `diagnostics` into `master`. This results in the following conflict.

```csharp
 using System;
 <<<<<<< HEAD
 using System.IO;
 =======
 using System.Text;
 >>>>>>> diagnostics
 using System.Collections;
 using System.Diagnostics;
 using System.Console;
 using System.Diagnostics;
```

Git reports a conflict with the second line because Alice added `System.Text` there and Bob added `System.IO` there. Notice that Git doesn't have any problem with the fact that both developers added a redundant `using System.Diagnostics` in two different places.

What happens in gmaster when we launch the semantic merge tool?

![gmaster resolves the conflict and duplicate usings](https://user-images.githubusercontent.com/19977/59700793-00c63100-91a9-11e9-8bf6-af1d7798920c.PNG)

Because gmaster understands C#, it is not only able to automatically resolve the conflict, it resolves the duplicate usings as well.

To be fair, the duplicate usings issue is very minor. It doesn't affect the correctness of the program to have an extra using statement. Also, at the time I write this, gmaster will only resolve the duplicate if there's another conflict in the file. The reason for this is it still relies on Git to report a merge conflict. If Git doesn't think there's a conflict, gmaster won't intervene.

## Divergent Move



We start with

```csharp
public interface IStudent
{
    string Name { get; }
    string Teacher { get; }
    IClass Class { get; }
}

public interface IClass
{
    string Name { get; }
    string Subject { get; }
}

public interface IEnrollment
{
    IStudent Student { get; }
    IClass Class { get; }
}
```

Then in a branch named `move-teacher-to-class` we move the `Teacher` property from `IStudent` to the `IClass` interface. We also add a `Grade` property to `IStudent`. This results in these changes:

```diff
 public interface IStudent
 {
     string Name { get; }
-    string Teacher { get; }
+    int Grade { get; }
     IClass Class { get; }
 }
 
 public interface IClass
 {
     string Name { get; }
     string Subject { get; }
+    string Teacher { get; }
 }
```

Meanwhile, on `master`, a different developer moves the `Teacher` property from `IStudent` to the `IEnrollment` interface.

```diff
 public interface IStudent
 {
     string Name { get; }
-    string Teacher { get; }
     IClass Class { get; }
 }

 public interface IEnrollment
 {
     IStudent Student { get; }
     IClass Class { get; }
+    string Teacher { get; }
 }
```

Now what happens when we merge `move-teacher-to-class` into `master`?

```diff
 public interface IStudent
 {
     string Name { get; }
+<<<<<<< HEAD
+=======
+    int Grade { get; }
+>>>>>>> move-teacher-to-class
     IClass Class { get; }
 }

public interface IClass
 {
     string Name { get; }
     string Subject { get; }
+    string Teacher { get; }
 }
```

Git notices the conflict in the `IStudent` class. One developer removed a property. Another developer added a property. But something else interesting happened here that Git did not notice. We have a divergent move situation here. Both developers moved the `Teacher` property to different branches. Let's see how gmaster handles this.

