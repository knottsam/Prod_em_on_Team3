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
        protected float statusCheck;
        protected ContentManager _content;

        //Stats
        private int Health = 6; //20 max
        private double Damage = 3;
        private float BulletSpeed = 1f;
        private float shotsPerSec = 2.73f;
        private float moveSpeed = 1f;
        private float range = 6.5f;
        private int gold = 0;
        private int bombs = 1;
        private int keys = 0;
        private bool combat = false;
        private float bulletSize = 1f;
        //Stats

        private Vector2 playerVelocity;
        //
        private float cooldownCheck = 0;
        private List<Bullet> existingBullets = new List<Bullet>();

        public static Player instance;

        public void LoadContent(ContentManager Content)
        {
            instance = this;
            _content = Content;
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

            _animationHeadManager = new AnimationManager(_HeadAnimations.First().Value, 0.6f/shotsPerSec);
            _animationBodyManager = new AnimationManager(_bodyAnimations.First().Value, moveSpeed/12);
        }

        public virtual void Update(GameTime gameTime)
        {
            Room currentRoom = RoomController.instance.currentRoom;

            bool canCollide = false;

            statusCheck += gameTime.ElapsedGameTime.Milliseconds;

            foreach (Door door in currentRoom.roomDoors)
            {
                if (_hitBox.Intersects(door.BoundingBox) && !door.Closed)
                {
                    canCollide = true;
                    if (statusCheck > 500)
                    {
                        statusCheck = 0;
                        _position = door.Enter();
                    }
                }
            }

            playerVelocity = new Vector2(0, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.W) && (canCollide || (_position.Y - moveSpeed * 5) > currentRoom.Hitbox.Location.Y)) //-160
            {
                playerVelocity += new Vector2(0,-1) * 5;
                _position.Y -= moveSpeed * 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && (canCollide || (_position.X - moveSpeed * 5) > currentRoom.Hitbox.Location.X))//-200
            {
                playerVelocity += new Vector2(-1,0) * 5;
                _position.X -= moveSpeed * 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && (canCollide || (_position.Y + moveSpeed * 5) < currentRoom.Hitbox.Location.Y + currentRoom.Hitbox.Size.Y))//+270
            {
                playerVelocity += new Vector2(0,1) * 5;
                _position.Y += moveSpeed * 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && (canCollide || (_position.X + moveSpeed * 5) < currentRoom.Hitbox.Location.X + currentRoom.Hitbox.Size.X))//+270
            {
                playerVelocity += new Vector2(1,0) * 5;
                _position.X += moveSpeed * 5;
            }

            cooldownCheck += gameTime.ElapsedGameTime.Milliseconds;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                FireBullet(new Vector2(-1, 0), gameTime);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                FireBullet(new Vector2(1, 0), gameTime);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                FireBullet(new Vector2(0, -1), gameTime);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                FireBullet(new Vector2(0, 1), gameTime);
            }



            SetAnimations();

            _animationBodyManager.Position = _position;

            _animationHeadManager.Position = _animationBodyManager.Position + new Vector2(-18,-87);

            _hitBox = new Rectangle((int)_position.X, (int)_position.Y, 50,100);

            for (int i = 0; i < existingBullets.Count; i++)
            {
                existingBullets[i].Update(gameTime);
                if (existingBullets[i].Sprite.Position.X < (currentRoom.X * currentRoom.Width) || existingBullets[i].Sprite.Position.X > (currentRoom.X * currentRoom.Width) + currentRoom.Width ||
                    existingBullets[i].Sprite.Position.Y < (currentRoom.Y * currentRoom.Height) || existingBullets[i].Sprite.Position.Y > (currentRoom.Y * currentRoom.Height) + currentRoom.Height || existingBullets[i].IsFinished)
                {
                    existingBullets.Remove(existingBullets[i]);
                }
            }

            _animationBodyManager.Update(gameTime);
            _animationHeadManager.Update(gameTime);
        }

        public void FireBullet(Vector2 Direction, GameTime gameTime)
        {
            if (cooldownCheck >= (1 / shotsPerSec) * 1000)
            {
                cooldownCheck = 0;
                Direction *= (BulletSpeed*10);
                if (!(playerVelocity.Y > 0 && Direction.Y < 0) && !(playerVelocity.Y < 0 && Direction.Y > 0))
                {
                    Direction.Y += playerVelocity.Y/2;
                }
                if (!(playerVelocity.X > 0 && Direction.X < 0) && !(playerVelocity.X < 0 && Direction.X > 0))
                {
                    Direction.X += playerVelocity.X/2;
                }
                Bullet tempBullet = new Bullet(_content, (BulletSpeed*range)*150,_position, Direction, bulletSize);
                existingBullets.Add(tempBullet);

            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_animationBodyManager != null)
            {
                _animationBodyManager.Draw(spriteBatch);
                _animationHeadManager.Draw(spriteBatch);
            }
            else
                return;

            foreach (Bullet bullet in existingBullets)
                if (bullet.Firing)
                    bullet.Sprite.Draw(spriteBatch);
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
            { 

            }
                //_animationHeadManager.Play(_HeadAnimations["Down"]);
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
        public float Range
        {
            get { return range; }
            set { range = value; }
        }
        public float ShotSpeed
        {
            get { return BulletSpeed; }
            set { BulletSpeed = value; }
        }

        public int Money
        {
            get { return gold; }
            set { gold = value; }
        }

        public int Bombs
        {
            get { return bombs; }
            set { bombs = value; }
        }

        public int Key
        {
            get { return keys; }
            set { keys = value; }
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
