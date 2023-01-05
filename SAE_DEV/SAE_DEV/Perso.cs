﻿using Microsoft.Xna.Framework.Graphics;
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
        private double vitesse_mvt = 100;
        private Vector2 direction;

        private string _animationPerso;

        private KeyboardState _previouskeyboardState;
        private KeyboardState _currentkeyboardState;



        public Vector2 Position { get => _position; set => _position = value; }
    
        public void LoadContent(Game game)
        {
            SpriteSheet finnAT = game.Content.Load<SpriteSheet>("FinnSprite.sf", new JsonContentLoader());
            _spritePerso = new AnimatedSprite(finnAT);
        }
        public void Update(float deltaTime)
        {
            _animationPerso = "idle";

           
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
