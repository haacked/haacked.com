---
title: "Resolve Merge Conflicts the Easy Way"
description: "Merge conflicts don't have to ruin your day. I use mergiraf for structural merging and a Claude Code skill for everything else. Here's how to set it up."
tags: [git, productivity, open-source]
excerpt_image: https://i.haacked.com/blog/2026-03-25-resolve-merge-conflicts/TODO-placeholder.png
---

Git is great at merging until it isn't. Most of the time, when I rebase my feature branch against the main branch, it all goes to plan. Nothing to do for me. But when it doesn't go to plan, it can be a big mess. Git dumps a wall of conflict markers on you. You resolve those, continue the rebase, and the next commit has conflicts too. Depending on the scope of changes, resolving merge conflicts can be a very tedious chore. The temptation to `git rebase --abort` and pretend this never happened is overwhelming.

It turns out, we have some great tools now for dealing with tedious chores. In particular, I've set up two tools that turned merge conflicts from a dreaded chore into a minor speed bump. Most of the time, they resolve themselves before I even see them. For the ones that don't, automation handles the tedious parts so I only deal with the genuinely ambiguous cases.

## The Problem with Textual Merging

Git's built-in merge is purely textual. It compares lines of text and looks for overlapping changes. It doesn't understand your code. It doesn't know what a function is, or an import statement, or a class definition. It just sees lines.

This means Git reports conflicts that aren't actually conflicts. Two developers add different imports to the same file, near the same spot. Git sees overlapping line changes and panics:

```text
<<<<<<< HEAD
import { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
=======
import { useState } from 'react';
import { useEffect } from 'react';
>>>>>>> feature/dashboard
```

A human can see instantly that both changes are independent additions. One added `useQuery`, the other added `useEffect`. The correct resolution is to keep all three imports. But Git can't see that because Git doesn't understand syntax. It only sees text.

These false conflicts add up. On a large rebase, they can turn a five-minute task into a thirty-minute slog.

## Layer 1: Mergiraf

