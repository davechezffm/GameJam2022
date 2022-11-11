INCLUDE Globals.ink

*{Water}
->GotItem

*{not Water}
-> main 

===main===
Can't beat a nice bottle of water can you? Feel free to take it.

+ [Who are you?]
-> WhoAmI

+ [How can I get that big Guy to give me a beer?]
-> HowToGetABeer

-> Bye

===Bye===
->END

===WhoAmI===
Just a solider in charge of guarding the water, not much point really anyone is free to take it.
+[Understood]
-> AnythingElse

===HowToGetABeer===
Hmmm, I think if you scare him he will run away and drop it.

+[Understood]
->AnythingElse

===AnythingElse===

Anything else?

+ [Who are you?]
-> WhoAmI

+ [How can I get that big Guy to give me a beer?]
-> HowToGetABeer

+[Bye]

See you around.

->END

===GotItem===
Enjoy the water!

->END


