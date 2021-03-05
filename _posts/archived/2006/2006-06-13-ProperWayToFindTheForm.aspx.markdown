---
title: Proper Way To Find The Form
tags: [code,aspnet]
redirect_from: "/archive/2006/06/12/ProperWayToFindTheForm.aspx/"
---

![1040 EZ Form](https://haacked.com/assets/images/1040ez.jpg) Today I ran across
some code in a 3rd party open source library that used the following
function in order to retrieve the form id.

```csharp
public static string GetPageFormID(Control page)
{
    string id = null;

    foreach (Control con in page.Controls)
    {
        if (con is HtmlForm)
        {
            id = con.ClientID;
            break;
        }
    }
    return id;
}
```

Which gets called like so:

```csharp
Control page = HttpContext.Current.Page;
string formID = GetPageFormID(page);
```

Unfortunately this didn’t work for me because I don’t have the form
declared as a direct child of the page. Instead the page contains a user
control which contains the form. This is a common scenario when using a
MasterPage (in my case an ASP.NET 1.1 backported master page control).
When looking for the form, the function should search recursively like
so:

```csharp
public static string GetPageFormID(Control page)
{
    string id = null;

    foreach (Control con in page.Controls)
    {
        if (con is HtmlForm)
        {
            return con.ClientID;
        }
        id = GetPageFormID(con);
        if(id != null)
            return id;
    }
    return id;
}
```

This will search the entire control hierarchy until it finds the
HtmlForm. In the most common case, it will find it without having to
recurse. But for crazy folks like me who always look for ways to be
different, this will do the trick. Luckily this was an open source
library I was using so I was able to fix the code and send the authors a
patch.

