using System;

using Stock_Game.core;

namespace Stock_Game.ui
{
    public class FormInput
    {
		public event EventHandler ValueModified;
		
        int inputLength;
        int xPos;
        int yPos;
        bool highlighted = false;
        string valueText = "";
        string optionText = "";

        public FormInput(string text, int inputLength, int x, int y)
        {
            this.ValueText = "";
            this.OptionText = text;
            this.inputLength = inputLength;
            this.xPos = x;
            this.yPos = y;
        }

        public void Draw()
        {
            Console.SetCursorPosition(xPos, yPos);
            Console.Write("[" + (this.Highlighted ? "*": " ") + "]" + this.OptionText + ":");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(this.ValueText);
            for (int i = 0; i < inputLength - this.ValueText.Length; i++)
                Console.Write(" ");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
			
        public int XPos
        {
            get { return xPos; }
            set { xPos = value; }
        }

        public int YPos
        {
            get { return yPos; }
            set { yPos = value; }
        }

        public int InputLength
        {
            get { return inputLength; }
            set { inputLength = value; }
        }

        public string ValueText
        {
            get { return valueText; }
            set { valueText = value; }
        }
		
        public string OptionText
        {
            get { return optionText; }
            set { optionText = value; }
        }

        public bool Highlighted
        {
            get { return highlighted; }
            set
            {
                Console.SetCursorPosition(this.xPos, this.yPos);
                highlighted = value;
				
				EventHandler handler = ValueModified;
				if (handler != null)
				{
					handler(this, EventArgs.Empty);
				}	
            }
        }
    }
}