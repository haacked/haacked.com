---
title: "HTTPS with LetsEncrypt for Azure Functions"
description: "Setting up HTTPS with a proper certificate for Azure Functions should be straightforward and easy, but it's not. In this post I walk through one aspect of it that tripped me up."
tags: [azure,security]
---

_UPDATE: there might be an easier way now. [App Service Managed Certificates now supports apex domains](https://azure.microsoft.com/en-us/updates/public-preview-app-service-managed-certificates-now-supports-apex-domains/). I'll give it a try and report back._

My friends, in an ideal world, it would be dead simple to set up a certificate for an Azure App Service. For example, GitHub Pages gets this right.

![Screen shot showing a checkbox for enforcing HTTPS](https://user-images.githubusercontent.com/19977/114780771-b3776400-9d2c-11eb-91e0-6cd175428496.png "Could it be any easier?")

Look at that. A thing of beauty. Just click that checkbox and now your site is being served from HTTPS using a free certificate from LetsEncrypt. From an _apex domain_ no less!

But to set up a custom apex domain with HTTPS for an Azure App Service is not so easy. It takes about a hundred steps. That's not that much of an exaggeration.

For my site, I use the wonderful [ohadschn/letsencrypt-webapp-renewer](https://github.com/ohadschn/letsencrypt-webapp-renewer) tool. Like I said, it takes some time to set up manually, but once you do, it works great.

This post is not going to walk through that part. For that, I followed [this excellent guide by Dixin](https://weblogs.asp.net/dixin/end-to-end-setup-free-ssl-certificate-to-secure-azure-web-app-with-https). But be aware, the only constant is change and the Azure Portal embraces that credo. It's changed a lot since this guide was written, so the screenshots may not match exactly what you need to do today, but you should be able to figure it out. Even if you follow the guide, it may be worth reading the README in the original repository.

## Azure Functions

Now suppose you want to serve an Azure Function using HTTPS and a LetsEncrypt certificate. To clarify, I'm not talking about using an Azure Function to run `letsencrypt-webapp-renewer` on a schedule. In fact, if you search Azure Functions and `letsencrypt-webapp-renewer`, almost all the results are about that. No, I'm talking about being able to access your function via `https://your-custom-domain/api/your-function`.

Since an Azure Function is an App Service under the hood, wouldn't the instructions I mentioned earlier just work?

You wish. No, Azure Functions are _SPECIAL!_

See, the problem is that LetsEncrypt needs to be able to verify that the domain is under your control. So it's going to make a `GET` request to `http://your-custom-domain/.well-known/acme-challenge/{some-code}` and expect a certain response. By default, all requests to Azure Functions have the `/api` prefix. So we need to do a little magic to get this to work. We need to create a proxy!

Fortunately, there's [a guide for that](https://github.com/sjkp/letsencrypt-siteextension/wiki/Azure-Functions-Support). Unfortunately, it's a bit outdated. Also, it doesn't work if your function is controlled via source control. For example, my functions are in GitHub so the Azure Portal won't let me create a proxy in the Azure Portal.

And here is where I come to save the day. This is what I did to fix the situation.

First, add a `proxies.json` file to your function directory. This is in the same place where your `hosts.json` file is located. In your project file, make sure this file is copied to the output directory. I forgot the first time I added this file and it filled me with regret as nothing worked.

```xml
<None Update="proxies.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
</None>
```

Your `proxies.json` file should look something like this:

```json
{
  "$schema": "http://json.schemastore.org/proxies",
  "proxies": {
    "letsencrypt-proxy": {
      "matchCondition": {
        "route": "/.well-known/acme-challenge/{*rest}"
      },
      "backendUri": "https://%WEBSITE_HOSTNAME%/api/letsencrypt/{rest}"
    }
  }
}
```

This proxy will route requests to `https://your-custom-domain/.well-known/acme-challenge/{*rest}` to `https://your-custom-domain/api/letsencrypt/{rest}`.

Now, you need to add a function class to handle that request.

Here's the code for mine, adapted from the guide I mentioned.

```csharp
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Serious.Abbot.Functions
{
    public class AcmeChallengeFunction
    {
        [FunctionName("AcmeChallenge")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, Route = "letsencrypt/{*rest}")]
            HttpRequest req,
            ILogger log,
            string rest)
        {
            log.LogInformation($"Acme challenge requested with {req.Method}.");
            var content = await File.ReadAllTextAsync($@"D:\home\site\wwwroot\.well-known\acme-challenge\{rest}");
            var resp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content, Encoding.UTF8, "text/plain")
            };
            return resp;
        }
    }
}
```

Commit that, deploy it, and try it out! Now, hopefully the next person that runs into this will find my blog post and not the hundreds of other irrelevant posts, and know what to do.
