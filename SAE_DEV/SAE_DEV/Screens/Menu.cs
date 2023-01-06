using Microsoft.Xna.Framework;
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
    internal class Menu : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        public Menu(Game1 game) : base(game)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Game.LoadMonde();
            }
        }
    }
}
