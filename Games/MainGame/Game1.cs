using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
 * Problem: c# the source file is different from when the module was built.
 * 
 * In Configuration Manager, my start-up project didn't have "Build" checked
 */

namespace MainGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player droid;

        PlatpormContent platformContent;
        PlatformList platformList;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = GameConstants.WindowWidth;
            graphics.PreferredBackBufferHeight = GameConstants.WindowHeight;

        }

        protected override void Initialize()
        {

            platformContent = new PlatpormContent();

            platformList = new PlatformList(platformContent);
            
            platformList.AddPlatform(new Vector2(100, GameConstants.WindowHeight - 100));
            platformList.AddPlatform(new Vector2(400, GameConstants.WindowHeight - 200));

            droid = new Player(new Vector2(GameConstants.WindowWidth / 2, GameConstants.WindowHeight - 100), 5, platformList);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            droid.LoadContent(Content);

            platformContent.LoadContent(Content);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            droid.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            droid.Draw(spriteBatch);

            platformList.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
