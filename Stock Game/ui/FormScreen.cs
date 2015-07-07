using System;
using System.Collections.Generic;

using Stock_Game.core;

namespace Stock_Game.ui
{
    public abstract class FormScreen : Screen
    {
        public int menuWidth;
        public int menuHeight;
        public int menuXPos;
        public int menuYPos;
		public int textXPos;
		
		public int MAX_MENU_WIDTH = (int)Math.Round(Console.WindowWidth * 0.8);
		public int MAX_MENU_HEIGHT = (int)Math.Round(Console.WindowHeight * 0.8);
		
		
		public int inputsDisplayed = -1;
		
        public List<FormInput> inputs;
        public string title;
        public string bottomMsg = "Submit a blank form to return";
        public string errorString;

        public virtual void Draw()
        {
            Launcher.stockGame.SetConsoleColors(ConsoleColor.White, ConsoleColor.Black);

            Console.Clear();
            DrawBorder(0, 0, Console.WindowWidth, Console.WindowHeight - 1);
			
			if (menuWidth != default(int) && menuHeight != default(int) && menuXPos != default(int) && menuYPos != default(int))
				DrawBorder(menuXPos, menuYPos, menuWidth, menuHeight);
			if(!String.IsNullOrEmpty(title))
				DrawTitle();
            if (inputs != null)
                DrawInputs();

            Console.SetCursorPosition(textXPos, Console.CursorTop + 2);
            Console.Write("Press <Enter> to submit");
            Console.SetCursorPosition(textXPos, Console.CursorTop + 1);
            Console.Write(bottomMsg);

            if(errorString != null)
                PrintError(errorString);
            errorString = null;
			
			HightlightInput(GetHighlighted());
        }

        public virtual void EnterAction()
        {

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
		
		public void DrawInputs(){
			Console.SetCursorPosition(textXPos, menuYPos + 2);
			
			if(inputsDisplayed == -1){
				for (int i = 0; i < inputs.Count; i++)
				{
                    inputs[i].XPos = textXPos;
                    inputs[i].YPos = Console.CursorTop + 2;
                    inputs[i].Draw();
				}
			}
			else{
				int startDrawing = 0;
				if(GetHighlighted() > (int)(inputsDisplayed*0.3))
					startDrawing = GetHighlighted()-1;
				
				if(startDrawing > inputs.Count-inputsDisplayed) startDrawing = inputs.Count-inputsDisplayed; 
						
				
				for (int i = startDrawing; i < inputsDisplayed+startDrawing; i++)
				{
					if(i < inputs.Count){
						inputs[i].XPos = textXPos;
						inputs[i].YPos = Console.CursorTop + 2;
						inputs[i].Draw();
					}
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
                EnterAction();
                return true;
            }
			
			if (key.Key == ConsoleKey.Tab)
            {
				ChangeHighlighted(-1);
                return true;
            }

            if (key.Key == ConsoleKey.UpArrow) { ChangeHighlighted(1); return true; }
            if (key.Key == ConsoleKey.DownArrow) { ChangeHighlighted(-1); return true; }
            //if (key.Key == ConsoleKey.LeftArrow) { Console.Write("<"); return true; }
            //if (key.Key == ConsoleKey.RightArrow) { Console.Write(">"); return true; }

            string validChars = "abcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()_+-=[]{}\\|;':\"<>,./?";
            if (validChars.Contains(key.KeyChar.ToString()))
            {
                if (inputs[GetHighlighted()].ValueText.Length < inputs[GetHighlighted()].InputLength)
                    inputs[GetHighlighted()].ValueText += key.KeyChar;
                else
                {
                    return true;
                }
            }
            if (key.Key == ConsoleKey.Backspace)
            {
                if (inputs[GetHighlighted()].ValueText.Length > 0)
                    inputs[GetHighlighted()].ValueText = inputs[GetHighlighted()].ValueText.Substring(0, inputs[GetHighlighted()].ValueText.Length - 1);
				else
					Launcher.stockGame.GoBack();
                return true;
            }
            return false;
        }

        public void HightlightInput(int inputIndex)
        {
            if (inputIndex >= 0 && inputIndex < inputs.Count)
            {
                inputs[GetHighlighted()].Highlighted = false;
                inputs[inputIndex].Highlighted = true;
                MoveCursorToInput(inputs[inputIndex]);
            }
        }

        public void ChangeHighlighted(int toMove)
        {
            int currentSelected = GetHighlighted();
            if (currentSelected != -1 && (currentSelected - toMove >= 0 && currentSelected - toMove < inputs.Count))
            {
                inputs[currentSelected].Highlighted = false;
                inputs[currentSelected - toMove].Highlighted = true;
            }
        }

        public static void MoveCursorToInput(FormInput input){
            Console.SetCursorPosition(input.XPos + input.OptionText.Length + input.ValueText.Length + 4, input.YPos);
            Launcher.stockGame.SetConsoleColors(ConsoleColor.Black, ConsoleColor.White);
        }

        public int GetHighlighted()
        {
            if (inputs != null)
            {
                for (int i = 0; i < inputs.Count; i++)
                {
                    if (inputs[i].Highlighted)
                        return i;
                }
            }
            return -1;
        }

        public static void PrintError(string message)
        {
            Console.SetCursorPosition(Console.WindowWidth-1-message.Length-5, Console.WindowHeight-1);
            Console.Write(message);
        }

        public void CalculateWindowSize()
        {
            if (inputs != null)
            {
                menuWidth = (GetMaxWidth() + 6 <= MAX_MENU_WIDTH) ? GetMaxWidth() + 6 : MAX_MENU_WIDTH;
                if (inputs.Count * 2 + 7 <= MAX_MENU_HEIGHT)
                    menuHeight = inputs.Count * 2 + 7;
				else{
					menuHeight = MAX_MENU_HEIGHT;
					inputsDisplayed = (MAX_MENU_HEIGHT - 7)/2;
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
            if (inputs != null)
            {
                int maxLength = 0;
                foreach (FormInput opt in inputs)
                {
                    if (opt.OptionText.Length + opt.InputLength > maxLength)
                        maxLength = opt.OptionText.Length + opt.InputLength;
                }
                if (title.Length > maxLength)
                    maxLength = title.Length;
                if (bottomMsg.Length > maxLength)
                    maxLength = bottomMsg.Length;
                return maxLength;
            }
            return -1;
        }
    }
}