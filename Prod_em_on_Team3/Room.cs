using System;
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
        public bool wallCollision = true;
        public Dictionary<string, Animation> DoorsAnims;
        public AnimationManager topDoor;
        public AnimationManager bottomDoor;
        public AnimationManager leftDoor;
        public AnimationManager rightDoor;
        


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
             DoorsAnims = new Dictionary<string, Animation>()
             {
                { "TopDoor", new Animation(contentManager.Load<Texture2D>("TopDoor"), 2) },
                { "LeftDoor", new Animation(contentManager.Load<Texture2D>("LeftDoor"), 2) },
                { "BottomDoor", new Animation(contentManager.Load<Texture2D>("BottomDoor"), 2) },
                { "RightDoor", new Animation(contentManager.Load<Texture2D>("RightDoor"), 2) }
             };

            topDoor = new AnimationManager(DoorsAnims["TopDoor"], 0.5f);
            rightDoor = new AnimationManager(DoorsAnims["RightDoor"], 0.5f);
            leftDoor = new AnimationManager(DoorsAnims["LeftDoor"], 0.5f);
            bottomDoor = new AnimationManager(DoorsAnims["BottomDoor"], 0.5f);

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

            base.Draw(gameTime, spriteBatch);

            topDoor.Draw(spriteBatch);
            rightDoor.Draw(spriteBatch);
            leftDoor.Draw(spriteBatch);
            bottomDoor.Draw(spriteBatch);
        }

        public virtual void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {


            UpdateDoors();

            topDoor.Update(gameTime);
            rightDoor.Update(gameTime);
            leftDoor.Update(gameTime);
            bottomDoor.Update(gameTime);

            base.Update(gameTime);
        }

        public void UpdateDoors()
        {

            if (Player._player.InCombat)
            {
                
            }
            else
            {
                topDoor.Stop();
                rightDoor.Stop();
                leftDoor.Stop();
                bottomDoor.Stop();
            }


        }

        public void AddDoor(string Type, Vector2 DoorPos)
        {
            if (Type == "Right")
            {
                Debug.WriteLine(rightDoor.Position);
                rightDoor.Position = DoorPos;
                Debug.WriteLine(rightDoor.Position);
            }
            if (Type == "Bottom")
            {
                bottomDoor.Position = DoorPos;
            }
            if (Type == "Left")
            {
                leftDoor.Position = DoorPos;
            }
            if (Type == "Up")
            {
                topDoor.Position = DoorPos;
            }
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
