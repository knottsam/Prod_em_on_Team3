using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System.Collections.Generic;

namespace Prod_em_on_Team3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _DungeonWallLeftEdge, _DungeonWallRightEdge, _DungeonWallMiddle, _DungeonWallTopBasic, _DungeonFloorTopLeft, _DungeonFloorTopRight, _DungeonFloorTopConnector, _DungeonFloorBasic, _DungeonFloorLeftEdge, _DungeonFloorRightEdge;
        private Texture2D _dungeonTileSet;
        private List<Texture2D> allTextures = new List<Texture2D>();
        private char[,] tilePositionsArray;
        private Tile[,] tileArray;
        private Tile[,][,] mapTileArray;
        private int mapX, mapY;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1386;
            _graphics.PreferredBackBufferHeight = 936;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spriteBatch.Begin();

            _dungeonTileSet = Content.Load<Texture2D>("DungeonTileSet");

            allTextures = TileManager.CreateTextureMap(_dungeonTileSet, 24, 24, GraphicsDevice);

            tilePositionsArray = TileManager.FileReader("WorldTileMap.txt", 15);
            mapTileArray = TileManager.CreateWorldMap(tilePositionsArray, (_graphics.PreferredBackBufferWidth / 10), (_graphics.PreferredBackBufferHeight / 10), allTextures);

            mapX = 1;
            mapY = 0;

            _spriteBatch.End();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                mapX = 0;

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                mapX = 1;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                mapX = 2;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            foreach (Tile tile in mapTileArray[mapX, mapY])
            {
                tile.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void MoveRoom(string direction)
        {
            switch (direction)
            {
                case "Up":
                    if (mapY != 0)
                        mapY -= 1;
                    break;

                case "Down":
                    if (mapY != 4)
                        mapY += 1;
                    break;

                case "Left":
                    if (mapX != 0)
                        mapX -= 1;
                    break;

                case "Right":
                    if (mapX != 4)
                        mapX += 1;
                    break;
            }
        }
    }
}