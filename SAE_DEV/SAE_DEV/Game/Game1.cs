using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using SAE_DEV.Screens;

namespace SAE_DEV
{
    public class Game1 : Game
    {
        public static int MAP1_TAILLE = 800;
        public static int MAP2_TAILLE = 560;
        public float deltaTime;
        private GraphicsDeviceManager _graphics;
        public static int _screenWidth = 1280;
        public static int _screenHeight = 720;
        public static int _choixMap;
        public SpriteBatch SpriteBatch { get; private set; }

        private ScreenManager _screenManager;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
            
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 720;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Window.Title = "Sae Dev";
            _screenManager = new ScreenManager();
            

            base.Initialize();
        }
        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds; // TIME
            
            _screenManager.Update(gameTime);

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Black);
            _screenManager.Draw(gameTime);

            base.Draw(gameTime);
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
        public void LoadGameOver()
        {
            LoadScreen(new GameOver(this));
        }
        protected override void LoadContent()
        {
            LoadMenu();
        }

    }
}