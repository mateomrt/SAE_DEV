using System;
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
using SAE_DEV.Screens;

namespace SAE_DEV
{
    internal class IAZombie
    {
        private Zombie zombie;
        private float speed;

        public IAZombie(float speed, Zombie zombie)
        {
            this.Speed = speed;
            this.Zombie = zombie;
        }

        public float Speed { get => speed; set => speed = value; }
        internal Zombie Zombie { get => zombie; set => zombie = value; }

        
        public void Update(GameTime gameTime)
        {
            if(Math.Sqrt(Math.Pow(Perso._positionPerso.X-zombie.PositionZombie.X, 2) + Math.Pow(Perso._positionPerso.Y-zombie.PositionZombie.Y, 2)) > 400)
            {
                speed = 0;
            }
            else
            {
                Vector2 _direction = Vector2.Zero;
                Random rand = new Random();
                speed = rand.Next(40,65);
                _direction = Perso._positionPerso - zombie.PositionZombie;
                // Deplacement vers le perso et collision vers la droite
                if (_direction.X > 0)
                {
                    _direction.X += 1;
                    ushort tx = (ushort)(zombie.PositionZombie.X / Monde._tiledMap.TileWidth + 0.5);
                    ushort ty = (ushort)(zombie.PositionZombie.Y / Monde._tiledMap.TileWidth);
                    if (Collision.IsCollision(tx, ty))
                    {
                        _direction.X = 0;
                    }
                }
                // Deplacement vers le perso et collision vers le haut 
                if (_direction.Y < 0)
                {
                    _direction.Y -= 1;
                    ushort tx = (ushort)(zombie.PositionZombie.X / Monde._tiledMap.TileWidth);
                    ushort ty = (ushort)(zombie.PositionZombie.Y / Monde._tiledMap.TileWidth -0.5);
                    if (Collision.IsCollision(tx, ty))
                    {
                        _direction.Y = 0;
                    }

                }
                // Deplacement vers le perso et collision vers le bas
                if (_direction.Y > 0)
                {
                    _direction.Y += 1;
                    ushort tx = (ushort)(zombie.PositionZombie.X / Monde._tiledMap.TileWidth);
                    ushort ty = (ushort)(zombie.PositionZombie.Y / Monde._tiledMap.TileWidth + 0.5);
                    if (Collision.IsCollision(tx, ty))
                    {
                        _direction.Y = 0;
                    }

                }
                // Deplacement vers le perso et collision vers la gauche
                if (_direction.X < 0)
                {
                    _direction.X -= 1;
                    ushort tx = (ushort)(zombie.PositionZombie.X / Monde._tiledMap.TileWidth - 0.5);
                    ushort ty = (ushort)(zombie.PositionZombie.Y / Monde._tiledMap.TileWidth);
                    if (Collision.IsCollision(tx, ty))
                    {
                        _direction.X = 0;
                    }
                }
                if (_direction != Vector2.Zero)
                    _direction.Normalize();

                zombie.PositionZombie += _direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

        }



    }
}
