using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upbit.ColumnNameBuilder
{
    class ColNameBuilder 
    {
        public enum EUnitCurrency
        {
            KRW,
            BTC,
            USDT,

            Count
        };

        public enum EGridType
        {
            market,
            myAsset,


            Count   
        };


        public enum EColItem
        {
            MarketCode,
            CurPrice,
            Compare24H,
            TransVolume,
            OwnCount,
            AvgBuyPrice,
            BuyVolume,
            CurNetValue,
            CurProfitPercentage,


            Count
        };

        public EUnitCurrency UnitCurrency { get; set; }
        public EGridType GridType { get; set; }
        public EColItem ColItem { get; set; }

        public string BuildColName()
        {
            StringBuilder sbToString = new StringBuilder();
            sbToString.AppendFormat(GridType.ToString());
            sbToString.AppendFormat(UnitCurrency.ToString());
            sbToString.AppendFormat(ColItem.ToString());
            return sbToString.ToString();
        }

    }
}
