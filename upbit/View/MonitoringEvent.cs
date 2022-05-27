using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using upbit.UpbitAPI.Model;
namespace upbit.View
{
    internal class MonitoringEvent
    {
    }

    public partial class MainForm
    {
        private void AddMonitoringGridCol(string marketInfos)
        {
            string[] marketArray = marketInfos.Split(',');
            foreach(string marketInfo in marketArray)
            {
                int row = dataGridView_Monitoring.Rows.Add();
                dataGridView_Monitoring["Monitoring_market", row].Value = marketInfo;
            }
        }

        private void OnMonitoringEvent(object sender, Monitoring e)
        {
            if(dataGridView_Monitoring.InvokeRequired)
            {
                dataGridView_Monitoring.Invoke((MethodInvoker)delegate ()
                {
                    IEnumerable<DataGridViewRow> updateRowEnum = dataGridView_Monitoring.Rows.Cast<DataGridViewRow>();
                    DataGridViewRow updataRow = updateRowEnum.
                });
            }
        }
    }

}
