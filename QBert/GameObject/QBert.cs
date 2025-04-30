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
        private Texture2D _texture;
        private SpriteBatch _spriteBatch;
        private Vector2 _origin;
        private float _distance;
        private Game _gameRef;
        private Vector2 _velocity;
        private bool _onDisc = false;
        private bool _moving = false;
        private float _pixelsMoved = 0f;
        private bool _collided = false;
        private Pyramid _pyramid;
        private Player _player;
        private int _currentCubeIndex;
        Cube _nextCube;
        int _nextIndex;
        private bool _pause;
        private TimeSpan _pauseTimer;

        private KeyboardState KeyboardState { get; set; }
        private KeyboardState PreviousKeyboardState { get; set; }

        public Direction Direction { get; set; }
        public Rectangle BoundingBox 
        { 
            get 
            { 
                return new Rectangle((int)Position.X, (int)Position.Y, 64, 64); 
            } 
        }

        public Vector2 Position
        {
            get { return new(_origin.X - 32, _origin.Y - 32); }
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

        public bool OnDisc
        {
            get { return _onDisc; }
            set { _onDisc = value; }
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
            Direction = Direction.RightDown;
        }

        public override void Initialize()
        {
            foreach (var component in Game.Components)
            {                
                if (component is Pyramid pyramid)
                {
                    _pyramid = pyramid;
                }

                if (component is Player player)
                {
                    _player = player;
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
            _texture = _rightDownTexture;
        }

        public override void Update(GameTime gameTime)
        {
            PreviousKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();
            _pauseTimer -= gameTime.ElapsedGameTime;    

            if (!_moving && !_onDisc)
            {
                _velocity = Vector2.Zero;

                if (PreviousKeyboardState.IsKeyUp(Keys.C) && KeyboardState.IsKeyDown(Keys.C))
                {
                    _texture = _rightDownTexture;
                    Direction = Direction.RightDown;
                    _nextIndex = _pyramid.CubeList[_currentCubeIndex].DownRight;
                    if (_nextIndex != -1)
                    {
                        _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.CubeList[_nextIndex].Origin);
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
                        Direction = Direction.LeftDown;
                        Player.Lives--;
                    }
                }
                else if (PreviousKeyboardState.IsKeyUp(Keys.Z) && KeyboardState.IsKeyDown(Keys.Z))
                {
                    _texture = _leftDownTexture;
                    Direction = Direction.LeftDown;
                    _nextIndex = _pyramid.CubeList[_currentCubeIndex].DownLeft;
                    if (_nextIndex != -1)
                    {
                        _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.CubeList[_nextIndex].Origin);
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
                        Player.Lives--;
                    }
                }
                else if (PreviousKeyboardState.IsKeyUp(Keys.Q) && KeyboardState.IsKeyDown(Keys.Q))
                {
                    _texture = _leftUpTexture;
                    Direction = Direction.LeftUp;
                    _nextIndex = _pyramid.CubeList[_currentCubeIndex].UpLeft;
                    if (_nextIndex != -1)
                    {
                        _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.CubeList[_nextIndex].Origin);
                        _nextCube = _pyramid.CubeList[_nextIndex];
                        _velocity = Helpers.CalcDirection(Origin, _nextCube.Origin);
                        _moving = true;
                        _pixelsMoved = 0;
                        _collided = false;
                        _currentCubeIndex = 0;
                    }
                    else if (_pyramid.DiscList.ContainsKey(_currentCubeIndex))
                    {
                        _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.DiscList[_currentCubeIndex].Origin);
                        _velocity = Helpers.CalcDirection(Origin, _pyramid.DiscList[_currentCubeIndex].Origin);
                        _moving = true;
                        _pixelsMoved = 0;
                        _collided = false; 
                        _pyramid.SetTarget(_currentCubeIndex);
                    }
                    else
                    {
                        SetOrigin(_pyramid.CubeList[0].Origin);
                        Moving = false;
                        _pixelsMoved = 0;
                        _collided = false;
                        _currentCubeIndex = 0;
                        Player.Lives--;
                    }
                }
                else if (PreviousKeyboardState.IsKeyUp(Keys.E) && KeyboardState.IsKeyDown(Keys.E))
                {
                    _texture = _rightUpTexture;
                    Direction = Direction.RightUp;
                    _nextIndex = _pyramid.CubeList[_currentCubeIndex].UpRight;
                    if (_nextIndex != -1)
                    {
                        _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.CubeList[_nextIndex].Origin);
                        _nextCube = _pyramid.CubeList[_nextIndex];
                        _velocity = Helpers.CalcDirection(Origin, _nextCube.Origin);
                        _moving = true;
                        _pixelsMoved = 0;
                        _collided = false;
                    }
                    else if (_pyramid.DiscList.ContainsKey(_currentCubeIndex))
                    {
                        _distance = Vector2.Distance(_pyramid.CubeList[_currentCubeIndex].Origin, _pyramid.DiscList[_currentCubeIndex].Origin);
                        _velocity = Helpers.CalcDirection(Origin, _pyramid.DiscList[_currentCubeIndex].Origin);
                        _moving = true;
                        _pixelsMoved = 0;
                        _collided = false;
                        _pyramid.SetTarget(_currentCubeIndex);
                    }
                    else
                    {
                        SetOrigin(_pyramid.CubeList[0].Origin);
                        Moving = false;
                        _pixelsMoved = 0;
                        _collided = false;
                        _currentCubeIndex = 0;
                        Player.Lives--;
                    }
                }
            }

            if (_moving)
            {
                SetOrigin(_origin + _velocity * 256 * (float)gameTime.ElapsedGameTime.TotalSeconds);
                _pixelsMoved += 256 * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_pixelsMoved > _distance && !_collided)
                {
                    _pixelsMoved = 0;
                    _moving = false;
                    _velocity = Vector2.Zero;
                    _currentCubeIndex = _nextIndex;
                    _collided = true;
                    _nextCube.NextColor();
                }
            }

            foreach (var sprite in _pyramid.Sprites)
            {
                if (sprite.BoundingBox.Intersects(BoundingBox) && sprite is not GreenOrb && sprite is not Snake && sprite is not Faerie)
                {
                    Reset(sprite);
                    Player.Lives--;
                    break;
                }
                else if (sprite.BoundingBox.Intersects(BoundingBox) && sprite is GreenOrb)
                {
                    sprite.Enabled = false;
                    sprite.Visible = false;
                    _pyramid.Pause = true;
                    _pyramid.PauseTimer = TimeSpan.FromSeconds(5f);
                    Player.Score += 100;
                    break;
                }
                else if (sprite.BoundingBox.Intersects(BoundingBox) && sprite is Faerie)
                {
                    sprite.Visible = false;
                    Player.Score += 100;
                    break;
                }
                else if (sprite.BoundingBox.Intersects(BoundingBox) && sprite is Snake)
                {
                    if ((sprite as Snake).CurrentCubeIndex == _currentCubeIndex)
                    {
                        Player.Lives--;
                        Reset(sprite);
                        break;
                    }
                }
            }

            if (_pause)
            {
                foreach (var sprite in _pyramid.Sprites)
                {
                    sprite.Enabled = false;
                }

                if (_pauseTimer <= TimeSpan.Zero)
                {
                    _pause = false;
                    _pauseTimer = TimeSpan.FromSeconds(5f);
                    _pixelsMoved = 0;
                    _currentCubeIndex = 0;
                    foreach (var sprite in _pyramid.Sprites)
                    {
                        sprite.Enabled = true;
                    }
                }

                base.Update(gameTime);
            }
        }

        public void FallFromDisc()
        {
            _moving = false;
            _onDisc = false;
            _collided = false;
            _pixelsMoved = 0;
            SetOrigin(_pyramid.CubeList[0].Origin);
            _currentCubeIndex = 0;
            _pyramid.CubeList[0].NextColor();
        }

        public void Reset(Sprite sprite)
        {
            _collided = false;
            _moving = false;
            _onDisc = false;
            SetOrigin(_pyramid.CubeList[0].Origin);
            _pixelsMoved = 0;
            _currentCubeIndex = 0;
            if (sprite != null)
            {
                sprite.Enabled = false;
                sprite.Visible = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 drawPosition = new(Position.X, Position.Y - 16);
            switch (Direction)
            {
                case Direction.LeftDown:
                    _spriteBatch.Draw(_leftDownTexture, drawPosition, Color.White);
                    break;
                case Direction.RightDown:
                    _spriteBatch.Draw(_rightDownTexture, drawPosition, Color.White);
                    break;
                case Direction.LeftUp:
                    _spriteBatch.Draw(_leftUpTexture, drawPosition, Color.White);
                    break;
                case Direction.RightUp:
                    _spriteBatch.Draw(_rightUpTexture, drawPosition, Color.White);
                    break;
            }
        }

        public void SetOrigin(Vector2 origin)
        {
            _origin = origin;
        }
    }
}
