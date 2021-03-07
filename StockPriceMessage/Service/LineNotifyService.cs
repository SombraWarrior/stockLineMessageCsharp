using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace StockPriceMessage.Service
{
    public  class LineNotifyService
    {
        public bool Send(string msg)
        {
            try
            {
                //TODO:待改善
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://notify-api.line.me/api/notify");
                    client.DefaultRequestHeaders.Add("Authorization",
                        "Bearer " + "");

                    var form = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("message", msg)
                    });

                    var result = client.PostAsync("", form).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
