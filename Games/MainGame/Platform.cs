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

        // Platform texture
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
            platformTexture = Content.Load<Texture2D>(@"background\block");
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

        int WindowWidth;
        int WindowHeight;

        #endregion

        #region Constructor

        public Platform(PlatpormContent platformTexture, Vector2 position, int windowWidth, int windowHeight)
        {
            this.platformTexture = platformTexture;
            this.position = position;

            WindowWidth = windowWidth;
            WindowHeight = windowHeight;

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
            return ((position.X >= 0 && position.X < WindowWidth) && (position.Y >= 0 && position.Y < WindowHeight));
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

        int WindowWidth;
        int WindowHeight;

        #endregion

        #region Constructor

        public PlatformList(PlatpormContent platformContent, int windowWidth, int windowHeight)
        {
            this.platformContent = platformContent;
            platformList = new List<Platform>();

            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
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
            Platform currentPlatform = new Platform(platformContent, position, WindowWidth, WindowHeight);
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
}