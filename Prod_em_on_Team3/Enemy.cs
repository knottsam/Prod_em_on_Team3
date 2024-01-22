using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Prod_em_on_Team3;
using Space_invaders.Content;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Space_invaders.Content
{
    internal class Enemy : Sprite
    {
        protected Color _enemyColour;
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;

        //Slow down frame animation
        private int timeSinceLastFrame = 0;
        private int millisecondsPerFrame = 400;

        public Enemy(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }




        public bool IsDrawn { get; internal set; }


        public Enemy()
        { }

        public Enemy(Texture2D enemyTexture, Vector2 enemyposition, Rectangle boundingBox, Color enemyColour) :
            base(enemyposition, boundingBox, enemyColour)
        {
            Position = enemyposition;
            BoundingBox = boundingBox;
            _enemyColour = enemyColour;
            SpriteTexture = enemyTexture;
            IsDrawn = true; //this is wrong... this should be set and then set to false when they get hit
        }
        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                KeyboardState keystate = Keyboard.GetState();

                //Idle animation
                if (keystate.GetPressedKeys().Length == 0)
                    currentFrame++;
                timeSinceLastFrame = 0;
                if (currentFrame == 2)
                    currentFrame = 0;

                //Walking Animation
                if (keystate.IsKeyDown(Keys.Left))
                {

                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }




        public class enemyColour
        {
            public static readonly enemyColour Black = new(0, 0, 0);
            public static readonly enemyColour White = new(255, 255, 255);
            public static readonly enemyColour Red = new(255, 0, 0);
            public static readonly enemyColour Green = new(0, 255, 0);
            public static readonly enemyColour Blue = new(6, 5, 255);

            public byte R;
            public byte G;
            public byte B;

            public enemyColour(byte r, byte g, byte b)
            {
                R = r;
                G = g;
                B = b;
            }

        }


        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(SpriteTexture, Position, _enemyColour);
        }


    }
}