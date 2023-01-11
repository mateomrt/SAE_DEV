using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SAE_DEV.Screens;

namespace SAE_DEV
{
    public class Bullet
    {
        private float LinearVelocity = 4f;

        private Vector2 _position;
        private Vector2 _direction;


        private Texture2D _sprite;

        public bool _collision;

        public Bullet(Vector2 position, Vector2 direction, Texture2D bulletSprite)
        {
            this.Position = position;
            this._direction = direction;
            this._sprite = bulletSprite;
        }

        public Vector2 Position { get => _position; set => _position = value; }

        public void Draw(SpriteBatch spriteBatch)
        {
            // On dessine la texture
            spriteBatch.Draw(_sprite, Position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            float _timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _collision = false;
            Position += _direction * LinearVelocity;

            BulletCollision();
        }

        public bool BulletCollision()
        {
            //COLLISION DES BALLES AVEC LES OBSTACLES
            _collision = false;
            ushort tx = (ushort)(Position.X / Monde._tiledMap.TileWidth);
            ushort ty = (ushort)(Position.Y / Monde._tiledMap.TileWidth);
            if (Collision.IsCollision(tx, ty))
                _collision = true;

            return _collision;
        }

    }
}