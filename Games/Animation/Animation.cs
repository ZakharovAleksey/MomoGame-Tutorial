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
    enum AnimationType
    {
        STAND,
        GO,
        RUN,
        FIGHT,
    }

    class Animation
    {
        protected Texture2D sprite;
        protected Rectangle positionRectangle;

        protected int frameCount;
        protected int currentFrame;

        protected int currentExecutedMilliseconds = 0;
        protected int millisecondsPerFrame;

        int WindowWidth;
        int WindowHeight;

        public Animation(Texture2D newSprite, Rectangle newPositionRectangle, int newFrameCount, int newMillisecondsPerFrame, int newWindowWidth, int newWindowHeight)
        {
            sprite = newSprite;
            positionRectangle = newPositionRectangle;
            frameCount = newFrameCount;
            currentFrame = 0;

            millisecondsPerFrame = newMillisecondsPerFrame;

            WindowWidth = newWindowWidth;
            WindowHeight = newWindowHeight;
        }

        virtual public void Update(GameTime gametime) { }
        virtual public void Draw(SpriteBatch spriteBatch) { }
    }

    class Warrior : Animation
    {

        #region Constructor

        public Warrior(Texture2D newSprite, Rectangle newPositionRectangle, int newFrameCount, int newMillisecondsPerFrame, int newWindowWidth, int newWindowHeight) :
            base(newSprite, newPositionRectangle, newFrameCount, newMillisecondsPerFrame, newWindowWidth, newWindowHeight)
        { }

        #endregion

        #region Methods

        void goRight()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                positionRectangle.X += 3;
            } 
            
        }

        public override void Update(GameTime gametime)
        {
            currentExecutedMilliseconds += gametime.ElapsedGameTime.Milliseconds;

            goRight();

            if (currentExecutedMilliseconds >= millisecondsPerFrame)
            {
                currentFrame++;
                currentExecutedMilliseconds = 0;
                if (currentFrame >= frameCount)
                {
                    currentFrame = 0;
                }
            }
            base.Update(gametime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Calculate frame width and height
            int frameWigth = sprite.Width / frameCount;
            int frameHeight = sprite.Height;

            // Calculate position of current frame
            int currentSpriteRow = currentFrame / frameCount;
            int currentSpriteColl = currentFrame % frameCount;

            // Rectangle witch determ the current sprite Area in list of sprites
            Rectangle currentSpriteArea = new Rectangle(currentSpriteColl * frameWigth, currentSpriteRow * frameHeight, frameWigth, frameHeight);

            spriteBatch.Draw(sprite, positionRectangle, currentSpriteArea, Color.White); //, 0.0f, new Vector2(50, 50), SpriteEffects.None, 0.0f);
        }

        #endregion
    }
}
