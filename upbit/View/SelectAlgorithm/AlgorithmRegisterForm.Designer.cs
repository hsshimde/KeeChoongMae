using System.Drawing;
using System.Windows.Forms;

namespace upbit.View.SelectAlgorithm
{
    partial class AlgorithmRegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Form mParentForm { get; set; }
        public enum EColContent
        {
            Index,
            MarketInfo,
            Algorithm,
            UseStatus,
            ShouldCutLoss,
            CutLossRatio,
            ShouldTakeProfit,
            TakeProfitRatio
        }



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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView_Algorithm = new System.Windows.Forms.DataGridView();
            this.button_StartAndStop = new System.Windows.Forms.Button();
            this.label＿RunningInfo = new System.Windows.Forms.Label();
            this.textBox_RunningVolume = new System.Windows.Forms.TextBox();
            this.label_RunningVolSet = new System.Windows.Forms.Label();
            this.button_SetVolume = new System.Windows.Forms.Button();
            this.label_CurRunningVolume = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Algorithm)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.37349F));
            this.tableLayoutPanel.Controls.Add(this.dataGridView_Algorithm, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.00873F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1349, 378);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // dataGridView_Algorithm
            // 
            this.dataGridView_Algorithm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Algorithm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Algorithm.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Algorithm.MultiSelect = false;
            this.dataGridView_Algorithm.Name = "dataGridView_Algorithm";
            this.dataGridView_Algorithm.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView_Algorithm.RowHeadersVisible = false;
            this.dataGridView_Algorithm.RowTemplate.Height = 23;
            this.dataGridView_Algorithm.Size = new System.Drawing.Size(1343, 372);
            this.dataGridView_Algorithm.TabIndex = 0;
            // 
            // button_StartAndStop
            // 
            this.button_StartAndStop.Location = new System.Drawing.Point(12, 499);
            this.button_StartAndStop.Name = "button_StartAndStop";
            this.button_StartAndStop.Size = new System.Drawing.Size(105, 45);
            this.button_StartAndStop.TabIndex = 0;
            this.button_StartAndStop.Text = "자동매매 시작";
            this.button_StartAndStop.UseVisualStyleBackColor = true;
            this.button_StartAndStop.Click += new System.EventHandler(this.button_StartAndStop_Click);
            // 
            // label＿RunningInfo
            // 
            this.label＿RunningInfo.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label＿RunningInfo.Location = new System.Drawing.Point(17, 399);
            this.label＿RunningInfo.Name = "label＿RunningInfo";
            this.label＿RunningInfo.Size = new System.Drawing.Size(100, 23);
            this.label＿RunningInfo.TabIndex = 1;
            this.label＿RunningInfo.Text = "대기중";
            this.label＿RunningInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_RunningVolume
            // 
            this.textBox_RunningVolume.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_RunningVolume.Location = new System.Drawing.Point(171, 455);
            this.textBox_RunningVolume.Name = "textBox_RunningVolume";
            this.textBox_RunningVolume.Size = new System.Drawing.Size(100, 26);
            this.textBox_RunningVolume.TabIndex = 3;
            // 
            // label_RunningVolSet
            // 
            this.label_RunningVolSet.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label_RunningVolSet.Location = new System.Drawing.Point(201, 399);
            this.label_RunningVolSet.Name = "label_RunningVolSet";
            this.label_RunningVolSet.Size = new System.Drawing.Size(100, 23);
            this.label_RunningVolSet.TabIndex = 4;
            this.label_RunningVolSet.Text = "운용 금액 설정";
            this.label_RunningVolSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_SetVolume
            // 
            this.button_SetVolume.Location = new System.Drawing.Point(289, 458);
            this.button_SetVolume.Name = "button_SetVolume";
            this.button_SetVolume.Size = new System.Drawing.Size(75, 23);
            this.button_SetVolume.TabIndex = 5;
            this.button_SetVolume.Text = "설정";
            this.button_SetVolume.UseVisualStyleBackColor = true;
            this.button_SetVolume.Click += new System.EventHandler(this.button_SetVolume_Click);
            // 
            // label_CurRunningVolume
            // 
            this.label_CurRunningVolume.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label_CurRunningVolume.Location = new System.Drawing.Point(201, 510);
            this.label_CurRunningVolume.Name = "label_CurRunningVolume";
            this.label_CurRunningVolume.Size = new System.Drawing.Size(100, 23);
            this.label_CurRunningVolume.TabIndex = 6;
            this.label_CurRunningVolume.Text = "현재 설정 금액";
            this.label_CurRunningVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlgorithmRegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 556);
            this.Controls.Add(this.label_CurRunningVolume);
            this.Controls.Add(this.button_SetVolume);
            this.Controls.Add(this.label_RunningVolSet);
            this.Controls.Add(this.textBox_RunningVolume);
            this.Controls.Add(this.label＿RunningInfo);
            this.Controls.Add(this.button_StartAndStop);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "AlgorithmRegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "알고리듬";
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Algorithm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button button_StartAndStop;
        private System.Windows.Forms.DataGridView dataGridView_Algorithm;
        private Label label＿RunningInfo;
        private TextBox textBox_RunningVolume;
        private Label label_RunningVolSet;
        private Button button_SetVolume;
        private Label label_CurRunningVolume;
    }
}