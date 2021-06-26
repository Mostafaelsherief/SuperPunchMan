# SuperPunchMan
<body>
  
A Mobile Game built with Unity 3D Engine.
  
</body>

<img SRC="RobbersRace/Media/Photo1.png" width="200">  <img SRC="RobbersRace/Media/Photo2.png" width="200"> <img SRC="RobbersRace/Media/ezgif.com-gif-maker.gif" width="285">
<body>
  
## Game Link 
  
  Available on Google Playstore : https://play.google.com/store/apps/details?id=com.MymCo.SuperPunchman
 ## Game Concept
  A Stealth Game where the player has to kill all guards without being detected to win a Level ,the player can be detected if he is in the guards field of view ,the guard can only be killed if the player hits him from his back.  
  
  ## Code Structure
  
 ### Characters  
**The Player** exists in four states: Moving State, Dead State, Hit by Laser State and Win State. **A Guard** exists in three states: patrolling state, A shooting state, and dead state.Each state is characterized by a certain action and an animation.
  
  **In this example** the player is in a Moving State,  and the guard is in a Patrolling state,a "Player caught" event is triggered when the player is in the guard's field of view causing the guard  to change to a shooting State, and the player to a dead state .
 <body>
  <img SRC="RobbersRace/Media/Move-Dead.gif" width="285">
  </body>
  
In  [Charater](https://github.com/Mostafaelsherief/SuperPunchMan/tree/main/RobbersRace/Assets/Scripts/Character) you can access **Character.cs** containing Movement and Animation functionalities shared between Guard and Player,  **State.cs** for State Class, and **StateMachine.cs** for the State Machine ,in [Player](https://github.com/Mostafaelsherief/SuperPunchMan/tree/main/RobbersRace/Assets/Scripts/Character/Player) and [Guard](https://github.com/Mostafaelsherief/SuperPunchMan/tree/main/RobbersRace/Assets/Scripts/Character/Guard) folders you can access the States that correspond to each character mentioned in the section above. 
  
  
### Ground

MazeSpawner Class generates complex non-cycled mazes using a recursive approach . Source:https://assetstore.unity.com/packages/tools/modeling/maze-generator-38689

a Maze has only one path available and some areas are dead ends, After changing the Wall prefab used on the original project with some crates and broken wall prefabs you get an open ground with no closed Areas.
| With Crates/Broken Walls  | Without Crates/Broken Walls |
| ------------- | ------------- |
| <img SRC="RobbersRace/Media/MazeWithCrates.jpg" width="200">  |  <img SRC="RobbersRace/Media/MazeWithoutCrates.jpg" width="200">  |

 In [MazeGenerator](https://github.com/Mostafaelsherief/SuperPunchMan/tree/main/RobbersRace/Assets/Scripts/ManagerScripts/MazeGenerator)  **MazeSpawner.cs** can be accessed and all helper classes.

### Level Generation

To Make the game progressive the ground size  and the number of guards increases , depending on the number of Levels you’ve passed , and later in the game  Laser traps and  Electric Traps are introduced,if the Player hits a laser a siren will be heard and the guards will move towards the laser position , if the Player hits an  Electric trap you die instantly.
 | Laser Trap   | Electric Trap |
| ------------- | ------------- |
| <img SRC="RobbersRace/Media/LaserTrap.gif" width="200">  |  <img SRC="RobbersRace/Media/ElectricTrap.gif" width="200">  |

  in [Traps](https://github.com/Mostafaelsherief/SuperPunchMan/tree/main/RobbersRace/Assets/Scripts/Traps) **LaserObstacle.cs** is responsible for alteranting the laser activation , **LaserTrigger.cs** for triggering a "player detect" event causing guards to move near the laser position , **FloorTrap.cs** triggers a kill player event if the player is inside the trap and the it was active.  
  
 The Mazes are generated Randomly ,so if a certain random seed is to be chosen with the same maze width and height the same ground will be generated .
NavmeshSurface is a component  of Unity Built-in Pathfinding Algorithm it generates the Ground on which a player can Move , so before the Game starts a NavMeshSurface is generated.According to this a Level Generator Class , each Level has a predetermined Level number Random Seed number of Guards , Mazewidth , Mazeheight ,is there an Electric Trap and  is there a Laser Trap. 
  
In [ManagerScripts](https://github.com/Mostafaelsherief/SuperPunchMan/tree/main/RobbersRace/Assets/Scripts/ManagerScripts/MazeGenerator) the folowing Classes can be accessed : 
* **GameManager.cs** responsible for UI , Camera Movement , and determines current level.  
* **AudioManager.cs** responsible for game Audio Control.
* **GameEvents.cs** responsible for Game events.
* **LevelManager.cs** repsonsible for Level control
  
  



  </body >


