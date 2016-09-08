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
        // Number of all possible actions
        int actionsCount;
        // Set of all possible actions
        Animation[] actions;
        // Id of current action
        int currentAction;

        // Current player position
        Vector2 position;
        Vector2 velocity;

        //float velocityX = 0.15f;
        float velocityY = 0.45f;


        // Graviatational force
        float gravity = 0.1f;
        #region Jump

#if JUMP

        // Total milliseconds number, witch object could jumping
        float totalJumpingTime;
        // Current milliseconds number, witch object could be jumping at this time
        int currentJumpingTime;
        // If could jump - true, false otherwise
        bool couldJump;
#endif

#if PLATFORM

        PlatformList platformList;

#endif

        #endregion

        #endregion

        KeyboardState state;
        SpriteEffects currentSpriteEffect;

        // window parametres
        int WindowWidth;
        int WindowHeight;

#if DROP

        // Store all textures for droping process animation
        DropObjectStates states;
        // Object witch allow to drop set of objects
        DropSetOfObjects swordSet;

#endif

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor with one parameter: 
        /// - allcoate memory for all Players posiible actions
        /// - set initial sprite to IDLE
        /// - set zero inital velocity
        /// </summary>
        /// <param name="position"> Initial object position </param>
        /// <param name="actionsCount"> number of possible Players actions </param>
        /// <param name="windowWidth"> window width </param>
        /// <param name="windowHeight"> window height </param>
        public Player(Vector2 position, int actionsCount, int windowWidth, int windowHeight)
        {
            this.actionsCount = actionsCount;
            actions = new Animation[actionsCount];
            currentAction = (int)ActionType.IDLE;

            this.position = position;
            velocity = new Vector2();

#if JUMP
            currentJumpingTime = 0;
            totalJumpingTime = 400;
            couldJump = true;
#endif            

            // window 
            currentSpriteEffect = SpriteEffects.None;
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
        }

#if PLATFORM
        /// <summary>
        /// Constructor with one parameter: 
        /// - allcoate memory for all Players posiible actions
        /// - set initial sprite to IDLE
        /// - set zero inital velocity
        /// </summary>
        /// <param name="position"> Initial object position </param>
        /// <param name="actionsCount"> number of possible Players actions </param>
        /// <param name="platformList"> list of platfor for interaction </param>
        /// <param name="windowWidth"> window width </param>
        /// <param name="windowHeight"> window height </param>
        public Player(Vector2 position, int actionsCount, PlatformList platformList, int windowWidth, int windowHeight)
        {
            this.actionsCount = actionsCount;
            actions = new Animation[actionsCount];
            currentAction = (int)ActionType.IDLE;

            this.platformList = platformList;

            this.position = position;
            velocity = new Vector2();

#if JUMP
            currentJumpingTime = 0;
            totalJumpingTime = 400;
            couldJump = true;
#endif            

            // window 
            currentSpriteEffect = SpriteEffects.None;
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
        }
#endif

        #endregion

        #region Properties

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, actions[currentAction].X, actions[currentAction].Y); }
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager Content)
        {
            actions[(int)ActionType.IDLE] = new Animation(Content.Load<Texture2D>(@"droid\idle"), position, 3, 100);
            actions[(int)ActionType.GO] = new Animation(Content.Load<Texture2D>(@"droid\go"), position, 3, 100);
            actions[(int)ActionType.FIGHT] = new Animation(Content.Load<Texture2D>(@"droid\attack"), position, 3, 100);
            actions[(int)ActionType.JUMP] = new Animation(Content.Load<Texture2D>(@"droid\fly"), position, 3, 100);
            actions[(int)ActionType.JUMP_FIGHT] = new Animation(Content.Load<Texture2D>(@"droid\jump_attack"), position, 3, 100);

#if DROP
            // Load all possible states of droping object
            states = new DropObjectStates(6);
            states.LoadContent(Content);

            // Initilise set of droping objects
            swordSet = new DropSetOfObjects();
#endif
        }

        #region Player movement

        void idleImplementation()
        {
            velocity = new Vector2();
            currentAction = (int)ActionType.IDLE;
        }

        void forwardMovementImplementation()
        {
            velocity.X = GameConstants.playerVelocityX;

            currentAction = (int)ActionType.GO;
            currentSpriteEffect = SpriteEffects.None;
        }

        void backwardMovementImplementation()
        {
            velocity.X = -GameConstants.playerVelocityX;

            currentAction = (int)ActionType.GO;
            currentSpriteEffect = SpriteEffects.FlipHorizontally;
        }

#if JUMP
        void jumpMovementImplementation(GameTime gameTime)
        {
            // If player could jump
            if (couldJump)
            {
                // Update current time object in the air
                currentJumpingTime += gameTime.ElapsedGameTime.Milliseconds;
                // If this time bigger then total time it could spent in the air
                if (currentJumpingTime >= totalJumpingTime)
                {
                    couldJump = false;
                    currentJumpingTime = 0;
                }

                // Implementation of jumping in velocity frame
                velocity.Y = -velocityY;
                currentAction = (int)ActionType.JUMP;
            }
        }
#endif
        void gravityImplementation(GameTime gameTime)
        {
            if (position.Y < WindowHeight - actions[currentAction].Y)
                // Decrease velocity (it is move physically)
                velocity.Y += gravity;

#if JUMP
            // If sprite rach the ground
            if (position.Y >= WindowHeight - actions[currentAction].Y)
                couldJump = true;
#endif
        }

        void fightImplementation()
        {
            // If player in the air set sprite to jump attack
            if (position.Y < WindowHeight - actions[currentAction].Y)
                currentAction = (int)ActionType.JUMP_FIGHT;
            // Otherwise set to origin attack
            else
                currentAction = (int)ActionType.FIGHT;
            // Drop direction depends on previous sprite orientation
#if DROP
            swordSet.Add(states, position, WindowWidth, 100, currentSpriteEffect);
#endif
        }

#if PLATFORM

        /// <summary>
        /// Implementation of player-platform interaction
        /// </summary>
        void platformInteraction()
        {
            // If platform list is not empty
            if (!platformList.Empty())
            {
                // Look throu all platforms and find out any interactions between them
                for (int i = 0; i < platformList.Count; ++i)
                    if (this.Rectangle.Intersects(platformList.At(i).Rectangle))
                    {
                        // THE MOST COMMON IMPLEMENTATION OF INTERACTION
                        position.Y = platformList.At(i).Rectangle.Top - actions[currentAction].Y;

                        // We ned to do on interactions in therm of TOP BOTTOM LEFT RIGHT
                        couldJump = true;
                    }
            }

        }

#endif

        #endregion

        public void Update(GameTime gameTime)
        {
            position += velocity * gameTime.ElapsedGameTime.Milliseconds;
            velocity = new Vector2();
            // Initial sprite - is IDLE (It takes plase then no one other actions could be)
            idleImplementation();

            // Now read and store input user key
            state = Keyboard.GetState();

            // Good DONE GO AND FIGHT IN THE SAME TIME ALGORITHM
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

            // Here will be extern condition (boundary conditions, gravity, )
            gravityImplementation(gameTime);

            // platform interaction implemantation

#if PLATFORM

            platformInteraction();

#endif
            // Update current animation
            actions[currentAction].PlayAnimation(gameTime);

#if DROP
            swordSet.Update(gameTime, position, currentSpriteEffect);
#endif
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            actions[currentAction].Draw(spriteBatch, position, currentSpriteEffect);

#if DROP
            swordSet.Draw(spriteBatch);
#endif

        }

        #endregion
    }
}
