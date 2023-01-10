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
            //Initialisation de la position et de la vitesse du personnage
            _positionPerso = new Vector2(150, 250);
            vitesse_mvt = 200 ;
        }
        public static void LoadContent(SpriteSheet spriteSheet)
        {
            //on affecte la texture de notre personnage
            _spritePerso = new AnimatedSprite(spriteSheet);
        }
        public static void Update()
        {
            _animationPerso = "idle";
        }
        public static void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            
            //On dessine notre personnage avec sa texture et sa position
            _spriteBatch.Draw(_spritePerso, _positionPerso);
            
        }
    }

}
