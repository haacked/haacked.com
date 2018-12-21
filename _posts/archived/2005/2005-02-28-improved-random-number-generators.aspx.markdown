---
title: Improved Random Number Generators
date: 2005-02-28 -0800
tags: []
redirect_from: "/archive/2005/02/27/improved-random-number-generators.aspx/"
---

Via [Dare's blog](http://www.25hoursaday.com/weblog/), I found this
interesting post on [Random Number
Generation](http://www.qbrundage.com/michaelb/about.html) on Michael
Brundage's website. My undergrad thesis was on the topic of pseudorandom
number generation so I thought I'd take the two classes he provided for
a quick spin.

Unfortunately, the C# samples did not compile as is. In his post he
discusses how the C++ samples are optimized. I figured I might be able
to use them to guide changes to the C# port and could post the results
here. Please note that I have not tested them yet and need to verify
that my changes were correct. Enjoy and let me know if I got anything
wrong.

[CLSCompliant(false)]

public class MersenneTwister

{

    private ulong \_index;

    private ulong[] \_buffer = new ulong[624];

 

    /// \<summary\>

    /// Creates a new \<see cref="MersenneTwister"/\> instance.

    /// \</summary\>

    public MersenneTwister()

    {

        Random r = new Random();

        for (int i = 0; i \< 624; i++)

            \_buffer[i] = (ulong)r.Next();

        \_index = 0;

    }

 

    /// \<summary\>

    /// Returns a random long integer.

    /// \</summary\>

    /// \<returns\>\</returns\>

    public ulong Random()

    {

        if (\_index == 624)

        {

            \_index = 0;

            long i = 0;

            ulong s;

            for (; i \< 624 - 397; i++)

            {

                s = (\_buffer[i] & 0x80000000) | (\_buffer[i+1] &
0x7FFFFFFF);

                \_buffer[i] = \_buffer[i + 397] \^ (s \>\> 1) \^ ((s &
1) \* 0x9908B0DF);

            }

            for (; i \< 623; i++)

            {

                s = (\_buffer[i] & 0x80000000) | (\_buffer[i+1] &
0x7FFFFFFF);

                \_buffer[i] = \_buffer[i - (624 - 397)] \^ (s \>\> 1) \^
((s & 1) \* 0x9908B0DF);

            }

 

            s = (\_buffer[623] & 0x80000000) | (\_buffer[0] &
0x7FFFFFFF);

            \_buffer[623] = \_buffer[396] \^ (s \>\> 1) \^ ((s & 1) \*
0x9908B0DF);

        }

        return \_buffer[\_index++];

    }

}

[CLSCompliant(false)]

public sealed class R250Combined521

{

    private ulong r250\_index;

    private ulong r521\_index;

    private ulong[] r250\_buffer = new ulong[250];

    private ulong[] r521\_buffer = new ulong[521];

 

    /// \<summary\>

    /// Creates a new \<see cref="R250Combined521"/\> instance.

    /// \</summary\>

    public R250Combined521()

    {

        Random r = new Random();

        ulong i = 521;

        ulong mask1 = 1;

        ulong mask2 = 0xFFFFFFFF;

 

        while (i-- \> 250)

        {

            r521\_buffer[i] = (ulong)r.Next();

        }

        while (i-- \> 31)

        {

            r250\_buffer[i] = (ulong)r.Next();

            r521\_buffer[i] = (ulong)r.Next();

        }

 

        /\*

        Establish linear independence of the bit columns

        by setting the diagonal bits and clearing all bits above

        \*/

        while (i-- \> 0)

        {

            r250\_buffer[i] = (((uint)r.Next()) | mask1) & mask2;

            r521\_buffer[i] = (((uint)r.Next()) | mask1) & mask2;

            mask2 = mask2 \^ mask1;

            //mask2 \^= mask1;

            mask1 \>\>= 1;

        }

        r250\_buffer[0] = mask1;

        r521\_buffer[0] = mask2;

        r250\_index = 0;

        r521\_index = 0;

    }

 

    /// \<summary\>

    /// Returns a random long integer.

    /// \</summary\>

    /// \<returns\>\</returns\>

    public ulong random()

    {

        ulong i1 = r250\_index;

        ulong i2 = r521\_index;

 

        ulong j1 = i1 - (250-103);

        if (j1 \< 0)

            j1 = i1 + 103;

        ulong j2 = i2 - (521-168);

        if (j2 \< 0)

            j2 = i2 + 168;

 

        ulong r = (r250\_buffer[j1] \^ r250\_buffer[i1]);

        r250\_buffer[i1] = r;

        ulong s = (r521\_buffer[j2] \^ r521\_buffer[i2]);

        r521\_buffer[i2] = s;

 

        i1 = (i1 != 249) ? (i1 + 1) : 0;

        r250\_index = i1;

        i2 = (i2 != 521) ? (i2 + 1) : 0;

        r521\_index = i2;

 

        return r \^ s;

    }

}

