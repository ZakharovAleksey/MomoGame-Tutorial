using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame
{
    /// <summary>
    /// Class store texture for platform
    /// </summary>
    class PlatpormContent
    {
        #region Fields

        Texture2D platformTexture;

        #endregion

        #region Constructor

        public PlatpormContent() { }

        #endregion

        #region Properties

        /// <summary>
        /// Get platform texture width
        /// </summary>
        public int X
        {
            get { return platformTexture.Width; }
        }

        /// <summary>
        /// Get platform texture height
        /// </summary>
        public int Y
        {
            get { return platformTexture.Height; }
        }

        /// <summary>
        /// Get platform texture
        /// </summary>
        public Texture2D Texture
        {
            get { return platformTexture; }
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager Content)
        {
            platformTexture = Content.Load<Texture2D>(@"background\platform");
        }

        #endregion

    }

    class Platform
    {
        #region Fields

        // Store texture for current platform
        PlatpormContent platformTexture;
        // Store current position of platform
        Vector2 position;
        // true if platform in screen
        bool visibility = new bool();   // Set initial value to false


        #endregion

        #region Constructor

        public Platform(PlatpormContent platformTexture, Vector2 position)
        {
            this.platformTexture = platformTexture;
            this.position = position;

            if (isVisible())
                visibility = true;
            
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get current platform state - visible / invisible
        /// </summary>
        public bool VISIBILITY
        {
            get { return visibility; }
            set { visibility = value; }
        }

        /// <summary>
        /// Get Platform Rectangle
        /// </summary>
        public Rectangle Rectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, platformTexture.X, platformTexture.Y); }
        }

        #endregion

        #region Method

        /// <summary>
        /// Check platform position on the screen
        /// </summary>
        /// <returns> true if is on the screen, false otherwise </returns>
        public bool isVisible()
        {
            return ((position.X >= 0 && position.X < GameConstants.WindowWidth) && (position.Y >= 0 && position.Y < GameConstants.WindowHeight));
        }

        /// <summary>
        /// Draw platform
        /// </summary>
        /// <param name="spriteBatch"> current sprite batch </param>
        public void DrawPlatform(SpriteBatch spriteBatch)
        {
            if (visibility)
                spriteBatch.Draw(platformTexture.Texture, new Rectangle((int)position.X, (int)position.Y, platformTexture.X, platformTexture.Y), Color.White);
        }

        #endregion

    }

    class PlatformList
    {
        #region Fields

        PlatpormContent platformContent;
        List<Platform> platformList;

        #endregion

        #region Constructor

        public PlatformList(PlatpormContent platformContent)
        {
            this.platformContent = platformContent;
            platformList = new List<Platform>();
        }

        #endregion

        #region Properties

        public int Count
        {
            get { return platformList.Count; }
        }

        #endregion

        #region Methods

        public Platform At(int platformID)
        {
            if (platformID >= 0 && platformID < platformList.Count)
                return platformList.ElementAt(platformID);
            else
                throw new IndexOutOfRangeException();
        }

        public bool Empty()
        {
            return (platformList.Count == 0);
        }

        public void AddPlatform(Vector2 position)
        {
            Platform currentPlatform = new Platform(platformContent, position);
            platformList.Add(currentPlatform);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < platformList.Count; ++i)
            {
                if (!platformList[i].isVisible())
                    platformList.RemoveAt(i);
            }

            foreach (Platform platform in platformList)
                platform.DrawPlatform(spriteBatch);

        }

        #endregion
    }

    static class RectangleHelper
    {
        public static bool TouchTopOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top - 1 &&
                    r1.Bottom <= r2.Top + (r2.Height / 2) &&
                    r1.Right >= r2.Left + r2.Width / 5 &&
                    r1.Left <= r2.Right - r2.Width / 5);
        }

        public static bool TouchBottomOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom + (r2.Height / 20) &&
                    r1.Top >= r2.Bottom - 1 &&
                    r1.Right >= r2.Left + (r2.Width / 5) &&
                    r1.Left <= r2.Right - (r2.Width / 5));
        }

        public static bool TouchLeftOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left <= r2.Right &&
                    r1.Left >= r2.Right - 5 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
        }

        public static bool TouchRightOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right >= r2.Left &&
                    r1.Right <= r2.Left + 5 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
        }
    }
}