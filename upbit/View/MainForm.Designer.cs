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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_START = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_SelectCoin = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_STOP = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel_totalAsset = new System.Windows.Forms.ToolStripLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl_Features = new System.Windows.Forms.TabControl();
            this.dataGridView_Monitoring = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.dataGridView_KRW = new System.Windows.Forms.DataGridView();
            this.dataGridView_BTC = new System.Windows.Forms.DataGridView();
            this.dataGridView_USDT = new System.Windows.Forms.DataGridView();
            this.dataGridView_Interseted = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl_Features.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Monitoring)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KRW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_BTC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_USDT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Interseted)).BeginInit();
            this.SuspendLayout();
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
            // toolStripLabel_totalAsset
            // 
            this.toolStripLabel_totalAsset.Name = "toolStripLabel_totalAsset";
            this.toolStripLabel_totalAsset.Size = new System.Drawing.Size(59, 27);
            this.toolStripLabel_totalAsset.Text = "전체 자산";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView_Monitoring);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(694, 401);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "투자내역";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tabControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(694, 401);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "거래소";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            // dataGridView_Monitoring
            // 
            this.dataGridView_Monitoring.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Monitoring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Monitoring.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Monitoring.Name = "dataGridView_Monitoring";
            this.dataGridView_Monitoring.RowTemplate.Height = 23;
            this.dataGridView_Monitoring.Size = new System.Drawing.Size(688, 395);
            this.dataGridView_Monitoring.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(688, 395);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView_KRW);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(680, 365);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "KRW";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView_BTC);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(680, 365);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "BTC";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dataGridView_USDT);
            this.tabPage5.Location = new System.Drawing.Point(4, 26);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(680, 365);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "USDT";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.dataGridView_Interseted);
            this.tabPage6.Location = new System.Drawing.Point(4, 26);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(680, 365);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "관심";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // dataGridView_KRW
            // 
            this.dataGridView_KRW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_KRW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_KRW.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_KRW.Name = "dataGridView_KRW";
            this.dataGridView_KRW.RowTemplate.Height = 23;
            this.dataGridView_KRW.Size = new System.Drawing.Size(674, 359);
            this.dataGridView_KRW.TabIndex = 0;
            // 
            // dataGridView_BTC
            // 
            this.dataGridView_BTC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_BTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_BTC.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_BTC.Name = "dataGridView_BTC";
            this.dataGridView_BTC.RowTemplate.Height = 23;
            this.dataGridView_BTC.Size = new System.Drawing.Size(674, 359);
            this.dataGridView_BTC.TabIndex = 0;
            // 
            // dataGridView_USDT
            // 
            this.dataGridView_USDT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_USDT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_USDT.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_USDT.Name = "dataGridView_USDT";
            this.dataGridView_USDT.RowTemplate.Height = 23;
            this.dataGridView_USDT.Size = new System.Drawing.Size(680, 365);
            this.dataGridView_USDT.TabIndex = 1;
            // 
            // dataGridView_Interseted
            // 
            this.dataGridView_Interseted.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Interseted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Interseted.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Interseted.Name = "dataGridView_Interseted";
            this.dataGridView_Interseted.RowTemplate.Height = 23;
            this.dataGridView_Interseted.Size = new System.Drawing.Size(680, 365);
            this.dataGridView_Interseted.TabIndex = 2;
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl_Features.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Monitoring)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KRW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_BTC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_USDT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Interseted)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_START;
        private System.Windows.Forms.ToolStripButton toolStripButton_SelectCoin;
        private System.Windows.Forms.ToolStripButton toolStripButton_STOP;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_totalAsset;
        private System.Windows.Forms.TabControl tabControl_Features;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView_Monitoring;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView_KRW;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView dataGridView_BTC;
        private System.Windows.Forms.DataGridView dataGridView_USDT;
        private System.Windows.Forms.DataGridView dataGridView_Interseted;
    }
}