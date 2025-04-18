using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QBert.GameObject
{
    public enum Direction
    {
        LeftDown,
        RightDown,
        LeftUp,
        RightUp
    }

    public class QBert : DrawableGameComponent
    {
        private Texture2D _leftDownTexture;
        private Texture2D _rightDownTexture;
        private Texture2D _leftUpTexture;
        private Texture2D _rightUpTexture;
        private SpriteBatch _spriteBatch;
        private Vector2 _position;
        private Vector2 _origin;
        private float _distance;
        private Game _gameRef;
        private Vector2 _velocity;
        private bool _moving = false;
        private float _pixelsMoved = 0f;
        private bool _collided = false;
        private Pyramid _pyramid;
        private Color[] _colorDetails;
        private int _currentCubeIndex;
        Cube _nextCube;
        int _nextIndex;

        private KeyboardState KeyboardState { get; set; }
        private KeyboardState PreviousKeyboardState { get; set; }

        public Direction Direction { get; set; }
        public Rectangle BoundingBox { get { return new Rectangle((int)_position.X, (int)_position.Y, 64, 64); } }
        public Vector2 Position
        {
            get { return new(_origin.X - 32, _origin.Y - 32); }
            set { _position = value; }
        }

        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public bool Moving
        {
            get { return _moving; }
            set { _moving = value; }
        }

        public bool Collided
        {
            get { return _collided; }
            set { _collided = value; }
        }

        public int CurrentCubeIndex
        {
            get { return _currentCubeIndex; }
            set { _currentCubeIndex = value; }
        }

        public QBert(Game game, SpriteBatch spriteBatch) : base(game)
        {
            _gameRef = game;
            _spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
            foreach (var component in Game.Components)
            {                
                if (component is Pyramid pyramid)
                {
                    _pyramid = pyramid;
                }
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _leftDownTexture = Game.Content.Load<Texture2D>("QBertleftdown");
            _rightDownTexture = Game.Content.Load<Texture2D>("QBertrightdown");
            _leftUpTexture = Game.Content.Load<Texture2D>("QBertupleft");
            _rightUpTexture = Game.Content.Load<Texture2D>("QBertupright");
        }

        public override void Update(GameTime gameTime)
        {
            PreviousKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();

            if (!_moving)
            {
                _velocity = Vector2.Zero;

                if (PreviousKeyboardState.IsKeyUp(Keys.C) && KeyboardState.IsKeyDown(Keys.C))
                {
                    Direction = Direction.RightDown;
                    _nextIndex = _pyramid.CubeList[_currentCubeIndex].DownRight;
                    if (_nextIndex != -1)
                    {
                        _distance = Vector2.Distance(_origin, _pyramid.CubeList[_nextIndex].Origin);
                        _nextCube = _pyramid.CubeList[_nextIndex];
                        _velocity = Helpers.CalcDirection(Origin, _nextCube.Origin);
                        _moving = true;
                        _pixelsMoved = 0;
                        _collided = false;
                    }
                    else
                    {
                        SetOrigin(_pyramid.CubeList[0].Origin);
                        Moving = false;
                        _pixelsMoved = 0;
                        _collided = false;
                    }
                }
                else if (PreviousKeyboardState.IsKeyUp(Keys.Z) && KeyboardState.IsKeyDown(Keys.Z))
                {
                    Direction = Direction.LeftDown;
                    _nextIndex = _pyramid.CubeList[_currentCubeIndex].DownLeft;
                    if (_nextIndex != -1)
                    {
                        _distance = Vector2.Distance(_origin, _pyramid.CubeList[_nextIndex].Origin);
                        _nextCube = _pyramid.CubeList[_nextIndex];
                        _velocity = Helpers.CalcDirection(Origin, _nextCube.Origin);
                        _moving = true;
                        _pixelsMoved = 0;
                        _collided = false;
                    }
                    else
                    {
                        SetOrigin(_pyramid.CubeList[0].Origin);
                        Moving = false;
                        _pixelsMoved = 0;
                        _collided = false;
                        _currentCubeIndex = 0;
                    }
                }
                else if (PreviousKeyboardState.IsKeyUp(Keys.Q) && KeyboardState.IsKeyDown(Keys.Q))
                {
                    Direction = Direction.LeftUp;
                    _nextIndex = _pyramid.CubeList[_currentCubeIndex].UpLeft;
                    if (_nextIndex != -1)
                    {
                        _distance = Vector2.Distance(_origin, _pyramid.CubeList[_nextIndex].Origin);
                        _nextCube = _pyramid.CubeList[_nextIndex];
                        _velocity = Helpers.CalcDirection(Origin, _nextCube.Origin);
                        _moving = true;
                        _pixelsMoved = 0;
                        _collided = false;
                        _currentCubeIndex = 0;
                    }
                    else
                    {
                        SetOrigin(_pyramid.CubeList[0].Origin);
                        Moving = false;
                        _pixelsMoved = 0;
                        _collided = false;
                        _currentCubeIndex = 0;
                    }
                }
                else if (PreviousKeyboardState.IsKeyUp(Keys.E) && KeyboardState.IsKeyDown(Keys.E))
                {
                    Direction = Direction.RightUp;
                    _nextIndex = _pyramid.CubeList[_currentCubeIndex].UpRight;
                    if (_nextIndex != -1)
                    {
                        _distance = Vector2.Distance(_origin, _pyramid.CubeList[_nextIndex].Origin);
                        _nextCube = _pyramid.CubeList[_nextIndex];
                        _velocity = Helpers.CalcDirection(Origin, _nextCube.Origin);
                        _moving = true;
                        _pixelsMoved = 0;
                        _collided = false;
                    }
                    else
                    {
                        SetOrigin(_pyramid.CubeList[0].Origin);
                        Moving = false;
                        _pixelsMoved = 0;
                        _collided = false;
                        _currentCubeIndex = 0;
                    }
                }
            }

            if (_moving)
            {
                SetOrigin(_origin + _velocity * 192 * (float)gameTime.ElapsedGameTime.TotalSeconds);
                _pixelsMoved += Math.Abs(_velocity.Y) * 192 * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_pixelsMoved > _distance && !_collided)
                {
                    _pixelsMoved = 0;
                    _moving = false;
                    _velocity = Vector2.Zero;
                    _currentCubeIndex = _nextIndex;
                    _collided = true;

                    Texture2D texture = _leftDownTexture;

                    switch (Direction)
                    {
                        case Direction.RightDown:
                            texture = _rightDownTexture;
                            break;
                        case Direction.LeftUp:
                            texture = _leftUpTexture;
                            break;
                        case Direction.RightUp:
                            texture = _rightUpTexture;
                            break;
                    }

                    if (_nextCube.ActiveColorIndex < _nextCube.TopColor.Count - 1)
                    {
                        _nextCube.ActiveColorIndex++;
                        _collided = true;
                    }
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            switch (Direction)
            {
                case Direction.LeftDown:
                    _spriteBatch.Draw(_leftDownTexture, BoundingBox, Color.White);
                    break;
                case Direction.RightDown:
                    _spriteBatch.Draw(_rightDownTexture, BoundingBox, Color.White);
                    break;
                case Direction.LeftUp:
                    _spriteBatch.Draw(_leftUpTexture, BoundingBox, Color.White);
                    break;
                case Direction.RightUp:
                    _spriteBatch.Draw(_rightUpTexture, BoundingBox, Color.White);
                    break;
            }
        }

        public void SetOrigin(Vector2 origin)
        {
            _origin = origin;
            _position = new Vector2(_origin.X - 32, _origin.Y - 32);
        }
    }
}
