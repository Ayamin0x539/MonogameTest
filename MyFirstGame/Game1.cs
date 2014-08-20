#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace MyFirstGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
       
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Private member variables to store our images
        private Texture2D background, shuttle, earth;

        // Private member variables for our fonts
        private SpriteFont font;

        // Our score:
        private int score = 0;

        private int xpos;
        private int ypos;
        private int shipX = 450, shipY = 240;
        private bool lost = false;

        // MouseState variable
        private MouseState oldState;
        // KeyboardState variable
        private KeyboardState oldkbdState;
        

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() // once, before loop
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() // once, before loop
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // Texture2D image = Content.Load<Texture2D>("kakuna");
            background = Content.Load<Texture2D>("stars");
            shuttle = Content.Load<Texture2D>("shuttle");
            earth = Content.Load<Texture2D>("earth");
            // These three variables are now loaded with the images we have in our Content folder and ready to be drawn.
            // THEY ARE NOT DRAWN YET. 

            // Now we'll load the spriteFont.
            font = Content.Load<SpriteFont>("score");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            MouseState newState = Mouse.GetState();
            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                xpos = newState.X;
                ypos = newState.Y;
            }
            KeyboardState newkbdState = Keyboard.GetState();
            if (newkbdState.IsKeyDown(Keys.W) && oldkbdState.IsKeyUp(Keys.W))
            {
                shipY -= 50;
                score += (480 - shipY);
            }
            if (newkbdState.IsKeyDown(Keys.A) && oldkbdState.IsKeyUp(Keys.A))
            {
                shipX -= 50;
                score += (480 - shipY);
            }
            if (newkbdState.IsKeyDown(Keys.S) && oldkbdState.IsKeyUp(Keys.S))
            {
                shipY += 50;
                score += (480 - shipY);
            }
            if (newkbdState.IsKeyDown(Keys.D) && oldkbdState.IsKeyUp(Keys.D))
            {
                shipX += 50;
                score += (480 - shipY);
            }
            //oldState = newState;
            oldkbdState = newkbdState;
            base.Update(gameTime);
            shipY++;
            if (shipY > 480)
            {
                lost = true;
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here


            spriteBatch.Begin();
            // Draw Sprites
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White); // SpriteBatch.Draw(Texture2D, Rectangle*, Tint); //(Color.White = no tint)
            spriteBatch.Draw(earth, new Vector2(400, 240), Color.White); // the upper left corner of "earth" will be placed at (400, 240)
            spriteBatch.Draw(shuttle, new Vector2(shipX, shipY), Color.White);
            // Draw font
            spriteBatch.DrawString(font, "Score : " + score, new Vector2(100, 100), Color.HotPink);
            spriteBatch.DrawString(font, "X - position: " + xpos, new Vector2(100, 150), Color.HotPink);
            spriteBatch.DrawString(font, "Y - position: " + ypos, new Vector2(100, 200), Color.HotPink);
            if (lost)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.DrawString(font, "YOU LOSE!", new Vector2(400, 200), Color.Red);
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }

        // Do not do any drawing in the Update() method, nor do any updating in the Draw() method.
    }
}
