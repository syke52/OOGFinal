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
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, highlightFont;
        private List<string> menuItems;
        private int selectedIndex = 0;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        private Vector2 position;
        private Color regularColor = Color.Black;
        private Color highlightColor = Color.BlanchedAlmond;

        private KeyboardState oldState;      

        public MenuComponent(Game game, SpriteBatch spriteBatch, 
            SpriteFont regularFont, SpriteFont highlightedFont, 
            string[] menus)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.highlightFont = highlightedFont;
            menuItems = menus.ToList();
            position = new Vector2(Shared.stage.X/2, Shared.stage.Y/2);

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
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count-1;
                }
            }
            oldState = ks;

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            spriteBatch.Begin();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(highlightFont, menuItems[i], tempPos, highlightColor);
                    tempPos.Y += highlightFont.LineSpacing;
                    //Do stuff to make glowy
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += highlightFont.LineSpacing;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
