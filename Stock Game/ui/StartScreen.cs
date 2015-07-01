using System;
using System.Collections.Generic;
using System.IO;

namespace Stock_Game.ui
{
    class StartScreen : MenuScreen
    {
        //int bWidth = 30;
        //int bHeight = 8;
        //int bX;
        //int bY;
        int textXPos;

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

        }

        public void CreateAccountSelected(object sender, EventArgs e)
        {

        }

        public void DrawTitle()
        {
            Console.SetCursorPosition(textXPos, menuYPos + 1);
            Console.Write(title);
            Console.SetCursorPosition(menuXPos + 1, menuYPos + 2);
            DrawMenuLine();
        }

        public override void Draw()
        {
            base.Draw();
            DrawBorder(menuXPos, menuYPos, menuWidth, menuHeight);
            DrawTitle();

            Console.SetCursorPosition(textXPos, menuYPos + 3);
            for (int i = 0; i < options.Count; i++)
            {
                Console.Write("[{0}] {1}", options[i].Highlighted ? "*" : Convert.ToString(i+1) , options[i].OptionText);
                Console.SetCursorPosition(textXPos, Console.CursorTop + 2);
            }

            Console.SetCursorPosition(0, Console.WindowHeight-1);
        }
    }
}