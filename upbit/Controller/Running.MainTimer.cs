using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upbit.Enum;
using upbit.Model;
using upbit.UpbitAPI;
using upbit.UpbitAPI.Model;
using static upbit.Enum.EnumClass;

namespace upbit.Controller
{
    public partial class Running
    {

        //struct AssetInfo
        //{
        //    public Coin myCoin { get; set; }
        //    public Account account { get; set; }
        //}

        //public enum EDayFlag
        //{
        //    PastOneDay,
        //    NotYet
        //};


        public EventHandler<CoinAccount> ehUpdateCoinAccount;
        public EventHandler<Monitoring> ehUpdateMonitoring;

        public EventHandler<string> ehRemoveAccount;
        public EventHandler<double> ehUpdateTotalAsset;
        public EventHandler<double> ehRemoveTotalAsset;
        public EventHandler<Coin> ehUpdateTicker;
        public Dictionary<string, CoinAccount> DictMyAssetInfo { get; set; }
        public Dictionary<string, Coin> DictAllMarketInfo { get; set; }

        public List<Coin> RunningMarketList { get; private set; }

        public EnumClass.EIsRunning RunningStatus { get; set; }

        //public Dictionary<string, Coin> DictRunningMarketInfo { get; private set; }
        //private Dictionary<string, Coin> mDictSelectedMarketCode;


        //public bool BShouldUpdateAllMarket { get; private set; }

        //public bool BSearchBox


        //private List<string> m_listMyAsset;
        //private Dictionary<string, Coin> m_dictMyAsset;


        private async Task updateMyAccountInfoAndMakeTrades()
        {
            DateTime now = DateTime.Now;
            Task<List<Account>> accountTask = mAPI.GetAccount();
            List<Account> myAssetAccount = await accountTask;
            MyTotalAccountInfo myAccountInfo = null;
            if (myAssetAccount != null)
            {
                myAccountInfo = GetMyAsset(myAssetAccount);
            }
            if (myAccountInfo != null)
            {
                if (RunningStatus == EnumClass.EIsRunning.Running)
                {
                    //if(mTmSpanCheck.IsPassedOneDay(now) == EnumClass.EDayFlag.PastOneDay)
                    {
                        makeTrade(myAccountInfo, mTmSpanCheck.IsPassedOneDay(now));
                    }
                }
            }

            //if (registeredAccountList != null && selectedCoinList != null)
            //{
            //    GetTicker(selectedCoinList);
            //    GetMyTotalAsset(registeredAccountList);
            //    GetAccountInfo(registeredAccountList);
            //}
            //else
            //{
            //    //m_MainForm.ConnectionNotWorking();
            //}

            //Console.WriteLine("Update Execution Complete");
        }

        public async Task DoUpdate()
        {
            if (mMainForm.IsDisposed)
            {
                return;
            }
            await updateAllTickerData();
            await updateMyAccountInfoAndMakeTrades();
        }

        private async Task updateAllTickerData()
        {
            Task<List<Ticker>> taskTickerList = mAPI.GetTicker(this.allMarketCode);
            List<Ticker> tickerList = await taskTickerList;
            if (null == tickerList)
            {
                return;
            }
            foreach (Ticker ticker in tickerList)
            {
                updateSingleTickerData(ticker);
                string marketCode = ticker.market;
                Coin mappedCoin = DictAllMarketInfo[marketCode];
                ehUpdateTicker?.Invoke(this, mappedCoin);
            }
        }

        private void updateGrid(Coin coin)
        {

        }

        private void updateSingleTickerData(Ticker ticker)
        {
            string marketCode = ticker.market;
            Coin mappedCoin = DictAllMarketInfo[marketCode];
            Debug.Assert(mappedCoin != null);
            if (mappedCoin == null)
            {
                return;
            }
            //mappedCoin.AccumulateTradePrice = ticker.acc_trade_price_24h;
            //mappedCoin.CurPrice = ticker.trade_price;
            mappedCoin.MarketTicker = ticker;
            //float fCompared24H = (float)CalculateProfit(ticker.prev_closing_price, ticker.trade_price);
            float fCompared24H = (float)CalculateProfit(mappedCoin.MarketTicker.prev_closing_price, mappedCoin.MarketTicker.trade_price);
            mappedCoin.Compare24H = fCompared24H;
        }


