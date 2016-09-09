#define DROP
#define JUMP
#define PLATFORM

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MainGame
{
    enum ActionType
    {
        IDLE,
        GO,
        FIGHT,
        JUMP,
        JUMP_FIGHT
    }

    class Player
    {
        #region Fields

        #region Sprite movement
        // Total count of all possible player action
        int totalPlayerActionCount;
        // Array of all possible player action
        Animation[] playerActions;
        // ID of current player action
        int currentPlayerAction;

        Vector2 playerPosition;
        Vector2 playerVelocity;
        
        #region Jump

#if JUMP      
        // Current time in milliseconds during which player is already jumping
        int currentJumpingTime;
        // True if player could jump in current moment, False otherwise
        bool couldJump;
#endif

#if PLATFORM
        PlatformList platformList;
#endif

        #endregion

        #endregion

        KeyboardState state;
        SpriteEffects currentSpriteEffect;

#if DROP

        // Store dropping object content for animation implementation
        DropObjectStates states;
        // Object witch allow to drop set of objects
        DropSetOfObjects swordSet;

#endif

        #endregion

        #region Constructor

        public Player(Vector2 playerPosition, int totalPlayerActionCount, PlatformList platformList = null)
        {
            this.totalPlayerActionCount = totalPlayerActionCount;
            playerActions = new Animation[totalPlayerActionCount];
            currentPlayerAction = (int)ActionType.IDLE;

            this.playerPosition = playerPosition;
            playerVelocity = new Vector2();

#if JUMP
            currentJumpingTime = 0;
            couldJump = true;
#endif

#if PLATFORM
            this.platformList = platformList;
#endif
            currentSpriteEffect = SpriteEffects.None;
        }

        #endregion

        #region Properties

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerActions[currentPlayerAction].X, playerActions[currentPlayerAction].Y); }
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager Content)
        {
            playerActions[(int)ActionType.IDLE] = new Animation(Content.Load<Texture2D>(@"droid\idle"), playerPosition, 3, 100);
            playerActions[(int)ActionType.GO] = new Animation(Content.Load<Texture2D>(@"droid\go"), playerPosition, 3, 100);
            playerActions[(int)ActionType.FIGHT] = new Animation(Content.Load<Texture2D>(@"droid\attack"), playerPosition, 3, 100);
            playerActions[(int)ActionType.JUMP] = new Animation(Content.Load<Texture2D>(@"droid\fly"), playerPosition, 3, 100);
            playerActions[(int)ActionType.JUMP_FIGHT] = new Animation(Content.Load<Texture2D>(@"droid\jump_attack"), playerPosition, 3, 100);

#if DROP
            // Load all possible states of droping object
            states = new DropObjectStates(GameConstants.droppingObjectStatesCount);
            states.LoadContent(Content);

            // Initilise set of droping objects
            swordSet = new DropSetOfObjects();
#endif
        }

        #region Player actions implementation

        void idleImplementation()
        {
            playerVelocity = new Vector2();
            currentPlayerAction = (int)ActionType.IDLE;
        }

        void forwardMovementImplementation()
        {
            playerVelocity.X = GameConstants.playerVelocityX;

            currentPlayerAction = (int)ActionType.GO;
            currentSpriteEffect = SpriteEffects.None;
        }

        void backwardMovementImplementation()
        {
            playerVelocity.X = -GameConstants.playerVelocityX;

            currentPlayerAction = (int)ActionType.GO;
            currentSpriteEffect = SpriteEffects.FlipHorizontally;
        }

#if JUMP
        void jumpMovementImplementation(GameTime gameTime)
        {
            if (couldJump)
            {
                currentJumpingTime += gameTime.ElapsedGameTime.Milliseconds;
                // If current jumping time is bigger then maximum possible jumping time
                if (currentJumpingTime >= GameConstants.totalJumpingTime)
                {
                    couldJump = false;
                    currentJumpingTime = 0;
                }

                // Jumping implementation in velocity frame
                playerVelocity.Y = -GameConstants.playerVelocityY;
                currentPlayerAction = (int)ActionType.JUMP;
            }
        }
