using System;
using Microsoft.Xna.Framework;
using SAE_DEV.Screens;

namespace SAE_DEV
{
    internal class IAZombie
    {
        private Zombie zombie;
        private float _speed;

        public IAZombie(float speed, Zombie zombie)
        {
            this.Speed = speed;
            this.Zombie = zombie;
        }

        public float Speed { get => _speed; set => _speed = value; }
        internal Zombie Zombie { get => zombie; set => zombie = value; }

        
        public void Update(GameTime gameTime)
        {
            if(Math.Sqrt(Math.Pow(Perso._positionPerso.X-zombie.PositionZombie.X, 2) + Math.Pow(Perso._positionPerso.Y-zombie.PositionZombie.Y, 2)) > 400)
            {
                _speed = 0;
            }
            else
            {
                Vector2 _direction = Vector2.Zero;
                Random rand = new Random();
                _speed = rand.Next(15,100);
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
                    ushort ty = (ushort)(zombie.PositionZombie.Y / Monde._tiledMap.TileWidth - 0.5);
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

                zombie.PositionZombie += _direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

        }



    }
}
