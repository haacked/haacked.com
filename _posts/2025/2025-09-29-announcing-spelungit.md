---
title: "Spelungit: When `git log --grep` isn't enough"
description: "Ever tried to remember why you made a change six months ago? I built Spelungit so you can search Git commit history with natural language instead of praying to the regex gods."
tags: [git,open-source,mcp,claude,ai]
excerpt_image: https://i.haacked.com/blog/2025-09-29-announcing-spelungit/robot-spelunking-git.png
---

Supporting a large codebase is challenging. Sometimes, the questions you have can't be answered by the code at hand. For example, "When did we switch from class components to hooks and what discussion led to that decision?" or "Did we used to have logging here for invalid keys?" or "Did we ever have code to handle Zstd compression?"

Git's search is challenging for these sorts of queries. `git log --grep="auth"` assumes you remember exact words from commit messages. Want to find "commits where we improved error handling in API endpoints"? You'll need multiple greps with regex gymnastics, and still miss commits that used different terminology. We need a better tool.

![Robot spelunking Git](https://i.haacked.com/blog/2025-09-29-announcing-spelungit/robot-spelunking-git.png)

## Enter Spelungit

I built [Spelungit](https://github.com/haacked/spelungit), a semantic search engine for Git commit history. Instead of keyword roulette, you search using natural language through Claude Code's MCP interface.

Want commits where you refactored authentication? Ask for "authentication flow refactoring." Looking for race conditions in background processing? Search for "race conditions in job processing." It understands intent instead of making you guess exact words from three months ago.

## How it works

Instead of this:

```bash
# How do you even grep for "race condition fixes"?
git log --grep="race\|concurrent\|thread\|lock\|mutex" --all
# Now manually read through 50 commits...
```

You do this in your AI development tool of choice:

```bash
Search git history for "race condition fixes in background jobs"
```

Queries that work:

- "When did we switch from REST to GraphQL?"
- "Commits where we improved error handling in API endpoints"
- "Show me refactoring of the authentication flow"
- "Security improvements in file upload handling"
- "Where did we fix that memory leak in the worker process?"

It creates embeddings for both commit messages and code changes, which makes semantic search possible. It knows the difference between new features and bug fixes, even when commit messages are vague (looking at you, past me).

## Installation

Spelungit uses SQLite and local embeddings to make installation simple. Everything runs on your machine. No need for an API key, a database, or Docker.

One line:

```bash
curl -sSL https://raw.githubusercontent.com/haacked/spelungit/main/install-remote.sh | bash
```

Downloads everything, sets up Python venv, installs dependencies, configures Claude Code. Suspicious of pipe-to-bash? Clone the repo and run `./install.sh`.

## Using it

Shows up in Claude Code (or your AI tool of choice) as an MCP server. The first time you ask a question in a repository that `spelungit` responds to, spelungit will index the repository. Once it's done, it'll be able to answer questions.For large repos, this can take a few minutes while it analyzes commits and creates embeddings.

Then search naturally:

- "Show me commits about error handling improvements"
- "Find where we refactored the authentication"
- "What changed in the API layer recently?"

Claude calls the appropriate MCP tools (`index_repository`, `search_commits`, `repository_status`, `get_database_info`) behind the scenes.

## Under the hood

Analyzes commit messages and code changes. For each commit:

- Semantic content from messages and diffs
- Code patterns like function names and classes
- File types and directory structure
- Feature vs. bug fix vs. refactoring

Uses Microsoft's `all-MiniLM-L6-v2` (384 dimensions) with local sentence-transformers to create embeddings. Embeddings are stored in SQLite. Searching for git history uses cosine similarity searches.

The sqlite database only stores commit SHAs and embeddings, leaving the full commit message in git. Thousands of commits = few megabytes. Handles Git worktrees if you're into that masochism.

## Why I built this

I work across multiple repositories and constantly need to understand why changes were made. Traditional Git tools are limited. Too much detective work scrolling through commits.

I wanted to ask Git history questions in plain English and get the right commits back. Semantic search of Git history is something I always wished GitHub would add. I got tired of waiting so I built this.

Also, I've been experimenting with MCP servers and wanted to build something useful instead of another todo app.

## Future ideas for improvement

**Configurable models**: Local embeddings work pretty well, but OpenAI's models would be better for teams willing to trade privacy for better accuracy.
**PostgreSQL support**: For massive repos or shared team search.
**Smarter code understanding**: Better refactoring vs. feature detection, architectural changes over time.

## Try it

[Spelungit](https://github.com/haacked/spelungit) is MIT licensed. The project scratched a real itch for me. If it doesn't work perfectly, that's what GitHub issues are for.

Find it at [https://github.com/haacked/spelungit](https://github.com/haacked/spelungit).
