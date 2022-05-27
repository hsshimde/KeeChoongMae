using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using upbit.UpbitAPI;
using upbit.UpbitAPI.Model;

namespace upbit.View
{
    public partial class SelectCoinForm : Form
    {
        private List<MarketAll> m_ListMarketInfo;
        public EventHandler<StringBuilder> m_ehSaveSelectCoin;
        public string SelectedMarketInfo { get; set; }
        public APIClass api;
        public  SelectCoinForm(APIClass api)
        {
            InitializeComponent();
            this.api = api;
            SelectedMarketInfo = System.String.Empty;
        }

        public async Task Init(string strBeforeSelectedMarketInfo)
        {
            Task<List<MarketAll>> marketInfoTask = this.api.GetMarketAll();
            m_ListMarketInfo = await marketInfoTask;

            foreach (DataGridViewColumn item in dataGridView_Left.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            if(strBeforeSelectedMarketInfo != null)
            {
                SelectedMarketInfo = strBeforeSelectedMarketInfo;
                FillLeftGridWithoutBeforeSelected();
                RestoreBeforeSelectedOnRight();
            }
            else
            {
                FillLeftGridWithAllMarketInfoList();
            }
        }

        //public SelectCoinForm()
        //{
        //    InitializeComponent();
        //    marketList = new List<MarketAll>();
        //    string[] girlfriends = { "Dami", "Sai", "Nat", "Yoyo", "BigBoobWeatherGirl" };

        //    foreach (string girlFriend in girlfriends)
        //    {
        //        MarketAll newGirlFriend = new MarketAll();
        //        newGirlFriend.korean_name = girlFriend;
        //        newGirlFriend.market = girlFriend;
        //        marketList.Add(newGirlFriend);
        //    }
        //    //FillLeftGrid();
        //    FillLeftGridWithGirlName();
        //}

        private void FillLeftGridWithoutBeforeSelected()
        {
            dataGridView_Left.Rows.Clear();
            List<string> listBeforeSelectedMarketInfo = GetBeforeSelectedMarketInfo();

            foreach (MarketAll item in m_ListMarketInfo)
            {
                if (item.market.Contains("KRW-"))
                {
                    bool bShouldAdd = true;
                    if(listBeforeSelectedMarketInfo!=null)
                    {
                        for(int nIdx = 0; nIdx < listBeforeSelectedMarketInfo.Count; nIdx++)
                        {
                            if(item.market.CompareTo(listBeforeSelectedMarketInfo[nIdx]) == 0)
                            {
                                bShouldAdd = false;
                                break;
                            }
                        }
                    }
                    if(!bShouldAdd)
                    {
                        continue;
                    }
                    int nRow = dataGridView_Left.Rows.Add();
                    dataGridView_Left["LeftCol_MarketInfo", nRow].Value = item.market;
                    dataGridView_Left["LeftCol_KoreanName", nRow].Value = item.korean_name;
                }
            }
        }

        private void FillLeftGridWithAllMarketInfoList()
        {
            dataGridView_Left.Rows.Clear();
            foreach (MarketAll item in m_ListMarketInfo)
            {
                if (item.market.Contains("KRW-"))
                {
                    //bool bShouldAdd = true;
                    //if (listBeforeSelectedMarketInfo != null)
                    //{
                    //    for (int nIdx = 0; nIdx < listBeforeSelectedMarketInfo.Count; nIdx++)
                    //    {
                    //        if (item.market.CompareTo(listBeforeSelectedMarketInfo[nIdx]) == 0)
                    //        {
                    //            bShouldAdd = false;
                    //            break;
                    //        }
                    //    }
                    //}
                    //if (!bShouldAdd)
                    //{
                    //    continue;
                    //}
                    int nRow = dataGridView_Left.Rows.Add();
                    dataGridView_Left["LeftCol_MarketInfo", nRow].Value = item.market;
                    dataGridView_Left["LeftCol_KoreanName", nRow].Value = item.korean_name;
                }
            }
        }

        private void RestoreBeforeSelectedOnRight()
        {
            List<string> listBeforeSelectedMarketInfo = GetBeforeSelectedMarketInfo();
            if(listBeforeSelectedMarketInfo == null)
            {
                return;
            }
            foreach (MarketAll item in m_ListMarketInfo)
            {
                if (item.market.Contains("KRW-"))
                {
                    bool bShouldAdd = false;
                    for(int nIdx = 0; nIdx < listBeforeSelectedMarketInfo.Count; nIdx++)
                    {
                        if(item.market.CompareTo(listBeforeSelectedMarketInfo[nIdx]) == 0)
                        {
                            bShouldAdd = true;
                            break;
                        }
                        
                    }
                    if(!bShouldAdd)
                    {
                        continue;
                    }
                    int nRow = dataGridView_Right.Rows.Add();
                    dataGridView_Right["RightCol_MarketInfo", nRow].Value = item.market;
                    dataGridView_Right["RightCol_KoreanName", nRow].Value = item.korean_name;
                }
            }
        }

        private List<string> GetBeforeSelectedMarketInfo()
        {
            if (SelectedMarketInfo.Length < 1)
            {
                return null;
            }

            List<string> listMarketInfo = new List<string>();
            int nStartIdx = 0;
            int nFoundIdx = SelectedMarketInfo.IndexOf(",");
            while(nFoundIdx!=-1)
            {
                int nLength = nFoundIdx - nStartIdx;
                string strMarketInfo = SelectedMarketInfo.Substring(nStartIdx, nLength);
                listMarketInfo.Add(strMarketInfo);
                nStartIdx = nFoundIdx + 1;
                nFoundIdx = SelectedMarketInfo.IndexOf(",", nStartIdx);
            }

            return listMarketInfo;
        }
        private void FillLeftGridWithGirlName()
        {
            dataGridView_Left.Rows.Clear();
            foreach (MarketAll item in m_ListMarketInfo)
            {
                int nRow = dataGridView_Left.Rows.Add();
                dataGridView_Left["LeftCol_MarketInfo", nRow].Value = item.market;
                dataGridView_Left["LeftCol_KoreanName", nRow].Value = item.korean_name;
            }
        }

        private void RegisterItemsToRightGrid()
        {
            List<int> listRemovingRowIdx = new List<int>();
            foreach (DataGridViewRow row in dataGridView_Left.Rows)
            {
                if (Convert.ToBoolean(row.Cells["LeftCol_Select"].Value))
                {
                    string marketInfo = row.Cells["LeftCol_MarketInfo"].Value.ToString();
                    string itemKoreanName = row.Cells["LeftCol_KoreanName"].Value.ToString();

                    IEnumerable<DataGridViewRow> IEnumGridViewRow = dataGridView_Right.Rows.Cast<DataGridViewRow>();
                    IEnumerable<DataGridViewRow> IEnumFoundGridViewRow = IEnumGridViewRow.Where(x => x.Cells["RightCol_MarketInfo"].Value.Equals(marketInfo));
                    DataGridViewRow foundRightGridRow = IEnumFoundGridViewRow.FirstOrDefault();
                    if (foundRightGridRow == null)
                    {
                        listRemovingRowIdx.Add(row.Index);
                        int rightMovedRow = dataGridView_Right.Rows.Add();
                        dataGridView_Right["RightCol_MarketInfo", rightMovedRow].Value = marketInfo;
                        dataGridView_Right["RightCol_KoreanName", rightMovedRow].Value = itemKoreanName;
                    }
                }
            }
            //delete registered items in the option list.
            for (int nCount = 0; nCount < listRemovingRowIdx.Count; nCount++)
            {
                dataGridView_Left.Rows.RemoveAt(listRemovingRowIdx[nCount]);
                for (int nAdjustIdx = nCount + 1; nAdjustIdx < listRemovingRowIdx.Count; nAdjustIdx++)
                {
                    listRemovingRowIdx[nAdjustIdx]--;
                }

            }
            listRemovingRowIdx = null;
        }

        private void RestoreItemsToLeftGrid()
        {
            FillLeftGridWithAllMarketInfoList();
            List<int> listRemovingRowIdx = new List<int>();
            List<string> listRemovingRowMarketInfo = new List<string>();
            foreach (DataGridViewRow row in dataGridView_Right.Rows)
            {
                if (Convert.ToBoolean(row.Cells["RightCol_Select"].Value))
                {
                    listRemovingRowIdx.Add(row.Index);
                }
                else
                {
                    string marketInfo = row.Cells["RightCol_MarketInfo"].Value.ToString();
                    IEnumerable<DataGridViewRow> IEnumGridViewRowLeft = dataGridView_Left.Rows.Cast<DataGridViewRow>();
                    IEnumerable<DataGridViewRow> IEnumFoundGridViewRow = IEnumGridViewRowLeft.Where(x => x.Cells["LeftCol_MarketInfo"].Value.Equals(marketInfo));
                    DataGridViewRow foundRightGridRow = IEnumFoundGridViewRow.FirstOrDefault();
                    dataGridView_Left.Rows.RemoveAt(foundRightGridRow.Index);
                }
            }
            for (int nCount = 0; nCount < listRemovingRowIdx.Count; nCount++)
            {
                dataGridView_Right.Rows.RemoveAt(listRemovingRowIdx[nCount]);
                for (int nAdjustIdx = nCount + 1; nAdjustIdx < listRemovingRowIdx.Count; nAdjustIdx++)
                {
                    listRemovingRowIdx[nAdjustIdx]--;
                }
            }
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            if (sender.Equals(button_ToRight))
            {
                RegisterItemsToRightGrid();
            }
            else if (sender.Equals(button_ToLeft))
            {
                RestoreItemsToLeftGrid();
            }
            else if(sender.Equals(button_Save))
            {
                Save();
                return;
            }
            ClearGridSelection();
        }

        private void ClearGridSelection()
        {
            dataGridView_Right.ClearSelection();
            dataGridView_Left.ClearSelection();
        }

        private void Save()
        {
            StringBuilder sbSelectInfo = new StringBuilder();
            //StringBuilder sbSelectCoinName = new StringBuilder();
            foreach(DataGridViewRow row in dataGridView_Right.Rows)
            {
                string strMarketInfo = row.Cells["RightCol_MarketInfo"].Value.ToString();
                string strCoinName = row.Cells["RightCol_KoreanName"].Value.ToString();
                sbSelectInfo.AppendFormat(strMarketInfo);
                sbSelectInfo.AppendFormat(",");
                sbSelectInfo.AppendFormat(strCoinName);
                sbSelectInfo.AppendFormat(",");

                //sbSelectCoinName.AppendFormat(strCoinName);
                //sbSelectCoinName.AppendFormat(",");
            }
            
            //sbSelectInfo.Length--;
            m_ehSaveSelectCoin?.Invoke(this, sbSelectInfo);
            this.Close();
        }
    }
}
