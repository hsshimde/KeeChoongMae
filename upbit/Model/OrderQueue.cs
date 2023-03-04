using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upbit.Enum;
namespace upbit.Model
{
    public class MyOrder : IDisposable
    {
        public string market;
        public EnumClass.EOrderState OrderState { get; set; }
        public double OrderValue { get; set; }
        public double OrderQuantity { get; set; }
        public double OrderPrice { get; set; }
        public string uuid { get; set; }
        public DateTime OrderDateTime { get; set; }

        public double AvgPrice { get; set; }
        public double OwnQuantity { get; set; }

        public double WaitOrderSeconds;

        public MyOrder(string market, EnumClass.EOrderState state, double OrderValue, double waitOrderSeconds)
        {
            this.market = market;
            this.OrderState = state;
            this.OrderValue = OrderValue;
            this.WaitOrderSeconds = waitOrderSeconds;
            //bool bOK = state | EnumClass.EOrderState.WaitSell;
            uint IsSell = (uint)(state & EnumClass.EOrderState.WaitSell);
            if(IsSell != 0)
            {
                this.OrderState = EnumClass.EOrderState.WaitSell;
            }
            uint IsBuy = (uint)(state & EnumClass.EOrderState.WaitBuy);
            if(IsBuy != 0)
            {
                this.OrderState = EnumClass.EOrderState.WaitBuy;
            }

        }

       
        

        public double GetOrderQuantity(double curPrice)
        {
            this.OrderQuantity = Math.Round(OrderValue/curPrice,8);
            return this.OrderQuantity;
        }


        public void Dispose()
        {
            
        }

    }
}
