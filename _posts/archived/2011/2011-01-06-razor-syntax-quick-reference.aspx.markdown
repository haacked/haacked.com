---
title: C# Razor Syntax Quick Reference
date: 2011-01-06 -0800
tags:
- aspnetmvc
- aspnet
- code
redirect_from: "/archive/2011/01/05/razor-syntax-quick-reference.aspx/"
---

I gave a presentation to another team at Microsoft yesterday on ASP.NET MVC and the Razor view engine and someone asked if there was a reference for the Razor syntax.

It turns out, there is a pretty [good guide about Razor](http://www.asp.net/webmatrix/tutorials/2-introduction-to-asp-net-web-programming-using-the-razor-syntax "Razor Guide") available, but it’s focused on covering the basics of web programming using Razor and inline pages and not just the Razor syntax.

So I thought it might be handy to write up a a really concise quick reference about the Razor syntax.

<table class="matrix"><tbody>
    <tr>
        <th valign="top" width="183">Syntax/Sample</th>
        <th valign="top" width="149">Razor</th>
        <th valign="top">Web Forms Equivalent (or remarks)</th>
    </tr>
    <tr>
        <td valign="top" width="183">Code Block</td>
        <td valign="top" width="149"><pre class="csharpcode"><span class="asp">@{</span> 
  <span class="rzr"><span class="kd">int</span> x = 123;</span> 
  <span class="rzr"><span class="kd">string</span> y = <span class="s">"because."</span>;</span>
<span class="asp">}</span></pre>
      </td>

      <td valign="top">
        <pre class="csharpcode"><span class="asp">&lt;%</span>
  <span class="kd">int</span> x = 123; 
  <span class="kd">string</span> y = <span class="s">"because."</span>; 
<span class="asp">%&gt;</span>
      </pre>
      </td>
    </tr>

    <tr>
      <td valign="top" width="183">Expression (Html Encoded)</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">span</span><span class="kd">&gt;</span><span class="asp">@</span><span class="rzr">model.Message</span><span class="kd">&lt;/</span><span class="html">span</span><span class="kd">&gt;</span></pre>
      </td>

      <td valign="top">
        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">span</span><span class="kd">&gt;</span><span class="asp">&lt;%</span>: model.Message <span class="asp">%&gt;</span><span class="kd">&lt;/</span><span class="html">span</span><span class="kd">&gt;</span></pre>
      </td>
    </tr>

    <tr>
      <td valign="top" width="183">Expression (Unencoded)</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">span</span><span class="kd">&gt;
</span><span class="asp">@</span><span class="rzr">Html.Raw(model.Message)</span>
<span class="kd">&lt;/</span><span class="html">span</span><span class="kd">&gt;</span></pre>
      </td>

      <td valign="top">
        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">span</span><span class="kd">&gt;</span><span class="asp">&lt;%</span>= model.Message <span class="asp">%&gt;</span><span class="kd">&lt;/</span><span class="html">span</span><span class="kd">&gt;</span></pre>
      </td>
    </tr>

    <tr>
      <td valign="top" width="183">Combining Text and markup</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="asp">@</span><span class="rzr"><span class="kd">foreach</span>(var item <span class="kd">in</span> items) {</span>
  <span class="kd">&lt;</span><span class="html">span</span><span class="kd">&gt;</span><span class="asp">@</span><span class="rzr">item.Prop</span><span class="kd">&lt;/</span><span class="html">span</span><span class="kd">&gt;</span> 
<span class="rzr">}</span></pre>
      </td>

      <td valign="top">
        <pre class="csharpcode"><span class="asp">&lt;%</span> <span class="kd">foreach</span>(var item <span class="kd">in</span> items) { <span class="asp">%&gt;</span>
  <span class="kd">&lt;</span><span class="html">span</span><span class="kd">&gt;</span>&lt;%: item.Prop %&gt;<span class="kd">&lt;/</span><span class="html">span</span><span class="kd">&gt;</span>
<span class="asp">&lt;%</span> } <span class="asp">%&gt;</span></pre>
      </td>
    </tr>

    <tr>
      <td valign="top" width="183">Mixing code and Plain text</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="asp">@</span><span class="rzr"><span class="kd">if</span> (foo) {</span>
  <span class="kd">&lt;</span><span class="html">text</span><span class="kd">&gt;</span>Plain Text<span class="kd">&lt;/</span><span class="html">text</span><span class="kd">&gt;</span> 
<span class="rzr">}</span></pre>
      </td>

      <td valign="top">
        <pre class="csharpcode"><span class="asp">&lt;%</span> <span class="kd">if</span> (foo) { <span class="asp">%&gt;</span> 
  Plain Text 
<span class="asp">&lt;%</span> } <span class="asp">%&gt;</span></pre>
      </td>
    </tr>

    <tr>
      <td valign="top" width="183">Using block</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="asp">@</span><span class="kd">using</span> (Html.BeginForm()) {
  <span class="kd">&lt;</span><span class="html">input</span> <span class="na">type</span><span class="kd">="text"</span> <span class="na">value</span><span class="kd">="input here"</span><span class="kd">&gt;</span>
}</pre>
      </td>

      <td valign="top">
        <pre class="csharpcode"><span class="asp">&lt;%</span> <span class="kd">using</span> (Html.BeginForm()) { <span class="asp">%&gt;</span>
  <span class="kd">&lt;</span><span class="html">input</span> <span class="na">type</span><span class="kd">="text"</span> <span class="na">value</span><span class="kd">="input here"</span><span class="kd">&gt;</span>
<span class="asp">&lt;%</span> } <span class="asp">%&gt;</span></pre>
      </td>
    </tr>

    <tr>
      <td valign="top" width="183">Mixing code and plain text (alternate)</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="asp">@</span><span class="rzr"><span class="kd">if</span> (foo) {</span>
  <span class="asp">@:</span>Plain Text is <span class="asp">@</span><span class="rzr">bar</span>
<span class="rzr">}</span></pre>
      </td>

      <td valign="top">Same as above</td>
    </tr>

    <tr>
      <td valign="top" width="183">Email Addresses</td>

      <td valign="top" width="149">
        <pre class="csharpcode">Hi philha@example.com</pre>
      </td>

      <td valign="top">Razor recognizes basic email format and is smart enough not to treat the @ as a code delimiter</td>
    </tr>

    <tr>
      <td valign="top" width="183">Explicit Expression</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">span</span><span class="kd">&gt;</span>ISBN<span class="asp">@(</span><span class="rzr">isbnNumber</span><span class="asp">)</span><span class="kd">&lt;/</span><span class="html">span</span><span class="kd">&gt;</span></pre>
      </td>

      <td valign="top">In this case, we need to be explicit about the expression by using parentheses.</td>
    </tr>

    <tr>
      <td valign="top" width="183">Escaping the @ sign</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">span</span><span class="kd">&gt;</span>In Razor, you use the 
@@foo to display the value 
of foo<span class="kd">&lt;/</span><span class="html">span</span><span class="kd">&gt;</span></pre>
      </td>

      <td valign="top">@@ renders a single @ in the response.</td>
    </tr>

    <tr>
      <td valign="top" width="183">Server side Comment</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="asp">@*</span>
<span class="rem">This is a server side 
multiline comment </span>
<span class="asp">*@</span></pre>
      </td>

      <td valign="top">
        <pre class="csharpcode"><span class="asp">&lt;%</span><span class="rem">--
This is a server side 
multiline comment
--</span><span class="asp">%&gt;</span></pre>
      </td>
    </tr>

    <tr>
      <td valign="top" width="183">Calling generic method</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="asp">@(</span>MyClass.MyMethod&lt;AType&gt;()<span class="asp">)</span></pre>
      </td>

      <td valign="top">Use parentheses to be explicit about what the expression is.</td>
    </tr>

    <tr>
      <td valign="top" width="183">Creating a Razor Delegate</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="asp">@{</span>
  Func&lt;dynamic, <span class="kd">object</span>&gt; b = 
   <span class="asp">@</span><span class="kd">&lt;</span><span class="html">strong</span><span class="kd">&gt;</span><span class="asp">@</span>item<span class="kd">&lt;/</span><span class="html">strong</span><span class="kd">&gt;</span>;
<span class="asp">}</span>
<span class="asp">@</span>b("Bold this")</pre>
      </td>

      <td valign="top">Generates a <code>Func&lt;T, HelperResult&gt;</code> that you can call from within Razor. See <a title="Templated Razor Delegates" href="https://haacked.com/archive/2011/02/27/templated-razor-delegates.aspx">this blog post</a> for more details.</td>
    </tr>

    <tr>
      <td valign="top" width="183">Mixing expressions and text</td>

      <td valign="top" width="149">
        <pre class="csharpcode">Hello <span class="asp">@</span>title. <span class="asp">@</span>name.</pre>
      </td>

      <td valign="top">
        <pre class="csharpcode">Hello <span class="asp">&lt;%</span>: title <span class="asp">%&gt;</span>. <span class="asp">&lt;%</span>: name <span class="asp">%&gt;</span>.</pre>
      </td>
    </tr>

    <tr>
      <td colspan="3"><strong><em>NEW IN RAZOR v2.0/ASP.NET MVC 4</em></strong></td>
    </tr>

    <tr>
      <td valign="top" width="183">Conditional attributes</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">div</span> <span class="na">class</span><span class="kd">="</span><span class="asp">@</span>className"<span class="kd">&gt;&lt;/</span><span class="html">div</span><span class="kd">&gt;</span></pre>
      </td>

      <td valign="top">When <code>className = null</code> 

        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">div</span><span class="kd">&gt;&lt;/</span><span class="html">div</span><span class="kd">&gt;</span></pre>
When <code>className = ""</code> 

        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">div</span> <span class="na">class</span><span class="kd">="</span>"<span class="kd">&gt;&lt;/</span><span class="html">div</span><span class="kd">&gt;</span></pre>
When <code>className = "my-class"</code> 

        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">div</span> <span class="na">class</span><span class="kd">="</span>my-class"<span class="kd">&gt;&lt;/</span><span class="html">div</span><span class="kd">&gt;</span></pre>
      </td>
    </tr>

    <tr>
      <td valign="top" width="183">Conditional attributes with other literal values</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">div</span> <span class="na">class</span><span class="kd">="</span><span class="asp">@</span>className foo bar"<span class="kd">&gt;
&lt;/</span><span class="html">div</span><span class="kd">&gt;</span></pre>
      </td>

      <td valign="top">When <code>className = null</code> 

        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">div</span> <span class="na">class</span><span class="kd">="</span>foo bar"<span class="kd">&gt;&lt;/</span><span class="html">div</span><span class="kd">&gt;</span></pre>
        <em>Notice the leading space in front of </em><code>foo</code><em> is removed.</em> 

        <br />When <code>className = "my-class"</code> 

        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">div</span> <span class="na">class</span><span class="kd">="</span>my-class foo bar"<span class="kd">&gt;
&lt;/</span><span class="html">div</span><span class="kd">&gt;</span></pre>
      </td>
    </tr>

    <tr>
      <td valign="top" width="183">Conditional data-* attributes.
        <br />

        <br /><em>data-* attributes are always rendered</em>.</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">div</span> <span class="na">data-x</span><span class="kd">="</span><span class="asp">@</span>xpos"<span class="kd">&gt;&lt;/</span><span class="html">div</span><span class="kd">&gt;</span></pre>
      </td>

      <td valign="top">When <code>xpos = null or ""</code> 

        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">div</span> <span class="na">data-x</span><span class="kd">="</span>"<span class="kd">&gt;&lt;/</span><span class="html">div</span><span class="kd">&gt;</span></pre>
When <code>xpos = "42"</code> 

        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">div</span> <font color="#ff0000">data-x</font><span class="kd">="</span>42"<span class="kd">&gt;&lt;/</span><span class="html">div</span><span class="kd">&gt;</span></pre>
      </td>
    </tr>

    <tr>
      <td valign="top" width="183">Boolean attributes</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">input</span> <span class="na">type</span><span class="kd">="checkbox"</span>
  <span class="na">checked</span><span class="kd">="</span><span class="asp">@</span>isChecked" <span class="kd">/&gt;</span></pre>
      </td>

      <td valign="top"><code>When isChecked = true</code> 

        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">input</span> <span class="na">type</span><span class="kd">="checkbox"</span>
  <span class="na">checked</span><span class="kd">="</span>checked" <span class="kd">/&gt;</span></pre>
        <code>When isChecked = false</code> 

        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">input</span> <span class="na">type</span><span class="kd">="checkbox"</span> <span class="kd">/&gt;</span></pre>
      </td>
    </tr>

    <tr>
      <td valign="top" width="183">URL Resolution with tilde</td>

      <td valign="top" width="149">
        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">script</span> <span class="na">src</span><span class="kd">="~/myscript.js"</span><span class="kd">&gt;<br />&lt;/</span><span class="html">script</span><span class="kd">&gt;</span></pre>
      </td>

      <td valign="top">When the app is at <code>/</code> 

        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">script</span> <span class="na">src</span><span class="kd">="/myscript.js"</span><span class="kd">&gt;<br />&lt;/</span><span class="html">script</span><span class="kd">&gt;</span></pre>
When running in a virtual application named <code>MyApp</code> 

        <pre class="csharpcode"><span class="kd">&lt;</span><span class="html">script</span> <span class="na">src</span><span class="kd">="/MyApp/myscript.js"</span><span class="kd">&gt;<br />&lt;/</span><span class="html">script</span><span class="kd">&gt;</span></pre>
      </td>
    </tr>
  </tbody>
</table>

Notice in the “mixing expressions and text” example that Razor is smart enough to know that the ending period is a literal text punctuation and not meant to indicate that it’s trying to call a method or property of the expression.

Let me know if there are other examples you think should be placed in this guide. I hope you find this helpful.

**UPDATE 12/30/2012:** I’ve added a few new examples to the table of new additions to Razor v2/ASP.NET MVC 4 syntax. Razor got a lot better in that release!

Also, if you want to know more, consider buying the [Programming ASP.NET MVC 4](http://www.amazon.com/gp/product/111834846X/ref=as_li_ss_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=111834846X&linkCode=as2&tag=youvebeenhaac-20) book. Full disclosure, I'm one of the authors, but the other three authors are way better.
