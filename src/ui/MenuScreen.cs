using System;
using System.Collections.Generic;

abstract class MenuScreen : Screen{
	public List<MenuOption> options;
	public string title;
	
	public virtual void Draw(){
		Console.Clear();
		DrawBorder(0, 0, Console.WindowWidth, Console.WindowHeight-1);
	}
	
	public static void DrawBorder(int xPos, int yPos, int w, int h){
		Console.SetCursorPosition(xPos, yPos);
		
		for(int y = 0; y < h; y++){
			for(int x = 0; x < w; x++){
				if(x == 0 || x == w-1 || y == 0 || y == h-1){
					Console.Write("*");
				}
				else
					Console.Write(" ");
			}
			Console.SetCursorPosition(xPos, yPos+y+1);
		}
	}
	
	public void KeyPress(ConsoleKeyInfo key){
		if (key.Key == ConsoleKey.Escape)
		{
			//Close Program
		}

		if (key.Key == ConsoleKey.Enter)
		{
			options[GetHighlighted()].Select(EventArgs.Empty);
		}
		
		if (key.Key == ConsoleKey.UpArrow){ChangeHighlighted(1);}
		if (key.Key == ConsoleKey.DownArrow){ChangeHighlighted(-1);}
		if (key.Key == ConsoleKey.LeftArrow){Console.Write("<");}
		if (key.Key == ConsoleKey.RightArrow){Console.Write(">");}
		
		switch(key.KeyChar){
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
	
	public void HightlightOption(int optionIndex){
		if(optionIndex >= 0 && optionIndex < options.Count){
			options[GetHighlighted()].Highlighted = false;
			options[optionIndex].Highlighted = true;
			options[optionIndex].Select(EventArgs.Empty);
		}
	}
	
	public void ChangeHighlighted(int toMove){
		int currentSelected = GetHighlighted();
		if(currentSelected != -1 && (currentSelected-toMove >= 0 && currentSelected-toMove < options.Count)){
			options[currentSelected].Highlighted = false;
			options[currentSelected-toMove].Highlighted = true;
		}
	}
	
	public int GetHighlighted(){
		if(options!= null){
			for(int i = 0; i < options.Count; i++){
				if(options[i].Highlighted)
					return i;
			}
		}
		return -1;
	}
}