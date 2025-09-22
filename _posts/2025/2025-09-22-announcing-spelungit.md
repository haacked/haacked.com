---
title: "Spelungit: Because `git log --grep` is terrible at its job"
description: "Ever tried to remember why you made a change six months ago? I built Spelungit so you can search Git commit history with natural language instead of praying to the regex gods."
tags: [git,open-source,mcp,claude,ai]
excerpt_image: https://github.com/haacked/spelungit/raw/main/docs/spelungit-demo.gif
---

You know that sinking feeling when you're debugging a production issue and need to understand why someone (spoiler: it was you) made a change six months ago? You fire up `git log` and scroll through hundreds of commits, squinting at messages like "fix thing" and "wip" hoping something will jog your memory.

Or you're onboarding a new team member and they ask "When did we add authentication?" and you're frantically searching through a junk drawer muttering "I know it's in here somewhere."

Git's search is terrible. `git log --grep="auth"` assumes you remember exact words from commit messages. Searching through actual code changes? That way lies madness.

## Enter Spelungit

I built [Spelungit](https://github.com/haacked/spelungit), a semantic search engine for Git commit history. Instead of keyword roulette, you search using natural language through Claude Code's MCP interface.

Want commits about database migrations? Ask for "database schema changes." Looking for that auth bug fix? Search for "login authentication fixes." It understands what you mean instead of making you guess exact words from three months ago.

## How it works

Instead of this:

```bash
git log --grep="auth" --grep="login" --grep="security" --all --pretty=oneline | head -20
```

You do this:

```
search_commits(query="authentication and login improvements")
```

Queries that work:

- "Find commits about database schema changes"
- "Show me when we fixed the memory leak"
- "What API endpoints were added recently?"
- "Show bug fixes related to user permissions"

It uses hybrid embeddings—semantic understanding plus code patterns. Knows the difference between new features and bug fixes, even when commit messages are vague (looking at you, past me).

## Installation

No PostgreSQL, no API keys, no Docker. SQLite and local embeddings. Everything runs on your machine.

One line:

```bash
curl -sSL https://raw.githubusercontent.com/haacked/spelungit/main/install-remote.sh | bash
```

Downloads everything, sets up Python venv, installs dependencies, configures Claude Code. Suspicious of pipe-to-bash? Clone the repo and run `./install.sh`.

## Using it

Shows up in Claude Code as an MCP server. Four tools:

1. **`index_repository`** - Process Git history (run first)
2. **`search_commits`** - Search stuff
3. **`repository_status`** - Check progress
4. **`get_database_info`** - Database stats

Setup: run `index_repository()` in your project. Analyzes commits, creates embeddings. Few minutes for most repos. Then search:

```python
search_commits(query="error handling improvements", limit=10)
```

## Under the hood

Analyzes commit messages and code changes. For each commit:

- Semantic content from messages and diffs
- Code patterns like function names and classes
- File types and directory structure
- Feature vs. bug fix vs. refactoring

Uses Microsoft's all-MiniLM-L6-v2 (384 dimensions) with local sentence-transformers. No cloud dependencies. Embeddings in SQLite with cosine similarity search.

Efficient storage—only commit SHAs and embeddings. Thousands of commits = few megabytes. Handles Git worktrees if you're into that masochism.

## Why I built this

I work across multiple repositories and constantly need to understand why changes were made. Traditional Git tools are limited. Too much detective work scrolling through commits.

I wanted to ask Git history questions in plain English and get the right commits back. Spelungit made my workflow less frustrating.

Also, I've been experimenting with MCP servers and wanted to build something useful instead of another todo app.

## Future ideas

**Configurable models**: Local embeddings work well, but OpenAI's models would be better for teams willing to trade privacy.

**PostgreSQL support**: For massive repos or shared team search.

**Smarter code understanding**: Better refactoring vs. feature detection, architectural changes over time.

**Standalone CLI**: Currently Claude Code only.

## Try it

[Spelungit](https://github.com/haacked/spelungit) is MIT licensed. The project scratched a real itch for me. If it doesn't work perfectly, that's what GitHub issues are for.

Find it at [https://github.com/haacked/spelungit](https://github.com/haacked/spelungit).