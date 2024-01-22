using System;
using System.Collections.Generic;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Prod_em_on_Team3
{
    public class Room : Sprite
    {

        public string Name;
        public int Width;
        public int Height;
        public int X;
        public int Y;
        public bool visibility = true;

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

            if (RoomController.instance == null)
            {
                Debug.WriteLine("Room instance nil");
                return false;
            }

            base.LoadContent(contentManager, spriteBatch, roomName);

            BoundingBox = new Rectangle(X, Y, SpriteTexture.Width, SpriteTexture.Height);

            RoomController.instance.RegisterRoom(this);

            return true;
        }

        public bool Visible
        {
            get { return visibility; }
            set { visibility = value; }
        }

        public Vector2 GetRoomCenter()
        {
            return new Vector2 (X*Width, Y*Height);
        }
    }
}
