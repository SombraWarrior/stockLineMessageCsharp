using StockPriceMessage.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace StockPriceMessage.Service
{
    public class StockService
    {
     
        public bool IsTaiwanStocksWorkingDay(DateTime today)
        {
            DateTime beginTime = GetBeginTime(today);
            DateTime endnTime = GetEndTime(today);
            int weekday = int.Parse(today.DayOfWeek.ToString("d"));
            return (weekday >= 1 && weekday <= 5) &&
                    (today.CompareTo(beginTime) >= 0 && 
                    today.CompareTo(endnTime) <= 0);
         }

       
        public void Job(object source, ElapsedEventArgs e)
        {
            var fileRepository = new FileRepository();
            var LineNotifyService = new LineNotifyService();
            var yahooRepository = new YahooRepository();
            var stocks = fileRepository.GetStocks();
            if(stocks.Count ==0)
                Console.WriteLine("無資料，請查看是否異常");
            foreach (var item in stocks)
            {
                string message = string.Empty;
                var price = yahooRepository.GetPrice(item.StockNum);
                if (item.Bs == "<" && Convert.ToDouble(price) < item.Price) 
                {
                    message = item.StockNum + "(可買進)" + "市價:"
                                          + price + "系統設定買進價:" + item.Price;
                    Console.WriteLine("time:" + DateTime.Now);
                    LineNotifyService.Send(message);
                }

                if (item.Bs == ">" && Convert.ToDouble(price) > item.Price)
                {
                    message = item.StockNum + "(可賣出)" + "市價:"
                                           + price + "系統設定賣出價:" + item.Price;
                    Console.WriteLine("time:" + DateTime.Now);
                    LineNotifyService.Send(message);
                }
            }
        }

        private DateTime GetBeginTime (DateTime today)
            => new DateTime(today.Year,
                            today.Month,
                            today.Day, 
                            09, 00, 00, 00);
        private DateTime GetEndTime(DateTime today)
          => new DateTime(today.Year,
                          today.Month,
                          today.Day,
                          13, 30, 00, 00);
    }
}
