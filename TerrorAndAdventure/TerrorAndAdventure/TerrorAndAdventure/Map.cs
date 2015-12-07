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
using System.Text;
using System.Xml;


namespace TerrorAndAdventure
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Map : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        public int[] tiles;
        private static int MAPHEIGHT = 10;
        private static int MAPWIDTH = 16;
        private static Vector2 TILEDIMENSION = new Vector2(32,32);
        private static int SPACING = 2;
        Texture2D tileSource;
        private int layers;
        private int tileWidth;
        private int tileHeight;
        private List<Rectangle> frames;
        public List<Rectangle> collisions;
        public List<Rectangle> poe;
        Game game;
        public Map(Game game, SpriteBatch spriteBatch, string fileName )
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            mapLoader(fileName);
        }
        public void buildMap (int imgWidth, int imgHeight, Texture2D tileSource, int[] tiles )
        {
            tileWidth = (imgWidth - SPACING) / ((int)TILEDIMENSION.X + SPACING);
            tileHeight = (imgHeight - SPACING) / ((int)TILEDIMENSION.Y + SPACING);
            this.tileSource = tileSource;
            this.tiles = tiles;
            layers = tiles.Length / 160;

            createFrames();
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
        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < tileHeight; i++)
            {
                for (int j = 0; j < tileWidth; j++)
                {
                    int x = (j * ((int)TILEDIMENSION.X+SPACING)) + SPACING;
                    int y = (i * ((int)TILEDIMENSION.Y+SPACING)) + SPACING;
                    Rectangle r = new Rectangle(x, y, (int)TILEDIMENSION.X, (int)TILEDIMENSION.Y);
                    frames.Add(r);
                }
            }
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
            int frameIndex = 0;
            int test = 0;
            Vector2 position = Vector2.Zero;
            for (int h = 0; h < layers; h++)
            {
                for (int i = 0; i < MAPHEIGHT; i++)
                {
                    for (int j = 0; j < MAPWIDTH; j++)
                    {
                        if ((tiles[frameIndex])-1>0)
                        {
                            spriteBatch.Draw(tileSource, position, frames.ElementAt<Rectangle>((tiles[frameIndex]) - 1), Color.White);
                        }
                        frameIndex++;
                        position.X += TILEDIMENSION.X;
                        test++;
                    }
                    position.X = 0;
                    position.Y += TILEDIMENSION.Y;
                }
                position = Vector2.Zero;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void mapLoader(string filePath)
        {
            XmlDocument xmlD = new XmlDocument();
            xmlD.Load(filePath);
            XmlNodeList tileList = xmlD.GetElementsByTagName("tile");
            int h = tileList.Count;
            int[] tiles = new int[h];
            int g = 0;
            foreach (XmlNode item in tileList)
            {
                tiles[g]=int.Parse(item.Attributes["gid"].Value);
                g++;
            }
            Texture2D tileSource;
            XmlNodeList tileset= xmlD.GetElementsByTagName("tileset");
            tileSource = game.Content.Load<Texture2D>("TileSets/" + tileset[0].Attributes["name"].Value);
            XmlNodeList image = xmlD.GetElementsByTagName("image");
            int imgHeight = int.Parse(image[0].Attributes["height"].Value);
            int imgWidth = int.Parse(image[0].Attributes["width"].Value);

            XmlNodeList collision = xmlD.GetElementsByTagName("tileset");
            foreach (XmlNode item in collision)
            {
                collisions.Add(new Rectangle( int.Parse(item.Attributes["x"].Value), 
                        int.Parse(item.Attributes["y"].Value),
                        int.Parse(item.Attributes["width"].Value),
                        int.Parse(item.Attributes["height"].Value)));
            }
            XmlNodeList poes = xmlD.GetElementsByTagName("tileset");
            foreach (XmlNode item in poes)
            {
                poe.Add(new Rectangle( int.Parse(item.Attributes["x"].Value), 
                        int.Parse(item.Attributes["y"].Value),
                        int.Parse(item.Attributes["width"].Value),
                        int.Parse(item.Attributes["height"].Value)));
            }

            buildMap(imgWidth, imgHeight, tileSource, tiles);
        }
    }
}
