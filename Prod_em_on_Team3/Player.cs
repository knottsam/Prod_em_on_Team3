using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;
using SharpDX.Direct3D9;

namespace Prod_em_on_Team3
{
    internal class Player : Sprite
    {

        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Vector2 _position;
        protected Texture2D _texture;

        //Stats
        private int Health = 6; //20 max
        private double Damage = 3;
        private double BulletSpeed = 1;
        private double shotsPerSec = 2.5;
        private float moveSpeed = 1;
        //Stats

        public Player() { }

        public Player(Vector2 Pos) 
        {
            _position = Pos;
        }

        public new void LoadContent(ContentManager Content)
        {
            _animations = new Dictionary<string, Animation>()
            {
                { "WalkRight", new Animation(Content.Load<Texture2D>("Right"), 9) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("Left"), 9) },
            };

            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _position.Y -= moveSpeed * 5;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _position.X -= moveSpeed * 5;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                    _position.Y += moveSpeed * 5;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _position.X += moveSpeed * 5;

            SetAnimations();

            _animationManager.Position = _position;
            _animationManager.Update(gameTime);
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else
                return;
        }

        protected virtual void SetAnimations()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _animationManager.Play(_animations["WalkRight"]);
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                _animationManager.Play(_animations["WalkLeft"]);
            //else if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down))
            //_animationManager.Play(_animations["WalkVertical"]);
            else
            {
                _animationManager.Stop();
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

        public double TearsRate
        {
            get { return shotsPerSec; }
            set { shotsPerSec = value; }
        }

        public double ShotSpeed
        {
            get { return BulletSpeed; }
            set { BulletSpeed = value; }
        }
    }
}
