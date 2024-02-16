using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace PongGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PongGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Game Data

        // Textures for the ball and the paddles
        Texture2D ballTexture;
        Texture2D lPaddleTexture;
        Texture2D rPaddleTexture;

        // Rectangles for the ball and the paddles
        Rectangle rPaddleRectangle;
        Rectangle lPaddleRectangle;
        Rectangle ballRectangle;

        // Speed of the ball
        int ballXSpeed = 5;
        int ballYSpeed = 5;

        // Distance of the paddle from the edge of the screen
        int margin;

        public PongGame()
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

            ballTexture = Content.Load<Texture2D>("ball");
            lPaddleTexture = Content.Load<Texture2D>("lpaddle");
            rPaddleTexture = Content.Load<Texture2D>("rpaddle");

            margin = Window.ClientBounds.Width / 40;

            ballRectangle = new Rectangle(
              50, 50,
              Window.ClientBounds.Width / 20,
              Window.ClientBounds.Width / 20);

            lPaddleRectangle = new Rectangle(
              margin, 0,
              Window.ClientBounds.Width / 20,
              Window.ClientBounds.Height / 6);

            rPaddleRectangle = new Rectangle(
              Window.ClientBounds.Width - lPaddleRectangle.Width - margin, 0,
              Window.ClientBounds.Width / 20,
              Window.ClientBounds.Height / 6);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            ballRectangle.X += ballXSpeed;

            if (ballRectangle.X < 0 ||
                ballRectangle.X + ballRectangle.Width > GraphicsDevice.Viewport.Width)
            {
                ballXSpeed = -ballXSpeed;
            }

            ballRectangle.Y += ballYSpeed;

            if (ballRectangle.Y < 0 ||
                ballRectangle.Y + ballRectangle.Height > GraphicsDevice.Viewport.Height)
            {
                ballYSpeed = -ballYSpeed;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(ballTexture, ballRectangle, Color.White);
            spriteBatch.Draw(lPaddleTexture, lPaddleRectangle, Color.White);
            spriteBatch.Draw(rPaddleTexture, rPaddleRectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
