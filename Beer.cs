using System;
using App05_Super_Rusty;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Rusty_App05.States;

namespace Super_Rusty_App05
{
    /// <summary>
    /// This class is responsible for the collectable
    /// beers in the Super Rusty game.
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>19th May 2022</version>
    public class Beer
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _velocity;

        // store the old position of the beer for animation purposes
        private float oldPosition;
        public bool IsVisible = true;

        public Beer(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
            oldPosition = _position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, Color.White, 0f,
                Vector2.Zero, 0.3f, SpriteEffects.None, 0f);
        }

        public void Update()
        {
            _position.Y += _velocity.Y;

            // if visible, start animation
            if (IsVisible)
            {
                if (_velocity.Y == 0)
                    _velocity.Y = -0.08f;
                else if (_velocity.Y < 0 && _position.Y <= oldPosition - 4)
                    _velocity.Y = 0.08f;
                else if (_velocity.Y > 0 && _position.Y >= oldPosition + 4)
                    _velocity.Y = -0.08f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && Game1.rusty.Position.X >= 400)
            {
                _position.X -= Game1.TRANSLATION;
            }

            // Checks if the player has picked the beer and plays sound effect
            if (IsVisible && Game1.CheckInterval(Game1.rusty.Position.X, _position.X - Game1.rusty.Texture.Width, _position.X + Game1.rusty.Texture.Width) &&
                Game1.CheckInterval(Game1.rusty.Position.Y, _position.Y - Game1.rusty.Texture.Height, _position.Y + Game1.rusty.Texture.Width))
            {
                Game1.rusty.Score++;
                IsVisible = false;
                Game1.BeerEffect.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
            }
        }
    }
}
