﻿using System;
using System.Collections.Generic;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Metadata;
using System.Linq;

namespace Prod_em_on_Team3
{
    public class Room : Sprite
    {

        public string Name;
        public int Width;
        public int Height;
        public int X;
        public int Y;
        public bool visibility = false;
        public Rectangle hitbox;
        public List<Door> roomDoors;
        
        public Room() { }

        public Room(int inWidth, int inHeight, int inX, int inY) 
            :base(new Vector2(inX, inY), new Rectangle(), Color.White)
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

            roomDoors = new List<Door>();

            hitbox = new Rectangle((X*Width)+205, (Y*Height)+205, 1390, 775);

            base.LoadContent(contentManager, spriteBatch, roomName);
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

        public Vector2 GetRoomCenter()
        {
            return new Vector2 (X*Width, Y*Height);
        }

        public void onCollisionTriggered()
        {
            RoomController.instance.OnPlayerEnter(this);
        }
    }
}
