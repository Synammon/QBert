using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using QBert.GameObject;
using System.Collections.Generic;

namespace QBert
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Pyramid _pyramid;
        private static QBert.GameObject.QBert _qBert;

        public static QBert.GameObject.QBert QBert
        {
            get { return _qBert; }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _qBert = new QBert.GameObject.QBert(this, _spriteBatch);
            _pyramid = new Pyramid(this, _spriteBatch);

            _pyramid.Initialize();
            Components.Add(_pyramid);

            Components.Add(_qBert);
            _qBert.Initialize();

            _qBert.SetOrigin(_pyramid.CubeList[0].Origin);
            _qBert.CurrentCubeIndex = 0;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            base.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}
