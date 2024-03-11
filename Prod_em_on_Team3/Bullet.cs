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
        private Sprite _playerSprite;
        private bool finishedFiring;
        private bool firingbullet;
        private Vector2 bulletVelocity;
        // 
        public Bullet() { }
        public Sprite bulletSprite;


        public Bullet(ContentManager content, Texture2D bulletTexture, Sprite playerSprite, Vector2 Velocity)
        {
            bulletVelocity = Velocity;
            bulletSprite = new Sprite(bulletTexture, new Vector2(playerSprite.Position.X, playerSprite.Position.Y), new Rectangle(), Color.White);
            firingbullet = true;
            bulletSprite.LoadContent();
            _playerSprite = playerSprite;
        }

        public void Update(GameTime gameTime, int screenWidth, int screenHeight)
        {
            bulletSprite.Position += bulletVelocity;
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