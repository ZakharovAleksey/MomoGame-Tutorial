using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkrollingScreen
{
    class Scrolling
    {
        // Left texture to diaplay and it's position
        Texture2D first;
        Rectangle firstRectangle;

        // Right texture to diaplay and it's position
        Texture2D second;
        Rectangle secondRectangle;

        // Velocity with witch screen scrolling
        int screenMoveVelocity = 10;

        /// <summary>
        /// Constrictor with one parameter 
        /// - set picture on background 
        /// </summary>
        /// <param name="texture"> Texture on background </param>
        public Scrolling(Texture2D texture)
        {
            // set first background
            first = texture;
            firstRectangle = new Rectangle(0, 0, first.Width, first.Height);

            // set second background
            second = texture;
            secondRectangle = new Rectangle(first.Width, 0, second.Width, second.Height);
        }

        /// <summary>
        /// Update the screen position by executing scrolling process
        /// </summary>
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                // Move screen with screen velocity 
                firstRectangle.X -= screenMoveVelocity;
                secondRectangle.X -= screenMoveVelocity;

                // Scrolling conditions
                if (firstRectangle.X + first.Width <= 0)
                    firstRectangle.X = secondRectangle.X + secondRectangle.Width;

                if (secondRectangle.X + second.Width <= 0)
                    secondRectangle.X = firstRectangle.X + firstRectangle.Width;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                // Move screen with screen velocity 
                firstRectangle.X += screenMoveVelocity;
                secondRectangle.X += screenMoveVelocity;

                // Scrolling conditions
                if (firstRectangle.X >= 0)
                    secondRectangle.X = firstRectangle.X - secondRectangle.Width;

                if (secondRectangle.X >= 0)
                    firstRectangle.X = secondRectangle.X - firstRectangle.Width;


            }
        }

        /// <summary>
        /// Draw screen
        /// </summary>
        /// <param name="spriteBatch"> sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(first, firstRectangle, Color.White);
            spriteBatch.Draw(second, secondRectangle, Color.White);
        }
    }
}
