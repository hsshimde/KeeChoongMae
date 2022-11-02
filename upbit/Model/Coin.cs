using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upbit.View;

namespace upbit.UpbitAPI.Model
{
    public class Coin : IDisposable
     {
        public string MarketCode;
        public string CoinNameKor;
        public string CoinNameEng;
        public double Quantity { get; set; }
        public double CurPrice { get; set; }
        public double AvgPrice { get; set; }
        public float Compare24H { get; set; }
        public int GridRowNumber { get; set; }

        public double Profit { get; set; }

        public double AccumulateTradePrice{ get; set; }

        public EGridKind GridKind { get; set; }

        public Coin(string market, string korName, string engName)
        {
            this.MarketCode = market;
            this.CoinNameKor = korName;
            this.CoinNameEng = engName;
        }

       

        public void Dispose()
        {

        }
    }
}
