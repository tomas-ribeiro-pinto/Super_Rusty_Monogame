using App05_Super_Rusty;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Super_Rusty_App05
{
    /// <summary>
    /// This class is responsible for the block
    /// platforms in the Super Rusty game.
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>19th May 2022</version>
    public class Block
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;

        public Block(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width * 0.3), (int)(Texture.Height * 0.3));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f,
                Vector2.Zero, 0.3f, SpriteEffects.None, 0f);
        }

        public void Update()
        {
            // move block at translation speed
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && Game1.rusty.Position.X >= Game1.SCREEN_WIDTH/2 && !Scrolling.IsLastBackground())
            {
                Position.X -= Game1.TRANSLATION;
            }
        }
    }
}
