---
title: "Argument parsing with Abbot"
description: "Abbot has some neat built-in tools for argument parsing that make use of C# tuple deconstruction for fun and profit."
tags: [abbot,chatops,csharp]
excerpt_image: https://user-images.githubusercontent.com/19977/107692389-92799080-6c61-11eb-9710-c75811b528ee.jpg
---

Most Bot skills strive for a more natural language feel to arguments passed them. For example, to remember something with Abbot you can use `@abbot rem haacked's blog is https://haacked.com`. And then later recall it with `@abbot rem haacked's blog`. Or just `@abbot rem haacked` because Abbot uses fuzzy matching.

Abbot doesn't strive for true natural language processing yet because many skills need precision in calling them and natural language interfaces can be stressful to use as you figure out the right way to call them. This may change in the future and it's an area we hope to explore.

To achieve a more natural language feel, Bot skills tend to have a pretty simple format for the arguments passed to the skill. But even a simple format can require a fairly complex regular expression to parse correctly. And we all know what happens when you decide to [use a regular expression to solve a problem](http://regex.info/blog/2006-09-15/247). Spending a day writing regular expressions can make you feel like you've been slugged.

![Image of woman slugging a man - Simplified Pixabay License](https://user-images.githubusercontent.com/19977/107692389-92799080-6c61-11eb-9710-c75811b528ee.jpg "The feeling I get when I parse arguments with a regular expression")

Let's follow an example to see what I mean. Suppose we have a skill for managing another user's favorite songs with the following usage pattern.

```
@abbot fave {@mention} add {song} [description]
```

This skill allows the user to add a favorite song for another user with an optional description.

The regular expression to parse this seems relatively straightforward at first. Note that the arguments always omit the skill name so the arguments in this case would be the part after `fave`.

Here's my regex so far: `^(?<mention>.*?)\s+(?<cmd>.*?)\s+(?<song>.*?)(\s+(?<description>.*))?$`.

The following set of chat transcripts show how the skill might be used.

```
@haack: @abbot fave @paul add Dynamite
@abbot: I've added `Dynamite` to @paul's list of favorite songs.
```

So far so good. Now it gets a bit trickier if we want to add a favorite song with a description.

```
@haack: @abbot fave @paul add Chandelier Because Sia speaks to me
@abbot: I've added `Chandelier` with the description `Because Sia speaks to me` to @paul's list of favorite songs
```

Which part is the song and which part is the description? Since descriptions tend to be sentences, it might make sense to have the first word after the command be the title, and the rest be the description. Until you run into the following example:

```
@haack: @abbot fave @paul add Baby Shark Makes me dance
@abbot: I've added `Baby` with the description `Shark Makes me dance` to @paul's list of favorite songs
```

In this case, "Baby Shark" is the song. So what we need to do is allow quoting an argument.

```
@haack: @abbot fave @paul add "Baby Shark" Makes me dance
@abbot: I've added `Baby Shark` with the description `Makes me dance` to @paul's list of favorite songs.
```

Ah! That's better.

To get this better behavior, we need to update the regular expression we've been using to something a bit more complicated: `^(?<mention>.*?)\s(?<cmd>.*?)\s(?:\"(?<song>.*?)\"|(?<song>.*?))(\s(?<description>.*))?$`. That's not terrible, but as we handle more and more conditions, it gets more and more complicated.

Fortunately, Abbot handles this sort of argument parsing for you. If you write a C# skill, you have access to the arguments via the `Bot.Arguments` property. `Bot.Arguments` is a custom collection with some interesting properties to make argument handling easier. It contains a tokenized set of incoming arguments that already handles quoting and whitespace.

So in the case of the argument `@paul add "Baby Shark" Makes me dance` (the skill name is always omitted from the arguments), `Bot.Arguments` would contain the collection:

```csharp
[0]: @paul
[1]: add
[2]: Baby Shark
[3]: Makes
[4]: me
[5]: dance
```

"Now wait a minute," you say. "Don't we want the _description_, the _fourth_ argument, to have the rest of the words after the song." Right you are!

But in the `Bot.Arguments` collection, the fourth element in the collection is "Makes" and not the full description. This is a problem.

Not to worry, Abbot has a solution for this. `Bot.Arguments` implements [tuple deconstruction](https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct) in a special way. Suppose you know that you will have at most four arguments for a skill. You can deconstruct the arguments into a tuple like so.

```csharp
var (cmdArg, mentionArg, songArg, descriptionArg) = Bot.Arguments;
```

If there are more than four arguments, the remaining arguments are captured in the last tuple parameter, in this case `descriptionArg`. If you're familiar with JavaScript, this is a lot like [rest parameters](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/rest_parameters).

Even though the remaining arguments are captured in `descriptionArg` (which is what we want in this case), you can still cast `descriptionArg` to `IArguments` to access each token that made up the description, if you needed to for some reason.

If there are less than four arguments, then the last argument will be of type `IMissingArgument`. So let's put this all together.

```csharp
var (cmdArg, mentionArg, songArg, descriptionArg) = Bot.Arguments;

if (cmdArg.Value is "add") {
    if (!(mentionArg is IMentionArgument mention)) {
        await Bot.ReplyAsync("Please mention someone whose favorite song this is.");
        return;
    }
    if (songArg is IMissingArgument) {
        await Bot.ReplyAsync("Please mention someone whose favorite song this is.");
        return;
    }
    // Some magic here to save the favorite song...
    var response = descriptionArg is IMissingArgument
        ? $"I've added `{songArg.Value}` to {mention.Mentioned}'s list of favorite songs."
        : $"I've added `{songArg.Value}` with the description `{descriptionArg.Value}` to {mention.Mentioned}'s list of favorite songs.";
    await Bot.ReplyAsync(response);
    return;
}
```

The rest of the code is left as an exercise for the reader.

A few things to note. At the moment, we only support deconstructing up to a four-tuple. We can easily add a five-tuple or six-tuple in the future. But in most cases, four is enough. And if it's not, you can still deconstruct that fourth argument by casting it to `IArguments`.

There are helpful extension methods on `IArgument` (the base interface for all arguments). For example, `ToLocalTime` attempts to parse the argument as a local time (such as "2pm") and return a [`LocalTime`](https://nodatime.org/2.2.x/api/NodaTime.LocalTime.html) if it succeeds. Otherwise it returns null. We'll add more of these helpers as we go along. Let us know what else we should add by emailing [feedback@aseriousbusinees.com](mailto:feedback@aseriousbusiness.com) or use the Abbot `feedback` skill.

If an argument is a mention, you can cast it to `IMentionArgument` to access information about the mentioned user. Mentions are also in the `Bot.Mentions` collection.

Also, if the default argument parsing doesn't work for you, you can always access the full arguments with `Bot.Arguments.Value`. Python and JavaScript skills also receive the arguments as a collection in `bot.tokenized_arguments` and `bot.tokenizedArguments` respectively. They don't have the same deconstructors that the C# code does, but mainly because those languages already have similar list operations.

If you're interested in seeing the code for our argument parsing, check out [this gist](https://gist.github.com/haacked/adbdc12fc6c8ea21d639deb3763fdd98). It's in a gist for now because I wanted a quick way to share it. We need to organize our code so it's easier to share the parts we want to share as open source libraries. The parsing code is fairly simple right now, but we hope to expand it. It doesn't support argument flags and such because the usage pattern for bot skills tend to be different than what you'd use with a command line tool. However, we may consider using an open source full-fledged command line parser in the future if there's demand for it.

For more about writing skills for Abbot, check out the [Getting Started Guides](https://ab.bot/help/guides/).
