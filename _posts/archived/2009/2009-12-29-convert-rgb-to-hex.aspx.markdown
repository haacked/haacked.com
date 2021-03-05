---
title: Converting an RGB Color To Hex With JavaScript
tags: [code]
redirect_from: "/archive/2009/12/28/convert-rgb-to-hex.aspx/"
---

UPDATE: 12/30 I had transposed the rgb colors. I corrected the function.

I’ve been distracted by a new jQuery plugin that I’m writing. The plugin
has certain situations where it sets various background and foreground
colors. You can have it set those styles explicitly or you can have it
set a CSS class, and let the CSS stylesheet do the work.

[![color-wheel](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ConvertinganRGBColorToHexWithJavaScript_12017/color-wheel_3.jpg "color-wheel")](http://www.sxc.hu/photo/828516 "Color Wheel on sxc.hu by asifthebes")I’m
writing some unit tests to test the former behavior and ran into an
annoying quirk. When testing the color value in IE, I’ll get something
like `#e0e0e0`, but when testing it in FireFox, I get
`rgb(224, 224, 224)`.

Here’s a function I wrote that normalizes colors to the hex format. Thus
if the specified color string is already hex, it returns the string. If
it’s in rgb format, it converts it to hex.

```csharp
function colorToHex(color) {
    if (color.substr(0, 1) === '#') {
        return color;
    }
    var digits = /(.*?)rgb\((\d+), (\d+), (\d+)\)/.exec(color);
    
    var red = parseInt(digits[2]);
    var green = parseInt(digits[3]);
    var blue = parseInt(digits[4]);
    
    var rgb = blue | (green << 8) | (red << 16);
    return digits[1] + '#' + rgb.toString(16).padStart(6, '0');
};
```

Now, I can compare colors like so.

```csharp
equals(colorToHex('rgb(120, 120, 240)'), '#7878f0');
```

I hope you find this useful. :)

