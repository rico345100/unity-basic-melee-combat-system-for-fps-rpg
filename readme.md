# Basic First Person Melee System Implementation with Unity 5

## Features
- Penetrating sword attacks without multiple hits on Single target(Accurate hit detection system)
- Weapon swaying
- Visible lower torso in first person view
- Guarding system (Block enemy attack)
- Kicking (Press G)
- Player chasing enemy

## Future plans
- Weapon switching
- Fully functioning Bow & Arrow system

## Controls
- W,S,A,D: Basic Movement
- Mouse 1: Attack
- Mouse 2: Guard(Keep pressing to hold)
- G: Kick(Auxiliary attacking function)

## Notes
- Guarding system is fine, but animation and detection range are kind of sucks so it's hard to success to guard. This causes because of Enemy's attack animation is too low and Player's guard animation is too high. So guarding system is just for showing it's possible.
- You can't attack while you are guarding, but you can kick it. This is because current hit detection system gives immune for same attack until end of attack animation. This can be changed later, if I found better solution.