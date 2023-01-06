using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using SAE_DEV;
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

namespace SAE_DEV.Screens
{
    internal class Monde : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        Perso character;
        private KeyboardState _keyboardState;
        Zombie zombielvl1;
        IAZombie iazombie;

        public int MAP1_TAILLE = 800;
        public int MAP2_TAILLE = 560;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer mapLayer;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _positionZombie;
        private AnimatedSprite _Zombielvl1;
        private Vector2 _positionPerso;
        private int _vitessePerso;
        private AnimatedSprite _perso;
        private string _animationPerso;
        private OrthographicCamera _camera;
        private float _positionCameraX;
        private float _positionCameraY;
        private int _screenWidth;
        private int _screenHeight;

        public Monde(Game1 game) : base(game)
        {

        }


        public override void Initialize()
        {
            character = new Perso();
            character.Position = new Vector2(140, 210);
            character.LoadContent(Game);
            zombielvl1 = new Zombie();
            zombielvl1.PositionZombie = new Vector2(300, 400);
            iazombie = new IAZombie(75, character, zombielvl1);
            _screenWidth = 1280;
            _screenHeight = 720;

            var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, _screenWidth, _screenHeight);
            _camera = new OrthographicCamera(viewportAdapter);
            _camera.Position = new Vector2(_screenWidth, _screenHeight);
            _camera.ZoomIn(1.5f);


            _positionPerso = new Vector2(150, 250);
            _vitessePerso = 150;
            _positionZombie = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);


            base.Initialize();
        }
        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("map1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Batiment");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("ZombieToast_50.sf", new JsonContentLoader());
            _Zombielvl1 = new AnimatedSprite(spriteSheet);
            SpriteSheet spritePerso = Content.Load<SpriteSheet>("FinnSprite.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spritePerso);

            zombielvl1.LoadContent(Game);

            base.LoadContent();
        }

        

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaTime * _vitessePerso;

            _Zombielvl1.Play("idle");

            _animationPerso = "idle";

            _positionCameraX = _positionPerso.X;
            _positionCameraY = _positionPerso.Y;

            // En a gauche
            if (_positionPerso.X < _screenWidth / 5)
                _positionCameraX = _screenWidth / 5;
            // A droite
            if (_positionPerso.X > (MAP1_TAILLE - _screenWidth / 5))
                _positionCameraX = (MAP1_TAILLE - _screenWidth / 5);
            //en haut
            if (_positionPerso.Y < _screenHeight / 5)
                _positionCameraY = _screenHeight / 5;
            // en bas
            if (_positionPerso.Y > (MAP1_TAILLE - _screenHeight / 5))
                _positionCameraY = (MAP1_TAILLE - _screenHeight / 5);



            //Deplacement du perso + collisions
            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth);
                _animationPerso = "running";
                if (!IsCollision(tx, ty))
                {
                    _positionPerso.X += walkSpeed;
                }
            }
            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth - 0.5);
                _animationPerso = "running";
                if (!IsCollision(tx, ty))
                {
                    _positionPerso.Y -= walkSpeed;
                }

            }
            if (_keyboardState.IsKeyDown(Keys.Down))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth + 0.5);
                _animationPerso = "running";
                if (!IsCollision(tx, ty))
                {
                    _positionPerso.Y += walkSpeed;
                }

            }
            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth);
                _animationPerso = "running";
                if (!IsCollision(tx, ty))
                {
                    _positionPerso.X -= walkSpeed;
                }
            }

            _camera.LookAt(new Vector2(_positionCameraX, _positionCameraY));


            _perso.Play(_animationPerso); // on joue l'animation du perso
            _perso.Update(deltaTime); // temps écoulé
            _tiledMapRenderer.Update(gameTime);
            _Zombielvl1.Update(deltaTime);

            _tiledMapRenderer.Update(gameTime);
            _keyboardState = Keyboard.GetState();
            character.Update(deltaTime);
            iazombie.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                Game.LoadMenu();
            }
            
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var transformMatrix = _camera.GetViewMatrix();

            _spriteBatch.Begin(transformMatrix: transformMatrix);
          

            _tiledMapRenderer.Draw(transformMatrix);
            _spriteBatch.End();


        }
        private bool IsCollision(ushort x, ushort y)
        {
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
    }
}
