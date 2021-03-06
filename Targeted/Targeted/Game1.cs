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

namespace Targeted
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteEffects sfx;

        Texture2D tank, square;
        Rectangle tankRct, sqrRct;

        float rotation = 0;
        const int DESIRED_SPEED = 7;

        int x, y;
        int shootx, shooty;
        bool shoot = false;

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
            this.IsMouseVisible = true;
            // TODO: Add your initialization logic here
            tankRct = new Rectangle(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, 50, 50);
            x = GraphicsDevice.Viewport.Width / 2 - 5;
            y = GraphicsDevice.Viewport.Height / 2 - 5;
            sqrRct = new Rectangle(x, y, 10, 10);

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
            tank = this.Content.Load<Texture2D>("Tank");
            square= this.Content.Load<Texture2D>("White Square");
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
            MouseState ms = Mouse.GetState();
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            int mouseXdist = ms.X - (GraphicsDevice.Viewport.Width / 2);
            int mouseYdist = ms.Y - (GraphicsDevice.Viewport.Height / 2);

            if (mouseXdist != 0 && ms.X >= 0 && ms.X <= GraphicsDevice.Viewport.Width && ms.Y >= 0 && ms.Y <= GraphicsDevice.Viewport.Height)
                rotation = (float)Math.Atan2(mouseXdist, mouseYdist);

            int dy = -1, dx = -1;

            if (ms.LeftButton == ButtonState.Pressed)
            {
                shoot = true;
                shootx = mouseXdist;
                shooty = mouseYdist;
                x = GraphicsDevice.Viewport.Width / 2 - 5;
                y = GraphicsDevice.Viewport.Height / 2 - 5;
            }

            if (shoot)
            {
                double hyp = Math.Sqrt(Math.Pow(shootx, 2) + Math.Pow(shooty, 2));
                double numUpdates = hyp / DESIRED_SPEED;
                dx = (int)(shootx / numUpdates);
                dy = (int)(shooty / numUpdates);
                x += dx;
                y += dy;
                sqrRct = new Rectangle(x, y, 10, 10);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            spriteBatch.Draw(square, sqrRct, Color.White);
            spriteBatch.Draw(tank, tankRct, null, Color.White, -rotation, new Vector2(115, 110), sfx, 0);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
