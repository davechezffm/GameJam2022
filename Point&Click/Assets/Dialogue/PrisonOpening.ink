INCLUDE Globals.ink

Guard: Mr X. Please hand over any and all of your personal items.

{GoldWatch}
->GoldWatch

{GoldRing}
->GoldRing

{Wallet}
->Wallet


===GoldWatch===
Desk Guard: One Watch. Gold. 
~Watch = true
*{Watch} + *{Ring} + *{Photo}
->ADVANCE
->END

===GoldRing===
Desk Guard: One Ring. Gold.
~Ring = true
    -> END
    
    ===Wallet===
Desk Guard: One Wallet. Black Leather. 
+[Can I keep the photo?]
->Option1
+[You can't take that photo away from me]
->Option1
===Option1===
Desk Guard: You may keep the photo sir, but not the wallet.
~Photo = true
->END

===ADVANCE===


Desk Guard: Now please follow my colleague.
Guard: Right this way sir.
Cutscene1 = True
->END
