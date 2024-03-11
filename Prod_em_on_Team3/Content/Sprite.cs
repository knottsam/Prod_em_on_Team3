using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms.VisualStyles;

namespace Prod_em_on_Team3
{
    public class Sprite
    {
        protected Texture2D _spritetexture;
        protected Vector2 _spritePosition;
        protected Rectangle _spriteBox;
        protected Color _spriteColour;
        public Vector2 _SpritePosition
        {
            get { return _spritePosition; }
            set { _spritePosition = value; }
        }
 
        public Sprite()
        { }

        public Sprite(Vector2 spritePosition, Rectangle boundingBox, Color spriteColour)
        {
            _spritePosition = spritePosition;
            _spriteBox = boundingBox;
            _spriteColour = spriteColour;
        }
        public void LoadContent(ContentManager mycontent, string textureName)
        {
            mycontent.RootDirectory = "Content";
            _spritetexture = mycontent.Load<Texture2D>(textureName);

        }
        public virtual void Update(GameTime gameTime, bool gameStarted, int rightEdge)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, Position, _spriteColour);
        }
        public Vector2 Position
        {
            get { return _spritePosition; }
            set { _spritePosition = value; }
        }
        public Texture2D SpriteTexture
        {
            get { return _spritetexture; }
            set { _spritetexture = value; }
        }
        public Rectangle BoundingBox
        {
            get { return _spriteBox; }
            set { _spriteBox = value; }

        }
    }
}
