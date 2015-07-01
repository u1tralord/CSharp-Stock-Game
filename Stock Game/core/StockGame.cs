using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Stock_Game.ui;

namespace Stock_Game.core
{
    class StockGame
    {
        bool running = true;
        Screen currentScreen;

        public void Start()
        {
            currentScreen = new StartScreen();

            while (running)
            {
                currentScreen.Draw();
                var x = Console.ReadKey();
                currentScreen.KeyPress(x);
            }
        }
    }
}
