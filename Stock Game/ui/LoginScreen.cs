using System;
using System.Collections.Generic;
using System.IO;

namespace Stock_Game
{
    class LoginScreen : MenuScreen
    {
        int bWidth = 30;
        int bHeight = 8;
        int bX;
        int bY;
        int textXPos;

        public LoginScreen()
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

            bX = Console.WindowWidth / 2 - bWidth / 2;
            bY = Console.WindowHeight / 2 - bHeight / 2;
            textXPos = bX + 2;
        }

        public void LoginSelected(object sender, EventArgs e)
        {

        }

        public void CreateAccountSelected(object sender, EventArgs e)
        {

        }

        public void DrawTitle()
        {
            Console.SetCursorPosition(textXPos, bY + 1);
            Console.Write(title);
            Console.SetCursorPosition(bX + 1, bY + 2);
            for (int i = 0; i < bWidth - 1; i++) Console.Write("*");
        }

        public override void Draw()
        {
            base.Draw();
            DrawBorder(bX, bY, bWidth, bHeight);
            DrawTitle();

            Console.SetCursorPosition(textXPos, bY + 3);
            for (int i = 0; i < options.Count; i++)
            {
                Console.Write("[{0}] {1}", options[i].Highlighted ? "*" : Convert.ToString(i+1) , options[i].OptionText);
                Console.SetCursorPosition(textXPos, Console.CursorTop + 2);
            }

            /*Console.SetCursorPosition(bX+2, bY+3);
            Console.Write("1) Login");
            Console.SetCursorPosition(bX+2, bY+5);
            Console.Write("2) Create Account");*/
        }
    }
}