using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;

namespace Prod_em_on_Team3
{
    public class Door
    {

        protected AnimationManager animationManager;
        protected Dictionary<string, Animation> DoorsAnims;
        protected Vector2 _position = new Vector2(1864 / 2, 1240 / 2);
        protected Rectangle _hitBox;
        protected Sprite hBoxSprite;
        public bool InCombat;
        public string _doorType;

        

        public bool Closed { get; set; }

        public Door(string Type, Vector2 Position)
        {
            _doorType = Type;
            _position = Position;
        }

        public new void LoadContent(ContentManager Content)
        {
            DoorsAnims = new Dictionary<string, Animation>()
             {
                { "Top", new Animation(Content.Load<Texture2D>("TopDoor"), 2) },
                { "Left", new Animation(Content.Load<Texture2D>("LeftDoor"), 2) },
                { "Bottom", new Animation(Content.Load<Texture2D>("BottomDoor"), 2) },
                { "Right", new Animation(Content.Load<Texture2D>("RightDoor"), 2) }
             };

            animationManager = new AnimationManager(DoorsAnims[_doorType], 2);
            Closed = false;
        }

        public virtual void Update(GameTime gameTime, bool Combat)
        {

            InCombat = Combat;
            SetAnimations();

            animationManager.Position = _position;

            if (_doorType == "Top")
                _hitBox = new Rectangle((int)_position.X+80, (int)_position.Y+100, 20, 40);
            else if (_doorType == "Bottom")
                _hitBox = new Rectangle((int)_position.X + 80, (int)_position.Y+30, 20, 20);
            else if(_doorType == "Left")
                _hitBox = new Rectangle((int)_position.X+20, (int)_position.Y + 90, 120, 20);
            else
                _hitBox = new Rectangle((int)_position.X-20, (int)_position.Y + 90, 120, 20);

            animationManager.Update(gameTime);
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {


            if (animationManager != null)
            {
                animationManager.Draw(spriteBatch);
            }
            else
                return;
        }

        protected virtual void SetAnimations()
        {
            if (InCombat == true)
            {
                Closed = true;
                animationManager.Stop(1);
            }
            else
            {
                Closed = false;
                animationManager.Stop(0);
            }
        }

        public Vector2 Enter()
        {
            Vector2 plrPosition = _position + new Vector2(0, -250); ;
            foreach (Room room in RoomController.instance.loadedRooms)
            {
                if (_doorType == "Top" && (room.Y == RoomController.instance.currentRoom.Y-1 && room.X == RoomController.instance.currentRoom.X))
                {
                    plrPosition = _position + new Vector2(70, -400);
                    RoomController.instance.OnPlayerEnter(room);
                    return plrPosition;
                }
                else if (_doorType == "Bottom" && (room.Y == RoomController.instance.currentRoom.Y + 1 && room.X == RoomController.instance.currentRoom.X))
                {
                    plrPosition = _position + new Vector2(70, 440);
                    RoomController.instance.OnPlayerEnter(room);
                    return plrPosition;
                }
                else if (_doorType == "Left" && (room.X == RoomController.instance.currentRoom.X - 1 && room.Y == RoomController.instance.currentRoom.Y))
                {
                    plrPosition = _position + new Vector2(-400, 80);
                    RoomController.instance.OnPlayerEnter(room);
                    return plrPosition;
                }
                else if (_doorType == "Right" && (room.X == RoomController.instance.currentRoom.X + 1 && room.Y == RoomController.instance.currentRoom.Y))
                {
                    plrPosition = _position + new Vector2(440, 80);
                    RoomController.instance.OnPlayerEnter(room);
                    return plrPosition;
                }
            }
            return plrPosition;
        }

        public Rectangle BoundingBox
        {
            get { return _hitBox; }
            set { _hitBox = value; }
        }
    }
}
