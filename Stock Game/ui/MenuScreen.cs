using System;
using System.Collections.Generic;

namespace Stock_Game.ui
{
    abstract class MenuScreen : Screen
    {
        public int menuWidth;
        public int menuHeight;
        public int menuXPos;
        public int menuYPos;
		public int textXPos;
		
        public List<MenuOption> options;
        public string title;

        public virtual void Draw()
        {
            Console.Clear();
            DrawBorder(0, 0, Console.WindowWidth, Console.WindowHeight - 1);
			
			if (menuWidth != default(int) && menuHeight != default(int) && menuXPos != default(int) && menuYPos != default(int))
				DrawBorder(menuXPos, menuYPos, menuWidth, menuHeight);
			if(!String.IsNullOrEmpty(title))
				DrawTitle();
			if(options != null)
				DrawOptions();
        }

        public static void DrawBorder(int xPos, int yPos, int w, int h)
        {
            Console.SetCursorPosition(xPos, yPos);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
                    {
                        Console.Write("*");
                    }
                    else
                        Console.Write(" ");
                }
                Console.SetCursorPosition(xPos, yPos + y + 1);
            }
        }
		
		public void DrawTitle()
        {
            Console.SetCursorPosition(textXPos, menuYPos + 1);
            Console.Write(title);
            Console.SetCursorPosition(menuXPos + 1, menuYPos + 2);
            DrawMenuLine();
        }
		
		public void DrawOptions(){
			Console.SetCursorPosition(textXPos, menuYPos + 3);
            for (int i = 0; i < options.Count; i++)
            {
                Console.Write("[{0}] {1}", options[i].Highlighted ? "*" : Convert.ToString(i+1) , options[i].OptionText);
                Console.SetCursorPosition(textXPos, Console.CursorTop + 2);
            }
		}
		
        public void DrawMenuLine()
        {
            for (int i = 0; i < menuWidth - 1; i++) 
                Console.Write("*");
        }

        public void KeyPress(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Escape)
            {
                //Close Program
            }

            if (key.Key == ConsoleKey.Enter)
            {
                options[GetHighlighted()].Select(EventArgs.Empty);
            }

            if (key.Key == ConsoleKey.UpArrow) { ChangeHighlighted(1); }
            if (key.Key == ConsoleKey.DownArrow) { ChangeHighlighted(-1); }
            if (key.Key == ConsoleKey.LeftArrow) { Console.Write("<"); }
            if (key.Key == ConsoleKey.RightArrow) { Console.Write(">"); }

            switch (key.KeyChar)
            {
                case '1':
                    HightlightOption(0);
                    break;
                case '2':
                    HightlightOption(1);
                    break;
                case '3':
                    HightlightOption(2);
                    break;
                case '4':
                    HightlightOption(3);
                    break;
                case '5':
                    HightlightOption(4);
                    break;
                case '6':
                    HightlightOption(5);
                    break;
                case '7':
                    HightlightOption(6);
                    break;
                case '8':
                    HightlightOption(7);
                    break;
                case '9':
                    HightlightOption(8);
                    break;
            }
            if (key.KeyChar == 'a')
            {
                //Console.Write("You pressed a");
            }
        }

        public void HightlightOption(int optionIndex)
        {
            if (optionIndex >= 0 && optionIndex < options.Count)
            {
                options[GetHighlighted()].Highlighted = false;
                options[optionIndex].Highlighted = true;
                options[optionIndex].Select(EventArgs.Empty);
            }
        }

        public void ChangeHighlighted(int toMove)
        {
            int currentSelected = GetHighlighted();
            if (currentSelected != -1 && (currentSelected - toMove >= 0 && currentSelected - toMove < options.Count))
            {
                options[currentSelected].Highlighted = false;
                options[currentSelected - toMove].Highlighted = true;
            }
        }

        public int GetHighlighted()
        {
            if (options != null)
            {
                for (int i = 0; i < options.Count; i++)
                {
                    if (options[i].Highlighted)
                        return i;
                }
            }
            return -1;
        }

        public void CalculateWindowSize()
        {
            if (options != null)
            {
                menuWidth = (GetMaxWidth() + 8 <= Console.WindowWidth) ? GetMaxWidth() + 8 : Console.WindowWidth;
                menuHeight = (options.Count * 2 + 4 <= Console.WindowHeight) ? options.Count * 2 + 4 : Console.WindowHeight;
            }
        }

        public void CalculateWindowPosition()
        {
            if (menuWidth == default(int) || menuHeight == default(int))
                CalculateWindowSize();

            menuXPos = Console.WindowWidth / 2 - menuWidth / 2;
            menuYPos = Console.WindowHeight / 2 - menuHeight / 2;
        }

        public int GetMaxWidth()
        {
            if (options != null)
            {
                int maxLength = 0;
                foreach (MenuOption opt in options)
                {
                    if (opt.OptionText.Length > maxLength)
                        maxLength = opt.OptionText.Length;
                }
                if (title.Length > maxLength)
                    maxLength = title.Length;

                return maxLength;
            }
            return -1;
        }
    }
}