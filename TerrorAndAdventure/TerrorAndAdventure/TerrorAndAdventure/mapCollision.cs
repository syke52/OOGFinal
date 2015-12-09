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
    public class mapCollision : Microsoft.Xna.Framework.GameComponent
    {
        Map map;
        List<Rectangle> collisions;
        List<Rectangle> poe;
        ActionScene actionscene;
        Rectangle stage = new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y);
        public Map Map
        {
            get { return map; }
            set { map = value; }
        }
        PlayerToken player;
        public mapCollision(Game game, PlayerToken player,Map map, ActionScene actionscene)
            : base(game)
        {
            this.player = player;
            this.map = map;
            this.poe = map.poe;
            this.collisions = map.collisions;
            this.actionscene = actionscene;
            // TODO: Construct any child components here
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
            Rectangle playerRect = player.getBounds();
            Rectangle full = player.getFullBounds();
            this.poe = map.poe;
            this.collisions = map.collisions;
            foreach (Rectangle item in collisions)
            {
                if (playerRect.Intersects(item))
                {
                    player.Position -= player.Speed;
                }
            }
            if (!full.Intersects(stage))
            {
                player.Position -= player.Speed;
            }
            foreach (Rectangle item in poe)
            {
                if (playerRect.Intersects(item))
                {
                    player.Speed = Vector2.Zero;
                    
                    if ((actionscene.CScene < 3 && player.Position.X > Shared.stage.X/2))
                    {
                        actionscene.Scene --;
                        player.Position = new Vector2(player.Position.X - 416, player.Position.Y);
                        
                    }
                    else if ((actionscene.CScene < 3 && player.Position.X < Shared.stage.X/2))
                    {
                           actionscene.Scene ++;
                           player.Position = new Vector2(player.Position.X + 416, player.Position.Y);
                    }
                    else if ( (actionscene.CScene==3 && player.Position.X < Shared.stage.X/2))
                    {
                        actionscene.Scene ++;
                        player.Position = new Vector2(player.Position.X +40, player.Position.Y);
                    }
                    else if ( (actionscene.CScene==4 && player.Position.X < Shared.stage.X/2))
                    {
                        actionscene.Scene --;
                        player.Position = new Vector2(player.Position.X -40, player.Position.Y);
                    }
                    else if ((actionscene.CScene > 3 && player.Position.X > Shared.stage.X/2))
                    {
                        actionscene.Scene ++;
                        player.Position = new Vector2(player.Position.X - 416, player.Position.Y);
                        
                    }
                    else if ((actionscene.CScene > 3 && player.Position.X < Shared.stage.X/2))
                    {
                           actionscene.Scene --;
                           player.Position = new Vector2(player.Position.X + 416, player.Position.Y);
                    }
                    
                }
            }
            base.Update(gameTime);
        }
    }
}
