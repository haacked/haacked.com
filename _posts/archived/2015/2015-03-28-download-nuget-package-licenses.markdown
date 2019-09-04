---
title: "Download all your NuGet Package Licenses"
tags: [oss,nuget,licensing]
---

The other day I was discussing the open source dependencies we had in a project with a lawyer. Forgetting my IANAL (I am not a lawyer) status, I made some bold statement regarding our legal obligations, or lack thereof, with respect to the licenses.

I can just see her rolling her eyes and thinking to herself, "ORLY?" She patiently and kindly asked if I could produce a list of all the licenses in the project.

_Groan!_ This means I need to look at every package in the solution and then either open the package and look for the license URL in the metadata, or I need to search for each package and find the license on [NuGet.org](https://nuget.org/).

If only the original creators of NuGet exposed the package metadata in a structured manner. If only they had the foresight to provide that information in a scriptable fashion.

Then it dawned on me. Hey! I'm one of those people! And that's exactly what we did! I bet I could programmatically access this information. So I immediately opened up the Package Manager Console in Visual Studio and cranked out a PowerShell script...HA HA HA! Just kidding. I, being the lazy ass I am, turned to Google and hoped someone else figured it out before me.

I didn't find an exact solution, but I found a really good start. This [StackOverflow answer](http://stackoverflow.com/a/10055564/598) by [Matt Ward](http://lastexitcode.com/) shows how to download every license for a single package. I then found [this post](http://www.edcourtenay.co.uk/musings-of-an-idiot/list-referenced-nuget-packages-from-the-package-manager-console) by Ed Courtenay to list every package in a solution. I combined the two together and tweaked them a bit (such as filtering out null project names) and ended up with this one liner you can paste into your Package Manager Console. Note that you'll want to change the path to something that makes sense on your machine.

I posted this as a [gist as well](https://gist.github.com/Haacked/31c645b2ca315ebf1a1f).

```powershell
Split-Path -parent $dte.Solution.FileName | cd
New-Item -ItemType Directory -Force -Path ".\licenses"
@( Get-Project -All | ? { $_.ProjectName } | % { Get-Package -ProjectName $_.ProjectName } ) | Sort -Unique Id | % { $pkg = $_ ; Try { (New-Object System.Net.WebClient).DownloadFile($pkg.LicenseUrl, (Join-Path (pwd) 'licenses\') + $pkg.Id + ".html") } Catch [system.exception] { Write-Host "Could not download license for $($pkg.Id)" } }
```

_UPDATE:_ My first attempt had a bug in the catch clause that would prevent it from showing the package when an exception occurred. Thanks to Graham Clark for noticing it, Stephen Yeadon for suggesting a fix, and Gabriel for providing a PR for the fix.

_UPDATE 2019-09-04:_ Updated the gist to work in VS 2019 thanks to [the work of Larry Silverman](https://gist.github.com/silverl/c945a494702c2469372b8be2ef95d319)!

Be sure to double check that the list is correct by comparing it to the list of package folders in your packages directory. This isn't the complete list for my project because we also reference submodules, but it's a really great start!

I have high hopes that some PowerShell guru will come along and improve it even more. But it works on my machine!
