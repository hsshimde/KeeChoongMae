using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace upbit.View
{
    internal class MainFormButton
    {
    }

    public partial class MainForm
    {
        private SelectCoinForm m_selectCoinForm;
        private string m_strSelectMarketInfo;
        private string m_strCoinName;
        private bool m_bIsRunning;
        private async void OnButtonClickedFromMainForm(object sender, EventArgs e)
        {
            //Loading Ticker Info of Chosen Items
            if(sender.Equals(toolStripButton_START))
            {
                StartKeeChoongMae();
            }
            //Show Form to select coin
            else if(sender.Equals(toolStripButton_SelectCoin))
            {
                SelectTargetItems();
                await m_selectCoinForm.Init(m_strSelectMarketInfo);
            }
            //Stop Renewing Ticker Info 
            else if(sender.Equals(toolStripButton_STOP))
            {
                StopKeeChoongMae();
            }
        }

        private void OnSaveSelectCoin(object sender, StringBuilder sb)
        {
            string strInput = sb.ToString();
            StringBuilder sbMarketInfo = new StringBuilder();
            StringBuilder sbCoinName = new StringBuilder();
            int nStartIdx = 0;
            int nFoundIdx = strInput.IndexOf(",");
            int nLength = nFoundIdx - nStartIdx;
            while (nFoundIdx != -1)
            {
                string strMarketInfo = strInput.Substring(nStartIdx, nLength);
                sbMarketInfo.AppendFormat(strMarketInfo);
                sbMarketInfo.AppendFormat(",");

                nStartIdx = nFoundIdx + 1;
                nFoundIdx = strInput.IndexOf(",", nStartIdx);
                nLength = nFoundIdx - nStartIdx;

                string strCoinName = strInput.Substring(nStartIdx, nLength);
                sbCoinName.AppendFormat(strCoinName);
                sbCoinName.AppendFormat(",");

                nStartIdx = nFoundIdx + 1;
                nFoundIdx = strInput.IndexOf(",", nStartIdx);
                nLength = nFoundIdx - nStartIdx;
            }
            if(sbMarketInfo.Length < 1 || sbCoinName.Length < 1)
            {
                m_strSelectMarketInfo = string.Empty;
                m_strCoinName = string.Empty;
            }
            else
            {
                m_strSelectMarketInfo = sbMarketInfo.ToString();
                m_strCoinName = sbCoinName.ToString();
            }
            sbMarketInfo.Clear();
            sbCoinName.Clear();
            sbMarketInfo = null;
            sbCoinName = null;
            bCoinSelectDone = true;
            ResetKeeChoongMae();
        }

        private void ClearCoinAccountDataGrid()
        {
            dataGridView_Account.Rows.Clear();
        }

        private void ResetKeeChoongMae()
        {
            if (m_bIsRunning)
            {
                running.Stop();
                m_bIsRunning = false;
                ClearCoinAccountDataGrid();
            }
            FillAccountGridWithSelectedItems();
            if(this.m_strSelectMarketInfo.Length < 1)
            {
                this.m_strSelectMarketInfo = string.Empty;
                return;
            }
            running.m_strSelection = this.m_strSelectMarketInfo.Substring(0, m_strSelectMarketInfo.Length - 1);
        }

        private void FillAccountGridWithSelectedItems()
        {
            dataGridView_Account.Rows.Clear();
            //List<string> listCoinName = new List<string>();
            int nCoinNameStartIdx = 0;
            int nCoinNameFoundIdx = m_strCoinName.IndexOf(",");
            int nMarketInfoStartIdx = 0;
            int nMarketInfoFoundIdx = m_strSelectMarketInfo.IndexOf(",");
            if(nCoinNameFoundIdx == -1)
            {
                dataGridView_Account.Rows.Clear();
                return;
            }
            
            while (nCoinNameFoundIdx != -1)
            {
                int nCoinNameLength = nCoinNameFoundIdx - nCoinNameStartIdx;
                string strCoinName = m_strCoinName.Substring(nCoinNameStartIdx, nCoinNameLength);

                int nMarketInfoLength = nMarketInfoFoundIdx - nMarketInfoStartIdx;
                string strMarketInfo = m_strSelectMarketInfo.Substring(nMarketInfoStartIdx, nMarketInfoLength);

                int nRow = dataGridView_Account.Rows.Add();
                dataGridView_Account["account_coinName", nRow].Value = strCoinName;
                dataGridView_Account["account_market", nRow].Value = strMarketInfo;
                dataGridView_Account["account_profit", nRow].Value = "-";
                dataGridView_Account["account_quant", nRow].Value = "-";
                dataGridView_Account["account_avgPrice", nRow].Value = "-";
                dataGridView_Account["account_curPrice", nRow].Value = "-";

                nCoinNameStartIdx = nCoinNameFoundIdx + 1;
                nCoinNameFoundIdx = m_strCoinName.IndexOf(",", nCoinNameStartIdx);

                nMarketInfoStartIdx = nMarketInfoFoundIdx + 1;
                nMarketInfoFoundIdx = m_strSelectMarketInfo.IndexOf(",", nMarketInfoStartIdx);
            }
        }

        private void StartKeeChoongMae()
        {
            if (this.bCoinSelectDone)
            {
                if(!m_bIsRunning)
                {
                    AddMonitoringGridCol(this.m_strSelectMarketInfo);
                    m_bIsRunning = true;
                    running.Go();
                    toolStripButton_START.Enabled = false;
                    toolStripButton_SelectCoin.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("설정 완료 후 시작 버튼을 눌러주세요");
            }

            Console.WriteLine("시작 ㅎㅎㅎㅎ");
        }

        private void SelectTargetItems()
        {
            m_selectCoinForm = new SelectCoinForm(this.m_API);
            m_selectCoinForm.FormClosing += this.OnFormClosingEvent;
            m_selectCoinForm.m_ehSaveSelectCoin += OnSaveSelectCoin;
            m_selectCoinForm.StartPosition = FormStartPosition.WindowsDefaultLocation;
            m_selectCoinForm.Owner = this;
            m_selectCoinForm.Location = this.Location;
            //m_selectCoinForm.SelectedMarketInfo = strSelectedBefore;
            m_selectCoinForm.Show();
        }

        private void StopKeeChoongMae()
        {
            if(m_bIsRunning)
            {
                bCoinSelectDone = false;
                m_bIsRunning = false;
                running.Stop();
                ResetKeeChoongMae();
            }
            else
            {
                MessageBox.Show("기충매가 실행 중이 아닙니다.");
            }
        }
    }

}
