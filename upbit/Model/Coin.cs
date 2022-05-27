using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upbit.UpbitAPI.Model
{
    public class Coin : IDisposable
     {
        public string MarketInfo;
        public double Quantity { get; set; }
        public double CurPrice { get; set; }
        public double AvgPrice { get; set; }

        public double Profit { get; set; }

        public Coin(string market)
        {
            this.MarketInfo = market;
        }

       

        public void Dispose()
        {

        }
    }
}
