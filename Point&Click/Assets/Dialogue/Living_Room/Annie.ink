INCLUDE C:\Projects\AdventureGame\Point&Click\Assets\Dialogue\Globals.ink

Viola: “Hmpf, Who do you think you are, approaching me, The world renowned, Viola!”

+ [I am the protagonist in this story!]
->positive

+[Some call me the Stranger]

->Negative

+[Hi, I’m <Player>, Nice to meet you Viola!]

->Neutral



===positive===
Viola: My story has only Villains!
Viola: “Unless you know the way to the palace, You had better be on your way!”

+[Is that a Flute? Would you like to join my Band?.]
->Band

+[Have you tried following the Road?]
->Road
+[Goodbye Viola!]
->DONE

===Negative===

Viola: Eww, That is probably due to you needing to bathe.
Viola: “Unless you know the way to the palace, You had better be on your way!”
+[Is that a Flute? Would you like to join my Band?.]
->Band

+[Have you tried following the Road?]
->Road
+[Goodbye Viola!]
->DONE


===Neutral===
Viola: I don’t know why I asked, I don’t care.
Viola: “Unless you know the way to the palace, You had better be on your way!”
+[Is that a Flute? Would you like to join my Band?.]
->Band

+[Have you tried following the Road?]
->Road
+[Goodbye Viola!]
->DONE



===Band===
Only when pigs fly Ruffian!
->END

===Road===
Obviously! It took me over a rainbow!
->END







