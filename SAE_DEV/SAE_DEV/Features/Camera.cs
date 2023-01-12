using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace SAE_DEV
{
    internal class Camera
    {
        public static OrthographicCamera _camera;
        public static Vector2 _cameraPosition;

        public static void Initialise(BoxingViewportAdapter viewportAdapter)
        {
            //Initialisation de la caméra + sa position + son zoom
            _camera = new OrthographicCamera(viewportAdapter);
            _cameraPosition = new Vector2(Perso._positionPerso.X, Perso._positionPerso.Y);
            _camera.ZoomIn(1.5f);
        }
        public static void Update()
        {
            //On déplace la caméra avec le perso
            _cameraPosition = new Vector2(Perso._positionPerso.X, Perso._positionPerso.Y);

            
            // ON CHANGE LE MOYEN DE BLOQUER LA CAMERA CAR LES 2 MAP NE FONT PAS LA MEME TAILLE
            if (Game1._choixMap == 1)
            {
                // On fixe la caméra quand on arrive a gauche
                if (Perso._positionPerso.X < Game1.SCREEN_WIDTH / 5)
                    _cameraPosition.X = Game1.SCREEN_WIDTH / 5;
                // On fixe la caméra quand on arrive a droite
                if (Perso._positionPerso.X > (Game1.MAP1_TAILLE - Game1.SCREEN_WIDTH / 5))
                    _cameraPosition.X = (Game1.MAP1_TAILLE - Game1.SCREEN_WIDTH / 5);
                // On fixe la caméra quand on arrive en haut
                if (Perso._positionPerso.Y < Game1.SCREEN_HEIGHT / 5)
                    _cameraPosition.Y = Game1.SCREEN_HEIGHT / 5;
                // On fixe la caméra quand on arrive en bas
                if (Perso._positionPerso.Y > (Game1.MAP1_TAILLE - Game1.SCREEN_HEIGHT / 5))
                    _cameraPosition.Y = (Game1.MAP1_TAILLE - Game1.SCREEN_HEIGHT / 5);
            }
            if (Game1._choixMap == 2)
            {
                // On fixe la caméra quand on arrive a gauche
                if (Perso._positionPerso.X < Game1.SCREEN_WIDTH / 5)
                    _cameraPosition.X = Game1.SCREEN_WIDTH / 5;
                // On fixe la caméra quand on arrive a droite
                if (Perso._positionPerso.X > (Game1.MAP2_TAILLE - Game1.SCREEN_WIDTH / 5))
                    _cameraPosition.X = (Game1.MAP2_TAILLE - Game1.SCREEN_WIDTH / 5);
                // On fixe la caméra quand on arrive en haut
                if (Perso._positionPerso.Y < Game1.SCREEN_HEIGHT / 5)
                    _cameraPosition.Y = Game1.SCREEN_HEIGHT / 5;
                // On fixe la caméra quand on arrive en bas
                if (Perso._positionPerso.Y > (Game1.MAP2_TAILLE - Game1.SCREEN_HEIGHT / 5))
                    _cameraPosition.Y = (Game1.MAP2_TAILLE - Game1.SCREEN_HEIGHT / 5);
            }


            _camera.LookAt(_cameraPosition);
        }
        
    }
}
