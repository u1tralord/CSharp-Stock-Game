using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Stock_Game.ui;
using Stock_Game.ui.screens;
using Stock_Game.market;

namespace Stock_Game.core
{
    class StockGame
    {
        bool running = true;
        Screen currentScreen;

        public void Start()
        {
            currentScreen = new StartScreen();
			//Stock google = new Stock("GOOG");

            while (running)
            {
                currentScreen.Draw();
                var x = Console.ReadKey();
                currentScreen.KeyPress(x);
            }
        }
    }
}
