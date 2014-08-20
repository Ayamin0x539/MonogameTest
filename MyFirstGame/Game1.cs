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
        //  private Texture2D background, shuttle, earth, ball;
        // We'll use the Sprite class instead.

        private Sprite background, shuttle, earth, ball, pinkpoint;

        // Private member variables for our fonts
        private SpriteFont font;
        private SpriteFont lose;

        // Our score:
        private int score = 0;

        // The coordinate linked list classes
        private Coordinate first, current;

        // Mouse coordinates
        private int xpos;
        private int ypos;
        // private int shipX = 450, shipY = 240; // this information will be in the Sprite class for shuttle
        private bool lost = false;
        bool beginBallCoordinateAssignment = false;
        bool ballCoordinateAssignment = false;
        bool ballDrawing = false;

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

            // initialize the sprites:
            background = new Sprite(Color.White);
            shuttle = new Sprite(new Vector2(450, 240), Color.White);
            earth = new Sprite(new Vector2(400, 240), Color.White);
            ball = new Sprite(new Vector2(400, 100), Color.White);

            // We'll use this for the line
            pinkpoint = new Sprite(Color.White);

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
            background.image = Content.Load<Texture2D>("stars");
            shuttle.image = Content.Load<Texture2D>("shuttle");
            earth.image = Content.Load<Texture2D>("earth");
            ball.image = Content.Load<Texture2D>("redsmall");
            pinkpoint.image = Content.Load<Texture2D>("pink5x5"); // our "line"
            // These three variables are now loaded with the images we have in our Content folder and ready to be drawn.
            // THEY ARE NOT DRAWN YET. 

            // Now we'll load the spriteFont.
            font = Content.Load<SpriteFont>("score");
            lose = Content.Load<SpriteFont>("Lose");
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
            //if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            //{
            //    xpos = newState.X;
            //    ypos = newState.Y;
            //}

            if (newState.LeftButton == ButtonState.Pressed)
            {
                xpos = newState.X;
                ypos = newState.Y;
            }

            // Calculate distance from ball
            // distance = sqrt( (x1-x2)^2 + (y2 - y1)^2)
            // we use ball.position.X + 50 and ball.position.Y + 50 because the ball is 100 pixels and we want to calculate the distance from the middle of it... :

            float distance = (float)Math.Sqrt(Math.Pow(newState.X - (ball.position.X + 50), 2) + Math.Pow(newState.Y - (ball.position.Y + 50), 2)); 
            if (newState.LeftButton == ButtonState.Pressed && (distance < 60) && !ballCoordinateAssignment)
            {
                // We only need to do this part once.

                first = new Coordinate(xpos, ypos); // we need to keep track of the first node when we draw. we only need to store it once so it doesn't update constantly while mouse is pressed down.
                current = first;
                ballCoordinateAssignment = true; // make sure to turn this off at the end when the last pixel is filled by the ball
            }
            if(newState.LeftButton == ButtonState.Pressed && (ballCoordinateAssignment))
            {
                xpos = newState.X;
                ypos = newState.Y;
                if (current.xpoint == xpos && current.ypoint == ypos)
                {
                    // we'll skip the below in the if/else ladder if we're pausing in the same position with the mouse. no need to travel to the same coordinate over and over
                    // and we definitely don't need to be making more instances of Coordinate than necessary.
                }
                else
                {
                    current.next = new Coordinate(xpos, ypos); // the next node will have the new coordinates
                    current = current.next; // the next node will now be the current one.
                }                
            }

            if (newState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed && ballCoordinateAssignment) // on mouse up...
            {
                current.next = null; // we will check for this null node during drawing; if we find it, we'll stop drawing and turn ballDrawing to 0.
                ballCoordinateAssignment = false; // we're done assigning the coordinates.
                ballDrawing = true; // now we begin drawing.
                current = first; // we'll set the current back to first (we only need to do this once, and this block is the perfect place to do it.)
            }
            KeyboardState newkbdState = Keyboard.GetState();
            if (newkbdState.IsKeyDown(Keys.W) && oldkbdState.IsKeyUp(Keys.W))
            {
                // shipY -= 50;
                shuttle.position.Y -= 50;
                score += (int)(480 - shuttle.position.Y);
            }
            if (newkbdState.IsKeyDown(Keys.A) && oldkbdState.IsKeyUp(Keys.A))
            {
                shuttle.position.X -= 50;
                score += (int)(480 - shuttle.position.Y);
            }
            if (newkbdState.IsKeyDown(Keys.S) && oldkbdState.IsKeyUp(Keys.S))
            {
                shuttle.position.Y += 50;
                score += (int)(480 - shuttle.position.Y);
            }
            if (newkbdState.IsKeyDown(Keys.D) && oldkbdState.IsKeyUp(Keys.D))
            {
                shuttle.position.X += 50;
                score += (int)(480 - shuttle.position.Y);
            }
            oldState = newState;
            oldkbdState = newkbdState;
            base.Update(gameTime);
            shuttle.position.Y++;
            if (shuttle.position.Y > 480)
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
            if (lost)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();
                spriteBatch.DrawString(lose, "YOU LOSE!", new Vector2(150, 150), Color.Red);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                // Draw Sprites
                //spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White); // SpriteBatch.Draw(Texture2D, Rectangle*, Tint); //(Color.White = no tint)
                //spriteBatch.Draw(earth, new Vector2(400, 240), Color.White); // the upper left corner of "earth" will be placed at (400, 240)
                //spriteBatch.Draw(shuttle, new Vector2(shipX, shipY), Color.White);
                //spriteBatch.Draw(ball, new Vector2(400, 100), Color.White);

                // With Sprite class:
                spriteBatch.Draw(background.image, new Rectangle(0, 0, 800, 480), background.color);
                spriteBatch.Draw(earth.image, earth.position, earth.color);
                spriteBatch.Draw(shuttle.image, shuttle.position, shuttle.color);
                spriteBatch.Draw(ball.image, ball.position, ball.color);
                // Draw font
                spriteBatch.DrawString(font, "Score : " + score, new Vector2(100, 100), Color.HotPink);
                spriteBatch.DrawString(font, "X - position: " + xpos, new Vector2(100, 150), Color.HotPink);
                spriteBatch.DrawString(font, "Y - position: " + ypos, new Vector2(100, 200), Color.HotPink);

                spriteBatch.End();

                if (ballCoordinateAssignment)
                {
                    pinkpoint.position.X = current.xpoint;
                    pinkpoint.position.Y = current.ypoint;

                    spriteBatch.Begin();
                    spriteBatch.Draw(pinkpoint.image, new Vector2(pinkpoint.position.X, pinkpoint.position.Y), pinkpoint.color);
                    spriteBatch.End();
                }
                if (ballDrawing)
                {
                    // Update the ball's coordinates according to the path we drew:
                    ball.position.X = current.xpoint - 50;
                    ball.position.Y = current.ypoint - 50;
                    // minus 50 because we want the center of the ball to follow the path, not the top left corner of it

                    spriteBatch.Begin();
                    spriteBatch.Draw(ball.image, ball.position, ball.color);
                    spriteBatch.End();

                    if (current.next == null)
                    {
                        ballDrawing = false; // we'll end it here if the next node doesn't have any coordinates (we reached the end of the path)
                    }
                    else
                    {
                        current = current.next; // otherwise, we'll set the next node to be the current on and continue on drawing the ball's motion
                    }
                }
            }


            base.Draw(gameTime);
        }

        // Do not do any drawing in the Update() method, nor do any updating in the Draw() method.
    }
}
