using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upbit.UpbitAPI.Model;

namespace upbit.UpbitAPI.Model
{
    public class TradeMarketSetting
    {
        public Coin MarketCoin { get; private set; }
        public bool bUseStatus { get; set; }
        public bool bCutLossUseStatus { get; set; }
        public float CutLossRatio { get; set; }

        public int AlgorithmIdx { get; set; }



        //TradeMarketSetting
        public TradeMarketSetting(Coin marketCoin, bool bChecked)
        {
            MarketCoin = marketCoin;
            bUseStatus = bChecked;
        }
    }
}
