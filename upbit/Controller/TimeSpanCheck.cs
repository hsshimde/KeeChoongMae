using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static upbit.Controller.Running;
using static upbit.Enum.EnumClass;

namespace upbit.Controller
{
    class TimeSpanCheck
    {
        private DateTime mNextDateTime;


        public TimeSpanCheck(DateTime now)
        {
            updateTime(now);
        }

        public EDayFlag IsPassedOneDay(DateTime now)
        {
            TimeSpan diff = now - this.mNextDateTime;
            if(diff.TotalHours > 0)
            {
                updateTime(now);

                return Enum.EnumClass.EDayFlag.PastOneDay;
            }
            else
            {
                return Enum.EnumClass.EDayFlag.NotYet;
            }
        }

        private void updateTime(DateTime now)
        {
            mNextDateTime = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0);
            mNextDateTime = mNextDateTime.AddDays(1);
        }  

    }
}
