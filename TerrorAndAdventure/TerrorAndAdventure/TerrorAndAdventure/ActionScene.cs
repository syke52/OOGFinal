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
    public class ActionScene : GameScene
    {
        SpriteBatch spriteBatch;
        Texture2D tex;
        Bat bat;
        Vector2 position;
        Vector2 speed = new Vector2(4,0);
        Map outdoor;
        Game game;
        public ActionScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.game = game;
            outdoor = new Map(game, spriteBatch, "E:\\P2370OOG\\OOGFinal\\TerrorAndAdventure\\TerrorAndAdventure\\TerrorAndAdventureContent\\Maps\\Indoor.tmx");

            this.Components.Add(outdoor);
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
    }
}
