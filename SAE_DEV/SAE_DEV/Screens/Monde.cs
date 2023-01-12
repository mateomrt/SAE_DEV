using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using Microsoft.Xna.Framework.Media;

namespace SAE_DEV.Screens
{
    internal class Monde : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        //Tableau de zombie et de IA et des balles
        Zombie[] zombielvl1;
        IAZombie[] iazombie;
        List<Bullet> bullets = new List<Bullet>();
        
        
        Random random = new Random();
        
        
        
        private int nbZombie;
        
        private MouseState previousState;
        private MouseState currentState;

        public Matrix transformMatrix; // Pour la caméra

        // On déclare toute les textures
        private TiledMapRenderer _tiledMapRenderer;
        public static TiledMap _tiledMap;
        private Texture2D _spriteBullet;
        private Texture2D _textureCoeurVide;
        private Texture2D _textureCoeurPlein;
        private Texture2D _textureZQSD;
        private Texture2D _textureFlecheDirectionnelle;
        private Texture2D _textureCliqueGauche;
        private Texture2D _texturePhraseFonction;
        private Texture2D _texturePhraseInvincible;
        private SpriteBatch _spriteBatch;
        public static SpriteSheet _spritePerso;

        private SpriteFont _font;
        private string _text;
        private int _score;

        private float _chrono;
        private float _chronoInvincible;
        private bool _affiche;

        private bool _affichePhraseInvinsible;
        private bool _EstInvincible;

        private int _screenWidth;
        private int _screenHeight;

        private Song _ball;
        private Song _death;

        public Monde(Game1 game) : base(game)
        {

        }


        public override void Initialize()
        {
            // NOMBRE DE ZOMBIE EN FONCTION DE LA MAP
            if (Game1._choixMap == 1)
            {
                nbZombie = 30;
            }
            else
                nbZombie = 15;

            //Initialisation des zombie, du perso et de l'IA des zombies
            zombielvl1 = new Zombie[nbZombie];
            iazombie = new IAZombie[nbZombie];
            
            // On fait spawn les zombies au bon endroit
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
            
            
            Perso.Initialize();
            
            
            
            //Initialisation du score
            _score = 0;
            _text = "Score : " + _score.ToString();
           

            // INITIALISATION DE LA CAMÉRA
            _screenWidth = 1280;
            _screenHeight = 720;
            var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, _screenWidth, _screenHeight);
            Camera.Initialise(viewportAdapter);

            //Initialisation du chrono
            _chrono = 0;
            
            _affiche = true;
            _affichePhraseInvinsible = false;


            base.Initialize();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //Load des map
            if (Game1._choixMap == 1)
            {
                _tiledMap = Content.Load<TiledMap>("map1");
            }
            else if (Game1._choixMap == 2)
            {
                _tiledMap = Content.Load<TiledMap>("map2");
            }
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            
            //Load des textures des coeurs
            _textureCoeurPlein = Content.Load<Texture2D>("coeurPlein");
            _textureCoeurVide = Content.Load<Texture2D>("coeurVide");
            
            
            //Load des écritures
            _textureCliqueGauche = Content.Load<Texture2D>("cliqueGauche");
            _textureFlecheDirectionnelle = Content.Load<Texture2D>("flecheDirectionnelle");
            _textureZQSD = Content.Load<Texture2D>("zqsd");
            _texturePhraseFonction = Content.Load<Texture2D>("phraseFonction");
            _texturePhraseInvincible = Content.Load<Texture2D>("phraseInvincible");


            //Chargement texture Perso
            _spritePerso = Content.Load<SpriteSheet>("elf_spritesheet.sf", new JsonContentLoader());
            Perso.LoadContent(_spritePerso);
            
            
            _spriteBullet = Content.Load<Texture2D>("Bullet");
            

            // CHARGEMENT DE LA FONT
            _font = Content.Load<SpriteFont>("nosifer");

            //Load des zombie et de leur spawn
            for (int i = 0; i < zombielvl1.Length; i++)
            {
                zombielvl1[i].LoadContent(Game);
                zombielvl1[i].SpawnDuZombie();
            }

            //Load des sons
            _ball = Content.Load<Song>("bruitBalle");
            _death = Content.Load<Song>("scream");
            MediaPlayer.Volume = 0.1f; // Volume du son
            

