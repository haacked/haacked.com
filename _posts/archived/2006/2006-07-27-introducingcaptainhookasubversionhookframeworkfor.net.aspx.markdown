---
title: Introducing CaptainHook - A Subversion Hook Framework For .NET
tags: [source-control]
redirect_from: "/archive/2006/07/26/introducingcaptainhookasubversionhookframeworkfor.net.aspx/"
---

UPDATE: CaptainHook is now an [Open Source project on
SourceForge](https://haacked.com/archive/2006/07/31/CaptainHookIsOnSourceForge.aspx "CaptainHook on SourceForge").

![Hook Logo](https://haacked.com/assets/images/captainhook.gif) One potent tool
for team communications on a project, especially one with distributed
developers, is the simple commit email. Setting up Subversion to send
out an email when a developer commits changes to the repository is
fairly easy. The Subversion distribution comes with a PERL script
([commit-email.pl](http://svn.collab.net/repos/svn/trunk/tools/hook-scripts/commit-email.pl.in "Commit Email Perl Script"))
that works quite well for this purpose.

At least it did for me until we changed mail servers to one that did not
allow SMTP relay. Try as I might, I could not get the script to
authenticate with our SMTP credentials. I downloaded various other PERL
modules that were supposed to be able to authenticate with no luck. I
read the [RFC 2554](http://www.faqs.org/rfcs/rfc2554.html "RFC") (seat
of your pants reading) and authenticated manually via telnet and
compared that to the SMTP logs for the components and realized that for
some reasons, these scripts were doing the wrong thing.

That’s when it occurred to me that I could probably write a simple .NET
app in a minute that could send out the commit email. As I got started
though, I realized that it might be nice to write something that would
allow others to easily handle other Subversion hooks. Hence CaptainHook
was born.

Hooks in Subversion are scripts (or executables) that are triggered by
an event in the subversion version control life cycle. The following are
the five hooks supported by Subversion. For a short discussion on how to
install the hooks, read [this post by Pete
Freitag](http://www.petefreitag.com/item/244.cfm "Using Subversion Hooks to send out build emails").

Note that (except for `post-commit` and `post-revprop-change`) the
return value of the script controls whether or not the commit should
continue. If the script returns 0, the commit continues. If it returns
anything other than 0, the commit is stopped.

<table>
    <tbody>
        <tr>
            <th>Script Name</th>
            <th>Triggered By</th>
            <th>Arguments</th>
            <th>Notes</th>
        </tr>
        <tr>
            <td><code>start-commit</code></td>
            <td>Before the commit transaction starts</td>
            <td>Repository Path and username</td>
            <td>Can be used for repository access control.</td>
        </tr>
        <tr>
            <td><code>pre-commit</code></td>
            <td>After the commit transaction starts but before the transaction is commited</td>
            <td>Repository Path and transaction name</td>
            <td>Often used to validate a commit such as checking for a non-empty log message.</td>
        </tr>
        <tr>
            <td><code>post-commit</code></td>
            <td>After the commit transaction completes</td>
            <td>Repository Path and the revision number of the commit</td>
            <td>Can be used to send emails or backup repository.</td>
        </tr>
        <tr>
            <td><code>pre-revprop-change</code></td>
            <td>Before a revision property is changed</td>
            <td>Repository Path, Revision, Username, name of the property</td>
            <td>Revision Property’s new value is passed into standard input.  Can be used to check permission.</td>
        </tr>
        <tr>
            <td><code>post-revprop-change</code></td>
            <td>After a revision property is changed</td>
            <td>Repository Path, Revision, Username, name of the property</td>
            <td>Can be used to email or backup these changes.</td>
        </tr>
    </tbody>
</table>

My goal was to provide a nice strongly typed interface and a few useful
service methods for accessing Subversion. Thus handling a Subversion
hook is as easy as implementing an abstract base class and calling
methods on a Subversion wrapper interface.

### Setup

To setup CaptainHook, simply unzip the exe file and its related
assemblies into the `hooks` folder on your Subversion server. The
distribution includes a `plugins` directory with a single plugin already
there. The plugins directory is where CaptainHook looks for other hook
handlers. Be sure to update the config file with settings that match
your environment.

The next step is to rename the .tmpl file for the hook events you wish
CaptainHook to handle. CaptainHook comes with some sample batch files
you can use instead, one for each hook. Just copy the ones you want to
use from the `SampleBatchFiles` directory into the `hooks` directory.

### Flow

Now when a commit occurs, Subversion will call the `post-commit.bat`
file which in turn calls CaptainHook with the `post-commit` flag. This
flag indicates to CaptainHook which plugins to load. CaptainHook then
looks in the `plugins` directory for any assemblies with types that
implement the `PostCommitHook` abstract class. It then instantiates
instances of these types and calls the `Initialize` method and then the
`HandleHook` method.

Note that for the time being, CaptainHook is an executable so it has to
incur the cost of searching the plugin assemblies every time which may
seem like overkill (and it is). However my focus was on the *model* for
CaptainHook. At some point it will evolve to use remoting as a [Server
Activated
Singleton](http://www.code-magazine.com/article.aspx?quickid=0301091&page=4 "Remote Object Models")
or it may become a Windows Service which fit the model better. Either
way, there are ways in which we can incur this cost only once. But for
now, this will do and performs well enough.

### Assemblies and Classes

Captain hook contains serveral assemblies.

<table>
    <tbody>
        <tr>
            <th>Assembly</th>
            <th>Purpose</th>
        </tr>
        <tr>
            <td>CaptainHook</td>
            <td>This is the main console exe and is the starting point for the application.</td>
        </tr>
        <tr>
            <td>CaptainHook.Interfaces</td>
            <td>Contains the interface definitions.  This is the only assembly you need to reference when writing a plugin.</td>
        </tr>
        <tr>
            <td>CaptainHook.SubversionWrapper</td>
            <td>This is potentially useful as a stand-alone library.  It includes the <code>SubversionRepository</code> class which allows running commands against Subversion and receiving the output as a string.  This is useful for running straight commands against Subversion. This assembly also includes the <code>SubversionTranslator</code> class.  This class wraps the <code>SubversionRepository</code> class and provides an object oriented means to calling the Subversion commands.</td>
        </tr>
        <tr>
            <td>Velocit.Hook.Plugins</td>
            <td>Contains the <code>PostCommitEmailHook</code> plugin that started this whole ordeal as well as the <code>RequireLogMessageHook</code> which is a <code>pre-commit</code> hook that demonstrates how to reject a commit if no log message is specified.</td>
        </tr>
    </tbody>
</table>

Let me know if you find this useful. It is definitely a work in progress
as not every command is implemented in the `SubversionTranslator` class.
If it turns out that several people find this useful and want to
contribute to the code, I am willing to put the code on
[SourceForge](http://sourceforge.net/ "SourceForge").

[Download
CaptainHook](http://tools.veloc-it.com/tabid/58/grm2id/5/Default.aspx "Captain Hook")
from my company’s [tools site](http://tools.veloc-it.com/ "Tools").

The zip archive contains both the source code and the binaries. I also
compiled an x64 version of the exe for you 64bit kids.