        private void InitializeRunning(APIClass api)
        {
            this.mMainTimer = new System.Timers.Timer();
            this.mMainTimer.Interval = 500; // 1000ms = 1 second
            this.mMainTimer.Elapsed += ElapsedEventReceiver;
            //mDictSelectedMarketCode = new Dictionary<string, Coin>();
            //DictMyAssetInfo = new Dictionary<string, CoinAccount>();
            this.mAPI = api;
            this.mOrderTimer = new System.Timers.Timer();
            this.mOrderTimer.Interval = 333;
            this.mOrderTimer.Elapsed += ElapsedEventReceiver;
            mInsertOrderQueue = new ConcurrentQueue<Model.MyOrder>();
            mTmSpanCheck = new TimeSpanCheck(DateTime.Now);
            //RunningMarketLiet = new List<Coin>();
            //DictRunningMarketInfo = new Dictionary<string, Coin>();
        }

        //private void GetTicker(List<Ticker> listTicker)
        //{
        //    //Receive current price and process transactions
        //    //DO Parallel
        //    Parallel.ForEach(listTicker, item =>
        //    {
        //        //if (mDictSelectedMarketCode.ContainsKey(item.market))
        //        //{
        //        //    using (Coin coin = mDictSelectedMarketCode[item.market])
        //        //    {
        //        //        double curPrice = item.trade_price;
        //        //        coin.CurPrice = item.trade_price;
        //        //        double _24h = CalculateProfit(item.opening_price, curPrice);
        //        //        using (Monitoring monitoring = new Monitoring(coin.MarketCode, coin.CurPrice, _24h))
        //        //        {
        //        //            //ehUpdateMonitoring?.Invoke(this, monitoring);
        //        //        }
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    Coin coin = new Coin(item.market, item.market, item.market, mMainForm);
        //        //    coin.CurPrice = item.trade_price;
        //        //    lock (mDictSelectedMarketCode)
        //        //    {
        //        //        mDictSelectedMarketCode.Add(coin.MarketCode, coin);
        //        //    }
        //        //}
        //    });
        //}

        //private void SetCurrentAccountInfo(List<Account> listAccount)
        //{
        //    StringBuilder sbMarketCode = new StringBuilder();
        //    bool bKoreanWonIgonored = false;
        //    for (int nIdx = 0; nIdx < listAccount.Count; nIdx++)
        //    {
        //        sbMarketCode.Clear();
        //        Account account = listAccount[nIdx];
        //        sbMarketCode.AppendFormat(account.unit_currency);
        //        sbMarketCode.AppendFormat("-");
        //        sbMarketCode.AppendFormat(account.currency);

        //        string marketCode = sbMarketCode.ToString();
        //        Coin assetCoin = null;
        //        if(!bKoreanWonIgonored && marketCode =="KRW-KRW")
        //        {
        //            bKoreanWonIgonored = true;
        //            continue;
        //        }
        //        else
        //        {
        //            assetCoin = DictAllMarketInfo[marketCode];
        //        }
        //        Debug.Assert(assetCoin != null);
        //        CoinAccount myCoinAccount = new CoinAccount(marketCode, 0, account.balance, 0, account.avg_buy_price);
        //        myCoinAccount.CoinNameKor = assetCoin.CoinNameKor;
        //        myCoinAccount.CoinNameEng = assetCoin.CoinNameEng;
        //        myCoinAccount.GridRowNumber = -1;
        //        DictMyAssetInfo.Add(myCoinAccount.MarketCode, myCoinAccount); 
        //    }
        //}

        //private void GetAccountInfo(List<Account> listAccount)
        //{
        //    using (Account account = listAccount.Where(x => x.currency.Equals("KRW")).FirstOrDefault())
        //    {
        //        double quantity = account.balance + account.locked;
        //        double price = account.avg_buy_price;
        //        listAccount.Remove(account);
        //    }
        //    foreach (KeyValuePair<string, Coin> items in mDictSelectedMarketCode)
        //    {
        //        using (Account account = listAccount.Where(x => items.Key.Contains(x.currency)).FirstOrDefault())
        //        {
        //            if (account != null)
        //            {
        //                items.Value.Quantity = account.balance + account.locked;
        //                items.Value.AvgPrice = account.avg_buy_price;
        //                items.Value.Profit = CalculateProfit(items.Value.AvgPrice, items.Value.CurPrice);
        //                double curValue = items.Value.Quantity * items.Value.CurPrice;
        //                using (CoinAccount ca = new CoinAccount(items.Key, items.Value.Profit, items.Value.Quantity, items.Value.CurPrice, items.Value.AvgPrice))
        //                {
        //                    ehUpdateCoinAccount?.Invoke(this, ca);
        //                }
        //            }
        //            else
        //            {
        //                items.Value.Quantity = 0;
        //                items.Value.AvgPrice = 0;
        //                items.Value.Profit = 0;
        //                //ehRemoveAccount?.Invoke(this, items.Key);
        //            }
        //            //items.Value.dblQuantity = account.balance + account.locked;
        //            //items.Value.dblAvgPrice = account.avg_buy_price;
        //            //items.Value.dblProfit = CalculateProfit(items.Value.dblAvgPrice, items.Value.dblCurPrice);
        //        }
        //    }
        //}

