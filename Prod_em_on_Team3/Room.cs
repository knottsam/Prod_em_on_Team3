using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Prod_em_on_Team3.EnemySystem;

namespace Prod_em_on_Team3
{
    public class Room : Sprite
    {

        public string Name;
        public int Width;
        public int Height;
        public int X;
        public int Y;

        private bool visibility = false;
        private bool playerInRoom = false;
        private bool roomFound = false;

        public Rectangle hitbox;
        public List<Door> roomDoors;
        public Rectangle[,] tileMap;
        public List<EnemyObj> enemies = new List<EnemyObj>();

        public Texture2D tempTexture;

        public Room() { }

        public Room(int inWidth, int inHeight, int inX, int inY) 
            :base(new Vector2(inX, inY), new Rectangle(), Color.White, 1f)
        {

            Width = inWidth;
            Height = inHeight;
            X = inX;
            Y = inY;

        }

        public virtual void LoadContent(ContentManager contentManager, SpriteBatch spriteBatch, string roomName)
        {
            if (RoomController.instance == null)
            {
                Debug.WriteLine("Room instance nil");
                return;
            }

            tempTexture = contentManager.Load<Texture2D>("TempSprite");

            roomDoors = new List<Door>();

            hitbox = new Rectangle((X*Width)+205, (Y*Height)+205, 1390, 775);

            base.LoadContent(contentManager, roomName);
            BoundingBox = new Rectangle(X*Width, Y*Height, SpriteTexture.Width, SpriteTexture.Height);
            Debug.WriteLine("Room Content load");

            RoomController.instance.RegisterRoom(this);
        }

        public void AddDoor(string Type, Vector2 DoorPos, ContentManager content)
        {
            Door newDoor = new Door(Type, DoorPos);
            newDoor.LoadContent(content);
            roomDoors.Add(newDoor);
        }

        public bool Visible
        {
            get { return visibility; }
            set { visibility = value; }
        }

        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }

        public bool PlayerRoom
        {
            get { return playerInRoom; }
            set { playerInRoom = value; }
        }
        public bool PreviouslyEntered
        {
            get { return roomFound; }
            set { roomFound = value; }
        }

        public Vector2 GetRoomCenter()
        {
            return new Vector2 (X*Width, Y*Height);
        }

        public void OnRoomReady()
        {
            tileMap = Tilemap.CreateTilemap(GetRoomCenter() + new Vector2(200, 200));
        }
    }
}
