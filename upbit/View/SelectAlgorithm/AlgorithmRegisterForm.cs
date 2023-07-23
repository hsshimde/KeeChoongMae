using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using upbit.UpbitAPI.Model;
using System.Diagnostics;
using upbit.Controller;
using upbit.Enum;
using static upbit.Enum.EnumClass;

namespace upbit.View.SelectAlgorithm
{
    public partial class AlgorithmRegisterForm : Form
    {

        private EnumClass.EIsRunning mRunningStatus;
        private Dictionary<int, Coin> mDictRowNumToCoin;
        private List<Coin> mListRegisteredCoin;
        private List<Coin> mListSelectedRunningMarketCoin;
        public double RunningVolumeValue;

        private bool mbIsRunningVolumeSet;
        public List<TradeMarketSetting> mListTradeMarketSetting { get; set; }

        string[] ALGORITHM_NAME = { "종가 배팅", "Fucking Lunatic", "Test1", "Test2", "Test3" };

        const float COL_FONT_SIZE = 12.0f;
        const float COL_HEADER_FONT_SIZE = 14.0f;
        //static string ALGORITHM_NAME[] = { "", "" };

        //private List<string> 

        private int curColCount;


        public AlgorithmRegisterForm()
        {
            InitializeComponent();
            curColCount = 0;
            Hide();
            setUpDataGridView();
            InitRunningInfoLabel();
            addEventHandler();
            mbIsRunningVolumeSet = false;

            //mListTradeMarketSetting = new List<TradeMarketSetting>();
        }

        private void InitRunningInfoLabel()
        {
            mRunningStatus = EIsRunning.Paused;
            ChangeRunningStatusUI();
        }


        //private void dataGridView_DefaultValueNeeded(object sender, DataGridViewRowEventArgs e)
        //{
        //    e.Row.Cells[(int)EColContent.Algorithm].Value = "맞춤 전략";
        //    //int x = 10;
        //   //e.Row.Co
        //}

        public void AddBeforeRegisterdMarket(List<TradeMarketSetting> tradeMarketSetting)
        {
            for (int idx = 0; idx < tradeMarketSetting.Count; idx++)
            {
                TradeMarketSetting marketCoinSetting = tradeMarketSetting.ElementAt(idx);
                AddMarketCoin(marketCoinSetting.MarketCoin);
                dataGridView_Algorithm[(int)EColContent.UseStatus, idx].Value = marketCoinSetting.bUseStatus;
                dataGridView_Algorithm[(int)EColContent.ShouldCutLoss, idx].Value = marketCoinSetting.bCutLossUseStatus;
                dataGridView_Algorithm[(int)EColContent.CutLossRatio, idx].Value = marketCoinSetting.CutLossRatio;
            }
            tradeMarketSetting.Clear();

        }

        private void addEventHandler()
        {
            this.Move += OnAlgorithmFormMoved;
            this.FormClosed += OnCloseForm;
            dataGridView_Algorithm.CellDoubleClick += OnDblClikcedRegisteredMarketDataGridView;
            dataGridView_Algorithm.RowsAdded += OnRowAdded;
            dataGridView_Algorithm.CellContentDoubleClick += OnCellContentClick;
            dataGridView_Algorithm.CellContentClick += OnCellContentClick;
            dataGridView_Algorithm.EditingControlShowing += OnCellEditingControlShowing;


        }


        private void OnCellEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //e.CellStyle.
            e.CellStyle.BackColor = Color.Aquamarine;
            //e.CellStyle.
        }

