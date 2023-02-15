using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Rusty_App05;

namespace App05_Super_Rusty
{
    /// <summary>
    /// This class generates Rusty, the player's character.
    /// It is responsible for its behaviour and position
    /// in the Super Rusty game.
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>19th May 2022</version>
    public class Rusty
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;

        // if the player has jumped
        public bool hasJumped;
        // if the player is above the ground
        public bool aboveGround;

        public int Score;
        public int Lives = 2;

        public const int TEXTURE_WIDTH = 58;
        public const int TEXTURE_HEIGHT = 60;

        public Rusty(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f,
                Vector2.Zero, 2f, SpriteEffects.None, 0f);
        }

        // Creates a rectangle with sprite texture details
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width * 2, Texture.Height * 2);
            }
        }

        public void Update(SoundEffect effect, GameTime gameTime, List<Block> blocks)
        {
            // Adds position to velocity to make move the character
            Position += Velocity;

            // If the user is not pressing the right or left keyboard arrows
            if(Keyboard.GetState().IsKeyUp(Keys.Right) || Keyboard.GetState().IsKeyUp(Keys.Left))
                Velocity.X = 0f;

            foreach (var block in blocks)
            {
                // Stop player if touching on the sides of a block
                if ((Velocity.X > 0 && IsTouchingLeft(block)) ||
                    (Velocity.X < 0 && IsTouchingRight(block)))
                {
                    Velocity.X = 0f;
                }
                // Stop player if touching on top of a block
                if (Velocity.Y > 0 && IsTouchingTop(block))
                {
                    Velocity.Y = 0f;
                }
            }

            // if the user is pressing right to go forward
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                if (Position.X <= Game1.SCREEN_WIDTH / 2 || (Scrolling.IsLastBackground() && Position.X < 790 - Texture.Width))
                    Velocity.X += Game1.TRANSLATION;

            // if the user is pressing left to go back
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (Position.X > 5)
                    Velocity.X = -Game1.TRANSLATION;
            }

            // Make the player jump by pressing space key in case the player is not jumping
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                Position.Y -= 10f;
                Velocity.Y = -7f;
                hasJumped = true;
                effect.Play();
            }

            // reduce velocity to go down when jumping
            if (hasJumped == true)
            {
                Velocity.Y += 0.2f;
            }

            // if on top of a block or reached the ground, stop the jump
            if (Position.Y >= Game1.Y_GROUND || OnTopOfBlock())
            {
                hasJumped = false;
            }

            // In case the player is no longer on top of a block, make it fall
            else if (!OnTopOfBlock() && Position.Y <= Game1.Y_GROUND && !hasJumped)
            {
                hasJumped = true;
            }

            // if it has reached the ground
            if (hasJumped == false)
            {
                Velocity.Y = 0f;
            }

        }

        #region

        /// <summary>
        /// Checks if the player is colliding with the left side of
        /// a block by comparing the position of the two rectangles
        /// associated with Rusty and the block.
        /// </summary>
        /// <param name="block">The block that we are checking for collision</param>
        /// <returns>Collision or not</returns>
        public bool IsTouchingLeft(Block block)
        {
            return Rectangle.Right < block.Rectangle.Left &&
                Rectangle.Bottom > block.Rectangle.Top &&
                Rectangle.Top < block.Rectangle.Bottom;
        }

        public bool IsTouchingRight(Block block)
        {
            return Rectangle.Left > block.Rectangle.Right &&
                Rectangle.Bottom > block.Rectangle.Top &&
                Rectangle.Top < block.Rectangle.Bottom;
        }

        public bool IsTouchingTop(Block block)
        {
            return Rectangle.Bottom <= block.Rectangle.Top + 1 &&
                Rectangle.Top >= block.Rectangle.Top - TEXTURE_HEIGHT - 2 &&
                Rectangle.Left < block.Rectangle.Right - 10 &&
                Rectangle.Right > block.Rectangle.Left + 10;
        }

        public bool IsTouchingBottom(Block block)
        {
            return Rectangle.Top > block.Rectangle.Bottom &&
                Rectangle.Bottom <= block.Rectangle.Top + TEXTURE_HEIGHT + 1 &&
                Rectangle.Right > block.Rectangle.Left &&
                Rectangle.Left < block.Rectangle.Right;
        }
        #endregion

        /// <summary>
        /// Checks if the player is on top of any block of the game
        /// <returns>true or false</returns>
        public bool OnTopOfBlock()
        {
            foreach (Block block in Game1.blocks)
                if (IsTouchingTop(block))
                    return true;

            return false;
        }
    }
}
