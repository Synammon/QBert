using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBert.GameStates
{
    public class TitleState : BaseGameState
    {
        private Texture2D _titleScreenTexture;
        private Color _tint = new Color(255, 255, 255, 0);
        private TimeSpan _fadeTimer = TimeSpan.FromSeconds(0);
        private int _tintAlpha = 0;
        private SpriteBatch _spriteBatch;

        public TitleState(Game game)
            : base(game)
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
            
            _titleScreenTexture = Game.Content.Load<Texture2D>("TitleScreen");
        }

        public override void Update(GameTime gameTime)
        {
            _fadeTimer += gameTime.ElapsedGameTime;
            double _tintPercent = 255 * (_fadeTimer.TotalSeconds / 8);
            _tintAlpha = (int)_tintPercent;
            _tint = new Color(_tintAlpha, _tintAlpha, _tintAlpha, _tintAlpha);
            if (_fadeTimer.TotalSeconds > 8)
            {
                // Transition to the next state (e.g., GamePlayState)
                IStateManager stateManager = (IStateManager)Game.Services.GetService(typeof(IStateManager));
                stateManager.PopState();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _spriteBatch.Draw(_titleScreenTexture, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), _tint);
        }
    }
}
