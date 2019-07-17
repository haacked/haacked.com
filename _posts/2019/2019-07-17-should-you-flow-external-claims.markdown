---
title: "Should You Flow External Claims On Every Login?"
description: "In a previous post I showed how to flow external claims on every login. In this post I examine whether or not that makes any sense to do at all."
tags: [aspnet,security]
excerpt_image: https://user-images.githubusercontent.com/19977/61344565-8966da00-a806-11e9-9e6c-954a42102b36.png
---

In my last post, I showed how to flow claims from an external identity provider to your application. My post walks through how to bring over the claims every time the user logs in. But why would I want to do this?

On Twitter, [Brock Allen](https://brockallen.com/) replied to my post [with this tweet](https://twitter.com/BrockLAllen/status/1151270781181136896),

> IMO, external claims (other than sub) are only useful to pre-populate the registration page in your app the first time the user ever shows up from the external IdP. Otherwise, and forevermore, you ignore those claims from the external IdP.

In case you're not up on the TLAs (Three Letter Acronyms), "IMO" means "In my opinion" and "IdP" refers to Identity Providers. As one of the maintainers of [IdentityServer](https://github.com/IdentityServer/IdentityServer4) (among many other identity related open source projects), Brock knows his stuff.

I picture Brock in the role of Dr. Ian Malcom in Jurassic Park.

![Your scientists were so preoccupied with whether they could, they didn't stop to think if they should.](https://user-images.githubusercontent.com/19977/61344565-8966da00-a806-11e9-9e6c-954a42102b36.png)

After discussing it on Twitter, I came to the conclusion that he's correct for 99.9% of use cases. So the behavior described in the documentation, [Persist additional claims and tokens from external providers in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/additional-claims?view=aspnetcore-2.2) is preferable to my approach in nearly all cases.

## Why Go Against The Grain?

So why am I going against the grain? Ultimately, what you do here is a product decision. And for most products, the default behavior is more correct than what I'm doing. But there are some situations where what I'm doing might make sense.

My post was a bit misleading because it mentioned social login providers. In general, these are untrusted providers. Anyone can sign up for a gmail account.

But allowing the general public to use these providers is not what I'm doing. Each of these social providers can be used for companies. For example, Facebook has Workplace, Google has G-Suite, and GitHub has orgs and a decent API.

In my case, I'm building an app for companies to use. When set up, the app lets employees of a company authenticate to the app using their trusted identity provider (right now I only support G-Suite). It's an application that's meant to be internal to a company.

For the sake of a concrete example, imagine your company has a set of internal apps such as an expense reporting app, a customer support app, an HR app, and so on. All these apps implement single sign-on with your company's G-Suite login provider. You might not want employees to have different emails, names, and profile images in all these other apps.

Why not?

It keeps things simpler to manage. Suppose an employee legally changes their name. It'd be nice if they could change it in one place and have it flow out to all the other apps. Or perhaps your company has pranksters who change their image and name to impersonate the CEO. It'd be nice to manage such changes in a single place.

And if I turn out to be wrong, it'll be simple to change course and allow people to change their own information in the app. Until then, my app just tells people to go change their information in their company Google profile. I feel it's a valid minimum viable approach for now.

If your app is using these social login providers to make it easier for the general public to log in, this approach is probably not for you.
