using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upbit.Controller
{
    public partial class Running
    {
        private void CheckAndStartOrderTimer()
        {
            if(mInsertOrderQueue.Count>0)
            {
                if(!mOrderTimer.Enabled)
                {
                    mOrderTimer.Start();
                }
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
