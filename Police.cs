using System;
using App05_Super_Rusty;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Super_Rusty_App05
{
    /// <summary>
    /// This class is responsible for the police
    /// enemies in the Super Rusty game.
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>19th May 2022</version>
    public class Police
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _velocity;

        private int timer = 1000;

        // if the enemy is no longer in the screen
        public bool isVisible = true;

        Random random = new Random();
        float randX;

        public Police(Texture2D texture, Vector2 position, bool onBlock)
        {
            _texture = texture;
            _position = position;

            // if this specific enemy is on top of a block
            // if false, set random velocity at ground for spawn
            if(!onBlock)
            {
                randX = (float)(-1.5 * random.NextDouble());
                _velocity = new Vector2(randX, Game1.Y_GROUND);
            }
            else
                _velocity = new Vector2(1, _position.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // check in which direction the enemy is going
            if(_velocity.X < 0)
                spriteBatch.Draw(_texture, _position, null, Color.White, 0f,
                    Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
            if (_velocity.X > 0)
                spriteBatch.Draw(_texture, _position, null, Color.White, 0f,
                    Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void Update()
        {
            _position.X += _velocity.X;

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && Game1.rusty.Position.X >= 400 && !Scrolling.IsLastBackground())
            {
                _position.X -= Game1.TRANSLATION;
            }

            if (_position.X < 0 - _texture.Width)
            {
                isVisible = false;
            }

            // checking if the enemy is above ground
            if (_position.Y < Game1.Y_GROUND)
            {
                // checking position of block2 and contains enemy inside it
                if (Game1.CheckInterval(_position.X, Game1.block2.Rectangle.X, Game1.block2.Rectangle.X + Game1.block2.Rectangle.Width))
                {
                    if ((_position.X <= Game1.block2.Rectangle.X + 10 && _velocity.X < 0) || (_position.X >= Game1.block2.Rectangle.X + Game1.block2.Rectangle.Width - 10 && _velocity.X > 0))
                        _velocity.X = -_velocity.X;
                }

                // checking position of block7 and contains enemy inside it
                if (Game1.CheckInterval(_position.X, Game1.block7.Rectangle.X, Game1.block7.Rectangle.X + Game1.block7.Rectangle.Width))
                {
                    if ((_position.X <= Game1.block7.Rectangle.X + 10 && _velocity.X < 0) || (_position.X >= Game1.block7.Rectangle.X + Game1.block7.Rectangle.Width - 10 && _velocity.X > 0))
                        _velocity.X = -_velocity.X;
                }
            }

            // check for collision of the player with enemy
            if (Game1.CheckInterval(Game1.rusty.Position.X, _position.X - 30, _position.X + 30) &&
                Game1.CheckInterval(Game1.rusty.Position.Y, _position.Y - 50, _position.Y + 50))
            {
                // if the player has 2 lives
                if(timer == 1000)
                {
                    Game1.rusty.Lives--;
                    Game1.LostLifeEffect.Play();
                    startTimer();
                }
                // if player only has one life, in collision stop the game
                if (timer == 0)
                {
                    Game1.rusty.Lives--;
                    Game1.lostGame = true;
                }
            }
        }

        /// <summary>
        /// Method equal to Game1.startTimer().
        /// In future, needs to be refactored due to bug and repetition.
        /// </summary>
        private void startTimer()
        {
            for (int i = 1; i <= 1000; i++)
                timer -= i;
        }
    }
}
