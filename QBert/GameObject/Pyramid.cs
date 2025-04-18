using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace QBert.GameObject
{
    public class Pyramid : DrawableGameComponent
    {
        private List<Cube> _cubeList;
        private Cube _cube; 
        private Game _gameRef;
        private SpriteBatch _spriteBatch;
        
        public List<Cube> CubeList
        {
            get { return _cubeList; }
        }

        public Pyramid(Game game, SpriteBatch spriteBatch) : base(game)
        {
            _gameRef = game;
            _spriteBatch = spriteBatch;
            _cubeList = new List<Cube>();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        override protected void LoadContent()
        {
            Texture2D top = Game.Content.Load<Texture2D>("top_side");
            Texture2D left = Game.Content.Load<Texture2D>("left_side");
            Texture2D right = Game.Content.Load<Texture2D>("right_side");

            _cubeList = new List<Cube>();
            // Top Row
            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 - 64, 100));
            _cubeList.Add(_cube);
            _cube.DownLeft = 1;
            _cube.DownRight = 2;
            _cube.UpLeft = -1;
            _cube.UpRight = -1;
            // Second Row
            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 - 128, 100 + 96));
            _cubeList.Add(_cube);
            _cube.DownLeft = 3;
            _cube.DownRight = 4;
            _cube.UpLeft = -1;
            _cube.UpRight = 0;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450, 100 + 96));
            _cubeList.Add(_cube);
            _cube.DownLeft = 4;
            _cube.DownRight = 5;
            _cube.UpLeft = 0;
            _cube.UpRight = -1;

            // Third Row
            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 - 192, 100 + 96 + 96));
            _cubeList.Add(_cube); // 3
            _cube.DownLeft = 6;
            _cube.DownRight = 7;
            _cube.UpLeft = -1;
            _cube.UpRight = 1;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 - 64, 100 + 96 + 96));
            _cubeList.Add(_cube); // 4
            _cube.DownLeft = 7;
            _cube.DownRight = 8;
            _cube.UpLeft = 1;
            _cube.UpRight = 2;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 + 64, 100 + 96 + 96));
            _cubeList.Add(_cube); // 5
            _cube.DownLeft = 8;
            _cube.DownRight = 9;
            _cube.UpLeft = 2;
            _cube.UpRight = -1;

            // Forth Row
            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 - 256, 100 + 96 * 3));
            _cubeList.Add(_cube); // 6
            _cube.DownLeft = 10;
            _cube.DownRight = 11;
            _cube.UpLeft = -1;
            _cube.UpRight = 3;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 - 128, 100 + 96 * 3));
            _cubeList.Add(_cube); // 7
            _cube.DownLeft = 11;
            _cube.DownRight = 12;
            _cube.UpLeft = 3;
            _cube.UpRight = 4;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450, 100 + 96 * 3));
            _cubeList.Add(_cube); // 8
            _cube.DownLeft = 12;
            _cube.DownRight = 13;
            _cube.UpLeft = 4;
            _cube.UpRight = 5;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 + 128, 100 + 96 * 3));
            _cubeList.Add(_cube); // 9
            _cube.DownLeft = 13;
            _cube.DownRight = 14;
            _cube.UpLeft = 5;
            _cube.UpRight = -1;

            // Fifth Row
            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 - 320, 100 + 96 * 4));
            _cubeList.Add(_cube); // 10
            _cube.DownLeft = 15;
            _cube.DownRight = 16;
            _cube.UpLeft = -1;
            _cube.UpRight = 6;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 - 192, 100 + 96 * 4));
            _cubeList.Add(_cube); // 11
            _cube.DownLeft = 16;
            _cube.DownRight = 17;
            _cube.UpLeft = 6;
            _cube.UpRight = -7;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 - 64, 100 + 96 * 4));
            _cubeList.Add(_cube); // 12
            _cube.DownLeft = 17;
            _cube.DownRight = 18;
            _cube.UpLeft = 7;
            _cube.UpRight = 8;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 + 64, 100 + 96 * 4));
            _cubeList.Add(_cube); // 13
            _cube.DownLeft = 18;
            _cube.DownRight = 19;
            _cube.UpLeft = 8;
            _cube.UpRight = 9;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 + 192, 100 + 96 * 4));
            _cubeList.Add(_cube); // 14
            _cube.DownLeft = 19;
            _cube.DownRight = 20;
            _cube.UpLeft = 9;
            _cube.UpRight = -1;

            // Sixth Row
            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 - 384, 100 + 96 * 5));
            _cubeList.Add(_cube); // 15
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = -1;
            _cube.UpRight = 10;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 - 256, 100 + 96 * 5));
            _cubeList.Add(_cube); // 16
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = 10;
            _cube.UpRight = 11;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 - 128, 100 + 96 * 5));
            _cubeList.Add(_cube); // 17
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = 11;
            _cube.UpRight = 12;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450, 100 + 96 * 5));
            _cubeList.Add(_cube); // 18
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = 12;
            _cube.UpRight = 13;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 + 128, 100 + 96 * 5));
            _cubeList.Add(_cube); // 19
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = 13;
            _cube.UpRight = 14;

            _cube = new Cube(top, left, right, new List<Color> { Color.Yellow, Color.Pink }, Color.Green, Color.Blue, new Point(450 + 256, 100 + 96 * 5));
            _cubeList.Add(_cube); // 20
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = 14;
            _cube.UpRight = -1;


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (var cube in _cubeList)
            {
                cube.Draw(gameTime, _spriteBatch);
            }
        }

        public void AddCube(Cube cube)
        {
            _cubeList.Add(cube);
        }
    }
}
