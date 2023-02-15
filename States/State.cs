using System;
using App05_Super_Rusty;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Rusty_App05.States
{
    /// <summary>
    /// This class is responsible of the states
    /// in the Super Rusty Game.
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>19th May 2022</version>
    public abstract class State
    {
        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;
        protected Game1 _game;

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, Scrolling background);

        public abstract void PostUpdate(GameTime gameTime);

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
        }

        public abstract void Update(GameTime gameTime);
    }
}
