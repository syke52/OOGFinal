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
    public class BattleScene : Microsoft.Xna.Framework.DrawableGameComponent
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BackgroundImage background;
        HeroIdle heroIdle;
        Texture2D heroTexIdle;
        Texture2D backgroundImage;

        public BattleScene(Game game, SpriteBatch spriteBatch, GraphicsDeviceManager graphics, int cScene)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;

            if (cScene <= 4)
            {
                backgroundImage = Game.Content.Load<Texture2D>("BattleImages/backgroundForest");
            }
            else if (cScene > 4)
            {
                backgroundImage = Game.Content.Load<Texture2D>("BattleImages/backgroundCave");
            }
            Rectangle r = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            background = new BackgroundImage(Game, spriteBatch, backgroundImage, r);
            this.Game.Components.Add(background);

            //hero idle animation
            heroTexIdle = Game.Content.Load<Texture2D>("BattleImages/antylamon_sprite_idle2");
            Vector2 posIdle = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight/ 2);
            int delayIdle = 30;
            heroIdle = new HeroIdle(Game, spriteBatch, heroTexIdle, posIdle, delayIdle);
            this.Game.Components.Add(heroIdle);
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

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
