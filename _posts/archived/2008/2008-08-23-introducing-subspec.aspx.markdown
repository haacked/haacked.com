---
title: Streamlined BDD Using SubSpec for xUnit.NET
tags: [tdd]
redirect_from:
  - "/archive/2008/08/22/introducing-subspec.aspx/"
  - "/archive/2008/08/24/introducing-subspec.aspx/"
---

I admit, up until now I’ve largely ignored the
[BDD](http://dannorth.net/introducing-bdd "Introducing BDD") (Behavior
Driven Development) Context/Specification style of writing unit tests.
It’s been touted as a more approachable way to learn TDD (Test Driven
Development) and as a more natural transition from user stories to the
actual code design. I guess my hesitation to give it a second thought
was that I felt I didn’t need a more approachable form of TDD.

Recently, my Subtext partner in crime, [Steve
Harman](http://stevenharman.net/ "StevenHarman"), urged me to take
another fresh look at BDD Context/Specification style tests. I trust
Steve’s opinion, so I took another look and in doing so, the benefits of
this approach dawned on me. I realized that it wasn’t BDD itself I
didn’t like, after all, I did enjoy [writing specs using minispec and
IronRuby](https://haacked.com/archive/2008/04/09/my-first-ironruby-unit-test-spec-for-asp.net-mvc.aspx "Minispec and IronRuby").
I realized that the part I didn’t really like was the .NET
implementations of this style. Keep in mind that I do not claim to be an
expert in TDD or BDD, I’m just a student and these are just my
observations. I’m sure others will chime in and provide corrections that
we can all learn from.

### SpecUnit.NET example

For example, let’s take a look at one example pulled from the sample
project of [Scott
Bellware’s](http://blog.scottbellware.com/ "Scott Bellware's Blog")
[SpecUnit.NET
project](http://code.google.com/p/specunit-net/ "SpecUnit.NET"), which
provides extensions supporting the BDD-style use with .NET unit testing
frameworks and has really pushed this space forward. I trimmed the name
of the class slightly by removing a couple articles (“the” and “an”) so
it would fit within the format of my blog post.

```csharp
[Concern("Funds transfer")]
public class when_transfering_amount_greater_than_balance_of_the_from_account
  : behaves_like_context_with_from_account_and_to_account
{
  private Exception _exception;

  protected override void Because()
  {
    _exception = ((MethodThatThrows) delegate
    {
      _fromAccount.Transfer(2m, _toAccount);
    })
    .GetException();
  }

  [Observation]
  public void should_not_allow_the_transfer()
  {
    _exception.ShouldNotBeNull();
  }

  [Observation]
  public void should_raise_System_Exception()
  {
    _exception.ShouldBeOfType(typeof(Exception));
  }
}
```

The `Because` method contains the code with the behavior we’re
interested in testing. The two methods annotated with `Observation` are
the specifications. Notice that the names of the classes and methods are
meant to be human readable. The output of running these tests remove
underscores and reads like a specification document. It’s all very cool.

What I like about this approach is there’s a crisp focus on having each
test class focused on a single behavior, in this case transferring a
balance from one account to another. In the past, I might have written
something like this as two test methods (which led to duplicating code
or putting code in some generic `Setup` method that seems detached from
what I’m trying to test) or as one method with two asserts. This
approach helps you think about how to organize tests along the lines of
your objects’ responsibilities.

What I don’t like about it, and I admit this is really just a nitpick,
is that it looks like someone’s keyboard puked underscores all over the
place. I feel like having to encapsulate each observation as a method
adds a lot of syntactic overhead when I’m trying to read this class from
top to bottom. Maybe that’s just something you get used to.

### MSpec example

Switching gears, let’s look at a different example by [Aaron
Jensen](http://codebetter.com/blogs/aaron.jensen/ "Aaron Jensen"). This
is an experiment in which he tried a very different approach. Look at
this code sample…

```csharp
[Description]   
public class Transferring_between_from_account_and_to_account   
{   
  static Account fromAccount;   
  static Account toAccount;   
  
  Context before_each =()=>   
  {   
    fromAccount = new Account {Balance = 1m};   
    toAccount = new Account {Balance = 1m};   
  };   
     
  When the_transfer_is_made =()=>   
  {   
    fromAccount.Transfer(1m, toAccount);   
  };   
      
  It should_debit_the_from_account_by_the_amount_transferred =()=>   
  {   
    fromAccount.Balance.ShouldEqual(0m);   
  };   
  
  It should_credit_the_to_account_by_the_amount_transferred =()=>   
  {   
    toAccount.Balance.ShouldEqual(2m);   
  };   
}  
```

There’s still the underscore porn, but it does read a little more like
prose from top to bottom, if you can get yourself to ignore that funky
operator right there. `=()=>` Whoa!

When I complained to Steve about all the underscores in these various
approaches, he suggested that being a fan of the more Zen-like Ruby
language, the underscores didn’t bother him. I didn’t buy that as part
of the aesthetic of Ruby is its clean DRY minimalism. Yes, it uses
underscores, but it doesn’t generally abuse them. Let’s take a look at a
BDD example using RSpec and Ruby. This is an example of a [spec in
progress from Luke
Redpath](http://www.lukeredpath.co.uk/2006/8/29/developing-a-rails-model-using-bdd-and-rspec-part-1 "Developing a Rails model using BDD and RSpec")…
(forgive the poor syntax highlighting. I need a ruby css stylesheet. :)

```csharp
context "A user (in general)" do 
  setup do 
    @user = User.new 
  end 

  specify "should be invalid without a username" do 
    @user.should_not_be_valid 
    @user.errors.on(:username).should_equal "is required" 
    @user.username = 'someusername' 
    @user.should_be_valid 
  end 

  specify "should be invalid without an email" do 
    @user.should_not_be_valid 
    @user.errors.on(:email).should_equal "is required" 
    @user.email = 'joe@bloggs.com' 
    @user.should_be_valid 
  end 
end
```

One thing to notice is that we’re not using separate methods and classes
here. Ruby doesn’t force you to put code in classes. You can just
execute a script top-to-bottom. In this case, the code sets up a context
block and within that block there is a setup block and a couple of
specify blocks. There’s no need to factor a specification into multiple
classes and methods.

Also notice that the context and specifications are described using
strings! Now we’re getting somewhere. If it’s meant to be human
readable, why don’t we use strings instead of the underscore porn? On
Twitter, many accused the ceremony and vagaries of C# of preventing
this approach. While I agree that Ruby has less ceremony than C#, I
also think C# doesn’t get its fair shake sometimes. We can certainly
take a C# approach down to its bare metal with the least syntactic
noise as possible, right?

### SubSpec

So in true Program Manager at Microsoft fashion, I spec’d out a rough
idea of the syntax I would like to use with BDD. I then showed it to
[Brad Wilson](http://bradwilson.typepad.com/ "Brad Wilson's blog")
asking him how can I make this work in xUnit.net. In true Developer
fashion, he ran with it and made it actually work. This blog post is the
part where I try to take all the credit. That’s what PMs do at
Microsoft, write specs, take credit for the hard work the developers do
in bringing the specs to life. ;) (*I kid, I kid)*

Here’s an example using this syntax…

```csharp
[Specification]
public void PushNullSpecifications()
{
  Stack<string> stack = null;

  "Given a new stack".Context(() => stack = new Stack<string>());

  "with null pushed into it".Do(() => stack.Push(null));

  "the stack is not empty".Assert(() => Assert.False(stack.IsEmpty));
  "the popped value is null".Assert(() => Assert.Null(stack.Pop()));
  "Top returns null".Assert(() => Assert.Null(stack.Top));
}
```

A few things to notice. First, the entire spec is in a single method,
which reduces some of the syntactic noise of splitting the spec into
multiple methods. Secondly, we’re using strings here to describe the
specification and context, rather than method names with underscores for
the human readable part.

**Lastly, *and most importantly*, while it may look like we’re
committing the sin of multiple asserts in a single test, this is not the
case.** Via the power of the xUnit.NET extensibility model, Brad was
able to generate a test per assertion. That’s why the `Assert` method
(should it be `Observe` or `Fact`?) takes in a lambda. We can return
these closures to xUnit.net and it will wrap each one in a separate
test. Here’s another look at the same method with some comments to
highlight how similar this is to the previous examples. (***UPDATE**: I
also changed the asserts to use the Should style extension methods to
demonstrate what it could look like).*

```csharp
[Specification]
public void PushNullSpecifications()
{
  Stack<string> stack = null;
  //equivalent to before-each
  "Given a new stack".Context(() => stack = new Stack<string>());

  //equivalent to Because()
  "with null pushed into it".Do(() => stack.Push(null));

  //Equivalent to [Observation]
  "the stack is not empty".Assert(() => stack.IsEmpty.ShouldBeFalse());
  //Equivalent to [Observation]
  "the popped value is null".Assert(() => stack.Pop().ShouldBeNull());
  //Equivalent to [Observation]
  "Top returns null".Assert(() => stack.Top.ShouldBeNull());
}
```

Keep in mind, at this point, this is a merely *proof-of-concept sample*
that will be included with the samples project in the next version of
[xUnit.NET](http://www.codeplex.com/xunit "xUnit.NET on CodePlex") and
by the time you read this sentence, it may have changed already. You can
download [this particular changeset
here](http://www.codeplex.com/xunit/SourceControl/DownloadSourceCode.aspx?changeSetId=22555 "xUnit.NET ChangeSet 22555").

The following is a screenshot of the HTML report generated by xUnit.NET
when using this syntax that Brad sent me
today.![subspec-report](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/SubSpecBDDExtensionsforxUnit.net_980A/subspec-report_3.png "subspec-report")

Despite it being a sample, I tried to give it a catchy name in case this
is intriguing to others and worth iterating on to make it better (not to
mention that I love putting the prefix “Sub” in
[front](http://subtextproject.com/ "Subtext") of
[everything](http://subkismet.com/ "Subkismet").)

Possible next steps would be to add all the `Woulda`, `Coulda`,
`Shoulda` extension methods so popular with this style of testing. For
example, that would allow you to replace `Assert.False(stack.IsEmpty)`
with `stack.IsEmpty.ShouldBeFalse()`. For those of you practicing BDD,
I’d be interested in hearing your thoughts, objections, etc… concerning
this approach.

For completeness sake, here’s another syntax I proposed to Brad. He
mentioned it was similar to something else he’s seen which he might port
over to xUnit.net.

```csharp
[Specification]
public void When_Transferring_To_An_Account()
{
  Election e = null;
  Account a = null;
  Account b = null;
 
  Where("both accounts have positive balances", () => {
    a = new Account { Balance = 1 };
    b = new Account { Balance = 2 };
  });
 
  When("transfer is made", () =>
    
    a.Transfer(1, b)
  );
 
  It("debits account by amount transferred", () => a.Balance.ShouldBe(0));
  It("credits account by amount transferred", () => b.Balance.ShouldBe(3));
}
```

For those of you completely new to BDD, check out [Scott Bellware’s Code
Magazine article on the
subject](http://www.code-magazine.com/Article.aspx?quickid=0805061 "Behavior-Driven Development").

