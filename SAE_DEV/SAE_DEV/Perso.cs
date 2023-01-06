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

namespace SAE_DEV
{
    internal class Perso
    {

        private AnimatedSprite _spritePerso;

        private Vector2 position;
        private float vitesse_mvt;
        private Vector2 direction;




        public Vector2 Position { get => position; set => position = value; }

        public void Initialize(Game game)
        {   
            vitesse_mvt = 100;
        }
        public void LoadContent(Game game)
        {
            SpriteSheet finnAT = game.Content.Load<SpriteSheet>("FinnSprite.sf", new JsonContentLoader());
            _spritePerso = new AnimatedSprite(finnAT);
        }
        public void Update(float deltaTime)
        {
            _spritePerso.Update(deltaTime);
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            
            _spriteBatch.Draw(_spritePerso, Position);
            
        }
    }

}