        private void OnRowAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dataGridView_Algorithm[(int)EColContent.Algorithm, e.RowIndex];
        }

        private void OnCloseForm(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)(mParentForm);
            mainForm.AlgoRegisterForm = null;
            for (int idx = 0; idx < dataGridView_Algorithm.Rows.Count; idx++)
            {
                bool bIsChecked = (bool)dataGridView_Algorithm[(int)EColContent.UseStatus, idx].Value;
                Coin marketCoin = mListRegisteredCoin[idx];
                TradeMarketSetting tradeMarketSetting = new TradeMarketSetting(marketCoin, bIsChecked);
                mainForm.MarketTradeSettingInfo.Add(tradeMarketSetting);
            }
        }

        public void OnAlgorithmFormMoved(object sender, EventArgs e)
        {

        }

        private void setUpDataGridView()
        {
            //DataGridView2.Dock = DockStyle.Bottom;
            //DataGridView2.TopLeftHeaderCell.Value = "Sales Details";
            //DataGridView2.RowHeadersWidthSizeMode =
            //    DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            //dataGridView_Algorithm.Columns.Remove("")
            //dataGridView_Algorithm.ColumnCount = 4;
            dataGridView_Algorithm.AllowUserToAddRows = false;
            dataGridView_Algorithm.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView_Algorithm.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_Algorithm.ColumnHeadersDefaultCellStyle.Font =
                new Font(dataGridView_Algorithm.Font, FontStyle.Bold);


            mDictRowNumToCoin = new Dictionary<int, Coin>();
            mListRegisteredCoin = new List<Coin>();
            mListSelectedRunningMarketCoin = new List<Coin>();
            //AddNormalColumn();
            addNumberCol();
            addMarketNameCol();
            addSelectStrategyCol();
            addUseStatusCheckBoxCol();
            addCutLossUseStatusColCheckBox();
            addCutLossRatioEditBoxCol();
            addTakeProfitUseStatusCheckBoxCol();
            addTakeProfitRatioEditBoxCol();
        }


        private void AddOperatingAmountCol()
        {

        }

        public void SetUpFormDesign()
        {
            this.Location = new Point(mParentForm.Location.X + mParentForm.Size.Width - 15, mParentForm.Location.Y);
        }

        enum ColumnName
        {

        };



        private void AddNormalColumn()
        {
            //addNumberCol();
            //addMarketNameCol();
            //addComboBoxColumn();
        }


        private void addNumberCol()
        {
            DataGridViewColumn numberColumn = new DataGridViewColumn();
            numberColumn.HeaderText = "No";
            numberColumn.Name = "No";
            numberColumn.CellTemplate = new DataGridViewTextBoxCell();
            numberColumn.Width = 40;
            numberColumn.ReadOnly = true;
            numberColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            numberColumn.DefaultCellStyle.Font = new Font("Gulim", COL_FONT_SIZE);
            numberColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            numberColumn.HeaderCell.Style.Font = new Font("Gulim", COL_HEADER_FONT_SIZE);
            dataGridView_Algorithm.Columns.Insert(curColCount++, numberColumn);
        }

        private void addMarketNameCol()
        {
            DataGridViewColumn marketInfoColumn = new DataGridViewColumn();
            marketInfoColumn.HeaderText = "종목";
            marketInfoColumn.Name = "Market";
            marketInfoColumn.CellTemplate = new DataGridViewTextBoxCell();
            marketInfoColumn.Width = 150;
            marketInfoColumn.ReadOnly = true;
            marketInfoColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            marketInfoColumn.DefaultCellStyle.Font = new Font("Gulim", COL_FONT_SIZE);
            marketInfoColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            marketInfoColumn.HeaderCell.Style.Font = new Font("Gulim", COL_HEADER_FONT_SIZE);
            dataGridView_Algorithm.Columns.Insert(curColCount++, marketInfoColumn);

        }

        private void addCutLossRatioEditBoxCol()
        {
            DataGridViewColumn cutLossRationCol = new DataGridViewColumn();
            cutLossRationCol.HeaderText = "손절 비율";
            cutLossRationCol.Name = "CutLossRatio";
            cutLossRationCol.CellTemplate = new DataGridViewTextBoxCell();
            cutLossRationCol.Width = 80;
            cutLossRationCol.ReadOnly = false;
            cutLossRationCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cutLossRationCol.DefaultCellStyle.Font = new Font("Gulim", COL_FONT_SIZE);
            cutLossRationCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cutLossRationCol.HeaderCell.Style.Font = new Font("Gulim", COL_HEADER_FONT_SIZE);
            dataGridView_Algorithm.Columns.Insert(curColCount++, cutLossRationCol);

        }

        private void addTakeProfitUseStatusCheckBoxCol()
        {
            DataGridViewCheckBoxColumn cutLossCheckBox = new DataGridViewCheckBoxColumn();
            cutLossCheckBox.HeaderText = "익절 여부";
            cutLossCheckBox.TrueValue = true;
            cutLossCheckBox.FalseValue = false;
            cutLossCheckBox.ThreeState = false;
            cutLossCheckBox.Width = 70;
            cutLossCheckBox.DefaultCellStyle.Font = new Font("Gulim", COL_FONT_SIZE);
            cutLossCheckBox.SortMode = DataGridViewColumnSortMode.NotSortable;
            cutLossCheckBox.HeaderCell.Style.Font = new Font("Gulim", COL_HEADER_FONT_SIZE);
            dataGridView_Algorithm.Columns.Insert(curColCount++, cutLossCheckBox);
        }

        private void addTakeProfitRatioEditBoxCol()
        {
            DataGridViewColumn cutLossRationCol = new DataGridViewColumn();
            cutLossRationCol.HeaderText = "익절 비율";
            cutLossRationCol.Name = "CutLossRatio";
            cutLossRationCol.CellTemplate = new DataGridViewTextBoxCell();
            cutLossRationCol.Width = 80;
            cutLossRationCol.ReadOnly = false;
            cutLossRationCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cutLossRationCol.DefaultCellStyle.Font = new Font("Gulim", COL_FONT_SIZE);
            cutLossRationCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_Algorithm.Columns.Insert(curColCount++, cutLossRationCol);

        }
        private void addPreserveProfitCheckBoxCell()
        {
            DataGridViewCheckBoxColumn cutLossCheckBox = new DataGridViewCheckBoxColumn();
            cutLossCheckBox.HeaderText = "익절 여부";
            cutLossCheckBox.TrueValue = true;
            cutLossCheckBox.FalseValue = false;
            cutLossCheckBox.ThreeState = false;
            cutLossCheckBox.Width = 70;
            cutLossCheckBox.DefaultCellStyle.Font = new Font("Gulim", COL_FONT_SIZE);
            cutLossCheckBox.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView_Algorithm.Columns.Insert(curColCount++, cutLossCheckBox);
        }


        private void addComboBoxColumn()
        {

        }

        private void addSelectStrategyCol()
        {
            DataGridViewComboBoxColumn comboboxColumn = null;
            comboboxColumn = CreateComboBoxColumn();
            comboboxColumn.HeaderText = "맞춤 전략";
            comboboxColumn.ReadOnly = false;
            int nDataGridViewWidth = dataGridView_Algorithm.Size.Width;
            comboboxColumn.Width = 260;
            comboboxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            comboboxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            comboboxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            comboboxColumn.DataSource = ALGORITHM_NAME;
            comboboxColumn.DataPropertyName = "맞춤 전략";
            comboboxColumn.DefaultCellStyle.NullValue = "세팅 전";
            comboboxColumn.DefaultCellStyle.Font = new Font("Gulim", COL_FONT_SIZE);
            comboboxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            comboboxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            //comboboxColumn.DefaultCellStyle.Font.Size
            //comboboxColumn.DefaultCellStyle.Font
            //DataGridViewCellStyle currentCellStyle = comboboxColumn.DefaultCellStyle;
            //currentCellStyle.Font.Size = new float(11.2f);
            //float currentSize = currentCellStyle.Font.Size;
            //currentSize = 12.0f;
            //comboboxColumn.DefaultCellStyle.Font = new Font("Gulim", 12.0f, currentCellStyle.Font.Style, currentCellStyle.Font.Unit);
            dataGridView_Algorithm.Columns.Insert(curColCount++, comboboxColumn);
        }


        private DataGridViewComboBoxColumn CreateComboBoxColumn()
        {
            DataGridViewComboBoxColumn column =
                new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "전략";
                column.HeaderText = "전략";
                column.DropDownWidth = 160;
                column.Width = 250;
                column.MaxDropDownItems = 3;
                column.FlatStyle = FlatStyle.Flat;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Font = new Font("Gulim", COL_FONT_SIZE);
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return column;
        }


        private void setNewRowDefaultValue(Coin newCoin, int addedRowIdx)
        {
            dataGridView_Algorithm[(int)EColContent.Index, addedRowIdx].Value = dataGridView_Algorithm.Rows.Count;
            dataGridView_Algorithm[(int)EColContent.MarketInfo, addedRowIdx].Value = newCoin.CoinNameSymbol;
            dataGridView_Algorithm[(int)EColContent.UseStatus, addedRowIdx].Value = true;
            dataGridView_Algorithm[(int)EColContent.ShouldCutLoss, addedRowIdx].Value = false;
            dataGridView_Algorithm[(int)EColContent.CutLossRatio, addedRowIdx].Value = 3.0;
            dataGridView_Algorithm[(int)EColContent.ShouldTakeProfit, addedRowIdx].Value = false;
            dataGridView_Algorithm[(int)EColContent.TakeProfitRatio, addedRowIdx].Value = 3.0;

        }
        public void AddMarketCoin(Coin coin)
        {
            if (mDictRowNumToCoin.ContainsValue(coin))
            {
                return;
            }

            int addedRowIdx = dataGridView_Algorithm.Rows.Add();
            DataGridViewRow newRow = dataGridView_Algorithm.Rows[addedRowIdx];
            Color clr = Color.White;
            if (addedRowIdx % 2 == 0)
            {
                clr = Color.LightGray;
            }
            newRow.DefaultCellStyle.BackColor = clr;
            DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)newRow.Cells[(int)EColContent.Algorithm];
            //comboBoxCell

            //{
            //    bIsFirstRow = true;
            //}
            //comboBoxCell.PositionEditingControl(false, true, comboBoxCell.ContentBounds, comboBoxCellRect, comboBoxCell.Style, false, false, false, bIsFirstRow);
            //comboBoxCell.


            newRow.Height = 40;
            setNewRowDefaultValue(coin, addedRowIdx);
            mListRegisteredCoin.Add(coin);
            mDictRowNumToCoin.Add(addedRowIdx, coin);
        }

        private void OnCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)EColContent.Algorithm)
            {
                dataGridView_Algorithm[e.ColumnIndex, e.RowIndex].Selected = false;
            }
        }


        public void OnDblClikcedRegisteredMarketDataGridView(object sender, DataGridViewCellEventArgs e)
        {
            //int rowIdx = e.RowIndex;
            //Debug.Assert(mDictRowNumToCoin.ContainsKey(rowIdx) == true, "There Is No Market Coin ");
            //bool bRemoveResult = mDictRowNumToCoin.Remove(rowIdx);
            //Debug.Assert(bRemoveResult, "Row Num to Coin Remove Result");
            //mDictRowNumToCoin.Remove(rowIdx);
            //mListRegisteredCoin.RemoveAt(rowIdx);
            //dataGridView_Algorithm.Rows.RemoveAt(rowIdx);
            //for (int idx = rowIdx; idx < dataGridView_Algorithm.Rows.Count; idx++)
            //{
            //    int oldRowIdx = ((int)dataGridView_Algorithm[0, idx].Value - 1);
            //    Coin oldRowCoin = mDictRowNumToCoin[oldRowIdx];
            //    mDictRowNumToCoin.Remove(oldRowIdx);
            //    mDictRowNumToCoin.Add(idx, oldRowCoin);
            //    dataGridView_Algorithm[0, idx].Value = idx + 1;
            //}
            //checkSelectedMarket();
        }


        private void addUseStatusCheckBoxCol()
        {
            DataGridViewCheckBoxColumn CheckBoxColumn = new DataGridViewCheckBoxColumn();
            CheckBoxColumn.HeaderText = "사용 여부";
            CheckBoxColumn.TrueValue = true;
            CheckBoxColumn.FalseValue = false;
            CheckBoxColumn.ThreeState = false;
            //CheckBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            CheckBoxColumn.Width = 70;
            CheckBoxColumn.DefaultCellStyle.Font = new Font("Gulim", COL_FONT_SIZE);
            CheckBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView_Algorithm.Columns.Insert(curColCount++, CheckBoxColumn);


        }

        private void AddCheckBoxColumn()
        {
            //CheckBoxColumn.Frozen
            //links.UseColumnTextForLinkValue = true;
            //links.HeaderText = ColumnName.ReportsTo.ToString();
            //links.DataPropertyName = ColumnName.ReportsTo.ToString();
            //links.ActiveLinkColor = Color.White;
            //links.LinkBehavior = LinkBehavior.SystemDefault;
            //links.LinkColor = Color.Blue;
            //links.TrackVisitedState = true;
            //links.VisitedLinkColor = Color.YellowGreen;

            addUseStatusCheckBoxCol();
            addCutLossUseStatusColCheckBox();
            addTakeProfitUseStatusCheckBoxCol();


            //DataGridViewRow row = dataGridView_Algorithm.Rows[0];

        }


        private void addCutLossUseStatusColCheckBox()
        {
            DataGridViewCheckBoxColumn cutLossCheckBox = new DataGridViewCheckBoxColumn();
            cutLossCheckBox.HeaderText = "손절 여부";
            cutLossCheckBox.TrueValue = true;
            cutLossCheckBox.FalseValue = false;
            cutLossCheckBox.ThreeState = false;
            cutLossCheckBox.Width = 70;
            cutLossCheckBox.DefaultCellStyle.Font = new Font("Gulim", COL_FONT_SIZE);
            cutLossCheckBox.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView_Algorithm.Columns.Insert(curColCount++, cutLossCheckBox);

        }

        private async void button_StartAndStop_Click(object sender, EventArgs e)
        {

            if (mbIsRunningVolumeSet == false)
            {
                MessageBox.Show("운용 금액을 설정해주세요");
                return;
            }
            MainForm mainForm = (MainForm)mParentForm;
            Running running = mainForm.RunMachine;
            if (mRunningStatus == EIsRunning.Paused)
            {
                mRunningStatus = EIsRunning.Running;
                checkSelectedMarket();
                setRunningMarketCoinSelectInfoAtMainForm();
                await running.BeforeGoRunning();
            }
            else
            {
                mRunningStatus = EIsRunning.Paused;
            }
            ChangeRunningStatusUI();
            running.RunningStatus = mRunningStatus;
        }


        private void checkSelectedMarket()
        {
            lock (mListSelectedRunningMarketCoin)
            {
                mListSelectedRunningMarketCoin.Clear();
                for (int idx = 0; idx < dataGridView_Algorithm.Rows.Count; idx++)
                {
                    DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dataGridView_Algorithm[(int)EColContent.UseStatus, idx];
                    if ((bool)checkBoxCell.Value == (bool)checkBoxCell.TrueValue)
                    {
                        Coin selCoin = mDictRowNumToCoin[idx];
                        lock (mListSelectedRunningMarketCoin)
                        {
                            mListSelectedRunningMarketCoin.Add(selCoin);
                        }
                        Debug.Assert(selCoin != null, "Coin NULL");
                    }
                    else
                    {
                        continue;
                    }

                    DataGridViewCheckBoxCell cutLossUseCheckBoxCell = (DataGridViewCheckBoxCell)dataGridView_Algorithm[(int)EColContent.ShouldCutLoss, idx];
                    if ((bool)cutLossUseCheckBoxCell.Value == (bool)cutLossUseCheckBoxCell.TrueValue)
                    {
                        Coin selCoin = mDictRowNumToCoin[idx];
                        DataGridViewTextBoxCell cutLossRatioCell = (DataGridViewTextBoxCell)dataGridView_Algorithm[(int)EColContent.CutLossRatio, idx];
                        selCoin.CutLossRatio = (float)Convert.ToDouble(cutLossRatioCell.Value.ToString());
                        selCoin.ShouldCutLoss = true;
                    }

                    DataGridViewCheckBoxCell takeProfitUseCheckBoxCell = (DataGridViewCheckBoxCell)dataGridView_Algorithm[(int)EColContent.ShouldTakeProfit, idx];
                    if ((bool)takeProfitUseCheckBoxCell.Value == (bool)takeProfitUseCheckBoxCell.TrueValue)
                    {
                        Coin selCoin = mDictRowNumToCoin[idx];
                        DataGridViewTextBoxCell takeProfitRatioCell = (DataGridViewTextBoxCell)dataGridView_Algorithm[(int)EColContent.TakeProfitRatio, idx];
                        //string takeProfitRatio = (string)(takeProfitRatioCell.Value);
                        selCoin.TakeProfitRatio = (float)(Convert.ToDouble(takeProfitRatioCell.Value.ToString()));
                        selCoin.ShouldTakeProfit = true;
                    }

                    DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dataGridView_Algorithm[(int)EColContent.Algorithm, idx];
                    //if(comboBoxCell.)
                    
                }
            }
        }


        public void ChangeRunningStatusUI()
        {
            if (mRunningStatus == EIsRunning.Running)
            {
                button_StartAndStop.Text = "자동매매 정지";
                label＿RunningInfo.BackColor = Color.Lime;
                label＿RunningInfo.Text = "매매 진행 중";
            }
            else
            {
                button_StartAndStop.Text = "자동매매 시작";
                label＿RunningInfo.BackColor = Color.Gray;
                label＿RunningInfo.Text = "대기중";
            }

        }

        private void setRunningMarketCoinSelectInfoAtMainForm()
        {
            MainForm mainForm = (MainForm)(mParentForm);
            mainForm.SelCoin = mListSelectedRunningMarketCoin;
        }

        private void button_SetVolume_Click(object sender, EventArgs e)
        {
            string input = textBox_RunningVolume.Text;
            if (input.Length < 1)
            {
                MessageBox.Show("다시 입력해주세요");
                return;
            }
            RunningVolumeValue = Convert.ToDouble(input);
            MainForm mainForm = (MainForm)mParentForm;
            Running runMachine = mainForm.RunMachine;
            runMachine.RunningValue = RunningVolumeValue;
            mbIsRunningVolumeSet = true;
        }
    }
}
