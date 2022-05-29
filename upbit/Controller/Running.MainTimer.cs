using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upbit.UpbitAPI;
using upbit.UpbitAPI.Model;

namespace upbit.Controller
{

    public partial class Running
    {
        public EventHandler<CoinAccount> ehUpdateCoinAccount;
        public EventHandler<Monitoring> ehUpdateMonitoring;

        public EventHandler<string> ehRemoveAccount;
        public EventHandler<double> ehUpdateTotalAsset;
        public EventHandler<double> ehRemoveTotalAsset;

        private Dictionary<string, Coin> dictStrCoin;

        private async Task GetMainData()
        {
            Task<List<Account>> accountTask = m_API.GetAccount();
            List<Account> registeredAccountList = await accountTask;
            
            Task<List<Ticker>> TickerTask = m_API.GetTicker(m_SelectMarketInfo);
            List<Ticker> tickerList = await TickerTask;
            List<Account> registeredAccountListCopy = new List<Account>(registeredAccountList);



            if (registeredAccountList != null && tickerList != null)
            {
                GetTicker(tickerList);
                GetAccountInfo(registeredAccountList);
                GetMyTotalAsset(registeredAccountListCopy);
            }
            else
            {
                m_MainForm.ConnectionNotWorking();
            }

            Console.WriteLine("Execution Complete");

        }

        private void InitializeRunning(APIClass api)
        {
            this.m_MainTimer = new System.Timers.Timer();
            this.m_MainTimer.Interval = 500; // 1000ms = 1 second
            this.m_MainTimer.Elapsed += ElapsedEventReceiver;
            dictStrCoin = new Dictionary<string, Coin>();
            this.m_API = api;
        }

        private void GetTicker(List<Ticker> listTicker)
        {
            //Receive current price and process transactions
            //DO Parallel
            Parallel.ForEach(listTicker, item =>
            {
                if (dictStrCoin.ContainsKey(item.market))
                {
                    using (Coin coin = dictStrCoin[item.market])
                    {
                        double curPrice = item.trade_price;
                        coin.CurPrice = item.trade_price;
                        double _24h = CalculateProfit(item.opening_price, curPrice);
                        using (Monitoring monitoring = new Monitoring(coin.MarketInfo, coin.CurPrice, _24h))
                        {
                            ehUpdateMonitoring?.Invoke(this, monitoring);
                        }
                    }
                }
                else
                {
                    Coin coin = new Coin(item.market);
                    coin.CurPrice = item.trade_price;
                    lock (dictStrCoin)
                    {
                        dictStrCoin.Add(coin.MarketInfo, coin);
                    }
                }
            });



        }

        private void GetAccountInfo(List<Account> listAccount)
        {
            //double KRW = 0;
            //double total = 0;
            using (Account account = listAccount.Where(x => x.currency.Equals("KRW")).FirstOrDefault())
            {
                //double quantity = account.balance + account.locked;
                //double price = account.avg_buy_price;
                //total += quantity;
                listAccount.Remove(account);
            }
            foreach (KeyValuePair<string, Coin> items in dictStrCoin)
            {
                using (Account account = listAccount.Where(x => items.Key.Contains(x.currency)).FirstOrDefault())
                {
                    if (account != null)
                    {
                        items.Value.Quantity = account.balance + account.locked;
                        items.Value.AvgPrice = account.avg_buy_price;
                        items.Value.Profit = CalculateProfit(items.Value.AvgPrice, items.Value.CurPrice);
                        double curValue = items.Value.Quantity * items.Value.CurPrice;
                        //total += curValue;

                        using (CoinAccount ca = new CoinAccount(items.Key, items.Value.Profit, items.Value.Quantity, items.Value.CurPrice, items.Value.AvgPrice))
                        {
                            ehUpdateCoinAccount?.Invoke(this, ca);
                        }

                        //}
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

        public void GetMyTotalAsset(List<Account> totalAccounts)
        {
            double total = 0;
            foreach (Account account in totalAccounts)
            {
                double quantity = account.balance + account.locked;
                double price = account.avg_buy_price;
                double curValue;
                bool bKoreanWonAdded = false;
                if (!bKoreanWonAdded)
                {
                    if (account.currency == "KRW")
                    {
                        curValue = quantity;
                        bKoreanWonAdded = true;
                    }
                    else
                    {
                        curValue = price * quantity;
                    }
                }
                else
                {
                    curValue = price * quantity;
                }

                total += curValue;
            }
            ehUpdateTotalAsset?.Invoke(this, total);
        }

        private double CalculateProfit(double dblOpen, double dblClose)
        {
            return Math.Round(100 * ((dblClose / dblOpen) - 1), 2);
        }

        private async Task BeforeGoRunning()
        {
            dictStrCoin.Clear();
            Task<List<Account>> accountTask = m_API.GetAccount();
            List<Account> registeredAccountList = await accountTask;
            string[] selectedMarketInfos = this.m_SelectMarketInfo.Split(',');

            bool bKoreanWonDone = false;
            for (int index = 0; index < registeredAccountList.Count(); index++)
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat(registeredAccountList[index].currency);
                string marketKey = strBuilder.ToString();
                if(!bKoreanWonDone)
                {
                    if (marketKey == "KRW")
                    {
                        bKoreanWonDone = true;
                        continue;
                    }
                }
                bool bAccountCoinAlreadySelected = false;
                for(int coinIdx = 0; coinIdx < selectedMarketInfos.Count(); coinIdx++)
                {
                    if (selectedMarketInfos[coinIdx].Contains(marketKey))
                    {
                        bAccountCoinAlreadySelected = true;
                        break;
                    }
                }
                if(!bAccountCoinAlreadySelected)
                {
                    StringBuilder newSelectBuilder = new StringBuilder(m_SelectMarketInfo);
                    newSelectBuilder.Append(",");
                    newSelectBuilder.Append("KRW-");
                    newSelectBuilder.Append(marketKey);
                    m_SelectMarketInfo = newSelectBuilder.ToString();
                }
            }
        }
    }

    class MainTimer
    {
    }
}
