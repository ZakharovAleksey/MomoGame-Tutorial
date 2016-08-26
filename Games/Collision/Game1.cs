using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Collision
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WindowWight = 800;
        const int WindowHeight = 500;

        Texture2D ball;
        Rectangle ballRectangle;
        Vector2 ballVelocity = new Vector2(4, 4);

        Texture2D block;
        Rectangle blockRectangle;
        Vector2 blockVelocity = new Vector2(5, 0);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            ball = Content.Load<Texture2D>(@"ball\ball");
            ballRectangle = new Rectangle((WindowWight - ball.Width) / 2, 0, ball.Width, ball.Height);

            block = Content.Load<Texture2D>(@"ball\block");
            blockRectangle = new Rectangle((WindowWight - block.Width) / 2, WindowHeight - block.Height - 50, block.Width, block.Height);

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
            ballRectangle.X += (int) ballVelocity.X;
            ballRectangle.Y += (int)ballVelocity.Y;

            // Ball boundary condition
            if (ballRectangle.X < 0)
                ballVelocity.X *= -1;
            if (ballRectangle.X > WindowWight - ball.Width)
                ballVelocity.X *= -1;
            if (ballRectangle.Y < 0)
                ballVelocity.Y *= -1;
            //if (ballRectangle.Y > WindowHeight - ball.Height)
            //    ballVelocity.Y *= -1;

            // Block movement
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                blockRectangle.X -= (int)blockVelocity.X;
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                blockRectangle.X += (int)blockVelocity.X;

            // Block boundary condition
            if (blockRectangle.X < 0)
                blockRectangle.X = 0;
            if (blockRectangle.X > WindowWight - block.Width)
                blockRectangle.X = WindowWight -block.Width;

            // collision
            if (ballRectangle.Intersects(blockRectangle))
                ballVelocity.Y *= -1;

            // Exit to loose
            if (ballRectangle.Y > WindowHeight - ball.Height)
                Exit();

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

            spriteBatch.Draw(ball, ballRectangle, Color.White);
            spriteBatch.Draw(block, blockRectangle, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
