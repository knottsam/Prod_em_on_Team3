using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using SharpDX.MediaFoundation;

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

        private Camera2D _camera;

        public static RoomController instance;

        string currentFloorName = "Basement";

        public RoomInfo currentLoadRoomData;

        public Room currentRoom;

        Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

        public List<Room> loadedRooms = new List<Room>();

        bool isLoadingRoom = false;

        public void Started(ContentManager inContent, SpriteBatch inSpriteBatch, Camera2D inCam)
        {
            instance = this;
            content = inContent;
            _spriteBatch = inSpriteBatch;
            _camera = inCam;
        }

        public void Update()
        {

            foreach(Room room in loadedRooms)
            {
                if(room != currentRoom)
                {
                    room.Visible = false;
                }
            }

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

            Debug.WriteLine(x+","+y);

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

            if (loadedRooms.Count == 0)
            {
                _camera.Position = room.GetRoomCenter() + new Vector2(580, 380);
            }

            if(DoesRoomExist(room.X+1, room.Y))
            {
                room.AddDoor("Right", room.Position + new Vector2(580, 380));
            }
            if (DoesRoomExist(room.X, room.Y+1))
            {
                room.AddDoor("Bottom", room.Position + new Vector2(880, 200));
            }
            if (DoesRoomExist(room.X - 1, room.Y))
            {
                room.AddDoor("Left", room.Position + new Vector2(200, 380));
            }
            if (DoesRoomExist(room.X, room.Y-1))
            {
                room.AddDoor("Up", room.Position + new Vector2(580, 900));
            }


            loadedRooms.Add(room);

        }

        public bool DoesRoomExist( int x, int y )
        {
            return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
        }

        public void OnPlayerEnter(Room inRoom)
        {
            foreach (Room room in loadedRooms)
            {
                room.Visible = false;
            }

            inRoom.Visible = true;
            _camera.Position = inRoom.GetRoomCenter() + new Vector2(580, 380);
            currentRoom = inRoom;
        }

    }

}
