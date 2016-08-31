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

namespace Animation
{
    class Animation
    {
        // Sprite
        protected Texture2D sprite;

        protected int frameCount;
        protected int currentFrame = 0;

        protected int currentExecutedMilliseconds = 0;
        protected int millisecondsPerFrame;

        public Animation(Texture2D newSprite, Vector2 newPosition, int newFrameCount, int newMillisecondsPerFrame)
        {
            sprite = newSprite;
            //position = newPosition;

            frameCount = newFrameCount;
            currentFrame = 0;

            millisecondsPerFrame = newMillisecondsPerFrame;
        }

        public int Y
        {
            get { return sprite.Height; }
        }

        public void Update(GameTime gametime)
        {
            currentExecutedMilliseconds += gametime.ElapsedGameTime.Milliseconds;

            if (currentExecutedMilliseconds >= millisecondsPerFrame)
            {
                currentFrame++;
                currentExecutedMilliseconds = 0;
                if (currentFrame >= frameCount)
                {
                    currentFrame = 0;
                }
            }
        }


        public void PlayAnimation(SpriteBatch spriteBatch, Vector2 currentPosition, SpriteEffects currentSpriteEffect)
        {
            // Calculate frame width and height
            int frameWigth = sprite.Width / frameCount;
            int frameHeight = sprite.Height;

            // Calculate position of current frame
            int currentSpriteRow = currentFrame / frameCount;
            int currentSpriteColl = currentFrame % frameCount;

            // Rectangle witch determ the current sprite Area in list of sprites
            Rectangle currentSpriteArea = new Rectangle(currentSpriteColl * frameWigth, currentSpriteRow * frameHeight, frameWigth, frameHeight);

            spriteBatch.Draw(sprite, new Rectangle( (int)currentPosition.X, (int) currentPosition.Y, frameWigth, frameHeight), currentSpriteArea, Color.White, 0.0f, new Vector2(50, 50), currentSpriteEffect, 0.0f);
        }
    }

}
