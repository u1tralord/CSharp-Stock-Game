/**
* TODO:
* MenuScreen Scrolling fix
* Buy Stock Screen
* Sell Stock Screen
* View Portfolio Screen
* Analyze Stock Screen
* Login Screen
* Create Account Screen
*/
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
    public class StockGame
    {
		
        private static bool running = true;
		private static List<Screen> previousScreens = new List<Screen>();
		
		private static Profile profile;
		private static Dictionary<string, Stock> stockCache = new Dictionary<string, Stock>();
		private static Screen currentScreen;
		
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
		
		public static void ChangeScreen(Screen newScreen, Screen oldScreen){
			currentScreen = newScreen;
			previousScreens.Add(oldScreen);
		}
		
		public static Stock GetStock(string stockSymbol){
			if(!stockCache.ContainsKey(stockSymbol) && stockSymbol.Length == 4)
				stockCache.Add(stockSymbol, new Stock(stockSymbol));
			
			return stockCache[stockSymbol];
		}
		
		public static double GetStockValue(string stockSymbol){
			return GetStock(stockSymbol).LatestTradePrice;
		}
		
		public static bool Running{
			get{ return running; }
			set{ running = value; }
		}
		
		public static Profile Account{
			get{ return profile; }
			set{
				profile = value; 
				foreach(string key in profile.Stocks.Keys){
					if(!stockCache.ContainsKey(key))
						stockCache.Add(key, new Stock(key));
				}
			}
		}
		
		public static Screen CurrentScreen{
			get{ return currentScreen; }
			set{ currentScreen = value; }
		}
		
		public static List<Screen> PreviousScreens{
			get{ return previousScreens; }
		}
    }
}
