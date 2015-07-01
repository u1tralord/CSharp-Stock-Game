using System;
using System.Collections.Generic;
using System.IO;

namespace Stock_Game.ui.screens
{
    class MainMenu : MenuScreen
    {
        public MainMenu()
        {
            title = "Main Menu";


            options = new List<MenuOption>();
			
            MenuOption sell = new MenuOption("Sell Stocks");
            sell.Highlighted = true;

            MenuOption buy = new MenuOption("Buy Stocks");
			
			MenuOption viewPortfolio = new MenuOption("View Portfolio");
			
			MenuOption goBack = new MenuOption("Go Back");
			
			options.Add(sell);
			options.Add(buy);
			options.Add(viewPortfolio);
			options.Add(goBack);

            CalculateWindowSize();
            CalculateWindowPosition();

            textXPos = menuXPos + 2;
        }

        public override void Draw()
        {
            base.Draw();
            Console.SetCursorPosition(0, Console.WindowHeight-1);
        }
    }
}