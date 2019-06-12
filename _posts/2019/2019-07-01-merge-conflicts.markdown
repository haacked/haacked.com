---
title: "..."
description: "..."
tags: [git]
excerpt_image: ...
---

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

## Multiple usings

```csharp
using System;
using System.Collections;

public class Main
{
}
```

On a branch, add `using System.Diagnostics` after `using System;`

```diff
using System;
+using System.Diagnostics;
using System.Collections;

public class Main
{}
```

Meanwhile, on `master`, the developer adds `using System.IO` after `using System` and adds `using System.Diagnostics` to the end of the usings section.

```diff
using System;
+using System.IO;
using System.Collections;
+using System.Diagnostics;

public class Main
{
}
```

This results in the following conflict.

```diff
using System;
<<<<<<< HEAD
using System.IO;
=======
using System.Diagnostics;
>>>>>>> add-using
using System.Collections;
using System.Diagnostics;

public class Main
{
}
```