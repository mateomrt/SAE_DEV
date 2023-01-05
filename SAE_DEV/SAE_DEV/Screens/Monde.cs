using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
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

        public Monde(Game1 game) : base(game)
        {

        }

        public override void Initialize()
        {
            character = new Perso();
            character.Position = new Vector2(100, 200);
            character.LoadContent(Game);
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Pink);
            
            character.Draw(Game.SpriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            character.Update(deltaTime);
            _keyboardState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                Game.LoadMenu();
            }
        }
    }
}
