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
        private float vitesse_mvt;
        private Vector2 direction;

        private string _animationPerso;

        private KeyboardState _previouskeyboardState;
        private KeyboardState _currentkeyboardState;



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
            _animationPerso = "idle";
            _previouskeyboardState = _currentkeyboardState;
            _currentkeyboardState = Keyboard.GetState();

            if (_currentkeyboardState.IsKeyDown(Keys.Left)) {
                direction.X -= vitesse_mvt; 
                _animationPerso = "running";
            }
            if (_currentkeyboardState.IsKeyDown(Keys.Right)) {
                direction.X += vitesse_mvt;
                _animationPerso = "running";
            }
            if (_currentkeyboardState.IsKeyDown(Keys.Up)) {
                direction.Y += vitesse_mvt;
                _animationPerso = "running";
            }
            if (_currentkeyboardState.IsKeyDown(Keys.Down)) {
                direction.Y -= vitesse_mvt ;
                _animationPerso = "running";
            }

            float movement = vitesse_mvt * deltaTime;
            //Vector2 moveDirection = new Vector2((float)Math.Cos(direction), (float)Math.Sin(direction));
            _position += movement * direction;

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
