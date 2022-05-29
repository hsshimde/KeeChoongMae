using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using upbit.UpbitAPI.Model;
namespace upbit.View
{
    internal class MonitoringEvent
    {
    }

    public partial class MainForm
    {
        private void AddMonitoringGridCol(StringBuilder marketInfoBuilder, StringBuilder coinNameBuilder)
        {
            string marketInfos = marketInfoBuilder.ToString();
            string coinNames = coinNameBuilder.ToString();
            string[] marketArray = marketInfos.Split(',');
            string[] coinNamesArray = coinNames.Split(',');
            int nArraySize = marketArray.Count();
            Debug.Assert(coinNamesArray.Count() == nArraySize);
            for(int nIdx = 0; nIdx < nArraySize; nIdx++)
            {
                int row = dataGridView_Monitoring.Rows.Add();
                dataGridView_Monitoring["Monitoring_marketInfo", row].Value = marketArray[nIdx];
                dataGridView_Monitoring["Monitoring_koreanName", row].Value = coinNamesArray[nIdx];
            }
        }

        private void OnMonitoringEvent(object sender, Monitoring e)
        {
            if(dataGridView_Monitoring.InvokeRequired)
            {
                dataGridView_Monitoring.Invoke((MethodInvoker)delegate ()
                {
                    IEnumerable<DataGridViewRow> updateRowEnum = dataGridView_Monitoring.Rows.Cast<DataGridViewRow>();
                    DataGridViewRow updateRow = updateRowEnum.Where(x => x.Cells["Monitoring_marketInfo"].Value.Equals(e.MarketInfo)).FirstOrDefault();

                    if (updateRow!= null)
                    {
                        updateRow.Cells["Monitoring_curPrice"].Value = e.CurPrice;
                        updateRow.Cells["Monitoring_24H"].Value = e._24Hour.ToString("N2");
                    }

                });
                
            }
        }
    }

}
