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

        private Vector2 _position;

        private int sens_deplacment_DG;
        private int vitesse_mvt;

        public Vector2 Position { get => _position; set => _position = value; }

        public void Initialize(Game game)
        {   
            vitesse_mvt = 100;
        }
        public void LoadContent(Game game)
        {
            SpriteSheet finnAT = game.Content.Load<SpriteSheet>("FinnSprite.sf", new JsonContentLoader());
            _spritePerso = new AnimatedSprite(finnAT);
        }
        public void Update(float delaTime)
        {
            _spritePerso.Play("idle");
            _spritePerso.Update(delaTime);
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_spritePerso, Position);
        }
    }

}
