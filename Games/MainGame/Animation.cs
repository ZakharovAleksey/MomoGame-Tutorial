using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame
{
    class Animation
    {
        #region Fields

        Texture2D texture;
        Vector2 position;

        int frameCount;
        int currentFrame;

        int millisecondsPerFrame;
        int currentExecutionTime;

        int frameWidth;
        int frameHeight;

        #endregion

        #region Constructor

        public Animation(Texture2D texture, Vector2 position, int frameCount, int millisecondsPerFrame)
        {
            this.texture = texture;
            this.position = position;
            this.frameCount = frameCount;
            this.millisecondsPerFrame = millisecondsPerFrame;


            frameWidth = texture.Width / frameCount;
            frameHeight = texture.Height;

            currentFrame = 0;
            currentExecutionTime = 0;
        }

        #endregion

        #region Properties

        public int X
        {
            get { return frameWidth; }
        }

        public int Y
        {
            get { return frameHeight; }
        }

        #endregion

        #region Methods

        public virtual void PlayAnimation(GameTime gameTime)
        {
            currentExecutionTime += gameTime.ElapsedGameTime.Milliseconds;
            if (currentExecutionTime >= millisecondsPerFrame)
            {
                currentExecutionTime = 0;
                ++currentFrame;
                if (currentFrame >= frameCount)
                    currentFrame = 0;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 currentPosition, SpriteEffects currentSpriteEffect = SpriteEffects.None)
        {
            // obtain position of current frame from sprite series
            Rectangle currentFramePosition = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);

            spriteBatch.Draw(
                texture,
                new Rectangle((int)currentPosition.X, (int)currentPosition.Y, frameWidth, frameHeight),
                currentFramePosition,
                Color.White,
                0.0f,   // rotation
                new Vector2(0, 0),   // origin : lockal coordinate system center (initially left upper corner of sprite rectangle)
                currentSpriteEffect,    // current sprite effect
                0.0f    // Layer depth [do not know]
            );
        }

        #endregion
    }
}
