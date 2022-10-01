---
title: "Pitfalls with eager loading of collections in EF Core"
description: "Eager loading of collections can come with pitfalls when it's not clear if the collection has been loaded or not. This post shows one such pitfall and one approach to working around it."
tags: [aspnetcore, efcore]
excerpt_image: https://user-images.githubusercontent.com/19977/193374485-45a55426-a73c-4971-b6f8-b67e81f91d0b.jpg
---

When using an ORM with a web app, lazy loading will almost certainly result in hidden [N+1 queries](https://medium.com/doctolib/understanding-and-fixing-n-1-query-30623109fe89). Eager loading is a great way to avoid this, but has its own pitfalls. In particular, for each query, you need to be careful about what you include in the query. If you include too much, you can end up with a lot of data that you don't need. If you include too little, you can end up with confusing logic. For example, deep in your application code, it may not be clear if a navigation collection has been loaded yet or not. This can lead to unexpected behavior.

![Drawing of Atari 2600 Pitfall game - CC BY-NC 2.0 by Doctor Popular on Flickr](https://user-images.githubusercontent.com/19977/193374485-45a55426-a73c-4971-b6f8-b67e81f91d0b.jpg "Pitfall - CC BY-NC 2.0 by Doctor Popular")

I know it's boring lazy to use the example of a blog post to illustrate this concept in a blog post, but it's a well understood domain and it's what's often used in EF Core's own documentation. So bear with me.

Here's one approach that you might take (with some properties omitted for brevity):

```csharp
public class Post {
    public int Id { get; set; }
    public IList<Author> Authors { get; set; }
    public IList<Comment> Comments { get; set; }
}
```

In our hypothetical web app, every page that displays a blog post will need to display the post's authors, but only some of them will display comments.

One approach we could take is to always load both collections:

```csharp
// For the individual post page.
public async Task<Post?> GetPostAsync(int id) {
    return await _context.Posts
        .Include(p => p.Authors)
        .Include(p => p.Comments)
        .FirstOrDefaultAsync(p => p.Id == id);
}

// For the home page.
public async Task<List<Post>> GetPostsAsync(int id) {
    return await _context.Posts
        .Include(p => p.Authors)
        .Include(p => p.Comments)
        .ToListAsync(p => p.Id == id);
}
```

This is a simple approach, but not really scalable. The method to get all posts is loading all the comments for every post even though they're not needed. On my blog, this wouldn't be a problem. But this can get expensive if the blog is very popular and millions of people post long-winded comments on it. Let's improve this:

```csharp
// For the home page.
public async Task<List<Post?>> GetPostsAsync(int id) {
    return await _context.Posts
        .Include(p => p.Authors)
        .ToListAsync(p => p.Id == id);
}
```

Better, but now we expose another problem. Suppose deep in our app logic, we get passed a `Post` instance and we want to show the comment count, but we're not sure which query it came from. Notice that `post.Comments` is non-nullable. So I should safely be able to reference `post.Comments.Count` as far as the compiler is concerned. But that'll throw a `NullReferenceException` if the query that loaded the `Post` didn't include comments.

One solution is to make the collection nullable.

```csharp
public class Post {
    public int Id { get; set; }
    public IList<Author> Authors { get; set; } // We always load this, so it's non-nullable.
    public IList<Comment>? Comments { get; set; } // We don't always load this, so it's nullable.
}
```

Then we'd check for null before accessing the collection. Something like this:

```csharp
public async Task<int> GetCommentCountAsync(Post post) {
    if (post.Comments is null) {
        await _dbContext.Entry(post).Collection(p => p.Comments).LoadAsync();
    }
    return post.Comments!.Count;
}
```

Yes, nullable collections suck, but in this case, it makes sense because it communicates an important distinction between the collection being empty vs the collection not being loaded.

So we're in the clear, right? Well, no. It's possible for `post.Comments` to be non-null, but not be fully loaded. Suppose, for some reason, earlier in the same request with the same `DbContext` we load a comment like this:

```csharp
var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
// comment.PostId just happens to be the same as post.Id.
```

And this comment belongs to the same post that we're trying to get the comment count for. It turns out that even though we haven't explicitely included or loaded `post.Comments`, it will be a non-null collection with one entry, the comment. Why?

When using eager-loading with EF Core, it [has an automatic-fixup feature](https://learn.microsoft.com/en-us/ef/core/querying/related-data/eager):

> Entity Framework Core will automatically fix-up navigation properties to any other entities that were previously loaded into the context instance. So even if you don't explicitly include the data for a navigation property, the property may still be populated if some or all of the related entities were previously loaded.

Since a `Comment` associated with the `Post` is already loaded in the `DbContext`, the `Post`'s `Comments` collection will be non-null and contain that comment. This is a bit of a gotcha, that we ran into with [Abbot](https://ab.bot/) in local development recently, so it's not just a hypothetical case. Here's how I ended up fixing it:

```csharp
public async Task<int> GetCommentCountAsync(Post post) {
    if (post.Comments is null || !_dbContext.Entry(post).Collection(p => p.Comments).IsLoaded) {
        await _dbContext.Entry(post).Collection(p => p.Comments).LoadAsync();
    }
    return post.Comments!.Count;
}
```

The call to `_dbContext.Entry(post).Collection(p => p.Comments).IsLoaded` tells us if the collection is fully loaded or not. Automatic fix-up does not set `IsLoaded` to true for the collection. I left the null check as an optimization, but it's not strictly necessary.

I think it would be interesting if the type system could somehow express this distinction. For example, if the entity is returned from a query that includes the collection, then the collection is non-nullable and fully loaded. If the entity is returned from a query that doesn't include the collection, then the collection is nullable and not fully loaded. I'm not sure how to do this in C#, but it would be interesting to explore.
