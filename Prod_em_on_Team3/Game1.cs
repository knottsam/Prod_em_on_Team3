using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Prod_em_on_Team3;
using Prod_em_on_Team3.Content;
using System;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace Prod_em_on_Team3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player firstPlayer;
        private Enemy firstEnemy;
        private Bullet firstBullet;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            firstEnemy = new Enemy(new Vector2(_graphics.PreferredBackBufferWidth / 5, _graphics.PreferredBackBufferHeight / 2),
                new Rectangle(), Color.White);
            firstBullet = new Bullet(firstEnemy.Position, new Rectangle(), Color.DarkGreen, true, firstEnemy);



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            firstPlayer.LoadContent(Content, "Player");
            firstEnemy.LoadContent(Content, "Enemy");
            firstBullet.LoadContent(Content, "round ball");



            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            firstPlayer.Update(gameTime, true, _graphics.PreferredBackBufferWidth); //FIX THIS... other class issue
            firstEnemy.Update(gameTime, true, _graphics.PreferredBackBufferWidth);
            firstBullet.Update(gameTime, true, _graphics.PreferredBackBufferWidth);


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            firstPlayer.Draw(_spriteBatch);
            firstEnemy.Draw(_spriteBatch);
            firstBullet.Draw(_spriteBatch);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);

        }
    }
}