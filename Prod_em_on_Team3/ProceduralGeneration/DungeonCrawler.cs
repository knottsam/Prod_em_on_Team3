using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prod_em_on_Team3.ProceduralGeneration
{
    public class DungeonCrawler
    {
        public Vector2Int Position {  get; set; }
        public DungeonCrawler(Vector2Int startPos) 
        {
            Position = startPos;
        }

        public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
        {
            Random random = new Random();
            Direction toMove = (Direction)random.Next(0, directionMovementMap.Count);
            Position += directionMovementMap[toMove];
            return Position;
        }

    }
}
