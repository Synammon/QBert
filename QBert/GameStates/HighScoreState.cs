using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QBert.GameStates
{
    public class HighScoreState : BaseGameState
    {
        private SpriteBatch _spriteBatch;
        private HighScoreList _highScoreList;
        private SpriteFont _font;
        private KeyboardState _keyboardState;
        private KeyboardState _previousKeyboardState;
        private int _index = -1;
        private int _letter = -1;
        private char[] _letters = new char[3] { '_', '_', '_' };

        public HighScoreState(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            _spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                IncludeFields = true
            };

            string fileName = "Content\\HighScores.json";
            string json = File.ReadAllText(fileName);

            _highScoreList = JsonSerializer.Deserialize<HighScoreList>(json, options);
            _font = Game.Content.Load<SpriteFont>("GameFont");
        }

        public void SetScore(int score)
        {
            _index = -1;
            _letter = -1;

            for (int i = 0; i < 10; i++)
            {
                if ( score >= _highScoreList.HighScores[i].Score)
                {
                    _highScoreList.HighScores.Insert(i, new HighScore() { Name = "___",
                        Score = score
                    });
                    _index = i;
                    _highScoreList.HighScores.RemoveAt(10);

                    _letter = 0;

                    _letters[0] = 'A';
                    _letters[1] = 'A';
                    _letters[2] = 'A';

                    break;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            IStateManager stateManager = (IStateManager)Game.Services.GetService(typeof(IStateManager));
            _previousKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();

            if (_index != -1)
            {
                if (_keyboardState.IsKeyDown(Keys.Up) &&
                    _previousKeyboardState.IsKeyUp(Keys.Up))
                {
                    _letters[_letter]--;
                }
                else if (_keyboardState.IsKeyDown(Keys.Down) &&
                    _previousKeyboardState.IsKeyUp(Keys.Down))
                {
                    _letters[_letter]++;
                }
                else if (_keyboardState.IsKeyDown(Keys.Left) &&
                    _previousKeyboardState.IsKeyUp(Keys.Left))
                {
                    _letter--;
                    if (_letter < 0)
                        _letter = 2;
                }
                else if (_keyboardState.IsKeyDown(Keys.Right) &&
                    _previousKeyboardState.IsKeyUp(Keys.Right))
                {
                    _letter++;
                    if (_letter > 2)
                        _letter = 0;
                }
            }
            if ((_previousKeyboardState.IsKeyDown(Keys.Space) &&
                _keyboardState.IsKeyUp(Keys.Space)) ||
                (_previousKeyboardState.IsKeyDown(Keys.Enter) &&
                _keyboardState.IsKeyUp(Keys.Enter)))
            {
                if (_index == -1)
                {
                    stateManager.PopState();
                    return;
                }
                _highScoreList.HighScores[_index].Name = $"{_letters[0]}{_letters[1]}{_letters[2]}";
                // Save the high scores to a file
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    IncludeFields = true
                };
                string json = JsonSerializer.Serialize(_highScoreList, options);
                File.WriteAllText("Content\\HighScores.json", json);
                // Transition to the next state (e.g., GamePlayState)
                stateManager.PopState();
                return;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);

            for (int i = 0; i < 10; i++)
            {
                if (i < _highScoreList.HighScores.Count)
                {
                    string scoreText = $"{i + 1}";
                    Vector2 position = new Vector2(100, 100 + (i * 30));
                    _spriteBatch.DrawString(_font, scoreText, position, Color.White);
                    scoreText = $"{_highScoreList.HighScores[i].Name}";
                    position = new Vector2(200, 100 + (i * 30));
                    _spriteBatch.DrawString(_font, scoreText, position, Color.White);
                    scoreText = $"{_highScoreList.HighScores[i].Score}";
                    position = new Vector2(400, 100 + (i * 30));
                    _spriteBatch.DrawString(_font, scoreText, position, Color.White);
                    if (i == _index)
                    {
                        string nameText = $"{_letters[0]}{_letters[1]}{_letters[2]}";
                        Vector2 namePosition = new Vector2(200, 100 + (i * 30));
                        _spriteBatch.DrawString(_font, nameText, namePosition, Color.Red);
                    }
                }
            }
        }
    }
}
