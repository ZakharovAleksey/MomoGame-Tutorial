using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerialization
{
    class Enemy
    {
        #region Fields

        Texture2D texture;
        Vector2 position;
        Vector2 velocity = new Vector2(GameConstants.rand.Next(-20,20)/ 100.0f, GameConstants.rand.Next(-20, 20) / 100.0f);

        bool isVissible = true;

        #endregion

        #region Constructor

        public Enemy()
        {
            position.X = GameConstants.WindowWidth;
            position.Y = GameConstants.rand.Next(0, GameConstants.WindowWidth);
        }

        public Enemy(Texture2D texture)
        {
            this.texture = texture;

            position.X = GameConstants.WindowWidth;
            position.Y = GameConstants.rand.Next(0, GameConstants.WindowWidth);
        }

        #endregion

        #region Properties

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
        }

        public bool IsVisible
        {
            get { return isVissible; }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(@"cube");
        }


        void BC()
        {
            // Bounce Back BC
            if (position.Y <= 0 || position.Y >= GameConstants.WindowHeight - texture.Height)
                velocity.Y *= -1.0f;
            if (position.X <= 0)
                isVissible = false;
        }

        public void Update(GameTime gameTime)
        {
            if (isVissible)
            {
                position += velocity * gameTime.ElapsedGameTime.Milliseconds;
                BC();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVissible)
            {
                spriteBatch.Draw(texture, this.Rectangle, Color.White);
            }
        }

        #endregion
    }
}
