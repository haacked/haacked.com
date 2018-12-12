---
title: 'AddressInfo.cs: Converting State Codes to State Names and Vice Versa'
date: 2005-04-08 -0800
disqus_identifier: 2599
categories: []
redirect_from: "/archive/2005/04/07/addressinfocs-converting-state-codes-to-state-names-and-vice-versa.aspx/"
---

I'm feeling a little uninspired to write anything interesting because my
hands are hurting from the keyboard pounding I've been doing. Instead, I
thought I'd dig up this really simple (and hopefully useful) AddressInfo
class for you. The code is extremely basic, but it might save you the
hassle of typing all fifty states (and some territories and the District
of Columbia) and their two letter postal codes into your own class
because I did it for you! Free of charge!

If I save one hand out there, my work here is done. The basic premise is
this, the U.S. isn't adding states to the union very often, so it makes
sense to have the list of states and state codes as an enumeration
rather than a lookup table in the database. Makes it easier to re-use
that information not to mention the speed. For example, here's a snippet
of one of the StateCode enum available in the class.

public enum StateCode

{

    /// \<summary\>Alabama\</summary\>

    AL,

    /// \<summary\>Alaska\</summary\>

    AK,

    /// \<summary\>American Samoa\</summary\>

    AS,

    //,... Bunch of other states ...,

    /// \<summary\>Wyoming\</summary\>

    WY

}

And here's a snippet of the corresponding State enum.

public enum State

{

    /// \<summary\>AL\</summary\>

    Alabama,

    /// \<summary\>AK (Home Sweet Home)\</summary\>

    Alaska,

    /// \<summary\>AS\</summary\>

    American\_Samoa,

 

    ///,... Bunch of other states...,

 

    /// \<summary\>WY\</summary\>

    Wyoming

}

The main AddressInfo class is used to hold an address, but isn't all
that useful nor interesting. The interesting methods are the static
methods used to convert from state codes to states and vice versa.
Here's a couple examples of how you might use these methods:

// Get the state name based on the state code.

string stateName = AddressInfo.GetState(StateCode.AK);

Console.WriteLine(stateName); // Prints "Alaska"

 

// Get the state name based on the state code string

string stateCodeText = "CA";

StateCode stateCode = AddressInfo.ParseStateCode(stateCodeText);

State state = AddressInfo.Convert(stateCode);

Console.WriteLine(state.ToString()); // Prints "California"

Let me know if you actually find this useful. The class itself can be
downloaded [here](https://haacked.com/code/AddressInfo.zip).

[Listening to: Psychedeliasmith / Give Me My Auger Back - Fat Boy Slim -
On The Floor At The Boutique (4:21)]

