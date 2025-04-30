using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QBert.GameObject
{
    public class Pyramid : DrawableGameComponent
    {
        private List<Cube> _cubeList;
        private Dictionary<int, Disc> _discList = new Dictionary<int, Disc>();
        private Levels _levels;
        private int _levelIndex = 0;
        private Cube _cube; 
        private Game _gameRef;
        private SpriteBatch _spriteBatch;
        private List<Sprite> _spriteList = new List<Sprite>();
        private TimeSpan _spawnTimer;
        private Random _random = new Random();

        public int Level { get; set; } = 1;
        public int Round { get; set; } = 1;
       
        public Levels Levels
        {
            get { return _levels; }
            set { _levels = value; }
        }

        public List<Cube> CubeList
        {
            get { return _cubeList; }
        }

        public List<Sprite> Sprites
        {
            get { return _spriteList; }
        }

        public Dictionary<int, Disc> DiscList
        {
            get { return _discList; }
        }

        public bool Pause
        {
            get; set;
        }

        public TimeSpan PauseTimer
        {
            get; set;
        }

        public int Target
        {
            get; private set;
        }

        public TimeSpan TargetTimer
        {
            get; private set;
        }
        public void SetTarget(int target)
        {
            Target = target;
            TargetTimer = TimeSpan.FromSeconds(3f);
        }

        public Pyramid(Game game, SpriteBatch spriteBatch) : base(game)
        {
            _gameRef = game;
            _spriteBatch = spriteBatch;
            _cubeList = new List<Cube>();

            _spawnTimer = TimeSpan.FromSeconds(2f);
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

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                IncludeFields = true
            };

            string json = "";
            string fileName = "";

            fileName = "Content\\Levels.json";
            json = File.ReadAllText(fileName);
            _levels = JsonSerializer.Deserialize<Levels>(json, options);

            // Top Row
            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 64, 200));

            _cubeList.Add(_cube);
            _cube.DownLeft = 1;
            _cube.DownRight = 2;
            _cube.UpLeft = -1;
            _cube.UpRight = -1;

            // Second Row
            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 128, 200 + 96));

            _cubeList.Add(_cube);
            _cube.DownLeft = 3;
            _cube.DownRight = 4;
            _cube.UpLeft = -1;
            _cube.UpRight = 0;

            _cube = new Cube(top,
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600, 200 + 96));

            _cubeList.Add(_cube);
            _cube.DownLeft = 4;
            _cube.DownRight = 5;
            _cube.UpLeft = 0;
            _cube.UpRight = -1;

            // Third Row
            _cube = new Cube(top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 192, 200 + 96 + 96));

            _cubeList.Add(_cube); // 3
            _cube.DownLeft = 6;
            _cube.DownRight = 7;
            _cube.UpLeft = -1;
            _cube.UpRight = 1;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 64, 200 + 96 + 96));

            _cubeList.Add(_cube); // 4
            _cube.DownLeft = 7;
            _cube.DownRight = 8;
            _cube.UpLeft = 1;
            _cube.UpRight = 2;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 + 64, 200 + 96 + 96));

            _cubeList.Add(_cube); // 5
            _cube.DownLeft = 8;
            _cube.DownRight = 9;
            _cube.UpLeft = 2;
            _cube.UpRight = -1;

            // Forth Row
            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 256, 200 + 96 * 3));

            _cubeList.Add(_cube); // 6
            _cube.DownLeft = 10;
            _cube.DownRight = 11;
            _cube.UpLeft = -1;
            _cube.UpRight = 3;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 128, 200 + 96 * 3));

            _cubeList.Add(_cube); // 7
            _cube.DownLeft = 11;
            _cube.DownRight = 12;
            _cube.UpLeft = 3;
            _cube.UpRight = 4;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor,
                new Point(600, 200 + 96 * 3));

            _cubeList.Add(_cube); // 8
            _cube.DownLeft = 12;
            _cube.DownRight = 13;
            _cube.UpLeft = 4;
            _cube.UpRight = 5;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor,
                new Point(600 + 128, 200 + 96 * 3));

            _cubeList.Add(_cube); // 9
            _cube.DownLeft = 13;
            _cube.DownRight = 14;
            _cube.UpLeft = 5;
            _cube.UpRight = -1;

            // Fifth Row
            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 320, 200 + 96 * 4));

            _cubeList.Add(_cube); // 10
            _cube.DownLeft = 15;
            _cube.DownRight = 16;
            _cube.UpLeft = -1;
            _cube.UpRight = 6;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 192, 200 + 96 * 4));

            _cubeList.Add(_cube); // 11
            _cube.DownLeft = 16;
            _cube.DownRight = 17;
            _cube.UpLeft = 6;
            _cube.UpRight = 7;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 64, 200 + 96 * 4));

            _cubeList.Add(_cube); // 12
            _cube.DownLeft = 17;
            _cube.DownRight = 18;
            _cube.UpLeft = 7;
            _cube.UpRight = 8;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 + 64, 200 + 96 * 4));

            _cubeList.Add(_cube); // 13
            _cube.DownLeft = 18;
            _cube.DownRight = 19;
            _cube.UpLeft = 8;
            _cube.UpRight = 9;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 + 192, 200 + 96 * 4));

            _cubeList.Add(_cube); // 14
            _cube.DownLeft = 19;
            _cube.DownRight = 20;
            _cube.UpLeft = 9;
            _cube.UpRight = -1;

            // Sixth Row
            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 384, 200 + 96 * 5));
            _cubeList.Add(_cube); // 15
            _cube.DownLeft = 21;
            _cube.DownRight = 22;
            _cube.UpLeft = -1;
            _cube.UpRight = 10;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor,
                new Point(600 - 256, 200 + 96 * 5));

            _cubeList.Add(_cube); // 16
            _cube.DownLeft = 22;
            _cube.DownRight = 23;
            _cube.UpLeft = 10;
            _cube.UpRight = 11;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor,
                new Point(600 - 128, 200 + 96 * 5));

            _cubeList.Add(_cube); // 17
            _cube.DownLeft = 23;
            _cube.DownRight = 24;
            _cube.UpLeft = 11;
            _cube.UpRight = 12;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600, 200 + 96 * 5));

            _cubeList.Add(_cube); // 18
            _cube.DownLeft = 24;
            _cube.DownRight = 25;
            _cube.UpLeft = 12;
            _cube.UpRight = 13;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 + 128, 200 + 96 * 5));

            _cubeList.Add(_cube); // 19
            _cube.DownLeft = 25;
            _cube.DownRight = 26;
            _cube.UpLeft = 13;
            _cube.UpRight = 14;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor,
                new Point(600 + 256, 200 + 96 * 5));

            _cubeList.Add(_cube); // 20
            _cube.DownLeft = 26;
            _cube.DownRight = 27;
            _cube.UpLeft = 14;
            _cube.UpRight = -1;

            // Row 7
            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 448, 200 + 96 * 6));

            _cubeList.Add(_cube); // 21
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = -1;
            _cube.UpRight = 15;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 320, 200 + 96 * 6));

            _cubeList.Add(_cube); // 22
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = 15;
            _cube.UpRight = 16;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 192, 200 + 96 * 6));

            _cubeList.Add(_cube); // 23
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = 16;
            _cube.UpRight = 17;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 - 64, 200 + 96 * 6));

            _cubeList.Add(_cube); // 24
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = 17;
            _cube.UpRight = 18;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 + 64, 200 + 96 * 6));

            _cubeList.Add(_cube); // 25
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = 18;
            _cube.UpRight = 19;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 + 192, 200 + 96 * 6));

            _cubeList.Add(_cube); // 26
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = 19;
            _cube.UpRight = 20;

            _cube = new Cube(
                top, 
                left, 
                right, 
                _levels.LevelDefs[_levelIndex].TopColors, 
                _levels.LevelDefs[_levelIndex].LeftColor, 
                _levels.LevelDefs[_levelIndex].RightColor, 
                new Point(600 + 320, 200 + 96 * 6));

            _cubeList.Add(_cube); // 27
            _cube.DownLeft = -1;
            _cube.DownRight = -1;
            _cube.UpLeft = 20;
            _cube.UpRight = -1;

            foreach (var disc in _levels.LevelDefs[_levelIndex].Discs)
            {
                if (disc < 0 || disc > 27)
                {
                    continue;
                }

                Disc d = new Disc(_gameRef, _spriteBatch);

                d.Initialize();

                if (_cubeList[disc].UpLeft == -1)
                {
                    d.Position = new Vector2(
                        _cubeList[disc].TopRectangle.X - 128,
                        _cubeList[disc].TopRectangle.Y - 64);
                }
                else if (_cubeList[disc].UpRight == -1)
                {
                    d.Position = new Vector2(
                        _cubeList[disc].TopRectangle.X + 128,
                        _cubeList[disc].TopRectangle.Y - 64);
                }

                _discList.Add(disc, d);
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _spawnTimer -= gameTime.ElapsedGameTime;
            PauseTimer -= gameTime.ElapsedGameTime;
            TargetTimer -= gameTime.ElapsedGameTime;

            if (TargetTimer.TotalSeconds < 0 && Target != -1)
            {
                Target = -1;
            }

            if (Pause)
            {
                if (PauseTimer.TotalSeconds < 0)
                {
                    Pause = false;
                    PauseTimer = TimeSpan.FromSeconds(2f);
                }
            }

            if (_spawnTimer.TotalSeconds < 0 && !Pause)
            {
                if (_random.Next(100) < 25)
                {
                    GenerateRedOrb();
                }
                else if (_random.Next(100) < 25)
                {
                    GeneratePurpleOrb();
                }
                else if (_random.Next(100) < 25)
                {
                    GenerateFaerie();
                }
                else if (_random.Next(100) < 25)
                {
                    GenerateGreenOrb();
                }

                _spawnTimer = TimeSpan.FromSeconds(2f);
            }

            CheckIfLevelFinished(gameTime);

            for (int i = 0; i < _spriteList.Count; i++)
            {
                if (Pause)
                {
                    _spriteList[i].Enabled = false;
                }
                else
                {
                    _spriteList[i].Enabled = true;
                    _spriteList[i].Update(gameTime);
                }
            }

            foreach (var disc in _discList.Keys)
            {
                _discList[disc].Update(gameTime);
            }

            _spriteList.RemoveAll(s => s.Position.Y > 950 || s.Visible == false);
            

            base.Update(gameTime);
        }

        private void CheckIfLevelFinished(GameTime gameTime)
        {
            bool levelDone = true;

            foreach (var cube in _cubeList)
            {
                if (cube.ActiveColorIndex < cube.TopColor.Count - 1)
                {
                    levelDone = false;
                }
                cube.Update(gameTime);
            }

            if (levelDone)
            {
                if (_levelIndex < _levels.LevelDefs.Count - 1)
                {
                    Player.Score += Math.Min(1000 + _levelIndex * 250, 5000); ;
                    _levelIndex++;
                    ResetLevel();
                }
                else
                {
                    // Game Over
                }
            }
        }

        private void GenerateFaerie()
        {
            foreach (var obj in _spriteList)
            {
                if (obj is Faerie)
                {
                    return;
                }
            }

            var sprite = new Faerie(_gameRef, _spriteBatch);
            sprite.Initialize();


            if (_random.Next(100) < 50)
            {
                sprite.Position = new Vector2(
                    _cubeList[2].TopRectangle.X + 32,
                    -16f);
                sprite.Distance = Vector2.Distance(
                    sprite.Position + sprite.Origin,
                    _cubeList[2].Origin);
                (sprite as Faerie).CurrentCubeIndex = 2;
            }
            else
            {
                sprite.Position = new Vector2(
                    _cubeList[1].TopRectangle.X + 32,
                    -16f);
                sprite.Distance = Vector2.Distance(sprite.Position + sprite.Origin,
                    _cubeList[1].Origin);
                (sprite as Faerie).CurrentCubeIndex = 1;
            }

            _spriteList.Add(sprite);
        }

        private void GenerateGreenOrb()
        {
            foreach (var obj in _spriteList)
            {
                if (obj is GreenOrb)
                {
                    return;
                }
            }

            var sprite = new GreenOrb(_gameRef, _spriteBatch);
            sprite.Initialize();


            sprite.Position = new Vector2(
                _cubeList[0].TopRectangle.X + 32,
                -16f);
            sprite.Distance = Vector2.Distance(
                sprite.Position + sprite.Origin,
                _cubeList[0].Origin);
            (sprite as GreenOrb).CurrentCubeIndex = 0;
            _spriteList.Add(sprite);
        }

        private void GeneratePurpleOrb()
        {
            foreach (var obj in _spriteList)
            {
                if (obj is PurpleOrb || obj is Snake)
                {
                    return;
                }
            }

            var sprite = new PurpleOrb(_gameRef, _spriteBatch);
            sprite.Initialize();

            if (_random.Next(100) < 50)
            {
                sprite.Position = new Vector2(
                    _cubeList[2].TopRectangle.X + 32,
                    -16f);
                sprite.Distance = Vector2.Distance(
                    sprite.Position + sprite.Origin,
                    _cubeList[2].Origin);
                (sprite as PurpleOrb).CurrentCubeIndex = 2;
            }
            else
            {
                sprite.Position = new Vector2(
                    _cubeList[1].TopRectangle.X + 32,
                    -16f);
                sprite.Distance = Vector2.Distance(sprite.Position + sprite.Origin,
                    _cubeList[1].Origin);
                (sprite as PurpleOrb).CurrentCubeIndex = 1;
            }

            (sprite as PurpleOrb).HatchingComplete += (s, e) =>
            {
                int cubeIndex = 21;
                for (int i = 21; i < 28; i++)
                {
                    if (_cubeList[i].TopRectangle.Intersects(sprite.BoundingBox))
                    {
                        cubeIndex = i;
                        break;
                    }
                }

                var newSprite = new Snake(_gameRef, _spriteBatch, cubeIndex);
                newSprite.Initialize();
                newSprite.Position = sprite.Position;
                (newSprite as Snake).Distance = 115;
                _spriteList.Add(newSprite);
            };
            _spriteList.Add(sprite);
        }

        private void GenerateRedOrb()
        {
            var sprite = new RedOrb(_gameRef, _spriteBatch);
            sprite.Initialize();

            if (_random.Next(100) < 50)
            {
                sprite.Position = new Vector2(
                    _cubeList[2].TopRectangle.X + 32,
                    0f);
                sprite.Distance = Vector2.Distance(
                    sprite.Position + sprite.Origin,
                    _cubeList[2].Origin);
                (sprite as RedOrb).CurrentCubeIndex = 2;
            }
            else
            {
                sprite.Position = new Vector2(
                    _cubeList[1].TopRectangle.X + 32,
                    0f);
                sprite.Distance = Vector2.Distance(sprite.Position + sprite.Origin,
                    _cubeList[1].Origin);
                (sprite as RedOrb).CurrentCubeIndex = 1;
            }

            _spriteList.Add(sprite);
        }

        public static int Row(int index)
        {
            if (index < 0 || index > 27)
            {
                return -1;
            }
            if (index == 0)
            {
                return 1;
            }
            else if (index < 3)
            {
                return 2;
            }
            else if (index < 6)
            {
                return 3;
            }
            else if (index < 10)
            {
                return 4;
            }
            else if (index < 15)
            {
                return 5;
            }
            else if (index < 21)
            {
                return 6;
            }

            return 7;
        }

        public void ResetGame()
        {
            _levelIndex = 0;
            ResetLevel();
        }

        private void ResetLevel()
        {
            foreach (var cube in _cubeList)
            {
                cube.ActiveColorIndex = 0;
                cube.Wrap = _levels.LevelDefs[_levelIndex].WrapAround;

                cube.TopColor.Clear();

                foreach (var c in _levels.LevelDefs[_levelIndex].TopColors)
                {
                    cube.TopColor.Add(c);
                }

                cube.LeftColor = _levels.LevelDefs[_levelIndex].LeftColor;
                cube.RightColor = _levels.LevelDefs[_levelIndex].RightColor;
            }

            _discList.Clear();

            foreach (var disc in _levels.LevelDefs[_levelIndex].Discs)
            {
                Disc d = new Disc(_gameRef, _spriteBatch);

                if (_cubeList[disc].UpLeft == -1)
                {
                    d.Position = new Vector2(
                        _cubeList[disc].TopRectangle.X - 96,
                        _cubeList[disc].TopRectangle.Y - 64);
                }
                else if (_cubeList[disc].UpRight == -1)
                {
                    d.Position = new Vector2(
                        _cubeList[disc].TopRectangle.X + 96,
                        _cubeList[disc].TopRectangle.Y - 64);
                }

                _discList.Add(disc, d);
            }

            foreach (var c in Game.Components)
            {
                if (c is QBert)
                {
                    (c as QBert).Reset(null);
                    break;
                }
            }
            _spriteList.Clear();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (var cube in _cubeList)
            {
                cube.Draw(gameTime, _spriteBatch);
            }

            foreach (var disc in _discList.Keys)
            {
                _discList[disc].Draw(gameTime);
            }

            foreach (var sprite in _spriteList)
            {
                sprite.Draw(gameTime);
            }
        }

        public void AddCube(Cube cube)
        {
            _cubeList.Add(cube);
        }
    }
}
