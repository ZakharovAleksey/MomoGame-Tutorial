using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootBullet
{
    class Platform
    {
        #region Fields

        Texture2D texture;
        Vector2 position;

        bool isVisible;
        #endregion

        #region Constructors

        public Platform(Texture2D texture, Vector2 position)
        {
            this.texture = texture;

            this.position = position;
            isVisible = true;
        }

        #endregion

        #region Properties

        public bool IsVisible
        {
            get { return isVisible; }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        #endregion

        #region Methods

        public bool Intersect(Platform other)
        {
            return (new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height).Intersects(
                new Rectangle((int)other.position.X, (int)other.position.Y, other.texture.Width, other.texture.Height) ) ) ? true : false;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(@"background\block");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(isVisible)
                spriteBatch.Draw(texture, new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height), Color.White);
        }

        #endregion

    }

    class PlatformSet
    {
        #region Fields
        Platform template;

        // List of all possible platforms
        List<Platform> platformList;

        #endregion

        #region Constructor

        public PlatformSet(Platform platformTemplate)
        {
            template = platformTemplate;
            platformList = new List<Platform>();
        }

        #region Constructor


        #region Methods

        public void LoadContent(ContentManager Content)
        {
            template.LoadContent(Content);
        }

        /// <summary>
        /// Add another platform to the platform set
        /// </summary>
        /// <param name="position"> position of currentlly added platform </param>
        void AddPlatform(Texture2D texture, Vector2 position)
        {
            // If it is first platform
            if (platformList.Count == 0)
                platformList.Add(new Platform(template.Texture, position));
            else
            {
                // true - if any of all position intersects with current
                bool isIntersect = false;
                foreach (Platform platform in platformList)
                    if (platform.Intersect(new Platform(template.Texture, position)))
                        isIntersect = true;

                // If no one of currently existed platform intersect with this one add it to the set
                if (!isIntersect)
                    platformList.Add(new Platform(template.Texture, position));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Platform platform in platformList)
                platform.Draw(spriteBatch);
        }

        #endregion

    }

}
