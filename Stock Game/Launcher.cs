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
		public const string GAME_NAME = "Stock Game";
		public const string VERSION = "Version: v0.01";
		public static StockGame stockGame;
		
        static void Main(string[] args)
        {
			Console.Title = GAME_NAME + " | " + VERSION;
            stockGame = new StockGame();
            stockGame.Start();
        }
    }
}
