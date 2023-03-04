using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using upbit.UpbitAPI.Model;

namespace upbit.View
{
    internal class SearchBox { }

    public partial class MainForm
    {
        private bool mbCoinSymbolNameInitialized;
        //EMarketGridTabIdx
        public List<string> ListCoinNameSymbolKRW;
        public List<string> ListCoinNameSymbolBTC;
        public List<string> ListCoinNameSymbolUSDT;
        public List<string> ListCoinNameSymbolInterset;

        public Dictionary<int, Coin> DictCoinByRowNumMarketKRW { get; set; }
        public Dictionary<int, Coin> DictCoinByRowNumMarketBTC { get; set; }
        public Dictionary<int, Coin> DictCoinByRowNumMarketUSDT { get; set; }
        public Dictionary<int, Coin> DictCoinByRowNumMarketInterest { get; set; }
            
        

        //public List<string> listCoinNameSymbol[EMarketGridTabIdx.Count];

        private void textBox_SearchBox_TextChanged(object sender, EventArgs e)
        {
            string input = textBox_SearchBox.Text;

        }

        private  void textBoxMarketSearchBox_TextChanged(object sender, EventArgs e)
        {
            string coinSymbolInput = textBoxMarketSearchBox.Text;
            //if (coinSymbolInput.Length < 1)
            //{
            //    dgvMarketKRW.Rows.Clear();
            //    Task<bool> taskResetMarketGrid = ResetMarketGrid(EGridKind.KRW);
            //    bool bUpdateAllMarketData = await taskResetMarketGrid;
            //    //running.SetUpdateAllMarketData(bUpdateAllMarketData);
            //}
            //else
            //{
            //    running.SetUpdateAllMarketData(false);
            //    //running.BShouldUpdateAllMarket = false;
            //}

        }

        private async Task<bool> ResetMarketGrid(EMarketGridTabIdx eGrid)
        {
           Task<List<Ticker>> taskTickerList = mAPI.GetTicker(running.allMarketCode);
            List<Ticker> tickerList = await taskTickerList;
            if (null == tickerList)
            {
                return false;
            }

            DataGridView UpdateDataGridView = null;
            switch (eGrid)
            {
                case EMarketGridTabIdx.KRW:
                    {
                        UpdateDataGridView = dgvMarketKRW;
                    }
                    break;


                case EMarketGridTabIdx.BTC:
                    {
                        UpdateDataGridView = dgvMarketBTC;
                    }
                    break;

                case EMarketGridTabIdx.USDT:
                    {
                        UpdateDataGridView = dgvMarketUSDT;
                    }
                    break;

                case EMarketGridTabIdx.Interest:
                    {
                        UpdateDataGridView = dgvMarketInterestCoin;
                    }
                    break;
            }
            Debug.Assert(UpdateDataGridView != null);
            lock(UpdateDataGridView)
            {
                foreach (Ticker ticker in tickerList)
                {
                    //updateSingleTickerData(ticker);
                    string marketCode = ticker.market;
                    Coin mappedCoin = running.DictAllMarketInfo[marketCode];
                    if (mappedCoin.MarketGridTabIdx == eGrid)
                    {
                        Debug.Assert(UpdateDataGridView != null);
                        //lock (UpdateDataGridView)
                        //{
                        AddMarketInfo(eGrid, mappedCoin);
                        //}
                    }
                }

            }

            //int x = 10;

            return true;
        }

