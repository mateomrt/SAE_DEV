using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;
using System;
using MonoGame.Extended;

using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System.Xml.Linq;
using SAE_DEV.Screens;

namespace SAE_DEV
{
    public class Game1 : Game
    {
        public static int MAP1_TAILLE = 800;
        public static int MAP2_TAILLE = 560;
        private KeyboardState _keyboardState;
        private GraphicsDeviceManager _graphics;
        public static int _screenWidth;
        public static int _screenHeight;
        public SpriteBatch _SpriteBatch { get; set; }

        private ScreenManager _screenManager;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
            
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 720;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            _screenWidth = _graphics.PreferredBackBufferWidth;
            _screenHeight = _graphics.PreferredBackBufferHeight;

            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            Window.Title = "Sae Dev";


            _screenManager = new ScreenManager();

            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, _screenWidth, _screenHeight);
            Camera.Initialise(viewportadapter);

            Perso.Initialize();
            
            
            base.Initialize();
        }

        public void LoadScreen(GameScreen screen)
        {
            _screenManager.LoadScreen(screen, new FadeTransition(GraphicsDevice, Color.Black, .5f));

        }
        

        public void LoadMenu()
        {
            LoadScreen(new Menu(this));
        }
        public void LoadMonde()
        {
            LoadScreen(new Monde(this));
        }

        protected override void LoadContent()
        {
            _SpriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet spriteZombie = Content.Load<SpriteSheet>("ZombieToast_50.sf", new JsonContentLoader());

            SpriteSheet spritePerso = Content.Load<SpriteSheet>("FinnSprite.sf", new JsonContentLoader());
            Perso.LoadContent(spritePerso);

            Monde.LoadContent(Content, GraphicsDevice);

            LoadMenu();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds; // TIME

            float walkSpeed = deltaTime * Perso._vitesseMvt;
            Perso.Update();
            Touche.Presse(Perso._positionPerso, Monde._tiledMap1, Perso._animationPerso, walkSpeed, _graphics);

            Camera.Update();

            Perso._spritePerso.Play(Perso._animationPerso);
            Perso._spritePerso.Update(deltaTime);
            Monde.Update(gameTime);

            _screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Black);

            var transformMatrix = Camera._camera.GetViewMatrix();

            _SpriteBatch.Begin(transformMatrix: transformMatrix);

            Monde.Draw(transformMatrix);
            Perso.Draw(_SpriteBatch);

            

            _screenManager.Draw(gameTime);

            base.Draw(gameTime);
        }


    }
}