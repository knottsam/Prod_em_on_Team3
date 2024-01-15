using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Prod_em_on_Team3.Content;
using System;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace Space_invaders
{
    public class Player : Sprite
    {
        private int lives = 3;
        private int spriteXvalue;
        KeyboardState keyboard, prevkeyboard;
        int gameState;

        public Player() : base()
        {

        }


        public Player(Vector2 spritePosition, Rectangle spriteBox, Color spriteColour)
            : base(spritePosition, spriteBox, spriteColour)
        {
            _spriteBox = spriteBox;
            _spriteColour = spriteColour;
            _spritePosition = Position;
            

        }

        public override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                //up
                Position = new Vector2(Position.Y, Position.X + 3);
                // _spritePosition.Y -= 3; // _position.y = _position..y-1
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                //down
                Position = new Vector2(Position.X, Position.Y + 3);
               // _spritePosition.Y += 3
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                //left
                Position = new Vector2(Position.X, Position.Y - 3);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                //right
                Position = new Vector2(Position.Y, Position.X - 3);
                // _spritePosition.X += 3;
            }
        }


       


        public int Lives
        {
            get { return lives; }
            set { lives = value; }

        }


    }
}


