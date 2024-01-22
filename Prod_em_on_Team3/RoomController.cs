using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Prod_em_on_Team3
{
    public class RoomInfo
    {
        public string name;
        public int x;
        public int y;
        public Room room;
    }

    public class RoomController
    {
        private SpriteBatch _spriteBatch;
        private ContentManager content;

        public static RoomController instance;

        string currentFloorName = "Basement";

        public RoomInfo currentLoadRoomData;

        Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

        public List<Room> loadedRooms = new List<Room>();

        bool isLoadingRoom = false;

        public void Started(ContentManager inContent, SpriteBatch inSpriteBatch)
        {
            instance = this;
            content = inContent;
            _spriteBatch = inSpriteBatch;
        }

        public void Update()
        {

            UpdateRoomQueue();

        }
        void UpdateRoomQueue()
        {
            if (isLoadingRoom)
                return;

            if (loadRoomQueue.Count == 0)
                return;


            currentLoadRoomData = loadRoomQueue.Dequeue();
            isLoadingRoom = true;

            LoadRoomRoutine(currentLoadRoomData);
        }

        public void LoadRoom(string roomName, int x, int y)
        {

            if (DoesRoomExist(x, y))
            {
                return;
            }
            RoomInfo newRoomData = new RoomInfo();
            newRoomData.name = roomName;
            newRoomData.x = x;
            newRoomData.y = y;
            newRoomData.room = new Room(1864, 1240, x, y);

            loadRoomQueue.Enqueue(newRoomData);
            
        }

        public void LoadRoomRoutine(RoomInfo info)
        {
            string roomName = currentFloorName + info.name;

            bool finished = info.room.LoadContent(content, _spriteBatch, roomName);

            while (!finished)
            {
                
            }
        }

        public void RegisterRoom( Room room)
        {
            room.Position = new Vector2(
                currentLoadRoomData.x * room.Width, 
                currentLoadRoomData.y * room.Height
            );

            room.X = currentLoadRoomData.x;
            room.Y = currentLoadRoomData.y;
            room.Name = currentFloorName + " - " + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            Debug.WriteLine(room.Name);

            isLoadingRoom = false;

            loadedRooms.Add(room);

        }

        public bool DoesRoomExist( int x, int y )
        {
            return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
        }

    }

}
