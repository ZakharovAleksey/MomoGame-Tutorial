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
        Texture2D texture;
        Rectangle position;

        int framesCount;

        int millisecondaPerFrame;
        int currentGameTime = 0;

        int currentFrame = 0;

        public Animation(Texture2D newTexture, Rectangle newRectangle, int newFramesCount, int newMillisecobdsPerFrame)
        {
            //texture = newTexture;
            //rectangle = newRectangle;
            //framesCount = newFramesCount;
            //millisecondaPerFrame = newMillisecobdsPerFrame;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Rectangle destinationRectangle = new Rectangle(rectangle.X, rectangle.Y, texture.Width / framesCount, texture.Height);
            //Rectangle sourceRectangle = new Rectangle()

            //spriteBatch.Draw(texture, )
        }

    }

    class AnimationExecution
    {
        Animation animation;

        AnimationExecution(Animation newAnimation, int newMillisecondsPerFrame)
        {
            animation = newAnimation;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            

        }
    }
}
