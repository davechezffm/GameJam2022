INCLUDE Globals.ink
Ello’ Stranger, Odd to see wanderers’ about these parts”

+ [Do you see anyone about these parts?]
-> Parts

+ [My name is John, Not Stranger.]
-> John

+ [Can I get a drink barman?]
-> Drink



===Parts===
No, actually, you’re the first in months.
-> Items

===John===
Beggin’ your Pardon John but you are mighty strange.
-> Items


===Drink===
Unfortunately the taps have been dry for a season, I can offer you some assorted wares though.
-> Items


===Items===
I should charge for these but I’ve no use for coin in these parts.

+ [No beer and no money, Where the hell am I?]
-> Where

+ [You can't charge for this junk!]
-> Junk

+ [Where should I go next?]
-> GoNext



===Where===
One of them for sure. Not sure which one. Now beat it stranger.
->END


===Junk===
I can! And I will next time! Now scoot!
->END

===GoNext===
Out of my Tavern would be a good start.
->END








