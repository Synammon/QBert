using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QBert.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace QBert.GameStates
{
    public class GamePlayState : BaseGameState
    {
        private SpriteBatch _spriteBatch;
        private Pyramid _pyramid;
        private static GameObject.QBert _qBert;
        private Cube _previewCube;
        private Player _player;

        public static GameObject.QBert QBert
        {
            get { return _qBert; }
        }

        public GamePlayState(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            Texture2D top = content.Load<Texture2D>("top_side");
            Texture2D left = content.Load<Texture2D>("left_side");
            Texture2D right = content.Load<Texture2D>("right_side");

            List<Color> topColors = new List<Color>();
            topColors.Add(Color.White);

            Color leftColor = Color.White;
            Color rightColor = Color.White;

            _previewCube = new Cube(top, left, right, topColors, leftColor, rightColor, new Point(75, 75));

            _qBert = new GameObject.QBert(GameRef, _spriteBatch);
            _pyramid = new Pyramid(GameRef, _spriteBatch);
            _player = new Player(GameRef, _spriteBatch);

            _pyramid.Initialize();
            GameRef.Components.Add(_pyramid);

            GameRef.Components.Add(_qBert);

            _qBert.Initialize();            
            _qBert.SetOrigin(_pyramid.CubeList[0].Origin);
            _qBert.CurrentCubeIndex = 0;

            _player.Initialize();
            GameRef.Components.Add(_player);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var component in GameRef.Components)
            {
                if (component is Pyramid p)
                {
                    if (_qBert.CurrentCubeIndex != -1)
                    {
                        Cube c = p.CubeList[_qBert.CurrentCubeIndex];

                        _previewCube.RightColor = c.RightColor;
                        _previewCube.LeftColor = c.LeftColor;
                        _previewCube.TopColor[0] =
                            c.TopColor[c.TopColor.Count - 1];
                    }
                }
            }
        }

        public override void Show()
        {
            base.Show();
            Enabled = true;
            _pyramid.Enabled = true;
            _qBert.Enabled = true;
            _pyramid.ResetGame();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _previewCube.Draw(gameTime, _spriteBatch);
        }
    }
}
