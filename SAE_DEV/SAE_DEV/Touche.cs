using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using SAE_DEV.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV
{
    internal class Touche
    {
        public static void Presse(Vector2 _positionPerso, TiledMap _tiledMap, string _animationPerso, float walkSpeed, float deltaTime)
        {
            KeyboardState _keyboardState = Keyboard.GetState();

            var _direction = Vector2.Zero;

            //Deplacement du perso + collisions
            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth);
                Perso._animationPerso = "walkEast";
                if (!Collision.IsCollision(tx, ty))
                {
                    _direction.X += 1;
                }
            }
            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth - 0.5);
                Perso._animationPerso = "walkNorth";
                if (!Collision.IsCollision(tx, ty))
                {
                    _direction.Y -= 1;
                }

            }
            if (_keyboardState.IsKeyDown(Keys.Down))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth + 0.5);
                Perso._animationPerso = "walkSouth";
                if (!Collision.IsCollision(tx, ty))
                {
                    _direction.Y += 1;
                }

            }
            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth);
                Perso._animationPerso = "walkWest";
                if (!Collision.IsCollision(tx, ty))
                {
                    _direction.X -= 1;
                }
            }
            if (_direction != Vector2.Zero)
                _direction.Normalize();

            Perso._positionPerso += _direction * walkSpeed;
            
        }
    }
}
