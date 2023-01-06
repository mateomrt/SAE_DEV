using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV
{
    internal class Camera
    {
        public static OrthographicCamera _camera;
        public static Vector2 _cameraPosition;

        public static void Initialise(BoxingViewportAdapter viewportadapter)
        {
            _camera = new OrthographicCamera(viewportadapter);
            _cameraPosition = new Vector2(Perso._positionPerso.X, Perso._positionPerso.Y);
            _camera.ZoomIn(1.5f);
        }
        public static void Update()
        {
            _cameraPosition = new Vector2(Perso._positionPerso.X, Perso._positionPerso.Y);

            if (Perso._positionPerso.X < Game1._screenWidth / 5)
                _cameraPosition.X = Game1._screenWidth / 5;
            if (Perso._positionPerso.X > (Game1.MAP1_TAILLE - Game1._screenWidth / 5))
                _cameraPosition.X = (Game1.MAP1_TAILLE - Game1._screenWidth / 5);
            if (Perso._positionPerso.Y < Game1._screenHeight / 5)
                _cameraPosition.Y = Game1._screenHeight / 5;
            if (Perso._positionPerso.Y > (Game1.MAP1_TAILLE - Game1._screenHeight / 5))
                _cameraPosition.Y = (Game1.MAP1_TAILLE - Game1._screenHeight / 5);

            _camera.LookAt(_cameraPosition);

        }
    }
}
