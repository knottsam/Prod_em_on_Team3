using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Prod_em_on_Team3.ProceduralGeneration;

namespace Prod_em_on_Team3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<EnemyObj> EnemyTypes;
        private RoomController _roomController;
        private Player _player;
        private Camera2D _camera;
        private ResolutionIndependentRenderer _resolutionIndependentRenderer;

        private const int enemyTypes = 1;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _resolutionIndependentRenderer = new ResolutionIndependentRenderer(this);
            _graphics.PreferredBackBufferHeight = 950;
            _graphics.PreferredBackBufferWidth = 1600;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _player = new Player();
            _roomController = new RoomController();
            _camera = new Camera2D(_resolutionIndependentRenderer);
            _camera.Zoom = 1.05f; //1.05f

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //EnemyTypes = EnemyHandler.InitialiseSprites(_graphics, Content, _spriteBatch, enemyTypes);

            _roomController.Started(Content,_spriteBatch, _camera);
            _player.LoadContent(Content);
            DungeonGenerator generation = new DungeonGenerator();
            generation.Start();

        }

        protected override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);

            foreach (Room room in _roomController.loadedRooms)
            {

                foreach(Door door in room.roomDoors)
                {
                    door.Update(gameTime, _player.InCombat);
                }
                
            }

            _roomController.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone,null, _camera.GetViewTransformationMatrix());


            foreach(Room room in _roomController.loadedRooms)
            {
                if (room.Visible)
                {
                    room.Draw(gameTime, _spriteBatch);
                    foreach (Door door in room.roomDoors)
                    {
                        door.Draw(_spriteBatch);
                    }
                }
            }
            _player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}