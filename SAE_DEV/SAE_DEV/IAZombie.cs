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
     
            Vector2 direction = Perso._positionPerso - zombie.PositionZombie;
            direction.Normalize();
            
            zombie.PositionZombie += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        }



    }
}
