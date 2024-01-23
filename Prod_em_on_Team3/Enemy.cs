using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Prod_em_on_Team3;
using System;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace Prod_em_on_Team3
{
    internal class Enemy : Sprite
    {
        private int lives = 3;
        private int spriteXvalue;
        int gameState;
        private Vector2 position;
        protected int health;
        protected int speed;
        protected int radius;

        public Enemy() : base()
        {

        }


        public Enemy(Vector2 spritePosition, Rectangle spriteBox, Color spriteColour)
            : base(spritePosition, spriteBox, spriteColour)
        {
            _spriteBox = spriteBox;
            _spriteColour = spriteColour;
            _spritePosition = Position;


        }
        public int Health
        {
            get { return health; }
            set { health = value; }

        }

        public Vector2 Position
        {
            get { return position; }
        }

        public int Radius
        {
            get { return radius; }
        }

        public Enemy(Vector2 newPos)
        {
            position = newPos;
        }

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;


            Vector2 moveDir = playerPos - position;
            moveDir.Normalize();
            position += moveDir * speed * dt;
            distanceToPlayer = ;
            int state = 0;

            if (state == 0)
            {
                // Everything the enemy does during "idle"
                if (distanceToPlayer < 600)
                {
                    state = 1;
                }
            }
            else if (state == 1)
            {
                // Everything the enemy does during "following"
                Position += moveDir * speed * dt; // move enemy towards player
            }

        }




        public int Lives
        {
            get { return lives; }
            set { lives = value; }

        }


    }
}