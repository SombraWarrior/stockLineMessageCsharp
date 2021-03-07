using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPriceMessage.Repository
{
    public class YahooRepository
    {
        public string GetPrice(string stockNum)
        {
            var url = "https://tw.stock.yahoo.com/q/q?s=" + stockNum;
            string node = "/html/body/center/table[2]/tr/td/table/tr[2]/td[3]";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var nameNode = doc.DocumentNode.SelectSingleNode(node);
            return nameNode.InnerText;
        }

    }
}
