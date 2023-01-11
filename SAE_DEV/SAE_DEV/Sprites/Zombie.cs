using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using SAE_DEV.Screens;

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

        
        public void LoadContent(Game game)
        {
            SpriteSheet spritezombie = game.Content.Load<SpriteSheet>("ZombieToast_50.sf", new JsonContentLoader());
            _spriteZombie = new AnimatedSprite(spritezombie);
        }

        public void SpawnDuZombie()
        {
            //SPAWN DES ZOMBIE EN DEHORS DES BATIMENTS
            bool posvalide = false;
            do
            {
                Random random = new Random();
                if(Game1._choixMap == 1)
                {
                    this.PositionZombie = new Vector2(random.Next(150, 800), random.Next(200, 700));
                }
                else
                {
                    this.PositionZombie = new Vector2(random.Next(0, 450), random.Next(100, 500));
                }
                
               
                posvalide = true;
                ushort tx = (ushort)(this.PositionZombie.X / Monde._tiledMap.TileWidth);
                ushort ty = (ushort)(this.PositionZombie.Y / Monde._tiledMap.TileWidth);
                if (Collision.IsCollision(tx, ty))
                {
                    posvalide = false;
                }
                if (Math.Sqrt(
                    Math.Pow(Perso._positionPerso.X - this.PositionZombie.X, 2) +
                    Math.Pow(Perso._positionPerso.Y - this.PositionZombie.Y, 2)) < 200)
                {
                    posvalide = false;
                }
            } while (!posvalide);

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