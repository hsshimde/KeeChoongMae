using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upbit.UpbitAPI;
using upbit.UpbitAPI.Model;
using upbit.View;

namespace upbit.Controller
{
    public partial class Running
    {
        private System.Timers.Timer m_MainTimer;
        private APIClass m_API;
        public string m_SelectMarketInfo { get; set; }
        private MainForm m_MainForm;
        
        public Running(APIClass api, MainForm mainForm)
        {
            InitializeRunning(api);
            m_MainForm = mainForm;
        }

        public async Task Go()
        {
            await BeforeGoRunning();
            this.m_MainTimer.Start();
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
                await this.GetMainData();
            }
        }

    }


}
