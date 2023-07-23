using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upbit.UpbitAPI.Model
{
    class Asset : IDisposable
    {
        public Account account{get; set; }
        public Coin coin { get; set; }


        public void Dispose()
        {
            
        }
    }
}
