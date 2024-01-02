# CardReveal

CardReveal is a simple memory game, but it also has a lot of cool features. Made in Unity, using C#

## Card Distribution 

The card distribution of the game is based on a “Image”, the user chooses the level in a map, the game gets the level image and builds the card layout.


## Card Faces Generation

The card generation isn’t a simple random, but a more balanced random, you can learn more about it [here](https://github.com/Jean-Delfino/Simple-Reuse/blob/main/Assets/Reuse/Utils/UtilProbability.cs), the main principle is that the most frequent “proc” (value that has been generated) start the next wave with a less chance of been generated, but if at one point all the chances were chosen, his probability increases or gets equals to every other value chance.


## Card Reveal Score

When you reveal two cards you can get four outputs: Lucky, MemoryReveal, MemoryMiss and Miss. Lucky is when you haven’t seen any of the revealed card faces, but both card are equal; MemoryReveal is when you seen at least one of the card, and both card are equal; MemoryMiss is when you seen at least one of the card, but the cards are different, this is the only output where you lose points;Miss is when the card are not equal, and you haven’t seen at least one of them.


## Language

The game is available in two languages: Portuguese and English. The system automatically gets your system language and sets all texts based on it. All game texts work with the actual language.


## OptionMenu

The game has a configurable options menu, where you can change the music and sound effect volume, the game camera sensibility and the current game language.


## Save

The game saves your preferences and your score in a level.
