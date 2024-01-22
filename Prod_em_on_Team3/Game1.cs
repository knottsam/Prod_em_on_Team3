using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_invaders.Content;

namespace Prod_em_on_Team3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player firstPlayer;
        private Enemy firstEnemy;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            firstPlayer = new Player(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2),
                new Rectangle(), Color.White);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            firstPlayer.LoadContent(Content, "Player");
            firstEnemy.LoadContent(Content, "Enemy");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            firstPlayer.Update(gameTime, true, _graphics.PreferredBackBufferWidth); //FIX THIS... other class issue


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            firstPlayer.Draw(_spriteBatch);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

    }
}