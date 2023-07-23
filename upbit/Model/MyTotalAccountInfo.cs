using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upbit.Model
{
    public class MyTotalAccountInfo : IDisposable
    {
        public double KRW { get; set; }

        public double TotalAssetValueInKRWIncludingKRW { get; set; }

        public double TradeEnabledKRW { get; set; }

        public double SetRunningValue { get; set; }

        public double ProfitLossValue { get; set; }

        public double NetBuyValue { get; set; }

        public double ProfitLossRatio { get; set; }

        public MyTotalAccountInfo(double total, double KRW, double tradeEnabledKRW, double profitLossValue, double netBuyValue)
        {
            this.KRW = KRW;
            TotalAssetValueInKRWIncludingKRW = total;
            TradeEnabledKRW = tradeEnabledKRW;
            ProfitLossValue = profitLossValue;
            NetBuyValue = netBuyValue;
            ProfitLossRatio = ProfitLossValue / NetBuyValue;
        }

        public void Dispose()
        {


        }
    }
}
