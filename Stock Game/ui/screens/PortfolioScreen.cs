using System;
using System.Collections.Generic;
using System.IO;

using Stock_Game.core;
using Stock_Game.market;
using Stock_Game.ui;

namespace Stock_Game.ui.screens
{
    public class PortfolioScreen : MenuScreen
    {
		List<string> keys = new List<string>();
        public PortfolioScreen()
        {
            title = "Portfolio";
            options = new List<MenuOption>();
			MenuOption rowTitle = new MenuOption(string.Format("{0, 5}|{1, 6}|{2, 10}|{3, 10}", "SYMB", "Owned", "Value", "Total"));
			options.Add(rowTitle);
			
			bool firstItem = true;
			foreach(string key in Launcher.stockGame.Account.Stocks.Keys)
			{
				Stock stock = Launcher.stockGame.GetStock(key);
				int owned = Launcher.stockGame.Account.Stocks[key];
				
				keys.Add(key);
				
				MenuOption stockOption = new MenuOption(string.Format("{0, 5}|{1, 6}|{2, 10:0.00}|{3, 10:0.00}", stock.Symbol.PadLeft(3, ' ').ToUpper(), owned.ToString(), stock.LatestTradePrice, (owned * stock.LatestTradePrice)));
				stockOption.Highlighted = firstItem;
				stockOption.OptionSelected += StockSelected;
				options.Add(stockOption);
				firstItem = false;
			}
            CalculateWindowSize();
            CalculateWindowPosition();
			
            textXPos = menuXPos + 2;
        }
		
		public void StockSelected(object sender, EventArgs e)
        {
			EnterAction();
        }
		
		public override void EnterAction(){
			int nowHighlighted = GetHighlighted();
			if(nowHighlighted > 0 && nowHighlighted < keys.Count){
				string stockSymbol = keys[nowHighlighted-1];
				Launcher.stockGame.ChangeScreen(new AnalyzeForm(stockSymbol));
			}
		}
		
		public override bool KeyPress(ConsoleKeyInfo key){
			if (key.KeyChar == 'b')
            {
				int nowHighlighted = GetHighlighted();
				if(nowHighlighted > 0 && nowHighlighted < keys.Count){
					string stockSymbol = keys[nowHighlighted-1];
					Launcher.stockGame.ChangeScreen(new BuyForm(stockSymbol));
				}
                return true;
            }
			if (key.KeyChar == 's')
            {
				int nowHighlighted = GetHighlighted();
				if(nowHighlighted > 0 && nowHighlighted < keys.Count){
					string stockSymbol = keys[nowHighlighted-1];
					Launcher.stockGame.ChangeScreen(new SellForm(stockSymbol));
				}
                return true;
            }
			return base.KeyPress(key);
		}

        public override void Draw()
        {
            base.Draw(); 
			Console.SetCursorPosition(1, 1);
			Console.Write("Balance: {0} Total Stock Worth: {1}", Launcher.stockGame.Account.Balance, Launcher.stockGame.Account.TotalStockWorth);
			
            Console.SetCursorPosition(0, Console.WindowHeight-1);
        }
    }
}