using Microsoft.VisualBasic.Devices;
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
        private Player Player


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

        public override void Update(GameTime gameTime, bool gameStarted, int rightEdge)
        {
            if (gameStarted)
            {
                Position = Player.Position.X 
            }

        }




        public int Lives
        {
            get { return lives; }
            set { lives = value; }

        }


    }
}