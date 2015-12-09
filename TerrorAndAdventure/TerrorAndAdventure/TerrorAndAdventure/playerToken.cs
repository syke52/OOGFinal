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
        private int wDelay = 0;
        private int wFrame; 

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
        private bool custom = false;
        private int[] cFrames;
        private int[] cDelay;
        private int cMaxIndex;
        private int cIndex = 0;
        private Vector2[] cSpeed;

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
            if (wDelay<0 && !custom)
            {
                if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
                {
                    fIndex = 0;
                    lIndex = 2;
                    speed.Y = 3;
                    speed.X = 0;

                    delayCounter++;
                }
                else if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
                {
                    fIndex = 3;
                    lIndex = 5;
                    speed.Y = 0;
                    speed.X = -3;
                    delayCounter++;
                }
                else if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
                {
                    fIndex = 6;
                    lIndex = 8;
                    speed.Y = 0;
                    speed.X = 3;
                    delayCounter++;
                }
                else if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
                {
                    fIndex = 9;
                    lIndex = 11;
                    speed.Y = -3;
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
            }
            if (custom)
            {
                delayCounter ++;
                if (delayCounter > cDelay[cIndex])
                {
                    cIndex++;
                    if (cIndex>cMaxIndex)
                    {
                        custom = false;
                    }
                }
            }
            else if (wDelay >= 0)
            {
                frameIndex = wFrame;
                wDelay--;
            }
            

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //version 4
            if (!custom)
            {
                if (frameIndex >= 0)
                {
                    position += speed;
                    spriteBatch.Draw(tex, position, frames.ElementAt<Rectangle>(frameIndex), Color.White);

                }
            }
            else
            {
                spriteBatch.Draw(tex, position, frames.ElementAt<Rectangle>(cFrames[cIndex - 1]), Color.White);
                position += cSpeed[cIndex - 1];
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X+12, (int)position.Y+39, 25, 10);
        }
        public Rectangle getFullBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y,(int)dimension.X, (int)dimension.Y);
        }
        public void setDelay(int delay, int frame)
        {
            wDelay = delay;
            wFrame = frame;
        }
        public void customAnimation(Vector2[] speeds, int[] frame, int[] delay )
        {
            cFrames = frame;
            cSpeed = speeds;
            cDelay = delay;
            cIndex = 0;
            cMaxIndex = frame.Length;
            custom = true;
        }
    }
}
