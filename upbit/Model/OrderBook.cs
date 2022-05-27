using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upbit.UpbitAPI.Model {
    public class OrderBook : IDisposable {
        public string market;
        public double timestamp;
        public double total_ask_size;
        public double total_bid_size;
        public List<OrderBookUnit> orderbook_units;

        public class OrderBookUnit {
            public double ask_price;
            public double bid_price;
            public double ask_size;
            public double bid_size;

        }
        public void Dispose() { }
    }
}
