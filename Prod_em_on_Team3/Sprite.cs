﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Prod_em_on_Team3;
using SharpDX.Direct3D9;
using System.Diagnostics;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Prod_em_on_Team3
{
    internal class Sprite : Game1
    {
        private Texture2D _spriteTexture;
        private Vector2 _spritePosition;
        private Rectangle _boundingBox;
        private Color _spriteColor;
        public Sprite() { }
        public Sprite(Vector2 spritePosition, Rectangle boundingBox, Color spriteColor)
        {
            _spritePosition = spritePosition;
            _boundingBox = boundingBox;
            _spriteColor = spriteColor;
        }

        public virtual void LoadContent(ContentManager contentManager, SpriteBatch spriteBatch, string type, string TextureName)
        {
            contentManager.RootDirectory = "Content";

            _spriteTexture = contentManager.Load<Texture2D>(TextureName);
            _spritePosition = new Vector2(_spritePosition.X - (_spriteTexture.Width / 2), _spritePosition.Y - _spriteTexture.Height);
        }

        public virtual void Update(GameTime gameTime, bool gameStarted, int rightEdge)
        {


        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteTexture, _spritePosition, null, _spriteColor);
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