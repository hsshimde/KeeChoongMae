using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            GainLossValuation,


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


        public int BuildColIdx()
        {
            int colIdx = 0;
            //switch (GridType)
            //{
            //    case EGridType.market:
            //        {
                        
            //        }
            //        break;


            //    case EGridType.myAsset:
            //        {

            //        }
            //        break;

            //}

            if(GridType == EGridType.market)
            {
                switch (ColItem)
                {
                    case EColItem.MarketCode:
                        {
                            colIdx = 0;
                        }
                        break;


                    case EColItem.CurPrice:
                        {
                            colIdx = 1;
                        }
                        break;

                    case EColItem.Compare24H:
                        {
                            colIdx = 2;
                        }
                        break;

                    case EColItem.TransVolume:
                        {
                            colIdx = 3;
                        }
                        break;

                    default:
                        {
                            Debug.Assert(false, "Col Item Idx Wrong");
                        }
                        break;
                }

            }
            else if(GridType == EGridType.myAsset)
            {
                switch (ColItem)
                {
                    case EColItem.MarketCode:
                        {
                            colIdx = 0;
                        }
                        break;


                    case EColItem.OwnCount:
                        {
                            colIdx = 1;
                        }
                        break;

                    case EColItem.AvgBuyPrice:
                        {
                            colIdx = 2;
                        }
                        break;

                    case EColItem.CurNetValue:
                        {
                            colIdx = 3;
                        }
                        break;

                    case EColItem.GainLossValuation:
                        {
                            colIdx = 4;
                        }
                        break;


                    case EColItem.BuyVolume:
                        {
                            colIdx = 6;
                        }
                        break;


                    case EColItem.CurProfitPercentage:
                        {
                            colIdx = 5;
                        }
                        break;

                    case EColItem.Compare24H:
                        {
                            colIdx = 7;
                        }
                        break;

                    default:
                        {
                            Debug.Assert(false, "Col Item Idx Wrong");
                        }
                        break;


                        //case 
                }
            }
            else
            {
                Debug.Assert(false, "Col Idx Wrong");
            }
            //return 


            //     public enum EColItem
            //{
            //    MarketCode,
            //    CurPrice,
            //    Compare24H,
            //    TransVolume,
            //    OwnCount,
            //    AvgBuyPrice,
            //    BuyVolume,
            //    CurNetValue,
            //    CurProfitPercentage,
            //    GainLossValuation,


            //    Count
            //};
            return colIdx;
        }





}
}
