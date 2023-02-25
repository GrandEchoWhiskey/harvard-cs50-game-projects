## Tank Game
This is a tank game made with Unity, where the player controls a tank and shoots enemy targets.

## Gameplay
The game features a tank that can be moved with the arrow keys or the W, A, S, D keys. The tank has a turret that can be rotated with the mouse, and a muzzle that can be elevated with the mouse wheel. The player can shoot bullets with the left mouse button.

The objective of the game is to shoot down all enemy targets to win. The game ends when the player reaches a score of 3.

## Scripts
TankControll.cs
This script is responsible for the main game logic. It handles the movement and rotation of the tank, turret, and muzzle. It also handles shooting and reloading, and updates the player's score and the UI text accordingly.

## CameraControll.cs
This script handles the first-person camera movement. It allows the player to look around with the mouse and switch between first-person and third-person views.

## Bullet.cs
This script defines the behavior of bullets. It sets the speed and direction of the bullet, and destroys it when it collides with an object.

## Credits
This game was created by Eric Wolf. The tank and bullet models were downloaded from the Unity Asset Store.