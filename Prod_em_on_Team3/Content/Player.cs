using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Prod_em_on_Team3
{
    public class Player : Sprite
    {
        private int lives = 3;
        public Player() : base()
        {

        }
        public Player(Vector2 spritePosition, Rectangle spriteBox, Color spriteColour)
            : base(spritePosition, spriteBox, spriteColour)
        {
            _spriteBox = spriteBox;
            _spriteColour = spriteColour;
            _spritePosition = spritePosition;
        }

        public override void Update(GameTime gameTime, bool gamestarted, int rightedge)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                //left
                Position = new Vector2(Position.X - 3, Position.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //right
                Position = new Vector2(Position.X + 3, Position.Y);
            }


        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }

        }

    }
}


