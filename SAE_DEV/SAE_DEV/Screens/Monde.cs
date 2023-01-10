﻿using Microsoft.Xna.Framework;
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
        public const int nbZombie = 20;
        public static TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        public static SpriteSheet _spritePerso;

        List<Bullet> bullets = new List<Bullet>();
        public Texture2D _spriteBullet;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _screenWidth;
        private int _screenHeight;

        public Monde(Game1 game) : base(game)
        {

        }


        public override void Initialize()
        {

            Random random = new Random();

            zombielvl1 = new Zombie[nbZombie];
            iazombie = new IAZombie[nbZombie];
            
            

            for (int i = 0; i < zombielvl1.Length; i++)
            {
                zombielvl1[i] = new Zombie();

                if (Game1._choixMap == 1)
                {
                    do
                    {
                        zombielvl1[i].PositionZombie = new Vector2(random.Next(518, 1078), random.Next(354, 598));
                    } while (zombielvl1[i].PositionZombie.X > 1078 && zombielvl1[i].PositionZombie.X < 518 && zombielvl1[i].PositionZombie.Y > 598 && zombielvl1[i].PositionZombie.Y < 354);
                    
                }
                if (Game1._choixMap == 2)
                {
                    do
                    {
                        zombielvl1[i].PositionZombie = new Vector2(random.Next(244, 1036), random.Next(138, 556));
                    } while (zombielvl1[i].PositionZombie.X > 1036 && zombielvl1[i].PositionZombie.X < 244 && zombielvl1[i].PositionZombie.Y > 556 && zombielvl1[i].PositionZombie.Y < 138);
                }
                

                


                iazombie[i] = new IAZombie(random.Next(40, 60), zombielvl1[i]);

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
            if (Game1._choixMap == 1)
            {
                _tiledMap = Content.Load<TiledMap>("map1");
            }
            else if (Game1._choixMap == 2)
            {
                _tiledMap = Content.Load<TiledMap>("map2");
            }
            
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Chargement texture Perso
            _spritePerso = Content.Load<SpriteSheet>("elf_spritesheet.sf", new JsonContentLoader());
            _spriteBullet = Content.Load<Texture2D>("Bullet");
            Perso.LoadContent(_spritePerso);

            for (int i = 0; i < zombielvl1.Length; i++)
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

            //On vérifie si une touche est pressée DANS cette classe
            Touche.Presse(Perso._positionPerso, _tiledMap, Perso._animationPerso, walkSpeed, deltaTime);


            // Creation des balles et mise à jour
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                CreateBullet();
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

            for(int i = 0; i < zombielvl1.Length; i++)
            {
                if (Math.Sqrt(Math.Pow(Perso._positionPerso.X - zombielvl1[i].PositionZombie.X, 2) + Math.Pow(Perso._positionPerso.Y - zombielvl1[i].PositionZombie.Y, 2)) < 10)
                {
                    Game.LoadMenu();
                }
            }

        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //On met en place la caméra
            var transformMatrix = Camera._camera.GetViewMatrix();
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

            //On dessine la map avec la "vision" de la caméra
            _tiledMapRenderer.Draw(transformMatrix);

            _spriteBatch.End();

        }

        private void CreateBullet()
        {
            bullets.Add(new Bullet(Perso._positionPerso, Vector2.Normalize(Mouse.GetState().Position.ToVector2() - Perso._positionPerso), _spriteBullet));
        }

    }
}
