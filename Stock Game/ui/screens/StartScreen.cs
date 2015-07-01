using System;
using System.Collections.Generic;
using System.IO;

using Stock_Game.core;

namespace Stock_Game.ui.screens
{
    public class StartScreen : MenuScreen
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
			

            options.Add(login);
            options.Add(createAcct);

            CalculateWindowSize();
            CalculateWindowPosition();

            textXPos = menuXPos + 2;
        }

        public void LoginSelected(object sender, EventArgs e)
        {
			Profile p = new Profile(@"profiles\Jacob.profile");
			StockGame.Account = p;
			StockGame.ChangeScreen(new MainMenu(), this);
        }
        public void CreateAccountSelected(object sender, EventArgs e)
        {
			Profile p = new Profile("Jacob", Cryptography.GetHash("PASSWORD"), 100000);
			p.Save();
        }

        public override void Draw()
        {
            base.Draw();
            Console.SetCursorPosition(0, Console.WindowHeight-1);
        }
    }
}