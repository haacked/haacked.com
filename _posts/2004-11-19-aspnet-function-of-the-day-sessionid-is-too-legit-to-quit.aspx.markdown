---
layout: post
title: "ASP.NET Function of the Day: SessionId is &quot;Too Legit to Quit&quot;."
date: 2004-11-19 -0800
comments: true
disqus_identifier: 1653
categories: []
---
So I was poking around the source code for how ASP.NET initiates session
state etc... and noticed this method of the SessionId class. You gotta
love the naming of this one. Steve McConnell (via Code Complete) would
probably have recommended something like "IsValid" but he has no
imagination nor flair.

~~~~ {style="MARGIN: 0px"}
internal static bool IsLegit(string s)
~~~~

~~~~ {style="MARGIN: 0px"}
{
~~~~

~~~~ {style="MARGIN: 0px"}
    bool flag1;
~~~~

~~~~ {style="MARGIN: 0px"}
    if ((s == null) || (s.Length != 0x18))
~~~~

~~~~ {style="MARGIN: 0px"}
    {
~~~~

~~~~ {style="MARGIN: 0px"}
        return false;
~~~~

~~~~ {style="MARGIN: 0px"}
    }
~~~~

~~~~ {style="MARGIN: 0px"}
    try
~~~~

~~~~ {style="MARGIN: 0px"}
    {
~~~~

~~~~ {style="MARGIN: 0px"}
        int num1 = 0x18;
~~~~

~~~~ {style="MARGIN: 0px"}
        while (--num1 >= 0)
~~~~

~~~~ {style="MARGIN: 0px"}
        {
~~~~

~~~~ {style="MARGIN: 0px"}
            char ch1 = s[num1];
~~~~

~~~~ {style="MARGIN: 0px"}
            if (!SessionId.s_legalchars[ch1])
~~~~

~~~~ {style="MARGIN: 0px"}
            {
~~~~

~~~~ {style="MARGIN: 0px"}
                return false;
~~~~

~~~~ {style="MARGIN: 0px"}
            }
~~~~

~~~~ {style="MARGIN: 0px"}
        }
~~~~

~~~~ {style="MARGIN: 0px"}
        flag1 = true;
~~~~

~~~~ {style="MARGIN: 0px"}
    }
~~~~

~~~~ {style="MARGIN: 0px"}
    catch (Exception)
~~~~

~~~~ {style="MARGIN: 0px"}
    {
~~~~

~~~~ {style="MARGIN: 0px"}
        flag1 = false;
~~~~

~~~~ {style="MARGIN: 0px"}
    }
~~~~

~~~~ {style="MARGIN: 0px"}
    return flag1;
~~~~

~~~~ {style="MARGIN: 0px"}
}
~~~~

MC Hammer would be proud!

