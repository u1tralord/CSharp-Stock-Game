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
			
			MenuOption viewPortfolio = new MenuOption("View Portfolio");
			viewPortfolio.OptionSelected += PortfolioSelected;
			
			MenuOption goBack = new MenuOption("Go Back");
			goBack.OptionSelected += GoBackSelected;
			
			options.Add(buy);
			options.Add(sell);
			options.Add(viewPortfolio);
			options.Add(goBack);

			for(int i = 0; i < 10; i++)
			{
				options.Add(new MenuOption("Option #"+i));
			}
            CalculateWindowSize();
            CalculateWindowPosition();

            textXPos = menuXPos + 2;
        }
		
		public void BuySelected(object sender, EventArgs e)
        {
			
        }
		
		public void SellSelected(object sender, EventArgs e)
        {
			
        }
		
		public void PortfolioSelected(object sender, EventArgs e)
        {
			
        }
		
		public void GoBackSelected(object sender, EventArgs e)
        {
			StockGame.ChangeScreen(StockGame.PreviousScreens[StockGame.PreviousScreens.Count - 1], this);
        }

        public override void Draw()
        {
            base.Draw();
            Console.SetCursorPosition(0, Console.WindowHeight-1);
        }
    }
}