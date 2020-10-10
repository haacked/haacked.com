---
title: "..."
description: "..."
tags: [dotnet,azure,deployment]
excerpt_image: ...
---

On Twitter the other day, [David Anson asked](https://twitter.com/DavidAns/status/1314598412277280776),

> If someone is working 100% remotely, why should their pay be tied to which city they are in? They produce exactly the same work if they are in a big city vs. a farm house. "Cost of living" adjustments are for when the job forces people to work somewhere; that's not relevant here.

A lively debate ensued. Several people suggested cost of living and local market rates warrant different pay based on a person's city of residence. I agree that is the case today, but I don't believe this is sustainable.

My belief is based on a few assumptions. First, this discussion is focused on information workers. More specifically, developers because that's an audience I understand. My assumption is that developers can produce the same quality of work no matter where they live as long as they have good Internet access.

Second, I assume that remote work will become more prevalent and even dominant in the future. The current pandemic is one driver of that. As people work from home, many companies realize their employees are just as productive. Microsoft, as one example, announced they [will let even more employees to work from home](https://www.theverge.com/2020/10/9/21508964/microsoft-remote-work-from-home-covid-19-coronavirus).

__So if my assumptions are correct, in the long run, I expect companies to disregard location when considering compensation more and more.__ If my assumptions are incorrect, then, well you can disregard all this. The reason for this compensation equilibrium is described by an economic principle called the law of one price (LOOP). I'll get back to that later. 

## How Companies Determine Compensation

Let's start with a basic question. How do companies determine compensation? Is it based on a moral idea of what a person needs or deserves? Is it based on cost of living? __The answer is that companies in general pay as little as they can get away with.__ I'm not being cynical when I say this, but realistic. And yes, there may be exceptions, but they are few.

What a company can get away with depends on a number of factors, but the most significant one is how much other employers are paying. In other words, the market rate.

A simple thought experiment clears up why this is the case. Suppose you're a company in a city where the average compensation for a starting developer is $100K. You decide to pay $75,000 for starting developers. You will have a hard time hiring developers. They'll flock to the companies that pay $100,000. This explains why the market puts a lower limit on pay.

Now suppose you decide to be generous and pay $125,000. You'll probably have no trouble hiring developers, but now you have to rely on smaller team sizes than if you paid the market rate. The natural pressure is to pay something close to the market rate. This is why companies do not have a tendency to pay above market.

But as I mentioned before, the market is not the only factor in play. Information asymmetry plays a role. Companies often have better salary data than individual employees. Companies leverage that to keep pay as low as possible. If an employee doesn't realize they are being paid less than their peers, they won't agitate for more money.

A [recent study](http://www.forbes.com/sites/cameronkeng/2014/06/22/employees-that-stay-in-companies-longer-than-2-years-get-paid-50-less/) supports this claim.

> Staying employed at the same company for over two years on average is going to make you earn less over your lifetime by about 50% or more.
> 
> Keep in mind that 50% is a conservative number at the lowest end of the spectrum. This is assuming that your career is only going to last 10 years. The longer you work, the greater the difference will become over your lifetime.

This evidence supports the idea that companies will pay as little as they can get away with. If companies tracked the market, then the difference between staying or leaving should be negligible. If they paid above market, then employees would be rewarded for their loyalty. __Instead, the evidence demonstrates that companies penalize loyalty financially.__

## How Remote Work Changes The Market

Again, while many factors come into play when determining compensation, the market is the most significant factor. Today, the market is still largely local. The compensation for developers in San Francisco are significantly higher than for those in Seattle. And those in Seattle are still higher than those in Columbus Ohio.

These differences persist because of market friction. The company in Columbus can continue to pay Columbus wages because developers in the area don't have easy access to better options. It's a lot of friction for a developer there to move to SF for higher pay. Likewise, it's a lot of friction for SF companies to build a satellite office there to access cheaper talent. Wages in these cities match the local market.

This makes sense when companies require people to have their butts in seats at the company office. But what happens as more and more companies embrace remote work and allow their employees to live wherever they want?

For companies that embrace a remote workforce, the market for talent expands beyond their current geographical boundaries. This leads to a talent market that has less and less friction. The company in SF that is willing to have remote workers now has less friction to hire a developer in Columbus.

This is where [the law of one price](https://www.investopedia.com/terms/l/law-one-price.asp) I mentioned earlier comes into play.

> The law of one price is an economic concept that states that the price of an identical asset or commodity will have the same price globally, regardless of location, when certain factors are considered.
> 
> The law of one price takes into account a frictionless market, where there are no transaction costs, transportation costs, or legal restrictions, the currency exchange rates are the same, and that there is no price manipulation by buyers or sellers. The law of one price exists because differences between asset prices in different locations would eventually be eliminated due to the arbitrage opportunity

Ah yes, described in the dry and detached language of an economist.

So how does this apply to developer compensation and remote work? A hypothetical may help here.

Imagine a future in which remote work is the dominant mode of work in the United States. Developers can work for any company anywhere in the country. Yet developers are still compensated according to the local market in the city where they live. In this scenario, developers in SF make $200k and developers in Ohio make $100K. What would happen over time?

Well, smart companies in SF would realize they're overpaying for developers. Why not dangle $125k salaries in front of the best developers in Ohio and poach them all? Faced with this competition, companies in Ohio would have to start raising wages in order to retain their best employees. At the same time, wages in SF would start to drop as SF developers are competing with developers in Ohio and across the company. The general trend would be for developer salaries across the country to reach something approaching equilibrium. This follows the law of one price.

## The Wrench in the Market

Does that mean all developers of the same level and capability in the country will be paid exactly the same amount. No. Of course not. The point I'm making is that as more companies embrace remote work, that reduces friction in the market for developer talent, and that leads to a _trend_ towards equilibrium.

So why wouldn't it reach full equilibrium?

Well, for starters, frictionless markets are a theoretical environment. There will always be some friction. For example, for many companies, time zone differences will be a source of friction. While [I believe the discipline required to run successful teams across time zones](https://haacked.com/archive/2020/03/09/geographically-distributed-teams/) produce many benefits, it's still a big challenge for many companies.

Also, one of the conditions for the law of one price is,

> there is no price manipulation by buyers or sellers.

Only five years ago, several tech companies paid a settlement due to wage manipulation,

> A federal judge has approved a $415 million settlement that ends a lengthy legal saga revolving around allegations that Apple, Google and several other Silicon Valley companies illegally conspired to prevent their workers from getting better job offers.
> The case focused on a “no-poaching” pact prohibiting Apple, Google and other major employers from recruiting each other’s workers. Lawyers for the employees argued the secret agreement illegally suppressed the wages of the affected workers.

[Racism](https://www.inc.com/salvador-rodriguez/hired-salaries-report.html) and [sexism](https://spectrum.ieee.org/view-from-the-valley/at-work/tech-careers/us-women-tech-paid-less-men) also continue to contribute to pay disparity.
