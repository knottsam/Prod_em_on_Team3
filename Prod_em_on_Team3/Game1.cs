using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System.Collections.Generic;
using System.Diagnostics;

namespace Prod_em_on_Team3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<EnemyObj> EnemyTypes;
        private RoomController _roomController;
        private Player _player;

        private const int enemyTypes = 1;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 1240;
            _graphics.PreferredBackBufferWidth = 1864;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _roomController = new RoomController();
            _player = new Player(new Vector2(1864/2,1240 / 2));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //EnemyTypes = EnemyHandler.InitialiseSprites(_graphics, Content, _spriteBatch, enemyTypes);

            _roomController.Started(Content,_spriteBatch);

            _roomController.LoadRoom("Start", 0, 0); //Middle or Start Room
            _roomController.LoadRoom("Room", 1, 0); //Right Room
            _roomController.LoadRoom("Room", -1, 0); //Left Room
            _roomController.LoadRoom("Room", 0, 1); //Bottom Room
            _roomController.LoadRoom("Room", 0, 0); //Top Room

            _player.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);

            _roomController.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _roomController.currentLoadRoomData.room.Draw(gameTime, _spriteBatch);
            _player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}