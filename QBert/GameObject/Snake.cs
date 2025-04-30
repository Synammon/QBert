using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBert.GameObject
{
    public class Snake : Sprite
    {
        private QBert _qbert;
        private int _nextCubeIndex;
        private int _currentCubeIndex;
        private Cube _nextCube;

        public int CurrentCubeIndex 
        { 
            get { return _currentCubeIndex; } 
            set { _currentCubeIndex = value; } 
        }

        public int NextCubeIndex
        {
            get { return _nextCubeIndex; }
            set { _currentCubeIndex = value; }
        }

        public float Distance { get; set; } = 0;
        public Direction Direction { get; private set; }
        public override Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, (int)(_texture.Width), (int)(_texture.Height));
            }
        }
        public Snake(Game game, SpriteBatch spriteBatch, int currenteCubeIndex) : base(game, spriteBatch)
        {
            for (int i = 0; i < Game.Components.Count; i++)
            {
                var component = Game.Components[i];
                if (component is QBert qbert)
                {
                    _qbert = qbert;
                    break;
                }
            }
            _currentCubeIndex = currenteCubeIndex;
            _speed = 128;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _texture = Game.Content.Load<Texture2D>("Snake");
            _origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            if (!_moving && _pyramid.Target != -1)
            {
                if (_pyramid.Target == _currentCubeIndex)
                {
                    Visible = false;
                }
                else
                {
                    if (_pyramid.CubeList[_currentCubeIndex].TopRectangle.X <= _pyramid.CubeList[_pyramid.Target].TopRectangle.X &&
                        _pyramid.CubeList[_currentCubeIndex].TopRectangle.Y <= _pyramid.CubeList[_pyramid.Target].TopRectangle.Y)
                    {
                        Direction = Direction.RightDown;
                        _nextCubeIndex = _pyramid.CubeList[_currentCubeIndex].DownRight;
                        if (_nextCubeIndex != -1)
                        {
                            _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.CubeList[_nextCubeIndex].Origin);
                            _nextCube = _pyramid.CubeList[_nextCubeIndex];
                            _velocity = Helpers.CalcDirection(_nextCube.TopRectangle, Position);
                            _moving = true;
                            _pixelsMoved = 0;
                            _collided = false;
                        }
                    }
                    else if (_pyramid.CubeList[_currentCubeIndex].TopRectangle.X >= _pyramid.CubeList[_pyramid.Target].TopRectangle.X &&
                                _pyramid.CubeList[_currentCubeIndex].TopRectangle.Y <= _pyramid.CubeList[_pyramid.Target].TopRectangle.Y)
                    {
                        Direction = Direction.LeftDown;
                        _nextCubeIndex = _pyramid.CubeList[_currentCubeIndex].DownLeft;
                        if (_nextCubeIndex != -1)
                        {
                            _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.CubeList[_nextCubeIndex].Origin);
                            _nextCube = _pyramid.CubeList[_nextCubeIndex];
                            _velocity = Helpers.CalcDirection(_nextCube.TopRectangle, Position);
                            _moving = true;
                            _pixelsMoved = 0;
                            _collided = false;
                        }
                    }
                    else if (_pyramid.CubeList[_currentCubeIndex].TopRectangle.X >= _pyramid.CubeList[_pyramid.Target].TopRectangle.X &&
                                _pyramid.CubeList[_currentCubeIndex].TopRectangle.Y >= _pyramid.CubeList[_pyramid.Target].TopRectangle.Y)
                    {
                        Direction = Direction.RightUp;
                        _nextCubeIndex = _pyramid.CubeList[_currentCubeIndex].UpRight;
                        if (_nextCubeIndex != -1)
                        {
                            _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.CubeList[_nextCubeIndex].Origin);
                            _nextCube = _pyramid.CubeList[_nextCubeIndex];
                            _velocity = Helpers.CalcDirection(_nextCube.TopRectangle, Position);
                            _moving = true;
                            _pixelsMoved = 0;
                            _collided = false;
                        }
                    }
                    else
                    {
                        Direction = Direction.LeftUp;
                        _nextCubeIndex = _pyramid.CubeList[_currentCubeIndex].UpLeft;
                        if (_nextCubeIndex != -1)
                        {
                            _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.CubeList[_nextCubeIndex].Origin);
                            _nextCube = _pyramid.CubeList[_nextCubeIndex];
                            _velocity = Helpers.CalcDirection(_nextCube.TopRectangle, Position);
                            _moving = true;
                            _pixelsMoved = 0;
                            _collided = false;
                        }
                    }
                }
            }

            if (!_moving && _pyramid.Target == -1)
            {
                _velocity = Vector2.Zero;

                if (_qbert.Position.X >= Position.X && _qbert.Position.Y >= Position.Y)
                {
                    Direction = Direction.RightDown;
                    _nextCubeIndex = _pyramid.CubeList[_currentCubeIndex].DownRight;
                    if (_nextCubeIndex != -1)
                    {
                        _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.CubeList[_nextCubeIndex].Origin);
                        _nextCube = _pyramid.CubeList[_nextCubeIndex];
                        _velocity = Helpers.CalcDirection(_nextCube.TopRectangle, Position);
                        _moving = true;
                        _pixelsMoved = 0;
                        _collided = false;
                    }
                }
                else if (_qbert.Position.X <= Position.X && _qbert.Position.Y >= Position.Y)
                {
                    Direction = Direction.LeftDown;
                    _nextCubeIndex = _pyramid.CubeList[_currentCubeIndex].DownLeft;
                    if (_nextCubeIndex != -1)
                    {
                        _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.CubeList[_nextCubeIndex].Origin);
                        _nextCube = _pyramid.CubeList[_nextCubeIndex];
                        _velocity = Helpers.CalcDirection(_nextCube.TopRectangle, Position);
                        _moving = true;
                        _pixelsMoved = 0;
                        _collided = false;
                    }
                }
                else if (_qbert.Position.X >= Position.X && _qbert.Position.Y <= Position.Y)
                {
                    Direction = Direction.RightUp;
                    _nextCubeIndex = _pyramid.CubeList[_currentCubeIndex].UpRight;
                    if (_nextCubeIndex != -1)
                    {
                        _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.CubeList[_nextCubeIndex].Origin);
                        _nextCube = _pyramid.CubeList[_nextCubeIndex];
                        _velocity = Helpers.CalcDirection(_nextCube.TopRectangle, Position);
                        _moving = true;
                        _pixelsMoved = 0;
                        _collided = false;
                    }
                }
                else if (_qbert.Position.X <= Position.X && _qbert.Position.Y <= Position.Y)
                {
                    Direction = Direction.LeftUp;
                    _nextCubeIndex = _pyramid.CubeList[_currentCubeIndex].UpLeft;
                    if (_nextCubeIndex != -1)
                    {
                        _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.CubeList[_nextCubeIndex].Origin);
                        _nextCube = _pyramid.CubeList[_nextCubeIndex];
                        _velocity = Helpers.CalcDirection(_nextCube.TopRectangle, Position);
                        _moving = true;
                        _pixelsMoved = 0;
                        _collided = false;
                    }
                }
            }
            if (_moving)
            {
                _position = _position + _velocity * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _pixelsMoved += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_pixelsMoved > _distance && !_collided)
                {
                    _moving = false;
                    _pixelsMoved = 0;
                    _velocity = Vector2.Zero;
                    _currentCubeIndex = _nextCubeIndex;
                    _collided = true;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
