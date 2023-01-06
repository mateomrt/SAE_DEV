﻿using Microsoft.Xna.Framework;
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

using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System.Xml.Linq;
using SAE_DEV.Screens;

namespace SAE_DEV
{
    public class Game1 : Game
    {
        public int MAP1_TAILLE = 800;
        public int MAP2_TAILLE = 560;
        private KeyboardState _keyboardState;
        private GraphicsDeviceManager _graphics;
        private int _screenWidth;
        private int _screenHeight;
        public SpriteBatch SpriteBatch { get; private set; }

        private ScreenManager _screenManager;



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

            
            _screenManager = new ScreenManager();
            

            _screenWidth = 1280;
            _screenHeight = 720;

            base.Initialize();
        }

        public void LoadScreen(GameScreen screen)
        {
            _screenManager.LoadScreen(screen, new FadeTransition(GraphicsDevice, Color.Black, .5f));

        }
        

        public void LoadMenu()
        {
            LoadScreen(new Menu(this));
        }
        public void LoadMonde()
        {
            LoadScreen(new Monde(this));
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
            LoadMenu();
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds; // TIME
            _keyboardState = Keyboard.GetState();



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            
            _screenManager.Update(gameTime);

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _screenManager.Draw(gameTime);

            base.Draw(gameTime);
        }


    }
}