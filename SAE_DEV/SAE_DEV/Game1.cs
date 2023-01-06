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

namespace SAE_DEV
{
    public class Game1 : Game
    {
        public int MAP1_TAILLE = 800;
        public int MAP2_TAILLE = 560;
        private TiledMapTileLayer mapLayer;
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
        private OrthographicCamera _camera;
        private float _positionCameraX;
        private float _positionCameraY;
        private int _screenWidth;
        private int _screenHeight;




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

            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            Window.Title = "Sae Dev";

            _positionPerso = new Vector2(150, 250);
            _vitessePerso = 150;
            _positionZombie = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            _screenWidth = 1280;
            _screenHeight = 720;

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, _screenWidth, _screenHeight);
            _camera = new OrthographicCamera(viewportAdapter);
            _camera.Position = new Vector2(_screenWidth,_screenHeight);
            _camera.ZoomIn(1.5f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("ZombieToast_50.sf", new JsonContentLoader());
            _Zombielvl1 = new AnimatedSprite(spriteSheet);
            SpriteSheet spritePerso = Content.Load<SpriteSheet>("FinnSprite.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spritePerso);

            _tiledMap = Content.Load<TiledMap>("map1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Batiment");

        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds; // TIME
            float walkSpeed = deltaTime * _vitessePerso; // Vitesse de déplacement du sprite
            _keyboardState = Keyboard.GetState();



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            


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
                ushort tx = (ushort)(_positionPerso.X/ _tiledMap.TileWidth +0.5);
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
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth -0.5);
                _animationPerso = "running";
                if (!IsCollision(tx, ty))
                {
                    _positionPerso.Y -= walkSpeed;
                }
                
            }
            if (_keyboardState.IsKeyDown(Keys.Down))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth +0.5);
                _animationPerso = "running";
                if (!IsCollision(tx, ty))
                {
                    _positionPerso.Y += walkSpeed;
                }
                
            }
            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth -0.5);
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

            
            


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);

            var transformMatrix = _camera.GetViewMatrix();
            
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            
            _tiledMapRenderer.Draw(transformMatrix);
            

            _spriteBatch.Draw(_Zombielvl1, _positionZombie);
             _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End();





            base.Draw(gameTime);
        }



        private bool IsCollision(ushort x, ushort y)
        {
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x,y, out tile) == false )
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }

        private Vector2 GetMovementDirection()
        {
            var movementDirection = Vector2.Zero;
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Down))
            {
                movementDirection += Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                movementDirection -= Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                movementDirection -= Vector2.UnitX;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                movementDirection += Vector2.UnitX;
            }
            return movementDirection;
        }


    }
}