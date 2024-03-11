using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Prod_em_on_Team3.Content
{
    static class Player_Manager
    {

        static void Update(GameTime gameTime, bool gameStarted, int rightEdge)
        {
            Player.Update();

        }

        static void CreatePlayer()
        {
           
        }

        // static void CreatePlayer()
        // creates an instance of the player and stores it in a variable which is contained within the manager
        // you do .Update to this player

    }
 }