        public MyTotalAccountInfo GetMyAsset(List<Account> listAccount)
        {
            double totalAssetInKoreanWonIncludingKRW = 0.0;
            double justKoreanWon = 0.0;
            double enabledKoreanWon = 0.0;
            double netGainLossValuation = 0.0;
            double totalNetBuyValue = 0.0;
            using (Account account = listAccount.Where(x => x.currency.Equals("KRW")).FirstOrDefault())
            {
                double quantity = account.balance + account.locked;
                justKoreanWon += quantity;
                totalAssetInKoreanWonIncludingKRW += justKoreanWon;
                enabledKoreanWon = account.balance;
                listAccount.Remove(account);
            }
            StringBuilder sbMarketCode = new StringBuilder();
            for (int nIdx = 0; nIdx < listAccount.Count; nIdx++)
            {
                sbMarketCode.Clear();
                Account updateAccount = listAccount[nIdx];
                sbMarketCode.AppendFormat(updateAccount.unit_currency);
                sbMarketCode.AppendFormat("-");
                sbMarketCode.AppendFormat(updateAccount.currency);
                string marketCode = sbMarketCode.ToString();
                Coin tickerCoin = DictAllMarketInfo[marketCode];
                Debug.Assert(tickerCoin != null);
                CoinAccount tickerCoinAccount = DictMyAssetInfo[marketCode];
                Debug.Assert(tickerCoinAccount != null);

                tickerCoinAccount.Quantity = updateAccount.balance + updateAccount.locked;
                tickerCoinAccount.AvgBuyPrice = updateAccount.avg_buy_price;
                tickerCoinAccount.CurPrice = tickerCoin.MarketTicker.trade_price;
                tickerCoinAccount.ProfitPercentageBuy = CalculateProfit(tickerCoinAccount.AvgBuyPrice, tickerCoinAccount.CurPrice);
                tickerCoinAccount.ProfitPercentageCompare24H = tickerCoin.Compare24H;
                tickerCoin.EnabledQuantity = updateAccount.balance;
                tickerCoin.Quantity = tickerCoinAccount.Quantity;
                tickerCoin.GainLossValuationRatioComparedToBuyPoint = (float)tickerCoinAccount.ProfitPercentageBuy;

                double CurNetValue = tickerCoinAccount.CurPrice * tickerCoinAccount.Quantity;
                double BuyPointNetValue = updateAccount.avg_buy_price * tickerCoinAccount.Quantity;
                totalNetBuyValue += BuyPointNetValue;
                tickerCoinAccount.GainLossValuation = (float)(CurNetValue - BuyPointNetValue);
                netGainLossValuation += tickerCoinAccount.GainLossValuation;
                totalAssetInKoreanWonIncludingKRW += CurNetValue;
                tickerCoinAccount.CurNetValue = CurNetValue;
                ehUpdateCoinAccount?.Invoke(this, tickerCoinAccount);
            }
            MyTotalAccountInfo myTotalAccountInfo = new MyTotalAccountInfo(totalAssetInKoreanWonIncludingKRW, justKoreanWon, enabledKoreanWon, netGainLossValuation, totalNetBuyValue);
            mMainForm.UpdateTotalAccountInfo(myTotalAccountInfo);
            return myTotalAccountInfo;

        }
        //    foreach (KeyValuePair<string, CoinAccount> items in m_dictCoinAccount)
        //    {
        //        Coin tickerCoin = m_dictAllMarketCode[items.Key];
        //        Debug.Assert(tickerCoin != null);
        //        items.Value.dblQuantity = account.balance + account.locked;
        //        items.Value.AvgPrice = account.avg_buy_price;
        //        items.Value.Profit = CalculateProfit(items.Value.AvgPrice, items.Value.CurPrice);
        //        double curValue = items.Value.Quantity * items.Value.CurPrice;
        //        using (CoinAccount ca = new CoinAccount(items.Key, items.Value.Profit, items.Value.Quantity, items.Value.CurPrice, items.Value.AvgPrice))
        //        {
        //            ehUpdateCoinAccount?.Invoke(this, ca);
        //        }


