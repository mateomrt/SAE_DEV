using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using Microsoft.Xna.Framework;
using SAE_DEV;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework.Input;

namespace SAE_DEV.Screens
{
    internal class GameOver : GameScreen
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _gameOver;
        private Texture2D _OuiNon;
        private Texture2D _Voulez;
        private Vector2 _posGameOver;
        private Vector2 _posVoulez;
        private Vector2 _posOuiNon;
        private Vector2 _positionClique;
        private bool _isClicked;



        private new Game1 Game => (Game1)base.Game;

        public GameOver(Game1 game) : base(game)
        {

        }

        public override void Initialize()
        {
            _posGameOver = new Vector2(458, 224);
            _posOuiNon = new Vector2(500, 446);
            _posVoulez = new Vector2(300, 336);
            
            base.Initialize();
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameOver = Content.Load<Texture2D>("GameOver");
            _OuiNon = Content.Load<Texture2D>("OuiNon");
            _Voulez = Content.Load<Texture2D>("Voulez-vous");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            Console.WriteLine(" x : " + Mouse.GetState().X + " y : " + Mouse.GetState().Y);
            _isClicked = false;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _positionClique = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                _isClicked = true;

            }
            if (_positionClique.X > 515 && _positionClique.X < 582 && _positionClique.Y > 464 && _positionClique.Y < 493 && _isClicked == true)
            {
                Game.LoadMenu();
            }
            else if (_positionClique.X > 749 && _positionClique.X < 834 && _positionClique.Y > 466 && _positionClique.Y < 494 && _isClicked == true)
            {
                Game.Exit();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_gameOver, _posGameOver, Color.White);
            _spriteBatch.Draw(_OuiNon, _posOuiNon, Color.White);
            _spriteBatch.Draw(_Voulez, _posVoulez, Color.White);
            _spriteBatch.End();
        }
    }
}
