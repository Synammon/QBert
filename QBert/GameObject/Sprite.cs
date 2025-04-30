using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBert.GameObject
{
    public abstract class Sprite : DrawableGameComponent
    {
        protected Texture2D _texture;
        protected Vector2 _position;
        protected Vector2 _velocity;
        protected Vector2 _origin;
        protected bool _moving = false;
        protected bool _collided = false;
        protected Direction _direction;
        protected float _rotation;
        protected float _scale;
        protected SpriteBatch _spriteBatch;
        protected float _pixelsMoved = 0;
        protected float _distance = 0;
        protected float _speed;
        protected static Pyramid _pyramid;

        public Texture2D Texture
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
            get { return _origin; }
            set { _origin = value; }
        }
            
        public virtual Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, (int)(_texture.Width * _scale), (int)(_texture.Height * _scale));
            }
        }

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public static Random Random
        {
            get; private set;
        }

        public Sprite(Game game, SpriteBatch spriteBatch) : base(game)
        {
            _spriteBatch = spriteBatch;
            _position = Vector2.Zero;
            _velocity = Vector2.Zero;
            _origin = Vector2.Zero;
            _rotation = 0f;
            _scale = 1f;

            if (Random == null)
            {
                Random = new Random();
            }

            if (_pyramid == null)
            {
                foreach (var component in Game.Components)
                {
                    if (component is Pyramid pyramid)
                    {
                        _pyramid = pyramid;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled == false)
            {
                return;
            }
            if (_moving)
            {
                _position += _velocity * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _pixelsMoved += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
               
                if (_pixelsMoved >= _distance)
                {
                    _moving = false;
                    _pixelsMoved = 0f;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_texture != null)
            {
                _spriteBatch.Draw(_texture, _position, null, Color.White);
            }

            base.Draw(gameTime);
        }
    }
}
