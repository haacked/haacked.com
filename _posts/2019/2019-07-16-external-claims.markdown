---
title: "Flow External Claims to the Main Identity"
description: "How to flow or persist additional external claims from an external login/authentication provider such as Google."
tags: [aspnet,security]
excerpt_image: https://user-images.githubusercontent.com/19977/61318768-a970ac00-a7ba-11e9-9041-83ce8e081809.png
---

I love it when a website lets me use my Google, GitHub, or Facebook account to log in. Chances are, I'm already logged into those sites, so it's one click to log into a new site. This is a great experience for users. It reduces the friction to registration and loggin in to your site. They're less likely to clam up.

It's easy to add external authentication to ASP.NET Core applications. For example, if you want users to log in with their Google or Facebook credentials, [follow these instructions](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/?view=aspnetcore-2.2&tabs=visual-studio).

## Claims, Not Clams

![Clam on the beach](https://user-images.githubusercontent.com/19977/61318768-a970ac00-a7ba-11e9-9041-83ce8e081809.png)

So what happens when a user logs into your application with an external login provider? First, your application receives a set of claims from the login provider. A claim is a name value pair. It contains information about who the authenticated user is. It says nothing about what they can do. For example, a claim might include their given and surname. Or it might include a profile picture.

Often, you want these claims to flow into the local application identity. When you authenticate with a provider like Google, the provider redirects to a callback URL passing along these claims. In response, the asp.net core application will create a local identity. If there's no existing user account associated with the external login, the app prompts the user to create one.

You can see all this logic in the pages for ASP.NET Identity. For example, you can [scaffold Identity into your project](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-2.2&tabs=visual-studio) and see all the Razor Pages.

The specific logic to handle the callback occurs in `{YourProjectFolder}/Areas/Identity/Pages/Account/ExternalLogin.cshtml.cs`. You can see the source code for the [latest version on GitHub here](https://github.com/aspnet/AspNetCore/blob/master/src/Identity/UI/src/Areas/Identity/Pages/V4/Account/ExternalLogin.cshtml.cs). Note that version is in active development so it might not match up with what you have on your machine.

## Persisting Claims

Microsoft has a document, [Persist additional claims and tokens from external providers in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/additional-claims?view=aspnetcore-2.2) that provides details on how to persist those claims.

These steps require editing the `OnPostConfirmationAsync` method of `ExternalLogin.cshtml.cs`. When a user authenticates with an external provider for the first time, they won't have a local account. The `ExternalLogin` page displays the email address received from the provider and prompts the user to confirm their account information by submitting the form. When they submit the form, `OnPostConfirmationAsync` handles that post request.

## Problems With This Approach

There's a few issues with this approach for my needs. The first is that this only persists these claims when the user creates a local account. Subsequent logins won't update the claim with this approach. So if the user updates their profile pic on Google, your site won't receive that change by default.

The second issue is this updates a scaffolded page. Not terrible, but I try to keep updates to scaffolded pages to a minimum. That way, there's not too much to change when the next version of ASP.NET Core comes out and I want to use the updated Identity pages.

Another issue is this, what if I don't even want to persist these claims. There may be some claims I always want to pull from the provider each time they log in. I don't have to worry about the logic of storing them. __How do I flow these external claims into the local claims without persisting them?__

## But First, Should I Do This?

What I'm trying to do here is unusual and probably does NOT meet the needs of your app. I wrote a [follow-up post covering why I'm doing this](https://haacked.com/archive/2019/07/17/should-you-flow-external-claims/). Most apps should not.

## I Don't Claim to Have All the Answers

Turns out, this is not so simple. In researching this, I started to get a bit clammy. Perhaps it doesn't make sense to do it at all. There's an [existing issue that explains why](https://github.com/aspnet/Identity/issues/628).

To summarize, in order to authenticate a user to the local app, the app calls [`SignInManager.ExternalLoginSignInAsync`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.signinmanager-1.externalloginsigninasync?view=aspnetcore-2.2). This validates the external auth cookie and signs the user in to the local app. At the same time, it [clears the external auth cookie](https://github.com/aspnet/AspNetCore/blob/87a92e52c8b4bb7cb75ff78d53d641b1d34f8775/src/Identity/Core/src/SignInManager.cs#L483). Thus you no longer have access to the external claims.

You could save the claims prior to this method call by calling [`UserManager.AddClaimAsync`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.usermanager-1.addclaimasync?view=aspnetcore-2.2). In fact, this is what [Microsoft's own sample does](https://github.com/aspnet/AuthSamples/commit/404105bd191f2e973d4befb668f1310d2fd82701).

But keep in mind that will persist the claim to the database. Also, if you plan to update the claim with the latest value each time the user logs in, you have to rememeber to remove the existing claim and add the new one. Otherwise you get a database full of the same claims. That's a lot of cllaims!

And finally, you have to do this in two places in `ExternalLoginSignInAsync`, once in `OnGetCallbackAsync` and once in `OnPostConfirmationAsync`.

A Microsoft developer (who I used to work with and is a fine poker player) offers [another promising solution here](https://github.com/aspnet/AuthSamples/issues/6#issuecomment-356149753).

> So plug in your own IUserClaimsPrincipalFactory which is called during ExternalLoginSignInAsync, and have it look at the external cookie to add the claims.

That didn't work for me. I was unable to access the external claims from within a custom `IUserClaimsPrincipalFactory` implementation. To access externall claims, you call [`SignInManager.GetExternalLoginInfoAsync`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.signinmanager-1.getexternallogininfoasync?view=aspnetcore-2.2). But I don't have access to a `SignInManager` from within an `IUserClaimsPrincipalFactory` implementation. I can't inject one either because `SignInManager` depends on `IUserClaimsPrincipalFactory`. Injecting one would lead to a circular dependencies and probably cause the colllapse of the universe.

## My haacky claim to a solution

So the question remains, how do you add the claim to your local identity without persisting it via `AddClaimAsync`? I came up with a solution, but it's not as clean as I'd like because I had to copy a bit of code from ASP.NET Core. Thank goodness it's [open source under a permissive license](https://github.com/aspnet/AspNetCore/blob/master/LICENSE.txt)!

What I did was write a custom implementation of [`SignInManager<TUser>`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.signinmanager-1?view=aspnetcore-2.2).

I overrode the `SignInOrTwoFactorAsync` method so that it would copy any external claims over to the local identity. This method does a few other things I didn't want to lose, So I copied the internal implementation and just changed the bit I needed.

I then registered my custom implementation in `Startup.cs` after the `services.AddIdentity` call like so:

```csharp
services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// This replaces the default `SignInManager` with my custom one.
services.AddScoped<SignInManager<ApplicationUser>, ExternalClaimsSignInManager<ApplicationUser>>();
```

Just in case it's not obvious, `ApplicationUser` is a custom user class that inherits from `IdentityUser`. If you didn't override the class, you'd use `IdentityUser`. If you did, replace `ApplicationUser` with whatever you named your user class. The same goes for `ApplicationDbContext` which inherits from `IdentityDbContext<ApplicationUser>`.

Here's the full source for `ExternalClaimsSignInManager<TUser>`. I've tried to format it to fit the constraints of my blog.

```csharp
// Some of this code is copied and modified from
// https://github.com/aspnet/AspNetCore/blob/master/src/Identity/Core/src/SignInManager.cs
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See the license for that code at
// https://github.com/aspnet/AspNetCore/blob/master/LICENSE.txt
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Haack.Identity.Infrastructure
{
    public class ExternalClaimsSignInManager<TUser>
        : SignInManager<TUser> where TUser : class
    {
        public ExternalClaimsSignInManager(
            UserManager<TUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<TUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<TUser>> logger,
            IAuthenticationSchemeProvider schemes)
            : base(
                userManager,
                contextAccessor,
                claimsFactory,
                optionsAccessor,
                logger,
                schemes)
        {
        }

        protected override async Task<SignInResult> SignInOrTwoFactorAsync(
            TUser user,
            bool isPersistent,
            string loginProvider = null,
            bool bypassTwoFactor = false)
        {
            if (!bypassTwoFactor && await IsTfaEnabled(user))
            {
                if (!await IsTwoFactorClientRememberedAsync(user))
                {
                    // Store the userId for use after two factor check
                    var userId = await UserManager.GetUserIdAsync(user);
                    await Context.SignInAsync(IdentityConstants.TwoFactorUserIdScheme, StoreTwoFactorInfo(userId, loginProvider));
                    return SignInResult.TwoFactorRequired;
                }
            }

            // Grab external login info before we clean up the external cookie.
            // This contains the external tokens and claims.
            var externalLoginInfo = await GetExternalLoginInfoAsync();

            // Cleanup external cookie
            if (loginProvider != null)
            {
                await Context.SignOutAsync(IdentityConstants.ExternalScheme);
            }
            await SignInAsyncWithExternalClaims(
                user,
                isPersistent,
                loginProvider,
                externalLoginInfo);
            return SignInResult.Success;
        }

        async Task SignInAsyncWithExternalClaims(
            TUser user,
            bool isPersistent,
            string loginProvider,
            ExternalLoginInfo externalLoginInfo)
        {
            var authenticationProperties = new AuthenticationProperties
            {
                 IsPersistent = isPersistent
            };
            var userPrincipal = await CreateUserPrincipalAsync(user);
            if (loginProvider != null)
            {
                userPrincipal
                    .Identities
                    .First()
                    .AddClaim(new Claim(ClaimTypes.AuthenticationMethod, loginProvider));

                // Add External Claimns that start with "urn:"
                var claims = externalLoginInfo
                    .Principal
                    .Claims
                    .Where(c => c.Type.StartsWith("urn:"));
                userPrincipal.Identities.First().AddClaims(claims);
            }
            await Context.SignInAsync(IdentityConstants.ApplicationScheme,
                userPrincipal,
                authenticationProperties);
        }

        private async Task<bool> IsTfaEnabled(TUser user)
            => UserManager.SupportsUserTwoFactor &&
            await UserManager.GetTwoFactorEnabledAsync(user) &&
            (await UserManager.GetValidTwoFactorProvidersAsync(user)).Count > 0;


        /// <summary>
        /// Creates a claims principal for the specified 2fa information.
        /// </summary>
        /// <param name="userId">The user whose is logging in via 2fa.</param>
        /// <param name="loginProvider">The 2fa provider.</param>
        /// <returns>A <see cref="ClaimsPrincipal"/> containing the user 2fa information.</returns>
        internal ClaimsPrincipal StoreTwoFactorInfo(string userId, string loginProvider)
        {
            var identity = new ClaimsIdentity(IdentityConstants.TwoFactorUserIdScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, userId));
            if (loginProvider != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.AuthenticationMethod, loginProvider));
            }
            return new ClaimsPrincipal(identity);
        }
    }
}
```

What's nice about this approach is I don't have to change `ExternalLogin.cshtml.cs`.

## Summary

So, is this a good idea? If the information you want from the external provider doesn't need to be persisted, then it could be useful.

In my case, I realized I needed others in my site to see this info. For example, a profile picture only visible to the user and not others isn't really that useful. I ended up going with a different approach after figuring all this out. But maybe this will be useful to you. At the very least, it helps to understand the inner workings of ASP.NET authentication and the deep extensibility it supports.
