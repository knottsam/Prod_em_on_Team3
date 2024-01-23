using System;
using System.Collections.Generic;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Metadata;
using System.Linq;
using SharpDX.Direct3D9;

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
        public bool wallCollision = true;
        public Dictionary<string, Animation> Doors;
        public AnimationManager AnimationManager;

        public Room() { }

        public Room(int inWidth, int inHeight, int inX, int inY) 
            :base(new Vector2(inX, inY), new Rectangle(), Color.White)
        {

            Width = inWidth;
            Height = inHeight;
            X = inX;
            Y = inY;
        }

        public virtual bool LoadContent(ContentManager contentManager, SpriteBatch spriteBatch, string roomName)
        {
             Doors = new Dictionary<string, Animation>()
             {
                { "TopDoor", new Animation(contentManager.Load<Texture2D>("TopDoor"), 2) },
                { "LeftDoor", new Animation(contentManager.Load<Texture2D>("LeftDoor"), 2) },
                { "BottomDoor", new Animation(contentManager.Load<Texture2D>("BottomDoor"), 2) },
                { "RightDoor", new Animation(contentManager.Load<Texture2D>("RightDoor"), 2) }
             };

            AnimationManager = new AnimationManager(Doors.First().Value, 0.5f);

            if (RoomController.instance == null)
            {
                Debug.WriteLine("Room instance nil");
                return false;
            }

            base.LoadContent(contentManager, spriteBatch, roomName);
            BoundingBox = new Rectangle(X*Width, Y*Height, SpriteTexture.Width, SpriteTexture.Height);

            RoomController.instance.RegisterRoom(this);

            return true;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            AnimationManager.Draw(spriteBatch);

            base.Draw(gameTime, spriteBatch);
        }

        public virtual void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {


            UpdateDoors();

            base.Update(gameTime);
        }

        public void UpdateDoors()
        {

            if (Player._player.InCombat)
            {
                
            }
            else
                AnimationManager.Stop();


        }

        public void AddDoor(string Type)
        {

        }

        public bool Visible
        {
            get { return visibility; }
            set { visibility = value; }
        }

        public bool WallCollide
        {
            get { return wallCollision; }
            set { wallCollision = value; }
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
