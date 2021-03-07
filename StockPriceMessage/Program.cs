using System;
using System.Net.Http;
using System.Timers;
using StockPriceMessage.Service;
namespace StockPriceMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            var stockService = new StockService();
            if (stockService.IsTaiwanStocksWorkingDay(DateTime.Now))
            {
                Timer timer = new Timer();
                timer.Enabled = true;
                timer.Interval = 10000;  // 執行區隔時間,單位為毫秒; 60000/分鐘
                timer.Start();
                timer.Elapsed += new ElapsedEventHandler(stockService.Job);
            }
            //測試用
            //stockService.Job(null,null);
            Console.ReadKey();
        }
    }
}
