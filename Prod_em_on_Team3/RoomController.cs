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


        public List<Room> loadedRooms = new List<Room>();


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

            currentLoadRoomData = newRoomData;
            LoadRoomRoutine(newRoomData);
            
        }

        public void LoadRoomRoutine(RoomInfo info)
        {
            string roomName = currentFloorName + info.name;

            info.room.LoadContent(content, _spriteBatch, roomName);

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

            if (loadedRooms.Count == 0)
            {
                _camera.Position = room.GetRoomCenter() + new Vector2(580, 380);
                room.Visible = true;
            }
            if(room.X == 0 && room.Y == 0)
            {
                currentRoom = room;
            }

            loadedRooms.Add(room);

        }

        public void OnRoomsLoaded()
        {

            foreach(Room room in loadedRooms)
            {
                if (DoesRoomExist(room.X + 1, room.Y))
                {
                    room.AddDoor("Right", room.GetRoomCenter() + new Vector2(1650, 500), content);
                }
                if (DoesRoomExist(room.X, room.Y + 1))
                {
                    room.AddDoor("Bottom", room.GetRoomCenter() + new Vector2(825, 1025), content);
                }
                if (DoesRoomExist(room.X - 1, room.Y))
                {
                    room.AddDoor("Left", room.GetRoomCenter() + new Vector2(80, 500), content);
                }
                if (DoesRoomExist(room.X, room.Y - 1))
                {
                    room.AddDoor("Top", room.GetRoomCenter() + new Vector2(825, 80), content);
                }
            }
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
