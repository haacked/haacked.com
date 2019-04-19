---
title: Splitting Pascal/Camel Cased Strings
date: 2005-09-23 -0800 9:00 AM
redirect_from:
- "/archive/2005/09/24/10334.aspx"
- "/archive/2005/09/22/splitting-pascalcamel-cased-strings.aspx/"
tags: [code]
---

Found [this post in RossCode](http://www.rosscode.com/blog/index.php?title=quick_hits&more=1&c=1&tb=1&pb=1) which mentions a blog post that discusses how to [bind enumerations to drop downs](http://geekswithblogs.net/jawad/archive/2005/06/24/EnumDropDown.aspx),
something I’ve done quite often.

RossCode has an issue with using this approach personally because typically, the display text of a drop down should have spaces between words, which is not allowed in an enum value. For example...

```csharp
public enum UglinessFactor {
    ButtUgly,
    Fugly,
    NotSoBad,
}
```

In the preceding enumeration, you’d probably want the dropdown to display “Butt Ugly” and not “ButtUgly”.

Well if you follow standard .NET naming conventions and Pascal Case your enum values, the following method `SplitUpperCaseToString` may be of service. It depends on another method `SplitUpperCase` which will split a camel or pascal cased word into an array of component words.

As a refresher, a *Pascal Cased* string is one in which the first letter of each word is capitalized. For example, `ThisIsPascalCased`. By contrast, a *Camel Cased* string is one in which the first letter of the string is lowercase, but the first letter of each successive word is upper cased. For example, `thisIsCamelCased`.

```csharp
public static string SplitUpperCaseToString(this string source) {
  return string.Join(" ", SplitUpperCase(source));
}
 
public static string[] SplitUpperCase(this string source) {
  if (source == null) {
    return new string[] {}; //Return empty array.
  }
  if (source.Length == 0) {
    return new string[] {""};
  }
 
  StringCollection words = new StringCollection();
  int wordStartIndex = 0;
 
  char[] letters = source.ToCharArray();  char previousChar = char.MinValue;
  // Skip the first letter. we don't care what case it is.
  for (int i = 1; i < letters.Length; i++) {
    if (char.IsUpper(letters[i]) && !char.IsWhiteSpace(previousChar)) {
      //Grab everything before the current character.
      words.Add(new String(letters, wordStartIndex, i - wordStartIndex));
      wordStartIndex = i;
    }    previousChar = letters[i]; 
  }

  //We need to have the last word.
  words.Add(new String(letters, wordStartIndex,     letters.Length - wordStartIndex)); 
 
  string[] wordArray = new string[words.Count];
  words.CopyTo(wordArray, 0);
  return wordArray;
}
```

Try it out and let me know if it was useful for you.

**UPDATE (8/3/2010):**Fixed a bug so that this doesn’t affect strings that already have spaces in them.

