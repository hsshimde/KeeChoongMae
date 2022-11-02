using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using upbit.UpbitAPI;
using upbit.Controller;
using upbit.UpbitAPI.Model;
using upbit.ColumnNameBuilder;

namespace upbit.View
{
    public enum EGridKind
    {
        KRW,
        BTC,
        USDT,
        Interest
    }


    public partial class MainForm : Form
    {
        private APIClass mAPI;
        private Running running;
        private bool bCoinSelectDone;
        public ConnectionNoWorkDialogue m_dlgNoConnection { get; set; }
        public Dictionary<string, Coin> DictCoinInfo { get; private set; }



        public MainForm(APIClass api)
        {
            InitializeComponent();
            this.bCoinSelectDone = false;
            this.mAPI = api;
            this.running = new Running(api, this);
            InitDataGridView();
            SetCallBackFunction();
            DictCoinInfo = new Dictionary<string, Coin>();
            DictCoinAccount = new Dictionary<string, CoinAccount>();
        }

        public async void Init()
        {
            await DivideMarketGridByUnitCurrnecy();
            await DivideMyAssetGridByUnitCurrency();

            running.SetMyAssetInfo(DictCoinAccount);
            running.SetAllMarketInfo(DictCoinInfo);

            running.Go();
        }


        private void SetDataGridViewColMiddleCenter(DataGridView grid)
        {
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in grid.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void InitDataGridView()
        {
            SetDataGridViewColMiddleCenter(dgvMarketInterestCoin);
            SetDataGridViewColMiddleCenter(dgvMarketKRW);
            SetDataGridViewColMiddleCenter(dgvMarketBTC);
            SetDataGridViewColMiddleCenter(dgvMarketUSDT);
            SetDataGridViewColMiddleCenter(dgvMarketInterestCoin);
            SetDataGridViewColMiddleCenter(dgvMyAssetKRW);
            SetDataGridViewColMiddleCenter(dgvmyAssetUSDT);
        }

        private async Task DivideMarketGridByUnitCurrnecy()
        {
            Task<List<MarketAll>> allMarketInfoTask = mAPI.GetMarketAll();
            List<MarketAll> allMarketInfo = await allMarketInfoTask;
            StringBuilder tickerRequestBuilder = new StringBuilder();
            foreach (MarketAll marketInfo in allMarketInfo)
            {
                string marketName = marketInfo.market;
                string[] currency = marketName.Split('-');
                EGridKind gridType = new EGridKind();
                if (currency[0] == "KRW")
                {
                    gridType = EGridKind.KRW;
                }
                else if (currency[0] == "BTC")
                {
                    gridType = EGridKind.BTC;
                }
                else if (currency[0] == "USDT")
                {
                    gridType = EGridKind.USDT;
                }
                Coin coin = new Coin(marketInfo.market, marketInfo.korean_name, marketInfo.english_name);
                coin.GridKind = gridType;
                AddMarketInfo(gridType, coin);
                DictCoinInfo.Add(coin.MarketCode, coin);
                tickerRequestBuilder.AppendFormat(marketInfo.market);
                tickerRequestBuilder.AppendFormat(",");
            }
            tickerRequestBuilder.Length--;
            running.allMarketCode = tickerRequestBuilder.ToString();
            //foreach (MarketAll marketInfo in allMarketInfo)
            //{
            //}


            //Task<List<Ticker>> allTickerInfoTask = m_API.GetTicker(tickerRequestBuilder.ToString());
            //List<Ticker> allTickerInfo = await allTickerInfoTask;

            //foreach(Ticker tickerInfo in allTickerInfo)
            //{
            //    Coin tickerCoin = m_dictCoinInfo[tickerInfo.market];
            //    if(tickerCoin!=null)
            //    {
            //        tickerCoin.CurPrice = tickerInfo.trade_price;
            //        tickerCoin.AccumulateTradePrice = tickerInfo.acc_trade_price;
            //    }
            //}


        }

        private void AddMarketInfo(EGridKind gridType, Coin coin)
        {
            StringBuilder coinMakretNameBuilder = new StringBuilder();
            coinMakretNameBuilder.AppendFormat(coin.CoinNameKor);
            coinMakretNameBuilder.AppendFormat("(");
            coinMakretNameBuilder.AppendFormat(coin.MarketCode);
            coinMakretNameBuilder.AppendFormat(")");
            ColNameBuilder colBuilder = new ColNameBuilder();
            colBuilder.ColItem = ColNameBuilder.EColItem.MarketCode;
            colBuilder.GridType = ColNameBuilder.EGridType.market;

            if (gridType == EGridKind.KRW)
            {
                //colBuilder
                colBuilder.UnitCurrency= ColNameBuilder.EUnitCurrency.KRW;
                int rowIdx = dgvMarketKRW.Rows.Add();
                coin.GridRowNumber = rowIdx;
                //dgvMarketKRW["marketKRWMarketCode", rowIdx].Value = coinMakretNameBuilder.ToString();
                dgvMarketKRW[colBuilder.BuildColName(), rowIdx].Value = coinMakretNameBuilder.ToString();
            }
            else if (gridType == EGridKind.BTC)
            {
                colBuilder.UnitCurrency = ColNameBuilder.EUnitCurrency.BTC;
                int rowIdx = dgvMarketBTC.Rows.Add();
                coin.GridRowNumber = rowIdx;
                dgvMarketBTC[colBuilder.BuildColName(), rowIdx].Value = coinMakretNameBuilder.ToString();
            }
            else if (gridType == EGridKind.USDT)
            {
                int rowIdx = dgvMarketUSDT.Rows.Add();
                coin.GridRowNumber = rowIdx;
                colBuilder.UnitCurrency = ColNameBuilder.EUnitCurrency.USDT;
                //dgvMarketUSDT["USDT_marketInfo", rowIdx].Value = coinMakretNameBuilder.ToString();
                dgvMarketUSDT[colBuilder.BuildColName(), rowIdx].Value = coinMakretNameBuilder.ToString();
            }
            else
            {
                Debug.Assert(false);
            }

            colBuilder = null;


        }


        private void OnDataGridViewSorted(object sender, EventArgs e)
        {
            EGridKind gridType = new EGridKind();
            if (sender.Equals(dgvMarketKRW))
            {
                gridType = EGridKind.KRW;
            }
            else if (sender.Equals(dgvMarketBTC))
            {
                gridType = EGridKind.BTC;
            }
            else if (sender.Equals(dgvMarketUSDT))
            {
                gridType = EGridKind.USDT;
            }
            else if (sender.Equals(dgvMarketInterestCoin))
            {
                gridType = EGridKind.Interest;
            }
            updateCoinRowNumber(gridType);
        }

        private void updateCoinRowNumber(EGridKind gridKind)
        {
            //StringBuilder marketCodeBuilder = new StringBuilder();
            switch (gridKind)
            {
                case EGridKind.KRW:
                    {

                        foreach (DataGridViewRow row in dgvMarketKRW.Rows)
                        {
                            string marketName = row.Cells["KRW_marketInfo"].Value.ToString();
                            string[] marketCode = marketName.Split('(');
                            string[] marketCodeLatter = marketCode[1].Split(')');
                            string realMarketCode = marketCodeLatter[0];

                            Coin coin = DictCoinInfo[realMarketCode];
                            coin.GridRowNumber = row.Index;
                        }

                    }
                    break;

                case EGridKind.BTC:
                    {
                        foreach (DataGridViewRow row in dgvMarketBTC.Rows)
                        {
                            string marketName = row.Cells["BTC_marketInfo"].Value.ToString();
                            string[] marketCode = marketName.Split('(');
                            string[] marketCodeLatter = marketCode[1].Split(')');
                            string realMarketCode = marketCodeLatter[0];

                            Coin coin = DictCoinInfo[realMarketCode];
                            coin.GridRowNumber = row.Index;
                        }
                    }
                    break;

                case EGridKind.USDT:
                    {
                        foreach (DataGridViewRow row in dgvMarketUSDT.Rows)
                        {
                            string marketName = row.Cells["USDT_marketInfo"].Value.ToString();
                            string[] marketCode = marketName.Split('(');
                            string[] marketCodeLatter = marketCode[1].Split(')');
                            string realMarketCode = marketCodeLatter[0];

                            Coin coin = DictCoinInfo[realMarketCode];
                            coin.GridRowNumber = row.Index;
                        }
                    }
                    break;

                case EGridKind.Interest:
                    {
                        //gridView = dataGridView_Interested;
                        //marketCodeBuilder.AppendFormat("Interested_");
                    }
                    break;

                default:
                    {
                        Debug.Assert(false);
                    }
                    break;
            }
            //marketCodeBuilder.Clear();
            //marketCodeBuilder = null;
        }

        private void SetCallBackFunction()
        {
            toolStripButton_START.Click += OnButtonClickedFromMainForm;
            toolStripButton_SelectCoin.Click += OnButtonClickedFromMainForm;
            toolStripButton_STOP.Click += OnButtonClickedFromMainForm;
            this.running.ehUpdateCoinAccount += OnEventAccountUpdate;
            this.running.ehRemoveAccount += OnEventAccountDelete;
            this.running.ehUpdateTotalAsset += OnUpdateTotalAsset;
            this.running.ehUpdateMonitoring += OnMonitoringEvent;
            this.running.ehUpdateTicker += OnUpdateTickerEvent;

        }


        private void OnDataGridViewSortCompare(object sender,
       DataGridViewSortCompareEventArgs e)
        {
            string[] colNames = e.Column.Name.Split('_');
            double cellVal1 = 0.0;
            double cellVal2 = 0.0;
            if(e.CellValue1 == null || e.CellValue2 == null)
            {
                return;
            }
            string strCellVal1 = e.CellValue1.ToString();
            string strCellVal2 = e.CellValue2.ToString();
            if (colNames[1] == "24H")
            {
                strCellVal1 = strCellVal1.Remove(strCellVal1.Length - 1);
                strCellVal2 = strCellVal2.Remove(strCellVal2.Length - 1);
            }
            else if (colNames[1] == "transPrice")
            {
                strCellVal1 = strCellVal1.Substring(0, strCellVal1.Length - 2);
                strCellVal2 = strCellVal2.Substring(0, strCellVal2.Length - 2);
            } 
            //else if(colNames[1]=="marketInfo")
            //{

            //}
            cellVal1 = Convert.ToDouble(strCellVal1);
            cellVal2 = Convert.ToDouble(strCellVal2);

            if (cellVal1 < cellVal2)
            {
                e.SortResult = -1;
            }
            else if (cellVal1 > cellVal2)
            {
                e.SortResult = 1;
            }
            else
            {
                e.SortResult = 0;
            }
            e.Handled = true;
        }

        public void OnFormClosingEvent(object sender, FormClosingEventArgs e)
        {
            if (sender.Equals(this))
            {
                running.Stop();
            }

            else if (sender.Equals(m_selectCoinForm))
            {

            }
        }

        public void ConnectionNotWorking()
        {
            if (m_dlgNoConnection == null)
            {
                m_dlgNoConnection = new ConnectionNoWorkDialogue();
                m_dlgNoConnection.Owner = this;
                //m_dlgNoConnection.Show();
            }
            else
            {
                m_dlgNoConnection.Focus();
            }
        }
    }
}
