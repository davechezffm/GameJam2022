  
    INCLUDE C:\Projects\AdventureGame\Point&Click\Assets\Dialogue\Globals.ink



->main

===main===
Thanks for coming over with me tonight. My Grandad has gone away to London  and housesitting for him is kinda boring.
+ [What is more fun than visiting old people's homes?]
-> WeirdOption
+ [Don't worry about it. Great chance for us to watch some Stranger Things.]
-> NiceOption


===WeirdOption===
Depends who the old person is.I would love to go to David Attenborough's house. Jimmy Saville...not so much.
Anyhow, shall we watch season 1 of Stranger Things?
With Season 4 coming out I wouldnt mind going through all the episodes again.

+ [Why not. As long as you are free for 30 hours.]

->TV

===NiceOption===
Exactly! I need to rewatch all of them again before season 4 comes out. Though season 2 is kinda shitty so I am fine missing some of those episodes.
+ [So shall we put it on now?]

->TV

->END

===TV===
Sure, I will go get it setup, go to the kitchen and get some snacks will you.

~Cutscene2 = true

->END


