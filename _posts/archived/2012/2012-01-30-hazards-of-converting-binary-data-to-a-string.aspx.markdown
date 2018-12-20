---
title: Hazards of Converting Binary Data To A String
date: 2012-01-30 -0800
disqus_identifier: 18844
tags:
- code
redirect_from: "/archive/2012/01/29/hazards-of-converting-binary-data-to-a-string.aspx/"
---

Back in November, someone asked [a question on
StackOverflow](http://stackoverflow.com/questions/7996955/encoding-to-use-to-convert-bytes-array-to-string-and-vice-versa "Converting bytes to a string")
about converting arbitrary binary data (in the form of a byte array) to
a string. I know this because I make it a habit to read randomly
selected questions in StackOverflow written in November 2011. Questions
about text encodings in particular really turn me on.

In this case, the person posing the question was encrypting data into a
byte array and converting that data into a string. The conversion code
he used was similar to the following:

```csharp
string text = System.Text.Encoding.UTF8.GetString(data);
```

That isn’t exactly their code, but this is a pattern I’ve seen in the
past. In fact, I have a story about this I want to tell you in a future
blog post. But I digress.

The infamous [Jon Skeet](http://msmvps.com/blogs/jon_skeet/ "Jon Skeet")
answers:

> You should *absolutely not* use an `Encoding` to convert arbitrary
> binary data to text. `Encoding` is for when you've got binary data
> which genuinely is encoded text - this isn't.
>
> Instead, use
> [`Convert.ToBase64String`](http://msdn.microsoft.com/en-us/library/system.convert.tobase64string.aspx)
> to encode the binary data as text, then decode
> using[`Convert.FromBase64String`](http://msdn.microsoft.com/en-us/library/system.convert.frombase64string.aspx).

Yes! Absolutely. Totally agree. As a general rule of thumb, agreeing
with Jon Skeet is a good bet.

Not to give you the impression that I’m stalking Skeet, but I did notice
that this wasn’t the first time Skeet answered a question about using
encodings to convert binary data to text. In response to an [earlier
question](http://stackoverflow.com/questions/5915520/store-binary-data-string-into-byte-array-using-c-sharp "Store binary data string into a byte array")
he states:

> Basically, treating arbitrary binary data as if it were encoded text
> is a quick way to **lose data**. When you need to represent binary
> data in a string, you should use base64, hex or something similar.

This perked my curiosity. I’ve always known that if you need to send
binary data in text format, base64 encoding is the safe way to do so.
But I didn’t really understand why the other encodings were unsafe. What
are the cases in which you might lose data?

Round Tripping UTF-8 Encoded Strings
------------------------------------

Well let’s look at one example. Imagine you’re receiving a stream of
bytes and you store it as a UTF-8 string and pop it in the database.
Later on, you need to relay that data so you take it out, encode it back
to bytes, and send it on its merry way.

The [following code](https://gist.github.com/1702906 "As a gist")
simulates that scenario with a byte array containing a single byte, 128.

```csharp
var data = new byte[] { 128 };
string text = Encoding.UTF8.GetString(data);
var bytes = Encoding.UTF8.GetBytes(text);

Console.WriteLine("Original:\t" + String.Join(", ", data));
Console.WriteLine("Round Tripped:\t" + String.Join(", ", bytes));
```

The first line of code creates a byte array with a single byte. The
second line converts it to a UTF-8 string. The third line takes the
string and converts it back to a byte array.

If you drop that code into the `Main` method of a Console app, you’ll
get the following output.

    Original:      128
    Round Tripped: 239, 191, 189

WTF?! The data was changed and the original value is lost!

If you try it with 127 or less, it round trips just fine. What’s going
on here?

UTF-8 Variable Width Encoding
-----------------------------

To understand this, it’s helpful to understand what UTF-8 is in the
first place. UTF-8 is a format that encodes each character in a string
with one to four bytes. It can represent every unicode character, but is
also backwards compatible with ASCII.

ASCII is an encoding that represents each character with seven bits of a
single byte, and thus consists of 128 possible characters. The high
order bit in standard ASCII is always zero. Why only 7-bits and not the
full eight?

Because [seven bits ought to be enough for
anybody](http://www.neurophys.wisc.edu/comp/docs/ascii/ "ASCII Table 7-bit"):

> When you counted all possible alphanumeric characters (A to Z, lower
> and upper case, numeric digits 0 to 9, special characters like "% \* /
> ?" etc.) you ended up a value of 90-something. It was therefore
> decided to use 7 bits to store the new ASCII code, with the eighth bit
> being used as a parity bit to detect transmission errors.

UTF-8 takes advantage of this decision to create a scheme that’s both
backwards compatible with the ASCII characters, but also able to
represent all unicode characters by leveraging the high order bit that
ASCII ignores. Going back to Wikipedia:

> UTF-8 is a variable-width encoding, with each character represented by
> one to four bytes.**If the character is encoded by just one byte, the
> high-order bit is 0 and the other bits give the code value (in the
> range 0..127)**.

This explains why bytes 0 through 127 all round trip correctly. Those
are simply ASCII characters.

But why does 128 expand into multiple bytes when round tripped?

> If the character is encoded by a sequence of more than one byte, the
> **first byte has as many leading "1" bits** as the total number of
> bytes in the sequence, followed by a "0" bit, **and the succeeding
> bytes are all marked by a leading "10" bit pattern**.

How do you represent 128 in binary? 10000000

Notice that it’s marked with a leading 10 bit pattern which means it’s a
continuation character. Continuation of what?

> …**the first byte never has `10` as its two most-significant bits**.
> As a result, it is immediately obvious whether any given byte anywhere
> in a (valid) UTF‑8 stream represents the first byte of a byte sequence
> corresponding to a single character, or a continuation byte of such a
> byte sequence.

So in answer to the question of why does 128 expand into multiple bytes
when round tripped, ~~I don’t really know other than a single byte of
128 isn’t a valid UTF-8 character. So in all likelihood, the behavior
shouldn’t be defined.~~ it’s the [Unicode Replacement
Character](http://en.wikipedia.org/wiki/Specials_(Unicode_block)#Replacement_character "Unicode Replacement Character")
used for invalid data (*Thanks to RichB for the answer in the
comments!*).

I’ve noticed a lot of invalid ITF-8 values expand into these three
bytes. But that’s beside the point. The point is that using UTF-8
encoding to store binary data is a recipe for data loss and heartache.

What about Windows-1252?
------------------------

Going back to the original question, you’ll note that the code didn’t
use UTF-8 encoding. I took some liberties in describing his approach.
What he did was use  `System.Text.Encoding.Default`. This could be
different things on different machines, but on my machine it’s the
[Windows-1252 character
encoding](http://en.wikipedia.org/wiki/Windows-1252 "Windows-1252 character enconding")
also known as “Western European Latin”.

This is a single byte encoding and when I ran the same round trip code
against this encoding, I could not find a data-loss scenario. Wait,
could Jon be wrong?

To prove this to myself, I [wrote a little
program](https://gist.github.com/1702966 "Console app") that cycles
through every possible byte and round trips it.

```csharp
using System;
using System.Linq;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var encoding = Encoding.GetEncoding(1252);
        for (int b = Byte.MinValue; b <= Byte.MaxValue; b++)
        {
            var data = new[] { (byte)b };
            string text = encoding.GetString(data);
            var roundTripped = encoding.GetBytes(text);

            if (!roundTripped.SequenceEqual(data))
            {
                Console.WriteLine("Rount Trip Failed At: " + b);
                return;
            }
        }

        Console.WriteLine("Round trip successful!");
        Console.ReadKey();
    }
}
```

The output of this program shows that you can encode every byte, then
decode it, and get the same result every time.

So in theory, it could be safe to use Windows-1252 encoding of binary
data, despite what Jon said.

**But I still wouldn’t do it.** Not just because I believe Jon more than
my own eyes and code. If it were me, I’d still use Base64 encoding
because it’s known to be safe.

There are five unmapped code points in Windows-1252. You never know if
those might change in the future. Also, there’s just too much risk of
corruption. If you were to store this string in a file that converted
its encoding to Unicode or some other encoding, you’d lose data (as we
saw earlier).

Or if you were to pass this string to some unmanaged API (perhaps
inadverdently) that expected a null terminated string, it’s possible
this string would include an embedded null character and be truncated.

In other words, the safest bet is to listen to Jon Skeet as I’ve said
all along. The next time I see Jon, I’ll have to ask him if there are
other reasons not to use Windows-1252 to store binary data other than
the ones I mentioned.

