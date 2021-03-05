---
title: Did Microsoft Violate TestDriven.NET's EULA in Enforcing Its Own EULA?
tags: [legal,microsoft,community]
redirect_from: "/archive/2007/05/31/did-microsoft-violate-testdriven.nets-eula-to-defend-its-own-eula.aspx/"
---

![takeoff](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ATechnicalEvaluationofMicrosoftsCase.NET_13543/r_takeoff_1.gif)

Jamie Cansdale recently wrote about [some legal
troubles](http://weblogs.asp.net/nunitaddin/archive/2007/05/30/microsoft-vs-testdriven-net-express.aspx "Microsoft vs TestDrivien.NET Express")
he has with Microsoft. We were in the middle of an email correspondence
on an unrelated topic when he told me about the new chapter in this long
saga.

Jamie posted the entire email history and the three (so far) letters
received from Microsoft’s legal team. Rather than jump to any
conclusions, let’s dig into this a bit.

## The Claim

First, let’s examine the claim. In the first letter from OLSWANG, the
legal team representing Microsoft, the portion of the EULA for the
Visual Studio Express suite of products that Jaime is allegedly in
violation of is the following:

> ...you may use the software only as expressly permitted in this
> agreement. In doing so, you must comply with any technical limitations
> in the software that only allow you to use it in certain ways... You
> may not work around any technical limitations in the software.

The letter continues with...

> Your product enables users of Express to access Visual Studio
> functionality that has been de-activated in Express *and* to add new
> features of your own design to the product, thereby circumventing the
> measures put in place to prevent these scenarios.

## What *Technical Limitation*?

The interesting thing about all this is that nowhere in all the emails
is it specific about which “technical limitation” Jaime is supposedly
working around. Exactly what functionality has been “de-activated”?

So I decided to take a look around to see what I could find. The best I
could find is this [feature comparison chart](http://msdn2.microsoft.com/en-us/vstudio/aa700921.aspx "Visual Studio 2005 Product Line Overview").

In the row with the heading *Extensibility,*it says this about the
Express Products.

> Use 3rd party controls and content. No Macros, Add-ins or Packages

So 3rd party controls and content are enabled, but Macros and Add-ins or
packages are not enabled in this product.

When I pointed this out to Jaime, he pointed out that this is not true.
If the Express editions could not support Add-Ins, how does Microsoft
release a [Reporting Add-in for Microsoft Visual Web Developer 2005
Express](http://www.microsoft.com/downloads/details.aspx?FamilyID=D09C1D60-A13C-4479-9B91-9E8B9D835CDC&displaylang=en#ReportAddin "Reporting Add-In for Visual Web Developer Express")
or the [Popfly for Visual Studio Express
Users](http://www.popfly.com/Overview/Explorer.aspx "Popfly for Visual Studio Express")?

I imagine that Microsoft is probably not bound by their own EULA and
would be allowed to work around technical limitations in their own
product to create these Add-Ins. But another potential interpretation is
that creating these add-ins *is* possible and that there is no technical
limitation in the Express products.

**The problem here is how do you define a *technical limitation*.** It’s
obvious that the Express product did not remove support for add-ins in
the compiled code. In fact, it seems it didn’t remove add-in support at
all, it just didn’t provide a convenient manner for registering add-ins.
Is an omission the same thing as technical limitation?

Jamie sent me some code samples to demonstrate that he is in fact only
using public well documented APIs to get TestDriven.NET to work to show
up in the Express menus. He’s not decompiling the code, using any crazy
hacks or workarounds. It’s very simple straightforward code.

The only thing he does which might be interpreted as questionable is to
write a specific registry setting so that the TestDriven.NET menu
options show up within Visual Studio Express.

So it seems that supporting Add-Ins does not require any
decompilation. All it requires is adding a specific registry entry**.
Does that violate the EULA? Well whether I think so or not doesn’t
really matter. I’m not a lawyer and I’m pretty sure Microsoft’s lawyers
would have no problem convincing a judge that this is the case.

I would hope that we should have a higher standard for *technical
limitation* than something so obvious as a registry setting. If rooting
around the registry can be considered decompilation and violate EULAs,
we’ve got issues.

## The Kicker

Also, if that is the case, then you have to wonder about this section in
Microsoft’s letter to Jamie, which I glossed over until I noticed [Leon
Bambrick mention
it](http://www.secretgeek.net/testdrivengate.asp "Leon Bambrick - TestDrivenGate")...

> Thank you for not registering your project extender during
> installation and turning off your hacks by default. It appears that by
> setting a registry key your hacks can still be enabled. When do you
> plan to remove the Visual Studio express hacks, including your addin
> activator, from your product.

This is interesting on a couple levels.

First, if the lack of a registry entry is sufficient to count as a
“technical limitation” and “de-activation” of a feature in Visual Studio
Express, why doesn’t that standard also apply to TestDriven.NET?
Having removed the registry setting that lets TD.NET work in Express,
hasn’t Jamie complied?

Second, take a look at this snippet from [TestDriven.NET’s
EULA](http://testdriven.net/downloads/TestDriven.Professional%20EULA.pdf "TestDriven.NET Pro EULA")...

> Except as expressly permitted in this Agreement, Licensee shall not,
> and shall not permit 
> others to: ...
>
> ​(ii) reverse engineer, decompile, disassemble or otherwise reduce the
> Software to source code form;
>
> ...
> (v) use the Software in any manner not expressly 
> authorised by this Agreement.

It seems that by Microsoft’s own logic of what counts as a license
violation, **Microsoft itself has committed such a violation** by
reverse engineering TestDriven.NET to enable a feature that was
purposefully disabled via a registry hack.

## The Heart Of The Matter

All this legal posturing and gamesmanship aside, let’s get to the heart
of the matter. So it may well be that Microsoft is in its legal right
(*I’m no lawyer, so I don’t know for sure, but stick with me here*).
Hooray for you Microsoft. Being in the right is nice, but knowing when
to exercise that right is a true sign of wisdom. Is this the time to
exercise that right?

You’ve recently given [yourself one black eye](https://haacked.com/archive/2007/05/13/is-fighting-open-source-with-patents-a-smart-move-by.aspx "Is Fighting Open Source With Patents a Smart Move by Microsoft?")
in the developer community. Are you prepared to give yourself yet
another and continue [to erode your
reputation](http://www.hanselman.com/blog/IsMicrosoftLosingTheAlphaGeeks.aspx "Is Microsoft losing the Alpha Geeks?")?

The justification you give is that products like this that enable
disabled features in Visual Studio Express (a dubious claim) will hurt
sales of the full featured Visual Studio.NET. Really?! If I were you,
I’d worry more about the loss in sales represented by the potential
exodus developers leaving due to your heavy handed tactics and missteps.

