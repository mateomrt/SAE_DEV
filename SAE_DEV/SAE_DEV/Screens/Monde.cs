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
        public static Perso character;
        Zombie[] zombielvl1;
        IAZombie[] iazombie;


        public static int MAP1_TAILLE = 800;
        public static int MAP2_TAILLE = 560;
        public static TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        public static SpriteSheet _spritePerso;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private int _screenWidth;
        private int _screenHeight;

        public Monde(Game1 game) : base(game)
        {

        }


        public override void Initialize()
        {
            

            zombielvl1 = new Zombie[10];
            iazombie = new IAZombie[10];
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                zombielvl1[i] = new Zombie();
                zombielvl1[i].PositionZombie = new Vector2(random.Next(50, 800), random.Next(50, 800));

                iazombie[i] = new IAZombie(random.Next(40, 100), zombielvl1[i]);
               

            }
            _screenWidth = 1280;
            _screenHeight = 720;

            Perso.Initialize();

            // INITIALISATION DE LA CAMÉRA
            var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, _screenWidth, _screenHeight);
            Camera.Initialise(viewportAdapter);


            base.Initialize();
        }
        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("map1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            _spritePerso = Content.Load<SpriteSheet>("FinnSprite.sf", new JsonContentLoader());
            Perso.LoadContent(_spritePerso);

            for(int i = 0; i < 10; i++)
            {
                zombielvl1[i].LoadContent(Game);
            }
            

            base.LoadContent();
        }

        

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaTime * Perso.vitesse_mvt;


            Perso.Update();
            
            Touche.Presse(Perso._positionPerso, _tiledMap, Perso._animationPerso, walkSpeed, deltaTime);


            Perso._spritePerso.Play(Perso._animationPerso);
            Perso._spritePerso.Update(deltaTime);


            // UPDATE DE LA POSITION CAMERA
            Camera.Update();


            _tiledMapRenderer.Update(gameTime);
            

            for(int i = 0; i < 10; i++)
            {
                iazombie[i].Update(gameTime);

            }
                

            if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                Game.LoadMenu();
            }
            
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var transformMatrix = Camera._camera.GetViewMatrix();

            _spriteBatch.Begin(transformMatrix: transformMatrix);

          //  Console.WriteLine("Draw : " + Perso._positionPerso);
            Perso.Draw(_spriteBatch);
            
            for(int i = 0; i < 10; i++)
            {
                zombielvl1[i].Draw(_spriteBatch);
            }
            
            _tiledMapRenderer.Draw(transformMatrix);

            _spriteBatch.End();
            
        }
        
    }
}
