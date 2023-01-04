using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;

using System;

namespace SAE_DEV
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Vector2 _positionZombie;
        private AnimatedSprite _Zombielvl1;
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

            Window.Title = "Sae Dev";

            _positionPerso = new Vector2(20, 230);

            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _positionZombie = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            _positionPerso = new Vector2(GraphicsDevice.Viewport.Width / 2 + 50, GraphicsDevice.Viewport.Height / 2);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("ZombieToast_50.sf", new JsonContentLoader());
            _Zombielvl1 = new AnimatedSprite(spriteSheet);
            SpriteSheet spritePerso = Content.Load<SpriteSheet>("FinnSprite.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spritePerso);

            _tiledMap = Content.Load<TiledMap>("map1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _tiledMapRenderer.Update(gameTime);

            // TODO: Add your update logic here
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds; // TIME
            // TODO: Add your update logic here
            _Zombielvl1.Play("idle");
            _perso.Play("idle");
            _Zombielvl1.Update(deltaTime);
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _tiledMapRenderer.Draw();


            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_Zombielvl1, _positionZombie);
            _spriteBatch.End();

            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}