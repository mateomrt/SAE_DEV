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
using System.Diagnostics.Metrics;
using Transform;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SAE_DEV.Screens
{
    internal class Monde : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        Zombie[] zombielvl1;
        IAZombie[] iazombie;
        public Vector2 positionGameOver;

        public static int MAP1_TAILLE = 800;
        public static int MAP2_TAILLE = 560;
        public const int nbZombie = 50;
        public static TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        public static SpriteSheet _spritePerso;

        List<Bullet> bullets = new List<Bullet>();
        public Texture2D _spriteBullet;
        public MouseState previousState;
        public MouseState currentState;

        public static Matrix transformMatrix;

        public Texture2D _textureCoeurVide;
        public Texture2D _textureCoeurPlein;
        
        public Texture2D _textureZQSD;
        public Texture2D _textureFlecheDirectionnelle;
        public Texture2D _textureCliqueGauche;
        public Texture2D _texturePhraseFonction;
        public Texture2D _texturePhraseInvincible;


        private bool _affichePhraseInvinsible;

        private SpriteBatch _spriteBatch;
        
        
        private float _chrono;
        private float _chronoInvincible;
        private bool _affiche;


        private int _screenWidth;
        private int _screenHeight;

        private Song _rhoff;
        private Song _ball;

        Random random = new Random();
        private bool EstInvincible;




        public Monde(Game1 game) : base(game)
        {

        }


        public override void Initialize()
        {

            

            zombielvl1 = new Zombie[nbZombie];
            iazombie = new IAZombie[nbZombie];
            
            

            for (int i = 0; i < zombielvl1.Length; i++)
            {
                zombielvl1[i] = new Zombie();

                if (Game1._choixMap == 1)
                {
                    
                    
                    zombielvl1[i].PositionZombie = new Vector2(random.Next(518, 1078), random.Next(354, 598));
                    
                    
                }
                if (Game1._choixMap == 2)
                {
                    
                    zombielvl1[i].PositionZombie = new Vector2(random.Next(244, 1036), random.Next(138, 556));
                    
                    
                }
                

                


                iazombie[i] = new IAZombie(random.Next(40, 60), zombielvl1[i]);

            }
            _screenWidth = 1280;
            _screenHeight = 720;

            Perso.Initialize();
            positionGameOver = new Vector2(_screenWidth, _screenHeight);
            // INITIALISATION DE LA CAMÉRA
            var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, _screenWidth, _screenHeight);
            Camera.Initialise(viewportAdapter);


            _chrono = 0;
            _affiche = true;
            _affichePhraseInvinsible = false;


            base.Initialize();
        }

        public override void LoadContent()
        {
            if (Game1._choixMap == 1)
            {
                _tiledMap = Content.Load<TiledMap>("map1");
            }
            else if (Game1._choixMap == 2)
            {
                _tiledMap = Content.Load<TiledMap>("map2");
            }

            _textureCoeurPlein = Content.Load<Texture2D>("coeurPlein");
            _textureCoeurVide = Content.Load<Texture2D>("coeurVide");
            
            
            _textureCliqueGauche = Content.Load<Texture2D>("cliqueGauche");
            _textureFlecheDirectionnelle = Content.Load<Texture2D>("flecheDirectionnelle");
            _textureZQSD = Content.Load<Texture2D>("zqsd");
            _texturePhraseFonction = Content.Load<Texture2D>("phraseFonction");



            _texturePhraseInvincible = Content.Load<Texture2D>("phraseInvincible");


            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Chargement texture Perso
            _spritePerso = Content.Load<SpriteSheet>("elf_spritesheet.sf", new JsonContentLoader());
            _spriteBullet = Content.Load<Texture2D>("Bullet");
            Perso.LoadContent(_spritePerso);

            for (int i = 0; i < zombielvl1.Length; i++)
            {
                zombielvl1[i].LoadContent(Game);
                zombielvl1[i].SpawnDuZombie();
            }

            _ball = Content.Load<Song>("bruitBalle");

            _rhoff = Content.Load<Song>("Rohff - La Puissance");
            MediaPlayer.Play(_rhoff);

            base.LoadContent();
        }



        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaTime * Perso.vitesse_mvt;

            

            Perso.Update();

            //On vérifie si une touche est pressée DANS cette classe
            Touche.Presse(Perso._positionPerso, _tiledMap, Perso._animationPerso, walkSpeed, deltaTime);

            // Creation des balles et mise à jour
            previousState = currentState;
            currentState = Mouse.GetState();
            if (currentState.LeftButton == ButtonState.Pressed &&
                previousState.LeftButton == ButtonState.Released)
            {
                CreateBullet();
                MediaPlayer.Play(_ball);
            }
            foreach (Bullet bullet in bullets)
            {
                bullet.Update(gameTime);
                
            }

            //On joue ici l'animation du perso
            Perso._spritePerso.Play(Perso._animationPerso);
            //Si on ne se déplace plus on fait l'animation "idle"
            Perso._spritePerso.Update(deltaTime);


            // UPDATE DE LA POSITION CAMERA
            Camera.Update();


            //Ici on charge la map
            _tiledMapRenderer.Update(gameTime);


            for (int i = 0; i < zombielvl1.Length; i++)
            {
                iazombie[i].Update(gameTime);

            }


            //Touche Y pour retourner au menu du jeu
           
            if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                Game.LoadMenu();
            }
            _chronoInvincible += 1 * deltaTime;
            for (int i = 0; i < zombielvl1.Length; i++)
            {
                if (Math.Sqrt(
                    Math.Pow(Perso._positionPerso.X - zombielvl1[i].PositionZombie.X, 2) + 
                    Math.Pow(Perso._positionPerso.Y - zombielvl1[i].PositionZombie.Y, 2)) < 10 && EstInvincible == false)
                {

                    Perso.vie -= 1;
                    EstInvincible = true;
                    _chronoInvincible = 0;
                    

                }
            }
            if (EstInvincible == true)
            {
                _affichePhraseInvinsible = true;
            }
            else
                _affichePhraseInvinsible = false;
            Console.WriteLine(_chronoInvincible);
            if(_chronoInvincible > 5)
            {
                EstInvincible = false;
            }

            
            
            foreach (Bullet bullet in bullets.ToArray()) 
            {
                for(int j = 0; j < zombielvl1.Length; j++)
                {
                    if(Math.Sqrt(
                    Math.Pow(bullet.Position.X - zombielvl1[j].PositionZombie.X, 2) +
                    Math.Pow(bullet.Position.Y - zombielvl1[j].PositionZombie.Y, 2)) < 12)
                    {

                        zombielvl1[j].SpawnDuZombie();
                        
                        bullets.Remove(bullet);
                        
                    }
                }
            }


            // CHRONO POUR L4AFFICHAGE DES COMMANDES
            _chrono = _chrono + 1 * deltaTime;
            if (_chrono > 5)
            {
                _affiche = false;
            }
            else
                _affiche = true;




            if (Perso.vie == 0)
            {
                Game.LoadGameOver();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //On met en place la caméra
            transformMatrix = Camera._camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);

            //On dessine notre Perso
            Perso.Draw(_spriteBatch);

            for (int i = 0; i < zombielvl1.Length; i++)
            {
                zombielvl1[i].Draw(_spriteBatch);
            }

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(_spriteBatch);
            }

            

            
            
            //On dessine la map avec la "vue" de la caméra
            _tiledMapRenderer.Draw(transformMatrix);

            


            _spriteBatch.End();

            _spriteBatch.Begin();

            // AFFICHAGE DE LA VIE
            Vector2 _postionCoeur = new Vector2(10,40);

            for (int i = 0; i < Perso.vie; i++)
            {
                _spriteBatch.Draw(_textureCoeurPlein, new Vector2(_postionCoeur.X + 40 + (80 * i), _postionCoeur.Y), Color.White);
            }
            for (int i = 0; i < 5 - Perso.vie; i++)
            {
                _spriteBatch.Draw(_textureCoeurVide, new Vector2(_postionCoeur.X + 40 + (80 * Perso.vie) + 80 * i, _postionCoeur.Y), Color.White);
            }

            //AFFICHAGE DES COMMANDES
            if (_affiche)
            {
                _spriteBatch.Draw(_textureZQSD, new Vector2(400, 550), Color.White);
                _spriteBatch.Draw(_textureFlecheDirectionnelle, new Vector2(230, 550), Color.White);
                _spriteBatch.Draw(_textureCliqueGauche, new Vector2(930, 550), Color.White);
                _spriteBatch.Draw(_texturePhraseFonction, new Vector2(190, 500), Color.White);
            }

            if (_affichePhraseInvinsible == true)
            {
                _spriteBatch.Draw(_texturePhraseInvincible, new Vector2(100, 400), Color.White);
            }

            


            _spriteBatch.End();

            
            

        }

        private void CreateBullet()
        {
            Vector2 test = ScreenToWorldSpace(Mouse.GetState().Position.ToVector2(), transformMatrix);
            bullets.Add(new Bullet(Perso._positionPerso, Vector2.Normalize(test - Perso._positionPerso), _spriteBullet));
        }
        
        public Vector2 ScreenToWorldSpace(in Vector2 point, Matrix transformMatrix)
        {
            Matrix invertedMatrix = Matrix.Invert(transformMatrix);
            return Vector2.Transform(point, invertedMatrix);
        }

    }
}
