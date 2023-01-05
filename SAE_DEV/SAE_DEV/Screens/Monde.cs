using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using SAE_DEV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV.Screens
{
    internal class Monde : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        Perso character;
        private KeyboardState _keyboardState;
        Zombie zombielvl1;

        public int MAP1_TAILLE = 800;
        public int MAP2_TAILLE = 560;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        public Monde(Game1 game) : base(game)
        {

        }

        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("map1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            zombielvl1.LoadContent(Game);
            base.LoadContent();
        }

        public override void Initialize()
        {
            character = new Perso();
            character.Position = new Vector2(100, 200);
            character.LoadContent(Game);
            zombielvl1 = new Zombie();
            zombielvl1.PositionZombie = new Vector2(200, 200);
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _tiledMapRenderer.Draw();

            character.Draw(Game.SpriteBatch);
            zombielvl1.Draw(Game.SpriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _tiledMapRenderer.Update(gameTime);
            _keyboardState = Keyboard.GetState();
            character.Update(deltaTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                Game.LoadMenu();
            }
        }
    }
}
