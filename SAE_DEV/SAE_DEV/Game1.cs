using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;
using Comora;

using System;

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

        private Camera _camera;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
            
        protected override void Initialize()
        {
            //_graphics.PreferredBackBufferWidth = 800;  // set this value to the desired width of your window
            //_graphics.PreferredBackBufferHeight = 800;   // set this value to the desired height of your window
            //_graphics.ApplyChanges();

            // CAMERA
            _camera = new Camera(_graphics.GraphicsDevice);
            

            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            Window.Title = "Sae Dev";

            _positionPerso = new Vector2(150, 50);
            _vitessePerso = 150;
            _positionZombie = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("ZombieToast_50.sf", new JsonContentLoader());
            _Zombielvl1 = new AnimatedSprite(spriteSheet);
            SpriteSheet spritePerso = Content.Load<SpriteSheet>("FinnSprite.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spritePerso);

            _tiledMap = Content.Load<TiledMap>("map2");
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

            _tiledMapRenderer.Update(gameTime);


            _Zombielvl1.Play("idle");
            
            _animationPerso = "idle";
            

            

            //Deplacement du perso
            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(_positionPerso.X/ _tiledMap.TileWidth +0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileWidth);
                _animationPerso = "running";
                if (!IsCollision(tx, ty))
                {
                    _positionPerso.X += walkSpeed;
                    _camera.Position += new Vector2(walkSpeed,0);
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
                    _camera.Position = _positionPerso;
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
                    _camera.Position = _positionPerso;
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
                    _camera.Position = _positionPerso;
                }
            }

            
            _perso.Play(_animationPerso); // on joue l'animation du perso
            _perso.Update(deltaTime); // temps écoulé
            _Zombielvl1.Update(deltaTime);

            //CAMERA
            //_camera.Position = _positionPerso;
            _camera.Update(gameTime);
            


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);

            
            _spriteBatch.Begin();
            _tiledMapRenderer.Draw();
            
            
            _spriteBatch.Draw(_Zombielvl1, _positionZombie);

            _spriteBatch.End();


            _spriteBatch.Begin(_camera);
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

        
    }
}