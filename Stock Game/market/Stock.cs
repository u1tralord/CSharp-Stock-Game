using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Stock_Game.market
{
	public class Stock
	{	
		int lastUpdated;
		
		public Stock(string symbl){
			this.Symbol = symbl;
			Update();
		}
		
		public void Update(){
			using (XmlReader reader = XmlReader.Create("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22" + this.Symbol.ToUpper() + "%22)&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys")){
				while (reader.Read()){
					if (reader.IsStartElement()){
						switch (reader.Name){
							case "Name":
								if (reader.Read()) this.Name = reader.Value.Trim();
								break;
							
							case "Currency":
								if (reader.Read()) this.Currency = reader.Value.Trim();
								break;
								
							case "StockExchange":
								if (reader.Read()) this.StockExchange = reader.Value.Trim();
								break;

							case "Ask":
                                if (reader.Read())
                                {
                                    try { this.AskPrice = Convert.ToDouble(reader.Value.Trim()); }
                                    catch (FormatException e) { this.AskPrice = 0; }
                                }
								break;
								
							case "Bid":
                                if (reader.Read()){
                                    try { this.LatestTradePrice = Convert.ToDouble(reader.Value.Trim()); }
                                    catch (FormatException e) { this.LatestTradePrice = 0; }
                                }
								break;
							
							case "LastTradePriceOnly":
                                if (reader.Read())
                                {
                                    try { this.BidPrice = Convert.ToDouble(reader.Value.Trim()); }
                                    catch (FormatException e) { this.BidPrice = 0; }
                                }
								break;
							
							case "DaysLow":
                                if (reader.Read())
                                {
                                    try { this.DayLow = Convert.ToDouble(reader.Value.Trim()); }
                                    catch (FormatException e) { this.BidPrice = 0; }
                                }
								break;
							
							case "DaysHigh":
                                if (reader.Read())
                                {
                                    try { this.DayHigh = Convert.ToDouble(reader.Value.Trim()); }
                                    catch (FormatException e) { this.BidPrice = 0; }
                                }
								break;
								
							case "Change":
                                if (reader.Read())
                                {
                                    try { this.DayValueChange = Convert.ToDouble(reader.Value.Trim()); }
                                    catch (FormatException e) { this.BidPrice = 0; }
                                }
								break;
								
							case "YearLow":
                                if (reader.Read())
                                {
                                    try { this.YearLow = Convert.ToDouble(reader.Value.Trim()); }
                                    catch (FormatException e) { this.BidPrice = 0; }
                                }
								break;
								
							case "YearHigh":
                                if (reader.Read())
                                {
                                    try { this.YearHigh = Convert.ToDouble(reader.Value.Trim()); }
                                    catch (FormatException e) { this.BidPrice = 0; }
                                }
								break;
								
							case "article":
								// Detect this article element.
								Console.WriteLine("Start <article> element.");
								// Search for the attribute name on this current node.
								string attribute = reader["name"];
								if (attribute != null)
								{
								Console.WriteLine("  Has attribute name: " + attribute);
								}
								// Next read will contain text.
								if (reader.Read())
								{
								Console.WriteLine("  Text node: " + reader.Value.Trim());
								}
								break;
						}
					}
				}
			}
			lastUpdated = System.Environment.TickCount;
		}
		
		public String Name{get; set;}
		public String Symbol{get; set;}
		public String Currency{get; set;}
		public String StockExchange{get; set;}
		
		public double AskPrice{get; set;}
		public double BidPrice{get; set;}
		public double LatestTradePrice{get; set;}
		
		public double DayLow{get; set;}
		public double DayHigh{get; set;}
		public double DayValueChange{get; set;}
		
		public double YearLow{get; set;}
		public double YearHigh{get; set;}
		
		public int TimeSinceUpdate{
			get { return System.Environment.TickCount - lastUpdated; }
		}
	}
}