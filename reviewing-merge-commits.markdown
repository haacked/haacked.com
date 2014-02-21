Okay, so it may not be the best example, but github/github@6a961d6 is an example commit that includes a conflict resolution. At the command line you can look at `git diff 6a961d6a^1 6a961d6a` and `git diff 6a961d6a^2 6a961d6a` to see the changes on either side of the merge, and then if you run `git log -1 -p --cc 6a961d6ae1a829677849d2cb5c01396d25adfaae` you will see just the lines that were updated as part of the conflict resolution.

niik: It's not generally that hard to find an example. I just do `git log --min-parents=2 -p --cc` and then search for "diff" to find a merge commit that actually has a diff.

then you can diff that vs ^1 and ^2 to see the two sides of the merge and also look at"
 $ git log -p --cc -1 181c82420ff20182d508569f86dcfe8650957754
commit 181c82420ff20182d508569f86dcfe8650957754