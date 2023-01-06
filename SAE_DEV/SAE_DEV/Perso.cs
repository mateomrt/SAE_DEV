using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;

namespace SAE_DEV
{
    internal class Perso
    {

        public static AnimatedSprite _spritePerso;
        public static int _vitesseMvt;
        public static Vector2 _positionPerso;   
        public static string _animationPerso;

        public static void Initialize()
        {   
            _vitesseMvt = 100;
            _positionPerso = new Vector2(150,250);
        }

        public static void LoadContent(SpriteSheet Perso)
        {
            _spritePerso = new AnimatedSprite(Perso);
        }
        public static void Update()
        {
            _animationPerso = "idle";
        }
        public static void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_spritePerso, _positionPerso);
        }
    }

}
