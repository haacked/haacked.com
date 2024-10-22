---
title: "Calculating MRR with Stripe and C#"
description: "Monthly Recurring Revenue is an important indicator for the health of your business. Here's how to calculate it using the Stripe API and C#."
tags: [stripe,abbot]
excerpt_image: https://user-images.githubusercontent.com/19977/195433434-7d3bd771-e32a-4630-b12f-08980bf5abc2.jpg
---

Over here at [A Serious Business, Inc.](https://www.aseriousbusiness.com/) we're very [serious about security](https://ab.bot/blog/abbot-is-soc2-compliant). One principle that's important to us is what we call the *Principle of Least Exposure* (not to be confused with the similar [Principle of Least Privilege](https://en.wikipedia.org/wiki/Principle_of_least_privilege)).

In simple terms, the principle is:

> You can't expose what you don't have.

For example, our product [Abbot](https://ab.bot/) doesn't store or accept credit card payment info directly. Instead, we use a trusted third party, [Stripe](https://stripe.com/), to handle that for us.

[![Rolls of $100 bills](https://user-images.githubusercontent.com/19977/195433434-7d3bd771-e32a-4630-b12f-08980bf5abc2.jpg "Dolla dolla bills, y'all! - CC BY 2.0 by Pictures of Money")](https://www.flickr.com/photos/pictures-of-money/17123251389/)

Stripe can be a bit tricky to get started with, but that complexity reflects the inherent complexity of accepting payments. Fortunately, they supply some great tools and [documentation](https://stripe.com/docs) to help you get started. Also, their support is top quality. Every time I contact them, they're quick to respond and helpful. And before you ask, no, they're not paying me to say that. But in the interest of full disclosure, they did send me a Stripe t-shirt. I'm wearing it right now. It's very comfortable.

## Calculating MRR

One of the most important metrics for any SaaS business is Monthly Recurring Revenue (MRR). It's a simple concept: how much money are you making every month? It's a great indicator of the health of your business. If your MRR is growing, you're doing well. If it's shrinking, you're not.

MRR is forward looking, which is why you don't want to sum up invoices. Instead, you want to look at all the subscriptions you have and sum up the amount of money you're expecting to receive from them in the next month. For yearly subscriptions, you'll want to pro-rate the yearly amount to get the monthly amount.

Fortunately, Stripe has a nice dashboard where you can see your MRR. I'm not supposed to share ours, but we're all friends here right?!

![Example of MRR on the Stripe Dashboard](https://user-images.githubusercontent.com/19977/195445796-618ccc09-e369-46a8-b680-f4f60f6deb9f.png "We're RICH!...friend.")

Unfortunately, it can take some time to reflect recent purchases. In our case, we wanted to display the MRR in an internal dashboard and I could not find a way to grab the MRR that Stripe calculates from the Stripe API. If there is a way and I just missed it, I hope someone will let me know in the comments.

So naturally I wrote some code to do this. As with most .NET code, it starts with installing a NuGet package.

```bash
> dotnet add package Stripe.net
```

Then I wrote some methods to calculate the MRR. If you want to jump to the [full code listing, check out this gist](https://gist.github.com/haacked/0a34391bfc2fddda192a082cfe5867af). The following walks through each step.

The first step is to calculate the MRR for a single subscription. There's two important things to consider here. First, we need to normalize subscriptions to monthly. For example, for an annual subscription, you need to divide by 12 to get the monthly revenue amount. For days and weeks, I just picked 30 and 4 respectively. I'm not sure what the standard value should be here, but we don't offer daily or weekly subscriptions so it's not important to me. I just include it here for completeness.

The second important thing is to consider the item quantity. My first attempt at this code neglected the item quantity and I ended up underreporting our MRR.

```csharp
public static decimal CalculateSubscriptionMonthlyRevenue(Subscription subscription)
{
    decimal revenue = 0;
    foreach (var item in subscription.Items)
    {
        var multiplier = item.Plan.Interval switch
        {
            "day" => 30M,
            "week" => 4M,
            "month" => 1M,
            "year" => 1M / 12M,
            _ => throw new UnreachableException($"Unexpected plan interval: {item.Plan.Interval}.")
        };
        revenue += multiplier * item.Quantity * item.Price.UnitAmountDecimal.GetValueOrDefault();
    }
    return revenue / 100M; // The UnitAmount is in cents.
}
```

Next we calculate the MRR for a customer. In theory, a customer can have multiple subscriptions, so we handle that. More importantly, if your customer has a discount due to a coupon, you want to apply that discount to the calculation. However, for a one-time amount off discount, you can pretty much ignore it for the purposes of MRR as it's forward looking.

```csharp
public static decimal CalculateCustomerMonthlyRevenue(Customer customer)
{
    var subscriptions = customer.Subscriptions;
    var revenue = 0M;
    foreach (var subscription in subscriptions)
    {
        revenue += CalculateSubscriptionMonthlyRevenue(subscription);
    }
    // Apply the coupon, if any. We only look at % off coupons.
    // We can ignore the amount off discount. That's a one time discount and doesn't affect ongoing MRR.
    if (customer.Discount is { Coupon.PercentOff: { } percentOff })
    {
        revenue *= 1 - percentOff / 100M;
    }

    return revenue;
}
```

And finally, we put it all together by grabbing all of the active subscriptions.

```csharp
public static async Task<decimal> CalculateMonthlyRecurringRevenue()
{
    string? lastId = null;
    var customerClient = new CustomerService();
    decimal revenue = 0M;
    bool hasMore = true;
    while (hasMore)
    {
        var customers = await customerClient.ListAsync(
            new CustomerListOptions
            {
                Limit = 100, /* Max Limit is 100 */
                Expand = new List<string> { "data.subscriptions" },
                StartingAfter = lastId
            });

        revenue += customers.Sum(CalculateCustomerMonthlyRevenue);
        hasMore = customers.HasMore;
        if (hasMore)
        {
            lastId = customers.LastOrDefault()?.Id;
            if (lastId is null)
            {
                throw new InvalidOperationException("API reports more customers but no last id was returned.");
            }
        }
    }

    return revenue;
}
```

The code here is a bit tricky because the Stripe APIs only let you grab 100 customers at a time. And since we always want to prepare for success, we need to handle the case where there are more than 100 customers. So we loop through the customers, grabbing 100 at a time, until we get to the end.

## Conclusion

I hope this helps you use Stripe to calculate your MRR. If you have improvements to the code, feel free to comment on the gist with the [full code listing](https://gist.github.com/haacked/0a34391bfc2fddda192a082cfe5867af).
