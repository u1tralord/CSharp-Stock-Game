/**
* TODO:
* MenuScreen Scrolling fix
* Analyze Stock Screen
* Create Account Screen
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Stock_Game.ui;
using Stock_Game.ui.screens;
using Stock_Game.market;

namespace Stock_Game.core
{
    public class StockGame
    {
        private bool running = true;
		private List<Screen> previousScreens = new List<Screen>();
		
		private Profile profile;
		private Dictionary<string, Stock> stockCache = new Dictionary<string, Stock>();
		private Screen currentScreen;
		
        public void Start()
        {	
            currentScreen = new StartScreen();

            while (running)
            {
				DrawScreen();
                ConsoleKeyInfo x;
                do{
                    x = Console.ReadKey();
                }while(!currentScreen.KeyPress(x));
            }
        }
		
		public void DrawScreen(){
			SetConsoleColors(ConsoleColor.White, ConsoleColor.Black);
			currentScreen.Draw();
		}
		
		public void SetConsoleColors(ConsoleColor foreground, ConsoleColor background){
			Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
		}
		
		public void CoolLoad(){
			for(int x = 0; x < Console.WindowWidth; x++)
				for(int y = 0; y < Console.WindowHeight; y++){
					Console.SetCursorPosition(x,y);
					Console.Write("|");
				}
			Console.SetCursorPosition(0, 0);
		}
		public void GoBack(){
			ChangeScreen(PreviousScreens[PreviousScreens.Count - 1]);
		}
		
		public void ChangeScreen(Screen newScreen){
			previousScreens.Add(currentScreen);
			currentScreen = newScreen;
			CoolLoad();
		}
		
		public Stock GetStock(string stockSymbol){
			if(!stockCache.ContainsKey(stockSymbol) && stockSymbol.Length == 4)
				stockCache.Add(stockSymbol, new Stock(stockSymbol));
			
			if(stockCache.ContainsKey(stockSymbol) && stockCache[stockSymbol].TimeSinceUpdate > 5000)
				stockCache[stockSymbol].Update();
			
			return stockCache[stockSymbol];
		}
		
		public double GetStockValue(string stockSymbol){
			return GetStock(stockSymbol).LatestTradePrice;
		}
		
		public bool Running{
			get{ return running; }
			set{ running = value; }
		}
		
		public Profile Account{
			get{ return profile; }
			set{
				profile = value; 
				foreach(string key in profile.Stocks.Keys){
					if(!stockCache.ContainsKey(key))
						stockCache.Add(key, new Stock(key));
				}
			}
		}
		
		public Screen CurrentScreen{
			get{ return currentScreen; }
			set{ currentScreen = value; }
		}
		
		public List<Screen> PreviousScreens{
			get{ return previousScreens; }
		}
    }
}
