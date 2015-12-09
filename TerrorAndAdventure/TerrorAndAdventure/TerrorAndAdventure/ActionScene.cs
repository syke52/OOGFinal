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
using System.IO;


namespace TerrorAndAdventure
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ActionScene : GameScene
    {
        SpriteBatch spriteBatch;
        Vector2 speed = new Vector2(4,0);
        int scene = 0;
        mapCollision col;

        public int Scene
        {
            get { return scene; }
            set { scene = value; }
        }
        int cScene = -1;

        public int CScene
        {
            get { return cScene; }
        }
        Map[] forest = new Map[4];
        Map[] cave = new Map[4];
        Map start;
        Game game;
        PlayerToken player;

        public ActionScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            start = new Map(game, spriteBatch, "Content\\Maps\\Indoor.tmx");
            this.Components.Add(start);
            forest[0] = new Map(game, spriteBatch, "Content\\Maps\\Outdoor.tmx");
            forest[1] = new Map(game, spriteBatch, "Content\\Maps\\Forest-1.tmx");
            forest[2] = new Map(game, spriteBatch, "Content\\Maps\\Forest-2.tmx");
            forest[3] = new Map(game, spriteBatch, "Content\\Maps\\Forest-3.tmx");
            cave[0] = new Map(game, spriteBatch, "Content\\Maps\\cave-1.tmx");
            cave[1] = new Map(game, spriteBatch, "Content\\Maps\\cave-2.tmx");
            cave[2] = new Map(game, spriteBatch, "Content\\Maps\\cave-3.tmx");
            cave[3] = new Map(game, spriteBatch, "Content\\Maps\\cave-4.tmx");
            foreach (Map item in forest)
            {
                this.Components.Add(item);
                item.Visible = false;
                item.Enabled = false;
            }
            foreach (Map item in cave)
            {
                this.Components.Add(item);
                item.Visible = false;
                item.Enabled = false;
            }

            player = new PlayerToken(game, spriteBatch, game.Content.Load<Texture2D>("Images/Walk"), new Vector2(Shared.stage.X/2,Shared.stage.Y/2), 10);
            col = new mapCollision(game, player, forest[0], this);
            this.Components.Add(col);
            this.Components.Add(player);
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
            if (scene != cScene && scene<8)
            {
                int i;
                if (scene<4)
                {
                    i = scene;
                    col.Map = forest[i];
                    forest[i].Enabled = true;
                    forest[i].Visible = true;
                }
                else{
                    i = scene-4;
                    col.Map = cave[i];
                    cave[i].Enabled = true;
                    cave[i].Visible = true;
                }

                if (cScene == -1)
                {
                    start.Enabled = false;
                    start.Visible = false;
                }
                else if (cScene < 4)
                {
                    i = cScene;
                    forest[i].Enabled = false;
                    forest[i].Visible = false;
                }
                else
                {
                    i = cScene - 4;
                    cave[i].Enabled = false;
                    cave[i].Visible = false;
                }
                cScene = scene;

                
            }

            base.Update(gameTime);
        }
    }
}
