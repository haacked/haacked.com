# Haacked.com

This is my blog. There are many like it, but this one is mine.

## Run it locally

The following command builds the site and runs it on http://localhost:4000/
It takes a while because I have a lot of posts.

```shell
jekyll serve
```

## Testing

[HTML::Proofer](https://github.com/gjtorikian/html-proofer) is set up to validate all links within the project.  You can run this locally to ensure that your changes are valid:

```shell
bundle install
bundle exec rake test
```