#define DROP

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ShootBullet
{
    enum ActionType
    {
        IDLE,
        GO,
        FIGHT,
        JUMP,
        DEAD
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

        float velocityX = 0.15f;
        float velocityY = 0.45f;

        // Jump
        float gravity = 0.1f;


        float jumpMilliseconds; 
        bool couldJump;

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

            // jump
            couldJump = true;
            jumpMilliseconds = 100;

            // window 
            currentSpriteEffect = SpriteEffects.None;
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager Content)
        {
            actions[(int)ActionType.IDLE] = new Animation(Content.Load<Texture2D>(@"droid\idle"), position, 3, 100);
            actions[(int)ActionType.GO] = new Animation(Content.Load<Texture2D>(@"droid\go"), position, 3, 100);
            actions[(int)ActionType.FIGHT] = new Animation(Content.Load<Texture2D>(@"droid\attack"), position, 3, 100);
            actions[(int)ActionType.JUMP] = new Animation(Content.Load<Texture2D>(@"droid\fly"), position, 3, 100);

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
            velocity.X = velocityX;

            currentAction = (int)ActionType.GO;
            currentSpriteEffect = SpriteEffects.None;
        }

        void backwardMovementImplementation()
        {
            velocity.X = -velocityX;

            currentAction = (int)ActionType.GO;
            currentSpriteEffect = SpriteEffects.FlipHorizontally;
        }

        void jumpMovementImplementation(GameTime gameTime)
        {
            // Implementation of jumping in time frame
            velocity.Y = -velocityY;
            currentAction = (int)ActionType.JUMP;
            //couldJump = false;
        }

        void gravityImplementation(GameTime gameTime)
        {
            if (position.Y < WindowHeight - actions[currentAction].Y)
                velocity.Y += gravity; // Decrease velocity (it is move physically)

            // If sprite rach the ground
            if (position.Y == WindowHeight - actions[currentAction].Y)
                couldJump = true;
        }

        void fightImplementation()
        {
            currentAction = (int)ActionType.FIGHT;
            // Drop direction depends on previous sprite orientation
#if DROP
            swordSet.Add(states, position, WindowWidth, 100, currentSpriteEffect);
#endif
        }

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
            if (state.IsKeyDown(Keys.Space))
                jumpMovementImplementation(gameTime);

            if (state.IsKeyDown(Keys.A))
                fightImplementation();

            // Here will be extern condition (boundary conditions, gravity, )
            gravityImplementation(gameTime);


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
