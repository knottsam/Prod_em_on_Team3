using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prod_em_on_Team3
{
    internal static class TileManager
    {
        public static char[,] FileReader(string fileName, int mapSize)
        {
            int[] fileDimensions = FindFileDimensions(fileName);

            char[,] charArray = new char[fileDimensions[0], fileDimensions[1]];
            StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + fileName);
            string line = "";
            int counter = 0;

            do
            {
                line = reader.ReadLine();

                for (int i = 0; i < line.Length; i++)
                {
                    charArray[i, counter] = line[i];
                }

                if (counter < line.Length - 1)
                {
                    counter++;
                }
            } while (!reader.EndOfStream);

            reader.Close();
            return charArray;
        }

        public static Tile[,] CreateMap(char[,] mapArray, int tileWidth, int tileHeight, List<Texture2D> textures)
        {
            Tile[,] tileArray = new Tile[mapArray.GetLength(0), mapArray.GetLength(1)];
            Vector2 tilePosition;

            for (int i = 0; i < mapArray.GetLength(0); i++)
            {
                for (int j = 0; j < mapArray.GetLength(1); j++)
                {
                    tilePosition = new Vector2(tileWidth * i, tileHeight * j);

                    switch ((int)mapArray[i, j] - 48) // after 9 is :;<=>?@ (capitalised alphabet)
                    {
                        case 0:
                            tileArray[i, j] = new Tile(textures[0], tilePosition, Color.White, "DungeonWallLeftEdge", tileWidth, tileHeight);
                            tileArray[i, j].collidable = true;
                            break;

                        case 1:
                            tileArray[i, j] = new Tile(textures[1], tilePosition, Color.White, "DungeonWallRightEdge", tileWidth, tileHeight);
                            tileArray[i, j].collidable = true;
                            break;

                        case 2:
                            tileArray[i, j] = new Tile(textures[2], tilePosition, Color.White, "DungeonWallMiddle", tileWidth, tileHeight);
                            tileArray[i, j].collidable = true;
                            break;

                        case 3:
                            tileArray[i, j] = new Tile(textures[3], tilePosition, Color.White, "DungeonWallTopBasic", tileWidth, tileHeight);
                            tileArray[i, j].collidable = true;
                            break;

                        case 4:
                            tileArray[i, j] = new Tile(textures[4], tilePosition, Color.White, "DungeonFloorTopLeft", tileWidth, tileHeight);
                            tileArray[i, j].collidable = false;
                            break;

                        case 5:
                            tileArray[i, j] = new Tile(textures[5], tilePosition, Color.White, "DungeonFloorTopRight", tileWidth, tileHeight);
                            tileArray[i, j].collidable = false;
                            break;

                        case 6:
                            tileArray[i, j] = new Tile(textures[6], tilePosition, Color.White, "DungeonFloorTopConnector", tileWidth, tileHeight);
                            tileArray[i, j].collidable = false;
                            break;

                        case 7:
                            tileArray[i, j] = new Tile(textures[7], tilePosition, Color.White, "DungeonFloorBasic", tileWidth, tileHeight);
                            tileArray[i, j].collidable = false;
                            break;

                        case 8:
                            tileArray[i, j] = new Tile(textures[8], tilePosition, Color.White, "DungeonFloorLeftEdge", tileWidth, tileHeight);
                            tileArray[i, j].collidable = false;
                            break;

                        case 9:
                            tileArray[i, j] = new Tile(textures[9], tilePosition, Color.White, "DungeonFloorRightEdge", tileWidth, tileHeight);
                            tileArray[i, j].collidable = false;
                            break;
                    }
                }
            }

            return tileArray;
        }

        public static List<Texture2D> CreateTextureMap(Texture2D tileSet, int tileWidth, int tileHeight, GraphicsDevice graphics)
        {
            int tileSetWidth = tileSet.Width / tileWidth;
            int tileSetHeight = tileSet.Height / tileHeight;
            List<Texture2D> allTextures = new List<Texture2D>();
            Rectangle rectangle = new Rectangle();

            for (int i = 0; i < tileSetWidth; i++)
            {
                for (int j = 0; j < tileSetHeight; j++)
                {
                    rectangle = new Rectangle(i * tileSetWidth, j * tileHeight, tileWidth, tileHeight);
                    allTextures.Add(GetTileTexture(tileSet, graphics, rectangle));
                }
            }

            return allTextures;
        }

        public static Texture2D GetTileTexture(Texture2D tileMap, GraphicsDevice graphics, Rectangle rect)
        {
            Texture2D texture = new Texture2D(graphics, rect.Width, rect.Height);
            int count = rect.Width * rect.Height;
            Color[] data = new Color[count];
            tileMap.GetData(0, rect, data, 0, count);
            texture.SetData(data);
            return texture;
        }

        public static int[] FindFileDimensions(string fileName)
        {
            StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + fileName);
            string line = "";
            int counter = 0;
            int[] dimensions = new int[2];

            do
            {
                line = reader.ReadLine();
                counter++;
            } while (!reader.EndOfStream);

            dimensions[0] = line.Length;
            dimensions[1] = counter;

            reader.Close();

            return dimensions;
        }

        public static Tile[,][,] CreateWorldMap (char[,] mapArray, int tileWidth, int tileHeight, List<Texture2D> textures)
        {
            Tile[,][,] worldMapArray = new Tile[5,5][,];
            char[,] roomMapArray;

            for (int i = 0; i < mapArray.GetLength(0); i++)
            {
                for (int j = 0; j < mapArray.GetLength(1); j++)
                {
                    roomMapArray = FileReader("room" + mapArray[i,j] + ".txt", 10);
                    worldMapArray[i, j] = CreateMap(roomMapArray, tileWidth, tileHeight, textures);
                }
            }

            return worldMapArray;
        }
    }
}
