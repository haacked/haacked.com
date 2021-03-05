---
title: Route Around The Default Gateway On The Remote Network
tags: [tech]
redirect_from: "/archive/2007/01/26/Route_Around_The_Default_Gateway_On_The_Remote_Network.aspx/"
---

[Steve Harman](http://stevenharman.net/ "Steve Harman") digs in and
[solves a longtime
mystery](http://stevenharman.net/blog/archive/2007/01/26/VPN_Connections_and_Default_Gateways.aspx "VPN Connections and Default Gateways")
for me regarding VPN connections and default gateways on remote
networks.

Most clients we have require that we connect to their network via VPN.
Nothing new about that of course. But some clients require that we check
the *Use default gateway on remote network* option when setting up the
VPN.

That effectively shuts down my ability to access resources on my local
network as all traffic gets routed through the remote network.

![VPN Dialog. Shows the Use Default Gateway on Remote Network
Checkbox](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RouteAroundTheDefaultGatewayOnTheRemoteN_ADA/Vpn-Settings%5B14%5D.png)

Fortunately, Steve didn’t give up like I did. He persisted and,
following up on a tip by [Jon
Galloway](http://weblogs.asp.net/jgalloway/ "Jon Galloway"), figured out
[how to configure Routing
Tables](http://stevenharman.net/blog/archive/2007/01/26/VPN_Connections_and_Default_Gateways.aspx "VPN Connections and Default Gateways")
to achieve what he needed.

This so beats my twine and wire MacGuyver solution of simply setting up
Remote Desktop to another machine that is then connected to the VPN.

With persistence and problem solving skills like that, a company would
be lucky to hire Steve. So we did this past month!

