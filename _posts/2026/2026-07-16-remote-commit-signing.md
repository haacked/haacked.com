---
title: "Sign Commits from Anywhere Without Your Keys Going Anywhere"
description: "How I sign git commits with Secure Enclave keys while remoted into my main Mac from an iPad or a cheap laptop. All it takes is Secretive, SSH agent forwarding, and one well-placed symlink."
tags: [git, security, ssh, productivity, dotfiles]
excerpt_image: ""
---

These days, my main MacBook Pro does a lot of coding without me sitting in front of it. Claude Code churns through tasks in [worktrees](https://haacked.com/archive/2025/11/21/tree-me/) while I check in from wherever I happen to be. Sometimes that's the couch with an iPad. Sometimes it's a cheaper MacBook that exists mostly to be a remote control for the expensive one. [Jump Desktop](https://jumpdesktop.com/) gives me a screen share, [Blink Shell](https://blink.sh/) gives me a terminal on the iPad, and the work keeps going.

There was one snag: signed commits.

I sign all my commits, and my signing keys live in the Secure Enclave via [Secretive](https://github.com/maxgoedjen/secretive). That's great for security. The key physically cannot leave the machine. It's less great when git asks the Secure Enclave to sign a commit and the Secure Enclave asks for a Touch ID press from a person who is forty miles away from the keyboard.

[image1: A person relaxing on a couch with an iPad, remotely controlling a MacBook on a desk in another room. A glowing padlock hovers over the MacBook. Cozy, slightly whimsical illustration style.]

This post walks through the setup I landed on. I can be on my iPad or the cheap laptop, remote into the main Mac, and every commit gets signed by a hardware-backed key. Even commits made by a Claude Code session that's been running since morning. The approval prompt shows up on the device in my hands.

## Why Secretive Instead of a Key File?

The traditional approach stores your SSH private key in a file like `~/.ssh/id_ed25519`. The problem is that it's just that: a file. Any process running as you can read it. A malicious npm package can quietly copy it. So can a compromised build script or a sloppy backup. Once it's copied, it's gone, and you won't even know. A passphrase helps less than you'd think because the decrypted key sits in ssh-agent's memory anyway. Besides, you type that passphrase into enough prompts that you eventually stop looking at them.

Secretive generates keys inside the Secure Enclave, the same hardware chip that guards Touch ID and Apple Pay. The private key never exists on disk or in process memory, and there's no export button. Not even for you. When something wants a signature, it has to ask the enclave, and the enclave asks you for Touch ID or Apple Watch approval first.

That changes the threat model. Malware can't steal a key it can't read. The best it can do is request a signature, and an unexpected approval prompt is a pretty good alarm bell. A stolen laptop yields nothing usable. And since each machine's key is born in that machine's enclave and dies with it, revoking the key for a laptop I sold is a one-line change instead of a fire drill.

The tradeoff is the same as the feature: the key can't leave the machine. Which brings us back to my problem.

## The Setup

Each of my Macs has its own Secretive key. Git signs with SSH keys these days (no GPG ceremony required), so the base config is small:

```ini
[user]
    localSigningKey = ~/Library/Containers/com.maxgoedjen.Secretive.SecretAgent/Data/PublicKeys/<hash>.pub
[gpg]
    format = ssh
[gpg "ssh"]
    program = /usr/bin/ssh-keygen
    allowedSignersFile = ~/.dotfiles/git/allowed_signers
[commit]
    gpgsign = true
```

The `allowed_signers` file maps my email addresses to every machine's public key, so any machine can verify commits signed by any other:

```text
haacked@gmail.com ecdsa-sha2-nistp256 AAAA… GitHub-Commit-Signing@secretive.HaackBook-Pro.local
phil.h@posthog.com ecdsa-sha2-nistp256 AAAA… GitHub-Commit-Signing@secretive.HaackBook-Pro.local
haacked@gmail.com ecdsa-sha2-nistp256 AAAA… GitHub-Signing-Key@secretive.Phils-MacBook-Pro.local
…
```

Each public key is also registered on GitHub as a signing key, so GitHub shows the Verified badge no matter which machine did the signing.

> [!NOTE]
> That's `localSigningKey`, not `signingkey`. It's a custom config key I made up, and the difference matters. More on that in a moment.

So far, so standard. Sitting at any one of these machines, `git commit` triggers a Touch ID prompt and produces a signed commit. The interesting part is what happens when nobody is sitting at the machine.

## The Problem with Being Remote

When I remote into the main Mac, there are two ways in, and both break signing in their own way.

**Screen sharing (Jump Desktop)**: I'm looking at the Mac's actual desktop, driving its actual shells. When git commits, it asks the Mac's local Secretive for a signature, and Secretive pops a Touch ID prompt on a laptop that may well have its lid closed, in a room I'm not in. My iPad can show me the prompt. It can't press a fingerprint through the glass.

**SSH**: SSH has agent forwarding, which is almost the answer. With `ForwardAgent yes`, my client device's SSH agent (Secretive on the cheap MacBook, Blink's Secure Enclave keys on the iPad) becomes reachable on the main Mac through a socket that sshd creates. Signature requests travel back over the SSH connection to the device in my hands. That's exactly what I want.

But naive agent forwarding has three gaps:

1. **The socket dies with the session.** sshd puts it at a random path, exports it as `SSH_AUTH_SOCK` in that one session, and deletes it when the connection drops. Every other shell on the machine has no idea it exists.
2. **Long-lived processes never see it.** A Claude Code session started this morning inherited whatever `SSH_AUTH_SOCK` existed this morning. It's not going to re-read my zshrc because I SSHed in after lunch. Neither is the shell inside the Jump Desktop screen share.
3. **Git needs to know which key to ask for.** SSH signing requires the specific public key up front. When I'm remote, the right key is the client device's key, not the Mac's. A key baked into git config is wrong half the time.

I went looking for a way to remotely approve the Mac's own Secretive prompts. There isn't one. Secretive's approval is deliberately local-only, which is kind of the whole point of it. So instead of trying to weaken the Mac's key, I stopped using the Mac's key when I'm not at the Mac. Sign with the key of the device I'm actually touching.

## One Symlink, Zero Cached State

The whole system comes down to two decisions:

1. There is exactly one stable agent path on the machine: `~/.ssh/agent.sock`, a symlink. Shells, GUI apps, and git all point at the symlink, and the symlink points at whichever real agent socket is currently alive.
2. Git figures out the signing key at the moment of signing, not ahead of time. No cached key file, nothing to go stale.

Everything else is plumbing to keep those two things true. All of it lives in [my dotfiles](https://github.com/haacked/dotfiles/).

### The Symlink Healer

A small script, [`bin/ssh-agent-sync`](https://github.com/haacked/dotfiles/blob/main/bin/ssh-agent-sync), decides where the symlink points. Handed a live forwarded socket, it points the symlink there. Otherwise, if the symlink is dangling, it heals back to the local Secretive socket:

```bash
sock="$HOME/.ssh/agent.sock"
secretive="$HOME/Library/Containers/com.maxgoedjen.Secretive.SecretAgent/Data/socket.ssh"
forwarded="${1:-}"

if [[ -n "$forwarded" && -S "$forwarded" ]]; then
  [[ "$sock" -ef "$forwarded" ]] || ln -sf "$forwarded" "$sock"
elif [[ ! -S "$sock" ]] && [[ -S "$secretive" ]]; then
  ln -sf "$secretive" "$sock"
fi

if [[ -S "$sock" ]] && [[ "$sock" -ef "$secretive" ]]; then
  echo "local"
elif [[ -S "$sock" ]]; then
  echo "forwarded"
else
  echo "none"
fi
```

It's idempotent. It only touches the symlink when the symlink is wrong, and it reports which mode it resolved to (`local`, `forwarded`, or `none`) so callers don't have to figure that out themselves.

### The Shell Hook

My zshrc captures the forwarded socket and keeps the symlink synced. The subtle bits are when it captures and how often it syncs:

```sh
# Capture the sshd-provided socket once, at startup, before the first
# sync overwrites SSH_AUTH_SOCK with the symlink path.
[[ -n "$SSH_CONNECTION" ]] && _SSH_FWD_SOCK="$SSH_AUTH_SOCK"

_ssh_agent_sync() {
  "$HOME/.dotfiles/bin/ssh-agent-sync" "$_SSH_FWD_SOCK" >/dev/null
  [[ -S "$HOME/.ssh/agent.sock" ]] && export SSH_AUTH_SOCK="$HOME/.ssh/agent.sock"
}
_ssh_agent_sync
autoload -Uz add-zsh-hook
add-zsh-hook precmd _ssh_agent_sync
```

The forwarded socket path has to be captured before the first sync. After that, `SSH_AUTH_SOCK` is the symlink, and re-deriving the forwarded socket from it would just compare the symlink against itself.

It also re-syncs on every `precmd`, not just at shell startup. The symlink is shared machine-wide, and a local shell that was running before my SSH session started will never source zshrc again when that session ends. Without the recheck, that shell would keep pointing at a forwarding socket sshd already tore down. Since the healer is idempotent, idle prompts don't pay for a redundant `ln`.

Notice that every shell exports the symlink, never the raw socket. That's the indirection that makes long-lived sessions work. A Claude Code session started hours ago holds a path that never changes. What's on the other end of it does.

### Resolving the Key at Sign Time

Here's where `localSigningKey` pays off. Git has an escape hatch: if `user.signingkey` is unset, git runs `gpg.ssh.defaultKeyCommand` to ask which key to use, fresh, on every signature:

```ini
[gpg "ssh"]
    defaultKeyCommand = ~/.dotfiles/bin/git-signing-key
```

[`bin/git-signing-key`](https://github.com/haacked/dotfiles/blob/main/bin/git-signing-key) re-runs the healer, then answers based on what the symlink currently points to:

```bash
mode=$("$bin_dir/ssh-agent-sync") || mode="none"

if [[ "$mode" == "forwarded" ]]; then
  forwarded_key=$(SSH_AUTH_SOCK="$sock" ssh-add -L 2>/dev/null | head -1) || true
  if [[ "$forwarded_key" == ssh-* || "$forwarded_key" == ecdsa-* || "$forwarded_key" == sk-* ]]; then
    printf 'key::%s\n' "$forwarded_key"
    exit 0
  fi
fi

local_signing_key=$(git config --global --includes --get user.localSigningKey 2>/dev/null) || local_signing_key=""
if [[ -n "$local_signing_key" && -f "$local_signing_key" ]]; then
  printf 'key::%s\n' "$(cat "$local_signing_key")"
  exit 0
fi
```

If a forwarded agent is live, sign with whatever key it offers. That's the client device's Secure Enclave key. Otherwise, fall back to this machine's own Secretive key via `user.localSigningKey`. Whichever agent is alive when git signs is the one that gets asked.

An earlier version of this setup cached the resolved key in a file that shell hooks kept up to date. That had a hole in it. Anything that signed without a shell prompt having been drawn since the last agent change (a background agent session, a GUI git client) would sign with a stale key or fail. Deleting the cache and resolving at use time made that whole category of bugs impossible.

### GUI Apps

One LaunchAgent seeds the environment for everything that never sources zshrc:

```xml
<key>ProgramArguments</key>
<array>
    <string>/bin/sh</string>
    <string>-c</string>
    <string>"$HOME/.dotfiles/bin/ssh-agent-sync" >/dev/null && /bin/launchctl setenv SSH_AUTH_SOCK "$HOME/.ssh/agent.sock"</string>
</array>
<key>RunAtLoad</key>
<true/>
```

`launchctl setenv` makes `SSH_AUTH_SOCK` visible to GUI apps. And because it's set to the symlink rather than a raw socket, those apps track agent transitions too, without ever knowing transitions happen.

### The SSH Client Side

The last piece is the client config, which is refreshingly boring:

```text
Host phils-macbook-pro macbook
    HostName 100.x.y.z
    ForwardAgent yes

Host *
    IdentityAgent "~/Library/Containers/com.maxgoedjen.Secretive.SecretAgent/Data/socket.ssh"
```

The IP is a [Tailscale](https://tailscale.com/) address, so `ssh macbook` works from anywhere without exposing sshd to the internet. `ForwardAgent yes` does the forwarding. `IdentityAgent` points outbound SSH at Secretive, so authenticating to the Mac is enclave-backed too. On the iPad, Blink Shell does the equivalent with its own Secure Enclave keys.

## What It Looks Like in Practice

From the iPad or the cheap MacBook: `ssh macbook`. Tmux auto-attaches to my persistent session, and from that moment the entire machine signs through the device in my hands. The SSH session, sure. But also the Jump Desktop screen share I have open next to it, and the Claude Code session that's been grinding away since breakfast. They all hold the same symlink, and the symlink now points at the forwarded agent.

When any of them commits, the approval prompt appears right here. Touch ID on the cheap MacBook. Face ID via Blink on the iPad. The commit gets signed with this device's key, which is a different key than the main Mac would use, but `allowed_signers` and GitHub know all my keys, so every commit verifies the same.

When I close the connection, the next shell prompt on the Mac notices the dead socket and heals the symlink back to local Secretive. I sit down at the desk and Touch ID works like nothing happened. There's no remote mode to toggle. Just a symlink that points at whatever agent is real right now.

## Caveats

**Agent forwarding is a trust decision.** A forwarded agent means anyone with root on the host can send signature requests to your client's agent. Forwarding into a box you don't control is how people get their keys borrowed. I'm forwarding into my own Mac over my own tailnet, and every request still requires biometric approval on the client, so I sleep fine. I wouldn't `ForwardAgent yes` to a shared server.

**Pure screen share can't sign unattended.** If there's no live SSH connection, the symlink points at the Mac's local Secretive, and that still wants a human at the machine. Usually the fix is one `ssh macbook` from whatever device I'm holding. For the rare case where even that's not an option, I commit with `--no-gpg-sign` and batch re-sign later:

```ini
[alias]
    sign-unpushed = "!f() { DEFAULT=$(git default); UPSTREAM=${1-origin/$DEFAULT}; git rebase --exec \"git commit --amend --no-edit -S\" $UPSTREAM; }; f"
```

`git sign-unpushed` rebases the unpushed commits and re-signs each one. Back at the desk, it's a satisfying little drumroll of Touch ID prompts.

## Steal It

At no point in any of this does a private key exist as a file. Each device has its own key sealed in its own Secure Enclave. Every signature requires a biometric approval on a device I'm physically touching. Losing a device revokes exactly one key.

And I didn't give up anything to get here. Claude Code works on the big machine, I supervise from whatever screen is nearby, and commits come out signed and verified. The whole thing is two small scripts, a symlink, and one underappreciated git config setting.

Everything is in my [dotfiles repo](https://github.com/haacked/dotfiles/): `bin/ssh-agent-sync`, `bin/git-signing-key`, the zshrc hook, and the LaunchAgent. That's what dotfiles are for.
