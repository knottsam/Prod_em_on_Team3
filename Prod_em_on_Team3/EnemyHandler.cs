using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prod_em_on_Team3
{
    static class EnemyHandler
    {
        public static List<EnemyObj> InitialiseSprites(GraphicsDeviceManager _graphics, ContentManager Content, SpriteBatch spriteBatch, int types)
        {
            List<EnemyObj> enemyTypes = new();

            for (int i = 0; i < types;)
            {
                Sprite headSprite = new Sprite();
                Sprite bodySprite = new Sprite();

                headSprite.LoadContent(Content, spriteBatch, "Head - ENEMY" + i);
                bodySprite.LoadContent(Content, spriteBatch, "Movement - Stood Still");

                EnemyObj enemy = new EnemyObj(headSprite, bodySprite, 12, 1);

                enemyTypes.Add(enemy);
            }



            return enemyTypes;
        }

        public static void SpawnEnemies()
        {

        }

    }
}
