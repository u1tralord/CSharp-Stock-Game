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
			foreach(string key in StockGame.Account.Stocks.Keys)
			{
				Stock stock = StockGame.GetStock(key);
				int owned = StockGame.Account.Stocks[key];
				
				keys.Add(key);
				
				MenuOption stockOption = new MenuOption(string.Format("{0, 5}|{1, 6}|{2, 10:0.00}|{3, 10:0.00}", stock.Symbol.PadLeft(3, ' ').ToUpper(), owned.ToString(), stock.LatestTradePrice, (owned * stock.LatestTradePrice)));
				stockOption.Highlighted = firstItem;
				options.Add(stockOption);
				firstItem = false;
			}
            CalculateWindowSize();
            CalculateWindowPosition();
			
            textXPos = menuXPos + 2;
        }
		
		public override void EnterAction(){
			int nowHighlighted = GetHighlighted();
			if(nowHighlighted > 0 && nowHighlighted < keys.Count){
				string stockSymbol = keys[nowHighlighted];
				StockGame.ChangeScreen(new AnalyzeForm(stockSymbol));
			}
		}

        public override void Draw()
        {
            base.Draw(); 
			Console.SetCursorPosition(1, 1);
			Console.Write("Balance: {0} Total Stock Worth: {1}", StockGame.Account.Balance, StockGame.Account.TotalStockWorth);
			
            Console.SetCursorPosition(0, Console.WindowHeight-1);
        }
    }
}