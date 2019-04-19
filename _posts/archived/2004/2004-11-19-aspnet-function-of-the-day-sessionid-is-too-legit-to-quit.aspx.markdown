---
title: 'ASP.NET Function of the Day: SessionId is &quot;Too Legit to Quit&quot;.'
tags: [aspnet]
redirect_from: "/archive/2004/11/18/aspnet-function-of-the-day-sessionid-is-too-legit-to-quit.aspx/"
---

So I was poking around the source code for how ASP.NET initiates session
state etc... and noticed this method of the `SessionId` class. You gotta
love the naming of this one. Steve McConnell (via Code Complete) would
probably have recommended something like "IsValid" but he has no
imagination nor flair.

```csharp
internal static bool IsLegit(string s)
{
    bool flag1;

    if ((s == null) || (s.Length != 0x18))
    {
        return false;
    }

    try
    {
        int num1 = 0x18;
        while (--num1 >= 0)
        {
            char ch1 = s[num1];
            if (!SessionId.s_legalchars[ch1])
            {
                return false;
            }
        }
        flag1 = true;
    }
    catch (Exception)
    {
        flag1 = false;
    }
    return flag1;
}
```

MC Hammer would be proud!

