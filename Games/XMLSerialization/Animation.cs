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
    class Animation
    {
        #region Fields

        Texture2D Texture { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }

        int currentSpriteID = 0;
        int spriteCount { get;}

        int currentSpriteExecutionTime = 0;

        #endregion

        #region Constructor

        public Animation(ContentManager Content, string path, int spriteCount)
        {
            Texture = Content.Load<Texture2D>(path);
            this.spriteCount = spriteCount;

            Height = Texture.Height;
            Width = Texture.Width / this.spriteCount;
        }

        #endregion

        #region Properties

        Rectangle SpriteRectangle
        {
            get { return new Rectangle(currentSpriteID * Width, 0, Width, Height); }
        }

        #endregion

        #region Methods

        public void InitilizeAnimation(ContentManager Content, string path)
        {
            Texture = Content.Load<Texture2D>(path);
        }

        /// <summary>
        /// Update animation
        /// </summary>
        /// <param name="gameTime"> Current game time </param>
        /// <param name="isMoving"> true is object is moving, false otherwise </param>
        public void UpdateAnimation(GameTime gameTime, bool isMoving)
        {
            if (isMoving)
            {
                currentSpriteExecutionTime += gameTime.ElapsedGameTime.Milliseconds;
                if (currentSpriteExecutionTime >= GameConstants.MillisecondsPerSprite)
                {
                    currentSpriteExecutionTime = 0;
                    ++currentSpriteID;
                    if (currentSpriteID >= spriteCount)
                        currentSpriteID = 0;
                }
            }
            else
            {
                currentSpriteID = 1;
            }

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);
            spriteBatch.Draw(Texture, destinationRectangle, SpriteRectangle, Color.White, 0.0f, new Vector2(), SpriteEffects.None, 0.0f);
        }


        #endregion
    }
}
