using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation
{
    class Animation
    {
        protected Texture2D sprite;
        protected Rectangle positionRectangle;

        protected int frameCount;
        protected int currentFrame;

        protected int currentExecutedMilliseconds = 0;
        protected int millisecondsPerFrame;

        public Animation(Texture2D newSprite, Rectangle newPositionRectangle, int newFrameCount, int newMillisecondsPerFrame)
        {
            sprite = newSprite;
            positionRectangle = newPositionRectangle;
            frameCount = newFrameCount;
            currentFrame = 0;

            millisecondsPerFrame = newMillisecondsPerFrame;
        }

        virtual public void Update(GameTime gametime) { }
        virtual public void Draw(SpriteBatch spriteBatch) { }
    }

    class Warrior : Animation
    {
        Warrior(Texture2D newSprite, Rectangle newPositionRectangle, int newFrameCount, int newMillisecondsPerFrame) :
            base(newSprite, newPositionRectangle, newFrameCount, newMillisecondsPerFrame)
        { }

        public override void Update(GameTime gametime)
        {
            currentExecutedMilliseconds = gametime.ElapsedGameTime.Milliseconds;
            if (currentExecutedMilliseconds >= millisecondsPerFrame)
            {
                currentFrame++;
                currentExecutedMilliseconds = 0;
                if (currentFrame > frameCount)
                {
                    currentFrame = 0;
                }
            }
            base.Update(gametime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle positionOnSprite = new Rectangle(sprite.Width * currentFrame, sprite.Height, sprite.Width, sprite.Height);
            spriteBatch.Draw(sprite, positionRectangle, positionOnSprite, Color.White);

            base.Draw(spriteBatch);
        }
    }
}
