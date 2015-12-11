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
        private Vector2 speed = new Vector2(4, 0);
        private int scene = -1;
        private mapCollision col;
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
        private Map[] forest = new Map[4];
        private Map[] cave = new Map[4];
        private Map start;
        Game game;
        private PlayerToken player;
        private int storyIndex = 0;
        private bool story = true;
        private bool firstBoss = false;
        private bool secondBoss = false;
        private bool goodEnter;
        private SpriteFont font;
        private TextBox text;
        private int delaycounter;
        private int delay;
        private int tIndexes = 1;
        private int textIndex = 0;
        public int StoryIndex
        {
            get { return storyIndex; }
            set { storyIndex = value; }
        }
        ShineGreymonIdle bossOne;
        PhoenixIdleHFlipped bossTwo;

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
            player = new PlayerToken(game, spriteBatch, game.Content.Load<Texture2D>("Images/Walk"), new Vector2(204, 86), 10);
            col = new mapCollision(game, player, forest[0], this);
            bossOne = new ShineGreymonIdle(game, spriteBatch, new Vector2(75, 96), 30);
            bossTwo = new PhoenixIdleHFlipped(game, spriteBatch, new Vector2(320, 115), 30);
            //bossTwo = new PhoenixIdleHFlipped(game, spriteBatch, new Vector2(0, 100), 30);
            this.Components.Add(col);
            this.Components.Add(player);
            font = game.Content.Load<SpriteFont>("Fonts/pixelFont");
            this.Components.Add(bossOne);
            this.Components.Add(bossTwo);
            bossOne.Enabled = false;
            bossOne.Visible = false;
            bossTwo.Enabled = false;
            bossTwo.Visible = false;
            text = new TextBox(game, spriteBatch, font, 0, "12345678901234567890123456789012345678901234567\nline2\nline3 [...]", 5);
            this.Components.Add(text);
            //text.Visible = false;
            //text.Enabled = false;
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
            if (story)
            {
                if (storyIndex == 0)
                {
                    //display text
                    player.setDelay(1, 1, 10, 10, Vector2.Zero);
                    text.Enabled = true;
                    text.Visible = true;
                    if (textIndex == 0)
                    {
                        text.Message = "I felt different when I woke up this morning.\nI dreamt of carrots last night.\n[...]";
                    }
                    if (textIndex == 1)
                    {
                        text.Message = "I heard a voice saying 'Go to the cave'.\nI should see what's down there.";
                    }
                    KeyboardState ks = Keyboard.GetState();
                    if (ks.IsKeyUp(Keys.Enter))
                    {
                        goodEnter = true;
                    }
                    if (ks.IsKeyDown(Keys.Enter) && goodEnter)
                    {
                        if (textIndex >= tIndexes && text.Index == text.Message.Length)
                        {
                            storyIndex++;
                            text.Enabled = false;
                            text.Visible = false;
                            text.Index = 0;
                            textIndex = 0;
                        }
                        else
                        {
                            if (text.Index<text.Message.Length)
                            {
                                text.Index = text.Message.Length;
                            }
                            else
                            {
                                textIndex++;
                                text.Index = 0;
                            }
                            
                        }
                        goodEnter = false;
                    }
                }
                else if (storyIndex == 1)
                {
                    player.setDelay(0, 2, 10, 10000, new Vector2(0, 1));
                    storyIndex++;

                }
                else if (storyIndex == 2)
                {
                    if (player.Position.Y > 200)
                    {
                        player.WDelay = -1;
                        scene = 0;
                        storyIndex++;
                        player.setDelay(1, 1, 10, 10, new Vector2(0, 0));
                        player.Position = new Vector2(315, 110);
                        story = false;
                    }
                }
                else if (storyIndex == 3)
                {
                    //display text
                    player.setDelay(4, 4, 10, 100, Vector2.Zero);
                    text.Enabled = true;
                    text.Visible = true;
                    if (textIndex == 0)
                    {
                        text.Message = "That creature is blocking my way!\nLooks like he won't move without a fight.";
                    }
                    tIndexes = 0;
                    KeyboardState ks = Keyboard.GetState();
                    if (ks.IsKeyUp(Keys.Enter))
                    {
                        goodEnter = true;
                    }
                    if (ks.IsKeyDown(Keys.Enter) && goodEnter)
                    {
                        if (textIndex >= tIndexes && text.Index == text.Message.Length)
                        {
                            storyIndex++;
                            text.Enabled = false;
                            text.Visible = false;
                            text.Index = 0;
                            textIndex = 0;
                        }
                        else
                        {
                            if (text.Index < text.Message.Length)
                            {
                                text.Index = text.Message.Length;
                            }
                            else
                            {
                                textIndex++;
                                text.Index = 0;
                            }

                        }
                        goodEnter = false;
                    }
                }
                else if (storyIndex == 4)
                {
                    player.setDelay(3, 4, 8, 10000, new Vector2(-3, 0));
                    storyIndex++;
                }
                else if (storyIndex == 5)
                {
                    if (player.Position.X < 220)
                    {
                        player.WDelay = -1;
                        storyIndex++;
                        player.setDelay(4, 4, 10, 10, new Vector2(0, 0));
                        story = false;
                        bossOne.Enabled = false;
                        bossOne.Visible = false;
                        //trigger battle scene
                    }
                }
                else if (storyIndex == 6)
                {
                    //display text
                    text.Enabled = true;
                    text.Visible = true;
                    player.setDelay(7, 7, 10, 10, Vector2.Zero);
                    if (textIndex == 0)
                    {
                        text.Message = "This creature must be why there are so many\ncreatures in the forest and cave.\nI should do something about it!";
                    }
                    tIndexes = 0;
                    KeyboardState ks = Keyboard.GetState();
                    if (ks.IsKeyUp(Keys.Enter))
                    {
                        goodEnter = true;
                    }
                    if (ks.IsKeyDown(Keys.Enter) && goodEnter)
                    {
                        if (textIndex >= tIndexes && text.Index == text.Message.Length)
                        {
                            storyIndex++;
                            text.Enabled = false;
                            text.Visible = false;
                            text.Index = 0;
                            textIndex = 0;
                        }
                        else
                        {
                            if (text.Index < text.Message.Length)
                            {
                                text.Index = text.Message.Length;
                            }
                            else
                            {
                                textIndex++;
                                text.Index = 0;
                            }

                        }
                        goodEnter = false;
                    }
                }
                else if (storyIndex == 7)
                {
                    player.setDelay(6, 8, 8, 10000, new Vector2(3, 0));
                    storyIndex++;
                }
                else if (storyIndex == 8)
                {
                    if (player.Position.X > 260)
                    {
                        player.WDelay = -1;
                        storyIndex++;
                        player.setDelay(7, 7, 10, 10, new Vector2(0, 0));
                        story = false;
                        bossTwo.Enabled = false;
                        bossTwo.Visible = false;
                        //trigger battle scene
                    }
                }
                //black screen devil stuff
            }
            else if (scene != cScene && scene < 8)
            {
                int i;
                if (scene < 4)
                {
                    i = scene;
                    col.Map = forest[i];
                    forest[i].Enabled = true;
                    forest[i].Visible = true;
                }
                else
                {
                    i = scene - 4;
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
                if (cScene == 3 && !firstBoss)
                {
                    bossOne.Enabled = true;
                    bossOne.Visible = true;
                    firstBoss = true;
                    story = true;
                }
                if (scene == 7 && !secondBoss)
                {
                    bossTwo.Enabled = true;
                    bossTwo.Visible = true;
                    secondBoss = true;
                    story = true;
                }
            }
            else if (player.Encounter && cScene != 3 && cScene != 7)
            {
                //call battle scene
            }

            base.Update(gameTime);
        }
    }
}
