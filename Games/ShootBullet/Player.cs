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
        FIGHT
    }

    class Player
    {
        Animation[] actions;

        int currentAction;

        Vector2 position;
        Vector2 velocity;

        float velocityX = 0.15f;

        KeyboardState state;

        // SingleShoot 
        Texture2D singleShoot;
        List<SingleShoot> shoots;

        // SWORD
        DropObject sword;

        // swords
        //DropObject[] swords;
        //int size = 2;

        public Player(Vector2 position)
        {
            actions = new Animation[3];
            currentAction = (int)ActionType.IDLE;

            this.position = position;
            velocity = new Vector2();
        }

        public void LoadContent(ContentManager Content)
        {
            actions[(int)ActionType.IDLE] = new Animation(Content.Load<Texture2D>(@"droid\idle"), position, 3, 100);
            actions[(int)ActionType.GO] = new Animation(Content.Load<Texture2D>(@"droid\go"), position, 3, 100);
            actions[(int)ActionType.FIGHT] = new Animation(Content.Load<Texture2D>(@"droid\attack"), position, 3, 100);

            // sword
            sword = new DropObject(position, 6, 800);
            sword.LoadContent(Content);

            //swords
            //swords = new DropObject[size];
            //for (int i = 0; i < size; ++i)
            //{
            //    swords[i] = new DropObject(position, 6, 800);
            //    swords[i].LoadContent(Content);
            //}

        }

        void idleImplementation()
        {
            velocity = new Vector2();
            currentAction = (int)ActionType.IDLE;
        }

        void forvardMovementImplementation()
        {
            velocity.X = velocityX;
            currentAction = (int)ActionType.GO;
        }

        void fightImplementation(GameTime gameTime)
        {
            currentAction = (int)ActionType.FIGHT;

            //sword 
            sword.IsVisible = true;
        }
        

        public void Update(GameTime gameTime)
        {
            position += velocity * gameTime.ElapsedGameTime.Milliseconds;
            //sword
            sword.Position += velocity; // Чтобы летел с постоянной скоростью не зависящей от скорости чувака

            idleImplementation();

            state = Keyboard.GetState();

            // Good DONE GO AND FIGHT IN THE SAME TIME ALGORITHM
            if (state.IsKeyDown(Keys.Right))
                forvardMovementImplementation();
            if (state.IsKeyDown(Keys.A))
                fightImplementation(gameTime);

            actions[currentAction].PlayAnimation(gameTime);
            // sword
            sword.Update(gameTime, position);

            // swords
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            actions[currentAction].Draw(spriteBatch, position);
            //sword
            sword.Draw(spriteBatch);

            // swords

        }
    }
}
