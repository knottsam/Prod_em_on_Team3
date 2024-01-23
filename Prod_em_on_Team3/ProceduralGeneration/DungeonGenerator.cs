using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prod_em_on_Team3.ProceduralGeneration
{
    public class DungeonGenerator
    {
        public DungeonGenerationData dungeonGenerationData;
        private List<Vector2Int> dungeonRooms;

        public void Start()
        {
            dungeonGenerationData = new DungeonGenerationData(3, 7, 8);
            dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
            SpawnRooms(dungeonRooms);
        }

        private void SpawnRooms(IEnumerable<Vector2Int> rooms)
        {
            RoomController.instance.LoadRoom("Start", 0, 0);
            foreach(Vector2Int roomLocation in rooms)
            {
                RoomController.instance.LoadRoom("Room", roomLocation.X, roomLocation.Y);
            }
            RoomController.instance.OnRoomsLoaded();
        }
    }
}
