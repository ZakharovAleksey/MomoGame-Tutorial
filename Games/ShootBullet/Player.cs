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
        List<DropObject> swords;

        public Player(Vector2 position)
        {
            actions = new Animation[3];
            currentAction = (int)ActionType.IDLE;

            this.position = position;
            velocity = new Vector2();
            shoots = new List<SingleShoot>();
            // SWORD
            swords = new List<DropObject>();
        }

        public void LoadContent(ContentManager Content)
        {
            actions[(int)ActionType.IDLE] = new Animation(Content.Load<Texture2D>(@"droid\idle"), position, 3, 100);
            actions[(int)ActionType.GO] = new Animation(Content.Load<Texture2D>(@"droid\go"), position, 3, 100);
            actions[(int)ActionType.FIGHT] = new Animation(Content.Load<Texture2D>(@"droid\attack"), position, 3, 100);

            // shoot
            singleShoot = Content.Load<Texture2D>(@"singleShoot");
            sword = new DropObject(position, 6, 800);
            sword.LoadContent(Content);
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

        // shoot 
        void shootUpdate(GameTime gameTime)
        {
            foreach(SingleShoot shoot in shoots)
                shoot.Update(gameTime);
            for(int i = 0; i < shoots.Count; ++i)
            {
                if (!shoots[i].IsVisible)
                {
                    shoots.RemoveAt(i);
                    --i;
                }
            }
        }

        void fightImplementation(GameTime gameTime)
        {
            currentAction = (int)ActionType.FIGHT;
            // More if we want to do  shoot
            //if(shoots.Count < 1)
            //    shoots.Add(new SingleShoot(singleShoot, position, 800));

            //sword work for one
            sword.IsVisible = true;
            sword.Update(gameTime);

            // update sward
            //sword.IsVisible
        }
        

        public void Update(GameTime gameTime)
        {
            position += velocity * gameTime.ElapsedGameTime.Milliseconds;
            sword.Position += velocity * gameTime.ElapsedGameTime.Milliseconds;

            idleImplementation();

            state = Keyboard.GetState();

            // Good DONE GO AND FIGHT IN THE SAME TIME ALGORITHM
            if (state.IsKeyDown(Keys.Right))
                forvardMovementImplementation();
            if (state.IsKeyDown(Keys.A))
                fightImplementation(gameTime);
            
            //foreach (SingleShoot shoot in shoots)
            //    shoot.Update(gameTime);
            //shootUpdate(gameTime);
            //sword.Update(gameTime);
            actions[currentAction].PlayAnimation(gameTime);
            sword.Update(gameTime);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            actions[currentAction].Draw(spriteBatch, position);
            sword.Draw(spriteBatch);
            //foreach (SingleShoot shoot in shoots)
            //    shoot.Draw(spriteBatch);
        }
    }
}
