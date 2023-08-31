# technical-test-space-ape

## Introduction

This little game has the purpose of showing one of my knowledge of Unity and C#. 
There are many features that I could add but could be repetitive or take too long to finish the project. 
Next, I will explain about the project.

## Escape Space
This game has a test purpose and I focus on showing the coding and some improving user experience. 
The game is just to escape from the enemies and the level of difficulty changes the enemy velocity.
# Coding
- Object Pool - I used the Object Pool from the C# for the enemy pool.
- Singleton - I used Singleton for the audio to control any place in the game.
- Hashtable - I searched the audio with a hashtable, because is fast to search and recover in O(1) most of the time.
- Interface - I used interface for the Damage feature which is common in many different objects, as used in enemy, player,..
- Bug correction - The code had an error result in "WorldPositionToScreenPosition" after creating the UnityTest was easy to find.
- Scriptable Object(SO) - Objects that change the same data information the SO is better to use than the class that uses MonoBehavior.
- Enum - I used this as key of the hashtable, and an enum is very important for data that the name does not change.
# Others
- Slice - I used different button sizes Slice sprites are essential.


## Improvements
Some tasks that I could improve in my project:
- Bullets - I could use the same idea of enemies that use Object Pool.
- Animations - The animation controller is a very good component to make the player experience more immersive.
- Player - I could separate on different class and I could use a damage interface (IDamagleable) to get or set damage received by the bullets.
- ScriptableObject - I really like to use Scriptable Object to save objects and keep the data of the game (such as game settings, player settings, enemy settings,...) and remove the Game Settings class.
- Fade - Fade one screen to another to make the user experience better, I usually use Coroutine to not use the Update().
- Game flow - The minimum game flow is Menu => Game => GameOver/Win screens then I would add one more Stage => GameOver/Win screen.

And thank for your time to see my little game =)

I made my code available on [Github](https://github.com/kyumiouchi/technical-test-space-ape) and you can see how I progressed while running the game.