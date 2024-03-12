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
    public class Player : Sprite
    {
        private int lives = 3;
        private int spriteXvalue;
        KeyboardState keyboard, prevkeyboard;
        int gameState;
        private Vector2 vector2;
        private System.Drawing.Rectangle rectangle;
        private System.Drawing.Color color;
        private object green;

        public Player() : base()
        {

        }



        public Player(Vector2 spritePosition, Rectangle spriteBox, Color spriteColour)

        {
            _spriteBox = spriteBox;
            _spriteColour = spriteColour;
            _spritePosition = Position;


        }

        public Player(Vector2 vector2, System.Drawing.Rectangle rectangle, System.Drawing.Color color, object green)
        {
            this.vector2 = vector2;
            this.rectangle = rectangle;
            this.color = color;
            this.green = green;
        }

        public override void Update(GameTime gameTime, bool gameStarted, int rightEdge)
        {
            keyboard = Keyboard.GetState();



            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                //left
                Position = new Vector2(Position.X - 5, Position.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                //right
                Position = new Vector2(Position.X + 5, Position.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                //down
                Position = new Vector2(Position.X, Position.Y + 5);
                // _spritePosition.Y += 3
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                //up
                Position = new Vector2(Position.X, Position.Y - 5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                Position = new Vector2(Position.Y);
            }


        }





        public int Lives
        {
            get { return lives; }
            set { lives = value; }

        }


    }
}