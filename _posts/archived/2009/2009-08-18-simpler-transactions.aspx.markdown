---
title: Simpler Transactions
date: 2009-08-18 -0800
tags: [dotnet,code]
redirect_from: "/archive/2009/08/17/simpler-transactions.aspx/"
---

The .NET Framework provides support for managing transactions from code
via the
[System.Transactions](http://msdn.microsoft.com/en-us/library/system.transactions.aspx "System.Transactions Namespace")
infrastructure. Performing database operations in a transaction is as
easy as writing a using block with the
[TransactionScope](http://msdn.microsoft.com/en-us/library/system.transactions.transactionscope.aspx "TransactionScope")
class.

```csharp
using(TransactionScope transaction = new TransactionScope()) 
{
  DoSomeWork();
  SaveWorkToDatabase();

  transaction.Complete();
}
```

At the end of the using block, `Dispose` is called on the transaction
scope. If the transaction has not been completed (in other words,
`transaction.Complete` was not called), then the transaction is rolled
back. Otherwise it is committed to the underlying data store.

The typical reason a transaction might not be completed is that an
exception is thrown within the using block and thus the `Complete`
method is not called.

This pattern is simple, but I was looking at it the other day with a
co-worker wondering if we could make it even simpler. After all, if the
only reason a transaction fails is because an exception is thrown, why
must the developer remember to complete the transaction? Can’t we do
that for them?

My idea was to write a method that accepts an `Action` which contains
the code you wish to run within the transaction. I’m not sure if people
would consider this simpler, so you tell me. Here’s the usage pattern.

```csharp
public void SomeMethod()
{
  Transaction.Do(() => {
    DoSomeWork();
    SaveWorkToDatabase();
  });
}
```

Yay! I saved one whole line of code! :P

Kidding aside, we don’t save much in code reduction, but I think it
makes the concept slightly simpler. I figured someone has already done
this as it’s really not rocket science, but I didn’t see anything after
a quick search. Here’s the code.

```csharp
public static class Transaction 
{
  public static void Do(Action action) 
  {
    using (TransactionScope transaction = new TransactionScope())
   {
      action();
      transaction.Complete();
    }
  }
}
```

So you tell me, does this seem useful at all?

By the way, there are several overloads to the `TransactionScope`
constructor. I would imagine that if you used this pattern in a real
application, you’d want to provide corresponding overloads to the
`Transaction.Do` method.

UPDATE: What if you don’t want to rely on an exception to determine
whether the transaction is successful?

In general, I tend to think of a failed transaction as an exceptional
situation. I generally assume transactions will succeed and when they
don’t it’s an *exceptional* situation. In other words, I’m usually fine
with an exception being the trigger that a transaction fails.

However, [Omer Van
Kloeten](http://weblogs.asp.net/OKloeten/ "Omer van Kloeten") pointed
out on Twitter that this can be a performance problem in cases where
transaction failures are common and that returning true or false might
make more sense.

It’s trivial to provide an overload that takes in a `Func<bool>`. When
you use this overload, you simply return `true` if the transaction
succeeds or `false` if it doesn’t, which is kind of nice. Here’s an
example of usage.

```csharp
Transaction.Do(() => {

  DoSomeWork();
  if(SaveWorkToDatabaseSuccessful()) {
    return true;
  }
  return false;
});
```

The implementation is pretty similar to what we have above.

```csharp
public static void Do(Func<bool> action) {
  using (TransactionScope transaction = new TransactionScope()) {
    if (action()) {
      transaction.Complete();
    }
  }
}
```

