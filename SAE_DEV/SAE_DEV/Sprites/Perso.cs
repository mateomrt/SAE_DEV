using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;

namespace SAE_DEV
{
    internal class Perso
    {

        public static AnimatedSprite _spritePerso;
        public static string _animationPerso;
        public static Vector2 _positionPerso;
        public static float _vitesse_mvt;
        public static int _vie;


        public static void Initialize()
        {
            //Initialisation de la position et de la vitesse du personnage et de sa vie
            _positionPerso = new Vector2(150, 250);
            _vitesse_mvt = 100 ;
            _vie = 5;
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
