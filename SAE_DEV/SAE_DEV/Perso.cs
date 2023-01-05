using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics.Metrics;

namespace SAE_DEV
{
    internal class Perso
    {

        private AnimatedSprite _spritePerso;

        private Vector2 _position;
        private int vitesse_mvt;

        private string _animationPerso;
        private KeyboardState _keyboardState;



        public Vector2 Position { get => _position; set => _position = value; }

        public void Initialize(Game game)
        {   
            vitesse_mvt = 100;
        }
        public void LoadContent(Game game)
        {
            SpriteSheet finnAT = game.Content.Load<SpriteSheet>("FinnSprite.sf", new JsonContentLoader());
            _spritePerso = new AnimatedSprite(finnAT);
        }
        public void Update(float deltaTime)
        {
            _keyboardState = Keyboard.GetState();
            _animationPerso = "idle";
            Vector2 direction = Vector2.Zero;

            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                _animationPerso = "running";
                direction.X += vitesse_mvt;
            }
            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                _animationPerso = "running";
                direction.Y -= vitesse_mvt;
            }
            if (_keyboardState.IsKeyDown(Keys.Down))
            {
                _animationPerso = "running";
                direction.Y += vitesse_mvt;
            }
            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                _animationPerso = "running";
                direction.X -= vitesse_mvt;
            }
            if (direction == Vector2.Zero)
            {
                return;
            }
            _position += direction * deltaTime;
            _spritePerso.Play(_animationPerso);
            _spritePerso.Update(deltaTime);
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_spritePerso, Position);
            _spriteBatch.End();
        }
    }

}
