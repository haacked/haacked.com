---
title: "Lessons To My Younger Self Building a Startup"
description: "Depending on SAAS, PAAS, and IAAS is great, but it can hurt when you should have turned left at Albuquerque if you haven't insulated yourself from some of those choices."
tags: [aspnetcore, azure, cloud]
excerpt_image: https://user-images.githubusercontent.com/19977/180076597-6fbdb672-6539-43fd-b10d-1a1e667c8d8b.png
---

Today my team and I are launching [Abbot, a bot that helps teams keep track of customer conversations in Slack, on Product Hunt](https://www.producthunt.com/posts/abbot). Please check it out and upvote it if you like what we've presented there!

Some of you might be thinking, "Wait! I thought Abbot was [ChatOps as a service](https://news.ycombinator.com/item?id=27974077)?" Indeed, that was our original goal, but we found it tough to to sell. Conversations often would go like this:

> them: What problem do you solve?
> us: What problems do you have? Abbot can help with them all!
> them: *snoring*

Yeah, not too effective.

## Lesson: Articulate a specific problem your product solves

I know this isn't new or groundbreaking advice. In fact, it's one of the first things you hear when people give advice about building a company. But it's easy to fall into the trap of thinking that *your* product is going to be the exception.

It's not that Abbot wasn't interesting, useful, and wonderful. So many told us, "That's very cool! There's so much I could do with that! I hope to find some time to experiment with it." And there's the rub. People are busy and they don't have time to solve their own problems. They want your product to solve their problems. A specific problem that they deal with day to day, not something hypothetical. This leads us right to the next lesson. This leads right into the next lesson.

## Lesson: Start with selling a Product not a Platform

We built Abbot to be a platform for implementing a ton of useful utilities initiated in chat via our bot. But platforms solve classes of problems, not specific problems. And as we can infer from the previous lesson, platforms are hard to sell as a result. This is why Amazon didn't begin with selling AWS. They started with an online bookstore. A concrete product. And over time, they reached the point where they could sell the platform the bookstore was built on.

## Lesson: A Platform does make it easy to experiment and pivot

One thing we did well during this time was to listen to our current and prospective customers. We started to notice a theme. Many of them struggled to support their own customers in Slack. Slack has a neat feature called Slack Connect that lets you invite other companies into a shared Slack channel. We discovered that a lot of companies had customer success teams working with customers in Slack. Their customers liked the immediacy of being in a shared chat room. It's more responsive than getting on a phone or dealing with email. And it makes hand-off of discussions easy.

But supporting customers in chat has its own set of problems. With so many conversations coming in, it can be tough to keep track of them all. Did we respond to every conversation? Is there any conversations waiting on a response? What's our average response time? There are a ton of tools to answer those questions for email support, but not so much for chat.

Hey! Now we have a specific problem we can solve! Not only that, we have a flexible platform we can build on top of to address it quickly. So we pivoted. With the power of our platform, it was more like a slight graceful pirouette. It didn't take us long to repurpose our platform to solve this specific problem.

But as we did so, it didn't take us long to run into other problems.

## Lesson: Balance your early engineering and infrastructure with achieving Product Market Fit

Now, before I get into the problems we ran into, it's important to understand some context, lest you think we're running a clown shoes operation. When we joined the YCombinator startup program, one of the key lessons they drilled into us is tha Product Market Fit is our top priority. And the reasoning is simple, if you don't achieve product market fit, then you don't have a business and your company runs out of money and dies. So if you spend too much time on engineering the perfect infrastructure, but you don't achieve product market fit, then all that exquisite infrastructure goes to waste.

At the same time, if you wait too long to address your infrastructure needs, your product may fail under the weight of all that juicy product market fit. It's definitely a delicate balancing act and one that you have to feel out for yourself. I hope by sharing some of our failures, it can provide some insights on how to strike that balance for your own product and company. That balance is going to be different for every product and company.

## Lesson: All Services Are Leaky

Around the time dinosaurs roamed the wold hosting their own server racks, I was a fresh-faced college graduate starting my first job at a custom development shop. We had a full server rack in the closet where we hosted all our clients web sites. We were in control of our own hardware, software, and destiny. Which as it turns out, isn't always a good thing. I remember the time my boss wanted to demonstrate how our new hard-drive array was hot-swappable by pulling out a drive. He pulled out the drive and everything continued to work flawlessly. Just kidding. We know how this story goes. It was supposed route around the damage, instead it all came crashing down like a soccer player who feels a gentle tug on the shirt in the penalty box (if the sport metaphor doesn't work for you, just trust me on this one).

At the time, this was the way you built web software. You hosted everything.

Fast forward a bunch of years, and the pendulum swung to the other side. I didn't want to host a damn thing. And with tools like Heroku, Azure Web Sites, AWS, Google Cloud, etc. I really didn't need to. Much of my later career at Microsoft and GitHub was spent building developer tools and client applications. Occasionally, I'd build a website on the side for fun. I'd just deploy it to cloud provider, take advantage of all their services, and not worry about it too much.

