using System;
using System.Collections.Generic;
using System.IO;

using Stock_Game.core;
using Stock_Game.market;

namespace Stock_Game.ui.screens
{
    public class SellForm : FormScreen
    {
        List<Profile> loadedProfiles = new List<Profile>();

		public SellForm(string stockSymbol) : base()
		{
			init();
			Stock stock = Launcher.stockGame.GetStock(stockSymbol);
			inputs[0].ValueText = stock.Symbol;
			inputs[0].Highlighted = false;
			inputs[1].Highlighted = true;
			inputs[2].ValueText = stock.LatestTradePrice+"";
		}
		
        public SellForm()
        {
            init();
        }
		
		public void init(){
			title = "Sell Stock";
            inputs = new List<FormInput>();

            FormInput symbol = new FormInput("Symbol", 4, 0, 0);
            symbol.Highlighted = true;
			symbol.ValueModified += UpdateValues;
            inputs.Add(symbol);

            FormInput quantity = new FormInput("Quantity", 6, 0, 0);
			quantity.ValueModified += UpdateValues;
			quantity.ValueText = ""+1;
            inputs.Add(quantity);
			
			FormInput totalValue = new FormInput("Total", 10, 0, 0);
			totalValue.ValueModified += UpdateValues;
			totalValue.ValueText = ""+0;
            inputs.Add(totalValue);

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
				catch(Exception exception){}
			}
			if(stock != null){
				int quantity = inputs[1].ValueText.Length > 0 ? Convert.ToInt32(inputs[1].ValueText) : 0;
				inputs[2].ValueText = quantity*stock.LatestTradePrice + "";
			}
		}
		
        public override void EnterAction()
        {
            base.EnterAction();
			if(inputs[0].ValueText.Equals("") || inputs[1].ValueText.Equals("")){
				Launcher.stockGame.GoBack();
				return;
			}
			else{
				errorString = Launcher.stockGame.Account.Sell(inputs[0].ValueText, Convert.ToInt32(inputs[1].ValueText));
				Launcher.stockGame.Account.Save();
				Launcher.stockGame.GoBack();
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