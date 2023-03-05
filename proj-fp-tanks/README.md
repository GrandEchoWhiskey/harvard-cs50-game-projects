[<- Back to course](../README.md)

<p align="center"><a href="https://cs50.harvard.edu/games/2018">
  <img src="https://github.com/GrandEchoWhiskey/grandechowhiskey/blob/main/icons/course/harvard100.png" /><br>
</a></p>
<h1 align="center">CS50’s Introduction to Game Development<br><br>Final Project - Tanks</h1>

<p align="center"><a href="#">
  <img src="https://github.com/GrandEchoWhiskey/grandechowhiskey/blob/main/icons/programming/csharp.png" />
  <img src="https://github.com/GrandEchoWhiskey/grandechowhiskey/blob/main/icons/programming/unity.png" />
</a></p>

### Description:
This is a 3D tank game made with Unity, where the player controls a tank and shoots enemy targets in a battlefield environment.

#### Gameplay
The game features a tank that can be moved with the arrow keys or the W, A, S, D keys. The tank has a turret that can be rotated with the mouse, and a muzzle that can be elevated with the mouse also. The player can shoot bullets with the left mouse button to destroy enemy targets.

The objective of the game is to shoot down all enemy targets to win. The game ends when the player reaches a score of 3. The targets are other tanks.

#### Scripts
##### TankControll.cs
This script is responsible for the main game logic. It handles the movement and rotation of the tank, turret, and muzzle. It also handles shooting and reloading, and updates the player's score and the UI text accordingly.

Creating rotations for different parts of a tank in a game can be a bit challenging, as each part may require a different type of rotation. In this tank game, the tank, turret, and muzzle all required different types of rotations.

The tank movement was relatively straightforward, as it only required the tank to rotate left or right and move forward or backward. This was done using Unity's built-in Rigidbody component and the tank's Transform component.

On the other hand, the turret rotation required a different approach. It needed to rotate around the base of the turret and follow the mouse movement. This was done by using the Transform component of the turret and applying the rotation using the mouse position.

The muzzle elevation was another challenge as it required a combination of both rotation and translation. The muzzle needed to elevate or lower based on the mouse movement while keeping its rotation aligned with the turret. This was achieved by applying a rotation around the X-axis of the muzzle Transform and a translation along the Z-axis based on the mouse position.

Overall, creating rotations for the different parts of the tank required a combination of different techniques, such as using Transform components, applying rotations and translations, and following mouse positions. With careful planning and experimentation, it was possible to create a smooth and responsive tank game that feels natural and fun to play.

##### CameraControll.cs
This script handles the first-person camera movement. It allows the player to look around with the mouse and switch between first-person and third-person views.

##### Bullet.cs
This script defines the behavior of bullets. It sets the speed and direction of the bullet, and destroys it when it collides with an object.

#### Credits
This game was created by Eric Wolf. The tank and bullet models were downloaded from the Unity Asset Store. The battlefield environment was created by Eric Wolf using Unity's terrain tools.

#### Problems
Although this tank game has several interesting features, it also presents some challenges that could be improved in future versions:

- Limited enemy variety: The game only features one type of enemy target, which can make the gameplay repetitive after a while. Adding more types of enemies, each with unique behaviors and abilities, could make the game more engaging and challenging.

- Lack of terrain diversity: The game takes place in a urban landscape, which can be visually appealing but doesn't offer much variation in terms of gameplay. Adding different types of terrain, such as hills, forests, or desert areas, could make the game more dynamic and interesting to explore.

- Limited customization options: Although the game allows the player to switch between first-person and third-person views, it doesn't offer many other customization options, such as changing the tank's color, upgrading its weapons or armor, or choosing different types of ammunition. Adding more customization options could give the player a greater sense of ownership and investment in the game.

Despite these limitations, the game is still enjoyable and well-designed, and it showcases some of the capabilities of Unity and game development in general. By addressing these issues and adding more features, the game could become even more compelling and immersive.

#### TODO:
- Create enemy AI to make enemies more challenging.
- Add power-ups for the player to collect, such as health boosts or more powerful bullets.
- Create different types of enemy targets with varying health and attack patterns.
- Add different levels with unique landscapes and obstacles.
- Implement a multiplayer mode to allow players to compete against each other.
- Improve graphics and sound effects to enhance the player's experience.
- Add a tutorial or instructions screen for new players.
- Implement a leaderboard to track high scores.
- Test the game thoroughly and fix any bugs or glitches.
- Optimize the game for better performance on different devices.

### Getting Started:
Export this directory using SVN.
```
svn export https://github.com/GrandEchoWhiskey/harvard-cs50-game-projects/trunk/proj-10-portal
```
