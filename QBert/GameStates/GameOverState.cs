using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBert.GameStates
{
    public class GameOverState : BaseGameState
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _gameOverTexture;
        private Color _tint = new Color(255, 255, 255, 255);
        private TimeSpan _fadeTimer = TimeSpan.FromSeconds(0);
        private int _tintAlpha = 0;
        private IStateManager _stateManager;

        public GameOverState(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            _spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _gameOverTexture = Game.Content.Load<Texture2D>("GameOver");
            _stateManager = (IStateManager)Game.Services.GetService(typeof(IStateManager));
        }

        public void ResetTimer()
        {
            _fadeTimer = TimeSpan.FromSeconds(0);
        }

        public override void Update(GameTime gameTime)
        {
            _fadeTimer += gameTime.ElapsedGameTime;

            double _tintPercent = 255 * (1/_fadeTimer.TotalSeconds / 8);

            _tintAlpha = (int)_tintPercent;
            _tint = new Color(_tintAlpha, _tintAlpha, _tintAlpha, _tintAlpha);

            if (_fadeTimer.TotalSeconds > 8)
            {
                _stateManager.PopState();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _spriteBatch.Draw(_gameOverTexture, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), _tint);
        }
    }
}
