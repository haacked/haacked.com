---
layout: post
title: "Using Octokit.net to authenticate your app with GitHub"
date: 2014-04-24 10:44 -0800
comments: true
categories: [octokit github aspnetmvc oauth]
---

Some endpoints in the GitHub API require authorization to access private details. For example, if you want to get all of a user's repositories, you'll need to authenticate to see private repositories.

If you're building a third-party application that integrates with the GitHub API, it's poor form to ask for a user's GitHub credentials. Most users would be wary of providing that information.

Fortunately, Github supports the [OAuth web application flow](https://developer.github.com/v3/oauth/#web-application-flow). This allows your app to authenticate with GitHub without ever having access to a user's GitHub credentials.

In this post, I'll show the basics of implementing this workflow using Octokit.NET.

## OAuth Web Flow

The basic worfklow of an OAuth flow is as follows.

1. On an unauthenticated request to your site, your site redirects the user to the GitHub oauth login URL (hosted on github.com) with some information in the query string such as your application's identity and the list of scopes (permissions) your application requests.
2. The GitHub Oauth login page then prompts the user to either accept or reject this authentication request. If the user is not already logged into GitHub.com, they'll login first. ![](https://cloud.githubusercontent.com/assets/19977/2759594/06319936-c9a4-11e3-8792-bdfd0a0565c2.png)
3. If the user clicks "Authorize application", then this page redirects back to your site with a special session code.
4. Your site will then make a server to server request and exchange that session code and your application's client secret for an OAuth Access Token. You can then use that token with Octokit.net to make other API requests.

## Register your application

Before any of this will work, you'll need to register your application on GitHub.com to get your application's client secret. Never share this secret with anyone else!

While logged in, go to your account settings and click the Applications tab. Then click "Register new application". Or you can just navigate to [https://github.com/settings/applications/new](https://github.com/settings/applications/new).

Here's where you can fill in some details about your application.

![application registration](https://cloud.githubusercontent.com/assets/19977/2760125/62600c38-c9ae-11e3-911f-783d7a34aeaf.png)

After you click "Register application", you'll see your client id and client secret.

![OAuth application registration details](https://cloud.githubusercontent.com/assets/19977/2760128/95587e40-c9ae-11e3-84f2-053d2574f1e8.png)

## Implement the web flow

I've put together a simple raw [ASP.NET MVC demonstration of this workflow](https://github.com/Haacked/octokit-oauth-demo) to illustrate how the workflow works. In a real ASP.NET MVC application, I would probably implement Owin middleware (which has been done before and I link to it later). In an older ASP.NET MVC application I might implement a custom `AuthorizeAttribute`.   

If you want to follow along from scratch, create a new ASP.NET MVC project in Visual Studio and then install the Octokit.net package:

```bash
Install-Package Octokit
```

Here's the code for the `HomeController`. I tried to make it easy to follow along.

```csharp
public class HomeController : Controller
{
    // TODO: Replace the following values with the values from your application registration. Register an
    // application at https://github.com/settings/applications/new to get these values.
    const string clientId = "106002c37f27482617fb";
    private const string clientSecret = "66d5263cadd3bfe056dd46147154ba1eb2fe60b8";
    readonly GitHubClient client =
        new GitHubClient(new ProductHeaderValue("Haack-GitHub-Oauth-Demo"), new Uri("https://github.com/"));

    // This URL uses the GitHub API to get a list of the current user's
    // repositories which include public and private repositories.
    public async Task<ActionResult> Index()
    {
        var accessToken = Session["OAuthToken"] as string;
        if (accessToken != null)
        {
            // This allows the client to make requests to the GitHub API on the user's behalf
            // without ever having the user's OAuth credentials.
            client.Credentials = new Credentials(accessToken);
        }

        try
        {
            // The following requests retrieves all of the user's repositories and
            // requires that the user be logged in to work.
            var repositories = await client.Repository.GetAllForCurrent();
            var model = new IndexViewModel(repositories);

            return View(model);
        }
        catch (AuthorizationException)
        {
            // Either the accessToken is null or it's invalid. This redirects
            // to the GitHub OAuth login page. That page will redirect back to the
            // Authorize action.
            return Redirect(GetOauthLoginUrl());
        }
    }

    // This is the Callback URL that the GitHub OAuth Login page will redirect back to.
    public async Task<ActionResult> Authorize(string code, string state)
    {
        if (!String.IsNullOrEmpty(code))
        {
            var expectedState = Session["CSRF:State"] as string;
            if (state != expectedState) throw new InvalidOperationException("SECURITY FAIL!");
            Session["CSRF:State"] = null;

            var token = await client.Oauth.CreateAccessToken(
                new OauthTokenRequest(clientId, clientSecret, code)
                {
                    RedirectUri = new Uri("http://localhost:58292/home/authorize")
                });
            Session["OAuthToken"] = token.AccessToken;
        }

        return RedirectToAction("Index");
    }

    private string GetOauthLoginUrl()
    {
        string csrf = Membership.GeneratePassword(24, 1);
        Session["CSRF:State"] = csrf;

        // 1. Redirect users to request GitHub access
        var request = new OauthLoginRequest(clientId)
        {
            Scopes = {"user", "notifications"},
            State = csrf
        };
        var oauthLoginUrl = client.Oauth.GetGitHubLoginUrl(request);
        return oauthLoginUrl.ToString();
    }

    public async Task<ActionResult> Emojis()
    {
        var emojis = await client.Miscellaneous.GetEmojis();

        return View(emojis);
    }
}
```

If you visit the `/Home/Emojis` endpoint, you'll see it works fine without authentication since GitHub doesn't require authentication in order to see the emojis.

But visiting `/Home/Index` requires authentication. That redirects to GitHub.com. GitHub.com in turn redirects back to `/Home/authorize` which stores the OAuth access token in the session. In a real application I might store this in an encrypted cookie or the database.

To get this sample working, make sure to replace the `clientId` and `clientSecret` constants with the values you got from registering your own application.

When you visit the home page and authorize the application, you'll see a list of your repositories lovingly rendered by my beautiful web design.

![the beautiful result](https://cloud.githubusercontent.com/assets/19977/2759992/5021c208-c9ab-11e3-9b86-27fb7e95d141.png)

## Next Steps

If you're using ASP.NET MVC 5 or any OWIN based application, there's an [Owin OAuth provider for GitHub](http://blog.beabigrockstar.com/owin-oauth-provider-github/) you can use instead to provide authentication. I haven't played with it so I have no idea how good it is or how you obtain the OAuth Access Token when you use it in order to pass it to Octokit.net.