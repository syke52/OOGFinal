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
    public class TextBox : Microsoft.Xna.Framework.DrawableGameComponent
    {
         protected SpriteBatch spriteBatch;
        protected Color color= Color.BlanchedAlmond;
        protected SpriteFont font;
        protected string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        protected Vector2 position;
        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        protected int delay;
        protected int delayCounter;
        protected Texture2D rect;
        protected Rectangle dest;

        public TextBox(Game game, SpriteBatch spriteBatch,
            SpriteFont font,int firstIndex,
            string message,int delay)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.font = font;
            position = new Vector2(0,224);
            dest = new Rectangle(0, 224, 512, 96);
            this.message = message;
            this.delay = delay;
            this.index = firstIndex;
            rect = new Texture2D(game.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.Black });
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
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
            if (delay != -1) 
            { 
            delayCounter++;
            if (delayCounter > delay)
            {
                delayCounter = 0;
                if (index < Message.Length)
                {
                    index++;
                }
                else
                {
                    index = message.Length;
                }
            }
            }
            
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            string subString = Message.Substring(0, index);
            spriteBatch.Begin();
            spriteBatch.Draw(rect, dest, color);
            spriteBatch.DrawString(font, subString, new Vector2(position.X+20, position.Y+20), color);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
