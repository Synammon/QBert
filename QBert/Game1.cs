using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using QBert.GameObject;
using QBert.GameStates;
using System.Collections.Generic;
using System.Numerics;

namespace QBert
{
    public class Game1 : Game
    {
        public const int ScreenWidth = 1200;
        public const int ScreenHeight = 1000;
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameStateManager _stateManager;
        private TitleState _titleIntroState;
        private GamePlayState _gamePlayState;
        private GameOverState _gameOverState;
        private HighScoreState _highScoreState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), _spriteBatch);

            _stateManager = new GameStateManager(this);
            Services.AddService(typeof(IStateManager), _stateManager);

            _titleIntroState = new TitleState(this);
            _titleIntroState.Initialize();

            _gamePlayState = new GamePlayState(this);
            _gamePlayState.Initialize();

            _gameOverState = new GameOverState(this);
            _gameOverState.Initialize();

            _highScoreState = new HighScoreState(this);
            _highScoreState.Initialize();

            _stateManager.ChangeState(_gamePlayState);

            _gamePlayState.Visible = false;
            _gamePlayState.Enabled = false;

            _stateManager.PushState(_titleIntroState);

            Levels levels = new();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Player.Lives <= 0)
            {
                _gameOverState.ResetTimer();
                _highScoreState.SetScore(Player.Score);

                Player.Lives = 3;
                Player.Score = 0;

                _gamePlayState.Enabled = false;
                _stateManager.PushState(_highScoreState);
                _stateManager.PushState(_gameOverState);
                
                foreach (var c in Components)
                {
                    if (c is Pyramid p)
                    {
                        p.Enabled = false;
                    }

                    if (c is GameObject.QBert q)
                    {
                        q.Enabled = false;
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            base.Draw(gameTime);
            
            _spriteBatch.End();
        }
    }
}
