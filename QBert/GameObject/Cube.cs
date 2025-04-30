using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBert.GameObject
{
    public class Cube
    {
        private Texture2D _topTexture;
        private Texture2D _leftTexture;
        private Texture2D _rightTexture;

        private int _activeColorIndex = 0;

        private List<Color> _topColor = new List<Color>();
        private Color _leftColor;
        private Color _rightColor;

        private Rectangle _topRectangle;
        private Rectangle _leftRectangle;
        private Rectangle _rightRectangle;

        private int _upLeft;
        private int _upRight;
        private int _downLeft;
        private int _downRight;

        public bool Wrap { get; set; } = false;

        public int ActiveColorIndex
        {
            get { return _activeColorIndex; }
            set { _activeColorIndex = value; }
        }

        public Texture2D TopTexture
        {
            get { return _topTexture; }
            set { _topTexture = value; }
        }

        public Texture2D LeftTexture
        {
            get { return _leftTexture; }
            set { _leftTexture = value; }
        }

        public Texture2D RightTexture
        {
            get { return _rightTexture; }
            set { _rightTexture = value; }
        }
        public Color LeftColor
        {
            get { return _leftColor; }
            set { _leftColor = value; }
        }

        public Color RightColor
        {
            get { return _rightColor; }
            set { _rightColor = value; }
        }

        public List<Color> TopColor
        {
            get { return _topColor; }
        }

        public Rectangle TopRectangle
        {
            get { return _topRectangle; }
            set { _topRectangle = value; }
        }

        public Rectangle LeftRectangle
        {
            get { return _leftRectangle; }
            set { _leftRectangle = value; }
        }

        public Rectangle RightRectangle
        {               
            get { return _rightRectangle; }
            set { _rightRectangle = value; }
        }

        public int UpLeft
        {
            get { return _upLeft; }
            set { _upLeft = value; }
        }

        public int UpRight
        {
            get { return _upRight; }
            set { _upRight = value; }
        }

        public int DownLeft
        {
            get { return _downLeft; }
            set { _downLeft = value; }
        }

        public int DownRight
        {
            get { return _downRight; }
            set { _downRight = value; }
        }

        public Vector2 Origin 
        {
            get { return new Vector2(_topRectangle.X + _topRectangle.Width / 2, _topRectangle.Y + _topRectangle.Height / 2); }
        }

        public Cube(
            Texture2D topTexture, 
            Texture2D leftTexture, 
            Texture2D rightTexture, 
            List<Color> topColors, 
            Color leftColor, 
            Color rightColor, 
            Point position)
        {
            _topTexture = topTexture;
            _leftTexture = leftTexture;
            _rightTexture = rightTexture;

            _topRectangle = new Rectangle(position.X, position.Y, topTexture.Width, topTexture.Height);
            _leftRectangle = new Rectangle(position.X, position.Y + topTexture.Height / 2, leftTexture.Width, leftTexture.Height);
            _rightRectangle = new Rectangle(position.X + topTexture.Width / 2, position.Y + topTexture.Height / 2, rightTexture.Width, rightTexture.Height);
            _topColor = new List<Color>();

            foreach (Color c in topColors)
            {
                _topColor.Add(c);
            }

            _leftColor = leftColor;
            _rightColor = rightColor;
        }

        public void NextColor()
        {
            if (!Wrap)
            {
                if (_activeColorIndex < _topColor.Count - 1)
                {
                    _activeColorIndex++;
                    Player.Score += 15;
                }
            }
            else
            {
                Player.Score += 15;
                _activeColorIndex = _activeColorIndex + 1;
                if (_activeColorIndex == _topColor.Count)
                {
                    _activeColorIndex = 0;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            // Update logic for the cube can be added here
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw the top face of the cube
            spriteBatch.Draw(_topTexture, _topRectangle, _topColor[ActiveColorIndex]);
            // Draw the left face of the cube
            spriteBatch.Draw(_leftTexture, _leftRectangle, _leftColor);
            // Draw the right face of the cube
            spriteBatch.Draw(_rightTexture, _rightRectangle, _rightColor);
        }
    }
}
