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
        public int MAP1_TAILLE = 800;
        public int MAP2_TAILLE = 560;
        private KeyboardState _keyboardState;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Vector2 _positionZombie;
        private AnimatedSprite _Zombielvl1;
        private Vector2 _positionPerso;
        private int _vitessePerso;
        private AnimatedSprite _perso;
        private string _animationPerso;
        Zombie zombielvl1;



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

            _positionPerso = new Vector2(400, 400);
            _vitessePerso = 100;
            //Zombie
            zombielvl1 = new Zombie();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("ZombieToast_50.sf", new JsonContentLoader());
            zombielvl1.SpriteZombie = new AnimatedSprite(spriteSheet);
            
            
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
            _keyboardState = Keyboard.GetState();
            _Zombielvl1.Play("idle");
            _animationPerso = "idle";
            Vector2 direction = Vector2.Zero;

            _Zombielvl1.Update(deltaTime);
            _perso.Update(deltaTime);
           
            //Deplacement du perso
            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                _animationPerso = "running";
                direction.X += _vitessePerso;
            }
            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                _animationPerso = "running";
                direction.Y -= _vitessePerso;
            }
            if (_keyboardState.IsKeyDown(Keys.Down))
            {
                _animationPerso = "running";
                direction.Y += _vitessePerso;
            }
            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                _animationPerso = "running";
                direction.X -= _vitessePerso;
            }
            if (direction == Vector2.Zero)
            {
                return;
            }
            _positionPerso += direction * deltaTime;
            _perso.Play(_animationPerso); // une des animations définies dans « persoAnimation.sf »
            _perso.Update(deltaTime); // temps écoulé


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