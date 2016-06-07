---
layout: post
title: "Building an Atom Package in ES6"
date: 2016-05-11 -0800
comments: true
categories: [nuget]
---

The tagline for the [Atom text editor](https://atom.io/) is "A hackable text editor for the 21st Century". As a Haack, this is a goal I can get behind.

It accomplishes this hackability by building on [Electron](http://electron.atom.io/), a platform for building cross-platform desktop applications with web technology (HTML, CSS, and JavaScript). The ability to leverage these skills in order to extend your text editor is really powerful.

I thought I'd put this to the test by building a simple extension for Atom. I decided to port the [Encourage extension for Visual Studio](http://haacked.com/archive/2014/06/20/encourage-vs/) I wrote a while back. For a lot of developers, this image rings true every day.

![How to program](https://cloud.githubusercontent.com/assets/19977/15877072/585bd80c-2cc6-11e6-8b6c-1daa39bfaa2c.png)

Who needs that negativity?! The Encourage extension for Atom displays a small bit of encouragement ("Way to go!", "You rock!", "People like you!") every time you save your document. Maybe it's true that nobody loves you, but your editor will, if you let it.

![Encourage screenshot](https://cloud.githubusercontent.com/assets/19977/15810806/21421734-2b57-11e6-9979-8a5092e6b417.png)

## Writing the extension

The [Atom Flight Manual](http://flight-manual.atom.io/) has a great [guide to creating and publishing an Atom package](http://flight-manual.atom.io/hacking-atom/sections/package-word-count/). The guide walks through using an Atom package that generates a simple package you can use as a starting point for your own package.

One tricky aspect though is that the documentation still assumes that the generated package is CoffeeScript. But all new Atom development (including the actual generated package) uses the latest version of JavaScript - ES6 (or ES2015 depending on who you ask).

I won't go into every detail about the package. You can see the [code on GitHub yourself](https://github.com/haacked/encourage-atom/). I'll just highlight a few gotchas I encountered.

By default, the "Generate Package" command creates a package that is activated via a command. Until you invoke the command, the package isn't activated. This confused me for a while because I wanted my package to be active when Atom starts up since it passively listens for the `onDidSave` event.

The trick here is to simply remove the `activationCommands` section from the `package.json` file.

```js
"activationCommands": {
  "atom-workspace": "my-package:toggle"
},
```

Then, the activation happens when the package is loaded. Many thanks to [@binarymuse](https://github.com/binarymuse) for that tip!

When you make changes to your extension, you can reload Atom by invoking `CTRL + ALT + R`. That'll save you from closing and reopening Atom all the time.

You can invoke the Developer Tools with the `CTRL + ALT + I` shortcut (similar to `CTRL + SHIFT + I` for Google Chrome). That'll allow you to step through the package code with the debugger.

Be sure to check out the [Atom API documentation](https://atom.io/docs/api/v1.8.0/AtomEnvironment) for details about the extensibility points provided by Atom. One of the challenges with Atom is there are so many different ways to extend it it's hard to know what the best approach is. Over time, I hope we start to gather these best practices.

For example, my package abuses the `Panel` class slightly by hacking the DOM element created to render the Panel. Panels tend to be a bar that's docked to the top, bottom, or side of the editor pane. The current API doesn't support resizing or fading out the Panel. I ended up using a mix of CSS and JavaScript to bend the Panel to my will and create the effect you get when you use this extension.

Maybe there's a better way, but I love that I had the ability to get this to work. I'll iterate on the package over time and make it better.

## Building and Testing the extension

By default, the extension comes with a few specs. You can run the specs by invoking `CTRL + ALT + P`. I set up continuous integration (CI) for the package [with AppVeyor](http://flight-manual.atom.io/hacking-atom/sections/writing-specs/) by following [these helpful instructions](https://github.com/atom/ci). I had continuous integration up and running in the matter of minutes.

## Publishing

Publishing an Atom package is super easy. Push your code to a public GitHub repository and then from the repository directory call `apm push patch|minor|major` depending on the type of change. The flight manual I mentioned has details on this command.

## What's Next?

I don't plan on investing a huge amount of time in this extension. It was more an exercise for me to learn about the Atom packaging system. If you're interested in helping out, I've already [started logging issues](https://github.com/haacked/encourage-atom/issues) such as being able to set the list of encouragements. I'd welcome the help!

For example, I want to add the ability for those who use the package to set up their own encouragements. Or perhaps, discouragements. I actually find it really [funny when my editor shits on my code](http://haacked.com/archive/2014/07/30/visual-studios-extensions-settings/). In fact, it causes me to think harder about my code because I want to prove it wrong. I should probably stop with all this editor anthropomorphism, huh?
