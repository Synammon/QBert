using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBert.GameObject
{
    public class Disc : DrawableGameComponent
    {
        private static Texture2D _texture;
        private Vector2 _position;
        private Vector2 _velocity;
        private Vector2 _origin;
        private bool _moving = false;
        private float _rotation;
        private float _scale;
        private SpriteBatch _spriteBatch;
        private float _pixelsMoved = 0;
        private float _distance = 0;
        private float _speed;
        private int _index;
        private static Pyramid _pyramid;

        public static Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Vector2 Origin
        {
            get { return _position + _origin; }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, (int)(_texture.Width * _scale), (int)(_texture.Height * _scale));
            }
        }

        public Disc(Game game, SpriteBatch spriteBatch) : base(game)
        {
            _spriteBatch = spriteBatch;
            _velocity = new Vector2(0, 0);
            _rotation = 0;
            _speed = 256;
        }

        public override void Initialize()
        {
            _moving = false;
            _scale = 1.0f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            if (_texture == null)
            {
                _texture = Game.Content.Load<Texture2D>("Disc");
                _origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            }
        }

        public override void Update(GameTime gameTime)
        {
            QBert qbert = null;

            foreach (var component in Game.Components)
            {
                if (component is QBert qb)
                {
                    qbert = qb;
                }
                if (component is Pyramid pyramid)
                {
                    _pyramid = pyramid;
                }
            }

            if (_moving)
            {
                _position += _velocity * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _pixelsMoved += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                qbert.SetOrigin(Origin);
                if (_pixelsMoved >= _distance)
                {
                    _moving = false;
                    _pixelsMoved = 0;
                    Visible = false;
                    qbert.OnDisc = false;
                    qbert.FallFromDisc();
                }
            }
            else
            {
                if (!qbert.OnDisc)
                {
                    if (BoundingBox.Intersects(qbert.BoundingBox))
                    {
                        _moving = true;
                        qbert.OnDisc = true;
                        _velocity = Helpers.CalcDirection(Origin, new Vector2(Game1.ScreenWidth / 2, 100));
                        _distance = Vector2.Distance(Origin, new Vector2(Game1.ScreenWidth / 2, 100));
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible)
            {
                return;
            }

            _spriteBatch.Draw(_texture, _position, null, Color.White);

            base.Draw(gameTime);
        }
    }
}
