using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Rusty_App05
{
    /// <summary>
    /// This class is part of Super Rusty game.
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>19th May 2022</version>
    public class OtherSprite
    {
        public Texture2D Texture;
        public Vector2 Position;

        public OtherSprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
