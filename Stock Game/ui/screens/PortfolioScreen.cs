using System;
using System.Collections.Generic;
using System.IO;

using Stock_Game.core;
using Stock_Game.market;

namespace Stock_Game.ui.screens
{
    public class PortfolioScreen : MenuScreen
    {
        public PortfolioScreen()
        {
            title = "Portfolio";
            options = new List<MenuOption>();
			
			MenuOption rowTitle = new MenuOption(string.Format("{0, 5}|{1, 6}|{2, 10}|{3, 10}", "SYMB", "Owned", "Value", "Total"));
			options.Add(rowTitle);
			
			foreach(string key in StockGame.Account.Stocks.Keys)
			{
				Stock stock = StockGame.GetStock(key);
				int owned = StockGame.Account.Stocks[key];
				
				MenuOption stockOption = new MenuOption(string.Format("{0, 5}|{1, 6}|{2, 10:0.00}|{3, 10:0.00}", stock.Symbol.PadLeft(3, ' '), owned.ToString(), stock.LatestTradePrice, (owned * stock.LatestTradePrice)));
				options.Add(stockOption);
			}
            CalculateWindowSize();
            CalculateWindowPosition();

            textXPos = menuXPos + 2;
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