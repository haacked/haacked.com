---
title: "Deploying ASP.NET Core From A Zip File on Azure"
description: "Using Azure Pipelines to deploy a Web App to Azure when running app from a package (zip) file. I had a lot of false starts getting this to work."
tags: [aspnet,azure]
excerpt_image: https://user-images.githubusercontent.com/19977/61091391-e32a6700-a3f6-11e9-96d1-6ca90804ef27.png
---

Azure has a neat feature that runs [Azure Functions from a package file (aka a zip file)](https://docs.microsoft.com/en-us/azure/azure-functions/run-functions-from-deployment-package). This same feature also applies to Azure Web Apps, though you wouldn't know it from [the documentation](https://docs.microsoft.com/en-us/azure/azure-functions/run-functions-from-deployment-package). The Run from Package docs only mention Azure Functions. [The GitHub issue that announced the feature](https://github.com/Azure/app-service-announcements/issues/84) makes it clear this also applies to Web Apps.

> Run From Package is an exciting new feature which lets you run a Web App or Function App by simply pointing it to a zip file containing your files.

There are some cool benefits of this approach according to the docs.

> * Reduces the risk of file copy locking issues.
> * Can be deployed to a production app (with restart).
> * You can be certain of the files that are running in your app.
> * Improves the performance of Azure Resource Manager deployments.
> * May reduce cold-start times, particularly for JavaScript functions with large npm package trees.

Performance doesn't seem to be an issue (caching is probably involved).

## The Punchline

You're a busy developer, so I'll get right to the punchline. Nevermind that I spent hours to get this to work.

If you're using Azure Pipelines to deploy to a Web App using this feature, most of the docs will point you to the [Azure App Service Deploy task](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/deploy/azure-rm-web-app-deployment?view=azure-devops). In the `azure-pipelines.yaml`, you invoke this task using `AzureRmWebAppDeployment@3`.

I could not get that to work. Instead, I discovered another task, [Azure Web App](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/deploy/azure-rm-web-app?view=azure-devops) which you invoke with `AzureWebApp@1`. That's the one that worked for me.

Read on to learn of my harrowing journey to discover this and more details on how to set this all up.

## Define some terms

Before I go further, let me clear up some terminology. Azure terminology keeps me in a constant state of confusion. For example, if you go to your portal, you won't see any navigation item for web apps.

![A portion of the nav bar in the Azure Portal](https://user-images.githubusercontent.com/19977/61091392-e32a6700-a3f6-11e9-848a-68e2ed401d1f.png)

What you're looking for is an App Service. Per [the App Service documentation](https://docs.microsoft.com/en-us/azure/app-service/?WT.mc_id=AzPortal_AppSvc_CmdBar_DocLink)

> Azure App Service enables you to build and host web apps, mobile back ends, and RESTful APIs in the programming language of your choice without managing infrastructure. It offers auto-scaling and high availability, supports both Windows and Linux, and enables automated deployments from GitHub, Azure DevOps, or any Git repo. Learn how to use Azure App Service with our quickstarts, tutorials, and samples.

A _Web App_ is one of the types of App Services. When you click on App Services in the left navigation, the portal displays a list of your app services. Your web apps will have an _App Type_ of _Web App_.

What's confusing to me is that a _Function App_ is not listed in the documentation for Azure App Services. It is logically a first class citizen in its own right. But, when you list your App Services, you'll see Function Apps listed with the _App Type_ of _Function App_. I assume they're mplemented the same way, which is why Run From Package works for Web Apps as well as Function Apps.

## Configure Run From Package

According to the docs,

> To enable your function app to run from a package, you just add a `WEBSITE_RUN_FROM_PACKAGE` setting to your function app settings.

To clarify, when they mention a "setting" here, it refers to an _Application Setting_. In the portal, you can set this by navigating to your App Service (Web App or Function App). Then click on the _Configuration_ link under the _Settings_ heading in secondary navigation on the left.

Make sure you're on the Application Settings tab and click _New application setting_ as shown in the screenshot.

![Screenshot of the Application Settings page](https://user-images.githubusercontent.com/19977/61226960-427ac680-a6d8-11e9-8e2c-6c76a0d9677e.png)

There are two values you can set for this setting.

* __URL to the package__: In this case, it's on you to figure out how to deploy the package to the URL. Azure Blob storage might be a good fit here.
* __1__: If you set the value of the setting to 1, it runs from a special folder in your Web App (or Function App). Let's dig into this.

According to the documentation:

> Run from a package file in the `d:\home\data\SitePackages` folder of your function app. If not deploying with zip deploy, this option requires the folder to also have a file named `packagename.txt`. This file contains only the name of the package file in folder, without any whitespace.

I like this option because in theory, it's less moving parts. However, to get it all to work turned out to be a pain because of some confusing documentation.

## Create the SitePackages folder

If you're running a Web App in App Services, Azure provides this useful administrative Kudu website. Kudu is a deployment system for deploying web apps to Azure from many sources.

To get to the website, append .scm to your web app's URL in the right place. For example, when you set up a web app, Azure will give you a URL that looks like: `https://my-app-name.azurewebsites.net`.

All you have to do is insert `scm.` between your app name and `azurewebsites.net`. Thus in the example above, you'd visit `https://my-app-name.scm.azurewebsites.net` in your browser.

In the menu, select `CMD` or `PowerShell`. This gives you access to both a terminal shell as well as the directory structure.

![Screen showing directory structure and command shell](https://user-images.githubusercontent.com/19977/61091391-e32a6700-a3f6-11e9-96d1-6ca90804ef27.png)

You can use the web UI (or shell) to create the `SitePackages` folder. You can also drag and drop files onto the web page to upload them to the folders.

If you want to do a manual deployment to test out running from a package file, you might run the following steps.

1. `dotnet publish -o c:\some\path\on\your\machine`
2. Zip up the published files (they need to be in the root of your zip archive).
3. Drag and drop the file into the `SitePackages` folder in the administrative Kudu site.
4. Create a file named `packagename.txt` in the `SitePackages` folder. This file only contains the name of the zip file you uploaded, nothing else. No newlines or spaces.

Now you should be able to visit your site and see that it's running from the zip package.

## Deploy from Azure Pipelines

Maybe you're like me and aren't into that whole manual approach. I like my deployments like my transmissions, automatic.

If you recall, earlier I noted the following statement in the docs:

> If not deploying with zip deploy, this option requires the folder to also have a file named `packagename.txt`.

A natural question is, what is a Zip Deploy? Well, [I found these docs](https://docs.microsoft.com/en-us/azure/azure-functions/deployment-zip-push). Those aren't helpful because I want to use Azure Functions to deploy the app.

I then found this documentation on how to [Deploy an Azure Web App](https://docs.microsoft.com/en-us/azure/devops/pipelines/targets/webapp?view=azure-devops&tabs=yaml). It mentions the _Azure App Service Deploy task_ which is invoked via `AzureRmWebAppDeployment@3` when using Yaml with Azure pipelines.

I read [the docs on that](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/deploy/azure-rm-web-app-deployment?view=azure-devops). It has a section with the heading _Run From Package_. Now we're getting somewhere. It states,

> Creates the same deployment package as Zip Deploy. However, instead of deploying files to the wwwroot folder, the entire package is mounted by the Functions runtime and files in the wwwroot folder become read-only. For more information, see Run your Azure Functions from a package file.

Typically, if you use the `AzureRmWebAppDeployment@3` task, it extracts the zip package and deploys it to `D:\home\site\wwwroot`. But you don't want that if you plan to run from package. In fact, when you enabled Run From Package, that changes your `wwwroot` to be read-only. Which makes sense. This deployment task is supposed to understand this setting and instead of deploying to wwwroot, it should deploy to `SitePackages` and create the `packagename.txt` file automatically.

Note, I said _should_. Here's the yaml I tried when I set out to set up an automatic deployment.

```yaml
- task: AzureRmWebAppDeployment@3
  displayName: 'Deploy to Azure App Service'
  inputs:
    azureSubscription: 'redacted - Azure'
    WebAppName: 'redacted'
    Package: $(Build.ArtifactStagingDirectory)/**/*.zip
    enableCustomDeployment: true
    DeploymentType: runFromZip
```

I also tried `runFromPackage` as well as leaving `DeploymentType` blank. Nothing worked. In the debug logs (I'll explain how I turned that on later), I noticed it was trying to do a `PUT` request to `/site/wwwroot` and getting access denied. Well of course access is denied, `wwwroot` is read-only now.

I couldn't understand why the deployment task wasn't working as advertized. I was lucky and happened to be clicking around the list of deploy tasks for Azure Pipelines and discovered one named _Azure Web App Task_. At first I ignored it because it didn't have the word "deploy" in the task name. But then I read the description.

> Use this task to deploy web applications to Azure App service.

WTF?!

Long story short, this worked!

```yaml
- task: AzureWebApp@1
  displayName: 'Deploy to Azure App Service'
  inputs:
    azureSubscription: 'redacted - Azure'
    appName: 'redacted'
    package: $(Build.ArtifactStagingDirectory)/**/*.zip
    enableCustomDeployment: true
    deploymentMethod: runFromPackage
```

I didn't find much in the way of documentation that points to it as an option. Either way, I'm glad I found it and I hope this saves you time and keeps you from headaches.

## Adding Debug Logs to Azure Pipelines

To help debug issues with my deployment, I turned on debug logging in Azure Pipelines. To do this, add the `system.debug: true` variable to your yaml file. For example, here's the set of variables in mine.

```yaml
variables:
  BuildConfiguration: Release
  vmImage: windows-2019
  system.debug: true
```

This will spit out a lot of extra details in your Azure Pipelines log. You may want to turn it back off when you no longer have issues.
