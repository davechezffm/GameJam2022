INCLUDE C:\Projects\AdventureGame\Point&Click\Assets\Dialogue\Globals.ink


*{Water}
->GotItem

*{not Water}
-> main 

===main===
Bob: Well Kelly, I sure am thirsty. What drinks do you have here, aynthing good?

+ [How about a beer and lets get shitfaced?]
-> BeerOption
+ [Errr I have some coffee if you fancy it.]
-> Coffee


===BeerOption===
Bob: Hell yeah that sounds good. Go get me a beer while I stand here and wait
{Water}

->DONE

->END

===Coffee===
Fuck that shit. LETS PARTY. SPRING BREAK BITCHES.
Go get me a beer and lets do this shit!
->END

===GotItem===
What are you waiting for, pass me the water!

->END




