namespace upbit.View
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl_Features = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView_Account = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_START = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_SelectCoin = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_STOP = new System.Windows.Forms.ToolStripButton();
            this.account_market = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_CoinName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_profit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_quant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_avgPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_curPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripLabel_totalAsset = new System.Windows.Forms.ToolStripLabel();
            this.dataGridView_Monitoring = new System.Windows.Forms.DataGridView();
            this.Monitoring_marketInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monitoring_koreanName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monitoring_curPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monitoring_24H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl_Features.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Account)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Monitoring)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl_Features
            // 
            this.tabControl_Features.Controls.Add(this.tabPage1);
            this.tabControl_Features.Controls.Add(this.tabPage2);
            this.tabControl_Features.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Features.Location = new System.Drawing.Point(3, 33);
            this.tabControl_Features.Multiline = true;
            this.tabControl_Features.Name = "tabControl_Features";
            this.tabControl_Features.SelectedIndex = 0;
            this.tabControl_Features.Size = new System.Drawing.Size(702, 431);
            this.tabControl_Features.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView_Account);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(694, 401);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "잔고";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Account
            // 
            this.dataGridView_Account.AllowUserToAddRows = false;
            this.dataGridView_Account.AllowUserToDeleteRows = false;
            this.dataGridView_Account.AllowUserToResizeColumns = false;
            this.dataGridView_Account.AllowUserToResizeRows = false;
            this.dataGridView_Account.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Account.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.account_market,
            this.account_CoinName,
            this.account_profit,
            this.account_quant,
            this.account_avgPrice,
            this.account_curPrice});
            this.dataGridView_Account.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Account.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Account.Name = "dataGridView_Account";
            this.dataGridView_Account.RowHeadersVisible = false;
            this.dataGridView_Account.RowTemplate.Height = 23;
            this.dataGridView_Account.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Account.Size = new System.Drawing.Size(688, 395);
            this.dataGridView_Account.TabIndex = 0;
            this.dataGridView_Account.SelectionChanged += new System.EventHandler(this.DataGridViewAccountSelectionChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView_Monitoring);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(694, 401);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "모니터링";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl_Features, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.423983F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.57602F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(708, 467);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_START,
            this.toolStripButton_SelectCoin,
            this.toolStripButton_STOP,
            this.toolStripLabel_totalAsset});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(708, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_START
            // 
            this.toolStripButton_START.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_START.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_START.Image")));
            this.toolStripButton_START.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_START.Name = "toolStripButton_START";
            this.toolStripButton_START.Size = new System.Drawing.Size(23, 27);
            this.toolStripButton_START.Text = "START";
            // 
            // toolStripButton_SelectCoin
            // 
            this.toolStripButton_SelectCoin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_SelectCoin.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_SelectCoin.Image")));
            this.toolStripButton_SelectCoin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_SelectCoin.Name = "toolStripButton_SelectCoin";
            this.toolStripButton_SelectCoin.Size = new System.Drawing.Size(23, 27);
            this.toolStripButton_SelectCoin.Text = "Select Coin";
            // 
            // toolStripButton_STOP
            // 
            this.toolStripButton_STOP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_STOP.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_STOP.Image")));
            this.toolStripButton_STOP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_STOP.Name = "toolStripButton_STOP";
            this.toolStripButton_STOP.Size = new System.Drawing.Size(23, 27);
            this.toolStripButton_STOP.Text = "STOP";
            // 
            // account_market
            // 
            this.account_market.HeaderText = "종목";
            this.account_market.Name = "account_market";
            // 
            // account_CoinName
            // 
            this.account_CoinName.HeaderText = "이름";
            this.account_CoinName.Name = "account_CoinName";
            // 
            // account_profit
            // 
            this.account_profit.HeaderText = "수익률";
            this.account_profit.Name = "account_profit";
            // 
            // account_quant
            // 
            this.account_quant.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Format = "N4";
            this.account_quant.DefaultCellStyle = dataGridViewCellStyle1;
            this.account_quant.HeaderText = "보유 수량";
            this.account_quant.Name = "account_quant";
            // 
            // account_avgPrice
            // 
            dataGridViewCellStyle2.NullValue = null;
            this.account_avgPrice.DefaultCellStyle = dataGridViewCellStyle2;
            this.account_avgPrice.HeaderText = "평균매수가";
            this.account_avgPrice.Name = "account_avgPrice";
            // 
            // account_curPrice
            // 
            this.account_curPrice.HeaderText = "현재가";
            this.account_curPrice.Name = "account_curPrice";
            // 
            // toolStripLabel_totalAsset
            // 
            this.toolStripLabel_totalAsset.Name = "toolStripLabel_totalAsset";
            this.toolStripLabel_totalAsset.Size = new System.Drawing.Size(59, 27);
            this.toolStripLabel_totalAsset.Text = "전체 자산";
            // 
            // dataGridView_Monitoring
            // 
            this.dataGridView_Monitoring.AllowUserToAddRows = false;
            this.dataGridView_Monitoring.AllowUserToDeleteRows = false;
            this.dataGridView_Monitoring.AllowUserToResizeColumns = false;
            this.dataGridView_Monitoring.AllowUserToResizeRows = false;
            this.dataGridView_Monitoring.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Monitoring.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Monitoring_marketInfo,
            this.Monitoring_koreanName,
            this.Monitoring_curPrice,
            this.Monitoring_24H});
            this.dataGridView_Monitoring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Monitoring.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Monitoring.Name = "dataGridView_Monitoring";
            this.dataGridView_Monitoring.RowHeadersVisible = false;
            this.dataGridView_Monitoring.RowTemplate.Height = 23;
            this.dataGridView_Monitoring.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Monitoring.Size = new System.Drawing.Size(688, 395);
            this.dataGridView_Monitoring.TabIndex = 1;
            // 
            // Monitoring_marketInfo
            // 
            this.Monitoring_marketInfo.HeaderText = "종목";
            this.Monitoring_marketInfo.Name = "Monitoring_marketInfo";
            // 
            // Monitoring_koreanName
            // 
            this.Monitoring_koreanName.HeaderText = "이름";
            this.Monitoring_koreanName.Name = "Monitoring_koreanName";
            // 
            // Monitoring_curPrice
            // 
            this.Monitoring_curPrice.HeaderText = "현재가";
            this.Monitoring_curPrice.Name = "Monitoring_curPrice";
            // 
            // Monitoring_24H
            // 
            this.Monitoring_24H.HeaderText = "24H";
            this.Monitoring_24H.Name = "Monitoring_24H";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(708, 467);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "기충매";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosingEvent);
            this.tabControl_Features.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Account)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Monitoring)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_Features;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView_Account;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_START;
        private System.Windows.Forms.ToolStripButton toolStripButton_SelectCoin;
        private System.Windows.Forms.ToolStripButton toolStripButton_STOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_market;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_CoinName;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_profit;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_quant;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_avgPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_curPrice;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_totalAsset;
        private System.Windows.Forms.DataGridView dataGridView_Monitoring;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monitoring_marketInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monitoring_koreanName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monitoring_curPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monitoring_24H;
    }
}