Recently, a couple of physicists blew a bunch of minds on the internet with their [video "proof"] that the sum of all natural numbers is -1/12 (or 0.833.. for you fraction haters).

This result, as you might expect, defies intuition. It turns out, that they played a bit fast and loose with the math, but there is a kernel of truth to what they did. Many much better mathematicians than me have explained the result, but I thought I'd have a bit of fun with it using computers because it's an interesting idea.

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

Let's look at an infinite series with a finite sum to prove this is possible. Intuitively, you'd expect that a series where each term approaches zero "faster" than they add to the sum could approach a finite number.


The following is known as the Basel Series where each member of the sequence is `1/n^2`. In 1735, Leonhard Euler (pronounced "oiler", not "yuler" as I did during nearly my entire time as an undergraduate), at the tender young age of 28, famously gave a proof for the exact value of this series as `n` goes to infinity. If you have a math symbol fetish, you can go read [Euler's proof here](http://en.wikipedia.org/wiki/Basel_series#Euler.27s_approach).

![sum-of-reciprocal-squares](https://f.cloud.github.com/assets/19977/1992154/a111bde6-84b2-11e3-8929-a974941b84eb.png)

But lacking the brilliance of Euler, I'll write some code to show this convergence.

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

This code keeps overwriting the previous result in the console. This allows you to see the values converging. The first few decimal places converge immediately. The further out you go in decimal places, the longer it takes for a value to converge. As we get closer to 1 million iterations, we're adding every smaller increments to the sum.

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

![grandi cesaro summation](https://f.cloud.github.com/assets/19977/1992343/1bef65ac-84ba-11e3-8057-f5b5e21f81e9.png)

That's kind of neat.

But here's the thing, a Cesàro summation is a different sort of thing than a straightforward summation. And in the video, the people treat it like a normal summation and perform some algebraic operations with it. That's not allowed. That's missing apples with oranges.

For example, at some point, they shift the series and start adding them. That might work for a convergent series, but not for divergent. 

Here's a simple proof from [Quora](http://www.quora.com/Mathematics/Theoretically-speaking-how-can-the-sum-of-all-positive-integers-be-1-12) that shows how this leads to a contradiction. We'll let `S` represent the sum of the natural numbers. Now, let's subtract `S` from itself, but we'll shift things over by one like they did in the video. 

```
 S =   1 + 2 + 3 + 4 + 5...
-S = -(    1 + 2 + 3 + 4)...
----------------------------
 0 =   1 + 1 + 1 + 1 + 1...
```

Clearly, that's not true.