        //        //items.Value.dblQuantity = account.balance + account.locked;
        //        //items.Value.dblAvgPrice = account.avg_buy_price;
        //        //items.Value.dblProfit = CalculateProfit(items.Value.dblAvgPrice, items.Value.dblCurPrice);

        //    }
        //}


        private double CalculateProfit(double dblOpen, double dblClose)
        {
            return Math.Round(100 * ((dblClose / dblOpen) - 1), 2);
        }
        //public async Task BeforeGoRunning(List<Coin> marketRunningList)
        public async Task BeforeGoRunning()
        {
            if (mMainForm.SelCoin == null)
            {
                return;
            }

            if (RunningMarketList == null)
            {
                RunningMarketList = new List<Coin>(GetRunngMarketCoinList());
            }
            else
            {
                RunningMarketList = GetRunngMarketCoinList();
            }
            foreach (Coin coin in RunningMarketList)
            {
                coin.InitOpenLow();
                Task<List<CandleDay>> taskCandleDay = mAPI.GetCandleDays(coin.MarketCode, DateTime.Now.AddDays(-1), 20);
                List<CandleDay> candleDataList = await taskCandleDay;
                if (candleDataList != null)
                {
                    coin.InitLowRateList(candleDataList);
                    coin.UpdatePredictedLowPrice();
                    for (int idx = 0; idx < coin.DividePriceGapNumber; idx++)
                    {
                        double buyPrice = coin.GetBuyPrice();
                        Console.WriteLine("{0} Buy Price : {1}", idx, buyPrice);
                    }
                }
            }
        }

        public bool UpdateRunningMarketList(List<Coin> updatedRunningList)
        {
            if (updatedRunningList == null)
            {
                return false;
            }

            RunningMarketList.Clear();
            for (int idx = 0; idx < updatedRunningList.Count; idx++)
            {
                Coin updatedMarket = updatedRunningList.ElementAt(idx);
                RunningMarketList.Add(updatedMarket);
            }
            return true;
        }



        public List<Coin> GetRunngMarketCoinList()
        {
            List<Coin> marketRunningList = null;
            if (mMainForm.SelCoin == null)
            {
                return null;
            }
            lock (mMainForm.SelCoin)
            {
                marketRunningList = mMainForm.SelCoin;
            }
            return marketRunningList;
        }


