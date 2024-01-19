using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prod_em_on_Team3
{
    internal class Tile
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Color _color;
        public string _type;
        public Rectangle _bounds;
        public bool collidable;
        private bool selected;
        public bool Selected
        {
            get => selected; set => selected = value;
        }
        public Tile() { }

        public Tile(Texture2D tileTexture, Vector2 tilePosition, Color tileColour, string tileType, int tileWidth, int tileHeight)
        {
            _texture = tileTexture;
            _position = tilePosition;
            _color = tileColour;
            _type = tileType;
            _bounds = new Rectangle((int)_position.X, (int)_position.Y, tileWidth, tileHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _bounds, _color);
        }
    }
}