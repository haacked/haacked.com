## Method Move

Initial commit

```csharp
using System.Net;

public interface ISocket
{
    string GetHostName(IPAddress address);
    void Listen();
    void Connect(IPAddress address);
    int Send(byte[] buffer);
    int Receive(byte[] buffer);
}
```

Developer 2 creates branch `separate-client-server` and renames `ISocket` to `ICllientSocket`. Developer also moves methods `Listen` and `Receive` into a new interface, `IServerSocket`.

```csharp
using System.Net;

public interface IClientSocket
{
    string GetHostName(IPAddress address);
    void Connect(IPAddress address);
    int Send(byte[] buffer);
}

public interface IServerSocket
{
    void Listen();
    int Receive(byte[] buffer);
}
```

Meanwhile, back on the `master` branch. The original developer moves `GetHostName` into a new interface, `IDns`

```csharp
using System.Net;

public interface ISocket
{
    void Listen();
    void Connect(IPAddress address);
    int Send(byte[] buffer);
    int Receive(byte[] buffer);
}

public interface IDns
{
    string GetHostName(IPAddress address);
}
```

Now Developer 1 tries to merge the `separate-client-server` branch into master and runs into conflicts. Boo hoo.

```csharp
using System.Net;

public interface IClientSocket
{
<<<<<<< HEAD
    void Listen();
=======
    string GetHostName(IPAddress address);
>>>>>>> separate-client-server
    void Connect(IPAddress address);
    int Send(byte[] buffer);
}

<<<<<<< HEAD
public interface IDns
{
    string GetHostName(IPAddress address);
=======
public interface IServerSocket
{
    void Listen();
    int Receive(byte[] buffer);
>>>>>>> separate-client-server
}
```

## Divergent Move

We start with

```csharp
public interface IStudent
{
    string Name { get; }
    string Teacher { get; }
    IClass Class { get; }
}

public interface IClass
{
    string Name { get; }
    string Subject { get; }
}

public interface IEnrollment
{
    IStudent Student { get; }
    IClass Class { get; }
}
```

Then in a branch named `move-teacher-to-class` we move the `Teacher` property from `IStudent` to the `IClass` interface. We also add a `Grade` property to `IStudent`. This results in these changes:

```csharp
public interface IStudent
{
    string Name { get; }
    int Grade { get; }
    IClass Class { get; }
}

public interface IClass
{
    string Name { get; }
    string Subject { get; }
    string Teacher { get; }
}
```

Meanwhile, on `master`, a different developer moves the `Teacher` property from `IStudent` to the `IEnrollment` interface.

```csharp
public interface IStudent
{
    string Name { get; }
    IClass Class { get; }
}

public interface IEnrollment
{
    IStudent Student { get; }
    IClass Class { get; }
    string Teacher { get; }
}
```

Now what happens when we merge `move-teacher-to-class` into `master`?

```csharp
public interface IStudent
{
    string Name { get; }
<<<<<<< HEAD
=======
    int Grade { get; }
>>>>>>> move-teacher-to-class
    IClass Class { get; }
}

public interface IClass
{
    string Name { get; }
    string Subject { get; }
    string Teacher { get; }
}

public interface IEnrollment
{
    IStudent Student { get; }
    IClass Class { get; }
    string Teacher { get; }
}
```

Git notices the conflict in the `IStudent` class. One developer removed a property. Another developer added a property. But something else interesting happened here that Git did not notice. We have a divergent move situation here. Both developers moved the `Teacher` property to different branches. Let's see how gmaster handles this.

