using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using SAE_DEV;

namespace SAE_DEV
{
    public class Bullet
    {
        private float LinearVelocity = 4f;

        private Vector2 _position;
        private Vector2 _direction;

        private float LifeSpan = 2f;
        private bool IsRemoved;

        private Texture2D sprite;
        private object spriteBatch;

        public Bullet(Vector2 position, Vector2 direction, Texture2D bulletSprite)
        {
            this._position = position;
            this._direction = direction;
            this.sprite = bulletSprite;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Just draw the texture we have at the rectangle x and y.
            spriteBatch.Draw(sprite, _position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            float _timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                IsRemoved = true;

            _position += _direction * LinearVelocity;
        }
    }
}