[Mergiraf](https://mergiraf.org/) is a structural merge driver for Git. Instead of comparing lines of text, it parses your files using language grammars and merges at the syntax tree level. If two changes touch different parts of the syntax tree, it merges them cleanly. If they genuinely conflict at the structural level, it falls back to standard conflict markers.

That import example above? Mergiraf resolves it automatically. It understands that those are independent additions to an import list and combines them.

Mergiraf supports over 25 languages: Java, Rust, Go, Python, JavaScript, TypeScript, C, C++, C#, Ruby, Elixir, PHP, Dart, Scala, Haskell, OCaml, Lua, Nix, YAML, TOML, HTML, XML, and more. For file types it doesn't support, it returns a non-zero exit code and Git falls back to its default textual merge. No harm done.

### Setup

Three steps.

**1. Install mergiraf:**

```bash
brew install mergiraf
```

**2. Register the merge driver in your `~/.gitconfig`:**

```ini
[merge "mergiraf"]
    name = mergiraf
    driver = mergiraf merge --git %O %A %B -s %S -x %X -y %Y -p %P -l %L
```

**3. Apply it globally in `~/.config/git/attributes`:**

```text
* merge=mergiraf
```

The wildcard (`*`) tells Git to run every file through mergiraf. This might sound aggressive, but it's fine. If mergiraf doesn't recognize the file type, it steps aside and Git handles it normally.

> [!NOTE]
> If you use my [dotfiles](https://github.com/haacked/dotfiles), the `git/install.sh` script creates the attributes file for you. Run it once and you're done.

### Companion Settings

Two additional Git settings complement mergiraf nicely.

**diff3 conflict style**: By default, Git's conflict markers only show your version and their version. With `diff3`, you also see the common ancestor (the "base"). This gives both mergiraf and humans more context to resolve conflicts correctly.

```ini
[merge]
    conflictStyle = diff3
```

Here's the difference. Standard conflict markers:

```text
<<<<<<< HEAD
const timeout = 5000;
=======
const timeout = 10000;
>>>>>>> feature
```

With diff3:

```text
<<<<<<< HEAD
const timeout = 5000;
||||||| base
const timeout = 3000;
=======
const timeout = 10000;
>>>>>>> feature
```

The base section tells you the original value was 3000. Now you can see that HEAD changed it to 5000 and the feature branch changed it to 10000. Without the base, you're guessing.

**rerere** (reuse recorded resolution): Rerere records how you resolve conflicts and automatically replays those resolutions if the same conflict comes up again. This is useful during rebases where you might encounter the same conflict multiple times.

```ini
[rerere]
    enabled = true
```

## Layer 2: Automating the Rest

Mergiraf handles the structural conflicts, but some conflicts are genuinely ambiguous. And some aren't ambiguous at all, they're just tedious. Lock files, database migrations, stacked PR duplicates. Each of these has a clear resolution strategy, but you still have to do the work manually.

This is drudgery. Drudgery that follows clear rules. Perfect for automation.

I built a [Claude Code skill](https://github.com/haacked/dotfiles/tree/main/ai/skills/resolve-conflicts) called `/resolve-conflicts` that handles the entire conflict resolution workflow. Type `/resolve-conflicts` and it takes over.

### How It Works

The skill follows a three-step loop:

1. **Detect context.** It reads Git's internal state files to determine whether you're in a rebase, merge, cherry-pick, or revert, and how far along you are (e.g., step 3 of 12 in a rebase).

2. **Categorize and resolve.** It runs a categorization script on every conflicted file, sorting them into buckets: lock file, migration, mergiraf-supported, or other. Then it resolves each bucket with the appropriate strategy.

3. **Continue.** It regenerates any lock files, runs the appropriate continue command (`git rebase --continue`, `git commit --no-edit`, etc.), and loops back to step 1 if more conflicts appear.

### Resolution Strategies

Each category gets its own treatment:

**Lock files**: Accept theirs to clear the conflict markers, then regenerate. The content of a lock file is derived from the dependency manifest, so there's no point resolving individual lines. The skill runs the appropriate package manager (`npm install`, `cargo generate-lockfile`, `poetry lock --no-update`, etc.) to produce a correct lock file from the resolved manifest.

**Migrations**: Ask the human. Migration files represent sequential schema changes where order matters. Getting this wrong can break your database. The skill flags these and asks you how to proceed.

**Mergiraf files**: Run mergiraf as a second pass. Even though mergiraf already ran as a merge driver during the git operation, sometimes conflicts remain (partial resolutions, complex restructuring). The skill runs `mergiraf solve` on the file. If conflict markers remain after that, it falls through to AI analysis.

**Everything else**: Read the conflict, analyze both sides, and resolve it. This is where the skill earns its keep on the genuinely tricky ones.

### Stacked PR Duplicate Detection

If you work with stacked PRs, you've probably hit this one. You have a feature branch with sub-PRs stacked on top of each other. You merge a sub-PR into main. Now when you rebase the parent branch, Git produces conflicts where both sides have nearly identical code and the base section is empty.

Here's what that looks like with diff3:

```text
<<<<<<< HEAD
function calculateTotal(items) {
  return items.reduce((sum, item) => sum + item.price, 0);
}
||||||| base
=======
function calculateTotal(items) {
  return items.reduce((sum, item) => sum + item.price, 0);
}
>>>>>>> feature/add-checkout
```

Empty base. Both sides identical. This isn't a real conflict. It's just Git seeing that code appeared on both sides independently (once from the merged sub-PR, once from the feature branch that originally authored it).

The skill detects this pattern. When the base is empty but HEAD and the incoming side are more than 95% similar (after normalizing whitespace), it auto-resolves by keeping the HEAD version and tells you what it did. For 70-95% similarity, it shows both versions and asks you to confirm. Below 70%, it's a genuine divergence and presents both options for you to decide.

### A Realistic Session

Here's what it looks like in practice. You're rebasing and hit conflicts:

```text
> /resolve-conflicts

Conflicts (rebase step 3/12):
- 1 lockfile: package-lock.json
- 2 mergiraf: src/app.ts, src/utils.ts
- 1 other: README.md

Resolving lockfile: package-lock.json... accepted theirs (will regenerate)
Resolving mergiraf: src/app.ts... resolved structurally
Resolving mergiraf: src/utils.ts... 1 conflict remains, analyzing...
  Auto-resolved src/utils.ts hunk at line 42: stacked PR duplicate
  (HEAD and incoming 98% similar with empty base). Kept HEAD version.
Resolving other: README.md... [presents conflict for user review]

Regenerating package-lock.json... done
Continuing rebase (step 4/12)...
```

If step 4 has conflicts, it resolves those too. All the way through step 12. You just watch it work and only chime in when it needs a human decision.

### Setup

The skill lives in my [dotfiles repo](https://github.com/haacked/dotfiles/tree/main/ai/skills/resolve-conflicts). If you use my dotfiles, it's already available via symlink. Otherwise, grab the files and drop them into `~/.claude/skills/resolve-conflicts/`.

> [!NOTE]
> The skill works best with [mergiraf](https://mergiraf.org/) installed for the structural merging step. Without it, those files fall through to AI analysis, which still works but is less precise for structural changes.

## Putting It Together

The two layers complement each other:

1. During any git merge, rebase, or cherry-pick, mergiraf runs automatically as the merge driver. It silently resolves structural conflicts before you ever see them. You don't have to do anything.
2. For the conflicts that remain, `/resolve-conflicts` categorizes them, applies the right strategy for each type, and continues the operation.

The result: most rebases that used to require manual intervention now complete with zero or minimal human input. The conflicts that do need your attention are the genuinely ambiguous ones that deserve it.

## Try It

Both tools are open source and available in my [dotfiles repo](https://github.com/haacked/dotfiles). Mergiraf is available at [mergiraf.org](https://mergiraf.org/) and installs in minutes. The resolve-conflicts skill requires [Claude Code](https://docs.anthropic.com/en/docs/claude-code/overview).

Merge conflicts are an inevitable part of collaborative development. The suffering is optional.
