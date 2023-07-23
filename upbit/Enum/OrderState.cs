using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upbit.Enum
{
    public static partial class EnumClass
    {
        public enum EOrderState
        {
            WaitBuy = 1 << 0,
            Buying = 1 << 1,
            IncompleteBuyOrder = 1 << 2,
            WaitSell = 1 << 3,
            Selling = 1 << 4,
            IncompleteSellOrder = 1 << 5
        }

        public enum EIsRunning
        {
            Running,
            Paused,
        }


        public enum EDayFlag
        {
            PastOneDay,
            NotYet
        };
    }
}
