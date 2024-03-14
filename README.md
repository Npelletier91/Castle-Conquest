# Castle Conquest

Castle Conquest is a 2D platform runner game developed in C# using Unity. In this adventurous game, players aim to collect coins, defeat enemies, progress through levels, and discover secret coins and hearts.

<img src="https://github.com/Npelletier91/Castle-Conquest-Game/assets/129113700/5afa5852-11fe-47c0-929f-5563831db2cd" width="400" height="255">



## Key Features

- **2D Platform Runner**: Navigate through captivating levels filled with challenges and obstacles.
- **Collectibles**: Gather coins to earn points and discover hidden hearts for additional health.
- **Enemy Encounters**: Defeat enemies strategically placed across the levels.
- **Level Progression**: Advance through different levels of difficulty and complexity.
- **Secrets to Discover**: Explore hidden areas to find secret coins and hearts.

Example Climbing Code:
```csharp
private void Climb()
    {
        bool canClimb = myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"));

        if (canClimb)
        {
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 playerVelocity = new Vector2(myRigidBody2D.velocity.x, controlThrow * runSpeed);
            myRigidBody2D.velocity = playerVelocity;

            myRigidBody2D.gravityScale = 0f;

            myAnimator.SetBool("isClimbing", true);
        }
        else
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody2D.gravityScale = 1f;

        }

    }
```

## How to Play

Describe the gameplay mechanics and controls here, for example:

- Use the arrow keys or WASD to move the character.
- Press the spacebar to jump.
- Collect coins by running into them.
- Defeat enemies by the left mouse click for the attack button.


## How to Run

1. **Clone Repository**: Clone this repository to your local machine.
2. **Open with Unity**: Open the project using Unity.
3. **Run the Game**: Launch the game from the Unity editor or build the game for your platform of choice.
