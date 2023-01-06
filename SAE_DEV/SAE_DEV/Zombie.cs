﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System.Reflection.Metadata;


namespace SAE_DEV
{
    internal class Zombie
    {
        private Vector2 _positionZombie;
        private int _vitesseZombie;
        private AnimatedSprite _spriteZombie;
        private string _animationZombie;
        private Vector2 _directionZombie;


        public Vector2 PositionZombie
        {
            get
            {
                return this._positionZombie;
            }

            set
            {
                this._positionZombie = value;
            }
        }

        public int VitesseZombie
        {
            get
            {
                return this._vitesseZombie;
            }

            set
            {
                this._vitesseZombie = value;
            }
        }

        public AnimatedSprite SpriteZombie
        {
            get
            {
                return this._spriteZombie;
            }

            set
            {
                this._spriteZombie = value;
            }
        }

        public string AnimationZombie
        {
            get
            {
                return this._animationZombie;
            }

            set
            {
                this._animationZombie = value;
            }
        }

        public Vector2 DirectionZombie
        {
            get
            {
                return this._directionZombie;
            }

            set
            {
                this._directionZombie = value;
            }
        }

        public void Initialize(Game game)
        {
            Random random = new Random();
            _vitesseZombie = random.Next(80, 150);
        }

        public void LoadContent(Game game)
        {
            SpriteSheet spritezombie = game.Content.Load<SpriteSheet>("ZombieToast_50.sf", new JsonContentLoader());
            _spriteZombie = new AnimatedSprite(spritezombie);
        }

        public void Update(float deltaTime)
        {
            _spriteZombie.Play("idle");
            _spriteZombie.Update(deltaTime);
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            
            _spriteBatch.Draw(_spriteZombie, _positionZombie);
            

        }

    }

}