using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MoveImave
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Set Window size
        const int WindowWidth = 800;
        const int WindowHeight = 600;


        // Texture and it's position
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;

        Vector2 velocityValue = new Vector2(5, 2);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            texture = Content.Load<Texture2D>(@"cat\slide");
            position = new Vector2(50, 200);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                position.X -= velocityValue.X;
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                position.X += velocityValue.X;
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                position.Y -= velocityValue.Y;
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                position.Y += velocityValue.Y;

            // check on boundary condition
            if (position.X < 0)
                position.X = 0;
            if (position.X > WindowWidth - texture.Width)
                position.X = WindowWidth - texture.Width;
            if (position.Y < 0)
                position.Y = 0;
            if (position.Y > WindowHeight - texture.Height)
                position.Y = WindowHeight - texture.Height;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
