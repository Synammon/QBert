using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBert.GameObject
{
    public class Faerie : Sprite
    {
        private int _currentCubeIndex;
        private int _nextCubeIndex;
        
        public int CurrentCubeIndex
        {
            get { return _currentCubeIndex; }
            set { _currentCubeIndex = value; }
        }

        public int NextCubeIndex
        {
            get { return _nextCubeIndex; }
            set { _nextCubeIndex = value; }
        }

        public float Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public Faerie(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            _speed = 256;
        }

        public override void Initialize()
        {
            _velocity = new Vector2(0, 1);
            _moving = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _texture = Game.Content.Load<Texture2D>("Sprite");
            _origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            // Update logic for Faerie
            base.Update(gameTime);

            if (!_moving)
            {
                if (_currentCubeIndex > 20)
                {
                    if (Random.Next(100) > 50)
                    {
                        _direction = Direction.RightDown;
                        _velocity = Helpers.CalcDirection(
                            _pyramid.CubeList[0].Origin,
                            _pyramid.CubeList[2].Origin);
                    }
                    else
                    {
                        _direction = Direction.LeftDown;
                        _velocity = Helpers.CalcDirection(
                            _pyramid.CubeList[0].Origin,
                            _pyramid.CubeList[1].Origin);
                    }

                    _distance = 115;
                    _moving = true;

                    return;
                }

                int newCubeIndex = -1;

                while (newCubeIndex < 0)
                {
                    if (Random.Next(0, 100) < 50)
                    {
                        newCubeIndex = _pyramid.CubeList[_currentCubeIndex].DownLeft;
                        _direction = Direction.LeftDown;
                    }
                    else
                    {
                        newCubeIndex = _pyramid.CubeList[_currentCubeIndex].DownRight;
                        _direction = Direction.RightDown;
                    }
                }

                _nextCubeIndex = newCubeIndex;
                _pyramid.CubeList[_currentCubeIndex].ActiveColorIndex = 0;
                _velocity = Helpers.CalcDirection(
                    _pyramid.CubeList[_currentCubeIndex].Origin,
                    _pyramid.CubeList[_nextCubeIndex].Origin);
                _distance = Vector2.Distance(
                    _pyramid.CubeList[_currentCubeIndex].Origin,
                    _pyramid.CubeList[_nextCubeIndex].Origin);
                _currentCubeIndex = _nextCubeIndex;
                _moving = true;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(_texture, _position, null, Color.White, _rotation, _origin, _scale, SpriteEffects.None, 0);
        }
    }
}
