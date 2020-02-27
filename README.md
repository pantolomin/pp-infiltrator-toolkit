# pp-infiltrator-toolkit

Links the class icon's color to the actor's current state.

For Phoenix Project:
* a yellow class icon means that the enemy has "located" you.
* an orange icon means that an enemy has "revealed" you.

For the enemy: a purple icon means that they are alerted.

The crossbows are now silent weapons. This means that you can shoot at targets while staying hidden.

There is a minimum perception of 5 tiles. That means that having a stealth of 100% doesn't bring full invisibility. It is still usefull against those pesky "double perception" tritons.

No need for a new game.

#### You can use the pp-infiltrator-toolkit.properties file if you want to tweak the settings:
```
# Make crossbows "silent weapons"
# Shooting with a crossbow won't give your position
CrossbowIsSilent = true

# Sets the minimum perception value
# Having ultra high stealth will still bring benefits against double perception tritons
# but not a totally unfair advantage
MinPerception = 5

# -----------------------------------------------------------------------------------------
# The class icon will change color depending on the current status for the unit.
# You can use the following page to pick a value: https://www.w3schools.com/colors/colors_picker.asp
# -----------------------------------------------------------------------------------------

# A soldier is "located" when the enemy knows that there is a unit in this location but hasn't "revealed" it.
# You become "located" when doing sounds: opening/closing a door, breaking through a window,
# shooting with a non silent weapon, move within 5 tiles of an enemy ...
LocatedColor = ffcc00

# A soldier is "revealed" when at least one enemy sees him (line of sight and inside the perception range).
# Enemies can shoot at you in this state
RevealedColor = e65c00

# Once an enemy is alerted, it will activly look for our operatives. Chirons will "goo", "worm" or "bomb" any "located" operative.
AlertedColor = cc0066
```
