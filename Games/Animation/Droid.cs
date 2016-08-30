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
        float jump = 80.0f;
        bool couldJump = true;

        // health
        int health = 100;
        bool live = true;

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
        }

        void goLeftImplementation()
        {
            velocity.X = -vx;
            currentAction = (int)ActionType.GET_DAMAGE;
        }

        void stayImplementation()
        {
            velocity = new Vector2();
            currentAction = (int) ActionType.IDLE;
        }

        void jumpImplementation()
        {
            if (couldJump)
            {
                position.Y -= jump;
                couldJump = false;
            }
        }

        void attackImplementation()
        {
            if (position.Y < WindowHeight - actions[currentAction].Y)
                currentAction = (int)ActionType.JUMP_ATTACK;
            else
                currentAction = (int) ActionType.ATTACK;
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
            if (position.Y < WindowHeight - actions[currentAction].Y)
                position.Y += g;
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            if (live)
            {
                position += velocity;

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    goRightImplementation();
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    goLeftImplementation();
                else if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    jumpImplementation();
                else if (Keyboard.GetState().IsKeyDown(Keys.P))
                    attackImplementation();
                else
                    stayImplementation();

                bottomBCImplementation();

                checkHealthImplementation();
            }
            else
                deathImplementation();

            actions[currentAction].Update(gameTime);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            actions[currentAction].PlayAnimation(spriteBatch, position);

        }

        #endregion

    }
}