#endif

        #region Physics implementation

        void gravityImplementation(GameTime gameTime)
        {
            if (playerPosition.Y < GameConstants.WindowHeight - playerActions[currentPlayerAction].Y)
                // Decrease velocity (it is move physically)
                playerVelocity.Y += GameConstants.gravityAcceleration;

#if JUMP
            // If sprite rach the ground
            if (playerPosition.Y >= GameConstants.WindowHeight - playerActions[currentPlayerAction].Y)
                couldJump = true;
#endif
        }

        void boundaryConditionImplementation()
        {
            // Check boundaty conditions on left, right, top boundary conditions
            if (playerPosition.X <= 0)
                playerPosition.X = 0;

            if (playerPosition.X >= GameConstants.WindowWidth - playerActions[currentPlayerAction].X)
                playerPosition.X = GameConstants.WindowWidth - playerActions[currentPlayerAction].X;

            if (playerPosition.Y <= 0)
                playerPosition.Y = 0;
        }

        #endregion

        void fightImplementation()
        {
            // If player in the air set sprite to jump attack
            if (playerPosition.Y < GameConstants.WindowHeight - playerActions[currentPlayerAction].Y)
                currentPlayerAction = (int)ActionType.JUMP_FIGHT;
            // Otherwise set to origin attack
            else
                currentPlayerAction = (int)ActionType.FIGHT;
            // Drop direction depends on previous sprite orientation
#if DROP
            swordSet.Add(states, playerPosition, 100, currentSpriteEffect);
#endif
        }

#if PLATFORM
        void playerPlatformInteractionImplementation()
        {
            if (!platformList.Empty())
            {
                // Look throw platforms for player-platform behaviour
                for (int i = 0; i < platformList.Count; ++i)
                {
                    if (Rectangle.TouchTopOf(platformList.At(i).Rectangle))
                    {
                        playerPosition.Y = platformList.At(i).Rectangle.Top - playerActions[currentPlayerAction].Y;
                        couldJump = true;
                    }
                    if (Rectangle.TouchBottomOf(platformList.At(i).Rectangle))
                        couldJump = false;
                    if (Rectangle.TouchLeftOf(platformList.At(i).Rectangle))
                        playerPosition.X = platformList.At(i).Rectangle.Right;
                    if (Rectangle.TouchRightOf(platformList.At(i).Rectangle))
                        playerPosition.X = platformList.At(i).Rectangle.Left - playerActions[currentPlayerAction].X;
                }
            }
        }
#endif

        #endregion

        public void Update(GameTime gameTime)
        {
            playerPosition += playerVelocity * gameTime.ElapsedGameTime.Milliseconds;
            playerVelocity = new Vector2();

            // Initial sprite - IDLE (By default initial state of the player)
            idleImplementation();

            state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Right))
                forwardMovementImplementation();
            if (state.IsKeyDown(Keys.Left))
                backwardMovementImplementation();
#if JUMP
            if (state.IsKeyDown(Keys.Space))
                jumpMovementImplementation(gameTime);
#endif
            if (state.IsKeyDown(Keys.A))
                fightImplementation();
            
            // Physics implementation
            gravityImplementation(gameTime);

            boundaryConditionImplementation();

#if PLATFORM
            playerPlatformInteractionImplementation();
#endif
            // Update current animation
            playerActions[currentPlayerAction].PlayAnimation(gameTime);

#if DROP
            swordSet.Update(gameTime, playerPosition, currentSpriteEffect);
#endif
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            playerActions[currentPlayerAction].Draw(spriteBatch, playerPosition, currentSpriteEffect);

#if DROP
            swordSet.Draw(spriteBatch);
#endif
        }

        #endregion
    }
}
