using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using upbit.UpbitAPI.Model;

namespace upbit.View
{
    internal class AccountEvent
    {
    }

    public partial class MainForm
    {
        private void OnEventAccountAdd(object sender, Account account)
        {
            if(dataGridView_Account.InvokeRequired)
            {
                dataGridView_Account.Invoke((MethodInvoker)delegate () 
                {
                    //DataGridViewRow updateRow = dataGridView_Account.Rows.Cast
                    //IEnumerable<DataGridViewRow> IERows = dataGridView_Account.Rows.Cast<DataGridViewRow>();
                    //DataGridViewRow = IERows.Where(x=>x.Cells["account_market"].Value.Equals(account.market)))
                });
            }
        }
        private void OnEventAccountUpdate(object sender, CoinAccount account)
        {
            if (dataGridView_Account.InvokeRequired)
            {
                dataGridView_Account.Invoke((MethodInvoker)delegate ()
                {
                    IEnumerable<DataGridViewRow> IERows = dataGridView_Account.Rows.Cast<DataGridViewRow>();
                    DataGridViewRow updateRow = IERows.Where(x => x.Cells["account_market"].Value.Equals(account.strMarketCode)).FirstOrDefault();
                    if (updateRow != null)
                    {
                        updateRow.Cells["account_profit"].Value = account.dblProfit.ToString("0.00\\");
                        updateRow.Cells["account_quant"].Value = account.dblQuantity;
                        updateRow.Cells["account_avgPrice"].Value = Rounding(account.dblAvgPrice);
                        updateRow.Cells["account_curPrice"].Value = Rounding(account.dblCurPrice);
                        if(account.dblProfit > 0)
                        {
                            updateRow.Cells["account_profit"].Style.ForeColor = Color.Tomato;
                        }
                        else if(account.dblProfit < 0)
                        {
                            updateRow.Cells["account_profit"].Style.ForeColor = Color.DodgerBlue;
                        }
                        else
                        {
                            updateRow.Cells["account_profit"].Style.ForeColor = Color.Black;
                        }

                    }
                    else
                    {
                        int nRow = dataGridView_Account.Rows.Add();
                        dataGridView_Account["account_market", nRow].Value = account.strMarketCode;
                        dataGridView_Account["account_profit", nRow].Value = account.dblProfit.ToString("0.00\\");
                        dataGridView_Account["account_quant", nRow].Value = account.dblQuantity;
                        dataGridView_Account["account_avgPrice",nRow].Value = Rounding(account.dblAvgPrice);
                        dataGridView_Account["account_curPrice", nRow].Value = Rounding(account.dblCurPrice);
                    }
                });
            }
        }

        private void OnEventAccountDelete(object sender, string strMarketInfo)
        {

            if (dataGridView_Account.InvokeRequired)
            {
                dataGridView_Account.Invoke((MethodInvoker)delegate () 
                {
                    IEnumerable<DataGridViewRow> IEDeleteRow = dataGridView_Account.Rows.Cast<DataGridViewRow>();
                    DataGridViewRow deleteRow = IEDeleteRow.Where(x => x.Cells["account_market"].Value.Equals(strMarketInfo)).FirstOrDefault();
                    
                    if(deleteRow!=null)
                    {
                        dataGridView_Account.Rows.Remove(deleteRow);
                    }
                });

            }
        }

        private string Rounding(double dblNum)
        {
            
            if(dblNum >=100)
            {
                return dblNum.ToString("C0");
            }
            else if(dblNum >= 1)
            {
                return dblNum.ToString("C2");
            }
            else
            {
                return dblNum.ToString("C4");
            }
        }

        private void OnUpdateTotalAsset(object sender, double dblTotal)
        {
            toolStripLabel_totalAsset.Text = dblTotal.ToString("C0");
        }
    }

}
