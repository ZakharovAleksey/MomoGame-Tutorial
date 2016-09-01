using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Animation
{
    enum ActionType
    {
        IDLE = 0,
        GO,
        RUN,
        FLY,
        GET_DAMAGE,
        ATTACK,
        JUMP_ATTACK,
        DEAD
    }

    class Droid
    {
        #region Fields

        // Screen parameters
        int WindowWidth;
        int WindowHeight;

        // All possible animations
        Animation[] actions = new Animation[8];
        int currentAction = (int)ActionType.IDLE;

        Vector2 velocity = new Vector2(0, 0);
        Vector2 position = new Vector2(100, 500);

        // velocity module
        float vx = 2.0f;
        float vy = 2.0f;

        // jump
        float g = 2.0f;
        float jump = 50.0f;
        bool couldJump = true;

        // health
        int health = 100;
        bool live = true;

        // rotate sprite
        bool rotateSprite = false;
        SpriteEffects currentSpriteEffect = SpriteEffects.None;

        // Current state
        KeyboardState oldState;

        #endregion

        #region Constructor

        public Droid(int newWindowWidth, int newWindowHeight)
        {
            WindowHeight = newWindowHeight;
            WindowHeight = newWindowHeight;
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public void LoadContent(ContentManager Content)
        {
            actions[(int) ActionType.IDLE] = new Animation(Content.Load<Texture2D>(@"droid\idle"), position, 3, 100);
            actions[(int)ActionType.GO] = new Animation(Content.Load<Texture2D>(@"droid\go"), position, 3, 100);
            actions[(int)ActionType.RUN] = new Animation(Content.Load<Texture2D>(@"droid\run"), position, 3, 100);
            actions[(int)ActionType.FLY] = new Animation(Content.Load<Texture2D>(@"droid\fly"), position, 3, 100);
            actions[(int)ActionType.GET_DAMAGE] = new Animation(Content.Load<Texture2D>(@"droid\get_damage"), position, 3, 100);
            actions[(int)ActionType.ATTACK] = new Animation(Content.Load<Texture2D>(@"droid\attack"), position, 3, 100);
            actions[(int)ActionType.JUMP_ATTACK] = new Animation(Content.Load<Texture2D>(@"droid\jump_attack"), position, 3, 100);
            actions[(int)ActionType.DEAD] = new Animation(Content.Load<Texture2D>(@"droid\dead"), position, 3, 100);
        }

        #region Different Droid actions implementation

        void goRightImplementation()
        {
            velocity.X = vx;
            currentAction = (int)ActionType.GO;
            currentSpriteEffect = SpriteEffects.None;
        }

        void goLeftImplementation()
        {
            velocity.X = -vx;
            currentAction = (int)ActionType.GO;
            currentSpriteEffect = SpriteEffects.FlipHorizontally;

        }

        void stayImplementation()
        {
            velocity = new Vector2();
            currentAction = (int) ActionType.IDLE;
            currentSpriteEffect = SpriteEffects.None;
        }

        void jumpImplementation()
        {
            if (couldJump)
            {
                position.Y -= jump;
                couldJump = false;
                currentAction = (int)ActionType.FLY;
                currentSpriteEffect = SpriteEffects.None;
            }
        }

        void attackImplementation()
        {
            if (position.Y < WindowHeight - actions[currentAction].Y)
                currentAction = (int)ActionType.JUMP_ATTACK;
            else
                currentAction = (int) ActionType.ATTACK;
            currentSpriteEffect = SpriteEffects.None;
        }

        void jumpRightImplementation()
        {
            velocity.X = vx;

            if (couldJump)
            {
                position.Y -= jump;
                couldJump = false;
                currentAction = (int)ActionType.FLY;
                currentSpriteEffect = SpriteEffects.None;
            }
        }

        void bottomBCImplementation()
        {
            if (position.Y < WindowHeight - actions[currentAction].Y)
            {
                position.Y += g;
            }
            else 
            {
                position.Y = WindowHeight - actions[currentAction].Y;
                couldJump = true;
            }
            
        }

        void checkHealthImplementation()
        {
            if (health == 0)
                live = false;
        }

        void deathImplementation()
        {
            currentAction = (int)ActionType.DEAD;
            currentSpriteEffect = SpriteEffects.None;
            if (position.Y < WindowHeight - actions[currentAction].Y)
                position.Y += g;
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            //newState = Keyboard.GetState();

            // IN SHOOTING PROJECT DONE GO AND FIGHT IN THE SAME TIME ALGORITHM

            if (live)
            {
                position += velocity;

                // Check on stay position [stay sprite is inital for all positions in witch player stay - attack, or another]
                if (velocity == new Vector2())
                    stayImplementation();
                // Then set velocity to zero
                velocity = new Vector2();

                // Right movement actions [jump right not from the first time: think why]
                if (oldState.IsKeyDown(Keys.Space) && newState.IsKeyDown(Keys.Right))
                    jumpRightImplementation();
                else if (newState.IsKeyDown(Keys.Right))
                    goRightImplementation();
                // TODO: Same Left movement actions
                if (newState.IsKeyDown(Keys.Left))
                    goLeftImplementation();
                //if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyDown(Keys.Space))
                //{
                //    jumpImplementation();
                //    jumpImplementation();
                //}
                if (newState.IsKeyDown(Keys.Space))
                    jumpImplementation();
                if (newState.IsKeyDown(Keys.P))
                    attackImplementation();
                
                // TODO: All Boundary conditions
                bottomBCImplementation();

                // Check Health
                checkHealthImplementation();
            }
            else
                deathImplementation();

            actions[currentAction].PlayAnimation(gameTime);
            oldState = newState;          
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            actions[currentAction].PlayAnimation(spriteBatch, position, currentSpriteEffect);
        }

        #endregion

    }
}
