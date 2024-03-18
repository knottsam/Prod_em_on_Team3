using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Prod_em_on_Team3
{
    public class Bullet
    {
        //
        private bool finishedFiring;
        private bool firingbullet;
        private Vector2 bulletVelocity;
        private float _gravity = 0.1f;
        private float airTime = 1000;
        private float statusCheck;
        // 
        public Bullet() { }
        public Sprite bulletSprite;


        public Bullet(ContentManager content, Vector2 Position, Vector2 Velocity, float scale)
        {
            bulletVelocity = Velocity;
            bulletSprite = new Sprite(new Vector2(Position.X + 15, Position.Y-33* (scale / 2)), new Rectangle(33,33, 33,33), Color.White, scale);
            bulletSprite.LoadContent(content, "Tear");
            firingbullet = true;
        }

        public void Update(GameTime gameTime)
        {
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