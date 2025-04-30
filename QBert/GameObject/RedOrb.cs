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
    public class RedOrb : Sprite
    {
        private int _nextCubeIndex = 0;
        private int _currentCubeIndex = 0;
        
        public float Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public int NextCubeIndex
        {
            get { return _nextCubeIndex; }
            set { _nextCubeIndex = value; }
        }

        public int CurrentCubeIndex
        {
            get { return _currentCubeIndex; }
            set { _currentCubeIndex = value; }
        }

        public RedOrb(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            _velocity = new Vector2(0, 1);
            _rotation = 0;
            _speed = 256;
        }

        public override void Initialize()
        {
            _moving = true;
            
            base.Initialize();
            // Additional initialization logic for RedOrb
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _texture = Game.Content.Load<Texture2D>("RedSphere");
            _origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            // Update logic for RedOrb
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
            // Draw logic for RedOrb
            base.Draw(gameTime);
        }
    }
}
