using System;
using System.Collections.Generic;
using System.IO;

using Stock_Game.core;

namespace Stock_Game.ui.screens
{
    class StartScreen : MenuScreen
    {
        public StartScreen()
        {
            title = "Login or create account";


            options = new List<MenuOption>();
            MenuOption login = new MenuOption("Login");
            login.Highlighted = true;
            login.OptionSelected += LoginSelected;

            MenuOption createAcct = new MenuOption("Create Account");
            createAcct.OptionSelected += CreateAccountSelected;
			
			MenuOption goBack = new MenuOption("Go Back");
            goBack.OptionSelected += GoBackSelected;

            options.Add(login);
            options.Add(createAcct);
			options.Add(goBack);

            CalculateWindowSize();
            CalculateWindowPosition();

            textXPos = menuXPos + 2;
        }

        public void LoginSelected(object sender, EventArgs e)
        {
			
        }

        public void CreateAccountSelected(object sender, EventArgs e)
        {
			
        }
		
		public void GoBackSelected(object sender, EventArgs e)
        {
			
        }

        public override void Draw()
        {
            base.Draw();
            Console.SetCursorPosition(0, Console.WindowHeight-1);
        }
    }
}