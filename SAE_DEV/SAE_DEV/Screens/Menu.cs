using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;

namespace SAE_DEV.Screens
{
    internal class Menu : GameScreen
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _phraseIntro;
        private Vector2 _posPhraseIntro;
        private Texture2D _textureMap1;
        private Vector2 _posmap1;
        private Texture2D _textureMap2;
        private Vector2 _posmap2;
        private Vector2 _positionClique;
        private Texture2D _map1;
        private Texture2D _map2;
        private Vector2 _posmapCapture1;
        private Vector2 _posmapCapture2;

        private bool _isClicked;
        private Song _menuMusique;
        private new Game1 Game => (Game1)base.Game;

        public Menu(Game1 game) : base(game)
        {

        }

        public override void Initialize()
        {
            _posPhraseIntro = new Vector2(181, 150);
            _posmap1 = new Vector2(383, 300);
            _posmap2 = new Vector2(741, 300);
            _posmapCapture1 = new Vector2(0, 0);
            _posmapCapture2 = new Vector2(650, 0);
            
            _isClicked = false;
            
            Game1._choixMap = 0;

            base.Initialize();
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _phraseIntro = Content.Load<Texture2D>("phrase_intro");
            _textureMap1 = Content.Load<Texture2D>("optionMap1");
            _textureMap2 = Content.Load<Texture2D>("optionMap2");
            _map1 = Content.Load<Texture2D>("Map1Capture");
            _map2 = Content.Load<Texture2D>("Map2Capture");
            _menuMusique = Content.Load<Song>("musiqueIntro");
            MediaPlayer.Play(_menuMusique);

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            _isClicked = false;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _positionClique = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                _isClicked = true;

            }
            if (_positionClique.X > 383 && _positionClique.X < 539 && _positionClique.Y > 300 && _positionClique.Y < 427 && _isClicked ==true)
            {
                Game1._choixMap = 1;
                Game.LoadMonde();
                MediaPlayer.Stop();
            }
            else if (_positionClique.X > 741 && _positionClique.X < 896 && _positionClique.Y > 300 && _positionClique.Y < 423 && _isClicked == true)
            {
                Game1._choixMap = 2;
                Game.LoadMonde();
                MediaPlayer.Stop();
            }

            // Dans le menu si on presse Échap on quitte le jeu
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();
        }
        public override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_map1, _posmapCapture1, Color.White);
            _spriteBatch.Draw(_map2, _posmapCapture2, Color.White);
            _spriteBatch.Draw(_phraseIntro, _posPhraseIntro, Color.White);
            _spriteBatch.Draw(_textureMap1, _posmap1, Color.White);
            _spriteBatch.Draw(_textureMap2, _posmap2, Color.White);
            _spriteBatch.End();
            

        }

    }
}
