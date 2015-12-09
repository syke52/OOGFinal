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


namespace TerrorAndAdventure
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    /// private SpriteBatch spriteBatch;
    public class PlayerToken : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed = new Vector2(0, 0);

        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = 0;
        private int delay;
        private int delayCounter;
        private int fIndex = 0;
        private int lIndex = 2;

        public PlayerToken(Game game, SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            int delay)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.delay = delay;
            dimension = new Vector2(49, 49);
            //call createFrames()
            createFrames();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);

                    frames.Add(r);
                }
            }
        }
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.S))
            {
                fIndex = 0;
                lIndex = 2;
                speed.Y = 2;
                speed.X = 0;

                delayCounter++;
            }
            else if (ks.IsKeyDown(Keys.A))
            {
                fIndex = 3;
                lIndex = 5;
                speed.Y = 0;
                speed.X =-2;
                delayCounter++;
            }
            else if (ks.IsKeyDown(Keys.D))
            {
                fIndex = 6;
                lIndex = 8;
                speed.Y = 0;
                speed.X = 2;
                delayCounter++;
            }
            else if (ks.IsKeyDown(Keys.W))
            {
                fIndex = 9;
                lIndex = 11;
                speed.Y = -2;
                speed.X = 0;
                
                delayCounter++;
            }
            else
            {
                speed = Vector2.Zero;
            }
            if (frameIndex > lIndex || frameIndex < fIndex)
            {
                frameIndex = fIndex;
            }
            
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > lIndex)
                {
                    frameIndex = fIndex;
                }
                delayCounter = 0;

            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //version 4
            if (frameIndex >= 0)
            {
                position += speed;
                spriteBatch.Draw(tex, position, frames.ElementAt<Rectangle>(frameIndex), Color.White);

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X-12, (int)position.Y-39, 25, 10);
        }
    }
}