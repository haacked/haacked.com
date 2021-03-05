---
title: ReSharper Property Expansion Live Template
redirect_from:
- "/archive/2004/08/20/954.aspx.html"
- "/archive/2004/08/19/resharper-property-expansion-live-template.aspx/"
tags: [tools]
---

One thing I liked about CodeRush is that it came with several property
expansion templates. However, ReSharper comes with a powerful template
expansion editor for creating your own templates similar to what Whidbey
has. I took it upon myself to create one for ReSharper. I hope you find
it useful. There's also a slight bug with this template in ReSharper
that I will report to them (via this blog entry) and hope they fix.

To add this template, go to the **ReSharper** menu and click
**Options**. Select the **Live Templates** node and click **New**. A
window for creating a Live Template will appear. Fill it out as below.

![Property Expansion Template](/assets/images/PropExpansion.jpg) \
**Figure 1:** Live Template Editor.

Now, to use the template, type the letters "prop" (sans quotes) and hit
the TAB key. This should expand to the following:

![Specifying](/assets/images/propExpanType.jpg) \
**Figure 2:** Specifying the property type.

Don't let all the red squigglies worry you. As you can see in the figure
above, the word TYPE is highlighted by a red box. Type in the type of
the property and hit tab. This will then take you to the second argument
of the template, the name for the private member. In the figure below, I
chose string as the type.

![Specifying the private member name](/assets/images/propExpanMember.jpg) \
**Figure 3:** Specifying the private property member name.

At this point, you can type in the name of the private member that will
hold the value of your property. Since I like to preface my private
members with an underscore, you'll notice that the underscore is part of
the template and is not typed in as part of the PRIVATEMEMBER argument.

As you are typing, you'll notice that public property name matches
whatever you type for the private member, but with the first character
capitalized as in Figure 4.

![It's working.](/assets/images/propExpanGood.jpg) \
**Figure 5** Look ma, I can name a property!

**Bug Alert!** at this point, do not hit the TAB key. Even though we've
set that the PROPERTYNAME is not editable, when you hit TAB after typing
in the private member name, the cursor is taken to the end of the
property expansion, but the PROPERTYNAME is removed. See figure 5.
Instead, you're going to have to hit the down arrow a few times.

![Property Expansion Lost The Name](/assets/images/propExpanLostName.jpg) \
**Figure 5** Where's my property name!?

**Conclusion**\
 Hopefully they fix this in the next version, or provide a workaround or
guideline for how a template like this should be built. I also wish
there was an easy way for me to export a template so that I can share
them easily. In any case, you can probably see all sorts of potential
for these live templates. It's great for company specific boilerplate.
If you have a greate template, post it in my comments section and I'll
try to compile them.

