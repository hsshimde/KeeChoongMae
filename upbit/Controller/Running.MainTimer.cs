using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upbit.UpbitAPI;
using upbit.UpbitAPI.Model;

namespace upbit.Controller
{
    public partial class Running
    {

        //struct AssetInfo
        //{
        //    public Coin myCoin { get; set; }
        //    public Account account { get; set; }
        //}



        public EventHandler<CoinAccount> ehUpdateCoinAccount;
        public EventHandler<Monitoring> ehUpdateMonitoring;

        public EventHandler<string> ehRemoveAccount;
        public EventHandler<double> ehUpdateTotalAsset;
        public EventHandler<double> ehRemoveTotalAsset;
        public EventHandler<Coin> ehUpdateTicker;
        public Dictionary<string, CoinAccount> DictMyAssetInfo { get; private set; }
        public Dictionary<string, Coin> DictAllMarketInfo { get; private set; }

        private Dictionary<string, Coin> mDictSelectedMarketCode;


        //private List<string> m_listMyAsset;
        //private Dictionary<string, Coin> m_dictMyAsset;


        private async Task GetMainData()
        {
            Task<List<Account>> accountTask = mAPI.GetAccount();
            List<Account> myAssetAccount = await accountTask;
            if (myAssetAccount!=null)
            {
                GetMyAsset(myAssetAccount);
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

            Console.WriteLine("Update Execution Complete");
        }

        public async Task DoUpdate()
        {
            await updateAllTickerData();
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
            mappedCoin.AccumulateTradePrice = ticker.acc_trade_price_24h;
            mappedCoin.CurPrice = ticker.trade_price;
            float fCompared24H = (float)CalculateProfit(ticker.prev_closing_price, ticker.trade_price);
            mappedCoin.Compare24H = fCompared24H;
        }


        private void InitializeRunning(APIClass api)
        {
            this.m_MainTimer = new System.Timers.Timer();
            this.m_MainTimer.Interval = 500; // 1000ms = 1 second
            this.m_MainTimer.Elapsed += ElapsedEventReceiver;
            mDictSelectedMarketCode = new Dictionary<string, Coin>();
            //DictMyAssetInfo = new Dictionary<string, CoinAccount>();
            this.mAPI = api;
        }

        private void GetTicker(List<Ticker> listTicker)
        {
            //Receive current price and process transactions
            //DO Parallel
            Parallel.ForEach(listTicker, item =>
            {
                if (mDictSelectedMarketCode.ContainsKey(item.market))
                {
                    using (Coin coin = mDictSelectedMarketCode[item.market])
                    {
                        double curPrice = item.trade_price;
                        coin.CurPrice = item.trade_price;
                        double _24h = CalculateProfit(item.opening_price, curPrice);
                        using (Monitoring monitoring = new Monitoring(coin.MarketCode, coin.CurPrice, _24h))
                        {
                            //ehUpdateMonitoring?.Invoke(this, monitoring);
                        }
                    }
                }
                else
                {
                    Coin coin = new Coin(item.market, item.market, item.market);
                    coin.CurPrice = item.trade_price;
                    lock (mDictSelectedMarketCode)
                    {
                        mDictSelectedMarketCode.Add(coin.MarketCode, coin);
                    }
                }
            });



        }

        private void SetCurrentAccountInfo(List<Account> listAccount)
        {
            StringBuilder sbMarketCode = new StringBuilder();
            bool bKoreanWonIgonored = false;
            for (int nIdx = 0; nIdx < listAccount.Count; nIdx++)
            {
                sbMarketCode.Clear();
                Account account = listAccount[nIdx];
                sbMarketCode.AppendFormat(account.unit_currency);
                sbMarketCode.AppendFormat("-");
                sbMarketCode.AppendFormat(account.currency);

                string marketCode = sbMarketCode.ToString();
                Coin assetCoin = null;
                if(!bKoreanWonIgonored && marketCode =="KRW-KRW")
                {
                    bKoreanWonIgonored = true;
                    continue;
                }
                else
                {
                    assetCoin = DictAllMarketInfo[marketCode];
                }
                Debug.Assert(assetCoin != null);
                CoinAccount myCoinAccount = new CoinAccount(marketCode, 0, account.balance, 0, account.avg_buy_price);
                myCoinAccount.CoinNameKor = assetCoin.CoinNameKor;
                myCoinAccount.CoinNameEng = assetCoin.CoinNameEng;
                myCoinAccount.GridRowNumber = -1;
                DictMyAssetInfo.Add(myCoinAccount.MarketCode, myCoinAccount); 
            }

        }

        private void GetAccountInfo(List<Account> listAccount)
        {
            using (Account account = listAccount.Where(x => x.currency.Equals("KRW")).FirstOrDefault())
            {
                double quantity = account.balance + account.locked;
                double price = account.avg_buy_price;
                listAccount.Remove(account);
            }
            foreach (KeyValuePair<string, Coin> items in mDictSelectedMarketCode)
            {
                using (Account account = listAccount.Where(x => items.Key.Contains(x.currency)).FirstOrDefault())
                {
                    if (account != null)
                    {
                        items.Value.Quantity = account.balance + account.locked;
                        items.Value.AvgPrice = account.avg_buy_price;
                        items.Value.Profit = CalculateProfit(items.Value.AvgPrice, items.Value.CurPrice);
                        double curValue = items.Value.Quantity * items.Value.CurPrice;
                        using (CoinAccount ca = new CoinAccount(items.Key, items.Value.Profit, items.Value.Quantity, items.Value.CurPrice, items.Value.AvgPrice))
                        {
                            ehUpdateCoinAccount?.Invoke(this, ca);
                        }
                    }
                    else
                    {
                        items.Value.Quantity = 0;
                        items.Value.AvgPrice = 0;
                        items.Value.Profit = 0;
                        //ehRemoveAccount?.Invoke(this, items.Key);
                    }
                    //items.Value.dblQuantity = account.balance + account.locked;
                    //items.Value.dblAvgPrice = account.avg_buy_price;
                    //items.Value.dblProfit = CalculateProfit(items.Value.dblAvgPrice, items.Value.dblCurPrice);
                }
            }
        }

        public void GetMyAsset(List<Account> listAccount)
        {
            double dblAssetAccumulated = 0.0;
            using (Account account = listAccount.Where(x => x.currency.Equals("KRW")).FirstOrDefault())
            {
                double quantity = account.balance + account.locked;
                dblAssetAccumulated += quantity;
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
                tickerCoinAccount.CurPrice = tickerCoin.CurPrice;
                tickerCoinAccount.ProfitPercentageBuy = CalculateProfit(tickerCoinAccount.AvgBuyPrice, tickerCoinAccount.CurPrice);
                tickerCoinAccount.ProfitPercentageCompare24H = tickerCoin.Compare24H;
                
                double CurNetValue = tickerCoinAccount.CurPrice * tickerCoinAccount.Quantity;
                dblAssetAccumulated += CurNetValue;
                tickerCoinAccount.CurNetValue = CurNetValue;
                ehUpdateCoinAccount?.Invoke(this, tickerCoinAccount);
            }
            ehUpdateTotalAsset?.Invoke(this, dblAssetAccumulated);
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
        private void BeforeGoRunning()
        {
            //Task<List<MarketAll>> allMarketInfoTask = m_API.GetMarketAll();
            //List<MarketAll> allMarketInfo = await allMarketInfoTask;
            //await DivideByUnitCurrnecy();
            //Task<List<Account>> taskMyAssetAccount = m_API.GetAccount();
            //List<Account> myAssetAccount = await taskMyAssetAccount;
            //SetCurrentAccountInfo(myAssetAccount);
            //await Task;
        }

        public async Task InitializeCoinAccountInfo()
        {
            Task<List<Account>> taskMyAssetAccount = mAPI.GetAccount();
            List<Account> myAssetAccount = await taskMyAssetAccount;
            SetCurrentAccountInfo(myAssetAccount);
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
