---
title: Indulging My OCD Using TestDriven.NET With NCoverExplorer
tags: [tools]
redirect_from: "/archive/2006/12/13/Indulging_My_OCD_Using_TestDriven.NET_With_NCoverExplorer.aspx/"
---

I don’t suffer from classic OCD (Obsessive Compulsive Disorder), but I
do sometimes have OCD tendencies. Just ask my poor wife when we’re
having dinner while my mind is still trying to resolve a thorny
programming problem. *Earth to Phil. Are you on this planet?*

Lately, the object of my OCD-like tendencies is getting the
Subtext unit
test code coverage up to 40%. At the time of this writing (and after
much work), it is at 38%. Why 40%? No reason. Just a nice round number
that we’ve never yet hit. Remember, OCD isn’t necessarily a rational
affliction.

**If code coverage is my
disease,**[**TestDriven.NET**](http://www.testdriven.net/ "TestDriven.NET")**with**[**NCoverExplorer**](http://www.kiwidude.com/blog/2006/01/ncoverexplorer-debut.html "NCoverExplorer")**integration
is my drug, and**[**Jamie
Cansdale**](http://weblogs.asp.net/nunitaddin/ "TestDriven.NET by Jamie Cansdale")**is
my dealer.** He graciously gave me a pro license as a donation to the
Subtext project.

So here’s the anatomy of a code coverage addiction. First, I simply
right click on our unit test project, *UnitTests.Subtext*, from within
VisualStudio.NET 2005 (also works with older versions of VS.NET). I
select the ***Test With*** menu option and click ***Coverage*** as in
the screenshot below*.*

![Test With Coverage in VS.NET
2005](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IndulgingMyOCDWith.NETWithNCoverExplorer_A08C/Test-With-Coverage%5B9%5D.png)

After running all of the unit tests, NCoverExplorer opens up with the a
coverage report.

![NCoverExplorer coverage results in Left
Pane](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IndulgingMyOCDWith.NETWithNCoverExplorer_A08C/NCoverExplorer-Result%5B4%5D.png)

I can drill down to take a look at code coverage all the way down to the
method level. In this case, let’s take a look at the *Subtext.Akismet*
assembly. Expanding that node, I can drill down to the *Subtext.Akismet*
namespace, then to the `HttpClient` class. Hey! The `PostClient` method
only has 91% code coverage! I’ve gotta do something about that!

![NCover Drill
Down](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IndulgingMyOCDWith.NETWithNCoverExplorer_A08C/NCoverExplorer-DrillDown-Left%5B15%5D.png)

When I click on the method, NCoverExplorer shows me the code in the
right pane along with which lines of code were covered. The lines in red
were not executed by my unit test. Click on the below image for a
detailed look.

[![NCoverExplorer Code Coverage
Pane](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IndulgingMyOCDWith.NETWithNCoverExplorer_A08C/NCoverExplorer-DrillDown_thumb%5B2%5D.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IndulgingMyOCDWith.NETWithNCoverExplorer_A08C/NCoverExplorer-DrillDown%5B10%5D.png)

As you can see, there are a couple of exception cases I need to test. It
turns out that one of these exception cases never happens, which is why
I cannot get that line covered. This may be better served using a
Debug.Assert statement than throwing an exception.

If you haven’t played around with TestDriven.NET and NCoverExplorer,
give it a twirl. But be careful, this is powerful stuff and you may
spend the next several hours writing all sorts of code to get that last
line of code tested.

**Here are a few posts I’ve written that you may find useful to eke out
every last line of code coverage.**

-   [Unit Test Events With Anonymous
    Delegates](https://haacked.com/archive/2006/12/13/Tip_Jar_Unit_Test_Events_With_Anonymous_Delegates.aspx "Testing Event Sources")
-   [Using Rhino Mocks to Unit Test Events On
    Interfaces](https://haacked.com/archive/2006/06/23/UsingRhinoMocksToUnitTestEventsOnInterfaces.aspx "Testing Event Handling Code")
-   [Using WebServer.WebDev for Unit
    Tests](https://haacked.com/archive/2006/12/12/Using_WebServer.WebDev_For_Unit_Tests.aspx "Use An Http Server from your unit test code")
-   [A Testing Mail Server for Unit Testing Email
    Functionality](https://haacked.com/archive/2006/05/30/ATestingMailServerForUnitTestingEmailFunctionality.aspx "An SMTP Server you can use from your unit tests.")
-   [Simulating Http Context For Unit Tests Without Using Cassini nor
    IIS](https://haacked.com/archive/2005/06/11/Simulating_HttpContext.aspx "Simulate the HttpContext")
-   [Unit Testing Data Access Code with the
    StubDataReader](https://haacked.com/archive/2006/05/31/UnitTestingDataAccessCodeWithTheStubDataReader.aspx "A useful class for stubbing the IDataReader interface")

Now get out there and test!

