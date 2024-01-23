using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prod_em_on_Team3.ProceduralGeneration
{

    public struct Vector2Int
    {
        public int X;
        public int Y;

        public Vector2Int(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2Int) return this.Equals((Vector2Int)obj);
            else return false;
        }

        public bool Equals(Vector2Int other)
        {
            return ((this.X == other.X) && (this.Y == other.Y));
        }

        public static bool operator ==(Vector2Int value1, Vector2Int value2)
        {
            return ((value1.X == value2.X) && (value1.Y == value2.Y));
        }

        public static bool operator !=(Vector2Int value1, Vector2Int value2)
        {
            if (value1.X == value2.X) return value1.Y != value2.Y;
            return true;
        }

        public static Vector2Int operator +(Vector2Int value1, Vector2Int value2)
        {
            
            return new Vector2Int(value1.X+value2.X, value1.Y+value2.Y);
        }

        public override int GetHashCode()
        {
            return (this.X.GetHashCode() + this.Y.GetHashCode());
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Z:{1}}}", this.X, this.Y);
        }
    }

    public enum Direction
    {
        up = 0,
        left = 1,
        down = 2,
        right = 3,
    };

    public class DungeonCrawlerController
    {
        public static List<Vector2Int> positionsVisited = new List<Vector2Int>();

        private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
        {
            {Direction.up, new Vector2Int(0,1)},
            {Direction.left, new Vector2Int(-1,0)},
            {Direction.down, new Vector2Int(0,-1)},
            {Direction.right, new Vector2Int(1,0)},

        };

        public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
        {
            List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();
            Random random = new Random();

            for(int i = 0; i < dungeonData.numberOfCrawlers; i ++)
            {
                dungeonCrawlers.Add(new DungeonCrawler(new Vector2Int(0, 0)));
            }

            int iterations = random.Next(dungeonData.iterationMin, dungeonData.iterationMax);

            for (int i = 0; i < iterations; i ++)
            {
                foreach (DungeonCrawler dungeonCrawler in dungeonCrawlers)
                {
                    Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);
                    positionsVisited.Add(newPos);
                }
            }
            return positionsVisited;
        }

    }
}
