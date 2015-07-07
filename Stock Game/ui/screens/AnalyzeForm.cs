using System;
using System.Collections.Generic;
using System.IO;

using Stock_Game.core;
using Stock_Game.market;

namespace Stock_Game.ui.screens
{
    public class AnalyzeForm : FormScreen
    {
        List<Profile> loadedProfiles = new List<Profile>();

		public AnalyzeForm(string stockSymbol) : base()
		{
			init();
			Stock stock = Launcher.stockGame.GetStock(stockSymbol);
			inputs[0].ValueText = stock.Symbol;
			UpdateValues(this, EventArgs.Empty);
		}
		
        public AnalyzeForm()
        {
            init();
        }
		
		public void init(){
			title = "Analyze Stock";
            inputs = new List<FormInput>();

            FormInput symbol = new FormInput("Symbol", 4, 0, 0);
            symbol.Highlighted = true;
			symbol.ValueModified += UpdateValues;
            inputs.Add(symbol);
			
			FormInput nameField = new FormInput("Name", 15, 0, 0);
			
			inputs.Add(nameField);
			inputs.Add(new FormInput("Currency", 15, 0, 0));
			inputs.Add(new FormInput("AskPrice", 15, 0, 0));
			inputs.Add(new FormInput("BidPrice", 15, 0, 0));
			inputs.Add(new FormInput("LatestTradePrice", 15, 0, 0));
			inputs.Add(new FormInput("DayLow", 15, 0, 0));
			inputs.Add(new FormInput("DayHigh", 15, 0, 0));
			inputs.Add(new FormInput("DayValueChange", 15, 0, 0));
			inputs.Add(new FormInput("YearLow", 15, 0, 0));
			inputs.Add(new FormInput("YearHigh", 15, 0, 0));
			
            CalculateWindowSize();
            CalculateWindowPosition();

            textXPos = menuXPos + 2;
		}
		
		private void UpdateValues(object sender, EventArgs e){
			Stock stock = null;
			if(inputs[0].ValueText.Length >=4 ){
				try{
					stock = Launcher.stockGame.GetStock(inputs[0].ValueText);
				}
				catch(Exception ex){}
			}
			if(stock != null){
				inputs[1].ValueText = " "+stock.Name;
				inputs[2].ValueText = " "+stock.Currency;
				inputs[3].ValueText = " "+stock.AskPrice;
				inputs[4].ValueText = " "+stock.BidPrice;
				inputs[5].ValueText = " "+stock.LatestTradePrice;
				inputs[6].ValueText = " "+stock.DayLow;
				inputs[7].ValueText = " "+stock.DayHigh;
				inputs[8].ValueText = " "+stock.DayValueChange;
				inputs[9].ValueText = " "+stock.YearLow;
				inputs[10].ValueText = " "+stock.YearHigh;
			}
		}
		
        public override void EnterAction()
        {
            base.EnterAction();
			if(inputs[0].ValueText.Equals("")){
				Launcher.stockGame.GoBack();
				return;
			}
			else{
				UpdateValues(this, EventArgs.Empty);
			}
        }
		
		public override void Draw(){
			base.Draw();
			Console.SetCursorPosition(1, 1);
			Console.Write("Balance: {0} Total Stock Worth: {1}", Launcher.stockGame.Account.Balance, Launcher.stockGame.Account.TotalStockWorth);
			HightlightInput(GetHighlighted());
		}
    }
}