            base.LoadContent();
        }



        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaTime * Perso._vitesse_mvt;

            //Update du perso
            Perso.Update();
            Touche.Presse(Perso._positionPerso, _tiledMap, Perso._animationPerso, walkSpeed, deltaTime);
            
            Perso._spritePerso.Play(Perso._animationPerso);
            Perso._spritePerso.Update(deltaTime);



            // Creation des balles et mise à jour collisison
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
            

            // UPDATE DE LA POSITION CAMERA
            Camera.Update();


            //Ici on charge la map
            _tiledMapRenderer.Update(gameTime);


            //Update du zombie
            for (int i = 0; i < zombielvl1.Length; i++)
            {
                iazombie[i].Update(gameTime);

            }


            //Collision du perso avec le zombie et chrono de l'invincibilité         
            _chronoInvincible += 1 * deltaTime;

            for (int i = 0; i < zombielvl1.Length; i++)
            {
                if (Math.Sqrt(
                    Math.Pow(Perso._positionPerso.X - zombielvl1[i].PositionZombie.X, 2) + 
                    Math.Pow(Perso._positionPerso.Y - zombielvl1[i].PositionZombie.Y, 2)) < 10 && _EstInvincible == false)
                {
                    Perso._vie -= 1;
                    _EstInvincible = true;
                    _chronoInvincible = 0;
                    MediaPlayer.Play(_death);
                    if (Perso._vie == 0)
                    {
                        Game.LoadGameOver();
                    }
                }
            }
            if(_EstInvincible == true)
            {
                _affichePhraseInvinsible = true;
            }
            else
                _affichePhraseInvinsible = false;
            if(_chronoInvincible > 2)
            {
                _EstInvincible = false;
            }

            

            //Collision bullet avec le zombie et avec les mur
            foreach (Bullet bullet in bullets.ToArray()) 
            {
                if (bullet.BulletCollision())
                    bullets.Remove(bullet);
                for (int j = 0; j < zombielvl1.Length; j++)
                {
                    if(Math.Sqrt(
                    Math.Pow(bullet.Position.X - zombielvl1[j].PositionZombie.X, 2) +
                    Math.Pow(bullet.Position.Y - zombielvl1[j].PositionZombie.Y, 2)) < 12)
                    {
                        zombielvl1[j].SpawnDuZombie();
                        zombielvl1[j].VitesseZombie =+ 10;
                        
                        bullets.Remove(bullet);
                        _score++;
                        _text = "Score :" + _score.ToString();
                    }
                }
            }


            // CHRONO POUR L'AFFICHAGE DES COMMANDES EN DEBUT DE JEU
            _chrono = _chrono + 1 * deltaTime;
            if (_chrono > 5)
            {
                _affiche = false;
            }
            else
                _affiche = true;

            // Dans le jeu si on presse Échap on reviens au menu
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.LoadMenu();

        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //On met en place la caméra on Draw à travers elle
            transformMatrix = Camera._camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);

            //On dessine notre Perso
            Perso.Draw(_spriteBatch);

            //On dessine les zombie
            for (int i = 0; i < zombielvl1.Length; i++)
            {
                zombielvl1[i].Draw(_spriteBatch);
            }

            //On dessine les balles
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

            for (int i = 0; i < Perso._vie; i++)
            {
                _spriteBatch.Draw(_textureCoeurPlein, new Vector2(_postionCoeur.X + 40 + (80 * i), _postionCoeur.Y), Color.White);
            }
            for (int i = 0; i < 5 - Perso._vie; i++)
            {
                _spriteBatch.Draw(_textureCoeurVide, new Vector2(_postionCoeur.X + 40 + (80 * Perso._vie) + 80 * i, _postionCoeur.Y), Color.White);
            }

            //AFFICHAGE DES COMMANDES
            if (_affiche)
            {
                _spriteBatch.Draw(_textureZQSD, new Vector2(400, 550), Color.White);
                _spriteBatch.Draw(_textureFlecheDirectionnelle, new Vector2(230, 550), Color.White);
                _spriteBatch.Draw(_textureCliqueGauche, new Vector2(930, 550), Color.White);
                _spriteBatch.Draw(_texturePhraseFonction, new Vector2(190, 500), Color.White);
            }

            //Affichge phrase incinsible
            if (_affichePhraseInvinsible == true)
            {
                _spriteBatch.Draw(_texturePhraseInvincible, new Vector2(100, 100), Color.White);
            }

            // AFFICHAGE DU SCORE
            Vector2 textMiddlePoint = _font.MeasureString(_text) / 2;
            Vector2 position = new Vector2(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height - 40);
            _spriteBatch.DrawString(_font, _text, position, new Color(159,2,2), 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);
            
            _spriteBatch.End();
        }

        private void CreateBullet()
        {
            Vector2 _positionSouris = ScreenToWorldSpace(Mouse.GetState().Position.ToVector2(), transformMatrix);
            bullets.Add(new Bullet(Perso._positionPerso, Vector2.Normalize(_positionSouris - Perso._positionPerso), _spriteBullet));
        }
        
        public Vector2 ScreenToWorldSpace(in Vector2 point, Matrix transformMatrix)
        {
            Matrix invertedMatrix = Matrix.Invert(transformMatrix);
            return Vector2.Transform(point, invertedMatrix);
        }

    }
}
