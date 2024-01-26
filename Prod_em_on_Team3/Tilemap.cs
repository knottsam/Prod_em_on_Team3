using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prod_em_on_Team3
{
    static class Tilemap
    { 

        public static Rectangle[,] CreateTilemap(Vector2 StartingPosition)
        {
            Rectangle[,] tileMap = new Rectangle[13,7];

            int TileWidth = 112;
            int TileHeight = 119;

            for(int i = 0; i < tileMap.GetLength(0); i++)
                for(int j = 0; j < tileMap.GetLength(1); j++)
                {
                    tileMap[i, j] = new Rectangle((int)StartingPosition.X + (TileWidth*i), (int)StartingPosition.Y+ (TileHeight*j),TileWidth,TileHeight);
                }



            return tileMap;
        }
    }
}
