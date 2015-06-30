using System;

class StockGame{
	static bool running = true;
	static Screen currentScreen;
	
	public static void Main(string[] args){
		currentScreen = new LoginScreen();
		
		while(running){
			currentScreen.Draw();
			var x = Console.ReadKey();
			currentScreen.KeyPress(x);
		}
	}
}