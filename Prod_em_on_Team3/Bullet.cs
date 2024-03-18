using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using Prod_em_on_Team3.EnemySystem;

namespace Prod_em_on_Team3
{
    public class Bullet
    {
        //
        private bool finishedFiring;
        private bool firingbullet;
        private Vector2 bulletVelocity;
        private float _gravity = 0.1f;
        private float _damage = 3.5f;
        private float airTime = 1000;
        private float _pierce = 0f;
        private float statusCheck;
        private Room currentRoom;
        private List<EnemyObj> hitList;
        // 
        public Bullet() { }
        public Sprite bulletSprite;
        public Rectangle _hitbox;

        public Bullet(ContentManager content, float gravTime,Vector2 Position, Vector2 Velocity, float scale, float ATK, float pierce)
        {
            _damage = ATK;
            airTime = gravTime;
            bulletVelocity = Velocity;
            bulletSprite = new Sprite(new Vector2(Position.X + 15, Position.Y-33* (scale / 2)), null, Color.White, scale);
            bulletSprite.LoadContent(content, "Tear");
            firingbullet = true;
            _hitbox = new Rectangle((int)bulletSprite.Position.X, (int)bulletSprite.Position.Y, (int)(33*scale), (int)(33*scale));
            hitList = new List<EnemyObj>();

            _pierce = pierce;
        }

        
        public void Update(GameTime gameTime)
        {
            currentRoom = RoomController.instance.currentRoom; //Player's Current Room, Object Type : [Room]

            statusCheck += gameTime.ElapsedGameTime.Milliseconds;
            if (statusCheck > airTime) 
            {
                _gravity = (statusCheck - airTime)/100;
            }
            else
            {
                _gravity = 0.2f;
            }
            if (_gravity >= 5f)
            {
                finishedFiring = true;
                //Put in animated tear burst here
            }
            bulletSprite.Position += (bulletVelocity + new Vector2(0,_gravity));
            _hitbox.Y = (int)bulletSprite.Position.Y;
            _hitbox.X = (int)bulletSprite.Position.X;

            foreach (EnemyObj enemy in currentRoom.enemies)
            {
                if (_hitbox.Intersects(enemy.Hitbox) && !hitList.Contains(enemy))
                {
                    enemy.Health -= _damage;
                    hitList.Add(enemy);
                    _pierce -= 1f;
                }
            }

            if (_pierce <= 0)
            {
                finishedFiring = true;
            }

        }

        public bool IsFinished
        {
            get { return finishedFiring; }
            set { finishedFiring = value; }
        }

        public bool Firing
        {
            get { return firingbullet; }
        }

        public Sprite Sprite
        {
            get { return bulletSprite; }
        }
    }
}