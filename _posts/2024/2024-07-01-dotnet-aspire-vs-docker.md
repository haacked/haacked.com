---
title: ".NET Aspire vs Docker."
description: "Comparing setting up a PostgreSql dependency using Docker vs using .NET Aspire?"
tags: [aspire dotnet nuget service-dependencies]
excerpt_image: https://github.com/user-attachments/assets/34aea55b-3493-401e-a90d-36427b61f6b8
---

This is a follow-up to [my previous post](https://haacked.com/archive/2024/06/27/dotnet-aspire/) where I compared .NET Aspire to NuGet. In that post, I promised I would follow up with a comparison of using .NET Aspire to add a service dependency to a project versus using Docker. And looky here, I'm following through for once!

![Docker vs Aspire](https://github.com/user-attachments/assets/34aea55b-3493-401e-a90d-36427b61f6b8)

The goal of these examples is to look at how much "ceremony" there is to add a service dependency to a .NET project using .NET Aspire versus using Docker. Even though it may not be the "best" example, I chose PostgreSql because it's often the first service dependency I add to a new project. The example would be stronger if I chose another service dependency in addition to Postgres, but I think you can extrapolate that as well. And I have another project I'm working on that will have more dependencies.

I won't include installing the pre-requisite tooling as part of the "ceremony" because that's a one-time thing. I'll focus on the steps to add the service dependency to a project.

## Tooling

I wrote this so that you can follow along and create the projects yourself on your own computer. If you want to follow along, you'll need the following tools installed.

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [.NET Aspire tooling](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/setup-tooling?tabs=linux&pivots=dotnet-cli#install-net-aspire)

Once you have these installed, you'll also need to install the Aspire .NET workloads.

```bash
dotnet workload update
dotnet workload install aspire
```

## Examples

This section contains two step-by-step walk throughs to create the example project, once with Docker and once with PostgreSql.

The example project is a simple Blazor web app with a PostgreSQL database. I'll use Entity Framework Core to interact with the database. I'll also use the `dotnet-ef` command line tool to create migrations.

Since we're creating the same project twice, I'll put the common code we'll need right here since both walkthroughs will refer to it.

Both projects will make use of a custom `DbContext` derived class and a simple `User` entity with an `Id` and a `Name`.

```csharp
using Microsoft.EntityFrameworkCore;

namespace HaackDemo.Web;

public class DemoDbContext(DbContextOptions<DemoDbContext> options)
  : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }
}
```

Also, both projects will have a couple of background services that run on startup:

- [`DemoDbInitializer`](https://github.com/haacked/aspire-efcore-postgres-demo/blob/main/AspireDemo.Web/Startup/DemoDbInitializer.cs) - runs migrations and seeds the database on startup.

- [`DemoDbInitializerHealthCheck`](https://github.com/haacked/aspire-efcore-postgres-demo/blob/main/AspireDemo.Web/Startup/DemoDbInitializerHealthCheck.cs) - sets up a health check to report on the status of the database initializer.

I used to run my migrations in `Program.cs` on startup, but I saw [this example in the Aspire samples](https://github.com/haacked/aspire-efcore-postgres-demo/blob/main/AspireDemo.Web/Startup/DemoDbInitializer.cs) and thought I'd try it out. I also copied their [health check initializer](https://github.com/haacked/aspire-efcore-postgres-demo/blob/main/AspireDemo.Web/Startup/DemoDbInitializerHealthCheck.cs).

Both of these need to be registered in `Program.cs`.

```csharp
builder.Services.AddSingleton<DemoDbInitializer>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<DemoDbInitializer>());
builder.Services.AddHealthChecks()
    .AddCheck<DemoDbInitializerHealthCheck>("DbInitializer", null);
```

With that in place, let's begin.

### Docker

From your root development directory, the following commands will create a new Blazor project and solution.

```bash
md docker-efcore-postgres-demo && cd docker-efcore-postgres-demo`
dotnet new blazor -n DockerDemo -o DockerDemo.Web`
dotnet new sln --name DockerDemo`
dotnet sln add DockerDemo.Web`
```

Npgqsl is the PostgreSql provider for Entity Framework Core. We need to add it to the web project.

```bash
cd DockerDemo.Web
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

We also need the EF Core Design package to support the `dotnet-ef` command line tool we're going to use to create migrations.

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Now we add the docker file to the root directory.

```bash
cd ..
touch docker-compose.yml
```

And paste the following in

```yaml
version: '3.7'

services:
  postgres:
    container_name: 'postgres'
    image: postgres
    environment:
      # change this for a "real" app!
      POSTGRES_PASSWORD: password
```

Note that the `container_name` could conflict with other containers on your system. You may need to change it to something unique.

Add the postgres connection string to your appsettings.json.

```json
    "ConnectionStrings": {
      "postgresdb": "User ID=postgres;Password=password;Server=postgres;Port=5432;Database=POSTGRES_USER;Integrated Security=true;Pooling=true;"
    }
```

Now we can add our custom `DbContext` derived class and `User` entity mentioned earlier. We also need to register `DemoDbInitializer` and `DemoDbInitializerHealthCheck` in `Program.cs` as mentioned before.

Next create the initial migration.

```bash
cd ../DockerDemo.Web
dotnet ef migrations add InitialMigration
```

We're ready to run the app. First, we need to start the Postgres container.

```bash
docker-compose build
docker-compose up
```

Finally, we can hit hit F5 in Visual Studio/Rider or run `dotnet run` in the terminal and run our app locally.

### Aspire

Once again, from your root development directory, the following commands will create a new Blazor project and solution. But this time, we'll use the Aspire starter template.

```bash
md aspire-efcore-postgres-demo && cd aspire-efcore-postgres-demo
dotnet new aspire-starter -n AspireDemo -o .
```

This creates three projects:

- AspireDemo.AppHost - The host project that configures the application.
- AspireDemo.Web - The web application project.
- AspireDemo.ApiService - An example web service to get the weather.

We don't need `AspireDemo.ApiService` for this example, so we can remove it.

The first thing we want to do is configure the PostgreSql service in the `AspireDemo.AppHost` project. In a way, this is analogous to how we configured Postgres in the `docker-compose.yml` file in the Docker example.

Switch to the App Host project and install the `Aspire.Hosting.PostgreSQL` package.

```bash
cd ../AspireDemo.AppHost
dotnet add package Aspire.Hosting.PostgreSQL
```

Add this snippet after the `builder` is created in `Program.cs`.

```csharp
var postgres = builder.AddPostgres("postgres");
var postgresdb = postgres.AddDatabase("postgresdb");
```

This creates a Postgres service named `postgres` and a database named `postgresdb`. We'll use the `postgresdb` reference when we want to connect to the database in the consuming project.

Finally, we update the existing line to include the reference to the database.

```diff
  builder.AddProject<Projects.AspireDemo_Web>("webfrontend")
-     .WithExternalHttpEndpoints();
+     .WithExternalHttpEndpoints()
+     .WithReference(postgresdb);
```

That completes the configuration of the PostgreSql service in the App Host project. Now we can consume this from our web project.

Add the PostgreSQL component to the consuming project, aka the web application.

```bash
cd ../AspireDemo.Web
dotnet add package Aspire.Npgsql.EntityFrameworkCore.PostgreSQL
```

We also need the EF Core Design package to support the ef command line tool we're going to use to create migrations.

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Once again, we add our custom `DbContext` derived class, `DemoDbContext`, along with the `User` entity to the project. Once we do that, we configure the `DemoDbContext` in the `Program.cs` file. Note that we use the `postgresdb` reference we created in the App Host project.

```csharp
builder.AddNpgsqlDbContext<DemoDbContext>("postgresdb");
```

Then we can create the migrations using the `dotnet-ef` cli tool.

```bash
cd ../AspireDemo.Web
dotnet ef migrations add InitialMigration
```

Don't forget to add the `DemoDbInitializer` and `DemoDbInitializerHealthCheck` to the project and register them in `Program.cs` as mentioned before.

Now to run the app, I can hit `F5` in Visual Studio/Rider or run `dotnet run` in the terminal. If you use `F5`, make sure the AppHost project is selected as the run project.`

## Conclusions

At the end of both walkthroughs we end up with a simple Blazor web app that uses a PostgreSQL database. Personally, I like the .NET Aspire approach because I didn't have to mess with connection strings and the `F5` to run experience is preserved.

As I mentioned before, I have another project I'm working on that has more dependencies. When I'm done with that port, I think it'll be a better example of the ceremony surrounding cloud dependencies when using .NET Aspire.

In any case, you can see both of these projects I created on GitHub.

* [haacked/docker-efcore-postgres-demo](https://github.com/haacked/docker-efcore-postgres-demo)
* [haacked/aspire-efcore-postgres-demo](https://github.com/haacked/aspire-efcore-postgres-demo)
