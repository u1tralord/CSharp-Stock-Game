using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Stock_Game.core;
using Stock_Game.ui;

namespace Stock_Game
{
    class Launcher
    {
		public static StockGame stockGame;
		
        static void Main(string[] args)
        {
            stockGame = new StockGame();
            stockGame.Start();
        }
    }
}
