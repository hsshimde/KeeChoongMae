using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using upbit.UpbitAPI;
using upbit.UpbitAPI.Model;
using upbit.View;

namespace upbit.Controller
{
    public partial class Running
    {
        private System.Timers.Timer m_MainTimer;
        private APIClass mAPI;
        public string m_strMyAssetCode { get; set; }
        public string m_SelectMarketInfo { get; set; }
        public string allMarketCode;
        private MainForm m_MainForm;
        
        public Running(APIClass api, MainForm mainForm)
        {
            Debug.Assert(api != null);
            Debug.Assert(mainForm != null);
            InitializeRunning(api);
            m_MainForm = mainForm;
        }

        public void Go()
        {
            //Debug.Assert(dictMarketCodeToCoin != null);
            //mDictAllMarketCode = dictMarketCodeToCoin;
            BeforeGoRunning();
            this.m_MainTimer.Start();
        }
        public void SetAllMarketInfo(Dictionary<string, Coin> dictMarketInfo)
        {
            Debug.Assert(dictMarketInfo != null);
            Debug.Assert(DictAllMarketInfo == null);
            DictAllMarketInfo = dictMarketInfo;
        }

        public void SetMyAssetInfo(Dictionary<string, CoinAccount> dictMyAssetInfo)
        {
            Debug.Assert(dictMyAssetInfo != null);
            Debug.Assert(DictMyAssetInfo == null);
            DictMyAssetInfo = dictMyAssetInfo;
        }

        //private void main()
        //{
        //    List<Ticker> tickerList = API.GetTicker(strSelectCoin);
        //    if(tickerList!=null)
        //    {
        //        foreach(Ticker tickerItem in tickerList)
        //        {
        //            Console.WriteLine($"{tickerItem.market} {tickerItem.trade_date_kst} {tickerItem.trade_price}");
        //        }
        //    }
        //}

        public void Stop()
        {
            this.m_MainTimer.Stop();
        }


        private async void ElapsedEventReceiver(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(sender.Equals(this.m_MainTimer))
            {
                await DoUpdate();
                await GetMainData();
            }
        }

    }


}
