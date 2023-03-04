using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upbit.UpbitAPI.Model
{
    using upbit.View;
    public class CoinAccount : IDisposable
    {
        public string MarketCode { get; set; }
        public double ProfitPercentageBuy { get; set; }
        public double ProfitPercentageCompare24H { get; set; }
        public double Quantity { get; set; }
        public double  CurPrice { get; set; }
        public double AvgBuyPrice { get; set; }

        public string CoinNameKor { get; set; }
        public string CoinNameEng { get; set; }

        public double CurNetValue { get; set; }

        public EMarketGridTabIdx GridKind { get; set; }

        public int GridRowNumber { get; set; }

        public CoinAccount(string MarketCode, double Profit, double Quantity, double CurPrice, double AvgPrice)
        {
            this.MarketCode = MarketCode;
            this.ProfitPercentageBuy = Profit;
            this.Quantity = Quantity;
            this.CurPrice = CurPrice;
            this.AvgBuyPrice = AvgPrice;
            string[] marketCodeSep = MarketCode.Split('-');
            if (marketCodeSep[0] == "KRW")
            {
                this.GridKind = EMarketGridTabIdx.KRW;
            }
            else if (marketCodeSep[0] == "BTC")
            {
                this.GridKind = EMarketGridTabIdx.BTC;
            }
            else if (marketCodeSep[0] == "USDT")
            {
                this.GridKind = EMarketGridTabIdx.USDT;
            }
        }
        public void Dispose()
        {

        }

    }
}
