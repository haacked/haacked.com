---
title: Gain Control Of Your Control State
tags: [aspnet,code]
redirect_from: "/archive/2007/03/15/gain-control-of-your-control-state.aspx/"
---

Some people think the [`ViewState` is the spawn of the
devil](http://staff.interesource.com/james/aug06/viewstate_postbacks_harmful.htm "ASP.NET Postbacks and ViewState Considered Harmful").
Not one to be afraid of being in bed with the devil, I feel a tad bit
less negative towards it, as it can be very useful.

Still, it has its share of disadvantages. It sure can get bloated. Not
only that, but disabling ViewState can wreack havock with the
functionality of many controls.

This is why ASP.NET 2.0 introduces the control state. **The basic idea
is that there is some state that should be considered the data for the
control, while other state is necessary for the control to function.**
For example, the contents of a GridView. The control doesn’t absolutely
need this data persisted across postbacks to function properly. You
could choose to reload it from the database, `Cache`, or `Session`.

In contrast is the state of the selected node in a `TreeView`. This is
state that is necessary for the control to function properly across
postbacks.

Unlike the `ViewState`, the control state isn’t implemented as a
property bag. You have to do a little bit of extra work to make use of
it. Namely, there are two methods you have to implement in your custom
control.

-   **`LoadControlState`** – Restores the control state from a previous
    page request. ASP.NET calls this method passing in the control state
    as an object to this method.
-   **`SaveControlState`** – Saves any changes to control state since
    the last post back. You need to return the state of the control as
    the return value of this method. ASP.NET will store it.

Your custom control must also register the fact that it needs the
control state by calling `Page.RegisterRequireControlState`.

### A Demonstration That Makes This All Clear As Mud

I’ve put together a simple control to demonstrate the control state. Now
before I go any further, I must warn you not to copy and paste this
implementation. This implementation is designed to clarify how the
control state works. I will present another implementation that
describes a safer approach, which you can feel free to copy and paste.
You’ll see what I mean.

```csharp
public class ControlStateDemo : WebControl
{
  public int ViewPostCount
  {
    get { return (int)(ViewState["ViewProp"] ?? 0); }
    set { ViewState["ViewProp"] = value; }
  }

  public int ControlPostCount
  {
    get { return controlPostCount; }
    set { controlPostCount = value; }
  }
  
  private int controlPostCount;

  protected override void OnInit(EventArgs e)
  {
    //Let the page know this control needs the control state.
    Page.RegisterRequiresControlState(this);
    base.OnInit(e);
  }

  protected override void OnLoad(EventArgs e)
  {
    ViewPostCount++;
    ControlPostCount++;
    base.OnLoad(e);
  }

  protected override void Render(HtmlTextWriter writer)
  {
    writer.Write("<p>ViewState: " + this.ViewPostCount + "</p>");
    writer.Write("<p>ControlState:" + this.ControlPostCount + "</p>");
    base.Render(writer);
  }
  
  protected override void LoadControlState(object savedState)
  {
    int state = (int)(savedState ?? 0);
    this.controlPostCount = state;
  }

  protected override object SaveControlState()
  {
    return controlPostCount;
  }
}
```

This control has two properties. One backed by the `ViewState` and the
other backed by a private member variable. Notice that we register this
control with the Page in the `OnInit` method.

In the `OnLoad` method, we increment each property. For demonstration
purposes, we need these properties to change on each postback, and this
is as good a method as any.

In the `Render` method, we simply output the values of the two
properties. So far so good, eh?

Now we get to the `LoadControlState` method. This method is called by
ASP.NET early in the control lifecyle (after `OnInit` but before
`LoadViewState`) in order to provide your control with the saved control
state from the previous request.

In this case, we can cast this value to an int and set the control’s
state (the value of controlPostCount) to this value.

The `SaveControlState` method provides ASP.NET the data to store in the
control state as the return value. In this example, we return the value
of `controlPostCount`. This is how we knew we could cast the value to an
`int` in `LoadControlState`.

Now if I drop this control onto a page with a Button control, let’s see
what happens after a few postbacks.

![Image showing the ViewState and Control State counters with values of
4
each](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GainControlOfYourControlState_2D7/UntitledPageWindowsInternetExplorer6.png)

As expected, both values increment, as they are persisted across
postbacks. But what happens if we disable ViewState on the page and
click the button a few more times.

![Image showing the ViewState counter with a value of 1 and the Control
State counters with a value of
8](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GainControlOfYourControlState_2D7/UntitledPageWindowsInternetExplorer7.png)

As you can see, we retain the control state, while the `ViewState` is
disabled.

### But What About Inherited Controls?

I am so glad you asked! In this example, I inherited from `WebControl`,
but what if I inherited from `TreeControl`, or some other control that
made use of the control state. My implementation of `LoadControlState`
and `SaveControlState` pretty much obliterates the control state for the
base class.

The class I wrote here is intentionally simple to show you no real magic
is going on. Let’s demonstrate the proper way to save and load the
control state by creating a class that inherits from this control.

```csharp
public class SubControlStateDemo : ControlStateDemo
{
  public int AnotherCount
  {
    get { return this.anotherCount; }
    set { this.anotherCount = value; }
  }

  private int anotherCount;

  protected override void OnLoad(EventArgs e)
  {
    AnotherCount++;
    base.OnLoad(e);
  }

  protected override void Render(HtmlTextWriter writer)
  {
    base.Render(writer);
    writer.Write("<p>AnotherCount:" + this.AnotherCount + "</p>");
  }

  protected override object SaveControlState()
  {
    //grab the state for the base control.
    object baseState = base.SaveControlState();

    //create an array to hold the base control’s state 
    //and this control’s state.
    object thisState = new object[] {baseState, this.anotherCount};
    return thisState;
  }

  protected override void LoadControlState(object savedState)
  {
    object[] stateLastRequest = (object[]) savedState;
    
    //Grab the state for the base class 
    //and give it to it.
    object baseState = stateLastRequest[0];
    base.LoadControlState(baseState);

    //Now load this control’s state.
    this.anotherCount = (int) stateLastRequest[1];
  }
}
```

In this control, we inherit from the `ControlStateDemo` control I wrote
earlier and added a new property called `AnotherCount`. The main thing
to focus on here is our new implementation of `SaveControlState` and
`LoadControlState`. We now take great pains to make sure that the base
control gets the value it is expecting.

In `SaveControlState`, the first thing we do is grab the control state
from the base control by calling `base.SaveControlState`. As you recall,
this holds the value for the private member `controlPostCount`.

Since we want to add our own private member, `anotherCount` to the
control state, we create an array to store both values and then return
this array to the caller.

Within the `LoadControlState` method, we know we’re going to be passed
in an object array and that the first element of the array is the
control state for our base class. So in that method, we grab the first
element and pass it to the method call `base.LoadControlState`, thus
giving the base class what it expects to receive for its control state.

We then grab the second element, which is our control state, and set
`anotherCount` to this value.

Let’s look at a screenshot of the result in action. Looks like
everything is humming along nicely.

![Screen showing our new property also being saved and restored
properly.](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GainControlOfYourControlState_2D7/UntitledPageWindowsInternetExplorer9.png)

I would recommend using this approach anytime you implement control
state in a custom control because you never know when you might override
the control state for a base class.

