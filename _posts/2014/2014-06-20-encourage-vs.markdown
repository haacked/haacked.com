---
layout: post
title: "Your Editor should Encourage You"
date: 2014-06-20 -0800
comments: true
categories: [vs vsix dev encouragement]
---

[I love to code](http://haacked.com/archive/2008/12/29/i-love-to-code.aspx/) as much as the next developer. I even professed my love [in a keynote once](https://www.youtube.com/watch?v=HYnEhDOKoxA).  
And judging by the fact that you're reading this blog, I bet you love to code too.

But in the immortal words of that philosopher, Pat Benatar,

> Love is a battlefield.

There are times when writing code is drudgery. That love for code becomes obsession and leads to an unhealthy relationship. Or worse, there are times when the thrill is gone and the love is lost. You're just going through the motions.

In those dark times, bathed in the soft glow of your monitor, engrossed in the rhythmic ticky tacka sound of of your keyboard, a few kind words can make a big difference. And who better to give you those kind words than your partner in crime - your editor.

With that, [I give you __ENCOURAGE__](http://visualstudiogallery.msdn.microsoft.com/1f3afebb-06c7-4b77-a54f-eb2f0784008d). It's a Visual Studio extension that provides a bit of encouragement every time you save your document. Couldn't we all use a bit more whimsy in our work?

![encouragement light](https://cloud.githubusercontent.com/assets/19977/3343412/5e5b933a-f89f-11e3-8c2b-21277dcd19e1.png)

And it's theme aware!

![encouragement dark](https://cloud.githubusercontent.com/assets/19977/3343409/5c74eb98-f89f-11e3-9a50-2aaa0983dd83.png)

## hWhat?!

Yes, it's silly. But try it out and tell me it doesn't put an extra smile on your face during your day.

This wasn't my idea. My co-worker [Pat Nakajima](http://patnakajima.com) came up with this idea and built a TextMate extension to do this. He showed it to me and I instantly fell in love. With the idea. And Pat, a little.

Apparently it's very easy to do this in TextMate. Here's the full source code:

```ruby
 #!/usr/bin/env ruby -wU

  puts ['Nice job!', 'Way to go!', 'Wow, nice change!'].sample
```

It's a bit deceiving because most of the work in getting this to work in Textmate is configuration.

![encourage-nakajima](https://cloud.githubusercontent.com/assets/19977/3345563/21038c2a-f8ba-11e3-973d-4ad3bd12776b.png)

As for Visual Studio, it takes quite a bit more work. You can find the [source code on GitHub under an MIT license](https://github.com/haacked/encourage).

The code hooks into the `DocumentSaved` event on the `DTE` and then cleverly (or hackishly depending on how you look at it) uses an [`IIntellisenseController`](http://msdn.microsoft.com/en-us/library/microsoft.visualstudio.language.intellisense.iintellisensecontroller.aspx) combined with an [`ISignatureHelpSource`](http://msdn.microsoft.com/en-us/library/microsoft.visualstudio.language.intellisense.isignaturehelpsource.aspx) to provide the tool tip.

Here's the relevant code snippet from the [`EncourageIntellisenseController`](https://github.com/Haacked/Encourage/blob/master/EncouragePackage/EncourageIntellisenseController.cs) class:

```csharp
public EncourageIntellisenseController(
  ITextView textView,
  DTE dte,
  EncourageIntellisenseControllerProvider provider)
{
  this.textView = textView;
  this.provider = provider;
  this.documentEvents = dte.Events.DocumentEvents;
  documentEvents.DocumentSaved += OnSaved;
}

void OnSaved(Document document)
{
  var point = textView.Caret.Position.BufferPosition;
  var triggerPoint = point.Snapshot
    .CreateTrackingPoint(point.Position, PointTrackingMode.Positive);
  if (!provider.SignatureHelpBroker.IsSignatureHelpActive(textView))
  {
    session = provider.SignatureHelpBroker
      .TriggerSignatureHelp(textView, triggerPoint, true);
  }
}
```

Many thanks to Pat Nakajima for the idea and [Jared Parsons](http://blog.paranoidcoding.com/) for his help with the Visual Studio extensibility parts. I'm still a n00b when it comes to extending Visual Studio and this silly project has been one fun way to try and get a handle on things.

## Get Involved!

As of today, this only supports Visual Studio 2013 because of my ineptitude and laziness. I welcome contributions to make it support more platforms.

## Parting Thoughts

On the positive side, when you need a specific service, it's nice to be able to slap an `[Import]` attribute and magically have the type available. The extensibility of Visual Studio appears to be nearly limitless.

On the downside, it's ridiculously difficult to write extensions to do some basic tasks. Yes, a big part of it is the learning curve. But when you compare the Textmate example to what I had to do here, clearly there's some middle ground here between simplicity and power.

Also, the documentation is quite good but often wrong in places. For example, in [this Walkthrough it notes](http://msdn.microsoft.com/en-us/library/ee197646.aspx):

> Make sure that the Content heading contains a MEF Component content type and that the Path is set to QuickInfoTest.dll.

That might have been true with the old VSIX manifest format, but is not correct for the new one. So none of my MEF imports worked until I added this line to the `Assets` element in my `.vsixmanifest` folder.

```xml
<Asset
  Type="Microsoft.VisualStudio.MefComponent"
  d:Source="Project"
  d:ProjectName="%CurrentProject%"
  Path="|%CurrentProject%|" />
```

I'm not really sure why that's just not there by default.

There are certainly a lot of extensions in the Visual Studio Extension Gallery, so I would still call consider the extensibility model to be a success for the most part. But there could be a lot more extensions in there. More people should be able to extend the IDE for their own needs without having to take a graduate course in Visual Studio Extensibility.