        private void makeTrade(MyTotalAccountInfo myAccountInfo, EDayFlag dayFlag)
        {
            if (RunningMarketList == null)
            {
                return;
            }
            int targetCoinCount = RunningMarketList.Count;
            if (targetCoinCount == 0)
            {
                return;
            }
            double limitKoreanWon = RunningValue / targetCoinCount;
            limitKoreanWon = Math.Round(limitKoreanWon, 0);
            Console.WriteLine("Make Trade Function Call");
            Parallel.ForEach(RunningMarketList, marketCoin =>
            {
                double enabledKRW = myAccountInfo.TradeEnabledKRW;
                //////////////////////////////////////////////////////////////////////////
                ///매도 판단
                ///
                marketCoin.CheckAndUpdateLow();
                if (dayFlag == EDayFlag.PastOneDay)
                {
                    if (marketCoin.EnabledQuantity > 0) // 수량이 0개 이상이어야 매도 가능 
                    {
                        double sellingValue = marketCoin.MarketTicker.trade_price * marketCoin.EnabledQuantity;
                        sellingValue = Math.Floor(sellingValue);
                        OrderEnqForSell(marketCoin.MarketCode, sellingValue, marketCoin.AvgPrice, marketCoin.EnabledQuantity, 60);
                        marketCoin.UpdateLowRateList();
                        marketCoin.UpdateOpenLow();
                    }

                }


                //손절
                if (marketCoin.ShouldCutLoss)
                {
                    //float 
                    if (marketCoin.Quantity > 0)
                    {
                        if (marketCoin.GainLossValuationRatioComparedToBuyPoint < (-1) * marketCoin.CutLossRatio)
                        {
                            //손실 %가 손실 기준치보다 낮으므로 바로 손절하기
                            double sellingValue = marketCoin.MarketTicker.trade_price * marketCoin.EnabledQuantity;
                            sellingValue = Math.Floor(sellingValue);
                            OrderEnqForSell(marketCoin.MarketCode, sellingValue, marketCoin.AvgPrice, marketCoin.EnabledQuantity, 60);
                            //marketCoin.UpdateLowRateList();
                            //marketCoin.UpdateOpenLow();
                        }
                    }
                }

                //익절
                if (marketCoin.ShouldTakeProfit)
                {
                    //float 
                    if (marketCoin.Quantity > 0)
                    {
                        if (marketCoin.GainLossValuationRatioComparedToBuyPoint >= marketCoin.TakeProfitRatio)
                        {
                            double sellingValue = marketCoin.MarketTicker.trade_price * marketCoin.EnabledQuantity;
                            sellingValue = Math.Floor(sellingValue);
                            OrderEnqForSell(marketCoin.MarketCode, sellingValue, marketCoin.AvgPrice, marketCoin.EnabledQuantity, 60);
                        }
                    }
                }

                //if(marketCoin)



                //////////////////////////////////////////////////////////////////////////
                /////매수 판단
                ///
                double buyPrice = marketCoin.GetBuyPrice();

                if (buyPrice > 0)
                {
                    //if (marketCoin.Quantity != 0)
                    {
                        double curOwnValue = marketCoin.Quantity * marketCoin.MarketTicker.trade_price;
                        if (curOwnValue < limitKoreanWon)
                        {
                            if (marketCoin.MarketTicker.trade_price <= buyPrice) // 목표 매수가보다 작으면 매수 
                            {
                                Console.WriteLine("Buying Coin Process Begin");
                                Console.WriteLine("Coin Market Code : {0}, Quantity : {1} Current Value {2}", marketCoin.MarketCode, marketCoin.EnabledQuantity, curOwnValue);
                                if (curOwnValue < limitKoreanWon)
                                {
                                    double buyValue = limitKoreanWon / marketCoin.DividePriceGapNumber;
                                    buyValue = buyValue * (1 - BUY_FEE);
                                    double combinedOwnValueAfterBidding = curOwnValue + buyValue;
                                    if (combinedOwnValueAfterBidding > limitKoreanWon)
                                    {//매수를 했을 떄 할당된 매수 금액보다 크면 
                                        buyValue = limitKoreanWon - curOwnValue;
                                    }
                                    buyValue = Math.Round(buyValue, 0);
                                    if (enabledKRW >= buyValue)
                                    {
                                        OrderEnqForBuy(marketCoin.MarketCode, buyValue, 60);
                                        marketCoin.UpdateBuyPricePos();
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }

        private void DecideBuy()
        {

        }


        //private void AddMarketInfo()

        //private async Task BeforeGoRunning()
        //{
        //    dictStrCoin.Clear();
        //    Task<List<Account>> accountTask = m_API.GetAccount();
        //    List<Account> registeredAccountList = await accountTask;
        //    string[] selectedMarketInfos = this.m_SelectMarketInfo.Split(',');

        //    bool bKoreanWonDone = false;
        //    for (int index = 0; index < registeredAccountList.Count(); index++)
        //    {
        //        StringBuilder strBuilder = new StringBuilder();
        //        strBuilder.AppendFormat(registeredAccountList[index].currency);
        //        string marketKey = strBuilder.ToString();
        //        if(!bKoreanWonDone)
        //        {
        //            if (marketKey == "KRW")
        //            {
        //                bKoreanWonDone = true;
        //                continue;
        //            }
        //        }
        //        bool bAccountCoinAlreadySelected = false;
        //        for(int coinIdx = 0; coinIdx < selectedMarketInfos.Count(); coinIdx++)
        //        {
        //            if (selectedMarketInfos[coinIdx].Contains(marketKey))
        //            {
        //                bAccountCoinAlreadySelected = true;
        //                break;
        //            }
        //        }
        //        if(!bAccountCoinAlreadySelected)
        //        {
        //            StringBuilder newSelectBuilder = new StringBuilder(m_SelectMarketInfo);
        //            newSelectBuilder.Append(",");
        //            newSelectBuilder.Append("KRW-");
        //            newSelectBuilder.Append(marketKey);
        //            m_SelectMarketInfo = newSelectBuilder.ToString();
        //        }
        //    }
        //}
    }

    class MainTimer
    {
    }
}
