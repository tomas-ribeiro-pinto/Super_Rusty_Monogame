# Project Title

Super Rusty

## Description

This is a simple game inspired in Nintendo's Super Mario. 
It was developed using Monogame Framework and C# for an assignment at my first year of university.

## Getting Started

### Dependencies

* This an ASP .NET Core game, which means that can be deployed to be run in any platform. 

### Installing

* Windows Version of the game available [here:](https://github.com/tomas-ribeiro-pinto/Super_Rusty_Monogame/tree/main/Download%20Game%20Windows)



# Introduction to Assignment Brief
This app will a create a simple 2D game similar to PacMan where the player moves a character around the screen eating, picking up or shooting some item, whilst other actors (objects that also move around) hinder or get in the way of the player's character completing their mission.    

The app must be developed using Visual Studio, C# and [MonoGame](https://www.monogame.net/) for any suitable platform such as Mobile Phone, Tablet, Games Console or Desktop using IOS, Android, Windows, Linux, PS4, XBox.  If you are working on a Mac with IOS please watch the first 7 minutes of [Getting Started with MonoGame using Visual Studio for Mac](https://www.youtube.com/watch?v=Hxo9A0-qcVo&ab_channel=TheDarksideofMonoGame)

[Please watch the following YouTube Video Series over the Easter Break](https://github.com/BNU-CO453/ConsoleApps15/wiki/Introduction-to-MonoGame)     

Come back from Easter with a selected game outline.   You can start the game from scratch with a new MonogGame project (see the videos for week 9) or take a copy of this 

[ App05 MonoGame Template created by Derek and Andrei by clicking on the **Use this Template** Button](https://github.com/BNU-CO453/App05MonoGame) 
or    
[ App05 MonoGame Template for Mac IOS](https://github.com/BNU-CO453/MacApp05Game)      
[Blank MonoGame using OpenGL for Mac and Windows](https://github.com/BNU-CO453/MonoGameOpenGL.git)

# General Game Requirements
'Super Rusty' is a game inspired in the 'Super Mario World' version. In this game, Rusty (university's mascot) has to collect as many beer bottles as possible and reach the end of the map however, if Rusty hits a policeman he gets arrested and a game is lost.

The game must contain the following basic general features
## Basic Features
1. The player can move back, forward, jump. 
2. Police NPC's spawning to prevent player from winning.
3. As the player proceeds their score can be increased by picking beer bottles.
4. If the player hits a police NPC, it loses one life.
5. There should be graphical setting or background for the game.

## Advanced Features
6. The game contains fixed objects such as platforms, rocks...
7. The game contains collision detection between the player object and other objects
8. Some of the moving characters are animated.
9. The game contains projectiles
10. The game can spawn enemies
## Outstanding Features
11. The game contains sound effects
12. The game contains music
13. The background graphics scroll.

# Game Use Case Diagram and Descriptions (20 Marks)
![Use Case Diagram](https://github.com/tomas-ribeiro-pinto/CO453_ApplicationProgramming/blob/master/App05%20resources/use_case_diagram.png)

The player can move forward, back and jump. 
If the player hits an enemy or it reaches the end of the map, the game ends.
 
# Game Class Diagram (10 marks)
![Class Diagram](https://github.com/tomas-ribeiro-pinto/CO453_ApplicationProgramming/blob/master/App05%20resources/class_diagram.png)

# Game Testing
## Screen Shots (16 Marks)
![Game1](https://github.com/tomas-ribeiro-pinto/CO453_ApplicationProgramming/blob/master/App05%20resources/test1.png)
![Game2](https://github.com/tomas-ribeiro-pinto/CO453_ApplicationProgramming/blob/master/App05%20resources/test2.png)
![Game3](https://github.com/tomas-ribeiro-pinto/CO453_ApplicationProgramming/blob/master/App05%20resources/test3.png)
![Game4](https://github.com/tomas-ribeiro-pinto/CO453_ApplicationProgramming/blob/master/App05%20resources/test4.png)

## Game Walkthrough (46 Marks awarded in Testing)
*A live demonstration or video of winning the game*
*A live demonstration or video example of loosing the game*
# Evaluation (20 Marks)
1. Add more levels and record player's progress.
2. Record higher scores in a list at the end.
3. Some code needs to be refactored to be more readable and maintainable for future releases.
4. Add more features like shooting abilities, more enemies on top of blocks in a refactored way.
5. Improve collision detection to prevent jumping and other bugs.
