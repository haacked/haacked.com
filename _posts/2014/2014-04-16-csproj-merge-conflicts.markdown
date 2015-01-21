---
layout: post
title: "Merge conflicts in csproj files"
date: 2014-04-16 14:07 -0800
comments: true
categories: [git ghfw vs]
---

In a recent version of GitHub for Windows, we made a quiet change that had a subtle effect you might have noticed. We changed the default merge strategy for `*.csproj` and similar files. If you make changes to a `.csproj` file in a branch and then merge it to another branch, you'll probably run into more merge conflicts now than before.

Why?

Well, it used to be that we would do a `union` merge for `*.csproj` files. The `git merge-file` [documentation describes this option as such](http://git-scm.com/docs/git-merge-file):

> Instead of leaving conflicts in the file, resolve conflicts favouring our (or their or both) side of the lines.

For those who don't speak the commonwealth English, "favouring" is a common British misspelling of the one true spelling of "favoring". :trollface:

So when a conflict occurs, it tries to resolve it by accepting all changes more or less. It's basically a cop out.

This strategy can be set in a `.gitattributes` file like so if you really want this behavior for your repository.

```
*.csproj  merge=union
```

But let me show why you probably don't want to do that and why we ended up changing this.

## Union Merges Gone Wild

Suppose we start with the following simplified `foo.csproj` file in our `master` branch along with that `.gitattributes` file:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project>
  <PropertyGroup>
    <Page Include="AAA.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
    <Page Include="DDD.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
  </PropertyGroup>
</Project>
```

After creating that file, let's make sure we commit it.

```bash
git init .
git add -A
git commit -m "Initial commit of gittattributes and foo.csproj"
```

We then create a branch (`git checkout -b branch`) creatively named "branch" and insert the following snippet into `foo.csproj` in between the `AAA.cs` and `DDD.cs` elements.

```xml
    <Page Include="BBB.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
```

For those who lack imagination, here's the result that we'll commit to this branch.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project>
  <PropertyGroup>
    <Page Include="AAA.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
    <Page Include="BBB.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
    <Page Include="DDD.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
  </PropertyGroup>
</Project>
```

Don't forget to commit this if you're following along.

```bash
git commit -a "Add BBB.cs element"
```

Ok, so let's switch back to our master branch.

```bash
git checkout master
```

And then insert the following snippet into the same location.

```xml
    <Page Include="CCC.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
```

The result now in master is this:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project>
  <PropertyGroup>
    <Page Include="AAA.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
    <Page Include="CCC.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
    <Page Include="DDD.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
  </PropertyGroup>
</Project>
```

Ok, commit that.

```bash
git commit -a "Add CCC.cs element"
```

Still with me?

Ok, now let's merge our branch into our `master` branch.

```bash
git merge branch
``` 

Here's the end result with the union merge.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project>
  <PropertyGroup>
    <Page Include="AAA.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
    <Page Include="CCC.cs">
    <Page Include="BBB.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
    <Page Include="DDD.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
  </PropertyGroup>
</Project>
```

Eww, that did not turn out well. Notice that "BBB.cs" is nested inside of "CCC.cs" and we don't have enough closing `</Page>` tags. That's pretty awful.

Without that `.gitattributes` file in place and using the standard merge strategy, the last merge command would result in a merge conflict which forces you to fix it. In our minds, this is better than a quiet failure that leaves your project in this weird state.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project>
  <PropertyGroup>
    <Page Include="AAA.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
<<<<<<< HEAD
    <Page Include="CCC.cs">
=======
    <Page Include="BBB.cs">
>>>>>>> branch
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
    <Page Include="DDD.cs">
      <SubType>Designer</SubType>
      <Generator>MSBuild:Compile</Generator>
    </Page>
  </PropertyGroup>
</Project>
```

Obviously, in some idyllic parallel universe, git would merge the full `CCC` element after the `BBB` element without fudging it up and without bothering us with these pesky merge conflicts. We don't live in that universe, but maybe ours could become more like that one. I hear it's cool over there.

## What's this gotta do with Visual Studio?

I recently asked folks on Twitter to vote up this User Voice issue asking the Visual Studio team to [support file patterns in project files](http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/4512873-vs-ide-should-support-file-patterns-in-project-fil). Wildcards in .csproj files are already supported by MSBuild, but Visual Studio doesn't deal with them very well.

One of the big reasons to do this is to ease the pain of merge conflicts. If I could wild-card a directory, I wouldn't need to add an entry to `*.csproj` every time I add afile.

Another way would be to write a proper XML merge driver for Git, but that's quite a challenge as my co-worker [Markus Olsson](https://twitter.com/niik) can attest to. If it were easy, or even moderately hard, it would have been done already. Though I wonder if we limited it to common `.csproj` issues could we write one that isn't perfect but good enough to handle common merge conflicts? Perhaps.

Even if we did this, the merge driver only solves the problem for one version control system, though arguably the only one that really matters. :trollface:

It's been suggested that if Visual Studio sorted its elements first, that would help mitigate the problem. That helps reduce the incidental conflicts caused by Visual Studio's apparent non-deterministic sort of elements. But it doesn't make the issue of merge conflicts go away. In the example I presented, every element remained sorted throughout my example. So any time two different branches adds files that would be adjacent, you run the risk of this conflict. This happens quite frequently.

Wild card support would make this problem almost completely go away. Note, I said _almost_. There will still be the occasional conflict in the file, but they'd be very rare.
