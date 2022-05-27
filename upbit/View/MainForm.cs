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
using upbit.Controller;

namespace upbit.View
{
    public partial class MainForm : Form
    {
        private APIClass m_API;
        private Running running;
        private bool bCoinSelectDone;
        public ConnectionNoWorkDialogue m_dlgNoConnection { get; set; }

        public MainForm(APIClass api)
        {
            InitializeComponent();
            this.bCoinSelectDone = false;
            this.m_API = api;
            this.running = new Running(api, this);

            InitDataGridView();
            SetCallBackFunction();
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
            SetDataGridViewColMiddleCenter(dataGridView_Account);
        }


        private void DataGridViewAccountSelectionChanged(object sender, EventArgs e)
        {
            dataGridView_Account.ClearSelection();
        }

        private void SetCallBackFunction()
        {
            toolStripButton_START.Click += OnButtonClickedFromMainForm;
            toolStripButton_SelectCoin.Click += OnButtonClickedFromMainForm;
            toolStripButton_STOP.Click += OnButtonClickedFromMainForm;
            this.running.ehUpdateCoinAccount += OnEventAccountUpdate;
            this.running.ehRemoveAccount += OnEventAccountDelete;
            this.running.ehUpdateTotalAsset += OnUpdateTotalAsset;

        }

        public void OnFormClosingEvent(object sender, FormClosingEventArgs e)
        {
            if(sender.Equals(this))
            {
                 running.Stop();
            }

            else if(sender.Equals(m_selectCoinForm))
            {
                //m_selectCoinForm = null;                
            }
        }

        public void ConnectionNotWorking()
        {
            if(m_dlgNoConnection == null)
            {
                m_dlgNoConnection = new ConnectionNoWorkDialogue();
                m_dlgNoConnection.Owner = this;
                m_dlgNoConnection.Show();
            }
            else
            {
                m_dlgNoConnection.Focus();
            }
        }
    }
}
