---
title: "When Git Resolves Changes It Shouldn't"
description: "Sometimes, git resolves changes when merging branches that it shouldn't. This is because Git doesn't understand the semantics of code. If it did, it would know these changes to be potential conflicts."
tags: [git,semantic]
excerpt_image: https://user-images.githubusercontent.com/19977/60196319-b7ea2a00-97f1-11e9-9f47-18a002abebd0.PNG
---

When you merge two branches, there may be conflicting changes between the branches. Git can often resolve these differences without intervention. For example, when each branch has changes to different files or lines of code.

But sometimes each branch has changes that Git cannot resolve without help. For example, if two developers change the same line of code. Or if one developer deletes a file, but the other changed the file. In these situations, Git fails the merge operation and reports a merge conflict.

This occurs more often than we'd like because Git doesn't understand the semantics of code. A tool that did understand code semantics could resolve many (but not all) of these conflicts. In my last post, I gave an example of a merge conflict that a semantic tool can resolve automatically.

What about the opposite situation? Are there cases where Git automatically resolves changes that it shouldn't? Of course the answer is yes, or I wouldn't pose the question and write this post.

## Multiple usings

Let's start with something that you're likely to run into if you're a .NET developer. Here we have a very simple initial commit by Bob.

```csharp
using System;
using System.Collections;
using System.Collections.Generic;

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
 using System.Collections.Generic;

 public class Main
 {}
```

Meanwhile, on `master`, Bob adds `using System.IO` in the same place where Alice added `System.Text`. Bob also adds `using System.Diagnostics` to the end of the usings section.

```diff
 using System;
+using System.IO;
 using System.Collections;
 using System.Collections.Generic;
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
 using System.Collections.Generic;
 using System.Diagnostics;
```

Git reports a conflict with the second line because Alice added `System.Text` there and Bob added `System.IO` there. Notice that Git doesn't have any problem with the fact that both developers added a redundant `using System.Diagnostics` in two different places.

What happens in gmaster when we launch the semantic merge tool?

![gmaster resolves the conflict and duplicate usings](https://user-images.githubusercontent.com/19977/60196319-b7ea2a00-97f1-11e9-9f47-18a002abebd0.PNG)

Because gmaster understands C#, it is not only able to automatically resolve the conflict, it resolves the duplicate usings as well.

To be fair, the duplicate usings issue is very minor. It doesn't affect the correctness of the program to have an extra using statement. Also, at the time I write this, gmaster will only resolve the duplicate if there's another conflict in the file. The reason for this is it still relies on Git to report a merge conflict. If Git doesn't think there's a conflict, gmaster won't intervene.

## Divergent Move

This scenario is a bit of an edge case, but more likely to cause problems with the final code. Here we start with an initial commit by Bob with a set of three simple interfaces.

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

Alice creates a branch named `move-teacher-to-class` and moves the `Teacher` property from `IStudent` to the `IClass` interface. She also adds a `Grade` property to `IStudent`. This results in these changes:

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

Meanwhile, on `master`, Bob moves the `Teacher` property from `IStudent` to the `IEnrollment` interface.

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

Now what happens when Bob merges `move-teacher-to-class` into `master`?

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

Git notices the conflict in the `IStudent` class. One developer removed a property. Another developer added a property. But something else interesting happened here that Git did not notice. We have a divergent move situation here. Both developers moved the `Teacher` property to different interfaces. Let's see how gmaster handles this. When we launch the Merge Tool from within gmaster, we get this.

![Gmaster Merge Tool notices divergent move](https://user-images.githubusercontent.com/19977/59949820-1d709c00-9429-11e9-8443-2029ee19b017.PNG)

Notice that gmaster resolves the conflict in `IStudent` that Git reported. Because it understands C#, it understands this change isn't actually in conflict.

However, gmaster does notice another conflict that Git did _not_ report. gmaster reports that the `Teacher` property was moved in each branch to two different locations. That is indicative of a semantic conflict. You can click on "Explain Move" to get a semantic visualization of the change.

![The divergent move explained in a class diagram](https://user-images.githubusercontent.com/19977/59949802-121d7080-9429-11e9-8060-d31be1e2b19d.png)

## Limitations

In the scenario I presented here, there's an important caveat. As I write this, gmaster relies on Git to detect conflicts. If Alice hadn't added the `Grade` property to `IStudent`, Git would not have reported a conflict. In that case, gmaster would not intervene to report the divergent move. Should a tool like gmaster intervene on every merge? That's an interesting question for the gmaster product team. In theory they could build a [custom Git merge driver](http://www.mcclellandlegge.com/2017-03-20-customgitmergedriver/) that understands the semantic of code.

## Future Exploration

There are other conflict scenarios that a semantic tool in theory could resolve. For example, suppose you rename a variable in one branch. Another developer in another branch makes use of the old variable name. When you merge the two branches, it would be nice if the merge tool could resolve that conflict.

As I write this, gmaster doesn't yet handle this situation. The creators of gmaster want to make sure that people find the existing tools useful before investing in deeper semantic merge scenarios.
