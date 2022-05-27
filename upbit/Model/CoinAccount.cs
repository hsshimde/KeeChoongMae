using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upbit.UpbitAPI.Model
{
    public class CoinAccount : IDisposable
    {
        public string strMarketCode { get; set; }
        public double dblProfit { get; set; }
        public double dblQuantity { get; set; }
        public double dblCurPrice { get; set; }
        public double dblAvgPrice { get; set; }

        public CoinAccount(string strMarketCode, double dblProfit, double dblQuan, double dblCurPrice, double dblAvgPrice)
        {
            this.strMarketCode = strMarketCode;
            this.dblProfit = dblProfit;
            this.dblQuantity = dblQuan;
            this.dblCurPrice = dblCurPrice;
            this.dblAvgPrice = dblAvgPrice; 
        }
        public void Dispose()
        {

        }

    }
}
