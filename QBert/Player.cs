using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBert
{
    public class Player : DrawableGameComponent
    {
        private static int _lives = 3;
        private static int _score = 0;
        private static Texture2D _texture;
        private static SpriteFont _font;
        private static SpriteBatch _spriteBatch;

        public static int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        public static int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public Player(Game game, SpriteBatch spriteBatch) : base(game)
        {
            _spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
            // Initialize player state here if needed
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load player textures or other resources here
            base.LoadContent();

            _font = Game.Content.Load<SpriteFont>("GameFont");
            _texture = Game.Content.Load<Texture2D>("QBertRightDown");
        }

        public override void Update(GameTime gameTime)
        {
            // Update player state here
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw player state here
            base.Draw(gameTime);

            _spriteBatch.DrawString(_font, $"Score: {_score}", new Vector2(10, 10), Color.White);

            int row = 0;

            for (int i = 1; i <= _lives; i++)
            {
                _spriteBatch.Draw(_texture, new Rectangle(10 + row * 35, 300 + (i % 4 * 35), 32, 32), Color.White);

                if (i % 4 == 0 && i != 0)
                {
                    row++;
                }
            }
        }
    }
}
