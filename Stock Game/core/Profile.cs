using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Stock_Game.core
{
    class Profile
    {
        string name;
        string hashedPassword;
        long balance;
        Dictionary<string, int> stocks;

        Profile(string accountFile) : this("", "", 0)
        {
            Load(accountFile);
        }

        Profile(string username, string hashPass, long balance0)
        {
            this.name = username;
            this.hashedPassword = hashPass;
            this.balance = balance0;
            this.stocks = new Dictionary<string, int>();
        }

        public int Load(string accountFile)
        {
            
            return -1;
        }

        public int Save()
        {
            return Save(@"\profiles\");
        }

        public int Save(string path)
        {
            path.Replace("/", @"\");
            if (path[path.Length - 1] != '\\')
                path += '\\';
            
            string fileName = path + this.name + ".profile";

            if (!File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Write);

                byte[] profileBytes = GetProfileAsBytes();
                fs.Write(profileBytes, 0, profileBytes.Length);

                return 1;
            }
            
            return -1;
        }

        public byte[] GetProfileAsBytes()
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
            return null;
        }
    }
}
