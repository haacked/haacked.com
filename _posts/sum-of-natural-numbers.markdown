---
layout: post
title: "Fun with infinite sums"
date: 2014-01-25 -0800
comments: true
categories: [math]
---
Recently an ["astounding result" from Numberphile](http://www.youtube.com/watch?v=w-I6XTVZXww) on Youtube made its way around the internet. In this video, a couple of physicists "prove" that sum of all natural numbers is `-1/12` (or 0.08333... for you fraction haters). That is, if you take `1 + 2 + 3 + ...` forever and ever, the eventual sum is a negative fraction. What?!

This result defies intuition. But as we'll see, most results when dealing with infinities defy intuition.

In this particular video, they seem to play a bit fast and loose with the math. But there is a kernel of truth to what they demonstrate. I thought it'd be fun to look at their result, throw some code at it, and explore it a bit. But first, I should warn you I am not a mathematician. I have some background in math, but some of the concepts I'll explore I don't have a firm grasp on and might make minor mistakes. Send me a pull request if you catch any!

## But first, an old mathematician vs physicist joke

> A mathematician will call an infinite series convergent if its terms go to zero. A physicist will call it convergent if the first term is finite.

This joke is funny to mathematicians because it pokes fun at the math ability of physicists. The joke is funny to me because it's wrong about the mathematician. Any decent mathematician knows that the terms going to zero is necessary, but not sufficient for the series to converge. For example, the terms of the harmonic series go to zero, but its sum is positive infinity.

![harmonic series](https://f.cloud.github.com/assets/19977/1991291/a3063dfa-8496-11e3-9f24-f932f87db240.png)

For those who need a refresher, it might help if I define what a sequence is and what a series is.

## Definitions

A __sequence__ is an _ordered_ list of elements. For our purposes, we'll limit it to numbers. A sequence can be finite or infinite. For infinite series, it's prudent to have some mathematical formula that tells you how to determine each member of the sequence rather than attempt to write them out by hand. But that could come in handy if you ever need to disable a rogue AI.

A __series__ is simply the sum of the members of a sequence. Clearly finite series are finite. What often is unintuitive is that an infinite series can also be finite.

A __partial sum__ is the sum of a finite portion of the sequence starting at the beginning. In looking at the harmonic series, this is like plugging in a specific value for `n`. So in the case of `n = 1`, the partial sum is `1`. For `n = 2`, it's `1.5`. For `n = 10` it's a lot of addition.

## Infinite series with finite sums

Let's look at an infinite series with a finite sum to prove that such a thing is even possible. Intuition suggests that a series where each term approaches zero "fast enough" could result in a sum that is finite. The approach to zero just has to be "faster" than the growth via accumulation.

The following is known as the Basel Series where each member of the sequence is `1/n^2`. In 1735, Leonhard Euler (pronounced "oiler", not "yuler"), at the tender young age of 28, famously gave a proof for the exact value of this series as `n` goes to infinity. If you have a math symbol fetish, you can go read [Euler's proof here](http://en.wikipedia.org/wiki/Basel_series#Euler.27s_approach). There's enough Greek there to write a tragedy.

![sum-of-reciprocal-squares](https://f.cloud.github.com/assets/19977/1992154/a111bde6-84b2-11e3-8929-a974941b84eb.png)

Since I lack the brilliance of Euler, I'll write some code to demonstrate this convergence because I have COMPUTERS!

The following method gets a partial sum of this series.

```csharp
private static IEnumerable<double> GetEulerPartialSum(int n)
{
    double sum = 0.0;
    for (int i = 1; i <= n; i++)
    {
        sum += 1.0 / Math.Pow(i, 2);
        yield return sum;
    }
}
```

In a console program, we can iterate over this series.

```csharp
static void Main(string[] args)
{
    foreach (var partialSum in GetEulerPartialSum(1000000))
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(partialSum);
    }
    Console.ReadLine();
}
```

The reason for the `SetCursorPosition` call is to have each successive result override the previous result. This way, you can watch the successive values change over time. The first few decimal places converge in a near instant. It takes longer for the further out decimal places. As we get closer to 1 million iterations, we're adding much smaller increments to the sum. If I could run this program with an infinite number of iterations, every position would eventually converge to a value. There's just an infinite number of them.

## Graph it

With C#, it's not too hard to see a graph of this. Just add a reference to `System.Windows.Forms.DataVisualization` and add a `Chart` control to your application and add the following code:

First, we'll slightly modify our previous method to return data points.

```csharp
private static IEnumerable<DataPoint> GetEulerPartialSum(int n)
{
    double sum = 0.0;
    for (int i = 1; i <= n; i++)
    {
        sum += 1.0 / Math.Pow(i, 2);
        yield return new DataPoint(i, (double)sum);
    }
}
```

Next, we'll add these data points to our chart.

```csharp
chart.Series.Clear();
chart.Series.Add("euler");
chart.Series["euler"].ChartType = SeriesChartType.Line;
foreach (var point in GetEulerPartialSum(100))
{
    chart.Series["euler"].Points.Add(point);
}
```

Here's the result.

![euler graph](https://f.cloud.github.com/assets/19977/1992284/d487acf4-84b6-11e3-99c6-c00c7b385a37.png)

Look at how pretty that is.

## Whoops, time for another math and theoretical physics joke

Ok, it's been a while, let's get back to the topic at hand.

> What is the result of `1 + 2 + 3 + 4 + ...`?

> _The mathematician:_ I cannot respond if you do not say to me what it follows after the dots..

> _The physicist:_ it diverges!

> _Polchinski:_ = -1/12

Polchinksi was a string theorist.

So what's the problem with the proof in the video? Well as we've seen, in order for a summation of a series to converge, the terms need to go to 0. But the first series they cover, Grandi's Series, does not converge!

`1 - 1 + 1 - 1 + 1 ...`

Or more succinctly,

![grandis-series](https://f.cloud.github.com/assets/19977/1992288/5ff3b986-84b7-11e3-88a6-541e22d38534.png)

Here's some code for it and the graph that results.

```csharp
private static IEnumerable<DataPoint> GetGrandiPartialSum(int n)
{
    double sum = 0.0;
    for (int i = 0; i < n; i++)
    {
        sum += Math.Pow(-1, i);
        yield return new DataPoint(i, sum);
    }
}
```

![grandi's series](https://f.cloud.github.com/assets/19977/1992306/35d7a29c-84b8-11e3-996b-283666a72093.png)

As you can see, that looks very different. There's no clear convergence to a single value. At every iteration, the partial sum is either `1` or `0`!

So in the video, they mention that you could assign a value of `0.5` to this series. What they refer to is another way of assigning a sum to an infinite series. For example, a [Cesàro summation](http://en.wikipedia.org/wiki/Ces%C3%A0ro_summation) is an approach where you take the average of the _partial sums_. This is slightly confusing so let me clarify. As we saw, the partial sum is either 0 or 1 at every iteration. If we sum up those partial sums at each iteration and divide it by the iteration, we get the average. That's the Cesàro partial sum.

| n | Sum | Sum of Partial Sums | Average = (Sum of Sums / n) |
|---| --- | ------------------- | --------------------------- |
| 1 |  1  |          1          |    1                        |
| 2 |  0  |          1          |    1/2 = 0.5                |
| 3 |  1  |          2          |    2/3 = 0.6666             |
| 4 |  0  |          2          |    2/4 = 0.5                |
| 5 |  1  |          3          |    3/5 = 0.6                |
| 6 |  0  |          3          |    3/6 = 0.5                |

As you can see, it appears to be converging to 0.5. So even though the Grandi series is divergent series and has an infinite sum, it is Cesàro summable and that sum is `0.5`.

There's some code and a graph again to visualize it.

```csharp
private static IEnumerable<DataPoint> GetGrandiCesaroPartialSum(int n)
{
    double sum = 0.0;
    double sumOfSums = 0.0;
    for (int i = 1; i <= n; i++)
    {
        sum += Math.Pow(-1, i);
        sumOfSums += sum;
        yield return new DataPoint(i, sumOfSums / i);
    }
}
```

![grandi Cesàro summation](https://f.cloud.github.com/assets/19977/1992343/1bef65ac-84ba-11e3-8057-f5b5e21f81e9.png)

That's kind of neat.

But here's the thing, a Cesàro summation is not the same thing as a normal additive summation. You can think of its result as a different property of a series from the normal summation. But the physicists in the video treat it like a normal summation as they perform various operations on it.

As an analogy, imagine that I show you two flat shapes and then provide a proof that the shapes cancel each other out by subtracting the perimeter of one from the area of the other. You'd probably call foul on that.

While not a perfect analogy, that's pretty close to what the video shows because they treat the Cesàro sum of the series as if it were a convergent series while they perform various operations to get to their result.

For example, at some point, they shift the series and start adding them. That might work for a convergent series, but not for divergent. 

Here's a simple proof from [Quora](http://www.quora.com/Mathematics/Theoretically-speaking-how-can-the-sum-of-all-positive-integers-be-1-12) that shows how this leads to a contradiction. We'll let `S` represent the sum of the natural numbers. Now, let's subtract `S` from itself, but we'll shift things over by one like they did in the video. 

```
 S =   1 + 2 + 3 + 4 + 5...
-S = -     1 + 2 + 3 + 4...
----------------------------
 0 =   1 + 1 + 1 + 1 + 1...
```

Let's subtract it from itself again shifted over by one.

```
 0 =   1 + 1 + 1 + 1 + 1...
-0 = -     1 + 1 + 1 + 1...
----------------------------
 0 =   1  
```

So here we've proven that `0 = 1`. Mathematicians would call this a _reductio ad absurdum_ which is latin for "that shit's cray!" The premise leads to an absurd result.

## So how do you get -1/12?

While I disagree with the way they get to their result, these guys are not stupid. They did literally point to an equation that shows the sum of natural numbers is `-1/12` in a book.

![-508](https://f.cloud.github.com/assets/19977/2002324/6f786528-85ed-11e3-9147-7594771df0c9.png)

IN A BOOK! Well it must be true then. Why the hell am I bothering with logic when it's in a book? Well if you look closely at the text, you'll see it mentions something about a "[renormalization](http://en.wikipedia.org/wiki/Renormalization)."

## Riemann zeta function and analytic continuations.

As I mentioned earlier, much like a shape has different properties such as area, height, and weight, a summation can have different properties. Or more accurately, there are different approaches to summing a series.

As I was working on this blog post, I learned that the Numberphile folks, who created the original video, produced [a follow-on video](http://www.youtube.com/watch?v=E-d9mgo8FGk) that shows an alternate proof that takes advantage of the [Riemann Zeta function](http://en.wikipedia.org/wiki/Riemann_zeta_function) and [analytic continuations](http://en.wikipedia.org/wiki/Analytic_continuation).

![riemann-zeta-function](https://f.cloud.github.com/assets/19977/2002454/838870a4-85f6-11e3-8988-c0a4164063e2.png)

It's worth a watch. They get into how this applies to string theory.

Analytic continuations are a technique to extend the domain of a function. Recall that a _domain_ is the range of allowed inputs into a function. These are the inputs for which the function provides a valid output. Inputs outside of the domain might diverge to infinity, for example.

The Zeta function's domain is for values greater than 1. `Zeta(1)` diverges to infinity. But we (well not "we", but mathematicians who have a clue what they're doing) can apply analytic continuation to extend the Zeta function below 1 just to see what happens. Mathematicians like to live dangerously.

When you plug in -1 to the zeta function, you get `-1/12`. By definition of the zeta function, that also happens to equal `1 + 2 + 3 + 4 + ...`. So the zeta function is another way to assign a finite value to this divergent series.

To visualize this, look at this graph of the zeta function here, it diverges at x = 1. But if you continue to the left pass the divergence, you can see finite values again.

![zeta function graph from http://planet.racket-lang.org/package-source/williams/science.plt/3/1/planet-docs/science/special-functions.html](https://f.cloud.github.com/assets/19977/2002436/7d0a992e-85f5-11e3-83f2-3b0db9c15c40.png)

In the original formulation of this function, Euler allowed for the exponents to be real numbers. Riemann took it further and generalized it to complex exponents. If you recall, a complex number is in the form of a real part added to an imaginary part. Rather than worry about the implications of "imaginary" numbers, think of complex numbers as coordinates where the real part is the x-axis and the imaginary part is the y-axis. In that regard, it becomes easy to imagine a series that diverges in one axis, but converges in another.

Graphing the Zeta function in this way produces some beautiful 3-D graphs as [seen on Wolfram Alpha](http://mathworld.wolfram.com/RiemannZetaFunction.html) and immortalized by XKCD.

![XKCD on Riemann Zeta - Creative Commons BY-NC 2.5](http://imgs.xkcd.com/comics/riemann-zeta.jpg)

Incomprehensible indeed! When you deal with infinite series, it's near impossible to have an intuitive handle on it. Our minds don't deal with infinity very well, but our math does.

I hope you enjoyed this exploration. I don't pretend to know much about math. I just found it fun to explore this problem with a little code and visualization. If you're interested in the code and graphs, I have a [GitHub Repository](https://github.com/haacked/mathexperiments) with the code I used. 

If you're looking for a practical use of this information, next time someone says they owe you money, just ask them if they'll accept an amount equal to the sum of all natural numbers. If they accept, send them a bill for `$-0.083`.