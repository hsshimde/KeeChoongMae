using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upbit.UpbitAPI.Model
{
    public class Monitoring : IDisposable
    {
        public string MarketInfo { get; set; }
        public double CurPrice { get; set; }
        public double _24Hour { get; set; }

        public Monitoring(string marketInfo, double curPrice, double _24h)
        {
            MarketInfo = marketInfo;
            CurPrice = curPrice;
            _24Hour = _24h;
        }

        public void Dispose()
        {

        }


    }
}
