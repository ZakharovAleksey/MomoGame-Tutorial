using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootBullet
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

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 currentPosition)
        {
            Rectangle currentFramePosition = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);

            spriteBatch.Draw(texture, new Rectangle( (int) currentPosition.X, (int) currentPosition.Y, frameWidth, frameHeight), currentFramePosition, Color.White);
        }

        #endregion
    }
}
