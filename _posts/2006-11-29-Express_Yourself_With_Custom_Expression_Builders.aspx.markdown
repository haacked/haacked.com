---
layout: post
title: "Express Yourself With Custom Expression Builders"
date: 2006-11-29 -0800
comments: true
disqus_identifier: 18152
categories: []
---
One of the hidden gems in ASP.NET 2.0 is the new expression syntax. For
example, to display the value of a setting in the AppSettings section of
your web.config, you can do this:

```csharp
<asp:Label Text="<%$ AppSettings:AnotherSetting %>"
    ID="setting" 
    runat="server" />
```

Notice that the value of the `Text` property of the `Label` control is
set to an expression that is similar to the DataBinding syntax (\<%\#),
but instead of a pound sign (\#) it uses a dollar sign (\$).

Expressions are distinguished by the expression prefix. In the above
example, the prefix is **AppSettings**.  The following is a short list
of built in expression prefixes you can use. I am not sure if there are
more:

-   Resources
-   ConnectionStrings
-   AppSettings

**But like most things in ASP.NET, this system is extensible, allowing
you to easily build your own custom expressions.** In this blog post,
I’ll walk you through building a query string expression builder. This
will allow you to display a query string value like so:

```csharp
<asp:Label Text="<%$ QueryString:SomeParamName %>"
    ID="setting" 
    runat="server" />
```

The first step is to create a class that inherits from
`System.Web.Compilation.ExpressionBuilder`. Be sure not to confuse this
with `System.Web.Configuration.ExpressionBuilder`

```csharp
using System.Web.Compilation;

[ExpressionPrefix("QueryString")]
public class QueryStringExpressionBuilder : ExpressionBuilder
{
  //Implementation goes here...
}
```

ExpressionBuilder is an abstract class with a single abstract method to
implement. This method returns an instance of `CodeExpression` which is
part of the `System.CodeDom` namespace. For those not familiar with
CodeDom, it’s short for Code Document Object Model. It is an API for
automatic code generation. The `CodeExpression` class is an abstract
representation of code that gets executed each time your custom
expression is evaluated.

You’ll probably use something similar to the following implementation
99% of the time though (sorry for the ugly formatting, but this pretty
much mimics the implementation in the [MSDN
documentation](http://msdn2.microsoft.com/en-US/library/system.web.compilation.expressionbuilder.getcodeexpression.aspx "MSDN Documentation on GetCodeExpression")).

```csharp
public override CodeExpression GetCodeExpression(
    BoundPropertyEntry entry
    , object parsedData
    , ExpressionBuilderContext context)
{
  Type type = entry.DeclaringType;
  PropertyDescriptor descriptor = 
    TypeDescriptor.GetProperties(type)
      [entry.PropertyInfo.Name];
  CodeExpression[] expressionArray = 
    new CodeExpression[3];
  expressionArray[0] = new 
    CodePrimitiveExpression(entry.Expression.Trim());
  expressionArray[1] = new 
    CodeTypeOfExpression(type);
  expressionArray[2] = new 
    CodePrimitiveExpression(entry.Name);

  return new CodeCastExpression(descriptor.PropertyType
    , new CodeMethodInvokeExpression(
        new CodeTypeReferenceExpression(GetType())
        , "GetEvalData"
       , expressionArray));
}
```

So what exactly is happening in this method? It is effectively
generating code. In particular, it generates a call to a static method
named `GetEvalData` which needs to be defined in this class. The return
value of this method is then cast to the type returned by
`descriptor.PropertyType`, which is why you see the `CodeCastExpression`
wrapping the other code expressions.

The arguments passed to `GetEvalData` are represented by the
`CodeExpression` array, `expressionArray`. The first argument is the
**expression** to evaluate (this is the the part after the prefix). The
second argument is the **target** type. This is the type of the class in
which the expression is being evaluated. In our case, this would be the
type `System.Web.UI.WebControls.Label` as we are using this expression
within a `Label` control. The final argument is **entry**. This is the
name of the property being set using the expression. In our case, this
would be the `Text` property of the `Label`.

You could really build any sort of code tree within this method, but as
I said before, most of the time, you will follow a similar pattern as
this. In fact, I would probably put this method in some sort of abstract
base class and then make sure to define the static `GetEvalData` method
in your inheriting class.

*Note, if you choose to move this method into an abstract base class as
I described, you can’t make `GetEvalData` an abstract method in that
class because we generated a call to a static method.*

*You could consider changing the above method to build a call to an
instance method, but then you the generate code would have to create the
instance everytime your expression is evaluated. It would not have
access to an instance of the expression builder automatically. The
choice is yours.*

Here is the `GetEvalData` method we need to add to
`QueryStringExpressionBuilder`.

```csharp
public static object GetEvalData(string expression
    , Type target, string entry)
{
    if (HttpContext.Current == null 
      || HttpContext.Current.Request == null)
        return string.Empty;

    return HttpContext.Current
      .Request.QueryString[expression];
}
```

With the code for the builder completed, you simply need to add an entry
within the compilation section under the `system.web` section of
`web.config` like so:

```csharp
<system.web>
  <compilation debug="true">
    <expressionBuilders>
      <add expressionPrefix="QueryString" 
        type="NS.QueryStringExpressionBuilder, AssemblyName"/>
    </expressionBuilders>
  </compilation>
</system.web>
```

This maps your custom expression class to the expression via its prefix.

In the MSDN examples, they tell you to drop your expression class file
into the `App_Code` directory. This works when you are using the Website
Project model. **Fortunately, you can also use custom expressions with
Web Application Projects.** Simply compile your builder into an assembly
and make sure to specify the *AssemblyName* as part of the *type*
attribute when declaring your expression builder.

If you are using the WebSite project model and the `App_Code` directory,
you should leave off the *AssemblyName* portion of the *type*.



