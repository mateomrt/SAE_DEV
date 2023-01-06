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
using SAE_DEV.Screens;

namespace SAE_DEV
{
    internal class Perso
    {

        public static AnimatedSprite _spritePerso;

        public static string _animationPerso;
        public static Vector2 _positionPerso;
        public static float vitesse_mvt;


        public static void Initialize()
        {
            _positionPerso = new Vector2(150, 250);
            vitesse_mvt = 100;
        }
        public static void LoadContent(SpriteSheet finnAT)
        {
            _spritePerso = new AnimatedSprite(finnAT);
        }
        public static void Update()
        {
            _animationPerso = "idle";   
        }
        public static void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            
            _spriteBatch.Draw(_spritePerso, _positionPerso);
            
        }
    }

}
