﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Stock_Game.market;

namespace Stock_Game.core
{
    public class Profile
    {
		public static string defaultProfileLocation = @"profiles\";
		
        string name;
        string hashedPassword;
        double balance;
		
        Dictionary<string, int> stocks;

        public Profile(string accountFile) : this("", "", 0)
        {
            Load(accountFile);
        }

        public Profile(string username, string hashPass, double balance0)
        {
            this.name = username;
            this.hashedPassword = hashPass;
            this.balance = balance0;
            this.stocks = new Dictionary<string, int>();
        }
		
		public string Buy(string stockSymbol, int quantity){
			Stock stock = Launcher.stockGame.GetStock(stockSymbol);
			double tradeValue = (double)(quantity * stock.LatestTradePrice);
			if(stock != null && balance >= tradeValue){
				if(stocks.ContainsKey(stockSymbol))
					stocks[stockSymbol] += quantity;
				else
					stocks.Add(stockSymbol, quantity);
				
				balance -= tradeValue;
				return "";
			}
			else{
				return "Invalid Stock or insufficient funds";
			}
		}
		
		public string Sell(string stockSymbol, int quantity){
			Stock stock = Launcher.stockGame.GetStock(stockSymbol);
			double tradeValue = (double)(quantity * stock.LatestTradePrice);
			
			if(stock != null && stocks.ContainsKey(stockSymbol)){
				if(stocks[stockSymbol] >= quantity)
					stocks[stockSymbol] -= quantity;
				
				if(stocks[stockSymbol] == 0) // REMOVE STOCK FROM LIST;
				
				balance += tradeValue;
				return "";
			}
			else{
				return "Invalid Stock or insufficient stock quantity owned";
			}
		}
		
        public int Load(string accountFile)
        {
            try {
				List<string> lines = File.ReadAllLines(accountFile).ToList<string>();
				this.name = lines[0];
				lines.RemoveAt(0);
				this.hashedPassword = lines[0];
				lines.RemoveAt(0);
				this.balance = Convert.ToDouble(lines[0]);
				lines.RemoveAt(0);
				
				foreach(String s in lines){
					String[] segments = s.Split(':');
					stocks.Add(segments[0], Convert.ToInt32(segments[1]));
				}
			} catch (IOException e){Console.WriteLine("The profile chosen could not be found." + e);
			} catch (Exception e){ Console.WriteLine("The selected file could not be parsed. It may be corrupted or unreadable." + e);}
		
            return -1;
        }

        public int Save()
        {
            return Save(defaultProfileLocation);
        }

        public int Save(string path)
        {
			if(path.Length > 0){
				path.Replace("/", @"\");
				if (path[path.Length - 1] != '\\')
					path += '\\';
				
				if(!Directory.Exists(path))
					Directory.CreateDirectory(path);
			}
			
            string fileName = path + this.name + ".profile";
			if(File.Exists(fileName))
				File.Delete(fileName);
			
            if (!File.Exists(fileName))
            {
                //FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Write);
                //byte[] profileBytes = GetProfileAsBytes();
                //fs.Write(profileBytes, 0, profileBytes.Length);
				
				using (StreamWriter file = new StreamWriter(fileName))
				{
					file.WriteLine(this.name);
					file.WriteLine(this.hashedPassword);
					file.WriteLine(this.balance);
					foreach(string key in stocks.Keys)
						file.WriteLine(key.ToUpper() + ":" + stocks[key]);
				}
                return 1;
            }
            
            return -1;
        }

		public string Name{
			get{ return this.name; }
			set{ this.name = value; }
		}
		
		public string HashedPassword{
			get{ return this.hashedPassword; }
			set{ this.hashedPassword = value; }
		}
		
		public double Balance{
			get{ return this.balance; }
		}
		
		public double TotalStockWorth{
			get{
				double sumValue = 0;
				
				foreach(string key in stocks.Keys)
				{
					//1 IS A SUBSTITUTION FOR THE STOCK VALUE
					sumValue += (double)(stocks[key] * Launcher.stockGame.GetStockValue(key));
				}
				return sumValue;
			}
		}
		
		public Dictionary<string, int> Stocks{
			get{ return this.stocks; }
		}
        /*public byte[] GetProfileAsBytes()
        {
            byte[] lengthOfName = BitConverter.GetBytes(name.Length);
            byte[] nameAsBytes = UTF8Encoding.ASCII.GetBytes(name);

            byte[] lengthOfHash = BitConverter.GetBytes(hashedPassword.Length);
            byte[] hashAsBytes = UTF8Encoding.ASCII.GetBytes(hashedPassword);

            byte[] balanceAsBytes = BitConverter.GetBytes(balance);

            List<byte[]> stocksAsBytes = new List<byte[]>();
            foreach(string key in stocks.Keys)
            {
                stocksAsBytes.Add(UTF8Encoding.ASCII.GetBytes(key));
                stocksAsBytes.Add(BitConverter.GetBytes(stocks[key]));
            }
			
            return CombineBytes(lengthOfName, nameAsBytes, lengthOfHash, hashAsBytes, balanceAsBytes, stocksAsBytes);
        }
		
		public static byte[] CombineBytes( params byte[][] arrays )
		{
			byte[] rv = new byte[ arrays.Sum( a => a.Length ) ];
			int offset = 0;
			foreach ( byte[] array in arrays ) {
				System.Buffer.BlockCopy( array, 0, rv, offset, array.Length );
				offset += array.Length;
			}
			return rv;
		}*/
    }
}
