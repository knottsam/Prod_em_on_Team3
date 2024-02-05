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
        private Player Player;
        KeyboardState keyboard, prevkeyboard;
        private bool enemyfired;
        private Sprite _ownerSprite;


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
        public override void Update(GameTime gameTime, bool gameStarted, int rightEdge)
        {

            keyboard = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.N))
            {

                enemyfired = true;
            }
            if (Position.Y <= 0)
            {
                enemyfired = false;
               
            }
            if (Position.Y > 0 && enemyfired)
            {
                Position = new Vector2(Position.X, Position.Y - 2);
            }
           


            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //{
            //    //left
            //    Position = new Vector2(Position.X - 10, Position.Y);
            //}
            //else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //{
            //    //right
            //    Position = new Vector2(Position.X + 10, Position.Y);
            //}



        }


        public Sprite Owner
        {
            get { return _ownerSprite; }
            set { _ownerSprite = value; }
        }

    }
}