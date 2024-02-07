using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Prod_em_on_Team3
{
    public class EnemyObj
    {
        protected Texture2D _texture;
        protected Rectangle _hitBox;
        protected AnimationManager _bodyAnimationManager;
        protected Dictionary<string, Animation> _animations;
        protected Room Parent;
        protected Sprite HeadSprite;
        protected Vector2 _position;

        private int _healthPoints;
        private int _contactDMG;
        private int _moveSpeed;
        private bool _aliveStatus = true;

        public EnemyObj() { }

        public EnemyObj(int moveSpeed, int healthPoints, int contactDMG)
        {
            this._moveSpeed = moveSpeed;
            this._healthPoints = healthPoints;
            this._contactDMG = contactDMG;
        }
        public new void LoadContent(ContentManager Content)
        {

            _hitBox = new Rectangle(0, 0, 224, 100);

            _texture = Content.Load<Texture2D>("Head - ENEMY1");
            _animations = new Dictionary<string, Animation>()
            {
                //{ "WalkVertical", new Animation(Content.Load<Texture2D>("Vertical"), 9) },
                { "WalkRight", new Animation(Content.Load<Texture2D>("Right"), 9) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("Left"), 9) },
            };
            _bodyAnimationManager = new AnimationManager(_animations.First().Value, _moveSpeed / 12);
        }

        public virtual void Update(GameTime gameTime)
        {
            Room currentRoom = RoomController.instance.currentRoom;

            bool canCollide = false;

            int multi = 5;

            if ((canCollide || (_position.Y - _moveSpeed * 5) > currentRoom.Hitbox.Location.Y))//Up
                //_position.Y -= _moveSpeed * multi;
            if ((canCollide || (_position.X - _moveSpeed * 5) > currentRoom.Hitbox.Location.X))//Left
                //_position.X -= _moveSpeed * multi;
            if ((canCollide || (_position.Y + _moveSpeed * 5) < currentRoom.Hitbox.Location.Y + currentRoom.Hitbox.Size.Y))//Down
                //_position.Y += _moveSpeed * multi;
            if ((canCollide || (_position.X + _moveSpeed * 5) < currentRoom.Hitbox.Location.X + currentRoom.Hitbox.Size.X))//Right
                //_position.X += _moveSpeed * multi;

            //SetAnimations();

            _bodyAnimationManager.Position = _position;

            _hitBox = new Rectangle((int)_position.X, (int)_position.Y, 50, 100);

            _bodyAnimationManager.Update(gameTime);
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

        public int Health
        {
            get { return _healthPoints; }
            set { _healthPoints = value; }
        }
        public int Damage
        {
            get { return _contactDMG; }
            set { _contactDMG = value; }
        }
        public bool LifeStatus
        {
            get { return _aliveStatus; }
            set { _aliveStatus = value; }
        }

    }
}
