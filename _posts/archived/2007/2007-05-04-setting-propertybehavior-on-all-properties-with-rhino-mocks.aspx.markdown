---
title: Setting PropertyBehavior On All Properties With Rhino Mocks
date: 2007-05-04 -0800
disqus_identifier: 18302
tags:
- code
- tdd
redirect_from: "/archive/2007/05/03/setting-propertybehavior-on-all-properties-with-rhino-mocks.aspx/"
---

Although I am a big fan of [Rhino
Mocks](http://www.ayende.com/projects/rhino-mocks.aspx "Rhino Mocks Website"),
I typically favor [State-Based over Interaction-Based unit
testing](http://www.benpryor.com/blog/index.php?/archives/28-State-based-vs.-Interaction-based-Unit-Testing.html "State-Based vs Interaction Based unit testing"),
though I am not totally against Interaction Based testing.

I often use Rhino Mocks to dynamically create Dummy objects and Fake
objects rather than true Mocks, based on this [definition given by
Martin
Fowler](http://www.martinfowler.com/articles/mocksArentStubs.html#TheDifferenceBetweenMocksAndStubs "Difference Between Mocks and Stubs").

-   **Dummy** objects are passed around but never actually used. Usually
    they are just used to fill parameter lists.\
-   **Fake** objects actually have working implementations, but usually
    take some shortcut which makes them not suitable for production (an
    in memory database is a good example).\
-   **Stubs** provide canned answers to calls made during the test,
    usually not responding at all to anything outside what’s programmed
    in for the test. Stubs may also record information about calls, such
    as an email gateway stub that remembers the messages it ’sent’, or
    maybe only how many messages it ’sent’.\
-   **Mocks** are what we are talking about here: objects pre-programmed
    with expectations which form a specification of the calls they are
    expected to receive.

Fortunately Rhino Mocks is well suited to this purpose. For example, you
can dynamically add a
[`PropertyBehavior`](http://www.ayende.com/Wiki/(S(erm2hwr3glqunji0xzztohaz))/Rhino+Mocks+Properties.ashx "Property Behavior Docs")
to a mock, which generates a backing member for a property. If that
doesn’t make sense, let’s let the code do the talking.

Here we have a very simple interface. In the real world, imagine there
are a lot of properties.

```csharp
public interface IAnimal
{
  int Legs { get; set; }
}
```

Next, we have a simple class we want to test that interacts with IAnimal
instances. This is a contrived example.

```csharp
public class SomeClass
{
  private IAnimal animal;

  public SomeClass(IAnimal animal)
  {
    this.animal = animal;
  }

  public void SetLegs(int count)
  {
    this.animal.Legs = count;
  }
}
```

Finally, let’s write our unit test.

```csharp
[Test]
public void DemoLegsProperty()
{
  MockRepository mocks = new MockRepository();
  
  //Creates an IAnimal stub    
  IAnimal animalMock = (IAnimal)mocks.DynamicMock(typeof(IAnimal));
  
  //Makes the Legs property actually work, creating a fake.
  SetupResult.For(animalMock.Legs).PropertyBehavior();
  mocks.ReplayAll();
    
  animalMock.Legs = 0;
  Assert.AreEqual(0, animalMock.Legs);
    
  SomeClass instance = new SomeClass(animalMock);
  instance.SetLegs(10);
  Assert.AreEqual(10, animalMock.Legs);
}
```

Keep in mind here that I did not need to stub out a test class that
inherits from IAnimal. Instead, I let RhinoMocks dynamically create one
for me. The bolded line modifies the mock so that the Legs property
exhibits property behavior. Behind the scenes, it’s generating something
like this:

```csharp
public int Legs
{
  get {return this.legs;}
  set {this.legs = value;}
}
int legs;
```

At this point, you might wonder what the point of this is? Why not just
create a test class that implements the IAnimal interface? It isn’t that
many more lines of code.

Now we get to the meat of this post. Suppose the interface was more
realistic and looked like this:

```csharp
public interface IAnimal
{
  int Legs { get; set; }
  int Eyes { get; set; }
  string Name { get; set; }
  string Species { get; set; }
  //... and so on
}
```

Now you have a lot of work to do to implement this interface just for a
unit test. At this point, some readers might be squirming in their seats
ready to jump out and say, “Aha! That’s what ReSharper|CodeSmith|Etc...
can do for you!”

Fair enough. And in fact, the code to add the `PropertyBehavior` to each
property of the IAnimal mock starts to get a bit cumbersome in this
situation too. Let’s look at what that would look like.

```csharp
SetupResult.For(animalMock.Legs).PropertyBehavior();
SetupResult.For(animalMock.Eyes).PropertyBehavior();
SetupResult.For(animalMock.Name).PropertyBehavior();
SetupResult.For(animalMock.Species).PropertyBehavior();
```

Still a lot less code to maintain than implementing each of the
properties of the interface. But not very pretty. So I wrote up a quick
utility method for adding the PropertyBehavior to every property of a
mock.

```csharp
/// <summary>
/// Sets all public read/write properties to have a 
/// property behavior when using Rhino Mocks.
/// </summary>
/// <param name="mock"></param>
public static void SetPropertyBehaviorOnAllProperties(object mock)
{
  PropertyInfo[] properties = mock.GetType().GetProperties();
  foreach (PropertyInfo property in properties)
  {
    if (property.CanRead && property.CanWrite)
    {
      property.GetValue(mock, null);
      LastCall.On(mock).PropertyBehavior();
    }
  }
}
```

Using this method, this approach now has a lot of advantages to
explicitly implementing the interface. Here’s an example of the test now
with a test of another property.

```csharp
[Test]
public void DemoLegsProperty()
{
  MockRepository mocks = new MockRepository();
  
  //Creates an IAnimal stub    
  IAnimal animalMock = (IAnimal)mocks.DynamicMock(typeof(IAnimal));
  UnitTestHelper.SetPropertyBehaviorOnAllProperties(animalMock);
  mocks.ReplayAll();
    
  SomeClass instance = new SomeClass(animalMock);
  instance.SetLegs(10);
  Assert.AreEqual(10, animalMock.Legs);
  animalMock.Eyes = 2;
  Assert.AreEqual(2, animalMock.Eyes);
}
```

Be warned, I didn’t test this with indexed properties. It only applies
to public read/write properties.

Hopefully I can convince
[Ayende](http://www.ayende.com/ "Ayende’s Blog") to include something
like this in a future version of Rhino Mocks.

