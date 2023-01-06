using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;

namespace SAE_DEV.Screens
{
    internal class Monde : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        public Monde(Game1 game) : base(game)
        {

        }

        public static TiledMap _tiledMap1;
        public static TiledMap _tiledMap2;
        public static TiledMapRenderer _tiledMapRenderer1;
        public static TiledMapRenderer _tiledMapRenderer2;

        
         public static void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content, Microsoft.Xna.Framework.Graphics.GraphicsDevice GraphicsDevice)
        {
            _tiledMap1 = Content.Load<TiledMap>("map1");
            _tiledMap2 = Content.Load<TiledMap>("map2");
            _tiledMapRenderer1 = new TiledMapRenderer(GraphicsDevice, _tiledMap1);
            _tiledMapRenderer2 = new TiledMapRenderer(GraphicsDevice, _tiledMap2);
            
        }
        public static void Update(GameTime gameTime)
        {
            _tiledMapRenderer1.Update(gameTime);
            
        }
        public static void Draw(Matrix transformMatrix)
        {
            _tiledMapRenderer1.Draw(transformMatrix);
        }
        
    }
}
