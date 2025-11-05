---
title: "IntelliSense for Hosted C# Script"
description: "We recently announced support for a local editing experience for Abbot skills that includes IntelliSense for C# Skills in Visual Studio Code. This post digs behind the scenes to talk about how that works."
tags: [abbot,csharp]
excerpt_image: https://user-images.githubusercontent.com/19977/131401537-533115bd-545f-4cf6-8b38-14000258e9e1.png
---

In a recent Abbot Blog Post, we covered [`abbot-cli` a new open source command-line tool](https://github.com/aseriousbiz/abbot-cli). `abbot-cli` made it possible to work on Abbot skills in your local editor. In that post, I mentioned that when you retrieve a C# skill to edit locally, the tool writes a few aditional files on your machine.

> For C# skills, these other files make it possible for us to provide Intellisense for the skill editing experience. More on that in another post for those interested.

In this post, I want to follow-up on that and cover how that works and why it's interesting.

## C# Scripts

First, a bit of background. If you went to Abbot and edited a C# skill, you got a nice in-browser editor with IntelliSense.

![Screenshot of C# IntelliSense within Abbot](https://user-images.githubusercontent.com/19977/131697837-c452ddcc-51b1-43d7-a6a4-919b400b71c1.png)

In truth, these aren't exactly C# skills. They're a dialect of C# known as [`CSharpScript`](https://github.com/dotnet/roslyn/blob/main/src/Scripting/CSharp/CSharpScript.cs). Roslyn supports this class in the [Microsoft.CodeAnalysis.CSharp.Scripting namespace](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp?view=roslyn-dotnet-3.11.0).

The benefit of using `CSharpScript` over proper C# is it removes a lot of the ceremony that comes with writing C# skills. For example, `CSharpScript` supports top-level statements, which are only now being [introduced in the latest version of C# proper](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/top-level-statements). Also, since Abbot hosts the C# scripting runtime, it can inject a global variable (`Bot`) into the script. This lets script authors focus on writing code to accomplish their task and not worry about declaring a class and method (unless they want to).

## MirrorSharp for the Web

To provide IntelliSense in our web editor, we use the excellent [MirrorSharp project](https://github.com/ashmind/mirrorsharp/) by [Andrey Shchekin](https://github.com/ashmind) who is also the creator of the more well-known [SharpLab](https://github.com/ashmind/SharpLab).

MirrorSharp sets up Roslyn on a websocket to provide IntelliSense. More importantly, it supports `CSharpScript`.

```csharp
endpoints.MapMirrorSharp("/mirrorsharp",
    new MirrorSharpOptions
    {
        SelfDebugEnabled = true,
        IncludeExceptionDetails = true
    }
    .SetupCSharp(o =>
    {
        // Other stuff omitted
        o.SetScriptMode(hostObjectType: typeof(IScriptGlobals));
    })
);
```

Note that the `Bot` instance we inject into Abbot skills is the `Bot` property of `IScriptGlobals`, which is specified as the `hostObjectType` in the `SetupCSharp` call. With this in place, we can provide really great IntelliSense when editing a C# Abbot skill in our web editor.

## OmniSharp for the desktop

But what happens when you want edit a skill on your local machine? Can we provide a good IntelliSense experience? Unfortunately, we can't use MirrorSharp for that as it's focused on the web. But there is a solution for desktop editors, [`omnisharp-roslyn`](https://github.com/OmniSharp/omnisharp-roslyn).

> OmniSharp is a .NET development platform based on Roslyn workspaces. It provides project dependencies and C# language services to various IDEs and plugins.

Several editors support OmniSharp including Visual Studio Code via the [`omnisharp-vscode`](https://github.com/OmniSharp/omnisharp-vscode) extension.

But OmniSharp doesn't understand the Abbot runtime. It's not going to know about the `Bot` instance we inject into Abbot skills. Also, what about the namespaces and dependencies we inject? And finally, how do we tell OmniSharp we're using `CSharpScript` and not C#?

### OmniSharp Configuration

The first step we did was to write a `.csx` file instead of `.cs`. `.csx` is a known extension for C# Script files and is recognized by OmniSharp.

The next step is to configure OmniSharp by writing an `omnisharp.json` file into the same directory as our `.csx` file.

```json
{
    "script": {
        "enabled": true,
        "defaultTargetFramework": "net5.0",
        "enableScriptNuGetReferences": true,
        "RspFilePath": "../.abbot/references.rsp"
    }
}
```

This lets OmniSharp know that we're using C# Script, and that we want to use `references.rsp` to specify the references. What is an RSP file? It provides command-line options to the C# compiler. For example, [this is the one used by `csc.exe`](https://github.com/dotnet/roslyn/blob/main/src/Compilers/CSharp/csc/csc.rsp).

Here's the one we write for Abbot, `references.rsp`.

```rsp
/u:System
/u:System.Collections
/u:System.Collections.Concurrent;
/u:System.Collections.Generic
/u:System.Data
/u:System.Dynamic
/u:System.Globalization
/u:System.Linq
/u:System.Linq.Expressions
/u:System.Net.Http
/u:System.Text
/u:System.Text.RegularExpressions
/u:System.Threading
/u:System.Threading.Tasks
/u:Serious.Abbot.Scripting
/u:NodaTime
```

With this configuration in place, OmniSharp includes all these namespaces in every `.csx` file in the project. That's an improvement, but we still have some work to do. For one thing, we can't assume `Serious.Abbot.Scripting`, nor `NodaTime` is on your machine. Also, we still haven't injected the `Bot` instance into the script.

Ideally, we could use the `.rsp` file to inject our Script Globals type into the script, but that's not possible at this time. There's [an open issue in the Roslyn repository](https://github.com/dotnet/roslyn/issues/23421) to add this feature. There's also an [issue in the `omnisharp-roslyn` repo](https://github.com/OmniSharp/omnisharp-roslyn/issues/1372) to provide a configuration option for the script host, but it's not feasible at this time.

### Engaging in some dark arts

This is where I had to engage in some dark arts. One thing that OmniSharp supports for `.csx` file is a `#load` directive. This lets you load another script into the script. When the `abbot-cli` tool downloads an Abbot skill, it writes the following directive at the top of the file:

```cs
#load ".meta/globals.csx" // This is required for Intellisense in VS Code, etc. DO NOT TOUCH THIS LINE!
// The rest of your skill code...
```

When `abbot-cli` runs the skill or deploys it, it strips that directive.

This directive injects `.meta/globals.csx` into the beginning of the script. Let's take a look at it.

```cs
#r "nuget:NodaTime,3.0.5"
#r "nuget:HtmlAgilityPack,1.11.34"
#r "nuget:Abbot.Scripting.Stubs,0.9.0"

var Bot = new Serious.Abbot.Scripting.Bot();
```

The first three lines use the `#r` directive which is used to reference an Assembly. However, OmniSharp supports the `nuget:` prefix that lets you reference NuGet packages. And then we just instantiate a local variable named `Bot`. It's not exactly the same thing as the `Bot` property of `IScriptGlobals`, but it's close enough.

With this in place, if you open an Abbot `.csx` file in your editor, you'll see something like this:

![Screen shot of VS Code showing Intellisense for Bot](https://user-images.githubusercontent.com/19977/131401537-533115bd-545f-4cf6-8b38-14000258e9e1.png)

Magic!

## Next Steps

I'm not above engaging in some dark arts here and there, but it's not a great permanent solution. I opened an `omnisharp-roslyn` issue [to allow specifying includes within `omnisharp.json`](https://github.com/OmniSharp/omnisharp-roslyn/issues/2213). It's not a perfect solution, but it would allow us to stop injecting our `#load` directive. I'm happy to work on that feature, but I'm waiting on the project maintainers to provide feedback to make sure the feature as described makes sense.

If you're writing C# skills for Abbot, I hope you give [`abbot-cli`](https://github.com/aseriousbiz/abbot-cli) a try and [give us some feedback](https://github.com/aseriousbiz/abbot-cli/issues/new)!