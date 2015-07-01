using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Game
{
    class StockGame
    {
        bool running = true;
        Screen currentScreen;

        public void Start()
        {
            currentScreen = new LoginScreen();

            while (running)
            {
                currentScreen.Draw();
                var x = Console.ReadKey();
                currentScreen.KeyPress(x);
            }
        }
    }
}
