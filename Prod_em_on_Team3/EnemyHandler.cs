using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tweening;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prod_em_on_Team3
{
    public class EnemyHandler
    {

        private SpriteBatch _spriteBatch;
        private ContentManager content;
        private readonly Tweener _tweener = new Tweener();
        private Camera2D _camera;

        public static EnemyHandler instance;

        public Room currentRoom;

        public void generateEntities(Room room)
        {
            currentRoom = room;
            if (room.enemies != null)
                return;
            


        }


    }
}
