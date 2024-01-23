using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Prod_em_on_Team3;
using SharpDX.Direct3D9;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Prod_em_on_Team3
{
    public class Sprite : Game1
    {

        private Texture2D _spriteTexture;
        private Vector2 _spritePosition;
        private Rectangle _boundingBox;
        private Color _spriteColor;
        private bool _Loaded;
        public Sprite() { }
        public Sprite(Vector2 spritePosition, Rectangle boundingBox, Color spriteColor)
        {
            _spritePosition = spritePosition;
            _boundingBox = boundingBox;
            _spriteColor = spriteColor;
        }

        public virtual bool LoadContent(ContentManager contentManager, SpriteBatch spriteBatch, string TextureName)
        {
            contentManager.RootDirectory = "Content";

            _spriteTexture = contentManager.Load<Texture2D>(TextureName);
            if(_spriteTexture != null ) 
                _spritePosition = new Vector2(_spritePosition.X - (_spriteTexture.Width / 2), _spritePosition.Y - _spriteTexture.Height);

            _Loaded = true;

            return _Loaded;
        }

        public virtual void Update(GameTime gameTime, int rightEdge)
        {


        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteTexture, _spritePosition, _boundingBox, _spriteColor);
        }

        public Vector2 Position
        {
            get { return _spritePosition; }
            set { _spritePosition = value; }
        }
        public Rectangle BoundingBox
        {
            get { return _boundingBox; }
            set { _boundingBox = value; }
        }
        public Texture2D SpriteTexture
        {
            get { return _spriteTexture; }
            set { _spriteTexture = value; }
        }
    }
}
