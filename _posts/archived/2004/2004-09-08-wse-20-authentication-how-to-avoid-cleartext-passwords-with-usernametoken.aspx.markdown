---
layout: post
title: 'WSE 2.0 Authentication: How To Avoid ClearText Passwords With UsernameToken'
date: 2004-09-08 -0800
comments: true
disqus_identifier: 1177
categories: []
redirect_from: "/archive/2004/09/07/wse-20-authentication-how-to-avoid-cleartext-passwords-with-usernametoken.aspx/"
---

[Aaron Skonnard](http://pluralsight.com/blogs/aaron/)
[mentions](http://pluralsight.com/blogs/aaron/archive/2004/07/03/1529.aspx)that

> When you take the custom authentication route and write a
> UsernameTokenManager (UTM), your implementation of AuthenticateToken
> must return the same secret (e.g., password) used on the client side
> to generate the hash/signature, depending on which option you use.

As he correctly points out, this makes security experts cringe and hide
under the bed (see Keith Brown's [cringing
response](http://pluralsight.com/blogs/keith/archive/2004/07/03/1532.aspx)
where he proposes a solution).

The big issue is that your UsernameTokenManager needs access to the
original cleartext password. But like any good security conscious
developer, you don't store passwords as cleartext, do you? (I sure hope
not. Bad security conscious developer. Bad!). Hopefully you do something
along the likes of what Keith suggests in his [MSDN
column](http://msdn.microsoft.com/msdnmag/issues/03/08/SecurityBriefs/).
For each user, he stores a randomly generated salt value and a hash of
the cleartext password combined with salt value. The salt value is
unique per user.

Keith points out that the secret returned by the AuthenticateToken
method doesn't have to be the actual cleartext password. It just has to
match the secret sent by the client. So if you store your passwords as
an SHA1 hash, your client just needs to hash the password before
creating the UsernameToken.

However, if you store your password as an SHA1 hash of the cleartext
password + salt value, you're going to have to do a little more work.
Your client isn't going to know the salt value for every user, so your
client needs a way to discover that. This may require calling a separate
web method just to query for the salt value given a user name. Service
clients would be required to store that value (probably on a "session"
basis) and use it when calling methods on the main web service.

Below is some sample code for doing just that. This assumes that user
passwords are stored as described in the aforementioned article using
salt and hash (no eggs, but do bring the ketchup). (My apologies for the
ugly formatting, I didn't want the code to be too wide)

```csharp
//Make an initial web service call to get the 
//the salt value for the user "haacked".  
//This should be stored by the client so its 
//not called for every method of our main service.
MyServiceWse proxy = new MyServiceWse();

//In order to get the salt value, a special account
//"saltAdmin" is used to call GetSalt().  This account
//only has access to this method.
//This also requires that the client app knows the;
//saltAdmin's salt value up front.
string adminPassword = GetAdminPassword(); 
//implementation not shown.

UsernameToken adminToken 
    = new UsernameToken("saltAdmin", adminPassword
                    , PasswordOption.SendHashed);

proxy.RequestSoapContext.Security.Tokens.Add(adminToken);
string username = "haacked";
string salt = proxy.GetSalt(username);
proxy.RequestSoapContext.Clear();

// Hash password and salt.
string pw = "Password"; //assume this came from the user.
SHA1CryptoServiceProvider hashProvider 
    = new SHA1CryptoServiceProvider();

byte[] inputBuffer = Encoding.Unicode.GetBytes(pw + salt);
byte[] result = hashProvider.ComputeHash(inputBuffer);
string hashedPassword = Convert.ToBase64String(result);
//Set up the user's token.
//Notice we the hashed password instead of the cleartext one.
UsernameToken token 
    = new UsernameToken(username, hashedPassword
                    , PasswordOption.SendHashed);

proxy.RequestSoapContext.Security.Tokens.Add(token);

//Make the actual service call.
proxy.SomeWebServiceMethod();
```

The AuthenitcateToken method of your custom UsernameTokenManager class
can now just return the hashed password value for the calling user from
your data store and everything will work just fine and security experts
can come out from under the bed.

