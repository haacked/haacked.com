---
layout: post
title: "Sql Query Analyzer Template Parameters"
date: 2005-07-01 -0800
comments: true
disqus_identifier: 7433
categories: [sql]
---
I’m not sure if this is common knowledge, but you can place template
parameters in your SQL scripts and evaluate them within query analyzer.
I think I learned this one a long time ago from a former fantastic
SysAdmin, turned DBA, turned Developer, Tyler.

Here’s an example of a short script that makes use of a template
variable.

```sql
`SELECT * FROM <tableName, varchar(32), 'MyTable'>`
```

Paste that into SQL Query Analyzer and hit `CTRL+SHIFT+M`. A dialog to
replace the template parameters will pop up like so

![Replace Template Parameters
Dialog](http://haacked.com/images/ReplaceTemplateParameters.jpg)

Just fill in the values and hit return and you’re ready to run the
script.

The format for a template parameter is
**`<parameterName, sql data type, default value>`**.

