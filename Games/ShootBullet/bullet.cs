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
        Texture2D[] actions;
        Vector2 position;
        Vector2 velocity;

        bool isVisible;

        int currentAction;
        int actionsCount;

        int WindowWidth;


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

        public bool IsVisible
        {
            set { isVisible = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public void LoadContent(ContentManager Content)
        {  
            for (int i = 0; i < actionsCount; ++i)
            {
               string fileName = @"sword\sword" + (i+1).ToString();
               actions[i] = Content.Load<Texture2D>(fileName);
            }
        }


        public void Update(GameTime gameTime)
        {
            if (isVisible)
            {
                position.X += velocity.X;
                ++currentAction;
                if (currentAction >= actionsCount)
                    currentAction = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(isVisible)
                spriteBatch.Draw(actions[currentAction], new Rectangle((int)position.X, (int)position.Y, actions[currentAction].Width, actions[currentAction].Height), Color.White);
        }

        //void DrowSetOfObjects( ref List<DropObject> objects, int objMaxCount)
        //{
        //    if (objects.Count < objMaxCount)
        //    {
        //        objects.Add(new DropObject(position, 6, WindowWidth));
        //        objects
        //    }

        //    foreach (DropObject obj in objects)
        //        if (position.X >= WindowWidth)
        //            obj.isVisible = false;

        //    for (int i = 0; i < objects.Count; ++i)
        //        if (!objects[i].isVisible)
        //        {
        //            objects.RemoveAt(i);
        //            --i;
        //        }
                
        //}   
    }

    
}
