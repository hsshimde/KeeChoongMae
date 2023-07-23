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
        public string MarketCode { get; set; }
        public string CoinNameKor { get; set; }
        public string CoinNameEng { get; set; }
        public double Quantity { get; set; } //갖고 있는 수량 = 사용 가능한 수량  + 거래에 묶인 수량
        //public double CurPrice { get; set; } //현재가
        public double AvgPrice { get; set; } //평단가
        public float Compare24H { get; set; } //수익률 
        public double EnabledQuantity { get; set; }

        public double OpenPrice { get; set; }
        public double LowPrice { get; set; }

        public int DividePriceGapNumber { get; set; }

        public double PredictedLowPrice { get; set; }
        public int BuyPricePosition { get; set; }
        public List<double> BuyPriceList { get; set; }
        public List<double> LowRateList { get; set; }

        public Ticker MarketTicker { get; set; }

        public bool IsBuyingRequested { get; set; }


        //public bool UseCutLoss { get; set; }

        //public float CutLossRatio { get; set; }

        //public bool UseTakeProfit { get; set; }

        //public float TakeProfitRatio { get; set; }
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

        //public double AccumulateTradePrice { get; set; }

        public EMarketGridTabIdx MarketGridTabIdx { get; set; }

        public string CoinNameSymbol { get; set; }

        public float CutLossRatio { get; set; }

        public bool ShouldCutLoss { get; set; }

        public bool ShouldTakeProfit { get; set; }

        public float TakeProfitRatio { get; set; }

        public float GainLossValuationRatioComparedToBuyPoint { get; set; }



        public Coin(string market, string korName, string engName, MainForm mainForm)
        {
            this.MarketCode = market;
            this.CoinNameKor = korName;
            this.CoinNameEng = engName;
            this.mMainForm = mainForm;
            this.BuyPricePosition = 0;
            this.BuyPriceList = new List<double>();
            this.LowRateList = new List<double>();
            this.DividePriceGapNumber = 5;
            this.ShouldCutLoss = false;
            this.CutLossRatio = 0.0f;
        }

        public void UpdateLowRateList()
        {
            //오전 9시 정각 
            // Day 시가를 업데이트 하기 전에 실행한다
            double newUpdatingLowPriceRate = 100 * ((this.LowPrice / this.OpenPrice) - 1);
            this.LowRateList.RemoveAt(0);
            this.LowRateList.Add(newUpdatingLowPriceRate);
        }

        //public void UpdateOpenLow(float openPrice)
        //{
        //    this.OpenPrice = openPrice;
        //    this.LowPrice = openPrice;
        //}

        public void UpdateOpenLow()
        {
            OpenPrice = MarketTicker.opening_price;
            LowPrice = MarketTicker.opening_price;
        }

        public void CheckAndUpdateLow()
        {
            if(LowPrice > MarketTicker.low_price)
            {
                LowPrice = MarketTicker.low_price;
            }
            
            //if (this.LowPrice > closePrice)
            //{
                //this.LowPrice = closePrice;
            //}
        }

        public void InitLowRateList(List<CandleDay> candleDayList)
        {
            candleDayList.Reverse();
            int nIdx = 0;
            StringBuilder sbNumber = new StringBuilder();
            foreach (CandleDay dayCandle in candleDayList)
            {
                sbNumber.Clear();
                sbNumber.AppendFormat("{0}", nIdx);
                double lowRate = ((dayCandle.low_price / dayCandle.opening_price) - 1) * 100;

                Console.WriteLine(sbNumber.ToString() + $" : Market : {dayCandle.market}, Time : {dayCandle.candle_date_time_kst}, Starting Price : {dayCandle.opening_price}, Low Price: {dayCandle.low_price}, Decrese Rate : {lowRate}, Change Price : {dayCandle.change_price}");
                LowRateList.Add(lowRate);
                nIdx++;
            }
        }


        public void InitOpenLow()
        {
            OpenPrice = MarketTicker.opening_price;
            LowPrice = MarketTicker.low_price;
        }

        private double Rounding(double price)
        {
            if (price >= 100)
            {
                return Math.Round(price, 0);
            }

            else if (price >= 1)
            {
                return Math.Round(price, 2);
            }
            else
            {
                return Math.Round(price, 4);
            }
        }

        public void UpdatePredictedLowPrice()
        {
            //Day 시가를 업데이트한 후 실행한다
            double predictedLowPrice = ((this.LowRateList.Average() + 100) * 0.01) * this.OpenPrice;
            Debug.Assert(DividePriceGapNumber != 0, ("Can not be divided by Zero"));
            double priceGap = (this.OpenPrice - predictedLowPrice) / DividePriceGapNumber;
            this.BuyPricePosition = 0;
            this.BuyPriceList.Clear();
            for (int idx = 0; idx < DividePriceGapNumber; idx++)
            {
                double buyPrice = this.OpenPrice - (priceGap * (idx + 1));
                this.BuyPriceList.Add(buyPrice);
            }
            //예를 들어 시가가 10이고 예측된 저가가 6이다. 
        }

        public double GetBuyPrice()
        {
            int maxIdx = BuyPriceList.Count - 1;
            if (this.BuyPricePosition <= maxIdx && BuyPricePosition >= 0)
            {
                return Rounding(BuyPriceList[BuyPricePosition]);
            }
            else
            {
                return -1;
            }
        }

        public void UpdateBuyPricePos()
        {
            int maxIdx = this.BuyPriceList.Count - 1;
            if ((BuyPricePosition <= maxIdx) && (BuyPricePosition >= 0))
            {
                BuyPricePosition++;
            }
        }


        public void Dispose()
        {

        }
    }
}
