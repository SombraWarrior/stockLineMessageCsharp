using StockPriceMessage.Entites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StockPriceMessage.Repository
{
    public class FileRepository
    {
        string filePath = System.Environment.CurrentDirectory + "/stockPrice.txt";
        private bool IsFile()
            => (File.Exists(filePath)) ? true : false;

        public List<stock> GetStocks()
        {
            var result = new List<stock>();
            try
            {
               
                if (!IsFile())
                    return result;

                string readText = File.ReadAllText(filePath);
                string[] rows = readText.Split('!');
                foreach (var row in rows)
                {
                    string[] item = row.Split(',');

                    result.Add(new stock
                    {
                        StockNum = item[0],
                        Bs = item[1],
                        Price = Convert.ToDouble(item[2])
                    });
                }
                return result;
               
            }
            catch (Exception e)
            {
                //log 自行發揮
            }

            return result;
        }
    }
}
