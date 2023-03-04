using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public double OpenPrice { get; set; }
        public double LowPrice { get; set; }
        public double PredictedLowPrice { get; set; }
        public int BuyPricePosition { get; set; }
        public List<double> BuyPriceList { get; set; }
        public List<double> LowRateList { get; set; }


        public int GridRowNumber
        {
            get { return innerGridRowIdx; }
            set
            {
                Dictionary<int, Coin> DictByRowIdx = null;
                switch (MarketGridTabIdx)
                {
                    case EMarketGridTabIdx.KRW:
                        DictByRowIdx = mMainForm.DictCoinByRowNumMarketKRW;
                        break;

                    case EMarketGridTabIdx.BTC:
                        DictByRowIdx = mMainForm.DictCoinByRowNumMarketBTC;
                        break;

                    case EMarketGridTabIdx.USDT:
                        DictByRowIdx = mMainForm.DictCoinByRowNumMarketUSDT;
                        break;

                    case EMarketGridTabIdx.Interest:
                        DictByRowIdx = mMainForm.DictCoinByRowNumMarketInterest;
                        break;

                    default:
                        Debug.Assert(false);
                        break;
                }
                DictByRowIdx[value] = this;
                innerGridRowIdx = value;
            }
        }

        private MainForm mMainForm;


        private int innerGridRowIdx;

        public double Profit { get; set; }

        public double AccumulateTradePrice{ get; set; }

        public EMarketGridTabIdx MarketGridTabIdx { get; set; }

        public string CoinNameSymbol { get; set; }
            

        public Coin(string market, string korName, string engName, MainForm mainForm)
        {
            this.MarketCode = market;
            this.CoinNameKor = korName;
            this.CoinNameEng = engName;
            this.mMainForm = mainForm;
            this.BuyPricePosition = 0;
            this.BuyPriceList = new List<double>();
            this.LowRateList = new List<double>();
        }

        public void UpdateLowRateList()
        {
            //오전 9시 정각 
            // Day 시가를 업데이트 하기 전에 실행한다
            double newUpdatingLowPriceRate = 100 * ((this.LowPrice / this.OpenPrice) - 1);
            this.LowRateList.RemoveAt(0);
            this.LowRateList.Add(newUpdatingLowPriceRate);
        }

        public void UpdateOpenLow(double openPrice)
        {
            this.OpenPrice = openPrice;
            this.LowPrice = openPrice; 
        }

        public void CheckAndUpdateLow(double closePrice)
        {
            if(this.LowPrice > closePrice)
            {
                this.LowPrice = closePrice;
            }
        }

        public void UpdatePredictedLowPrice()
        {
            this.PredictedLowPrice = ((this.LowRateList.Average() + 100) * 0.01) * (this.OpenPrice);
            double gap = this.OpenPrice - this.PredictedLowPrice;

            //for (int count = 1; )
            //{
            //}
        }
       

        public void Dispose()
        {

        }
    }
}
