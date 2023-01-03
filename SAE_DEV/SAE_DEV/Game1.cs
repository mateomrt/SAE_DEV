using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;

using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;

namespace SAE_DEV
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteBatch test;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;


        private Vector2 _positionPerso;
        private AnimatedSprite _perso;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            Window.Title = "Le jeu de la mort qui tue";

            _positionPerso = new Vector2(20, 230);

            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tiledMap = Content.Load<TiledMap>("map1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            // TODO: use this.Content to load your game content here

            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("FinnSprite.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _tiledMapRenderer.Update(gameTime);

            // TODO: Add your update logic here

            // TIME
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            // CHARACTER
            _perso.Play("idle");

            _perso.Update(deltaTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _tiledMapRenderer.Draw();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}