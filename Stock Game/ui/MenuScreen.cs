using System;
using System.Collections.Generic;

using Stock_Game.core;

namespace Stock_Game.ui
{
    public abstract class MenuScreen : Screen
    {
        public int menuWidth;
        public int menuHeight;
        public int menuXPos;
        public int menuYPos;
		public int textXPos;
		
		public int MAX_MENU_WIDTH = (int)Math.Round(Console.WindowWidth * 0.8);
		public int MAX_MENU_HEIGHT = (int)Math.Round(Console.WindowHeight * 0.8);
		
		
		public int optionsDisplayed = -1;
		
        public List<MenuOption> options;
        public string title;

        public virtual void Draw()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

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
			
			if(optionsDisplayed == -1){
				for (int i = 0; i < options.Count; i++)
				{
					Console.Write("[{0}] {1}", options[i].Highlighted ? "*" : Convert.ToString(i+1) , options[i].OptionText);
					Console.SetCursorPosition(textXPos, Console.CursorTop + 2);
				}
			}
			else{
				
				//SCOLLING NEEDS TO BE FIXED
				int startDrawing = 0;
				if(GetHighlighted() > (int)(optionsDisplayed*0.6))
					startDrawing = GetHighlighted()-1;
				
				for (int i = startDrawing; i < optionsDisplayed+startDrawing; i++)
				{
					Console.Write("[{0}] {1}", options[i].Highlighted ? "*" : Convert.ToString(i+1) , options[i].OptionText);
					Console.SetCursorPosition(textXPos, Console.CursorTop + 2);
				}
			}
		}
		
        public void DrawMenuLine()
        {
            for (int i = 0; i < menuWidth - 1; i++) 
                Console.Write("*");
        }

        public bool KeyPress(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Escape)
            {
                //Close Program
                return true;
            }

            if (key.Key == ConsoleKey.Enter)
            {
                options[GetHighlighted() >= 0 ? GetHighlighted() : 0].Select(EventArgs.Empty);
                return true;
            }
			
			if (key.Key == ConsoleKey.Backspace)
            {
				StockGame.GoBack();
			}
			
            if (key.Key == ConsoleKey.UpArrow) { ChangeHighlighted(1); return true; }
            if (key.Key == ConsoleKey.DownArrow) { ChangeHighlighted(-1); return true; }
            if (key.Key == ConsoleKey.LeftArrow) { Console.Write("<"); return true; }
            if (key.Key == ConsoleKey.RightArrow) { Console.Write(">"); return true; }

            switch (key.KeyChar)
            {
                case '1':
                    HightlightOption(0);
                    return true;
                case '2':
                    HightlightOption(1);
                    return true;
                case '3':
                    HightlightOption(2);
                    return true;
                case '4':
                    HightlightOption(3);
                    return true;
                case '5':
                    HightlightOption(4);
                    return true;
                case '6':
                    HightlightOption(5);
                    return true;
                case '7':
                    HightlightOption(6);
                    return true;
                case '8':
                    HightlightOption(7);
                    return true;
                case '9':
                    HightlightOption(8);
                    return true;
            }
            if (key.KeyChar == 'a')
            {
                //Console.Write("You pressed a");
                return true;
            }
            return false;
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
                menuWidth = (GetMaxWidth() + 8 <= MAX_MENU_WIDTH) ? GetMaxWidth() + 8 : MAX_MENU_WIDTH;
				
				if(options.Count * 2 + 4 <= MAX_MENU_HEIGHT)
					menuHeight = options.Count * 2 + 4;
				else{
					menuHeight = MAX_MENU_HEIGHT;
					optionsDisplayed = (MAX_MENU_HEIGHT - 4)/2;
				}
                //menuHeight = (options.Count * 2 + 4 <= Console.WindowHeight) ? options.Count * 2 + 4 : Console.WindowHeight;
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