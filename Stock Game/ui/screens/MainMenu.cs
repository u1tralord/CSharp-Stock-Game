using System;
using System.Collections.Generic;
using System.IO;

using Stock_Game.core;

namespace Stock_Game.ui.screens
{
    public class MainMenu : MenuScreen
    {
        public MainMenu()
        {
            title = "Main Menu";


            options = new List<MenuOption>();
			
			MenuOption buy = new MenuOption("Buy Stocks");
			buy.OptionSelected += BuySelected;
			buy.Highlighted = true;
			
            MenuOption sell = new MenuOption("Sell Stocks");
			sell.OptionSelected += SellSelected;

            MenuOption lookup = new MenuOption("Lookup Stock");
            lookup.OptionSelected += LookupSelected;

            MenuOption compare = new MenuOption("Compare Stocks");
            compare.OptionSelected += CompareSelected;

			MenuOption viewPortfolio = new MenuOption("View Portfolio");
			viewPortfolio.OptionSelected += PortfolioSelected;
			
			MenuOption goBack = new MenuOption("Go Back");
			goBack.OptionSelected += GoBackSelected;
			
			options.Add(buy);
			options.Add(sell);
            options.Add(lookup);
            options.Add(compare);
			options.Add(viewPortfolio);
			options.Add(goBack);

            CalculateWindowSize();
            CalculateWindowPosition();

            textXPos = menuXPos + 2;
        }
		
		public void BuySelected(object sender, EventArgs e)
        {
			Launcher.stockGame.ChangeScreen(new BuyForm());
        }
		
		public void SellSelected(object sender, EventArgs e)
        {
			Launcher.stockGame.ChangeScreen(new SellForm());
        }

        public void LookupSelected(object sender, EventArgs e)
        {
			Launcher.stockGame.ChangeScreen(new AnalyzeForm());
        }
        public void CompareSelected(object sender, EventArgs e)
        {

        }

		public void PortfolioSelected(object sender, EventArgs e)
        {
			Launcher.stockGame.ChangeScreen(new PortfolioScreen());
        }
		
		public void GoBackSelected(object sender, EventArgs e)
        {
			Launcher.stockGame.GoBack();
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