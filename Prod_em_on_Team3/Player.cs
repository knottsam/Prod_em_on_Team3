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
    public class Player
    {

        protected AnimationManager _animationBodyManager;
        protected Dictionary<string, Animation> _bodyAnimations;
        protected AnimationManager _animationHeadManager;
        protected Dictionary<string, Animation> _HeadAnimations;
        protected Vector2 _position = new Vector2(1864 / 2, 1240 / 2);
        protected Texture2D _texture;
        protected Rectangle _hitBox;
        protected Sprite hBoxSprite;
        protected float statusCheck;

        //Stats
        private int Health = 6; //20 max
        private double Damage = 3;
        private double BulletSpeed = 1;
        private float shotsPerSec = 2.5f;
        private float moveSpeed = 1f;
        private bool combat = false;
        //Stats

        public static Player instance;

        public new void LoadContent(ContentManager Content)
        {

            _hitBox = new Rectangle(0,0, 224, 100);
            _HeadAnimations = new Dictionary<string, Animation>()
            {
                { "Down", new Animation(Content.Load<Texture2D>("FireDown"), 2) },
            };

            _bodyAnimations = new Dictionary<string, Animation>()
            {
                //{ "WalkVertical", new Animation(Content.Load<Texture2D>("Vertical"), 9) },
                { "WalkRight", new Animation(Content.Load<Texture2D>("Right"), 9) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("Left"), 9) },
            };

            _animationHeadManager = new AnimationManager(_HeadAnimations.First().Value, 1/shotsPerSec);
            _animationBodyManager = new AnimationManager(_bodyAnimations.First().Value, moveSpeed/12);
        }

        public virtual void Update(GameTime gameTime)
        {
            Room currentRoom = RoomController.instance.currentRoom;

            bool canCollide = false;

            statusCheck += gameTime.ElapsedGameTime.Milliseconds;

            foreach(Door door in currentRoom.roomDoors)
            {
                if (_hitBox.Intersects(door.BoundingBox) && !door.Closed)
                {
                    canCollide = true;
                    if(statusCheck > 500)
                    {
                        statusCheck = 0;
                        _position = door.Enter();
                    }
                }
            }

            int multi = 5;

            if (Keyboard.GetState().IsKeyDown(Keys.W) && (canCollide || (_position.Y - moveSpeed*5) > currentRoom.Hitbox.Location.Y)) //-160
                _position.Y -= moveSpeed * multi;
            if (Keyboard.GetState().IsKeyDown(Keys.A) && (canCollide || (_position.X - moveSpeed * 5) > currentRoom.Hitbox.Location.X))//-200
                _position.X -= moveSpeed * multi;
            if (Keyboard.GetState().IsKeyDown(Keys.S) && (canCollide || (_position.Y + moveSpeed * 5) < currentRoom.Hitbox.Location.Y + currentRoom.Hitbox.Size.Y))//+270
                _position.Y += moveSpeed * multi;
            if (Keyboard.GetState().IsKeyDown(Keys.D) && (canCollide || (_position.X + moveSpeed * 5) < currentRoom.Hitbox.Location.X + currentRoom.Hitbox.Size.X))//+270
                _position.X += moveSpeed * multi;

            SetAnimations();

            _animationBodyManager.Position = _position;

            _animationHeadManager.Position = _position + new Vector2(-18,-87);

            _hitBox = new Rectangle((int)_position.X, (int)_position.Y, 50,100);

            _animationBodyManager.Update(gameTime);
            _animationHeadManager.Update(gameTime);
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, _animationBodyManager.Position, new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height), Color.White);
            else if (_animationBodyManager != null)
            {
                _animationBodyManager.Draw(spriteBatch);
                _animationHeadManager.Draw(spriteBatch);
            }
            else
                return;
        }

        protected virtual void SetAnimations()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _animationBodyManager.Play(_bodyAnimations["WalkRight"]);
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                _animationBodyManager.Play(_bodyAnimations["WalkLeft"]);
            //else if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down))
            //_animationBodyManager.Play(_bodyAnimations["WalkVertical"]);
            else
            {
                //_animationManager.Play(_bodyAnimations["WalkVertical"]);
                _animationBodyManager.Stop(0);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                _animationHeadManager.Play(_HeadAnimations["Down"]);
            //else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                //_animationBodyManager.Play(_bodyAnimations["WalkLeft"]);
            //else if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down))
            //_animationBodyManager.Play(_bodyAnimations["WalkVertical"]);
            else
            {
                //_animationManager.Play(_bodyAnimations["WalkVertical"]);
                _animationHeadManager.Stop(0);
            }
        }

        public int HP
        {
            get { return Health; }
            set { Health = value; }
        }

        public float Speed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }

        public double ATK
        {
            get { return Damage; }
            set { Damage = value; }
        }

        public float TearsRate
        {
            get { return shotsPerSec; }
            set { shotsPerSec = value; }
        }

        public double ShotSpeed
        {
            get { return BulletSpeed; }
            set { BulletSpeed = value; }
        }

        public bool InCombat
        {
            get { return combat; }
            set { combat = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value;}
        }
        public Rectangle BoundingBox
        {
            get { return _hitBox; }
            set { _hitBox = value; }
        }
    }
}
