using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upbit.Model;

namespace upbit.UpbitAPI.Model {
    public class MakeOrder : IDisposable {

        public string uuid;
        public string side;
        public string ord_type;
        public double price;
        public string state;
        public string market;
        public string created_at;
        public double volume;
        public double remaining_volume;
        public double reserved_fee;
        public double remaining_fee;
        public double paid_fee;
        public double locked;
        public double executed_volume;
        public int trades_count;
        //private Error error;
        public Error error;

        //internal Error Error { get => error; set => error = value; }

        public void Dispose() { }
    }
}