        private void textBoxMarketSearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string search = textBoxMarketSearchBox.Text;
                if (search.Length < 1)
                {
                    return;
                }
                int nIdx = tabControl_market.SelectedIndex;
                List<int> listFoundSymbolIdx = new List<int>();
                bool bFoundSymbol = searchByCoinSymbol(search, nIdx, listFoundSymbolIdx);
                if (bFoundSymbol)
                {
                    putUpSearchedCoin(listFoundSymbolIdx, nIdx);
                }
                else
                {
                    clearAllSelectedRowWithTabIdx(nIdx);
                }
                listFoundSymbolIdx = null;
                return;
            }
            else
            {
                return;
            }
            //int x = 10;
            //switch (e.KeyChar)
            //{
            //    // Counts the backspaces.
            //    //case '\b':
            //    //    break;
            //    // Counts the ENTER keys.
            //    case '\r':
            //        break;
            //    // Counts the ESC keys.  
            //    //case (char)27:
            //    //    break;
            //    //// Counts all other keys.
            //    //default:
            //        //break;
            //}
        }

        private bool searchByCoinSymbol(string searchInput, int nMarketGridTabIdx, List<int> listFoundSymbolRowIdx)
        {

            List<string> listCoinSymbol = null;
            switch (nMarketGridTabIdx)
            {
                case 0:
                    listCoinSymbol = ListCoinNameSymbolKRW;
                    break;

                case 1:
                    listCoinSymbol = ListCoinNameSymbolBTC; 
                    break;

                case 2:
                    listCoinSymbol = ListCoinNameSymbolUSDT;
                    break;

                case 3:
                    listCoinSymbol = ListCoinNameSymbolInterset;
                    break;

                default:
                    Debug.Assert(false, "No List");
                    return false;
            }
            bool bSymbolFound = false;
            for(int idx = 0; idx < listCoinSymbol.Count; idx++)
            {
                string coinSymbol = listCoinSymbol[idx];
                if(coinSymbol.Contains(searchInput))
                {
                    Coin searchedCoin = DictCoinWithSymbol[coinSymbol];
                    listFoundSymbolRowIdx.Add(searchedCoin.GridRowNumber);
                    bSymbolFound = true;
                }
            }
            return bSymbolFound;
        }


        private void putUpSearchedCoin(List<int> listFoundCoinIdx, int nTabIdx)
        {
            Debug.Assert(listFoundCoinIdx != null);
            Dictionary<int, Coin> dictCoinByRowIdx = null;
            DataGridView currentGridView = null;
            switch (nTabIdx)
            {
                case 0:
                    dictCoinByRowIdx = DictCoinByRowNumMarketKRW;
                    currentGridView = dgvMarketKRW;
                    break;

                case 1:
                    dictCoinByRowIdx = DictCoinByRowNumMarketBTC;
                    currentGridView = dgvMarketBTC;

                    break;

                case 2:
                    dictCoinByRowIdx = DictCoinByRowNumMarketUSDT;
                    currentGridView = dgvMarketUSDT;
                    break;

                case 3:
                    dictCoinByRowIdx = DictCoinByRowNumMarketInterest;
                    currentGridView = dgvMarketInterestCoin;

                    break;

                default:
                    Debug.Assert(false);
                    break;
            }
            for (int nFoundIdx = 0; nFoundIdx < listFoundCoinIdx.Count; ++nFoundIdx)
            {
                int matchCoinRowIdx = listFoundCoinIdx[nFoundIdx];
                Coin matchCoin = dictCoinByRowIdx[matchCoinRowIdx];
                Coin originalCoin = dictCoinByRowIdx[nFoundIdx];
                swapRow(currentGridView, nFoundIdx, matchCoinRowIdx);
                int nMatchCoinRowIdxBeforeSwap = matchCoin.GridRowNumber;
                matchCoin.GridRowNumber = nFoundIdx;
                originalCoin.GridRowNumber = nMatchCoinRowIdxBeforeSwap;
            }
            clearAllSelectedRowWithGrid(currentGridView);
            for(int i = 0; i < listFoundCoinIdx.Count;++i)
            {
                currentGridView.Rows[i].Selected = true;
            }
        }

        private void swapRow(DataGridView grid, int row1,  int row2)
        {
            if (row1 == row2)
            {
                return;
            }

            int nLowerIdx;
            int nHigherIdx;
            if(row1 > row2)
            {
                nLowerIdx = row2;
                nHigherIdx = row1;
            }
            else
            {
                nLowerIdx = row1;
                nHigherIdx = row2;
            }

            DataGridViewRow gridRow1 = grid.Rows[nLowerIdx];
            DataGridViewRow gridRow2 = grid.Rows[nHigherIdx];
            grid.Rows.Remove(gridRow1);
            grid.Rows.Remove(gridRow2);
            grid.Rows.Insert(nLowerIdx, gridRow2);
            grid.Rows.Insert(nHigherIdx, gridRow1);
            //gridRow1.Selected = false;
            //gridRow2.Selected = false;
            //grid.Rows[nLowerIdx + 1].
            //grid.Rows[nHigherIdx ].Selected = false;
            //grid.Rows[nLowerIdx + 1].Selected = false;
            //gridRow1.Selected = false;
            //gridRow2.Selected = false;
        }

        private void clearAllSelectedRowWithGrid(DataGridView grid)
        {
            for(int nColIdx = 0; nColIdx < grid.Columns.Count; nColIdx++)
            {
                grid.Columns[nColIdx].Selected = false;
            }
            for (int nIdx =0; nIdx < grid.Rows.Count; nIdx++)
            {
                grid.Rows[nIdx].Selected = false;
            }
        }

        private void clearAllSelectedRowWithTabIdx(int nTabIdx)
        {
            DataGridView currentGridView = null;
            switch (nTabIdx)
            {
                case 0:
                    currentGridView = dgvMarketKRW;
                    break;

                case 1:
                    currentGridView = dgvMarketBTC;

                    break;

                case 2:
                    currentGridView = dgvMarketUSDT;
                    break;

                case 3:
                    currentGridView = dgvMarketInterestCoin;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }
            clearAllSelectedRowWithGrid(currentGridView);
        }
        private void button_marketSearchBox_MouseDown(object sender, MouseEventArgs e)
        {
            //clearAllSelectedRowWithTabIdx(0);
            //string search = textBoxMarketSearchBox.Text;
            //if (search.Length < 1)
            //{
            //    return;
            //}
            int nTabIdx = tabControl_market.SelectedIndex;
            DataGridView currentGridView = null;
            switch (nTabIdx)
            {
                case 0:
                    currentGridView = dgvMarketKRW;
                    break;

                case 1:
                    currentGridView = dgvMarketBTC;

                    break;

                case 2:
                    currentGridView = dgvMarketUSDT;
                    break;

                case 3:
                    currentGridView = dgvMarketInterestCoin;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            //*/
             DataGridViewRow row1 = currentGridView.Rows[0];
             DataGridViewRow row2 = currentGridView.Rows[1];

             currentGridView.Rows.Remove(row2);
             currentGridView.Rows.Remove(row1);
             currentGridView.Rows.Insert(0, row2);
             currentGridView.Rows.Insert(1, row1);
             /*/
            for (int nIdx = 0; nIdx < currentGridView.Rows.Count; nIdx++)
            {
                currentGridView.Rows[nIdx].Selected = false;
            }

            //*/

            //List<int> listFoundSymbolIdx = new List<int>();
            //bool bFoundSymbol = searchByCoinSymbol(search, nIdx, listFoundSymbolIdx);
            //if (bFoundSymbol)
            //{
            //    putUpSearchedCoin(listFoundSymbolIdx, nIdx);
            //}
            //else
            //{
            //    clearAllSelectedRowWithTabIdx(nIdx);
            //}
            //listFoundSymbolIdx = null;
            //return;

        }


    }
}
