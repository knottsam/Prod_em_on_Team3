using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;

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

        //Stats
        private int Health = 6; //20 max
        private double Damage = 3;
        private double BulletSpeed = 1;
        private float shotsPerSec = 2.5f;
        private float moveSpeed = 1f;
        private bool combat = false;
        //Stats

        public static Player _player;

        public new void LoadContent(ContentManager Content)
        {

            _hitBox = new Rectangle(0,0, 224, 160);
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
            if (Keyboard.GetState().IsKeyDown(Keys.W) && (_position.Y - 160 > currentRoom.Y*currentRoom.Height || false )) //Change false to (if door is there)
                _position.Y -= moveSpeed * 5;
            if (Keyboard.GetState().IsKeyDown(Keys.A) && (_position.X - 200 > currentRoom.X * currentRoom.Width || false))
                _position.X -= moveSpeed * 5;
            if (Keyboard.GetState().IsKeyDown(Keys.S) && (_position.Y + 270 < currentRoom.Y+1 * currentRoom.Height || false))
                _position.Y += moveSpeed * 5;
            if (Keyboard.GetState().IsKeyDown(Keys.D) && (_position.X + 270 < currentRoom.X+1 * currentRoom.Width || false))
                _position.X += moveSpeed * 5;

            SetAnimations();

            _animationBodyManager.Position = _position;

            _animationHeadManager.Position = _position + new Vector2(-18,-87);

            _hitBox = new Rectangle((int)_position.X, (int)_position.Y, 224,160);

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
                _animationBodyManager.Stop();
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
                _animationHeadManager.Stop();
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

        public Rectangle BoundingBox
        {
            get { return _hitBox; }
            set { _hitBox = value; }
        }
    }
}
