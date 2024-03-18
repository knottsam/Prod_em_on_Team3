using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Threading;

namespace Prod_em_on_Team3.EnemySystem
{
    public enum EnemyState
    {
        Still,

        Follow,

        Die,
    };

    public class EnemyController
    {
        Player player;
        public static EnemyController instance;
        public EnemyState currState = EnemyState.Still;
        private Room currRoom;
        //
        public float range;
        public float speed;
        private bool dead = false;
        //

        public void LoadContent()
        {
            player = Player.instance;
            instance = this;
        }


        public void GenerateEntities(Room inRoom, ContentManager content)
        {
            currRoom = inRoom;

            if (currRoom.enemies.Count > 0 ) 
            {
                return;
            }

            for (int i = -1; i <= 1; i++)
            {
                EnemyObj newEnemy = new EnemyObj(0.6f, 10.5f, 1, new Vector2((inRoom.GetRoomCenter() + new Vector2(900, 0)).X + (120 * i), (inRoom.GetRoomCenter() + new Vector2(0, 580)).Y));
                newEnemy.LoadContent(content);

                inRoom.enemies.Add(newEnemy);
            }

        }
        public void RemoveEntities()
        {
            currRoom.enemies.Clear();
        }

        public void Update(GameTime gameTime)
        {
            currRoom = RoomController.instance.currentRoom;
            switch (currState)
            {
                case (EnemyState.Still):
                    break;
                case (EnemyState.Follow):
                    Follow();
                    break;
                case (EnemyState.Die):
                    break;
            }

            if(IsPlayerInRange(range) &&  currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Still;
            }
            foreach (EnemyObj enemy in currRoom.enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (EnemyObj enemy in currRoom.enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

        private bool IsPlayerInRange(float range)
        {
            return Vector2.Distance(new Vector2(), player.Position) <= range;
        }

        void Follow()
        {

        }
    }
}
