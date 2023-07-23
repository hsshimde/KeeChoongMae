using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upbit.Model;

namespace upbit.Controller
{
    public partial class Running
    {
        private void CheckAndStartOrderTimer()
        {
            if(false == mOrderTimer.Enabled )
            {
                MyOrder orderData;
                if(mInsertOrderQueue.TryPeek(out orderData))
                {
                    mOrderTimer.Start();
                }
               
                //if (mInsertOrderQueue.Count > 0)
                //{
                //    if (!mOrderTimer.Enabled)
                //    {
                //        mOrderTimer.Start();
                //    }
                //}

            }
        }

        private void CheckAndStopOrderTimer()
        {
            if(mInsertOrderQueue.Count <= 0)
            {
                if(mOrderTimer.Enabled)
                {
                    mOrderTimer.Stop();
                }
            }
        }
    }

}
 