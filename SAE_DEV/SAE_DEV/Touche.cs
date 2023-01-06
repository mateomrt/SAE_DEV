using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV
{
    internal class Touche
    {
        public static void Presse(Vector2 _positionPerso, TiledMap _tiledMap, string _animationPerso, float _walkSpeed, GraphicsDeviceManager _graphics)
        {
            KeyboardState _keyboardState = Keyboard.GetState();

            //Deplacement du perso + collisions
            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth);
                _animationPerso = "running";
                if (!Collision.IsCollision(tx, ty))
                {
                    _positionPerso.X += _walkSpeed;
                }
            }
            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth - 0.5);
                _animationPerso = "running";
                if (!Collision.IsCollision(tx, ty))
                {
                    _positionPerso.Y -= _walkSpeed;
                }

            }
            if (_keyboardState.IsKeyDown(Keys.Down))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth + 0.5);
                _animationPerso = "running";
                if (!Collision.IsCollision(tx, ty))
                {
                    _positionPerso.Y += _walkSpeed;
                }

            }
            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth);
                _animationPerso = "running";
                if (!Collision.IsCollision(tx, ty))
                {
                    _positionPerso.X -= _walkSpeed;
                }
            }
        }
    }
}
