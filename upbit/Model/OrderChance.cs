using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upbit.UpbitAPI.Model {
    public class OrderChance : IDisposable {

        public double bid_fee;
        public double ask_fee;
        public MarketInfo market;
        public OrderChanceAccount bid_account;
        public OrderChanceAccount ask_account;

        public class MarketInfo {

            public string id;
            public string name;
            public string[] order_types;
            public string[] order_sides;
            public MarketBidAsk bid;
            public MarketBidAsk ask;
            public double max_total;
            public string state;

            public class MarketBidAsk{
                public string currency;
                public string price_unit;
                public double min_total;
            }


        }
        public class OrderChanceAccount {
            public string currency;
            public double balance;
            public double locked;
            public double avg_buy_price;
            public bool avg_buy_price_modified;
            public string unit_currency;

        }
        

        public void Dispose() { }
    }
}
