using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace App05_Super_Rusty
{
    /// <summary>
    /// This class is responsible for the backgrounds
    /// and its scrolling in the Super Rusty game.
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>19th May 2022</version>
    public class Backgrounds
    {
        public Texture2D texture;
        public Rectangle rectangle;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }

    public class Scrolling : Backgrounds
    {
        public Scrolling(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

        public void Update()
        {
            // Scroll the background when the player gets to the middle of the screen
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (Game1.rusty.Position.X >= Game1.SCREEN_WIDTH/2 && !IsLastBackground())
                {
                    rectangle.X -= Game1.TRANSLATION;
                }                    
            }
        }

        /// <summary>
        /// This method checks if the current background is the last.
        /// If it returns true, it stops scrolling.
        /// </summary>
        /// <returns>true or false</returns>
        public static bool IsLastBackground()
        {
            var lastItem = Game1.Scrollings.Last();

            if (lastItem.rectangle.X <= 2)
                return true;

            return false;
        }
    }
}
