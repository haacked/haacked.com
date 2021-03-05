---
title: Using Rhino Mocks To Unit Test Events on Interfaces
tags: [code,tdd]
redirect_from: "/archive/2006/06/22/usingrhinomockstounittesteventsoninterfaces.aspx/"
---

![Rhino](https://haacked.com/assets/images/Rhino.jpg) I am working on some code
using the [Model View
Presenter](http://www.martinfowler.com/eaaDev/ModelViewPresenter.html "Fowler Article on MVP")
pattern for an article I am writing. I am using an event based approach
based on the work that Fowler did. For the sake of this discussion, here
is an example of a simplified view interface.

```csharp
public interface IView
{
    event EventHandler Load;
}
```

In the spirit of TDD I follow this up with the shell of my `Presenter`
class

```csharp
public class Presenter
{
    public Presenter(IView view)
    {
        throw new NotImplementedException("Not implemented.");
    }
}
```

And this is where I reached my first dilemma. What is the best way to
write my first unit test to test that the presenter class attaches
itself to the view’s events? Well I could write a stub class that
implements the interface and add a method to the stub that will raise
the event. In this example, that would be quite easy, but in the real
world, the interface might have multiple properties or methods and why
bother going through the trouble to implement them all just to test one
event? This is where a mock testing framework such as [Rhino
Mocks](http://www.ayende.com/projects/rhino-mocks.aspx "Rhino Mocks")
comes into play.

```csharp
[Test]
public void VerifyAttachesToViewEvents()
{
    MockRepository mocks = new MockRepository();
    IView viewMock = (IView)mocks.CreateMock(typeof(IView));
    viewMock.Load += null;
    LastCall.IgnoreArguments();
    mocks.ReplayAll();
    new Presenter(viewMock);
    mocks.VerifyAll();   
}
```

The second line of code creates a dynamic proxy that implements the
`IView` interface. In the third line, I set an expectation that the
`Load` event will be attached to. The line afterwards tells Rhino Mocks
to ignore the arguments in the last call. In other words, the Rhino
Mocks will expect the that the `Load` event will be attached, but don’t
worry about which method delegate gets attached to the event. Without
that line, the test would expect that `null` is attached to the load
event, which we do not want.

Finally we call `ReplayAll()`. I kinda think this is a misnomer since
what it really is doing, as far as I know, is telling the mock framework
that we are done setting all our expectations and we are going to
actually conduct the test now. Up until this method call, every method,
property, or event set on the mock instance is telling the mock that we
are going to call that particular member. If one of the expected members
is not invoked, the test has failed.

So finally after setting all these expectations, I create an instance of
`Presenter` which is the code being tested. I then ask the mock
framework to verify that all our expectations were met. Of course this
test fails, which is good, since I haven’t yet implemented the
presenter. Implementing the presenter is pretty straightforward.

```csharp
public class Presenter
{
    IView view;
    public Presenter(IView view)
    {
        this.view = view;
        this.view.Load += new EventHandler(view_Load);
    }

    void view_Load(object sender, EventArgs e)
    {
        throw new Exception("Not implemented.");
    }
}
```

Now my test passes. But wait! It gets better. Now suppose I want to
write a new test to test that the presenter handles the `Load` event.
How do I raise the `Load` event on my mock `IView` instance? Rhino Mocks
provides a way. First I will add a boolean property to the `Presenter`
class named `EventLoaded` and then write the following test. This will
allow me to know whether or not the event was raised. This is a
contrived example of course. In a real project, you probably have some
other condition you could test to verify that an event was raised.

I then write my test.

```csharp
[Test]
public void VerifyLoadEventHandled()
{
    MockRepository mocks = new MockRepository();
    IView viewMock = (IView)mocks.CreateMock(typeof(IView));
    viewMock.Load += null;
    IEventRaiser loadRaiser 
         = LastCall.IgnoreArguments().GetEventRaiser();
    mocks.ReplayAll();
    Presenter presenter = new Presenter(viewMock);
    loadRaiser.Raise(viewMock, EventArgs.Empty);
    mocks.VerifyAll();
    Assert.IsTrue(presenter.EventLoaded);
}
```

This test looks similar to the last test, but note the bolded line
(fourth line). This line creates an event raiser for the `Load` event
(ignoring arguments to the event of course). Now I can use that later to
raise the event after I create the presenter. Running this test fails,
as expected. We have to finish the implementation of the `Presenter`
class as follows:

```csharp
public class Presenter
{
    IView view;
    public Presenter(IView view)
    {
        this.view = view;
        this.view.Load += new EventHandler(view_Load);
    }

    public bool EventLoaded
    {
        get { return this.eventLoaded; }
        set { this.eventLoaded = value; }
    }

    bool eventLoaded;

    void view_Load(object sender, EventArgs e)
    {
        this.eventLoaded = true;
    }
}
```

Now when I run the test, it succeeds. Pretty dang nifty. Many thanks to
[Ayende](http://www.ayende.com/default.aspx "Ayende Blog") for clearing
up some confusion I had with the [Rhino Mocks
documentation](http://www.ayende.com/projects/rhino-mocks/documentation.aspx "Rhino Mocks Docs")
surrounding events.

