using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootBullet
{
    class SingleShoot
    {
        Texture2D shootTexture;

        Vector2 position;
        Vector2 velocity;

        bool isVisible;

        int WindowWidth;

        public SingleShoot(Texture2D shootTexture, Vector2 initialShootPosition, int windowWidth)
        {
            this.shootTexture = shootTexture;
            position = initialShootPosition;

            velocity = new Vector2(0.1f, 0);
            isVisible = true;

            WindowWidth = windowWidth;
        }


        public bool IsVisible
        {
            get { return isVisible; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
                spriteBatch.Draw(shootTexture, new Rectangle((int)position.X, (int)position.Y, shootTexture.Width, shootTexture.Height), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (position.X >= WindowWidth / 2)
                isVisible = false;
            else
                position.X += velocity.X * gameTime.ElapsedGameTime.Milliseconds;
        }
    }

    class DropObject
    {
        #region Fields

        Texture2D[] actions;
        Vector2 position;
        Vector2 velocity;

        bool isVisible;

        int currentAction;
        int actionsCount;

        int WindowWidth;

        #endregion

        #region Constructor

        public DropObject(Vector2 position, int actionsCount, int windowWidth)
        {
            this.position = position;
            this.actionsCount = actionsCount;
            actions = new Texture2D[this.actionsCount];

            isVisible = false;
            velocity = new Vector2(5, 5);
            currentAction = 0;

            WindowWidth = windowWidth;
        }

        #endregion

        #region Properties

        public bool IsVisible
        {
            set { isVisible = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager Content)
        {  
            for (int i = 0; i < actionsCount; ++i)
            {
               string fileName = @"sword\sword" + (i+1).ToString();
               actions[i] = Content.Load<Texture2D>(fileName);
            }
        }

        public void Update(GameTime gameTime, Vector2 spritePosition)
        {
            if (isVisible)
            {
                // while current object in the screen set it to the next animation frame
                position.X += velocity.X;
                ++currentAction;

                // If all droping animation complete then repeat it
                if (currentAction >= actionsCount)
                    currentAction = 0;

                // If current object leaves screen 
                if(position.X > WindowWidth)
                {
                    // Set it invisible beacause we did not drop it yet
                    isVisible = false;
                    // Set initial position for droping is equal to current sprite position
                    position = spritePosition;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(isVisible)
                spriteBatch.Draw(actions[currentAction], new Rectangle((int)position.X, (int)position.Y, actions[currentAction].Width, actions[currentAction].Height), Color.White);
        }

        #endregion
    }
}
