---
layout: post
title: Better CAPTCHA Through Encryption
date: 2006-10-02 -0800
comments: true
disqus_identifier: 17568
categories:
- code
- blogging
redirect_from: "/archive/2006/10/01/Better_CAPTCHA_Through_Encryption.aspx/"
---

I recently wrote about a [lightweight invisible CAPTCHA validator
control](http://haacked.com/archive/2006/09/26/Lightweight_Invisible_CAPTCHA_Validator_Control.aspx "Lightweight Invisible CAPTCHA")
I built as a defensive measure against comment spam.  I wanted the
control to work in as many situations as possible, so it doesn’t rely on
`ViewState` nor `Session` since some users of the control may want to
turn those things off.

Of course this raises the question, **how do I know the answer submitted
in the form is the answer to the question I asked?**  Remember, never
trust your inputs, even form submissions can easily be tampered with.

Well one way is to give the client the answer in some manner that it
can’t be read and can’t be tampered with.  Encryption to the rescue!

Using a few new objects from the `System.Security.Cryptography`
namespace in .NET 2.0, I quickly put together code that would encrypt
the answer along with the current system time into a base 64 encoded
string.  That string would then be placed in a hidden input field.

When the form is submitted, I made sure that the encrypted value
contained the answer and that the date inside was not too old, thus
defeating replay attacks.

The first change was to initialize the encryption algorithm via a static
constructor.

*The code can be hard to read in a browser, so I did include the source
code in the download link at the end of this post.*

```csharp
static SymmetricAlgorithm encryptionAlgorithm 
    = InitializeEncryptionAlgorithm();

static SymmetricAlgorithm InitializeEncryptionAlgorithm()
{
  SymmetricAlgorithm rijaendel = RijndaelManaged.Create();
  rijaendel.GenerateKey();
  rijaendel.GenerateIV();
  return rijaendel;
}
```

With that in place, I added a couple static methods to the control.

```csharp
static SymmetricAlgorithm InitializeEncryptionAlgorithm()
{
  SymmetricAlgorithm rijaendel = RijndaelManaged.Create();
  rijaendel.GenerateKey();
  rijaendel.GenerateIV();
  return rijaendel;
}

public static string EncryptString(string clearText)
{
  byte[] clearTextBytes = Encoding.UTF8.GetBytes(clearText);
  byte[] encrypted = encryptionAlgorithm.CreateEncryptor()
    .TransformFinalBlock(clearTextBytes, 0
    , clearTextBytes.Length);
  return Convert.ToBase64String(encrypted);
}
```

In the PreRender method I simply took the answer, appended the date
using a pipe character as a separator, encrypted the whole stew, and the
slapped it in a hidden form field.

```csharp
//Inside of OnPreRender
Page.ClientScript.RegisterHiddenField
    (this.HiddenEncryptedAnswerFieldName
    , EncryptAnswer(answer));

string EncryptAnswer(string answer)
{
  return EncryptString(answer 
    + "|" 
    + DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
}
```

Now with all that in place, when the user submits the form, I can
determine if the answer is valid by grabbing the value from the form
field, calling decrypt on it, splitting it using the pipe character as a
delimiter, and examining the result.

```csharp
protected override bool EvaluateIsValid()
{
  string answer = GetClientSpecifiedAnswer();
    
  string encryptedAnswerFromForm = 
    Page.Request.Form[this.HiddenEncryptedAnswerFieldName];
    
  if(String.IsNullOrEmpty(encryptedAnswerFromForm))
    return false;
    
  string decryptedAnswer = DecryptString(encryptedAnswerFromForm);
    
  string[] answerParts = decryptedAnswer.Split('|');
  if(answerParts.Length < 2)
    return false;
    
  string expectedAnswer = answerParts[0];
  DateTime date = DateTime.ParseExact(answerParts[1]
    , "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
  if ((DateTime.Now - date).Minutes > 30)
  {
    this.ErrorMessage = "Sorry, but this form has expired. 
      Please submit again.";
    return false;
  }

  return !String.IsNullOrEmpty(answer) 
    && answer == expectedAnswer;
}

// Gets the answer from the client, whether entered by 
// javascript or by the user.
private string GetClientSpecifiedAnswer()
{
  string answer = Page.Request.Form[this.HiddenAnswerFieldName];
  if(String.IsNullOrEmpty(answer))
    answer = Page.Request.Form[this.VisibleAnswerFieldName];
  return answer;
}
```

This technique could work particularly well for a **visible** CAPTCHA
control as well. The request for a CAPTCHA image is an asynchronous
request and the code that renders that image has to know which CAPTCHA
image to render. Implementations I’ve seen simply store an image in the
CACHE using a GUID as a key when rendering the control. Thus when the
asynchronous request to grab the CAPTCHA image arrives, the CAPTCHA
image rendering `HttpHandler` looks up the image using the GUID and
renders that baby out.

Using encryption, the URL for the CAPTCHA image could embed the answer
(aka the word to render).

If you are interested, you can download an updated binary and
source code for the Invisible CAPTCHA control which now includes the
symmetric encryption [from
here](http://tools.veloc-it.com/tabid/58/grm2id/14/Default.aspx "Invisible CAPTCHA Control").



