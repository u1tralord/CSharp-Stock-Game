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
		
		if (key.Key == ConsoleKey.UpArrow){ChangeHighlighted(1);}
		if (key.Key == ConsoleKey.DownArrow){ChangeHighlighted(-1);}
		if (key.Key == ConsoleKey.LeftArrow){Console.Write("<");}
		if (key.Key == ConsoleKey.RightArrow){Console.Write(">");}
		
		if (key.KeyChar == 'a')
		{
			Console.Write("You pressed a");
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