And this approach worked well! It worked so well, when I started a company with my colleague, Paul, we were able to [get a site up](https://ab.bot/) and running in no time. And this all worked great, until we needed to pivot.

![Driver telling a police office he should have turned left at Albuquerque](https://user-images.githubusercontent.com/19977/180076597-6fbdb672-6539-43fd-b10d-1a1e667c8d8b.png "Free Public Domain image from https://freesvg.org/1535673080")

You may have heard that [all abstractions are leaky](https://www.joelonsoftware.com/2002/11/11/the-law-of-leaky-abstractions/). In a similar manner, all services are leaky. Whether it be Software as a Service (SAAS), Platform as a Service (SAAS), Infrastructure as a Service (IAAS), or whatever the next thing is.

The first "abstraction" to break was relying on Azure Bot Service for our bot. The service provides a single API to make it possible to write a bot once that can communicate with multiple chat services. Where have we heard that promise before, right? As you might expect, it meant our bot had access to the lowest common denominator of what chat platforms have to offer. This was fine for our initial product idea, [ChatOps as a service](https://news.ycombinator.com/item?id=27974077). With ChatOps, the interface is primarily textual, so we didn't need all the bell and whistles. But as we started pivoting to providing a Customer Success product to help keep on top of conversations in Slack, it became imperative that we supported all the rich features that Slack has to offer.

This meant bypassing the bot service and writing our code to directly interact with the Slack API. I don't regret that we started with the bot service as it got us far fast, but I do regret when we configured our Slack Events Endpoints (the URLs that Slack sends its events to), we configured it with the bot service URLs. Something like `https://slack.botframework.com/api/messages/events` (or whatever it is). The reason this was a problem is that we submitted our Slack App to the Slack App Directory. So every change we make to the app's configuration can take up to six weeks to be approved by Slack. This meant that even though we were ready to go with our new approach, we had to wait for the Slack approval process to start serving Slack requests directly.

## Lesson: Own Your Traffic With A Level of Indirection built in

Ideally, we would have set up a DNS entry and configured Slack with that. It would have allowed us to point it to Azure Bot Service in the beginning, and later point it to our own web server when we needed to switch. At the time, we felt we wouldn't need it. But it's inexpensive to set up a DNS entry for a level of indirection.

As a corollary to this, it's really helpful to manage DNS as code. We have several DNS providers, but we now manage all our DNS in a repository using https://github.com/octodns.

Alright, back to the story. So what do you think happened once the approval went through and we were directing Slack traffic directly to our server? Of course, we found a bug and it didn't work. And we made the mistake of pointing Slack directly to https://ab.bot/ which is our main site, so we couldn't just point DNS back to botframework.com. We were really in a bind because it would take another six weeks to change the Slack configuration back to pointing to Bot Service.

## Lesson: Own your Ingress

Yes, we should have used a DNS entry other than our main app's entry. In retrospect it's so obvious. But even so, we also should have had some sort of forward proxy in place such as Azure Front Door or Azure Application Gateway. I should mention, neither of those choices worked out for us, which leads me to the next lesson.

## Lesson: Balance Convenience With The Right Tools

I mentioned before that my background is with Azure, so I tend to choose Azure tools because I know them and it's convenient to have everything in one place with the Azure Portal. However, this sometimes means I end up choosing tools that aren't up to snuff. It turns out that Azure Front Door and Azure Application Gateway each have deficiences. And the only solution that Microsoft could provide us was to use both at the same time, which seemed complex and overkill. Especially when Cloudflare does exactly what we need and is probably the best in breed of this type of thing.

As we started to build out our new product offering, we started to notice connection timeouts. Our database was getting hammered periodically and we had no understanding of why. Fortunately, we were able to figure it out by adding better instrumentation.

## Lesson: Observability and Monitoring With Alerts is Essential

See, we had a lot of logging, but not enough. Even worse, we didn't have alerts set up early on. So errors would occur and we wouldn't notice them. I know I'm making it sound like we're running a clown shoes operation, but our primary focus at the time was product market fit and getting something useful in our customers hands to try out. But as we started getting more people trying it out, being able to diagnose problems quickly rose up in priority.

## Lesson: Code Your Infrastructure

We ended up figuring out that some problematic code was the cause of some of our error spikes. But we were also on a pretty low-tier plan for the database and web server. As we got closer and closer to shipping our 1.0, we realized we needed to scale up. Now *this* is where the cloud shines, right?

Well no, we happened to choose a plan that didn't allow us to automatically scale up. We had to migrate to a new plan. This meant a lot of manual work and scripting to migrate our web servers and data to new servers and databases in the cloud.

And that helped a lot! So now we're on a plan that will allow us to automatically scale up our infrastructure! Except we happened to choose a region, US West 2, that's at capacity due to recent supply chain issues. So in order to scale up our hardware even more, we need to do yet another migration.

It wouldn't be so bad if all our infrastructure was scripted using something like Terraform or Pulumi, but we're not quite there yet. We've made a lot of progress with Pulumi lately, but we're still not at the point where we can just migrate to a new data center with a few commands. That's where we're headed next. I used to believe that being Cloud Provider agnostic was a waste of engineering effort, but as we build out our product, we can see that even if you stick with a single cloud provider, it has benefits.

They say that when you build a startup, you're going to make a lot of mistakes. And like an idiot, I thought I've been around the block and wouldn't make all those rookie mistakes. Yet here we are. I hope that your startup can learn from our mistakes (assuming you're building a similar type of product).

## Lesson: Hire great people

One of our saving graces is we've been very fortunate in our early hiring. We now have folks who are strong where I am weak. Many of the challenges we face have been addressed by their expertise.